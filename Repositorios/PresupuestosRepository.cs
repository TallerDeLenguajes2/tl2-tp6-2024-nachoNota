using System.ComponentModel;
using Microsoft.Data.Sqlite;
using SQLitePCL;

public class PresupuestoRepository
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

    public List<Presupuesto> listarPresupuestos()
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
                    presupuesto.FechaCreacion = (DateTime)reader["FechaCreacion"];
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    listaPresupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }

        return listaPresupuestos;
    }

    public Presupuesto GetDetalles(int id)
    {
        var querystring = "SELECT * FROM PresupuestosDetalle WHERE idPresupuesto = @idPresupuesto";
        var presupuesto = new Presupuesto();

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
                    presupuesto.añadirDetalle(detalle);
                }
            }
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
            command.Parameters.Add(new SqliteParameter("@idProducto", detalle.Producto.IdProducto));
            command.Parameters.Add(new SqliteParameter("@Cantidad", detalle.Cantidad));

            command.ExecuteNonQuery();

            connection.Close();
        }

    }

    public void delete(int id)
    {
        var querystring = "DELETE FROM Presupuesto WHERE idPresupuesto = @idPresupuesto";

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