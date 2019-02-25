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
    public class CategoriaService : ICategoriaService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        public CategoriaService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(CategoriaDto dto)
        {
            try
            {
                uowService.CategoriaRepository.Add(mapper.Map<Categoria>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la categoria", ex);
            }
        }

        public int AddWithReturn(CategoriaDto dto)
        {
            try
            {
                return uowService.CategoriaRepository.Add(mapper.Map<Categoria>(dto));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la categoria", ex);
            }
        }

        public void Delete(CategoriaDto entity)
        {
            try
            {
                var lotes = uowService.LoteRepository.GetAll().Where(r => r.CategoriaId == entity.CategoriaId);
                var razas = uowService.RazaRepository.GetAll().Where(r => r.CategoriaId == entity.CategoriaId);
                var clasificaciones = uowService.ClasificacionRepository.GetAll().Where(r => r.CategoriaId == entity.CategoriaId);

                if (lotes.Count() == 0 && razas.Count() == 0 && clasificaciones.Count() == 0)
                {
                    uowService.CategoriaRepository.Delete(mapper.Map<Categoria>(entity));
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
                throw new ExceptionCore("error al intentar eliminar la categoria", ex);
            }
        }

        public void Edit(CategoriaDto entity)
        {
            try
            {
                uowService.CategoriaRepository.Edit(mapper.Map<Categoria>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla la categoria", ex);
            }
        }

        public IQueryable<CategoriaDto> Find(Expression<Func<Categoria, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<CategoriaDto>>(uowService.CategoriaRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la categoria", ex);
            }
        }

        public CategoriaDto Find(object id)
        {
            try
            {
                return mapper.Map<CategoriaDto>(uowService.CategoriaRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la categoria", ex);
            }
        }

        public List<CategoriaDto> GetAll()
        {
            try
            {
                var result = uowService.CategoriaRepository.GetAll();
                return mapper.Map<List<CategoriaDto>>(result);
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
