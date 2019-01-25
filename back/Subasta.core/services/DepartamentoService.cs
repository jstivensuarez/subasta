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
    public class DepartamentoService: IDepartamentoService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public DepartamentoService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(DepartamentoDto dto)
        {
            try
            {
                uowService.DepartamentoRepository.Add(mapper.Map<Departamento>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el departamento", ex);
            }
        }

        public void Delete(DepartamentoDto entity)
        {
            try
            {
                uowService.DepartamentoRepository.Delete(mapper.Map<Departamento>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el departamento", ex);
            }
        }

        public void Edit(DepartamentoDto entity)
        {
            try
            {
                uowService.DepartamentoRepository.Edit(mapper.Map<Departamento>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el departamento", ex);
            }
        }

        public IQueryable<DepartamentoDto> Find(Expression<Func<Departamento, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<DepartamentoDto>>(uowService.DepartamentoRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el departamento", ex);
            }
        }

        public DepartamentoDto Find(object id)
        {
            try
            {
                return mapper.Map<DepartamentoDto>(uowService.DepartamentoRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el departamento", ex);
            }
        }

        public List<DepartamentoDto> GetAll()
        {
            try
            {
                var result = uowService.DepartamentoRepository.GetAll();
                return mapper.Map<List<DepartamentoDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los departamentos", ex);
            }
        }
    }
}
