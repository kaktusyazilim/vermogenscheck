using CorporateWebProject.Application.Repositories;
using Microsoft.AspNetCore.Mvc.Routing;
using Newtonsoft.Json.Linq;
using System;

namespace CorporateWebProject.WebUI.Handlers.Route
{
    public class RouteManager : DynamicRouteValueTransformer
    {
        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            var services = httpContext.RequestServices;
            var categoryRepository = (ICategoryRepository)services.GetService(typeof(ICategoryRepository));
            var subcategoryRepository = (ISubCategoryRepository)services.GetService(typeof(ISubCategoryRepository));
            var serviceRepository = (IServiceRepository)services.GetService(typeof(IServiceRepository));

            var routeValue = values["RouteValue"].ToString();

            var categoryControl=categoryRepository.Get(x=>x.FriendlyUrl== routeValue).Result.Data;

            if(categoryControl != null)
            {
                values["controller"] = "pages";
                values["action"] = "index";
                values["id"] = categoryControl.FriendlyUrl;
                values["type"] = "category";
                values["langid"] = values.ContainsKey("lang") ? values["lang"] : "tr";
                return values;
            }


            var subcategoryControl = subcategoryRepository.Get(x => x.FriendlyUrl == routeValue).Result.Data;
            if (subcategoryControl != null)
            {
                values["controller"] = "pages";
                values["action"] = "index";
                values["id"] = subcategoryControl.FriendlyUrl;
                values["type"] = "subcategory";
                values["langid"] = values.ContainsKey("lang") ? values["lang"] : "tr";
                return values;
            }
            var serviceControl=serviceRepository.Get(x=>x.FriendlyUrl== routeValue).Result.Data;
            if (serviceControl != null)
            {
                values["controller"] = "hizmetler";
                values["action"] = "details";
                values["id"] = values["RouteValue"].ToString();
                return values;

                
            }
            //return await Task.Run(() =>
            //{
            //    return ValueResult(values);
            //});

            
            
            return values;
        }

        
    }
}
