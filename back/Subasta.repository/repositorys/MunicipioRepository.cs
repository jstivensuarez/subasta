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
    public class MunicipioRepository: GenericRepository<Municipio>, IMunicipioRepository
    {
        readonly SubastaContext context;

        public MunicipioRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }      
    }
}
