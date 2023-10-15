namespace Dominio.Entities;

public class Raza : BaseEntity
{
    public string Nombre { get; set; }
    public int IdEspecieFk { get; set; }
    public Especie Especie { get; set; }

    public virtual Mascota Mascotas { get; set; }
}