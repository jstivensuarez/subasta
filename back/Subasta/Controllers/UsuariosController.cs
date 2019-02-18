using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subasta.core.constants;
using Subasta.core.dtos;
using Subasta.core.interfaces;

namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        readonly IUsuarioService usuarioService;
        readonly IRolService rolService;
        public UsuariosController(IUsuarioService usuarioService, IRolService rolService)
        {
            this.usuarioService = usuarioService;
            this.rolService = rolService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var usuarios = usuarioService.GetAllAdministradores();
                return Ok(usuarios);
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
                var usuario = usuarioService.Find(id);
                usuario.Clave = null;
                return Ok(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Post(UsuarioDto usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = usuarioService.GetAll().Where(
                    c => c.Nombre == usuario.Nombre
                   || c.Correo == usuario.Correo).ToList();
                if (entity.Count > 0)
                {
                    return BadRequest("Ya existe");
                }
                var rol = rolService.GetAll().
                  SingleOrDefault(r => r.Nombre.ToLower() == Roles.ADMINISTRAODR);
                usuario.RolId = rol.RolId;
                usuarioService.Add(usuario);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut]
        public IActionResult Put(UsuarioDto usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                usuarioService.Edit(usuario);
                return Ok(usuario);
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
                var entity = usuarioService.Find(id);
                if (entity == null)
                {
                    return NotFound();
                }
                usuarioService.Delete(entity);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
