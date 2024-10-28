
using CorporateWebProject.Application.Dto.Proposal;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.Mail;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class proposalController : Controller
    {
        private readonly MailManager _mailManager;
        private readonly ISmtpSettingRepository _smtpSettingRepository;
        private readonly IContactMessageRepository _contactMessageRepository;
        private readonly IMemoryCache _memoryCache;

        public proposalController(IMemoryCache memoryCache, ISmtpSettingRepository smtpSettingRepository, IContactMessageRepository contactMessageRepository)
        {
            _memoryCache = memoryCache;
            _mailManager = new MailManager();
            _smtpSettingRepository = smtpSettingRepository;
            _contactMessageRepository = contactMessageRepository;
        }

        public async Task<IActionResult> send(ProposalDTO proposal, IFormCollection fc)
        {
            var asdasd = fc["Proposal.Services"];
            ServiceVM model = new ServiceVM(HttpContext, _memoryCache);
            await model.LoadLanguageSettingsAsync(HttpContext);
            var smtpSetting = (await _smtpSettingRepository.GetListAsync()).Data.Last();
            await _mailManager.SendProposalForm(proposal, smtpSetting, model.CurrentSettings);
            if (proposal.Mail != null && proposal.Mail != "" && proposal.Mail.Length > 0)
            {
                await _mailManager.SendThankYouForm(proposal.Mail, smtpSetting, model.CurrentSettings);

            }
            try
            {
                await _contactMessageRepository.AddAsync(new ContactMessages()
                {
                    Mail = proposal.Mail,
                    Message = proposal.Message,
                    Name = proposal.Name,
                    Phone = proposal.Phone,
                    LangId = 1,
                    Surname = proposal.Surname,
                    IpAddress = JsonConvert.SerializeObject(proposal)

                });
            }
            catch (Exception ex)
            {

            }

            return Redirect("/iletisim-onaylandi");
        }
    }
}
