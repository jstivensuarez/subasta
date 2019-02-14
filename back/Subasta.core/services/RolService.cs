using AutoMapper;
using Subasta.core.dtos;
using Subasta.core.exceptions;
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
    public class RolService: IRolService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public RolService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(RolDto dto)
        {
            try
            {
                uowService.RolRepository.Add(mapper.Map<Rol>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el rol", ex);
            }
        }

        public void Delete(RolDto entity)
        {
            try
            {
                uowService.RolRepository.Delete(mapper.Map<Rol>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar eliminar el rol", ex);
            }
        }

        public void Edit(RolDto entity)
        {
            try
            {
                uowService.RolRepository.Edit(mapper.Map<Rol>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla el rol", ex);
            }
        }

        public IQueryable<RolDto> Find(Expression<Func<Rol, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<RolDto>>(uowService.RolRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el rol", ex);
            }
        }

        public RolDto Find(object id)
        {
            try
            {
                return mapper.Map<RolDto>(uowService.RolRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el rol", ex);
            }
        }

        public List<RolDto> GetAll()
        {
            try
            {
                var result = uowService.RolRepository.GetAll();
                return mapper.Map<List<RolDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener la categoria", ex);
            }
        }
    }
}
