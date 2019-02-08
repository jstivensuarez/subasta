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
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        readonly SubastaContext context;

        public ClienteRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }

        public List<Cliente> GetAllWithInclude()
        {
            try
            {
                var entitys = context.Clientes.AsNoTracking()
                    .Include(c => c.Municipio)
                    .Include(c => c.TipoDocumento)
                    .Where(c => c.Activo)
                    .ToList();
                return entitys;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }

        public Cliente GetWithAll(object id)
        {
            try
            {
                var entity = context.Clientes.AsNoTracking()
                    .Include(c => c.Municipio).Include(c => c.TipoDocumento)
                    .SingleOrDefault(c => c.ClienteId == Convert.ToString(id));
                return entity;
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al buscar la entidad", ex);
            }
        }

        public void LogicDelete(string id)
        {
            try
            {
                var entity = context.Clientes.Find(id);
                entity.Activo = false;
                context.Clientes.Update(entity);
            }
            catch (Exception ex)
            {
                throw new ExceptionData("error al eliminar la entidad", ex);
            }
        }
    }
}
