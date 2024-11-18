using Microsoft.AspNetCore.Identity;

public class Usuario{
    private int id;
    private string nombre;
    private string nombreUsuario;
    private string contrasena;
    private Rol rol;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Contrasena { get => contrasena; set => contrasena = value; }
    public Rol Rol { get => rol; set => rol = value; }
    public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
    
    public Usuario(){}

    public Usuario(string nombre)
    {
        this.nombre = nombre;
    }

    public Usuario(UsuarioViewModel usuVM)
    {
        nombreUsuario = usuVM.NombreUsuario;  
        contrasena = usuVM.Contrasena;
    }
}

public enum Rol
{
    Administrador,
    Cliente,
    NoLogueado
}