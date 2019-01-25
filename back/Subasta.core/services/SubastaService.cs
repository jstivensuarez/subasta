using AutoMapper;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.interfaces;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Subasta.core.services
{
    public class SubastaService: ISubastaService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public SubastaService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(SubastaDto dto)
        {
            try
            {
                uowService.SubastaRepository.Add(mapper.Map<repository.models.Subasta>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la subasta", ex);
            }
        }

        public void Delete(SubastaDto entity)
        {
            try
            {
                uowService.SubastaRepository.Delete(mapper.Map<repository.models.Subasta>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar la subasta", ex);
            }
        }

        public void Edit(SubastaDto entity)
        {
            try
            {
                uowService.SubastaRepository.Edit(mapper.Map<repository.models.Subasta>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar la subasta", ex);
            }
        }

        public IQueryable<SubastaDto> Find(Expression<Func<repository.models.Subasta, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<SubastaDto>>(uowService.SubastaRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la subasta", ex);
            }
        }

        public SubastaDto Find(object id)
        {
            try
            {
                return mapper.Map<SubastaDto>(uowService.SubastaRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la subasta", ex);
            }
        }

        public List<SubastaDto> GetAll()
        {
            try
            {
                var result = uowService.AnimalRepository.GetAll();
                return mapper.Map<List<SubastaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener las subastas", ex);
            }
        }
    }
}
