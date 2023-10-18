using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IPropietario : IGenericRepository<Propietario>
{
    Task<IEnumerable<object>> PropietariosConMascotas();
    Task<(int totalRegistros, IEnumerable<Object> registros)> PropietariosConMascotasPaginated(int pageIndex, int pageSize, string search = null);
}