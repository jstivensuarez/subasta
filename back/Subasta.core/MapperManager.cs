using AutoMapper;
using Subasta.core.dtos;
using Subasta.repository.models;

namespace Subasta.core
{
    public class MapperManager : Profile
    {
        public MapperManager()
        {
            //.ForMember(a => a.Cursos, opt=> opt.Ignore());
            CreateMap<Animal, AnimalDto>();
            CreateMap<AnimalDto, Animal>();

            CreateMap<Categoria, CategoriaDto>();
            CreateMap<CategoriaDto, Categoria>();

            CreateMap<Cliente, ClienteDto>();
            CreateMap<ClienteDto, Cliente>();

            CreateMap<Departamento, DepartamentoDto>();
            CreateMap<DepartamentoDto, Departamento>();

            CreateMap<Evento, EventoDto>();
            CreateMap<EventoDto, Evento>();

            CreateMap<Lote, LoteDto>();
            CreateMap<LoteDto, Lote>();

            CreateMap<Municipio, MunicipioDto>();
            CreateMap<MunicipioDto, Municipio>();

            CreateMap<Pujador, PujadorDto>();
            CreateMap<PujadorDto, Pujador>();

            CreateMap<Puja, PujaDto>();
            CreateMap<PujaDto, Puja>();

            CreateMap<Raza, RazaDto>();
            CreateMap<RazaDto, Raza>();

            CreateMap<repository.models.Subasta, SubastaDto>();
            CreateMap<SubastaDto, repository.models.Subasta>();

            CreateMap<TipoDocumento, TipoDocumentoDto>();
            CreateMap<TipoDocumentoDto, TipoDocumento>();

            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            CreateMap<Rol, RolDto>();
            CreateMap<RolDto, Rol>();

            CreateMap<SolicitudSubasta, SolicitudSubastaDto>();
            CreateMap<SolicitudSubastaDto, SolicitudSubasta>();

            CreateMap<Clasificacion, ClasificacionDto>();
            CreateMap<ClasificacionDto, Clasificacion>();
        }
    }
}
