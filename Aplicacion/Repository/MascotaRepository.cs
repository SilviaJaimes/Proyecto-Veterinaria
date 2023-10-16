using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class MascotaRepository : GenericRepository<Mascota>, IMascota
{
    private readonly ApiContext _context;

    public MascotaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Object>> MascotaEspecie(string Especie)
    {
        var mascotaEspecie = await (
            from ma in _context.Mascotas
            where ma.Raza.Especie.Nombre.ToLower() == Especie.ToLower() 
            select new 
            {
                Nombre = ma.Nombre,
                Especie = ma.Raza.Especie.Nombre
            }).ToListAsync();

        return mascotaEspecie;
    }

    public async Task<IEnumerable<Mascota>> MascotasVacunadas2023()
    {
        var inicioTrimestre = new DateOnly(2023, 1, 1);
        var finTrimestre = new DateOnly(2023, 3, 31);

        return await _context.Mascotas
            .Include(ma => ma.Citas)
            .Where(
                ma => ma.Citas.Any(
                c => c.Fecha >= inicioTrimestre && 
                c.Fecha <= finTrimestre && 
                c.Motivo.ToLower() == "vacunación")
            ).ToListAsync();
    }

    public async Task<IEnumerable<object>> MascotasPorEspecie()
    {
        return await (
            from ma in _context.Mascotas
            join r in _context.Razas on ma.IdRazaFk equals r.Id
            group ma by r.Especie into especies
            select new
            {
                Especie = especies.Key,
                Mascotas = especies.Select(mascota => new 
                {
                    Id = mascota.Id,
                    Nombre = mascota.Nombre,
                    Especie = mascota.Raza.Especie.Nombre
                }).ToList()
            }
        ).ToListAsync();
    }

    public async Task<IEnumerable<Mascota>> MascotasPorVeterinario(string Veterinario)
    {
        return await _context.Citas
            .Include(c => c.Mascota)
            .Where(c => c.Veterinario.Nombre == Veterinario)
            .Select(c => c.Mascota)
            .Distinct()
            .ToListAsync();
    }

    public override async Task<IEnumerable<Mascota>> GetAllAsync()
    {
        return await _context.Mascotas
            .ToListAsync();
    }

    public override async Task<Mascota> GetByIdAsync(int id)
    {
        return await _context.Mascotas
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}