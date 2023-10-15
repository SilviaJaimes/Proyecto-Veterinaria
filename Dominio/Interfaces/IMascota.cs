using Dominio.Entities;

namespace Dominio.Interfaces;

public interface IMascota : IGenericRepository<Mascota>
{
    Task<IEnumerable<object>> MascotaEspecie(string Especie);
    Task<IEnumerable<object>> MascotasVacunadas2023();
    Task<IEnumerable<object>> MascotasPorEspecie();
}