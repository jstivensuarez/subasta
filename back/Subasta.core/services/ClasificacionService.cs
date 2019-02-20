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
    public class ClasificacionService: IClasificacionService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        public ClasificacionService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(ClasificacionDto dto)
        {
            try
            {
                uowService.ClasificacionRepository.Add(mapper.Map<Clasificacion>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la clasificacion", ex);
            }
        }
        
        
        public int AddWithReturn(ClasificacionDto dto)
        {
            try
            {
               return uowService.ClasificacionRepository.Add(mapper.Map<Clasificacion>(dto));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la clasificacion", ex);
            }
        }

        public void Delete(ClasificacionDto entity)
        {
            try
            {
                var lotes = uowService.LoteRepository.GetAll().Where(r => r.ClasificacionId == entity.ClasificacionId);
                if (lotes.Count() == 0)
                {
                    uowService.ClasificacionRepository.Delete(mapper.Map<Clasificacion>(entity));
                    uowService.Save();
                }
                else
                {
                    throw new ExceptionCore("Entidad en uso");
                }
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar eliminar la clasificacion", ex);
            }
        }

        public void Edit(ClasificacionDto entity)
        {
            try
            {
                uowService.ClasificacionRepository.Edit(mapper.Map<Clasificacion>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla la clasificacion", ex);
            }
        }

        public IQueryable<ClasificacionDto> Find(Expression<Func<Clasificacion, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<ClasificacionDto>>(uowService.ClasificacionRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la clasificacion", ex);
            }
        }

        public ClasificacionDto Find(object id)
        {
            try
            {
                return mapper.Map<ClasificacionDto>(uowService.ClasificacionRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la clasificacion", ex);
            }
        }

        public List<ClasificacionDto> GetAll()
        {
            try
            {
                var result = uowService.ClasificacionRepository.GetllWithInclude();
                return mapper.Map<List<ClasificacionDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener la clasificacion", ex);
            }
        }

        public List<ClasificacionDto> GetByCategoria(int id)
        {
            try
            {
                var result = uowService.ClasificacionRepository.GetAll()
                    .Where(m => m.CategoriaId == id);
                return mapper.Map<List<ClasificacionDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener las clasificaciones", ex);
            }
        }
    }
}
