using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace Aplicacion.Repository;

public class CitaRepository : GenericRepository<Cita>, ICita
{
    private readonly ApiContext _context;

    public CitaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Cita>> GetAllAsync()
    {
        return await _context.Citas
            .ToListAsync();
    }

    public override async Task<Cita> GetByIdAsync(int id)
    {
        return await _context.Citas
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}
