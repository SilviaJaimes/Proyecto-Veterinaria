namespace API.Dtos;

public class MovimientoMedicamentoDto
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public DateOnly Fecha { get; set; }
    public int IdTipoMov { get; set; }
    public TipoMovimientoDto TipoMovimiento { get; set; }
}