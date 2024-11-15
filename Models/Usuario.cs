using Microsoft.AspNetCore.Identity;

public class Usuario{
    private int id;
    private string nombre;
    private string nombreUsuario;
    private string contraseña;
    private Rol rol;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Contraseña { get => contraseña; set => contraseña = value; }
    public Rol Rol { get => rol; set => rol = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
}

public enum Rol
{
    Administrador,
    Cliente,
    NoLogueado
}