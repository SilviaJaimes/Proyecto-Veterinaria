namespace API.Dtos;

public class RazaDto
{
    public int Id { get; set; }
    public int IdEspecieFk { get; set; }
    public EspecieDto Especie { get; set; }
}