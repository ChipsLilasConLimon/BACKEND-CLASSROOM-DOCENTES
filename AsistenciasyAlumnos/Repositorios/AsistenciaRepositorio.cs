using MySql.Data.MySqlClient;
using TextAPI_D311.Modelos;
using Dapper;
using System.Diagnostics;
using System.Configuration;

namespace TextAPI_D311.Repositorios
{
    public interface IAsistenciaRepositorio
    {
        AsistenciaDiaria CrearAsistenciaDiaria(AsistenciaDiaria asistenciaDiaria);
        AsistenciaAlumnos RegistrarAsistenciaAlumno(AsistenciaAlumnos asistenciaAlumnos);
        List<AsistenciaAlumnos> ObtenerListaAsistencia(DateTime fecha, int idGrupo);
        List<AsistenciaDiaria> ObtenerTodasAsistencias();
        List<AsistenciaDiaria> ObtenerTodasAsistenciasGrupo(int idGrupo);
        bool EliminarAsistenciaDelDia(int id);
    }
    public class AsistenciaRepositorio : IAsistenciaRepositorio
    {
        private readonly String _connectionString;

        public AsistenciaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQL");
        }

        public AsistenciaDiaria CrearAsistenciaDiaria(AsistenciaDiaria asistenciaDiaria)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                "INSERT INTO asistencias_Diarias(Id_Maestro, IdGrupo, fecha )" +
                "VALUES (@IdMaestro, @IdGrupo, @Fecha); SELECT LAST_INSERT_ID();";

            int idGenerado = conexion.ExecuteScalar<int>(query, asistenciaDiaria);
            asistenciaDiaria.Id = idGenerado;
            return asistenciaDiaria;
        }

        public bool EliminarAsistenciaDelDia(int id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                 "DELETE FROM asistencias_Diarias " +
                  "WHERE Id = @Id";
            int ejecucion1 = conexion.Execute(query, new { Id = id });

            string query2 =
                 "DELETE FROM asistencia_Alumnos " +
                  "WHERE Id_Asistencia_Dia = @IdAsistenciaDia";
            int ejecucion2 = conexion.Execute(query2, new { IdAsistenciaDia = id });

            return ejecucion1 > 0;
        }

        public List<AsistenciaAlumnos> ObtenerListaAsistencia(DateTime fecha, int idGrupo)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query = @"
            SELECT aa.Id, aa.Id_Asistencia_Dia AS IdAsistenciaDia, aa.Id_Alumno AS IdAlumno, aa.Asistencia
            FROM asistencia_Alumnos aa
            INNER JOIN asistencias_Diarias ad ON aa.Id_Asistencia_Dia = ad.Id
            WHERE ad.IdGrupo = @IdGrupo AND DATE(ad.Fecha) = @Fecha";

            var lista = conexion.Query<AsistenciaAlumnos>(query, new { IdGrupo = idGrupo, Fecha = fecha }).ToList();
            return lista;
        }

        public List<AsistenciaDiaria> ObtenerTodasAsistencias()
        {
            using var conexion = new MySqlConnection(_connectionString);

            var asistencias = conexion.Query<AsistenciaDiaria>(
               "SELECT * from asistencias_Diarias").ToList();
            return asistencias;
        }

        public List<AsistenciaDiaria> ObtenerTodasAsistenciasGrupo(int idGrupo)
        {
            using var conexion = new MySqlConnection(_connectionString);
            var asistenciatodas = conexion.Query<AsistenciaDiaria>(
                "SELECT * FROM asistencias_Diarias WHERE IdGrupo = @IdGrupo", new { IdGrupo = idGrupo }).ToList();
            return asistenciatodas;
        }

        public AsistenciaAlumnos RegistrarAsistenciaAlumno(AsistenciaAlumnos asistenciaAlumnos)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query =
                "INSERT INTO asistencia_Alumnos(Id_Asistencia_Dia, Id_Alumno, Asistencia)" +
                "VALUES (@IdAsistenciaDia, @IdAlumno, @Asistencia); SELECT LAST_INSERT_ID();";

            int idGenerado = conexion.ExecuteScalar<int>(query, asistenciaAlumnos);
            asistenciaAlumnos.Id = idGenerado;
            return asistenciaAlumnos;
        }
    }

}
