using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
   public class SexoRepository: GenericRepository<Sexo>, ISexoRepository
    {
        readonly SubastaContext context;

        public SexoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
