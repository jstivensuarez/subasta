using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Subasta.core.constants;
using Subasta.core.dtos;
using Subasta.core.interfaces;


namespace Subasta.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;
        private IUsuarioService usuarioService;
        private IClienteService clienteService;
        public LoginController(IConfiguration config,
            IUsuarioService usuarioService,
            IClienteService clienteService)
        {
            this.config = config;
            this.usuarioService = usuarioService;
            this.clienteService = clienteService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult Login(UsuarioDto usuario)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(usuario);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult ChangePass(UsuarioDto usuario)
        {
            try
            {
                IActionResult response = Unauthorized();
                var user = AuthenticateUser(usuario);
                if (user != null)
                {
                    usuarioService.CambiarClave(usuario);
                    return Ok();
                }
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        public IActionResult Register(ClienteDto cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var entity = clienteService.GetAll().Where(
                     c => c.ClienteId == cliente.ClienteId
                    || c.Correo == cliente.Correo).ToList();
                if (entity.Count > 0)
                {
                    return BadRequest("Ya existe");
                }
                cliente.Tipo = TipoUsuarios.PUJADOR;
                clienteService.addUsuario(cliente);
                var usuario = new UsuarioDto
                {
                    Clave = cliente.Clave,
                    Correo = cliente.Correo,
                    Nombre = cliente.Usuario
                };
                var user = AuthenticateUser(usuario);
                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { token = tokenString });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]/{nombreUsuario}")]
        public IActionResult ValidateUser(string nombreUsuario)
        {
            var usuario = usuarioService.GetAll().
                SingleOrDefault(u => u.Nombre == nombreUsuario);
            if (usuario == null)
            {
                return Ok();
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]/{nombreUsuario}")]
        public IActionResult RecoverePass(string nombreUsuario)
        {
            try
            {
                usuarioService.RecuperarClave(nombreUsuario);
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GenerateJSONWebToken(UsuarioDto userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Nombre),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Correo),
                new Claim("Role", userInfo.Rol.Nombre),
                new Claim("RoleId", userInfo.RolId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
              config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UsuarioDto AuthenticateUser(UsuarioDto login)
        {
            try
            {
                var usuario = usuarioService.AutenticarUsuario(login);
                return usuario;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
