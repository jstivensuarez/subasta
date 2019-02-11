using AutoMapper;
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
using System.Text;
using System.Text.RegularExpressions;

namespace Subasta.core.services
{
    public class AnimalService : IAnimalService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;
        readonly IFileHelper fileHelper;
        public AnimalService(IMapper mapper, IUnitOfWork uowService,
            IFileHelper fileHelper)
        {
            this.mapper = mapper;
            this.uowService = uowService;
            this.fileHelper = fileHelper;
        }

        public void Add(AnimalDto dto)
        {
            try
            {

                if (dto.Imagen != null)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(dto.Imagen.ContentDisposition).FileName.Trim('"');
                    dto.Foto = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                    fileHelper.DownLoadFile("images//ANIMALES", dto.Imagen, dto.Foto);
                }
                else
                {
                    dto.Foto = dto.Video;
                }
                dto.Activo = true;
                uowService.AnimalRepository.Add(mapper.Map<Animal>(dto));
                ActualizarInformacionLote(dto.LoteId, 1, dto.Peso);
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar agregar el animal", ex);
            }
        }

        public void Delete(AnimalDto animal)
        {
            try
            {
                var lote = mapper.Map<LoteDto>(uowService.LoteRepository.GetAllWithInclude()
                  .Find(a => a.LoteId == animal.LoteId));
                animal.Activo = false;
                uowService.AnimalRepository.Edit(mapper.Map<Animal>(animal));
                ActualizarInformacionLote(animal.LoteId, -1, -animal.Peso);
                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar eliminar el animal", ex);
            }
        }

        public void Edit(AnimalDto dto)
        {
            try
            {

                var animal = uowService.AnimalRepository.Find(dto.AnimalId);
                if (dto.Imagen != null)
                {
                    var regex = @"^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$";
                    var esVideo = Regex.Match(dto.Foto, regex, RegexOptions.IgnoreCase);
                    if (!esVideo.Success)
                        fileHelper.RemoveFile("images//ANIMALES", dto.Foto);
                    string fileName = ContentDispositionHeaderValue.Parse(dto.Imagen.ContentDisposition).FileName.Trim('"');
                    dto.Foto = $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileName)}";
                    fileHelper.DownLoadFile("images//ANIMALES", dto.Imagen, dto.Foto);
                }
                if (dto.Video != null)
                {
                    var regex = @"^(https?\:\/\/)?(www\.youtube\.com|youtu\.?be)\/.+$";
                    var esVideo = Regex.Match(dto.Foto, regex, RegexOptions.IgnoreCase);
                    if (!esVideo.Success)
                        fileHelper.RemoveFile("images//ANIMALES", dto.Foto);
                    dto.Foto = dto.Video;
                }
                dto.Activo = true;
                uowService.AnimalRepository.Edit(mapper.Map<Animal>(dto));

                if (animal.LoteId != dto.LoteId)
                {
                    ActualizarInformacionLote(dto.LoteId, 1, dto.Peso);
                    ActualizarInformacionLote(animal.LoteId, -1, -dto.Peso);
                }

                uowService.Save();
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar editar el animal", ex);
            }
        }

        public IQueryable<AnimalDto> Find(Expression<Func<Animal, bool>> predicate)
        {
            try
            {
                return mapper.Map<IQueryable<AnimalDto>>(uowService.AnimalRepository.Find(predicate));
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el animal", ex);
            }
        }

        public AnimalDto Find(object id)
        {
            try
            {
                var result = uowService.AnimalRepository.GetllWithInclude()
                    .Find(a => a.AnimalId == Convert.ToInt32(id));
                return mapper.Map<AnimalDto>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {

                throw new ExceptionCore("error al intentar buscar el animal", ex);
            }
        }

        public List<AnimalDto> GetAll()
        {
            try
            {
                var result = uowService.AnimalRepository.GetllWithInclude()
                    .OrderBy(a => a.LoteId)
                    .OrderBy(a => a.Descripcion);
                return mapper.Map<List<AnimalDto>>(result);
            }
            catch (ExceptionData)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ExceptionCore("error al intentar obtener los animales", ex);
            }
        }

        private void ActualizarInformacionLote(int loteId, int cantidad, decimal peso)
        {
            var lote = mapper.Map<LoteDto>(uowService.LoteRepository.GetAllWithInclude()
                                .Find(a => a.LoteId == loteId));
            lote.CantidadElementos += cantidad;
            lote.PesoTotal += peso;
            if (lote.PesoTotal != 0)
            {
                lote.PesoPromedio = lote.PesoTotal / lote.CantidadElementos;
            }
            else
            {
                lote.PesoPromedio = 0;
            }
            lote.PrecioInicial = lote.PrecioBase * lote.PesoPromedio;
            uowService.LoteRepository.Edit(mapper.Map<Lote>(lote));
        }
    }
}
