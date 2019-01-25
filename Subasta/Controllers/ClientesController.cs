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

        // GET: api/Clientes/5
        [HttpGet("{id}", Name = "Get")]
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
                clienteService.Add(cliente);
                return Ok(cliente);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, ClienteDto cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = clienteService.Find(id);
                if (entity == null)
                {
                    return NotFound();
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
        public IActionResult Delete(int id)
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
