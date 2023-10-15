using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class LaboratorioRepository : GenericRepository<Laboratorio>, ILaboratorio
{
    private readonly ApiContext _context;

    public LaboratorioRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Object>> MedicamentoLaboratorio(string Laboratorio)
    {
        var medicamentoLaboratorio = await (
            from m in _context.Medicamentos
            join l in _context.Laboratorios on m.IdLaboratorioFk equals l.Id
            where l.Nombre.ToLower() == Laboratorio.ToLower() 
            select new 
            {
                Nombre = m.Nombre,
                Laboratorio = l.Nombre
            }).ToListAsync();

        return medicamentoLaboratorio;
    }

    public override async Task<IEnumerable<Laboratorio>> GetAllAsync()
    {
        return await _context.Laboratorios
            .ToListAsync();
    }

    public override async Task<Laboratorio> GetByIdAsync(int id)
    {
        return await _context.Laboratorios
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}