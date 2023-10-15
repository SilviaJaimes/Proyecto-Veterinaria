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