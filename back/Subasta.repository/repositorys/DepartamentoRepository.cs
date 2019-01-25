using Subasta.repository.interfaces;
using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class DepartamentoRepository: GenericRepository<Departamento>, IDepartamentoRepository
    {
        readonly SubastaContext context;

        public DepartamentoRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
