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
    public class ClientesController : ControllerBase
    {
        readonly IClienteService clienteService;

        public ClientesController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }
        // GET: api/Clientes
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var clientes = clienteService.GetAll();
                return Ok(clientes);
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
                var cliente = clienteService.Find(id);
                return Ok(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // POST: api/Clientes
        [HttpPost]
        public IActionResult Post(ClienteDto cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = clienteService.Find(cliente.ClienteId);
                if (entity != null)
                {
                    return BadRequest("Ya existe");
                }
                clienteService.Add(cliente);
                return Ok(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Clientes/5
        [HttpPut]
        public IActionResult Put( ClienteDto cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                clienteService.Edit(cliente);
                return Ok(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var entity = clienteService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                clienteService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
