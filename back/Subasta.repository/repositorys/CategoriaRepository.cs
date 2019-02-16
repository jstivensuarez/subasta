using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        readonly SubastaContext context;

        public CategoriaRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public int Add(Categoria entity)
        {
            try
            {
                context.Categoria.Add(entity);
                context.SaveChanges();
                return entity.CategoriaId;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }
    }
}
