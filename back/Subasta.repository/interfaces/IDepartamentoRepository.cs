using Subasta.repository.models;

namespace Subasta.repository.interfaces
{
    public interface IDepartamentoRepository : IGenericRepository<Departamento>
    {
        int Add(Departamento entity);
    }
}
