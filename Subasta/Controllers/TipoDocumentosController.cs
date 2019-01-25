using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;
using System;

namespace Subasta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoDocumentosController : ControllerBase
    {
        readonly ITipoDocumentoService tdService;

        public TipoDocumentosController(ITipoDocumentoService tdService)
        {
            this.tdService = tdService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var tipoDocumentos = tdService.GetAll();
                return Ok(tipoDocumentos);
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
                var td = tdService.Find(id);
                return Ok(td);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(TipoDocumentoDto td)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                tdService.Add(td);
                return Ok(td);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, TipoDocumentoDto td)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = tdService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                tdService.Edit(td);
                return Ok(td);
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
                var entity = tdService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                tdService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
