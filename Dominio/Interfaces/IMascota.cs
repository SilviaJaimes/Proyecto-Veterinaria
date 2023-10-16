using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMascota : IGenericRepository<Mascota>
{
    Task<IEnumerable<Object>> MascotaEspecie(string Especie);
    Task<IEnumerable<Mascota>> MascotasVacunadas2023();
    Task<IEnumerable<object>> MascotasPorEspecie();
    Task<IEnumerable<Mascota>> MascotasPorVeterinario(string Veterinario);
}