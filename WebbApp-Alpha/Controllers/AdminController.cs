using Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers
{
    //[Route("admin")]
    public class AdminController : Controller
    {
        [Route("members")]
        public IActionResult Members()
        {
            return View();
        }

        [HttpGet]
        //[Route("clients")]
        public IActionResult Clients()
        {
            return View();
        }

        [HttpPost]        

        public IActionResult AddClient(AddClientForm form)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("clients");

            return View();
        }
    }
}
