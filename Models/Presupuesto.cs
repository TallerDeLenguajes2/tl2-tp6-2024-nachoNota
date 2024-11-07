public class Presupuesto
{
    private int id;
    private string nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public Presupuesto()
    {
        detalle = new List<PresupuestoDetalle>();
    }

    public int Id { get => id; set => id = value; }
    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; }

    public int MontoPresupuesto()
    {
        int monto = 0;
        foreach(var det in Detalle)
        {
            monto += det.Cantidad * det.Producto.Precio;
        }
        return monto;
    }

    public double MontoPresupuestoConIVA()
    {
        return MontoPresupuesto() * 1.21;
    }

    public int CantidadProductos()
    {
        int cant = 0;
        foreach(var det in detalle)
        {
            cant += det.Cantidad;
        }

        return cant;
    }

    public void añadirDetalle(PresupuestoDetalle detalle)
    {
        Detalle.Add(detalle);
    }

    public void añadirDetalle(List<PresupuestoDetalle> detalles)
    {
        foreach(var det in detalles)
        {
            añadirDetalle(det);
        }
    }
}