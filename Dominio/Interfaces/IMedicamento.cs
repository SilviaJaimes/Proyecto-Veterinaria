using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMedicamento : IGenericRepository<Medicamento>
{
    Task<IEnumerable<object>> MedicamentoPrecio50000();
}