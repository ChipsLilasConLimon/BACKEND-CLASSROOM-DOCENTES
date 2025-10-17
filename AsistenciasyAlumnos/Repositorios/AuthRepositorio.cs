using AsistenciasyAlumnos.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Repositorios
{
    public interface IAuthRepositorio
    {
        Usuario CrearUsuario(SignUp usuario);
        Usuario ObtenerUsuarioPorNombre(Login usuario);

    }
    public class AuthRepositorio : IAuthRepositorio
    {
        private readonly String _connectionString;
        public AuthRepositorio(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("MySQL");
        }

        public Usuario CrearUsuario(SignUp usuario)
        {
            using var conexion = new MySqlConnection(_connectionString);
            conexion.Open();
            string query =
                "INSERT INTO usuarios(Nombre_Usuario, Password_Usuario, Nombre, Apellidos_Paterno, Apellido_Materno)" +
                "VALUES (@NombreUsuario, @PasswordUsuario, @Nombre, @ApellidoPaterno, @ApellidoMaterno); SELECT LAST_INSERT_ID();";

            int idGenerado = conexion.ExecuteScalar<int>(query, usuario);
            var usuariocompelto = conexion.QuerySingle<Usuario>(
            "SELECT Id, " +
             "Nombre_Usuario AS NombreUsuario, " +
             "Password_Usuario AS PasswordUsuario, " +
             "Apellidos_Paterno AS ApellidoPaterno, " +
             "Apellido_Materno AS ApellidoMaterno, " +
             "Nombre AS Nombre, " +
             "Rol " +
             "FROM usuarios WHERE Id = @Id", new { Id = idGenerado });
            return usuariocompelto;
        }

        public Usuario ObtenerUsuarioPorNombre(Login usuario)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                @"SELECT Id,
                 Nombre_Usuario AS NombreUsuario,
                 Password_Usuario AS PasswordUsuario,
                 Nombre AS Nombre,
                 Apellidos_Paterno AS ApellidosPaterno,
                 Apellido_Materno AS ApellidoMaterno,
                 Rol
                 FROM usuarios
                 WHERE Nombre_Usuario = @username;";

            var u = conexion.QueryFirstOrDefault<Usuario>(query, new { username = usuario.username });
            return u;
        }
    }
}
