using Microsoft.EntityFrameworkCore;
using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class ClasificacionRepository: GenericRepository<Clasificacion>, IClasificacionRepository
    {
        readonly SubastaContext context;

        public ClasificacionRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public int Add(Clasificacion entity)
        {
            try
            {
                context.Clasificaciones.Add(entity);
                context.SaveChanges();
                return entity.ClasificacionId;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }

        public List<Clasificacion> GetllWithInclude()
        {
            try
            {
                var entitys = context.Clasificaciones.AsNoTracking()
                    .Include(c => c.Categoria)
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
