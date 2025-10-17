using AsistenciasyAlumnos.Modelos;
using Dapper;
using MySql.Data.MySqlClient;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Repositorios
{
    public interface IGruposRepositorio
    {
        List<Grupo> ObtenerTodosGruposDeDocente(int id);
    }
    public class GruposRepositorio : IGruposRepositorio
    {
        private readonly string _connectionString;

        public GruposRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQL");
        }

        public List<Grupo> ObtenerTodosGruposDeDocente(int id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            var data = conexion.Query<Grupo>(
               "SELECT grupos.Id, grupos.Nombre, grupos.Id_Docente, grupos.Semestre, grupos.Descripcion " +
               "FROM grupos INNER JOIN docentes_Por_Grupo ON grupos.Id = docentes_Por_Grupo.Id_Grupo WHERE docentes_Por_Grupo.Id_Docente = @id  " +
               "ORDER BY grupos.Id ASC;", new { id }).ToList();
            return data;
        }

    }
}
