using Subasta.repository.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Subasta.repository.interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IAnimalRepository AnimalRepository { get; }
        IClienteRepository ClienteRepository { get; }
        IEventoRepository EventoRepository { get; }
        ILoteRepository LoteRepository { get; }
        ISubastaRepository SubastaRepository { get; }
        ITipoDocumentoRepository TipoDocumentoRepository { get; }
        IDepartamentoRepository DepartamentoRepository { get; }
        IMunicipioRepository MunicipioRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
        void Save();
    }
}
