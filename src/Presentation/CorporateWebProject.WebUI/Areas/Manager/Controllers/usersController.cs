using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.Security;
using CorporateWebProject.Application.Utilities.WebSettings;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Images;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class usersController : BaseController<Users>
    {
        private readonly IUserRepository _userRepository;
        private readonly IModulRepository _modulRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IMemoryCache _memoryCache;
        public usersController(IUserRepository userRepository, IModulRepository modulRepository, IAuthorizationRepository authorizationRepository, IPageRepository pageRepository, IMemoryCache memoryCache) : base(userRepository, AuthPage.Files)
        {
            _userRepository = userRepository;
            _modulRepository = modulRepository;
            _authorizationRepository = authorizationRepository;
            _pageRepository = pageRepository;
            _memoryCache = memoryCache;
        }
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.UserList = (await _userRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Read",AuthPage.Files)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.ModulList = (await _modulRepository.GetListAsync(x => x.IsDeleted == false && x.IsPassive == false)).Data;
            model.AuthorizationList = new List<Authorizations>();
            model.PageList= (await _pageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            foreach (var modul in model.ModulList)
            {
                foreach (var item in model.PageList.Where(x => x.ModulId == modul.Id).ToList())
                {
                    
                        model.AuthorizationList.Add(new Authorizations
                        {
                           
                            PageId = item.Id,
                            ModulId = Convert.ToInt32(item.ModulId),
                            PageGuid = item.PageGuid,
                            AuthCreate = false,
                            AuthDelete = false,
                            AuthRead = false,
                            AuthUpdate = false,

                        });

                }
            }
            return View(model);
        }

        [HttpPost]
        [Auth("Read", AuthPage.Files)]
        public IActionResult Create(ServiceVM model,IFormCollection fc,List<Authorizations> AuthorizationList)
        {
            model.User.DisplayName = model.User.Name + " " + model.User.Surname;
            model.User.IsSuperAdmin = false;
            model.User.BirthDate = DateTime.Now;
            model.User.CompanyPhone = "";
            model.User.IsSuperAdmin = true;
            model.User.IsManager = false;
            model.User.IsEmployee = true;
            model.User.JobStartDate = DateTime.Now;

            model.User.Password = HashingHelper.CreatePasswordHash(model.User.Password, model.User.Mail);
            if (fc.Files["files"]!=null)
            {
                var imageResult = base.CreateFile(fc.Files["files"]);
                model.User.Photo = imageResult.Path;
            }
            else
            {
                model.User.Photo = "/uploads/default-user.jpg";

            }
            var result = _userRepository.AddAsync(model.User).Result;
            base.SetResponseMessage(result.Success);
            foreach (var item in AuthorizationList)
            {
                item.UserId = result.Data.Id;
                _authorizationRepository.AddAsync(item);
            }
            return Redirect("/manager/users");
        }


        [HttpGet]
        [Auth("Read", AuthPage.Files)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.User=_userRepository.Get(x=>x.ItemGuid==id).Result.Data;
            model.ModulList = (await _modulRepository.GetListAsync()).Data;
            model.PageList = (await _pageRepository.GetListAsync()).Data;
            model.AuthorizationList = new List<Authorizations>();
            foreach (var modul in model.ModulList)
            {
                foreach (var item in model.PageList.Where(x => x.ModulId == modul.Id).ToList())
                {
                    var currentAuth = model.AuthorizationList.FirstOrDefault(x => x.PageId == item.Id && x.ModulId == modul.Id);
                    if(currentAuth == null)
                    {
                        model.AuthorizationList.Add(new Authorizations
                        {

                            PageId = item.Id,
                            ModulId = Convert.ToInt32(item.ModulId),
                            PageGuid = item.PageGuid,
                            AuthCreate = false,
                            AuthDelete = false,
                            AuthRead = false,
                            AuthUpdate = false,

                        });
                    }
                  

                }
            }
            model.CurrentAuthorizationList = (await _authorizationRepository.GetListAsync(x => x.UserId == model.User.Id)).Data;
      
            return View(model);
        }

        [HttpPost]
        [Auth("Read", AuthPage.Files)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc, List<Authorizations> AuthorizationList)
        {
               await model.FillDataAsync(HttpContext);
            var currentUser = _userRepository.Get(x => x.ItemGuid == model.User.ItemGuid).Result.Data;
            if(currentUser!=null)
            {
                if(model.CurrentUser.IsAuthRegulation==true)
                {
                    var authList = (await _authorizationRepository.GetListAsync(x => x.UserId == currentUser.Id)).Data;
                    foreach (var item in authList)
                    {
                        await _authorizationRepository.Delete(item);
                    }
                    foreach (var item in model.AuthorizationList)
                    {
                        item.UserId = currentUser.Id;
                        var asdad = _authorizationRepository.AddAsync(item).Result;
                    }
                }
              
                currentUser.Surname = model.User.Surname;
                currentUser.DisplayName = model.User.Name + " " + model.User.Surname;
                currentUser.Name=model.User.Name;
                currentUser.Surname =model.User.Surname;
                currentUser.Job=    model.User.Job;
                currentUser.Phone = model.User.Phone;
                currentUser.Mail = model.User.Mail; 
                if(model.User.Password!= null)
                {
                    currentUser.Password = HashingHelper.CreatePasswordHash(model.User.Password, model.User.Mail);

                }
                if (fc.Files["files"] != null)
                {
                    var imageResult = base.CreateFile(fc.Files["files"]);
                    currentUser.Photo = imageResult.Path;
                }
                var result=await _userRepository.UpdateAsync(currentUser);
                base.SetResponseMessage(result.Success);
                if (WebSettings.SiteType=="CRM")
                {
                    return Redirect("/manager/users/update?id="+currentUser.ItemGuid+"&success=true");

                }
                else
                {
                    return Redirect("/manager/users");

                }
              

            }
            return View();
        }
    }
}
