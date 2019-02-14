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
        private UsuarioRepository usuarioRepository;
        private CategoriaRepository categoriaRepository;
        private RazaRepository razaRepository;
        private SexoRepository sexoRepository;
        private PujadorRepository pujadorRepository;
        private SolicitudRepository solicitudRepository;
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

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                if (this.usuarioRepository == null)
                {
                    this.usuarioRepository = new UsuarioRepository(context);
                }
                return usuarioRepository;
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                if (this.categoriaRepository == null)
                {
                    this.categoriaRepository = new CategoriaRepository(context);
                }
                return categoriaRepository;
            }
        }

        public IRazaRepository RazaRepository
        {
            get
            {
                if (this.razaRepository == null)
                {
                    this.razaRepository = new RazaRepository(context);
                }
                return razaRepository;
            }
        }

        public ISexoRepository SexoRepository
        {
            get
            {
                if (this.sexoRepository == null)
                {
                    this.sexoRepository = new SexoRepository(context);
                }
                return sexoRepository;
            }
        }

        public IPujadorRepository PujadorRepository
        {
            get
            {
                if (this.pujadorRepository == null)
                {
                    this.pujadorRepository = new PujadorRepository(context);
                }
                return pujadorRepository;
            }
        }

        public ISolicitudRepository SolicitudRepository
        {
            get
            {
                if (this.solicitudRepository == null)
                {
                    this.solicitudRepository = new SolicitudRepository(context);
                }
                return solicitudRepository;
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
