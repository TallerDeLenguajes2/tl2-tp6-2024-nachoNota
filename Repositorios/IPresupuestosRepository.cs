public interface IPresupuestosRepository
{
    

    public List<Presupuesto> GetPresupuestos();
    public List<PresupuestoDetalle> GetDetallesById(int id);
    public Presupuesto GetById(int id);

    public void Create(Presupuesto presupuesto);
    public void Update(Presupuesto presupuesto);
    public void Delete(int id);

    public void AddDetalle(PresupuestoDetalle detalle, int idPresupuesto);

    public void AddDetalle(int idPresupuesto, int idProducto, int cantidad);

    public void DeleteDetalle(int idPres, int idProd);
}