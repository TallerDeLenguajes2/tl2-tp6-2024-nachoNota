public interface IProductosRepository
{
    IEnumerable<Producto> GetAll();
    Producto GetById(int id);
    public void Create(Producto nuevoProducto);
    public void Update(Producto producto);
    public void Delete(int id);

}