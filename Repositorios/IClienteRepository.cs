public interface IClienteRepository
{
    List<Cliente> GetAll();
    Cliente GetById(int id);
    public void Create(Cliente nuevoProducto);
    public void Update(Cliente producto);
    public void Delete(int id);

}