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
using System.Threading;

namespace Subasta.core.services
{
    public class TipoDocumentoService : ITipoDocumentoService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        public TipoDocumentoService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(TipoDocumentoDto dto)
        {
            try
            {
                uowService.TipoDocumentoRepository.Add(mapper.Map<TipoDocumento>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el TD", ex);
            }
        }

        public int AddWithReturn(TipoDocumentoDto dto)
        {
            try
            {
                return uowService.TipoDocumentoRepository.Add(mapper.Map<TipoDocumento>(dto));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el TD", ex);
            }
        }

        public void Delete(TipoDocumentoDto entity)
        {
            try
            {
                var clientes = uowService.ClienteRepository.GetAll().Where(c => c.TipoDocumentoId == entity.TipoDocumentoId);
                if (clientes.Count() == 0)
                {
                    uowService.TipoDocumentoRepository.Delete(mapper.Map<TipoDocumento>(entity));
                    uowService.Save();
                }
                else {
                    throw new ExceptionCore("Entidad en uso");
                }

            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el TD", ex);
            }
        }

        public void Edit(TipoDocumentoDto entity)
        {
            try
            {
                uowService.TipoDocumentoRepository.Edit(mapper.Map<TipoDocumento>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el TD", ex);
            }
        }

        public IQueryable<TipoDocumentoDto> Find(Expression<Func<TipoDocumento, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<TipoDocumentoDto>>(uowService.TipoDocumentoRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el TD", ex);
            }
        }

        public TipoDocumentoDto Find(object id)
        {
            try
            {
                return mapper.Map<TipoDocumentoDto>(uowService.TipoDocumentoRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el TD", ex);
            }
        }

        public List<TipoDocumentoDto> GetAll()
        {
            try
            {
                var result = uowService.TipoDocumentoRepository.GetAll();
                return mapper.Map<List<TipoDocumentoDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los TD", ex);
            }
        }
    }
}
