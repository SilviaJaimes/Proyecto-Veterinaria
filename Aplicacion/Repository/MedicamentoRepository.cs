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