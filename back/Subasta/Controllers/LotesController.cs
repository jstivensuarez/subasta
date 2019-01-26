using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;
using System;

namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LotesController : ControllerBase
    {
        readonly ILoteService loteService;

        public LotesController(ILoteService loteService)
        {
            this.loteService = loteService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var lotes = loteService.GetAll();
                return Ok(lotes);
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
                var lote = loteService.Find(id);
                return Ok(lote);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(LoteDto lote)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                loteService.Add(lote);
                return Ok(lote);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, LoteDto lote)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = loteService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                loteService.Edit(lote);
                return Ok(lote);
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
                var entity = loteService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                loteService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
