using AutoMapper;
using Subasta.core.constants;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.helpers;
using Subasta.core.interfaces;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Subasta.core.services
{
    public class UsuarioService : IUsuarioService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly ICorreoHelper correoHelper;

        public UsuarioService(IMapper mapper, IUnitOfWork uowService,
             ICorreoHelper correoHelper)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.correoHelper = correoHelper;
        }

        public void Add(UsuarioDto dto)
        {
            try
            {
                string mensajeCorreo = Correos.MENSAJEBIENVENIDA;
                if (dto.Clave == null)
                {
                    string clave = getClave();
                    dto.Clave = clave;
                    mensajeCorreo = string.Concat(mensajeCorreo, $" su contraseña es: {clave}");
                }

                dto.Clave = Hash.GetHash(dto.Clave);
                uowService.UsuarioRepository.Add(mapper.Map<Usuario>(dto));
                correoHelper.enviarDesdeSubasta(mensajeCorreo,
                   Correos.ASUNTOBIENVENIDO, dto.Correo);
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el usuario", ex);
            }
        }

        public UsuarioDto AutenticarUsuario(UsuarioDto usuario)
        {
            try
            {
                usuario.Clave = Hash.GetHash(usuario.Clave);
                var entity = uowService.UsuarioRepository.AutenticarUsuario(usuario.Nombre, usuario.Correo, usuario.Clave);
                return mapper.Map<UsuarioDto>(entity);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el usuario", ex);
            }
        }

        public void CambiarClave(UsuarioDto usuario)
        {
            try
            {
                var entity = uowService.UsuarioRepository.GetAll()
                    .SingleOrDefault(u => u.Correo == usuario.Correo || u.Nombre == usuario.Nombre);
                entity.Clave = Hash.GetHash(usuario.ClaveChange);
                uowService.UsuarioRepository.Edit(mapper.Map<Usuario>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el usuario", ex);
            }
        }

        public void Delete(UsuarioDto entity)
        {
            try
            {
                uowService.UsuarioRepository.Delete(mapper.Map<Usuario>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el usuario", ex);
            }
        }

        public void Edit(UsuarioDto entity)
        {
            try
            {
                uowService.UsuarioRepository.Edit(mapper.Map<Usuario>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el usuario", ex);
            }
        }

        public IQueryable<UsuarioDto> Find(Expression<Func<Usuario, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<UsuarioDto>>(uowService.UsuarioRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el usuario", ex);
            }
        }

        public UsuarioDto Find(object id)
        {
            try
            {
                return mapper.Map<UsuarioDto>(uowService.UsuarioRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el usuario", ex);
            }
        }

        public List<UsuarioDto> GetAll()
        {
            try
            {
                var result = uowService.UsuarioRepository.GetAll();
                return mapper.Map<List<UsuarioDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los usuarios", ex);
            }
        }

        public List<UsuarioDto> GetAllAdministradores()
        {
            try
            {
                var result = uowService.UsuarioRepository.GetllWithInclude()
                    .Where(u => u.Rol.Nombre.ToLower() == Roles.ADMINISTRAODR);
                return mapper.Map<List<UsuarioDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los usuarios", ex);
            }
        }

        public void RecuperarClave(string usuario)
        {
            try
            {
                string clave = string.Empty;
                var entity = uowService.UsuarioRepository.GetAll()
                    .SingleOrDefault(u => u.Correo == usuario || u.Nombre == usuario);
                clave = getClave();
                entity.Clave = Hash.GetHash(clave);
                uowService.UsuarioRepository.Edit(entity);
                correoHelper.enviarDesdeSubasta(string.Concat(Correos.MENSAJERECUPERARCLAVE, clave),
                    Correos.ASUNTORECUPERAR, entity.Correo);
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar cambiar la contraseña", ex);
            }
        }

        private string getClave()
        {
            string clave = Guid.NewGuid().ToString().Substring(0, 7);
            return GetLetter() + clave;
        }
        private string GetLetter()
        {
            Random _random = new Random();
            int num = _random.Next(0, 26);
            char let = (char)('a' + num);
            return let.ToString();
        }
    }
}
