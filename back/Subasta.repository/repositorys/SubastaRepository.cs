using Subasta.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class SubastaRepository: GenericRepository<models.Subasta>, ISubastaRepository
    {
        readonly SubastaContext context;

        public SubastaRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
