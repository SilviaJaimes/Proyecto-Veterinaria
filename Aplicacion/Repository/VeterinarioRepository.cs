using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class VeterinarioRepository : GenericRepository<Veterinario>, IVeterinario
{
    private readonly ApiContext _context;

    public VeterinarioRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Object>> VeterinariosEspecialidad(string Especialidad)
    {
        var veterinariosEspecialidad = await (
            from v in _context.Veterinarios
            where v.Especialidad.ToLower() == Especialidad.ToLower() 
            select new 
            {
                Nombre = v.Nombre,
                Especialidad = v.Especialidad
            }).ToListAsync();

        return veterinariosEspecialidad;
    }

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> VeterinariosEspecialidadPaginated(string Especialidad, int pageIndex, int pageSize, string search = null)
    {
        var query = from v in _context.Veterinarios
                    where v.Especialidad.ToLower() == Especialidad.ToLower() 
                    select new 
                    {
                        Nombre = v.Nombre,
                        Especialidad = v.Especialidad
                    };

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.Nombre.ToLower().Contains(lowerSearch) || m.Especialidad.ToLower().Contains(lowerSearch));
        }

        int totalRegistros = await query.CountAsync();

        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<IEnumerable<Veterinario>> GetAllAsync()
    {
        return await _context.Veterinarios
            .ToListAsync();
    }

    public override async Task<Veterinario> GetByIdAsync(int id)
    {
        return await _context.Veterinarios
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}