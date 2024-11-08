using Microsoft.Data.Sqlite;

public class ClienteRepository
{
    private const string cadenaConexion = "Data source=db/Tienda.db;Cache=Shared";

    public List<Cliente> listarProductos()
    {
        var querystring = @"SELECT * FROM Cliente";
        List<Cliente> clientes = new List<Cliente>();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var cliente = new Cliente();
                    cliente.AsignarId(Convert.ToInt32(reader["idCliente"]));
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();

                    clientes.Add(cliente);
                }
            }
            
            connection.Close();
        }
        return clientes;
    }

    public Cliente GetCliente(int id)
    {
        var querystring = "SELECT * FROM Cliente WHERE idCliente = @id";
        var cliente = new Cliente();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@id", id));
            
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    cliente.AsignarId(Convert.ToInt32(reader["idCliente"]));
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.Email = reader["Email"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                }
            }

            command.ExecuteNonQuery();

            connection.Close();
        }
        return cliente;
    }

    public void Create(Cliente nuevoCliente)
    {
        var querystring = @"INSERT INTO Cliente (Nombre, Email, Telefono) VALUES (@nom, @email, @tel)";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);   

            command.Parameters.Add(new SqliteParameter("@nom", nuevoCliente.Nombre));
            command.Parameters.Add(new SqliteParameter("@email", nuevoCliente.Email));
            command.Parameters.Add(new SqliteParameter("@tel", nuevoCliente.Telefono));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }
    public void Update(Cliente cliente)
    {
        var querystring = @"UPDATE Cliente SET Nombre = @nom, Email = @email, Telefono = @tel WHERE idCliente = @id";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@nom", cliente.Nombre));
            command.Parameters.Add(new SqliteParameter("@email", cliente.Email));
            command.Parameters.Add(new SqliteParameter("@tel", cliente.Telefono));
            command.Parameters.Add(new SqliteParameter("@id", cliente.IdCliente));


            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void delete(int id)
    {
        var querystring = "DELETE FROM Cliente WHERE idCliente = @id";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion)){
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@id", id));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

}