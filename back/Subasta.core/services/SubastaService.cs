using AutoMapper;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.interfaces;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Subasta.core.services
{
    public class SubastaService: ISubastaService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public SubastaService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(SubastaDto dto)
        {
            try
            {
                dto.HoraFin = new DateTime(dto.HoraFin.Year, dto.HoraFin.Month, dto.HoraFin.Day, 
                    dto.HoraFinAux.Hour, dto.HoraFinAux.Minute, dto.HoraFinAux.Second);
                dto.HoraInicio = new DateTime(dto.HoraInicio.Year, dto.HoraInicio.Month, dto.HoraInicio.Day,
                    dto.HoraInicioAux.Hour, dto.HoraInicioAux.Minute, dto.HoraInicioAux.Second);
                uowService.SubastaRepository.Add(mapper.Map<repository.models.Subasta>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar la subasta", ex);
            }
        }

        public void Delete(SubastaDto entity)
        {
            try
            {
                entity.Activo = false;
                uowService.SubastaRepository.Edit(Mapper.Map<repository.models.Subasta>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar la subasta", ex);
            }
        }

        public void Edit(SubastaDto dto)
        {
            try
            {
                dto.HoraFin = new DateTime(dto.HoraFin.Year, dto.HoraFin.Month, dto.HoraFin.Day,
                  dto.HoraFinAux.Hour, dto.HoraFinAux.Minute, dto.HoraFinAux.Second);
                dto.HoraInicio = new DateTime(dto.HoraInicio.Year, dto.HoraInicio.Month, dto.HoraInicio.Day,
                    dto.HoraInicioAux.Hour, dto.HoraInicioAux.Minute, dto.HoraInicioAux.Second);
                uowService.SubastaRepository.Edit(mapper.Map<repository.models.Subasta>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar la subasta", ex);
            }
        }

        public IQueryable<SubastaDto> Find(Expression<Func<repository.models.Subasta, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<SubastaDto>>(uowService.SubastaRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la subasta", ex);
            }
        }

        public SubastaDto Find(object id)
        {
            try
            {
                return mapper.Map<SubastaDto>(uowService.SubastaRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar la subasta", ex);
            }
        }

        public List<SubastaDto> GetAll()
        {
            try
            {
                var result = uowService.SubastaRepository.GetAllWithInclude();
                return mapper.Map<List<SubastaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener las subastas", ex);
            }
        }

        public List<SubastaDto> GetPorEvento(object id)
        {
            try
            {
                var result = uowService.SubastaRepository.GetPorEvento(id);
                return mapper.Map<List<SubastaDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener las subastas", ex);
            }
        }
    }
}
