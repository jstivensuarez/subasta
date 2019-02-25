using AutoMapper;
using Subasta.core.constants;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.helpers;
using Subasta.core.interfaces;
using Subasta.repository;
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
    public class ClienteService: IClienteService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IMunicipioService municipioService;
        readonly IUsuarioService usuarioService;
        readonly IRolService rolService;
        public ClienteService(IMapper mapper, IUnitOfWork uowService, 
            IMunicipioService municipioService, IUsuarioService usuarioService,
            IRolService rolService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.municipioService = municipioService;
            this.usuarioService = usuarioService;
            this.rolService = rolService;
        }

        public void Add(ClienteDto dto)
        {
            try
            {
                dto.Activo = true;
                uowService.ClienteRepository.Add(mapper.Map<Cliente>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el cliente", ex);
            }
        }

        public void addUsuario(ClienteDto dto)
        {
            try
            {
                var rol = rolService.GetAll().
                    SingleOrDefault(r => r.Nombre.ToLower() == Roles.PUJADOR);
                var usuario = new UsuarioDto
                {
                    Clave = dto.Clave,
                    Correo = dto.Correo,
                    Nombre = dto.Usuario,
                    RolId = rol.RolId
                };
                usuarioService.Add(usuario);
                dto.Activo = true;
                uowService.ClienteRepository.Add(mapper.Map<Cliente>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el cliente", ex);
            }
        }

        public void Delete(ClienteDto entity)
        {
            try
            {
                entity.Activo = false;
                uowService.ClienteRepository.Edit(mapper.Map<Cliente>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el cliente", ex);
            }
        }

        public void Edit(ClienteDto entity)
        {
            try
            {
                entity.Activo = true;
                uowService.ClienteRepository.Edit(mapper.Map<Cliente>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el cliente", ex);
            }
        }

        public IQueryable<ClienteDto> Find(Expression<Func<Cliente, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<ClienteDto>>(uowService.ClienteRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el cliente", ex);
            }
        }

        public ClienteDto Find(object id)
        {
            try
            {
                var cliente = mapper.Map<ClienteDto>(uowService.ClienteRepository.GetWithAll(id));
                return cliente;
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el cliente", ex);
            }
        }

        public List<ClienteDto> GetAll()
        {
            try
            {
                var result = uowService.ClienteRepository.GetAllWithInclude()
                    .OrderBy(c => c.Nombre);
                return mapper.Map<List<ClienteDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los clientes", ex);
            }
        }

        public List<ClienteDto> GetPujadores()
        {
            try
            {
                var result = uowService.ClienteRepository.GetAllWithInclude()
                    .Where(c => c.Tipo == TipoUsuarios.PUJADOR)
                    .OrderBy(c => c.Nombre);
                return mapper.Map<List<ClienteDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los clientes", ex);
            }
        }

        public List<ClienteDto> GetPropietarios()
        {
            try
            {
                var result = uowService.ClienteRepository.GetAllWithInclude()
                    .Where(c => c.Tipo == TipoUsuarios.PROPIETARIO); ;
                return mapper.Map<List<ClienteDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los clientes", ex);
            }
        }
    }
}
