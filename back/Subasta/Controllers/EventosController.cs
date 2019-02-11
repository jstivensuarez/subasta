using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;

namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventosController : ControllerBase
    {
        readonly IEventoService eventoService;

        public EventosController(IEventoService eventoService)
        {
            this.eventoService = eventoService;
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public IActionResult GetForClients()
        {
            try
            {
                var eventos = eventoService.GetForClients();
                return Ok(eventos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var eventos = eventoService.GetAll();
                return Ok(eventos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet()]
        [Route("[action]/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var evento = eventoService.Find(id);
                return Ok(evento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(EventoDto evento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                evento = eventoService.AddWithReturn(evento);
                return Ok(evento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(EventoDto evento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                eventoService.Edit(evento);
                return Ok(evento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entity = eventoService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                eventoService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
