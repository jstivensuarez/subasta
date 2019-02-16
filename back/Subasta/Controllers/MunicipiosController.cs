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
    public class MunicipiosController : ControllerBase
    {
        readonly IMunicipioService municipioService;

        public MunicipiosController(IMunicipioService municipioService)
        {
            this.municipioService = municipioService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetPorDepartamento/{id}")]
        public IActionResult GetPorDepartamento(int id)
        {
            try
            {
                var municipios = municipioService.GetByDepartamentoId(id);
                return Ok(municipios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet()]
        [AllowAnonymous]
        [Route("[action]/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var municipio = municipioService.Find(id);
                return Ok(municipio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(MunicipioDto municipio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                return Ok(municipioService.AddWithReturn(municipio));
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(MunicipioDto municipio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                municipioService.Edit(municipio);
                return Ok(municipio);
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
                var entity = municipioService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                municipioService.Delete(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
