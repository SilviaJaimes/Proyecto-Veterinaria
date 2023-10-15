using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IPropietario : IGenericRepository<Propietario>
{
    Task<IEnumerable<object>> PropietariosConMascotas();
}