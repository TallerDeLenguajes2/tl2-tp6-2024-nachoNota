using Microsoft.Data.Sqlite;

public class ProductosRepository : IProductosRepository{
    private const string cadenaConexion = "Data source=db/Tienda.db;Cache=Shared";

    public IEnumerable<Producto> GetAll()
    {
        var querystring = @"SELECT * FROM Productos";
        List<Producto> productos = new List<Producto>();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var producto = new Producto();
                    producto.Id = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    productos.Add(producto);
                }
            }
            
            connection.Close();
        }
        return productos;
    }

    public Producto GetById(int id)
    {
        var querystring = "SELECT * FROM Productos WHERE idProducto = @idProducto";
        var producto = new Producto();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idProducto", id));
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    producto.Id = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                }
            }

            command.ExecuteNonQuery();

            connection.Close();
        }
        return producto;
    }

    public void Create(Producto nuevoProducto)
    {
        var querystring = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);   

            command.Parameters.Add(new SqliteParameter("@Descripcion", nuevoProducto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", nuevoProducto.Precio));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    public void Update(Producto producto)
    {
        var querystring = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @idProducto";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idProducto", producto.Id));
            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void Delete(int id)
    {
        DeleteDetalleCompleto(id);
        var querystring = "DELETE FROM Productos WHERE idProducto = @idProducto";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion)){
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idProducto", id));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void DeleteDetalleCompleto(int idProd)
    {
        var querystring = "DELETE FROM PresupuestosDetalle WHERE idProducto = @idProd";
        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idProd", idProd));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

}
