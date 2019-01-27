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
    public class DepartamentosController : ControllerBase
    {
        readonly IDepartamentoService departamentoService;

        public DepartamentosController(IDepartamentoService departamentoService)
        {
            this.departamentoService = departamentoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var departamentos = departamentoService.GetAll();
                return Ok(departamentos);
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
                var departamento = departamentoService.Find(id);
                return Ok(departamento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(DepartamentoDto departamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                departamentoService.Add(departamento);
                return Ok(departamento);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, DepartamentoDto departamento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = departamentoService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                departamentoService.Edit(departamento);
                return Ok(departamento);
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
                var entity = departamentoService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                departamentoService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
