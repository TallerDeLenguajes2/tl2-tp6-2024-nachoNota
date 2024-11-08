public class Presupuesto
{
    private int id;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;
    private Cliente cliente;

    public Presupuesto()
    {
        detalle = new List<PresupuestoDetalle>();
    }

    public int Id { get => id; set => id = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
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