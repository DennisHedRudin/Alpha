
using Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebbApp_Alpha.Controllers;


//[Authorize]
public class AdminController(IMemberService memberService) : Controller
{
    private readonly IMemberService _memberService = memberService;

    public IActionResult Projects()
    {
        return View();
    }

    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Members()
    {
        var members = await _memberService.GetMembersAsync();

        return View(members);
    }

    //[Authorize(Roles = "admin")]
    public IActionResult Clients()
    {
        return View();
    }

  
}
