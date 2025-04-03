using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels.Members;

namespace WebbApp_Alpha.Controllers;

public class MemberController : Controller
{
    //private readonly MemberService _memberService;

    [HttpPost]
    public IActionResult AddMember(AddMemberForm form)
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

        //var result = await _clientService.AddClientAsync(form);
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

        return Ok(new { seccess = true });

    }
}
