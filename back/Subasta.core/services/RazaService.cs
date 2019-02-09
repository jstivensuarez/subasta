﻿using AutoMapper;
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

        public RazaService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
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

        public void Delete(RazaDto entity)
        {
            try
            {
                uowService.RazaRepository.Delete(mapper.Map<Raza>(entity));
                uowService.Save();
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
                var result = uowService.RazaRepository.GetAll();
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