using CorporateWebProject.Application.Parameters;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Common;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Authorization;
using Microsoft.AspNetCore.Mvc;
using CorporateWebProject.WebUI.Handlers.Images;
using System.Reflection;
using SkiaSharp;
using System.IO;
using CorporateWebProject.Application.Utilities;
using CorporateWebProject.Domain.Entities;

namespace CorporateWebProject.WebUI.Handlers.Pages
{
    [Area("manager")]
    public class BaseController<T> : Controller where T : EntityBase
    {
        public readonly object _dynamicData;
        public string _pageGuid = "";
        private IWebHostEnvironment _hostEnvironment;
        string basePath = "";
        string rootPath = "";

        public BaseController(object dynamicData, string pageGuid)
        {
            _dynamicData = dynamicData;
            _pageGuid = pageGuid;

        }
        public object SetResponseMessage(bool result) => result ? TempData["info"] = ResponseMessage.SuccessMessage : TempData["warning"] = ResponseMessage.ErrorResponse;
        public IActionResult Passive(string id)
        {

            try
            {
                var CURRENT_SESSION = HttpContext.Request.Cookies["CURRENT_SESSION"];
                var user = AesEncryption<Users>.Decrypt(CURRENT_SESSION);
                AuthHelper authHelper = new AuthHelper(HttpContext);
                var isVerify = authHelper.IsVerify(user.ItemGuid,user.Password, _pageGuid, AuthTypes.Passive);
                if (!isVerify)
                {
                    return Redirect("/manager/403");
                }
                var repository = _dynamicData as IBaseRepository<T>;
                var model = repository!.Get(x => x.ItemGuid == id).GetAwaiter().GetResult().Data;
                if (model != null)
                {
                    model.IsPassive = !model.IsPassive;
                    this.SetResponseMessage(repository.UpdateAsync(model).GetAwaiter().GetResult().Success);
                    return Json("Y");
                }
                return Json("N");
            }
            catch (Exception ex)
            {
                return Json("N");

            }
        }

        public IActionResult Delete(string id)
        {
            try
            {
                var CURRENT_SESSION = HttpContext.Request.Cookies["CURRENT_SESSION"];
                var user = AesEncryption<Users>.Decrypt(CURRENT_SESSION);
                AuthHelper authHelper = new AuthHelper(HttpContext);
                var isVerify = authHelper.IsVerify(user.ItemGuid,user.Password, _pageGuid, AuthTypes.Delete);
                if (!isVerify)
                {
                    return Redirect("/manager/403");
                }

                var repository = _dynamicData as IBaseRepository<T>;
                var model = repository!.Get(x => x.ItemGuid == id).GetAwaiter().GetResult().Data;
                if (model != null)
                {
                    model.IsDeleted = true;
                    this.SetResponseMessage(repository.UpdateAsync(model).GetAwaiter().GetResult().Success);
                    return Json("Y");
                }
                return Json("N");
            }
            catch (Exception ex)
            {
                return Json("N");

            }


        }

        public ImageResult CreateFile(IFormFile file, string fileSourcePath = "/uploads/")
        {
            fileSourcePath = string.IsNullOrEmpty(fileSourcePath) ? "/uploads/" : fileSourcePath;
            var services = HttpContext.RequestServices;
            _hostEnvironment = (IWebHostEnvironment)services.GetService(typeof(IWebHostEnvironment));
            rootPath = _hostEnvironment.WebRootPath;

            string filePath = "";
            string webpFilePath = "";
            try
            {
                // Rastgele dosya adı oluşturma
                var tick = DateTime.Now.Ticks.ToString();
                var randomTicks = tick.Substring(tick.Length - 6, 6);
                var randomFileName = randomTicks + Path.GetFileNameWithoutExtension(file.FileName) + ".webp";
                filePath = Path.Combine(rootPath + fileSourcePath.Replace("/", @"\") + randomFileName);

                // Dosya okuma ve SkiaSharp ile webp'ye dönüştürme
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();

                    // SkiaSharp ile webp formatına dönüştürme
                    var webpBytes = ConvertToWebP(imageBytes, 1500, 1500, 75); // Genişlik: 800, Yükseklik: 600, Kalite: %80

                    // WebP dosyasını kaydetme
                    webpFilePath = Path.Combine(rootPath + fileSourcePath.Replace("/", @"\") + randomFileName);
                   System.IO.File.WriteAllBytes(webpFilePath, webpBytes);
                }

                return new ImageResult()
                {
                    FileName = randomFileName,
                    CurrentFile = file,
                    Path = fileSourcePath + randomFileName,
                    Statu = true,
                    StatuMessage = "Success",
                    FullPath = rootPath + fileSourcePath + randomFileName
                };
            }
            catch (Exception ex)
            {
                return new ImageResult()
                {
                    FileName = file.FileName,
                    CurrentFile = file,
                    Path = filePath,
                    Statu = false,
                    StatuMessage = ex.Message
                };
            }
        }

        // WebP dönüştürme metodu (Orijinal en-boy oranını koruyarak)
        private byte[] ConvertToWebP(byte[] imageBytes, int maxWidth, int maxHeight, int quality = 75)
        {
            using var inputStream = new MemoryStream(imageBytes);
            using var bitmap = SKBitmap.Decode(inputStream);

            // Orijinal genişlik ve yükseklik
            int originalWidth = bitmap.Width;
            int originalHeight = bitmap.Height;

            // Orijinal en-boy oranını koruyarak hedef genişlik ve yükseklik hesapla
            double ratioX = (double)maxWidth / originalWidth;
            double ratioY = (double)maxHeight / originalHeight;
            double ratio = Math.Min(ratioX, ratioY);

            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Resmi yeniden boyutlandır
            using var resizedBitmap = bitmap.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.High);
            using var image = SKImage.FromBitmap(resizedBitmap);

            // WebP formatında sıkıştırılmış veriyi almak için SKData kullanın
            using var data = image.Encode(SKEncodedImageFormat.Webp, quality);
            return data.ToArray();
        }



        public void Equalize(object destinationObject, object sourceObject)
        {
            string[] dontControl = new string[] { "Id", "CreateDate", "ModifiedDate", "IsPassive", "IsDeleted",  };
            foreach (var property in sourceObject.GetType().GetProperties())
            {
                var value = property.GetValue(sourceObject);
                if(!dontControl.Contains(property.Name) )
                {
                    if (value == null)
                    {
                        object defaultValue = GetDefault(property);
                        destinationObject.GetType().GetProperty(property.Name)?.SetValue(destinationObject, defaultValue);
                    }
                    else if (value!=null && value.ToString()== "1.01.0001 00:00:00")
                    {
                        object defaultValue = GetDefault(property);
                        destinationObject.GetType().GetProperty(property.Name)?.SetValue(destinationObject, defaultValue);

                    }
                    else
                    {
                        destinationObject.GetType().GetProperty(property.Name)?.SetValue(destinationObject, value);
                    }
                }
               
            }
        }
        private object GetDefault(PropertyInfo propertyInfo)
        {

            try
            {
                if (propertyInfo.PropertyType.FullName.Contains("System.Int32"))
                {
                    return 0;
                }
                else if (propertyInfo.PropertyType.FullName.Contains("System.DateTime"))
                {
                    return DateTime.Now;
                }
                else if (propertyInfo.PropertyType.FullName.Contains("System.String"))
                {
                    return "";
                }
                else if (propertyInfo.PropertyType.FullName.Contains("System.Boolean"))
                {
                    return false;
                }
                return 0;
            }
            catch (Exception)
            {
                return null;
            }

        }


    }
}
