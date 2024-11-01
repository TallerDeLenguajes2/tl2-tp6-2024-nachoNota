using Microsoft.Data.Sqlite;

public class ProductosRepository{
    private const string cadenaConexion = "Data source=db/Tienda.db;Cache=Shared";

    public List<Producto> listarProductos()
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
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    productos.Add(producto);
                }
            }
            
            connection.Close();
        }
        return productos;
    }

    public Producto GetProducto(int id)
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
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
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

            command.Parameters.Add(new SqliteParameter("@idProducto", producto.IdProducto));
            command.Parameters.Add(new SqliteParameter("@Descripcion", producto.Descripcion));
            command.Parameters.Add(new SqliteParameter("@Precio", producto.Precio));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void delete(int id)
    {
        var querystring = "DELETE FROM Productos WHERE idProducto = @idProducto";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion)){
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idProducto", id));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

}
