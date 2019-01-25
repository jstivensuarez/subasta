using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class LoteRepository: GenericRepository<Lote>, ILoteRepository
    {
        readonly SubastaContext context;

        public LoteRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
