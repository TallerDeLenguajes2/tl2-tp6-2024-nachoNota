public class Cliente
{
    private int idCliente;
    private string nombre;
    private string email;
    private string telefono;

    public int IdCliente { get => idCliente; set => idCliente = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Email { get => email; set => email = value; }
    public string Telefono { get => telefono; set => telefono = value; }

    public void AsignarId(int id)
    {
        idCliente = id;
    }
}