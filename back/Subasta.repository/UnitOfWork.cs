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
        private SubastaRepository subastaRepository;
        private TipoDocumentoRepository tipoDocumentoRepository;
        private DepartamentoRepository departamentoRepository;
        private MunicipioRepository municipioRepository;

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

        public ISubastaRepository SubastaRepository
        {
            get
            {
                if (this.subastaRepository == null)
                {
                    this.subastaRepository = new SubastaRepository(context);
                }
                return subastaRepository;
            }
        }

        public ITipoDocumentoRepository TipoDocumentoRepository
        {
            get
            {
                if (this.tipoDocumentoRepository == null)
                {
                    this.tipoDocumentoRepository = new TipoDocumentoRepository(context);
                }
                return tipoDocumentoRepository;
            }
        }

        public IDepartamentoRepository DepartamentoRepository
        {
            get
            {
                if (this.departamentoRepository == null)
                {
                    this.departamentoRepository = new DepartamentoRepository(context);
                }
                return departamentoRepository;
            }
        }

        public IMunicipioRepository MunicipioRepository
        {
            get
            {
                if (this.municipioRepository == null)
                {
                    this.municipioRepository = new MunicipioRepository(context);
                }
                return municipioRepository;
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
