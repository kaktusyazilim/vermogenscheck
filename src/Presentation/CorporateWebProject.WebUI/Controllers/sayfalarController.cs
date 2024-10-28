using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Corporate.WebUI.Controllers
{
    public class sayfalarController : Controller
    {
        [HttpGet("sayfalar/{id}")]
        public IActionResult Index(string id)
        {
            return View();
        }
        [HttpGet("sik-sorulan-sorular")]
        [HttpGet("sss")]
        public IActionResult sss(string id)
        {
            return View();
        }
    }
}
