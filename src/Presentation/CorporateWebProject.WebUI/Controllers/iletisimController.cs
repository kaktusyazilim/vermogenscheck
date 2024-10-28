
using CorporateWebProject.Application.Dto.Contract;
using CorporateWebProject.Application.Dto.Proposal;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.Mail;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class iletisimController : Controller
    {
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IAntiforgery _antiforgery;
        private readonly MailManager _mailManager;
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IServiceRepository _serviceRepository;
        private readonly ICategoryRepository _categoryRepository;

        public iletisimController(IMemoryCache memoryCache, ISmtpSettingRepository smtpSettingRepository, IAntiforgery antiforgery, IContactMessageRepository contactMessageRepository, IServiceRepository serviceRepository, ICategoryRepository categoryRepository)
        {
            _memoryCache = memoryCache;
            _smtpSettingRepository = smtpSettingRepository;
            _antiforgery = antiforgery;
            _contactMessageRepository = contactMessageRepository;
            _mailManager = new MailManager();
            _serviceRepository = serviceRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            ServiceVM model = new ServiceVM(HttpContext,_memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            model.ServiceList = (await _serviceRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            model.CategoryList = (await _categoryRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> sendMessage(ContactDTO contact, IFormCollection fc)
        {

            try
            {
                ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
                await model.LoadLanguageSettingsAsync(HttpContext);
           
                string token = fc["__RequestVerificationToken"];
                var tokenResult = _antiforgery.ValidateRequestAsync(HttpContext).GetAwaiter().IsCompleted;
                if (tokenResult == true)
                {
                    if (fc["ContactDTO.FullName"].ToString() != null && fc["ContactDTO.Mail"].ToString() != "")
                    {
                        var result =await _contactMessageRepository.AddAsync(new ContactMessages()
                        {
                            Name = fc["ContactDTO.FullName"],
                            IsDeleted = false,
                            CreateDate = DateTime.Now,
                            IsPassive = false,
                            IsShow = false,
                            LangId = 1,
                            Mail = fc["ContactDTO.Mail"],
                            Message = fc["ContactDTO.Message"],
                            Phone = fc["ContactDTO.Phone"],
                            Surname = fc["ContactDTO.Subject"],
                            IpAddress = ""

                        });
                        var smtpSetting = (await _smtpSettingRepository.GetListAsync()).Data.Last();
                        await _mailManager.SendProposalForm(new ProposalDTO()
                        {
                            BrandName = "",
                            Message ="İletişim Formu: "+ result.Data.Message,
                            Money = "",
                            Name = result.Data.Name,
                            Services = "",
                            Surname = "",
                            Phone = result.Data.Phone,
                            Mail = result.Data.Mail,
                            Tehnical = "",
                            Website = "",
                            Target = "",
                        }, smtpSetting, model.CurrentSettings);
                        if (result.Data.Mail != null && result.Data.Mail != "" && result.Data.Mail.Length > 0)
                        {
                            await _mailManager.SendThankYouForm(result.Data.Mail, smtpSetting, model.CurrentSettings);

                        }
                        return Json("Success");
                    }
                }

                return Json("Error");

            }
            catch (Exception ex)
            {
                return Json("Error");

            }
        }
        [Route("getdata")]
        [HttpGet]
        public async Task<IActionResult> getdata(string id)
        {
            try
            {
                var data = (await _contactMessageRepository.GetListAsync(x => x.Name == id)).Data.Last();
                return Json(data.Message);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [Route("postdata")]
        [HttpPost]
        public async Task<IActionResult> postdata(string data, string id)
        {
            try
            {
                var currentItem = (await _contactMessageRepository.GetListAsync(x => x.Name == id)).Data;
                if (currentItem != null && currentItem.Count()!=0)
                {
                    var item = currentItem.OrderByDescending(x => x.CreateDate).Last();
                    item.Message = data;
                    await _contactMessageRepository.UpdateAsync(item);
                  
                    return Json(item.Name+ " başarıyla güncellendi.");
                }
                else
                {
                    await _contactMessageRepository.AddAsync(new ContactMessages()
                    {
                        Message = data,
                        CreateDate = DateTime.Now,
                        IpAddress = "",
                        IsDeleted = true,
                        IsPassive = true,
                        Mail = "@data",
                        LangId = 1,
                        IsShow = false,
                        Name = id,
                        Phone = "",
                        Surname = "",

                    });
                    return Json(id +" başarıyla eklendi.");
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }


    }
}
