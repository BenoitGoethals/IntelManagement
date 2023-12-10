using IntelVault.ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using IntelVault.Infrastructure;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;

namespace IntelVault.IntelApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class HumIntController : Controller
    {
        private readonly IIntelService<HumInt> _service;

        public HumIntController(IIntelService<HumInt> service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] HumInt humInt)
        {
            try
            {
                _service.Add(humInt);
                return Ok(humInt);
            }
            catch (VaultException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception )
            {
                return NoContent();
            }

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_service.GetAll().Result.ToJson());

            }
            catch (VaultException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception)
            {
                return NoContent();
            }
        }
    }
}
