namespace API.Dtos;

public class MedicamentoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int CantidadDisponible { get; set; }
    public double Precio { get; set; }
    public int IdLaboratorioFk { get; set; }
    public LaboratorioDto Laboratorio { get; set; }
}