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
    public class RazaService: IRazaService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly ILoteRepository loteRepository;
        public RazaService(IMapper mapper, IUnitOfWork uowService, ILoteRepository loteRepository)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.loteRepository = loteRepository;
        }

        public void Add(RazaDto dto)
        {
            try
            {
                uowService.RazaRepository.Add(mapper.Map<Raza>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la raza", ex);
            }
        }

        public int AddWithReturn(RazaDto dto)
        {
            try
            {
                return uowService.RazaRepository.Add(mapper.Map<Raza>(dto));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la raza", ex);
            }
        }

        public void Delete(RazaDto entity)
        {
            try
            {
                var lotes = loteRepository.GetAll().Where(r => r.RazaId == entity.RazaId);
                if (lotes.Count() == 0)
                {
                    uowService.RazaRepository.Delete(mapper.Map<Raza>(entity));
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

                throw new ExceptionCore("error al intentar eliminar la raza", ex);
            }
        }

        public void Edit(RazaDto entity)
        {
            try
            {
                uowService.RazaRepository.Edit(mapper.Map<Raza>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar la raza", ex);
            }
        }

        public IQueryable<RazaDto> Find(Expression<Func<Raza, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<RazaDto>>(uowService.RazaRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar las razas", ex);
            }
        }

        public RazaDto Find(object id)
        {
            try
            {
                return mapper.Map<RazaDto>(uowService.RazaRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la raza", ex);
            }
        }

        public List<RazaDto> GetAll()
        {
            try
            {
                var result = uowService.RazaRepository.GetllWithInclude();
                return mapper.Map<List<RazaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener las razas", ex);
            }
        }

        public List<RazaDto> GetByCategoria(int id)
        {
            try
            {
                var result = uowService.RazaRepository.GetAll()
                    .Where(m => m.CategoriaId == id);
                return mapper.Map<List<RazaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener las razas", ex);
            }
        }
    }
}
