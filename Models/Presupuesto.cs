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