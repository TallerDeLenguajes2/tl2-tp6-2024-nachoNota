using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class UsuarioViewModel
{
    private string nombreUsuario;
    private string contrasena;
    
    [Required] 
    public string NombreUsuario { get; set; }

    [Required]
    [PasswordPropertyText]
    public string Contrasena { get; set; }
}