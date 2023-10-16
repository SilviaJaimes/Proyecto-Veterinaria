using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class MovimientoMedicamentoRepository : GenericRepository<MovimientoMedicamento>, IMovimientoMedicamento
{
    private readonly ApiContext _context;

    public MovimientoMedicamentoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> MovimientosYValores()
    {
        return await (
            from mm in _context.MovimientoMedicamentos
            join dm in _context.DetalleMovimientos on mm.Id equals dm.IdMovMedFk
            group dm by new { mm.Id, mm.TipoMovimiento.Descripcion } into movimiento
            select new
            {
                Movimiento = movimiento.Key.Descripcion,
                ValorTotal = movimiento.Sum(dm => dm.Cantidad * dm.Precio)
            }
        ).ToListAsync();
    }

    public override async Task<IEnumerable<MovimientoMedicamento>> GetAllAsync()
    {
        return await _context.MovimientoMedicamentos
            .ToListAsync();
    }

    public override async Task<MovimientoMedicamento> GetByIdAsync(int id)
    {
        return await _context.MovimientoMedicamentos
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}