
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IntelVault.IntelApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonOfInterestController : ControllerBase
    {

        private readonly IIntelService<PersonOfInterest> _service;

        public PersonOfInterestController(IIntelService<PersonOfInterest> service)
        {
            this._service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonOfInterest personOfInterest)
        {
            try
            {
                _service.Add(personOfInterest);
                return Ok(personOfInterest);
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
