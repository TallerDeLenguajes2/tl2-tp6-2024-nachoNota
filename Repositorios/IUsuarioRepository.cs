public interface IUsuarioRepository
{
    Usuario? GetUsuario(string usuario, string contrasena);

}