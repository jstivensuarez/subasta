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
        public LoginController(IConfiguration config, IUsuarioService usuarioService)
        {
            this.config = config;
            this.usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
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
