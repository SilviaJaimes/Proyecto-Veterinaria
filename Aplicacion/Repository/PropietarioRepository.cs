using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class PropietarioRepository : GenericRepository<Propietario>, IPropietario
{
    private readonly ApiContext _context;

    public PropietarioRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> PropietariosConMascotas()
    {
        return await (
            from p in _context.Propietarios
            join ma in _context.Mascotas on p.Id equals ma.IdPropietarioFk
            select new
            {
                Propietario = p.Nombre,
                IdPropietario = p.Id,
                Mascota = ma.Nombre,
                IdMascota = ma.Id,
            }
        ).ToListAsync();
    }

    public override async Task<IEnumerable<Propietario>> GetAllAsync()
    {
        return await _context.Propietarios
            .ToListAsync();
    }

    public override async Task<Propietario> GetByIdAsync(int id)
    {
        return await _context.Propietarios
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}