using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class SolicitudesController : ControllerBase
    {
        // GET: api/Solicitudes
        readonly ISolicitudService solicitudService;

        public SolicitudesController(ISolicitudService solicitudService)
        {
            this.solicitudService = solicitudService;
        }
        // GET: api/Clientes
        [HttpGet]
        public IActionResult Get()
        {
            try
            {               
                var solicitudes = solicitudService.GetAll();
                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet()]
        [Route("[action]/{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                var solicitud = solicitudService.Find(id);
                return Ok(solicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Clientes
        [HttpPost]
        public IActionResult Post(SolicitudSubastaDto solicitud)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var usuario = User.Claims.First().Value;
                solicitudService.Add(solicitud, usuario);
                return Ok(solicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Aceptar(SolicitudSubastaDto solicitud)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                solicitudService.Aceptar(solicitud);
                return Ok(solicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }


        // PUT: api/Clientes/5
        [HttpPut]
        public IActionResult Put(SolicitudSubastaDto solicitud)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                solicitudService.Edit(solicitud);
                return Ok(solicitud);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var entity = solicitudService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                solicitudService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
