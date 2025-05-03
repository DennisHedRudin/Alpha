using Business.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebbApp_Alpha.Controllers;


[Authorize]
public class AdminController(IMemberService memberService, IClientService clientService) : Controller
{
    private readonly IMemberService _memberService = memberService;    
    private readonly IClientService _clientService = clientService;
   

    public async Task<IActionResult> Members()
    {
        var result = await _memberService.GetMembersAsync();
        
        var members = result.Result ?? Enumerable.Empty<Member>();
        return View(members); 
    }

    
    public async Task<IActionResult> Clients()
    {
        var result = await _clientService.GetClientsAsync();
        
        var clients = result.Result ?? Enumerable.Empty<Domain.Models.Client>();
        return View(clients);  
    }

    
    public IActionResult Projects()
        => RedirectToAction("Index", "Projects");
}



