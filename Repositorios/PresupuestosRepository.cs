using System.ComponentModel;
using Microsoft.Data.Sqlite;
using SQLitePCL;

public class PresupuestosRepository
{
    private const string cadenaConexion = "Data source=db/Tienda.db;Cache=Shared";

    public void create(Presupuesto presupuesto)
    {
        var querystring = "INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@NombreDestinatario, @FechaCreacion)";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@NombreDestinatario", presupuesto.NombreDestinatario));
            command.Parameters.Add(new SqliteParameter("@FechaCreacion", presupuesto.FechaCreacion));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Presupuesto> GetPresupuestos()
    {
        var querystring = "SELECT * FROM Presupuestos";
        var listaPresupuestos = new List<Presupuesto>();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var presupuesto = new Presupuesto();
                    
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    presupuesto.Id = Convert.ToInt32(reader["idPresupuesto"]);
                    
                    presupuesto.añadirDetalle(GetDetalles(presupuesto.Id));
                    listaPresupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }

        return listaPresupuestos;
    }

    public List<PresupuestoDetalle> GetDetalles(int id)
    {
        var querystring = "SELECT * FROM PresupuestosDetalle WHERE idPresupuesto = @idPresupuesto";
        var detalles = new List<PresupuestoDetalle>();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);
            command.Parameters.Add(new SqliteParameter("@idPresupuesto", id));

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var detalle = new PresupuestoDetalle();
                    detalle.asignarProd(Convert.ToInt32(reader["idProducto"]));
                    detalle.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                    detalles.Add(detalle);
                }
            }
        }
        return detalles;
    }

    public Presupuesto GetPresupuesto(int id)
    {
        string querystring = "SELECT * FROM Presupuestos WHERE idPresupuesto = @id";
        var presupuesto = new Presupuesto();

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    presupuesto.Id = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    presupuesto.añadirDetalle(GetDetalles(presupuesto.Id));
                }
            }
            connection.Close();
        }
        return presupuesto;
    }

    public void agregarDetalle(PresupuestoDetalle detalle, int idPresupuesto)
    {
        var querystring = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPresupuesto, @idProducto, @Cantidad)";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
            command.Parameters.Add(new SqliteParameter("@idProducto", detalle.Producto.Id));
            command.Parameters.Add(new SqliteParameter("@Cantidad", detalle.Cantidad));

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public void delete(int id)
    {
        var querystring = "DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";

        using(SqliteConnection connection = new SqliteConnection(cadenaConexion))
        {
            connection.Open();

            SqliteCommand command = new SqliteCommand(querystring, connection);

            command.Parameters.Add(new SqliteParameter("@idPresupuesto", id));
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}