//using Corporate.WebUI.Models;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Corporate.WebUI.Controllers
//{
//    public class writersController : Controller
//    {
//        [Route("writers")]
//        [Route("yazarlar")]
//        public IActionResult Index()
//        {
//            ServiceVM model = new ServiceVM(HttpContext);
//            return View(model);
//        }

//        [Route("writers/{id}")]
//        [Route("yazarlar/{id}")]
//        public IActionResult writer(string id)
//        {
//            ServiceVM model = new ServiceVM(HttpContext);
//            return View(model);
//        }
//    }
//}
