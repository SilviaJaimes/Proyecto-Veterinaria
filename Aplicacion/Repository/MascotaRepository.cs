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

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> MascotaEspeciePaginated(string Especie, int pageIndex, int pageSize, string search = null)
    {
        // Obtener la consulta base.
        var query = from ma in _context.Mascotas
                    where ma.Raza.Especie.Nombre.ToLower() == Especie.ToLower() 
                    select new 
                    {
                        Nombre = ma.Nombre,
                        Especie = ma.Raza.Especie.Nombre
                    };

        // Aplicar búsqueda si se proporciona el término de búsqueda.
        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.Nombre.ToLower().Contains(lowerSearch) || m.Especie.ToLower().Contains(lowerSearch));
        }

        // Obtener el total de registros de la consulta.
        int totalRegistros = await query.CountAsync();

        // Aplicar la paginación a la consulta.
        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> MascotasVacunadas2023Paginated(int pageIndex, int pageSize, string search = null)
    {
        int year = 2023; 
        DateTime primerTrimestreInicio = new DateTime(year, 1, 1); 
        DateTime primerTrimestreFin = new DateTime(year, 3, 31); 

        var query = from c in _context.Citas
                    join m in _context.Mascotas on c.IdMascotaFk equals m.Id

                    where c.Motivo == "Vacunación" && 
                        c.Hora >= primerTrimestreInicio && c.Hora <= primerTrimestreFin

                    select new{
                        NombreMascota = m.Nombre,
                        Motivo = c.Motivo,
                        FechaNacimientoMascota = m.FechaNacimiento,
                        FechaCita = c.Hora
                    };

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.Motivo.ToLower().Contains(lowerSearch));
        }

        var distinct = query.Distinct();

        int totalRegistros = await distinct.CountAsync();

        var registros = await distinct
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> MascotasPorEspeciePaginated(int pageIndex, int pageSize, string search = null)
    {
        var query = from ma in _context.Mascotas
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
                    };

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.Especie.Nombre.ToLower().Contains(lowerSearch));
        }

        int totalRegistros = await query.CountAsync();

        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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

    public async Task<(int totalRegistros, IEnumerable<Mascota> registros)> MascotasPorVeterinarioPaginated(string Veterinario, int pageIndex, int pageSize, string search = null)
    {
        var query = _context.Citas
                    .Include(c => c.Mascota)
                    .Where(c => c.Veterinario.Nombre == Veterinario)
                    .Select(c => c.Mascota);

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.Nombre.ToLower().Contains(lowerSearch));
        }

        var distinct = query.Distinct();

        int totalRegistros = await distinct.CountAsync();

        var registros = await distinct
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        return (totalRegistros, registros);
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

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> MascotasYPropietariosPorRazaPaginated(string Raza, int pageIndex, int pageSize, string search = null)
    {
        var query = from ma in _context.Mascotas
                    join p in _context.Propietarios on ma.IdPropietarioFk equals p.Id
                    where ma.Raza.Nombre.ToLower() == Raza.ToLower() 
                    select new 
                    {
                        NombreMascota = ma.Nombre,
                        NombrePropietario = p.Nombre
                    };

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.NombrePropietario.ToLower().Contains(lowerSearch) || m.NombreMascota.ToLower().Contains(lowerSearch));
        }

        int totalRegistros = await query.CountAsync();

        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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

    public async Task<(int totalRegistros, IEnumerable<object> registros)> CantidadMascotasPorRazaPaginated(int pageIndex, int pageSize, string search = null)
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

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            razas = razas.Where(r => r.Value.ToLower().Contains(lowerSearch)).ToDictionary(r => r.Key, r => r.Value);
            mascotasPorRaza = mascotasPorRaza.Where(m => razas.ContainsKey(m.IdRaza)).ToList();
        }

        var resultado = mascotasPorRaza
            .Select(m => new 
            {
                Raza = razas[m.IdRaza],
                CantidadMascotas = m.Cantidad
            });

        int totalRegistros = resultado.Count();

        var registros = resultado
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return (totalRegistros, registros);
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