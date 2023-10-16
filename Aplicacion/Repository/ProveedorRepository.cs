using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class ProveedorRepository : GenericRepository<Proveedor>, IProveedor
{
    private readonly ApiContext _context;

    public ProveedorRepository(ApiContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<object>> ProveedorMedicamento(string Medicamento)
    {
        var proveedorMedicamento = await (
            from mp in _context.MedicamentoProveedores
            where mp.Medicamento.Nombre.ToLower() == Medicamento.ToLower() 
            select new 
            {
                NombreMedicamento = mp.Medicamento.Nombre,
                NombreProveedor = mp.Proveedor.Nombre
            }).ToListAsync();

        return proveedorMedicamento;
    }

    public override async Task<IEnumerable<Proveedor>> GetAllAsync()
    {
        return await _context.Proveedores
            .ToListAsync();
    }

    public override async Task<Proveedor> GetByIdAsync(int id)
    {
        return await _context.Proveedores
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}