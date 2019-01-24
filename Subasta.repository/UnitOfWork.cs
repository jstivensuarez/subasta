using Subasta.repository.interfaces;
using Subasta.repository.repositorys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private SubastaContext context;
        private AnimalRepository animalRepository;

        public UnitOfWork(SubastaContext context)
        {
            this.context = context;
        }

        public IAnimalRepository AnimalRepository
        {
            get
            {
                if (this.animalRepository == null)
                {
                    this.animalRepository = new AnimalRepository(context);
                }
                return animalRepository;
            }
        }

       
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
