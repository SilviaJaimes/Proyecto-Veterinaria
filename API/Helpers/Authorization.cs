namespace API.Helpers;

public class Authorization
{
    public enum Roles
    {
        Empleado,
        Proveedor,
        Propietario,
        Veterinario
    }

    public const Roles rol_default = Roles.Empleado;
}
