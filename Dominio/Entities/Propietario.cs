namespace Dominio.Entities;

public class Propietario : BaseEntity
{
    public string Nombre { get; set; }
    public string CorreoElectronico { get; set; }
    public string Telefono { get; set; }

    public ICollection<Mascota> Mascotas { get; set; }
}