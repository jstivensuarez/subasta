using AutoMapper;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.interfaces;
using Subasta.core.states;
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
    public class PujadorService: IPujadorService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public PujadorService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(PujadorDto dto)
        {
            try
            {
                dto.Estado = Estados.CREADO;
                uowService.PujadorRepository.Add(mapper.Map<Pujador>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el pujador", ex);
            }
        }

        public void Delete(PujadorDto entity)
        {
            try
            {
                entity.Estado = Estados.BORRADO;
                uowService.PujadorRepository.Edit(mapper.Map<Pujador>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar eliminar el pujador", ex);
            }
        }

        public void Edit(PujadorDto entity)
        {
            try
            {
                uowService.PujadorRepository.Edit(mapper.Map<Pujador>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla el pujador", ex);
            }
        }

        public IQueryable<PujadorDto> Find(Expression<Func<Pujador, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<PujadorDto>>(uowService.PujadorRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el pujador", ex);
            }
        }

        public PujadorDto Find(object id)
        {
            try
            {
                return mapper.Map<PujadorDto>(uowService.PujadorRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el pujador", ex);
            }
        }

        public List<PujadorDto> GetAll()
        {
            try
            {
                var result = uowService.PujadorRepository.GetllWithInclude()
                    .Where(p => p.Estado != Estados.BORRADO);
                return mapper.Map<List<PujadorDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los pujadores", ex);
            }
        }
    }
}
