using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMascota : IGenericRepository<Mascota>
{
    Task<IEnumerable<Object>> MascotaEspecie(string Especie);
    Task<(int totalRegistros, IEnumerable<Object> registros)> MascotaEspeciePaginated(string Especie, int pageIndex, int pageSize, string search = null);
    Task<IEnumerable<object>> MascotasVacunadas2023();
    Task<(int totalRegistros, IEnumerable<Object> registros)> MascotasVacunadas2023Paginated(int pageIndex, int pageSize, string search = null);
    Task<IEnumerable<object>> MascotasPorEspecie();
    Task<(int totalRegistros, IEnumerable<Object> registros)> MascotasPorEspeciePaginated(int pageIndex, int pageSize, string search = null);
    Task<IEnumerable<Mascota>> MascotasPorVeterinario(string Veterinario);
    Task<(int totalRegistros, IEnumerable<Mascota> registros)> MascotasPorVeterinarioPaginated(string Veterinario, int pageIndex, int pageSize, string search = null);
    Task<IEnumerable<object>> MascotasYPropietariosPorRaza(string Raza);
    Task<(int totalRegistros, IEnumerable<Object> registros)> MascotasYPropietariosPorRazaPaginated(string Raza, int pageIndex, int pageSize, string search = null);
    Task<IEnumerable<object>> CantidadMascotasPorRaza();
    Task<(int totalRegistros, IEnumerable<object> registros)> CantidadMascotasPorRazaPaginated(int pageIndex, int pageSize, string search = null);
}