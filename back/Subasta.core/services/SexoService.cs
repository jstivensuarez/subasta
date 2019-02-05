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
    public class SexoService: ISexoService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public SexoService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(SexoDto dto)
        {
            try
            {
                uowService.SexoRepository.Add(mapper.Map<Sexo>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el sexo", ex);
            }
        }

        public void Delete(SexoDto entity)
        {
            try
            {
                uowService.SexoRepository.Delete(mapper.Map<Sexo>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el sexo", ex);
            }
        }

        public void Edit(SexoDto entity)
        {
            try
            {
                uowService.SexoRepository.Edit(mapper.Map<Sexo>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el sexo", ex);
            }
        }

        public IQueryable<SexoDto> Find(Expression<Func<Sexo, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<SexoDto>>(uowService.SexoRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el sexo", ex);
            }
        }

        public SexoDto Find(object id)
        {
            try
            {
                return mapper.Map<SexoDto>(uowService.SexoRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el sexo", ex);
            }
        }

        public List<SexoDto> GetAll()
        {
            try
            {
                var result = uowService.SexoRepository.GetAll();
                return mapper.Map<List<SexoDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los sexos", ex);
            }
        }
    }
}
