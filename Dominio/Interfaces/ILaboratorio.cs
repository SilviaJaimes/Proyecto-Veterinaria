using Dominio.Entities;

namespace Dominio.Interfaces;

public interface ILaboratorio : IGenericRepository<Laboratorio>
{
    Task<IEnumerable<Object>> MedicamentoLaboratorio(string Laboratorio);
    Task<(int totalRegistros, IEnumerable<Object> registros)> MedicamentoLaboratorioPaginated(string Laboratorio, int pageIndex, int pageSize, string search = null);
}