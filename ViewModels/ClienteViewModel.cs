using System.ComponentModel.DataAnnotations;

public class ClienteViewModel
{
    private int idCliente;
    private string nombre;
    private string email;
    private string telefono;

    public int IdCliente { get => idCliente; set => idCliente = value; }
    
    [Required]
    public string Nombre { get => nombre; set => nombre = value; }
    
    [EmailAddress]
    public string Email { get => email; set => email = value; }
    
    [Phone]
    public string Telefono { get => telefono; set => telefono = value; }

    public ClienteViewModel()
    {

    }

    public ClienteViewModel(Cliente cliente)
    {
        idCliente = cliente.IdCliente;
        nombre = cliente.Nombre;
        email = cliente.Email;
        telefono = cliente.Telefono;
    }

    public void AsignarId(int id)
    {
        idCliente = id;
    }
}