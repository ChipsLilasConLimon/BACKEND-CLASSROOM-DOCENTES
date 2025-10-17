using MySql.Data.MySqlClient;
using Dapper;
using AsistenciasyAlumnos.Modelos;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Repositorios
{
    public interface ITareaRepositorio
    {
        List<Tarea> ObtenerTodas(int id);
        Tarea ObtenerPorId(int id);
        List<TareaAlumno> ObtenerTareasAlumnos(int id);
        bool Agregar(Tarea tarea);
        bool ActualizarTarea(Tarea tarea);
        bool ActualizarTareaAlumno(CalificarTarea tareacalificar);
        bool Eliminar(int id);
        bool CrearActualizarTareaAlumno(SubirTareaAlumno subirTareaAlumno);
        public string ObtenerTareaParaEstatus(int id);
    }
    public class TareaRepositorio : ITareaRepositorio
    {
        private readonly string _connectionString;

        public TareaRepositorio(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySQL");

        }
        public bool Agregar(Tarea tarea) //TAREA DE DOCENTE
        {
            using var conexion = new MySqlConnection(_connectionString);

            string query = @" INSERT INTO tareas (Titulo, Descripcion, FechaEntrega, Material_Adicional, Puntuacion, Id_Grupo)
                          VALUES (@Titulo, @Descripcion, @FechaEntrega, @MaterialAdicional, @Puntuacion, @Id_Grupo);  SELECT LAST_INSERT_ID();";
            int idTarea = conexion.ExecuteScalar<int>(query, tarea);

            string queryInsertarAlumno = @"INSERT INTO tareas_Entregar (Id_Tarea, Id_Alumno) SELECT @Id_Tarea, Id_Alumno FROM alumnos_Por_Grupo
            WHERE Id_Grupo = @Id_Grupo;";
            int filasAfectadas = conexion.Execute(queryInsertarAlumno, new { Id_Tarea = idTarea, tarea.Id_Grupo });

            return filasAfectadas > 0;
        }

        public bool ActualizarTarea(Tarea tarea) //TAREA DOCENTE
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query = "UPDATE tareas SET Titulo = @Titulo, Descripcion = @Descripcion, FechaEntrega = @FechaEntrega, " +
                "Material_Adicional = @MaterialAdicional, Puntuacion = @Puntuacion WHERE Id = @Id;";
            int filasAfectadas = conexion.Execute(query, tarea);
            return filasAfectadas > 0;
        }

        public bool Eliminar(int id) //TAREA DOCENTE
        {
            using var conexion = new MySqlConnection(_connectionString);
            string queryCount = "SELECT COUNT(*) FROM tareas_Entregar WHERE Id_Tarea = @Id AND Estatus IN ('ENTREGADO', 'RETARDADO', 'CALIFICADO');";
            int ejecucion1 = conexion.QuerySingle<int>(queryCount, new { Id = id });
            
            if (ejecucion1 > 0) { return false; }

            string query2 =
                 "DELETE FROM tareas_Entregar WHERE Id_Tarea = @id; DELETE FROM tareas " +
                  "WHERE Id = @id;";
            int ejecucion2 = conexion.Execute(query2, new { Id = id });
            return ejecucion2 > 0;
        }

        public Tarea ObtenerPorId(int id) //TAREA DOCENTE
        {
            using var conexion = new MySqlConnection(_connectionString);

            string query =
                "SELECT * FROM tareas WHERE Id = @Id";
            var tarea = conexion.QueryFirstOrDefault<Tarea>(query, new { id });
            return tarea;
        }

        public List<Tarea> ObtenerTodas(int id) //TAREAS DOCENTE
        {
            using var conexion = new MySqlConnection(_connectionString);

            var tareas = conexion.Query<Tarea>(
                "SELECT * FROM tareas WHERE Id_Grupo = @id", new { id }).ToList();
            return tareas;
        }
        public List<TareaAlumno> ObtenerTareasAlumnos(int id) //ID DE LA TAREA       TAREAS ALUMNOS
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query = @" SELECT te.Id, te.Id_Tarea AS IdTarea, te.Estatus, te.Material_Adjunto AS MaterialAdjunto,
                            u.Nombre, u.Apellidos_Paterno AS ApellidosPaterno, u.Apellido_Materno AS ApellidoMaterno
                            FROM tareas_Entregar te INNER JOIN usuarios u ON u.Id = te.Id_Alumno WHERE te.Id_Tarea = @id";
            var lista = conexion.Query<TareaAlumno>(query, new { id }).ToList();
            return lista;
        }
        public bool ActualizarTareaAlumno(CalificarTarea tareacalificar) //OBJETO A ACTUALIZAR        TAREAS ALUMNOS
        {
            using var conexion = new MySqlConnection(_connectionString);
            string query = "UPDATE tareas_Entregar SET Estatus = @Estatus, Puntuacion = @Puntuacion, Comentarios = @Comentarios " +
                 "WHERE Id = @Id;";
            int filasAfectadas = conexion.Execute(query, tareacalificar);
            return filasAfectadas > 0;
        }

        //PARA QUE LOS ALUMNOS SUBAN LAS TAREAS
        public bool CrearActualizarTareaAlumno(SubirTareaAlumno subirTareaAlumno)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string querysubirtarea = "UPDATE tareas_Entregar SET Material_Adjunto = @MaterialAdjunto, Estatus = @Estatus WHERE Id = @Id;";
            int filasAfectadas = conexion.Execute(querysubirtarea, subirTareaAlumno);
            return filasAfectadas > 0;
        }
        public string ObtenerTareaParaEstatus(int id)
        {
            using var conexion = new MySqlConnection(_connectionString);
            string querytarea = "SELECT tareas.FechaEntrega FROM tareas INNER JOIN tareas_Entregar ON tareas.Id = tareas_Entregar.Id_Tarea " +
                "WHERE tareas_Entregar.Id = @Id;";
            var tarea = conexion.QueryFirstOrDefault<string>(querytarea, new { Id = id });
            return tarea;
        }
    }
}
