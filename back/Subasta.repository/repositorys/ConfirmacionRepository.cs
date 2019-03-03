using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    class ConfirmacionRepository: GenericRepository<ConfirmacionPago>, IConfirmacionRepository
    {
        readonly SubastaContext context;

        public ConfirmacionRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
