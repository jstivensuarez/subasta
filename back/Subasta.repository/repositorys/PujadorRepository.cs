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
    public class PujadorRepository : GenericRepository<Pujador>, IPujadorRepository
    {
        readonly SubastaContext context;

        public PujadorRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }


        public List<Pujador> GetllWithInclude()
        {
            try
            {
                var entitys = context.Pujadores.AsNoTracking()
                    .Include(p => p.Cliente)
                    .Include(p => p.Lote)
                    .ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }

        public int AddWithReturn(Pujador pujador)
        {
            try
            {
                context.Pujadores.Add(pujador);
                context.SaveChanges();
                return pujador.PujadorId;
            }
            catch (Exception ex)
            {

                throw new ExceptionData("error al agregar el pujador", ex);
            }

        }
    }

}
