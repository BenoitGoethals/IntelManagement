using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IntelVault.IntelApi.Controllers;


[ApiController]
[Route("[controller]")]
public class CybIntController(IIntelService<CybInt> service) : Controller
{
    [HttpPost]
    public IActionResult Post([FromBody] CybInt cybInt)
    {
        try
        {
            _ = service.Add(cybInt);
            return Ok(cybInt);
        }
        catch (VaultException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception)
        {
            return NotFound();
        }

    }


    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            return Ok(service.GetAll().Result.ToJson());

        }
        catch (VaultException)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (Exception)
        {
            return NotFound();
        }

    }
}