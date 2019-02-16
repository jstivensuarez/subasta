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
    public class MunicipioRepository: GenericRepository<Municipio>, IMunicipioRepository
    {
        readonly SubastaContext context;

        public MunicipioRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public int Add(Municipio entity)
        {
            try
            {
                context.Municipios.Add(entity);
                context.SaveChanges();
                return entity.MunicipioId;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al agregar la entidad", ex);
            }
        }
    }
}
