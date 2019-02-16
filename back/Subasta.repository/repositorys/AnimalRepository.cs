using Microsoft.EntityFrameworkCore;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using Subasta.repository.repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class AnimalRepository: GenericRepository<Animal>, IAnimalRepository
    {
        readonly SubastaContext context;

        public AnimalRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public List<Animal> GetllWithInclude()
        {
            try
            {
                var entitys = context.Animales.AsNoTracking()
                    .Include(c => c.Municipio)
                    .Include(c => c.Lote)
                    .Where(a => a.Activo)
                    .ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }
    }
}
