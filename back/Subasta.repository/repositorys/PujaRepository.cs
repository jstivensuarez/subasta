using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class PujaRepository: GenericRepository<Puja>, IPujaRepository
    {
        readonly SubastaContext context;

        public PujaRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
