using AutoMapper;
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
    public class UsuarioService: IUsuarioService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public UsuarioService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(UsuarioDto dto)
        {
            try
            {
                dto.Clave = Hash.GetHash(dto.Clave);
                uowService.UsuarioRepository.Add(mapper.Map<Usuario>(dto));
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
                var entity = uowService.UsuarioRepository.AutenticarUsuario(usuario.Nombre,usuario.Correo, usuario.Clave);
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
    }
}
