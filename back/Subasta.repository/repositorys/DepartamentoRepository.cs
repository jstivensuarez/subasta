using Subasta.repository.exceptions;
using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class DepartamentoRepository: GenericRepository<Departamento>, IDepartamentoRepository
    {
        readonly SubastaContext context;

        public DepartamentoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public int Add(Departamento entity)
        {
            try
            {
                context.Departamentos.Add(entity);
                context.SaveChanges();
                return entity.DepartamentoId;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }
    }
}
