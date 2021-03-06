﻿using AutoMapper;
using Subasta.core.dtos;
using Subasta.core.exceptions;
using Subasta.core.interfaces;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Subasta.core.services
{
    public class LoteService : ILoteService
    {

        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IFileHelper fileHelper;
        public LoteService(IMapper mapper, IUnitOfWork uowService, IFileHelper fileHelper)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.fileHelper = fileHelper;
        }

        public void Add(LoteDto dto)
        {
            try
            {
                string fileName = ContentDispositionHeaderValue.Parse(dto.Imagen.ContentDisposition).FileName.Trim('"');
                dto.FotoLote = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                fileHelper.DownLoadFile("images//LOTES", dto.Imagen, dto.FotoLote);
                uowService.LoteRepository.Add(mapper.Map<Lote>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el lote", ex);
            }
        }

        public void Delete(LoteDto entity)
        {
            try
            {
                uowService.LoteRepository.Delete(mapper.Map<Lote>(entity));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el lote", ex);
            }
        }

        public void Edit(LoteDto dto)
        {
            try
            {
                if (dto.Imagen != null)
                {
                    fileHelper.RemoveFile("images//LOTES", dto.FotoLote);
                    string fileName = ContentDispositionHeaderValue.Parse(dto.Imagen.ContentDisposition).FileName.Trim('"');
                    dto.FotoLote = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                    fileHelper.DownLoadFile("images//LOTES", dto.Imagen, dto.FotoLote);
                }
                uowService.LoteRepository.Edit(mapper.Map<Lote>(dto));
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el lote", ex);
            }
        }

        public IQueryable<LoteDto> Find(Expression<Func<Lote, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<LoteDto>>(uowService.LoteRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el lote", ex);
            }
        }

        public LoteDto Find(object id)
        {
            try
            {
                return mapper.Map<LoteDto>(uowService.LoteRepository.Find(id));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el lote", ex);
            }
        }

        public List<LoteDto> GetAll()
        {
            try
            {
                var result = uowService.LoteRepository.GetAll();
                return mapper.Map<List<LoteDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los lotes", ex);
            }
        }

        public List<LoteDto> GetllWithInclude()
        {
            try
            {
                var result = uowService.LoteRepository.GetllWithInclude();
                return mapper.Map<List<LoteDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los lotes", ex);
            }
        }
    }
}
