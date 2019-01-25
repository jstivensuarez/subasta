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
    public class MunicipioService: IMunicipioService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public MunicipioService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(MunicipioDto dto)
        {
            try
            {
                uowService.MunicipioRepository.Add(mapper.Map<Municipio>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el municipio", ex);
            }
        }

        public void Delete(MunicipioDto entity)
        {
            try
            {
                uowService.MunicipioRepository.Delete(mapper.Map<Municipio>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el municipio", ex);
            }
        }

        public void Edit(MunicipioDto entity)
        {
            try
            {
                uowService.MunicipioRepository.Edit(mapper.Map<Municipio>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el municipio", ex);
            }
        }

        public IQueryable<MunicipioDto> Find(Expression<Func<Municipio, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<MunicipioDto>>(uowService.MunicipioRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el municipio", ex);
            }
        }

        public MunicipioDto Find(object id)
        {
            try
            {
                return mapper.Map<MunicipioDto>(uowService.AnimalRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el municipio", ex);
            }
        }

        public List<MunicipioDto> GetAll()
        {
            try
            {
                var result = uowService.AnimalRepository.GetAll();
                return mapper.Map<List<MunicipioDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los municipios", ex);
            }
        }
    }
}
