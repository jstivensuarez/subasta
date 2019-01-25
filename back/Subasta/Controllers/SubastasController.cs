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
    public class SubastasController : ControllerBase
    {
        readonly ISubastaService subastaService;

        public SubastasController(ISubastaService subastaService)
        {
            this.subastaService = subastaService;
        }

        [HttpGet]
        public IActionResult Get()
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


        [HttpGet("{id}", Name = "Get")]
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, SubastaDto subasta)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = subastaService.Find(id);
                if (entity == null)
                {
                    return NotFound();
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
