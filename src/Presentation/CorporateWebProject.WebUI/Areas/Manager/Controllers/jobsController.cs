using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Handlers.Authorization.Attributes;
using CorporateWebProject.WebUI.Handlers.Pages;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    public class jobsController : BaseController<Jobs>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICareerRepository _careerRepository;
        private readonly IMemoryCache _memoryCache;

        private readonly ICompanyRepository _companyRepository;
        public jobsController(IUserRepository userRepository, IJobRepository jobRepository, ICareerRepository careerRepository, ICompanyRepository companyRepository, IMemoryCache memoryCache) : base(jobRepository, AuthPage.Jobs)
        {
            _userRepository = userRepository;
            _jobRepository = jobRepository;
            _careerRepository = careerRepository;
            _companyRepository = companyRepository;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        [Auth("Read", AuthPage.Jobs)]
        public async Task<IActionResult> Index()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.JobList = (await _jobRepository.GetListAsync()).Data.OrderByDescending(x => x.StartDate).ToList();
            model.CareerList=(await _careerRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.CompanyList = (await _companyRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }
        [HttpGet]
        [Auth("Read", AuthPage.Jobs)]
        public async Task<IActionResult> details(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.JobList = (await _jobRepository.GetListAsync()).Data;
            model.CareerList = (await _careerRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.CompanyList = (await _companyRepository.GetListAsync()).Data;
            model.Job = (await _jobRepository.Get(x => x.ItemGuid == id)).Data;
            model.Company = (await _companyRepository.Get(x => x.ItemGuid == model.Job.CompanyGuid)).Data;
            return View(model);
        }

        [HttpGet]
        [Auth("Read", AuthPage.Jobs)]
        public async Task<IActionResult> applications()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.JobList = (await _jobRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            model.CareerList = (await _careerRepository.GetListAsync(x => x.IsDeleted == false)).Data;
            return View(model);
        }


        [HttpGet]
        [Auth("Create", AuthPage.Jobs)]
        public async Task<IActionResult> Create()
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CompanyList=(await _companyRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Create", AuthPage.Jobs)]
        public async Task<IActionResult> Create(ServiceVM model, IFormCollection fc)
        {
            var company = (await _companyRepository.Get(x => x.ItemGuid == model.Job.CompanyGuid)).Data;
            model.Job.CompanyGuid = company.ItemGuid;
            var result = await _jobRepository.AddAsync(model.Job);
            base.SetResponseMessage(result.Success);
            return Redirect("/manager/jobs");
        }

        [HttpGet]
        [Auth("Update", AuthPage.Jobs)]
        public async Task<IActionResult> Update(string id)
        {
                        ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            model.CompanyList = (await _companyRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.Job=(await _jobRepository.Get(x => x.ItemGuid == id)).Data;
            return View(model);
        }

        [HttpPost]
        [Auth("Update", AuthPage.Jobs)]
        public async Task<IActionResult> Update(ServiceVM model, IFormCollection fc)
        {
            var currentItem=_jobRepository.Get(x=>x.ItemGuid==model.Job.ItemGuid).Result.Data;
            if(currentItem!=null)
            {
                currentItem.StartDate=model.Job.StartDate;
                currentItem.IsUrgent = model.Job.IsUrgent;
                currentItem.Title=model.Job.Title;
                currentItem.SortDescription=model.Job.SortDescription;
                currentItem.EndDate=model.Job.EndDate;
                currentItem.FullDescription=model.Job.FullDescription;
                currentItem.IsUrgent=model.Job.IsUrgent;
                currentItem.CompanyGuid=model.Job.CompanyGuid;
                currentItem.Mail  = model.Job.Mail;
                currentItem.Phone= model.Job.Phone;
                currentItem.TimeType=model.Job.TimeType;
                var result = await _jobRepository.UpdateAsync(currentItem);
                base.SetResponseMessage(result.Success);
                return Redirect("/manager/jobs");
            }
            return View(model);
        }
    }
}