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
    public class MunicipioService : IMunicipioService
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

        public int AddWithReturn(MunicipioDto dto)
        {
            try
            {
                return uowService.MunicipioRepository.Add(mapper.Map<Municipio>(dto));
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
                var clientes = uowService.ClienteRepository.GetAll().Where(c => c.MunicipioId == entity.DepartamentoId);
                var eventos = uowService.EventoRepository.GetAll().Where(d => d.MunicipioId == entity.DepartamentoId);
                var lotes = uowService.LoteRepository.GetAll().Where(d => d.MunicipioId == entity.DepartamentoId);
                var animales = uowService.AnimalRepository.GetAll().Where(d => d.MunicipioId == entity.DepartamentoId);

                if (clientes.Count() == 0 && eventos.Count() == 0 && lotes.Count() == 0 && animales.Count() == 0)
                {
                    uowService.MunicipioRepository.Delete(mapper.Map<Municipio>(entity));
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
                return mapper.Map<MunicipioDto>(uowService.MunicipioRepository.Find(id));
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
                var result = uowService.MunicipioRepository.GetAll();
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

        public List<MunicipioDto> GetByDepartamentoId(int id)
        {
            try
            {
                var result = uowService.MunicipioRepository.GetAll()
                    .Where(m => m.DepartamentoId == id);
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
