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
    public class PujaService : IPujaService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public PujaService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(PujaDto dto)
        {
            try
            {

                dto.HoraPuja = DateTime.Now;
                var cliente = uowService.ClienteRepository.GetAll()
                    .SingleOrDefault(u => u.Usuario == dto.Usuario);
                var pujador = uowService.PujadorRepository.GetllWithInclude()
                   .SingleOrDefault(p => p.Estado != Estados.BORRADO && p.ClienteId == cliente.ClienteId
                   && p.LoteId == dto.LoteId);
                if (pujador == null)
                {
                    var pujadroDto = new PujadorDto();
                    pujadroDto.LoteId = dto.LoteId;
                    pujadroDto.ClienteId = cliente.ClienteId;
                    dto.PujadorId = uowService.PujadorRepository.AddWithReturn(mapper.Map<Pujador>(pujadroDto));
                }
                else {
                    dto.PujadorId = pujador.PujadorId;
                }
                uowService.PujaRepository.Add(mapper.Map<Puja>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
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
    }
}
