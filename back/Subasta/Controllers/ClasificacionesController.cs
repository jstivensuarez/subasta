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
    public class ClasificacionesController : ControllerBase
    {
        readonly IClasificacionService clasificacionService;

        public ClasificacionesController(IClasificacionService clasificacionService)
        {
            this.clasificacionService = clasificacionService;
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult GetPorCategoria(int id)
        {
            try
            {
                var clasificaciones = clasificacionService.GetByCategoria(id);
                return Ok(clasificaciones);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var clasificaciones = clasificacionService.GetAll();
                return Ok(clasificaciones);
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
                var clasificacion = clasificacionService.Find(id);
                return Ok(clasificacion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(ClasificacionDto clasificacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }              
                return Ok(clasificacionService.AddWithReturn(clasificacion));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(ClasificacionDto clasificacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                clasificacionService.Edit(clasificacion);
                return Ok(clasificacion);
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
                var entity = clasificacionService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                clasificacionService.Delete(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
