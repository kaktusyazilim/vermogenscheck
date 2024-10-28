using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.Security;
using CorporateWebProject.WebUI.Handlers.Authorization;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using NetOpenX.Rest.Client.Model;
using NetOpenX.Rest.Client;
using Newtonsoft.Json.Linq;
using CorporateWebProject.Application.Utilities.WebSettings;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using CorporateWebProject.Application.Utilities;
using CorporateWebProject.Domain.Entities;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class loginController : Controller
    {

        private readonly IUserRepository _userRepository;
        private oAuth2 _oAuth2;
        private readonly IMemoryCache _memoryCache;

        public loginController(IUserRepository userRepository, IWebHostEnvironment webHostEnvironment, IMemoryCache memoryCache)
        {

            //_oAuth2 = new oAuth2("http://78.188.223.60:7070");

            //_oAuth2.Login(new JLogin()
            //{

            //    BranchCode = 0,   //sube kodu bilgisi
            //    NetsisUser = "netsis", //netsis kullanıcı adı bilgisi
            //    NetsisPassword = "NET1", //netsis şifre bilgisi
            //    DbType = JNVTTipi.vtMSSQL, //veritabanı tipi
            //    DbName = "BEGOS2024", //şirket bilgisi
            //    DbPassword = "", //veritabanı şifre bilgisi
            //    DbUser = "TEMELSET" //veritabanı kullanıcı adı bilgisi
            //});




            //var token = _oAuth2.AccessToken;
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            var CURRENT_SESSION = HttpContext.Request.Cookies["CURRENT_SESSION"];
            model.CurrentUser = AesEncryption<Users>.Decrypt(CURRENT_SESSION);
            if (model.CurrentUser != null)
            {
               
                return Redirect("/manager/dashboard");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ServiceVM model)
        {
            var userList = (await _userRepository.CountAsync()).Data;
            if(userList==0)
            {
                var createResult = await _userRepository.AddAsync(new Users()
                {
                    BirthDate = DateTime.Now,
                    CompanyPhone = "",
                    CreateDate = DateTime.Now,
                    DisplayName = "",
                    IsAuthRegulation = true,
                    IsDeleted = false,
                    IsEmployee = false,
                    IsListHide = false,
                    IsManager = true,
                    IsPassive = false,
                    IsSuperAdmin = true,
                    IsWorkTable = true,
                    Job = "",
                    JobStartDate = DateTime.Now,
                    Mail = model.LoginDTO.Username,
                    Name = "",
                    ModifiedDate = DateTime.Now,
                    Password = HashingHelper.CreatePasswordHash(model.LoginDTO.Password, model.LoginDTO.Username),
                    Phone = "",
                    Photo = "/uploads/default.png",
                    Surname = "",


                });
            }
            var user = (await _userRepository.Get(x => x.Mail.ToLower() == model.LoginDTO.Username.ToLower() && !x.IsDeleted && !x.IsPassive)).Data;
            if (user != null)
            {
                var correctHast = HashingHelper.VerifyPasswordHash(model.LoginDTO.Password, model.LoginDTO.Username.ToLower(), user.Password);
                if (correctHast)
                {
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddYears(1);
                    string key = "CURRENT_SESSION";
                    string value = AesEncryption<Users>.Encrypt(user);
                    Response.Cookies.Append(key, value, cookie);
                    return Redirect("/manager/dashboard");
                }
                else TempData["failed"] = "E-posta adresi veya şifreniz hatalı.";
            }
            else TempData["failed"] = "Bu e-posta adresine kayıtlı bir kullanıcı bulunamadı.";

            return View();
        }

        public IActionResult logout()
        {
            Response.Cookies.Delete("userGuid");
            return Redirect("/manager/login");
        }
    }
}
