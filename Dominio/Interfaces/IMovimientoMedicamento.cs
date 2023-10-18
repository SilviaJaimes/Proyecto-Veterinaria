using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMovimientoMedicamento : IGenericRepository<MovimientoMedicamento>
{
    Task<IEnumerable<object>> MovimientosYValores();
    Task<(int totalRegistros, IEnumerable<Object> registros)> MovimientosYValoresPaginated(int pageIndex, int pageSize, string search = null);
}