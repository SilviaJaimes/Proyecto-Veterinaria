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

    public async Task<IEnumerable<object>> MascotasVacunadas2023()
    {
        int year = 2023; 
        DateTime primerTrimestreInicio = new DateTime(year, 1, 1); 
        DateTime primerTrimestreFin = new DateTime(year, 3, 31); 

        var Vacunadas = await (
            from c in _context.Citas
            join m in _context.Mascotas on c.IdMascotaFk equals m.Id

            where c.Motivo == "Vacunación" && 
                c.Hora >= primerTrimestreInicio && c.Hora <= primerTrimestreFin

            select new{
                NombreMascota = m.Nombre,
                Motivo = c.Motivo,
                FechaNacimientoMascota = m.FechaNacimiento,
                FechaCita = c.Hora
            }).Distinct()
            .ToListAsync();
        return Vacunadas;
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

    public async Task<IEnumerable<object>> MascotasYPropietariosPorRaza(string Raza)
    {
        var mascotasYPropietariosPorRaza = await (
            from ma in _context.Mascotas
            join p in _context.Propietarios on ma.IdPropietarioFk equals p.Id
            where ma.Raza.Nombre.ToLower() == Raza.ToLower() 
            select new 
            {
                NombreMascota = ma.Nombre,
                NombrePropietario = p.Nombre
            }).ToListAsync();

        return mascotasYPropietariosPorRaza;
    }

    public async Task<IEnumerable<object>> CantidadMascotasPorRaza()
    {
        var mascotasPorRaza = await _context.Mascotas
            .GroupBy(m => m.IdRazaFk)
            .Select(g => new 
            {
                IdRaza = g.Key,
                Cantidad = g.Count()
            })
            .ToListAsync();

        var razas = await _context.Razas
            .ToDictionaryAsync(r => r.Id, r => r.Nombre);

        var resultado = mascotasPorRaza
            .Select(m => new 
            {
                Raza = razas[m.IdRaza],
                CantidadMascotas = m.Cantidad
            })
            .ToList();

        return resultado;
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