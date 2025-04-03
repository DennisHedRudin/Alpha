using Microsoft.AspNetCore.Mvc;
using WebbApp_Alpha.ViewModels.Clients;


namespace WebbApp_Alpha.Controllers;


public class ClientsController : Controller
{
    //private readonly ClientService _clientService; 

    [HttpPost]
    public IActionResult AddClient(AddClientForm form)
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

        return Ok(new { seccess = true });


    }


    [HttpPost]
    public IActionResult EditClient(EditClientForm form)
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
