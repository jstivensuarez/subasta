using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class RazaRepository : GenericRepository<Raza>, IRazaRepository
    {
        readonly SubastaContext context;

        public RazaRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
