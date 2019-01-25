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
        private ClienteRepository clienteRepository;
        private EventoRepository eventoRepository;
        private LoteRepository loteRepository;

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

        public IClienteRepository ClienteRepository
        {
            get
            {
                if (this.clienteRepository == null)
                {
                    this.clienteRepository = new ClienteRepository(context);
                }
                return clienteRepository;
            }
        }

        public IEventoRepository EventoRepository
        {
            get
            {
                if (this.eventoRepository == null)
                {
                    this.eventoRepository = new EventoRepository(context);
                }
                return eventoRepository;
            }
        }

        public ILoteRepository LoteRepository
        {
            get
            {
                if (this.loteRepository == null)
                {
                    this.loteRepository = new LoteRepository(context);
                }
                return loteRepository;
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
