using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.dtos;
using Subasta.core.interfaces;
using System;
using System.Linq;

namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                var lotes = loteService.GetllWithInclude();
                return Ok(lotes);
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
                var lote = loteService.GetllWithInclude().SingleOrDefault(l => l.LoteId == id);
                return Ok(lote);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult Post()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                LoteDto lote = new LoteDto();
                if (Request.Form.Files.Count > 0)
                {
                    lote.Imagen = Request.Form.Files[0];
                }
                else
                {
                    lote.VideoLote = Request.Form["videoLote"];
                }
                lote.Nombre = Request.Form["nombre"];
                lote.Descripcion = Request.Form["descripcion"];
                lote.ClienteId = Request.Form["clienteId"];
                lote.MunicipioId = Convert.ToInt32(Request.Form["municipioId"]);
                lote.PrecioBase = Convert.ToDecimal(Request.Form["precioBase"]);
                lote.SubastaId = Convert.ToInt32(Request.Form["subastaId"]);
                lote.ValorAnticipo = Convert.ToDecimal(Request.Form["valorAnticipo"]);
                loteService.Add(lote);
                return Ok(lote);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut, DisableRequestSizeLimit]
        public IActionResult Put()
        {
            try
            {
                LoteDto lote = new LoteDto();
                if (Request.Form.Files.Count > 0)
                {
                    lote.Imagen = Request.Form.Files[0];
                }
                else
                {
                    lote.VideoLote = Request.Form["videoLote"];
                }
                lote.LoteId = Convert.ToInt32(Request.Form["loteId"]);
                lote.Nombre = Request.Form["nombre"];
                lote.Descripcion = Request.Form["descripcion"];
                lote.ClienteId = Request.Form["clienteId"];
                lote.FotoLote = Request.Form["fotoLote"];
                lote.MunicipioId = Convert.ToInt32(Request.Form["municipioId"]);
                lote.PrecioBase = Convert.ToDecimal(Request.Form["precioBase"]);
                lote.SubastaId = Convert.ToInt32(Request.Form["subastaId"]);
                lote.ValorAnticipo = Convert.ToDecimal(Request.Form["valorAnticipo"]);
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
