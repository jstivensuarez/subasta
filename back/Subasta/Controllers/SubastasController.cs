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
    public class SubastasController : ControllerBase
    {
        readonly ISubastaService subastaService;

        public SubastasController(ISubastaService subastaService)
        {
            this.subastaService = subastaService;
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int id)
        {
            try
            {
                var subastas = subastaService.GetAll();
                return Ok(subastas);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetPorEvento/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var subastas = subastaService.GetPorEvento(id);
                return Ok(subastas);
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
                var subasta = subastaService.Find(id);
                return Ok(subasta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(SubastaDto subasta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                subastaService.Add(subasta);
                return Ok(subasta);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(SubastaDto subasta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                subastaService.Edit(subasta);
                return Ok(subasta);
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
                var entity = subastaService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                subastaService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
