using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("members")]
        public IActionResult Members()
        {
            return View();
        }
    }
}
