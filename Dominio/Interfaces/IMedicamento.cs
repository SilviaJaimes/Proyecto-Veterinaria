using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMedicamento : IGenericRepository<Medicamento>
{
    Task<IEnumerable<object>> MedicamentoPrecio50000();
    Task<(int totalRegistros, IEnumerable<Object> registros)> MedicamentoPrecio50000Paginated(int pageIndex, int pageSize, string search = null);
}