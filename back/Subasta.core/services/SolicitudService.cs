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
    public class SolicitudService: ISolicitudService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IClienteService clienteService;
        public SolicitudService(IMapper mapper, IUnitOfWork uowService, IClienteService clienteService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.clienteService = clienteService;
        }

        public void Add(SolicitudSubastaDto dto, string usuario)
        {
            try
            {
                var cliente = clienteService.GetAll().SingleOrDefault( c => c.Usuario == usuario);
                dto.ClienteId = cliente.ClienteId;
                dto.Estado = Estados.PENDIENTE_APROBAR;
                uowService.SolicitudRepository.Add(mapper.Map<SolicitudSubasta>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la solicitud", ex);
            }
        }

        public void Add(SolicitudSubastaDto dto)
        {
            try
            {
                uowService.SolicitudRepository.Add(mapper.Map<SolicitudSubasta>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la solicitud", ex);
            }
        }

        public void Delete(SolicitudSubastaDto entity)
        {
            try
            {
                uowService.SolicitudRepository.Delete(mapper.Map<SolicitudSubasta>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar eliminar la solicitud", ex);
            }
        }

        public void Edit(SolicitudSubastaDto entity)
        {
            try
            {
                uowService.SolicitudRepository.Edit(mapper.Map<SolicitudSubasta>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla la solicitud", ex);
            }
        }

        public void Aceptar(SolicitudSubastaDto entity)
        {
            try
            {
                entity.Estado = Estados.AUTORIZADO;
                uowService.SolicitudRepository.Edit(mapper.Map<SolicitudSubasta>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editarla la solicitud", ex);
            }
        }

        public IQueryable<SolicitudSubastaDto> Find(Expression<Func<SolicitudSubasta, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<SolicitudSubastaDto>>(uowService.SolicitudRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la solicitud", ex);
            }
        }

        public SolicitudSubastaDto Find(object id)
        {
            try
            {
                return mapper.Map<SolicitudSubastaDto>(uowService.SolicitudRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la solicitud", ex);
            }
        }

        public List<SolicitudSubastaDto> GetAll()
        {
            try
            {
                var result = uowService.SolicitudRepository.GetllWithInclude()
                    .Where(s => s.Estado == Estados.PENDIENTE_APROBAR || s.Estado == Estados.AUTORIZADO);
                return mapper.Map<List<SolicitudSubastaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener la solicitud", ex);
            }
        }

    }
}
