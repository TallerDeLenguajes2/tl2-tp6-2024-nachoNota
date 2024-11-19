public class ProductoAltaViewModel
{
    private IEnumerable<Producto> productos;
    private int idPresupuesto;
    private int cantidad;

    public IEnumerable<Producto> Productos { get => productos; }
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }

    public ProductoAltaViewModel()
    {

    }

    public ProductoAltaViewModel(int idPres, IEnumerable<Producto> productos)
    {
        idPresupuesto = idPres;
        this.productos = productos; 
    }
}