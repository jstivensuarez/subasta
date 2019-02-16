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
        ICategoriaRepository CategoriaRepository { get; }
        IRazaRepository RazaRepository { get; }
        IPujadorRepository PujadorRepository { get; }
        ISolicitudRepository SolicitudRepository { get; }
        IClasificacionRepository ClasificacionRepository { get; }
        IRolRepository RolRepository { get; }
        void Save();
    }
}
