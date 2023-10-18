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

    public async Task<(int totalRegistros, IEnumerable<Object> registros)> ProveedorMedicamentoPaginated(string Medicamento, int pageIndex, int pageSize, string search = null)
    {
        var query = from mp in _context.MedicamentoProveedores
                    where mp.Medicamento.Nombre.ToLower() == Medicamento.ToLower() 
                    select new 
                    {
                        NombreMedicamento = mp.Medicamento.Nombre,
                        NombreProveedor = mp.Proveedor.Nombre
                    };

        if (!string.IsNullOrEmpty(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(m => m.NombreMedicamento.ToLower().Contains(lowerSearch));
        }

        int totalRegistros = await query.CountAsync();

        var registros = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
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