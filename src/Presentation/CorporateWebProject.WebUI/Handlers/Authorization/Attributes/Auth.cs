using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities;
using CorporateWebProject.Domain.Common;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CorporateWebProject.WebUI.Handlers.Authorization.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class Auth : Attribute, IActionFilter
    {
        private readonly string _method;
        private readonly string _areaType;
        public string pageGuid { get; set; }

        public Auth(string customParameter, string pageGuid, string areaType = "manager")
        {
            this._method = customParameter;
            this.pageGuid = pageGuid;
            this._areaType = areaType;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            if (_method == "Create")
            {
                var entity = context.ActionArguments.FirstOrDefault().Value as ServiceVM;
                if (entity != null)
                {
                    try
                    {
                        SetNullOrEmptyPropertiesToEmpty(entity);
                    }
                    catch (Exception ex)
                    {
                        // Handle exception if necessary
                    }
                }
            }

            if (this._method == "All")
            {
                return;
            }
            else
            {
                if (_areaType == "Client")
                {
                    var user = context.HttpContext.Request.Cookies["clientGuid"];
                    var authHelper = new AuthHelper(context.HttpContext);
                    var result = authHelper.IsClientVerify(user, this.pageGuid, this._method);
                    if (!result)
                        context.HttpContext.Response.Redirect("/manager/403");
                }
                else
                {
                    var CURRENT_SESSION = context.HttpContext.Request.Cookies["CURRENT_SESSION"];
                    if (string.IsNullOrEmpty(CURRENT_SESSION))
                    {
                        context.HttpContext.Response.Clear();
                        context.HttpContext.Response.StatusCode = 403; // İsteğin sonucu bir sunucu hatası olduğunu belirtir.
                        context.HttpContext.Response.Redirect("/manager/403", false);
                        await context.HttpContext.Response.CompleteAsync();
                    }
                    else
                    {
                        var user = AesEncryption<Users>.Decrypt(CURRENT_SESSION);
                        var authHelper = new AuthHelper(context.HttpContext);
                        var result = authHelper.IsVerify(user.ItemGuid, user.Password, this.pageGuid, this._method);
                        if (!result)
                        {
                            context.HttpContext.Response.Clear();
                            context.HttpContext.Response.StatusCode = 403; // İsteğin sonucu bir sunucu hatası olduğunu belirtir.
                            context.HttpContext.Response.Redirect("/manager/403", false);
                            await context.HttpContext.Response.CompleteAsync();
                        }
                    }
                    

                    // **Modul ve Page verilerini yükleme**
                    //var httpContext = context.HttpContext;
                    //var memoryCache = httpContext.RequestServices.GetService<IMemoryCache>();
                    //ServiceVM model = new ServiceVM(httpContext, memoryCache);

                    //await model.LoadLanguageSettingsAsync(); // Modül ve sayfa verilerini yükle
                    //var controller = context.Controller as Controller;
                    //if (controller != null)
                    //{
                    //    controller.ViewData["Modul"] = model.Modul;
                    //    controller.ViewData["Page"] = model.Page;
                    //}
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ViewResult)
            {
                var myResult = (ViewResult)context.Result!;
                if (myResult != null)
                {
                    var serviceVm = (ServiceVM)myResult.Model!;
                    if(serviceVm!=null)
                    {
                        serviceVm.LoadModulAndPageDataAsync(context.HttpContext).Wait();
                        serviceVm.LoadUserDataAsync(context.HttpContext).Wait();
                    }
               
                }
            }



            // Action işleminden sonra istediğiniz işlemleri gerçekleştirin
        }

        public static void SetNullOrEmptyPropertiesToEmpty(ServiceVM entity)
        {
            var properties = typeof(ServiceVM).GetProperties().Where(p => p.GetValue(entity) != null);

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = (string)property.GetValue(entity);
                    if (value == null)
                    {
                        property.SetValue(entity, string.Empty);
                    }
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var propertyValue = property.GetValue(entity);
                    if (propertyValue == null)
                    {
                        var newInstance = Activator.CreateInstance(property.PropertyType);
                        property.SetValue(entity, newInstance);
                    }
                    else
                    {
                        Type valueType = propertyValue.GetType();
                        PropertyInfo[] valueProperties = valueType.GetProperties();
                        foreach (var valueProperty in valueProperties)
                        {
                            string propertyName = valueProperty.Name;
                            object propValue = valueProperty.GetValue(propertyValue);
                            if (propValue == null)
                            {
                                valueProperty.SetValue(propertyValue, "");
                            }
                        }
                    }
                }
            }
        }
    }
}
