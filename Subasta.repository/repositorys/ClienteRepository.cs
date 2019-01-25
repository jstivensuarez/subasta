using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
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
    }
}
