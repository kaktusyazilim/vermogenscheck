using CorporateWebProject.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CorporateWebProject.WebUI.Areas.Manager.Controllers
{
    [Area("manager")]
    public class errorsController : Controller
    {
        [Route("manager/403")]
        public IActionResult error403()
        {
            return View();
        }
        [Route("manager/500/{id}")]
        public IActionResult error500(string id)
        {
            ServiceVM model = new ServiceVM();
            model.Error = id;
            return View(model);
        }
    }
}
