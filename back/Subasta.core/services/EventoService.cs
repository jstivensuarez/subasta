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
    public class EventoService : IEventoService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly ISubastaService subastaService;
        readonly ILoteService loteService;
        readonly IAnimalService animalService;
        public EventoService(IMapper mapper, IUnitOfWork uowService,
             ISubastaService subastaService,
             ILoteService loteService,
             IAnimalService animalService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.subastaService = subastaService;
            this.loteService = loteService;
            this.animalService = animalService;
        }

        public void Add(EventoDto dto)
        {
            try
            {
                dto.Activo = true;
                uowService.EventoRepository.Add(mapper.Map<Evento>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el evento", ex);
            }
        }

        public EventoDto AddWithReturn(EventoDto dto)
        {
            try
            {
                var result = uowService.EventoRepository.AddWithReturn(mapper.Map<Evento>(dto));
                return mapper.Map<EventoDto>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el evento", ex);
            }
        }

        public void Delete(EventoDto entity)
        {
            try
            {
                entity.Activo = false;
                uowService.EventoRepository.Edit(mapper.Map<Evento>(entity));
                EliminarSubastasEnCascada(entity.EventoId);
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el evento", ex);
            }
        }

        public void Edit(EventoDto entity)
        {
            try
            {
                entity.Activo = true;
                uowService.EventoRepository.Edit(mapper.Map<Evento>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el evento", ex);
            }
        }

        public IQueryable<EventoDto> Find(Expression<Func<Evento, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<EventoDto>>(uowService.EventoRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el evento", ex);
            }
        }

        public EventoDto Find(object id)
        {
            try
            {
                return mapper.Map<EventoDto>(uowService.EventoRepository.GetWithAll(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el evento", ex);
            }
        }

        public List<EventoDto> GetAll()
        {
            try
            {
                var result = uowService.EventoRepository.GetAllWithInclude()
                    .OrderBy(e => e.FechaInicio);
                return mapper.Map<List<EventoDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los eventos", ex);
            }
        }


        private void EliminarSubastasEnCascada(int eventoId)
        {
            var subastas = subastaService.GetAll()
                .Where(s => s.EventoId == eventoId).ToList();
            foreach (var subasta in subastas)
            {
                EliminarLotes(subasta.SubastaId);
                subastaService.Delete(subasta);
            }
        }

        private void EliminarLotes(int subastaId)
        {
            var lotes = loteService.GetAll()
                    .Where(l => l.SubastaId == subastaId).ToList();
            foreach (var lote in lotes)
            {
                eliminarAnimales(lote.LoteId);
                loteService.Delete(lote);
            }
        }

        private void eliminarAnimales(int loteId)
        {
            var animales = animalService.GetAll()
                .Where(a => a.LoteId == loteId).ToList();
            foreach (var animal in animales)
            {
                animalService.Delete(animal);
            }
        }
    }
}
