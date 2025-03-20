using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.Models;

namespace WebbApp_Alpha.Controllers;

[Route("/AddClients")]
public class ClientsController : Controller
{
   
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

    
    public IActionResult Create(ClientCreateFormModel model)
    { 
        if(!ModelState.IsValid)
            return View(model);



        return View();
    }
}
