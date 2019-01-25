using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;

namespace Subasta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipiosController : ControllerBase
    {
        readonly IMunicipioService municipioService;

        public MunicipiosController(IMunicipioService municipioService)
        {
            this.municipioService = municipioService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var municipios = municipioService.GetAll();
                return Ok(municipios);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(string id)
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
                municipioService.Add(municipio);
                return Ok(municipio);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, MunicipioDto municipio)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = municipioService.Find(id);
                if (entity == null)
                {
                    return NotFound();
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
