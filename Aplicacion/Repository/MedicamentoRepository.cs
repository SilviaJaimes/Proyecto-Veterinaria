using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class MedicamentoRepository : GenericRepository<Medicamento>, IMedicamento
{
    private readonly ApiContext _context;

    public MedicamentoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> MedicamentoPrecio50000()
    {
        return await (
            from m in _context.Medicamentos
            where m.Precio > 50000
            select new
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Precio = m.Precio
            }
        ).ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> MedicamentoPrecio50000Paginated(int pageIndex, int pageSize, string search = null)
    {
        var query = from m in _context.Medicamentos
                            where m.Precio > 50000
                            select new
                            {
                                Id = m.Id,
                                Nombre = m.Nombre,
                                Precio = m.Precio
                            };

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.Precio.ToString().ToLower().Contains(lowerSearch));
        }

        int totalRegistros = await query.CountAsync();

        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<IEnumerable<Medicamento>> GetAllAsync()
    {
        return await _context.Medicamentos
            .ToListAsync();
    }

    public override async Task<Medicamento> GetByIdAsync(int id)
    {
        return await _context.Medicamentos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}