using System.ComponentModel.DataAnnotations;

public class ProductoViewModel
{
    private int id;
    private string descripcion;
    private int precio;
    
    public int Id { get => id; set => id = value; }

    [StringLength(250, ErrorMessage = "la descripicion es de 250 caracteres como maximo")]
    public string Descripcion { get => descripcion; set => descripcion = value; }
    
    [Required(ErrorMessage = "Debe ingresar un precio")]
    [Range(0, int.MaxValue, ErrorMessage = "El precio ingresado debe ser positivo")]
    public int Precio { get => precio; set => precio = value; }

    public ProductoViewModel()
    {
        
    }

    public ProductoViewModel(Producto prod)
    {
        id = prod.Id;
        descripcion = prod.Descripcion;
        precio = prod.Precio;
    }

}