using Subasta.core.interfaces;
using Subasta.repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subasta.core.services
{
    public class AnimalService: IAnimalService
    {
        readonly IUnitOfWork uowService; 

        public AnimalService(IUnitOfWork uowService)
        {
            this.uowService = uowService;
        }
    }
}
