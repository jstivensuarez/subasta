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
    public class PujadoresController : ControllerBase
    {
        readonly IPujadorService pujadorService;

        public PujadoresController(IPujadorService pujadorService)
        {
            this.pujadorService = pujadorService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var pujadores = pujadorService.GetAll();
                return Ok(pujadores);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet()]
        [Route("[action]/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var pujador = pujadorService.Find(id);
                return Ok(pujador);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(PujadorDto pujador)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                pujadorService.Add(pujador);
                return Ok(pujador);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(PujadorDto pujador)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                pujadorService.Edit(pujador);
                return Ok(pujador);
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
                var entity = pujadorService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                pujadorService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
