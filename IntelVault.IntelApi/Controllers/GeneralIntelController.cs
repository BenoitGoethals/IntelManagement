using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IntelVault.IntelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralIntelController : ControllerBase
    {
        private readonly IIntelService<GeneralIntel> _service;

        public GeneralIntelController(IIntelService<GeneralIntel> service)
        {
            _service = service;
        }


        [HttpPost]
        public IActionResult Post([FromBody] GeneralIntel generalIntel)
        {
            try
            {
                _ = _service.Add(generalIntel);
                return Ok(generalIntel);
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
                return Ok(_service.GetAll().Result.ToJson());

            }
            catch (VaultException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception )
            {
                return NotFound();
            }

        }
    }
}
