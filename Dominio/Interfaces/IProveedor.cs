using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IProveedor : IGenericRepository<Proveedor>
{
    Task<IEnumerable<object>> ProveedorMedicamento(string Medicamento);
    Task<(int totalRegistros, IEnumerable<Object> registros)> ProveedorMedicamentoPaginated(string Medicamento, int pageIndex, int pageSize, string search = null);
}