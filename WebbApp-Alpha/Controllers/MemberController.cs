using Business.Services;
using Domain.Extensions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels.Members;

namespace WebbApp_Alpha.Controllers;

public class MemberController(MemberService memberService) : Controller
{
    private readonly MemberService _memberService = memberService;

    [HttpPost]
    public async Task<IActionResult> AddMember(AddMemberForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        var member = form.MapTo<MemberSignUpModel>();

        var result = await _memberService.CreateMemberAsync(member);
        if (result.Success)
        {
            return Ok(new { success = true });
        }
        else
        {
            return Problem("Unable to submit data.");
        }
        


    }


    [HttpPost]
    public IActionResult EditMember(EditMemberForm form)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(x => x.ErrorMessage).ToArray()
                );

            return BadRequest(new { success = false, errors });
        }

        //var result = await _clientService.UpdateClientAsync(form);
        //if (result)
        //{
        //    return Ok(new { success = true });
        //}
        //else
        //{
        //    return Problem("Unable to submit data.");
        //}

        return Ok(new { success = true });

    }
}
