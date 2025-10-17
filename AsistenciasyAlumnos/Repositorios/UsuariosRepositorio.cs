using AsistenciasyAlumnos.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Repositorios
{
    public interface IUsuariosRepositorio
    {
        List<Usuario> ObtenerListaUsuario();

    }
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly string _connectionString;
        public UsuariosRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQL");

        }
        public List<Usuario> ObtenerListaUsuario()
        {
            using var conexion = new MySqlConnection(_connectionString);

            var alumnos = conexion.Query<Usuario>(
               "SELECT * from usuarios").ToList();
            return alumnos;
        }

    }
}
