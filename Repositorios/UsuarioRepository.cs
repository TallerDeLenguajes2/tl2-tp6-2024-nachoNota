using Microsoft.Data.Sqlite;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString;

    public UsuarioRepository(string connectionString) {  _connectionString = connectionString; }

    public Usuario? GetUsuario(string nomUsuario, string contrasena)
    {
        string querystring = "SELECT * FROM Usuario WHERE usuario = @usu and contrasena = @contr";
        var usuario = new Usuario();

        using(SqliteConnection connection = new SqliteConnection(_connectionString))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(querystring, connection);
            command.Parameters.Add(new SqliteParameter("@usu", nomUsuario));
            command.Parameters.Add(new SqliteParameter("@contr", contrasena));

            using(SqliteDataReader reader = command.ExecuteReader())
            {
                if(!reader.HasRows) return null;

                while(reader.Read())
                {
                    usuario.Nombre = reader["nombre"].ToString();
                    usuario.Id = Convert.ToInt32(reader["id"]);
                    usuario.NombreUsuario = reader["usuario"].ToString();
                    usuario.Contrasena = reader["contrasena"].ToString();
                    int valorRol = Convert.ToInt32(reader["rol"]);
                    usuario.Rol = (Rol)valorRol;
                }
            }
            connection.Close();
        }
        return usuario;
    }
}