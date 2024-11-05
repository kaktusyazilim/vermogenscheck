using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CorporateWebProject.WebUI.Handlers.Authorization.Attributes
{
    public class AuthHelper
    {
        HttpContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationRepository _authorizationRepository;
        public AuthHelper(HttpContext context)
        {
            var services = context.RequestServices;
            _context = context;
            _authorizationRepository= (IAuthorizationRepository)services.GetService(typeof(IAuthorizationRepository));
            _userRepository= (IUserRepository)services.GetService(typeof(IUserRepository));
        }

      

        public bool IsVerify(string userid,string password,string pageid,string authType)
        {
            var user= _userRepository.Get(x=>x.ItemGuid==userid).Result.Data;
            if(user!=null && user.Password==password)
            {
                if (user.IsSuperAdmin == true) return true;
                var authVerify = _authorizationRepository.Get(x => x.UserId == user.Id && x.PageGuid == pageid).Result.Data;
                switch (authType)
                {
                    case "Read":
                        return authVerify.AuthRead;
                    case "Create":
                        return authVerify.AuthCreate;
                    case "Update":
                        return authVerify.AuthUpdate;
                    case "Delete":
                        return authVerify.AuthDelete;
                    default:
                        return false;
                }
            }
            return false;

        }
        public bool IsClientVerify(string userid, string pageid, string authType)
        {
            var services = _context.RequestServices;
            var companyRepository= (ICompanyRepository)services.GetService(typeof(ICompanyRepository));

            var user = companyRepository.Get(x => x.ItemGuid == userid).Result.Data;
            if (user != null)
            {
                return true;
                var authVerify = _authorizationRepository.Get(x => x.UserId == user.Id && x.PageGuid == pageid).Result.Data;
                switch (authType)
                {
                    case "Read":
                        return authVerify.AuthRead;
                    case "Create":
                        return authVerify.AuthCreate;
                    case "Update":
                        return authVerify.AuthUpdate;
                    case "Delete":
                        return authVerify.AuthDelete;
                    default:
                        return false;
                }
            }
            return false;

        }

    }
}
