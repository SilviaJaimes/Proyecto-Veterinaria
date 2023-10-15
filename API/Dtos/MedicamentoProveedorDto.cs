namespace API.Dtos;

public class MedicamentoProveedorDto
{
    public int IdMedicamentoFk { get; set; }
    public MedicamentoDto Medicamento { get; set; }
    public int IdProveedorFk { get; set; }
    public ProveedorDto Proveedor { get; set; }
}