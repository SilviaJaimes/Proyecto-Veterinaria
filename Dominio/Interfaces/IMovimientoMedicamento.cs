using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMovimientoMedicamento : IGenericRepository<MovimientoMedicamento>
{
    Task<IEnumerable<object>> MovimientosYValores();
}