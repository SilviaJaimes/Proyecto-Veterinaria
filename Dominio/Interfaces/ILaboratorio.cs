using Dominio.Entities;

namespace Dominio.Interfaces;

public interface ILaboratorio : IGenericRepository<Laboratorio>
{
    Task<IEnumerable<Object>> MedicamentoLaboratorio(string Laboratorio);
}