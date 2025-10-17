using AsistenciasyAlumnos.Modelos;
using Dapper;
using MySql.Data.MySqlClient;

namespace AsistenciasyAlumnos.Repositorios
{
    public interface IDatosUsuariosRepositorio
    {
        DatosUsuario ObtenerTodosLosDatosDeUsuario(int Id);
        bool ActualizarFotoDePeril(string url, int id);
        bool ActualizarPassword(string password, int id);
        bool ActualizarUsername(string username, int id);
    }

    public class DatosUsuarioRepositorio : IDatosUsuariosRepositorio
    {
        private readonly string _connectionString;

        public DatosUsuarioRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQL");
        }

        public bool ActualizarFotoDePeril(string url, int id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                "UPDATE datos_Usuario SET Url_Perfil = @url WHERE Id_Usuario = @id";
            int filasAfectadas = conexion.Execute(query, new { url, id });
            return filasAfectadas > 0;
        }

        public bool ActualizarPassword(string password, int id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                "UPDATE usuarios SET Password_Usuario = @password WHERE Id = @id";
            int filasAfectadas = conexion.Execute(query, new { password, id });
            return filasAfectadas > 0;
        }

        public bool ActualizarUsername(string username, int id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                "UPDATE usuarios SET Nombre_Usuario = @username WHERE Id = @id";
            int filasAfectadas = conexion.Execute(query, new { username, id });
            return filasAfectadas > 0;
        }

        public DatosUsuario ObtenerTodosLosDatosDeUsuario(int Id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query = @"
            SELECT 
            Id,
            Id_Usuario AS IdUsuario,
            Url_Perfil AS UrlPerfil,
            Cantidad_Grupos AS CantidadGrupos
            FROM datos_Usuario 
            WHERE Id_Usuario = @Id";
            var data = conexion.QueryFirstOrDefault<DatosUsuario>(query, new { Id });
            return data;
        }
    }
}
