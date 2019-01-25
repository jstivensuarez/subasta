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
using System.Text;

namespace Subasta.core.services
{
    public class AnimalService: IAnimalService
    {
        readonly IMapper mapper;
        readonly IUnitOfWork uowService;

        public AnimalService(IMapper mapper, IUnitOfWork uowService)
        {
            this.mapper = mapper;
            this.uowService = uowService;
        }

        public void Add(AnimalDto dto)
        {
            try
            {
                uowService.AnimalRepository.Add(mapper.Map<Animal>(dto));
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

        public void Delete(AnimalDto entity)
        {
            try
            {
                uowService.AnimalRepository.Delete(mapper.Map<Animal>(entity));
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

        public void Edit(AnimalDto entity)
        {
            try
            {
                uowService.AnimalRepository.Edit(mapper.Map<Animal>(entity));
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
                return mapper.Map<AnimalDto>(uowService.AnimalRepository.Find(id));
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
                var result = uowService.AnimalRepository.GetAll();
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
    }
}
