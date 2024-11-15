public interface IUsuarioRepository
{
    List<Usuario> GetAll();
    Producto GetById(int id);
    public void Create(Producto nuevoProducto);
    public void Update(Producto producto);
    public void Delete(int id);

}