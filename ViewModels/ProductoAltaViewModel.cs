public class ProductoAltaViewModel
{
    private List<Producto> productos;
    private int idPresupuesto;
    private int cantidad;

    public List<Producto> Productos { get => productos; }
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }

    public ProductoAltaViewModel()
    {

    }

    public ProductoAltaViewModel(int idPres, List<Producto> productos)
    {
        idPresupuesto = idPres;
        this.productos = productos; 
    }
}