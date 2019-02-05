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
    public class RazasController : ControllerBase
    {
        // GET: api/Razas
        readonly IRazaService razaService;

        public RazasController(IRazaService razaService)
        {
            this.razaService = razaService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var razas = razaService.GetAll();
                return Ok(razas);
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
                var raza = razaService.Find(id);
                return Ok(raza);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(RazaDto raza)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                razaService.Add(raza);
                return Ok(raza);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(RazaDto raza)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                razaService.Edit(raza);
                return Ok(raza);
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
                var entity = razaService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                razaService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
