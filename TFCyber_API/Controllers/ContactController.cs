using GestionContact_DAL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFCyber_API.Models;
using TFCyber_API.Tools;

namespace TFCyber_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private ContactService _service;    
        public ContactController()
        {
            _service = new ContactService();
        }
        
        [HttpGet]
        public IActionResult GetContact()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Create(Contact c)
        {
            if(_service.Create(c.ToDal())) return Ok();
            return BadRequest("Objet non créé");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            if(_service.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest("Id inexistant");
            }
               
        }

        [HttpPut]
        public IActionResult Update(Contact c) 
        {
            if(_service.Update(c.ToDal()))
                return Ok();

            return BadRequest();
        }
    }
}
