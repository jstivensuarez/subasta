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
    public class EventoService : IEventoService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly ISubastaService subastaService;
        readonly ILoteService loteService;
        readonly IAnimalService animalService;
        readonly IClienteService clienteService;
        readonly ISolicitudService solicitudService;
        public EventoService(IMapper mapper, IUnitOfWork uowService,
             ISubastaService subastaService,
             ILoteService loteService,
             IAnimalService animalService,
             IClienteService clienteService,
              ISolicitudService solicitudService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.subastaService = subastaService;
            this.loteService = loteService;
            this.animalService = animalService;
            this.clienteService = clienteService;
            this.solicitudService = solicitudService;
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
                dto.Activo = true;
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

        public List<EventoDto> GetForClientAutenticated(string usuario)
        {
            var cliente = clienteService.GetAll().SingleOrDefault(c => c.Usuario == usuario);
            var eventos = GetForClients();
            SolicitudSubastaDto solicitud = null;
            foreach (var evento in eventos)
            {
                foreach (var subasta in evento.SubastasDto)
                {
                    if (subasta.ValorAnticipo == 0)
                    {
                        subasta.EstadoSolicitud = Estados.AUTORIZADO;
                    }
                    else
                    {
                        solicitud = solicitudService.GetAll().SingleOrDefault(s => s.ClienteId == cliente.ClienteId
                            && s.SubastaId == subasta.SubastaId);
                        subasta.EstadoSolicitud = solicitud == null ? Estados.NO_AUTORIZADO: solicitud.Estado;
                    }
                }
            }
            return eventos;
        }

        public List<EventoDto> GetForClients()
        {
            try
            {
                var hoy = DateTime.Today;
                var eventos = (from evento in uowService.EventoRepository.GetAllWithInclude()
                               where evento.Activo && evento.FechaFin.Year >= hoy.Year &&
                               evento.FechaFin.Month >= hoy.Month && evento.FechaFin.Day >= hoy.Day
                               select new EventoDto
                               {
                                   EventoId = evento.EventoId,
                                   Activo = evento.Activo,
                                   Descripcion = evento.Descripcion,
                                   FechaFin = evento.FechaFin,
                                   FechaInicio = evento.FechaInicio,
                                   Municipio = mapper.Map<MunicipioDto>(evento.Municipio),
                                   MunicipioId = evento.MunicipioId,
                                   SubastasDto = (from subasta in uowService.SubastaRepository.GetAllWithInclude()
                                                  where subasta.EventoId == evento.EventoId
                                                  select new SubastaDto
                                                  {
                                                      Descripcion = subasta.Descripcion,
                                                      EventoId = subasta.EventoId,
                                                      HoraFin = subasta.HoraFin,
                                                      HoraInicio = subasta.HoraInicio,
                                                      SubastaId = subasta.SubastaId,
                                                      EstadoSolicitud = Estados.NO_AUTORIZADO,
                                                      ValorAnticipo = subasta.ValorAnticipo,
                                                      LotesDto = (from lote in uowService.LoteRepository.GetAllWithInclude()
                                                                  where lote.SubastaId == subasta.SubastaId
                                                                  select new LoteDto
                                                                  {
                                                                      CantidadElementos = lote.CantidadElementos,
                                                                      Descripcion = lote.Descripcion,
                                                                      FotoLote = lote.FotoLote,
                                                                      LoteId = lote.LoteId,
                                                                      Nombre = lote.Nombre,
                                                                      SubastaId = lote.SubastaId,
                                                                      Subasta = mapper.Map<SubastaDto>(subasta),
                                                                      PrecioInicial = lote.PrecioInicial,
                                                                      PesoPromedio = lote.PesoPromedio,
                                                                      PrecioBase = lote.PrecioBase,
                                                                      Municipio = mapper.Map<MunicipioDto>(lote.Municipio),
                                                                      MunicipioId = lote.MunicipioId,
                                                                      PesoTotal = lote.PesoTotal,
                                                                      Cliente = mapper.Map<ClienteDto>(lote.Cliente),
                                                                      Categoria = mapper.Map<CategoriaDto>(lote.Categoria),
                                                                      Raza =  mapper.Map<RazaDto>(lote.Raza),
                                                                      Clasificacion = mapper.Map<ClasificacionDto>(lote.Clasificacion)
                                                                  }).ToList()
                                                  }).OrderBy(s => s.HoraInicio).ToList()
                               });
                return mapper.Map<List<EventoDto>>(eventos);
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
                subastaService.Delete(subasta);
            }
        }
    }
}
