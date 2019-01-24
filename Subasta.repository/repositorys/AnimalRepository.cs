using Subasta.repository.interfaces;
using Subasta.repository.models;
using Subasta.repository.repositorys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.repositorys
{
    public class AnimalRepository: GenericRepository<Animal>, IAnimalRepository
    {
        readonly SubastaContext context;

        public AnimalRepository(SubastaContext context) : base(context)
        {
            this.context = context;
        }
    }
}
