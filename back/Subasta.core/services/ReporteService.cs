using AutoMapper;
using Subasta.core.constants;
using Subasta.core.dtos.reportes;
using Subasta.core.exceptions;
using Subasta.core.interfaces;
using Subasta.core.states;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subasta.core.services
{
    public class ReporteService : IReporteService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IPujaService pujaService;
        public ReporteService(IMapper mapper, IUnitOfWork uowService, IPujaService pujaService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.pujaService = pujaService;
        }

        public List<LotesVendidos> GetLotesVendidos(int eventoId)
        {
            try
            {
                var reporte = (from evento in uowService.EventoRepository.GetAll()
                               .Where(e => e.Activo)
                               join subasta in uowService.SubastaRepository.GetAll()
                               .Where(s => s.Activo)
                               on evento.EventoId equals subasta.EventoId
                               join lote in uowService.LoteRepository.GetAll()
                               .Where(l => l.Activo)
                               on subasta.SubastaId equals lote.SubastaId
                               join confirmacion in uowService.ConfirmacionRepository.GetAll()
                               .Where(c => c.Estado == Estados.CONFIRMADO)
                               on lote.LoteId equals confirmacion.LoteId
                               where evento.EventoId == eventoId
                               select new LotesVendidos
                               {
                                   NombreLote = lote.Nombre,
                                   NombreVendedor = uowService.ClienteRepository.GetAll()
                                   .SingleOrDefault(c => c.ClienteId == lote.Cliente.ClienteId).Nombre,
                                   PrecioInicial = string.Format("{0:C}", lote.PrecioInicial),
                                   PrecioFinal = string.Format("{0:C}", pujaService.obtenerGanadorInfo(lote.LoteId).Valor)
                               }
                               ).ToList();
                return reporte;
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los lotes vendidos", ex);
            }
        }

        public List<CompradorLote> GetCompradoresPorLote(int loteId)
        {
            try
            {
                var reporte = (from lote in uowService.LoteRepository.GetAll()
                               .Where(l => l.Activo)
                               join pujador in uowService.PujadorRepository.GetAll()
                               .Where(p => p.LoteId == loteId)
                               on lote.LoteId equals pujador.LoteId
                               where lote.LoteId == loteId
                               select new CompradorLote
                               {
                                   PujaMayor = string.Format("{0:C}", uowService.PujaRepository.GetAll()
                                   .OrderByDescending(p => p.Valor)
                                   .FirstOrDefault(p => p.PujadorId == pujador.PujadorId).Valor),
                                   NombreComprador = uowService.ClienteRepository.GetAll()
                                   .SingleOrDefault(c => c.ClienteId == pujador.ClienteId).Nombre,
                                   CantidadPujas = uowService.PujaRepository.GetAll()
                                   .Where(p => p.PujadorId == pujador.PujadorId).Count()
                               }).ToList();
                return reporte;
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los lotes vendidos", ex);
            }
        }

        public Total GetTotal()
        {
            try
            {
                var total = new Total
                {
                    Animales = uowService.AnimalRepository.GetllWithInclude().Count,
                    Clientes = uowService.ClienteRepository.GetAllWithInclude()
                    .Where(c => c.Tipo == TipoUsuarios.PUJADOR).Count(),
                    Eventos = uowService.EventoRepository.GetAllWithInclude().Count,
                    Lotes = uowService.LoteRepository.GetAllWithInclude().Count,
                    Propietarios = uowService.ClienteRepository.GetAllWithInclude()
                     .Where(c => c.Tipo == TipoUsuarios.PROPIETARIO).Count(),
                    Pujas = uowService.PujaRepository.GetAll().Count,
                    Subastas = uowService.SubastaRepository.GetAllWithInclude().Count
                };
                return total;
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los lotes vendidos", ex);
            }
        }
    }
}
