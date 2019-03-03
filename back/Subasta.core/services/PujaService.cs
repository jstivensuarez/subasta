using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Subasta.core.constants;
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
    public class PujaService : IPujaService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IMensajesService mensajesService;
        readonly ICorreoHelper correoHelper;
        readonly IConfiguration configuration;
        public PujaService(IMapper mapper, IUnitOfWork uowService,
            IMensajesService mensajesService, ICorreoHelper correoHelper, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.mensajesService = mensajesService;
            this.correoHelper = correoHelper;
            this.configuration = configuration;
        }

        public void Add(PujaDto dto)
        {
            try
            {
                var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                dto.HoraPuja = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, myTimeZone);
                var cliente = uowService.ClienteRepository.GetAll()
                    .SingleOrDefault(u => u.Usuario == dto.Usuario);
                var pujador = uowService.PujadorRepository.GetllWithInclude()
                   .SingleOrDefault(p => p.Estado != Estados.BORRADO && p.ClienteId == cliente.ClienteId
                   && p.LoteId == dto.LoteId);
                var lote = uowService.LoteRepository.GetAllWithInclude()
                    .SingleOrDefault(l => l.LoteId == dto.LoteId);

                if (dto.HoraPuja > lote.Subasta.HoraFin)
                {
                    throw new ExceptionCore("Subasta finalizada");
                }
                else
                {
                    if (pujador == null)
                    {
                        var pujadroDto = new PujadorDto();
                        pujadroDto.LoteId = dto.LoteId;
                        pujadroDto.ClienteId = cliente.ClienteId;
                        dto.PujadorId = uowService.PujadorRepository.AddWithReturn(mapper.Map<Pujador>(pujadroDto));
                    }
                    else
                    {
                        dto.PujadorId = pujador.PujadorId;
                    }
                    uowService.PujaRepository.Add(mapper.Map<Puja>(dto));
                    uowService.Save();
                    NotificarPuja(dto);
                }
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                if (ex.GetBaseException().Message.Contains("Valor actualizado"))
                {
                    throw new ExceptionCore("Valor actualizado");
                }
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la puja", ex);
            }
        }

        public void Add(Puja dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(PujaDto entity)
        {
            try
            {
                uowService.PujaRepository.Delete(mapper.Map<Puja>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar eliminar la puja", ex);
            }
        }

        public void Delete(Puja entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(PujaDto entity)
        {
            try
            {
                uowService.PujaRepository.Edit(mapper.Map<Puja>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla la puja", ex);
            }
        }

        public void Edit(Puja entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PujaDto> Find(Expression<Func<Puja, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<PujaDto>>(uowService.PujaRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la puja", ex);
            }
        }

        public PujaDto Find(object id)
        {
            try
            {
                return mapper.Map<PujaDto>(uowService.PujaRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la puja", ex);
            }
        }

        public IQueryable<Puja> Find(Expression<Func<PujaDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<PujaDto> GetAll()
        {
            try
            {
                var result = uowService.PujaRepository.GetAll();
                return mapper.Map<List<PujaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener la puja", ex);
            }
        }

        public void NotificarGanadores(List<PujaDto> ganadores)
        {
            try
            {
                DateTime hoy = DateTime.Now;
                foreach (var item in ganadores)
                {            
                    string host = configuration.GetSection("url-front").Value;                
                       var lote = uowService.LoteRepository.GetAll()
                         .SingleOrDefault(l => l.LoteId == item.Pujador.LoteId);
                    var cliente = uowService.ClienteRepository.GetAll()
                        .SingleOrDefault(c => c.ClienteId == item.Pujador.ClienteId);
                    string compraInfo = $"{cliente.Usuario}-{lote.LoteId}-{hoy.ToString()}";
                    string link = $"<a href = '{host}/confirmacion/{obtenerInfoCompra(compraInfo)}'" +
                        $" target = '_blank'>Confirmar Compra<a>";
                    lote.Finalizado = true;
                    ConfirmacionPago confirmacion = new ConfirmacionPago
                    {
                        Estado = Estados.PENDIENTE_PAGAR,
                        Fecha = hoy,
                        Usuario = cliente.Usuario,
                        LoteId = lote.LoteId
                    };
                    uowService.LoteRepository.Edit(lote);
                    uowService.ConfirmacionRepository.Add(confirmacion);
                    uowService.Save();
                    correoHelper.enviarDesdeSubasta(Correos.MENSAJEGANAR + link, Correos.ASUNTOGANAR, cliente.Correo);
                }
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al notificar los clientes", ex);
            }
        }
        public List<PujaDto> obtenerGanadores()
        {
            try
            {
                var myTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
                var hoy = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, myTimeZone);
                var ganadores = (from lote in uowService.LoteRepository.GetAll()
                                 join subasta in uowService.SubastaRepository.GetAll()
                                 on lote.SubastaId equals subasta.SubastaId
                                 where subasta.HoraFin < hoy && subasta.Activo && !lote.Finalizado && lote.Activo
                                 select (from puja in uowService.PujaRepository.GetAll().OrderByDescending(p => p.Valor)
                                         join pujador in uowService.PujadorRepository.GetAll()
                                         on puja.PujadorId equals pujador.PujadorId
                                         where pujador.LoteId == lote.LoteId
                                         select new PujaDto
                                         {
                                             HoraPuja = puja.HoraPuja,
                                             LoteId = lote.LoteId,
                                             Pujador = mapper.Map<PujadorDto>(pujador),
                                             PujadorId = pujador.PujadorId,
                                             PujaId = puja.PujaId,
                                             Valor = puja.Valor
                                         }).FirstOrDefault()
                                 ).ToList();
                return ganadores;
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los ganadores", ex);
            }
        }

        public PujaDto obtenerGanadorInfo(int loteId)
        {
            try
            {
                var ganador = (from lote in uowService.LoteRepository.GetAll()
                                 where lote.LoteId == loteId
                                 select (from puja in uowService.PujaRepository.GetAll().OrderByDescending(p => p.Valor)
                                         join pujador in uowService.PujadorRepository.GetAll()
                                         on puja.PujadorId equals pujador.PujadorId
                                         where pujador.LoteId == lote.LoteId
                                         select new PujaDto
                                         {
                                             HoraPuja = puja.HoraPuja,                                  
                                             LoteId = lote.LoteId,
                                             Pujador = mapper.Map<PujadorDto>(pujador),
                                             PujadorId = pujador.PujadorId,
                                             PujaId = puja.PujaId,
                                             Valor = puja.Valor
                                         }).FirstOrDefault()
                                 ).SingleOrDefault();
                ganador.Pujador.Cliente = mapper.Map<ClienteDto>(uowService.ClienteRepository.GetAll()
                    .SingleOrDefault(c => c.ClienteId == ganador.Pujador.ClienteId));
                ganador.Pujador.Lote = mapper.Map<LoteDto>(uowService.LoteRepository.GetAll()
                    .SingleOrDefault(c => c.LoteId == ganador.Pujador.LoteId));
                return ganador;
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener al ganador", ex);
            }
        }

        public void ConfirmarGanador(int loteId, string usuario)
        {
            try
            {
                var confirmacion = uowService.ConfirmacionRepository.GetAll()
                    .SingleOrDefault(c => c.LoteId == loteId);
                var tiempo = DateTime.Now - confirmacion.Fecha;
                if (tiempo.TotalMinutes < 10 && confirmacion.Usuario == usuario && confirmacion.Estado == Estados.PENDIENTE_PAGAR)
                {
                    confirmacion.Estado = Estados.CONFIRMADO;
                    uowService.ConfirmacionRepository.Edit(confirmacion);
                    uowService.Save();
                }         
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar guardar la confirmacion", ex);
            }
        }

        private void NotificarPuja(PujaDto puja)
        {
            var pujador = uowService.PujadorRepository.GetllWithInclude()
                .SingleOrDefault(p => p.Estado != Estados.BORRADO && p.PujadorId == puja.PujadorId);
            var subasta = uowService.SubastaRepository.GetAllWithInclude()
                            .SingleOrDefault(s => s.SubastaId == pujador.Lote.SubastaId);

            var mensaje = new
            {
                usuario = puja.Usuario,
                valor = puja.Valor,
                loteId = puja.LoteId,
                subastaId = subasta.SubastaId,
                eventoId = subasta.EventoId
            };

            string mensajeFinal = mensajesService.ObtenerMenajeFinal(mensaje, TipoMensajes.ACTUALIZARLOTEPUJA);

            mensajesService.EnviarMensaje(mensajeFinal);
        }

        private string obtenerInfoCompra(string mensaje)
        {
            byte[] toEncodeAsBytes

              = System.Text.ASCIIEncoding.ASCII.GetBytes(mensaje);

            string returnValue

                  = System.Convert.ToBase64String(toEncodeAsBytes);

            return returnValue;
        }
    }
}
