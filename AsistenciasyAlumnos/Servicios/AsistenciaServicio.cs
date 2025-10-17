
using TextAPI_D311.Excepciones;
using TextAPI_D311.Modelos;
using TextAPI_D311.Repositorios;

namespace TextAPI_D311.Servicios
{
    public interface IAsistenciaServicio
    {
        AsistenciaDiaria CrearAsistenciaDiaria(AsistenciaDiaria asistenciaDiaria);
        AsistenciaAlumnos RegistrarAsistenciaAlumno(AsistenciaAlumnos asistenciaAlumnos);
        List<AsistenciaAlumnos> ObtenerListaAsistencia(DateTime fecha, int idGrupo);
        List<AsistenciaDiaria> ObtenerTodasAsistencias();
        List<AsistenciaDiaria> ObtenerTodasAsistenciasGrupo(int idGrupo);
        bool EliminarAsistenciaDelDia(int id);
    }
    public class AsistenciaServicio : IAsistenciaServicio
    {
        private readonly IAsistenciaRepositorio _aistenciaRepositorio;

        public AsistenciaServicio(IAsistenciaRepositorio asistenciaRepositorio)
        {
            _aistenciaRepositorio = asistenciaRepositorio;
        }


        public AsistenciaDiaria CrearAsistenciaDiaria(AsistenciaDiaria asistenciaDiaria)
        {
            bool x = false;
            if (asistenciaDiaria.IdGrupo == null || asistenciaDiaria.IdGrupo == 0) x = true;
            if (asistenciaDiaria.IdMaestro == null || asistenciaDiaria.IdMaestro == 0) x = true;
            if (asistenciaDiaria.Fecha == null || asistenciaDiaria.Fecha == default(DateTime)) x = true;
            if (x) throw new ValidacionExcepcion("Valores incompletos en la peticion");
            var asistencia = _aistenciaRepositorio.CrearAsistenciaDiaria(asistenciaDiaria);
            return asistencia;
        }

        public bool EliminarAsistenciaDelDia(int id)
        {
            var estatus = _aistenciaRepositorio.EliminarAsistenciaDelDia(id);
            if (!estatus) throw new NoDataExcepcion($"No se encontro el registro");
            return estatus;
        }

        public List<AsistenciaAlumnos> ObtenerListaAsistencia(DateTime fecha, int idGrupo)
        {
            var asistencia = _aistenciaRepositorio.ObtenerListaAsistencia(fecha, idGrupo);
            if(!asistencia.Any()) throw new NoDataExcepcion($"No se encontro asistencia del dia {fecha.ToShortDateString()} en el grupo {idGrupo}");
            return asistencia;
        }

        public List<AsistenciaDiaria> ObtenerTodasAsistencias()
        {
           var asistencias = _aistenciaRepositorio.ObtenerTodasAsistencias();
            if (!asistencias.Any()) throw new NoDataExcepcion($"No se encontro ninguna asistencia realizada");
            return asistencias;
        }

        public List<AsistenciaDiaria> ObtenerTodasAsistenciasGrupo(int idGrupo)
        {
            var asistencias = _aistenciaRepositorio.ObtenerTodasAsistenciasGrupo(idGrupo);
            if(!asistencias.Any()) throw new NoDataExcepcion($"No se encontraron asistencias del grupo {idGrupo}");
            return asistencias;
        }

        public AsistenciaAlumnos RegistrarAsistenciaAlumno(AsistenciaAlumnos asistenciaAlumnos)
        {
            bool x = false;
            if (asistenciaAlumnos.IdAlumno == null || asistenciaAlumnos.IdAlumno == 0) x = true;
            if (asistenciaAlumnos.IdAsistenciaDia == null || asistenciaAlumnos.IdAsistenciaDia == 0) x = true;
            if (x) throw new ValidacionExcepcion("Valores incompletos en la peticion");
            var asistencia = _aistenciaRepositorio.RegistrarAsistenciaAlumno(asistenciaAlumnos);
            return asistencia;
        }
    }
}
