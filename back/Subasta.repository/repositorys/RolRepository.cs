using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class RolRepository: GenericRepository<Rol>, IRolRepository
    {
        readonly SubastaContext context;

        public RolRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
