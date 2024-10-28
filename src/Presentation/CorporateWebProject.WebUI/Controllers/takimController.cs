//using Corporate.BL.Abstract;
//using Corporate.WebUI.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Corporate.WebUI.Controllers
//{
//    public class takimController : Controller
//    {
//        private readonly ITeamService _teamService;

//        public takimController(ITeamService teamService)
//        {
//            _teamService = teamService;
//        }

//        public IActionResult Index()
//        {
//            ServiceVM model = new ServiceVM(HttpContext);
//            model.TeamList = _teamService.GetList(x => x.IsPassive == false && x.IsDeleted == false).Data.OrderBy(x => x.Queue).ToList();
//            return View(model);
//        }
//    }
//}
