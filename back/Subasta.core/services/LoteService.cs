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
using System.Text.RegularExpressions;

namespace Subasta.core.services
{
    public class LoteService : ILoteService
    {

        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IFileHelper fileHelper;
        readonly IAnimalService animalService;
        public LoteService(IMapper mapper, IUnitOfWork uowService,
            IFileHelper fileHelper,
            IAnimalService animalService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.fileHelper = fileHelper;
            this.animalService = animalService;
        }

        public void Add(LoteDto dto)
        {
            try
            {
                if (dto.Imagen != null)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(dto.Imagen.ContentDisposition).FileName.Trim('"');
                    dto.FotoLote = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                    fileHelper.DownLoadFile("images//LOTES", dto.Imagen, dto.FotoLote);
                }
                else
                {
                    dto.FotoLote = dto.VideoLote;
                }
                dto.Activo = true;
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
                entity.Activo = false;
                uowService.LoteRepository.Edit(mapper.Map<Lote>(entity));
                eliminarAnimales(entity.LoteId);
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
                    var regex = @"^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$";
                    var esVideo = Regex.Match(dto.FotoLote, regex, RegexOptions.IgnoreCase);
                    if (!esVideo.Success)
                        fileHelper.RemoveFile("images//LOTES", dto.FotoLote);
                    string fileName = ContentDispositionHeaderValue.Parse(dto.Imagen.ContentDisposition).FileName.Trim('"');
                    dto.FotoLote = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                    fileHelper.DownLoadFile("images//LOTES", dto.Imagen, dto.FotoLote);
                }
                if (dto.VideoLote != null)
                {
                    var regex = @"^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$";
                    var esVideo = Regex.Match(dto.FotoLote, regex, RegexOptions.IgnoreCase);
                    if (!esVideo.Success)
                        fileHelper.RemoveFile("images//LOTES", dto.FotoLote);
                    dto.FotoLote = dto.VideoLote;
                }
                dto.Activo = true;
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
                var result = uowService.LoteRepository.GetAllWithInclude()
                   .Find(a => a.LoteId == Convert.ToInt32(id));
                return mapper.Map<LoteDto>(result);
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
                var result = uowService.LoteRepository.GetAllWithInclude()
                    .OrderBy(l => l.Nombre);
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

        public List<LoteDto> GetAllWithOutInclude()
        {
            try
            {
                var result = uowService.LoteRepository.GetAll()
                    .OrderBy(l => l.Nombre);
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


        public List<LoteDto> GetAllNoAssociate(string clienteId)
        {
            try
            {
                var result = uowService.LoteRepository.GetAllNoAssociate(clienteId)
                    .Where(l => l.Activo)
                    .OrderBy(l => l.Nombre);
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

        private void eliminarAnimales(int loteId)
        {
            var animales = animalService.GetAllWithOutInclude()
                .Where(a => a.LoteId == loteId).ToList();
            foreach (var animal in animales)
            {
                animal.Lote = null;
                animalService.Delete(animal);
            }
        }
    }
}
