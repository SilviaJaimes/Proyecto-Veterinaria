using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IVeterinario : IGenericRepository<Veterinario>
{
    Task<IEnumerable<Object>> VeterinariosEspecialidad(string Especialidad);
    Task<(int totalRegistros, IEnumerable<Object> registros)> VeterinariosEspecialidadPaginated(string Especialidad, int pageIndex, int pageSize, string search = null);
}