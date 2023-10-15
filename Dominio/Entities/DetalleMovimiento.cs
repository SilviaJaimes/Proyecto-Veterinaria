namespace Dominio.Entities;

public class DetalleMovimiento : BaseEntity
{
    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public int IdMedicamentoFk { get; set; }
    public Medicamento Medicamento { get; set; }
    public int IdMovMedFk { get; set; }
    public MovimientoMedicamento MovimientoMedicamento { get; set; }
}