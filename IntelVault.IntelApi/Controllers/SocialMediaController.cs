using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntelVault.IntelApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly IIntelService<SocialMedia> _service;

        public SocialMediaController(IIntelService<SocialMedia> service)
        {
            this._service = service;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SocialMedia socialMedia)
        {
            try
            {
                _service.Add(socialMedia);
                return Ok(socialMedia);
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
            catch (Exception)
            {
                return NoContent();
            }
        }
    }
}
