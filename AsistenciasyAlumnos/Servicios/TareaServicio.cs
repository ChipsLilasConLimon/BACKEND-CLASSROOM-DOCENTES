using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Repositorios;
using TextAPI_D311.Excepciones;

namespace AsistenciasyAlumnos.Servicios
{
    public interface ITareaServicio
    {
        List<Tarea> ObtenerTodas(int id);
        Tarea ObtenerPorId(int id);
        List<TareaAlumno> ObtenerTareasAlumnos(int id);
        bool Agregar(Tarea tarea);
        bool Eliminar(int id);      
        bool ActualizarTarea(Tarea tarea);
        bool ActualizarTareaAlumno(CalificarTarea tareacalificar);
        bool CrearActualizarTareaAlumno(SubirTareaAlumno subirTareaAlumno);
    }

    public class TareaServicio : ITareaServicio
    {
        private readonly ITareaRepositorio _repositorio;

        public TareaServicio(ITareaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public Tarea? ObtenerPorId(int id)
        {
            return _repositorio.ObtenerPorId(id);
        }

        public List<Tarea> ObtenerTodas(int id)
        {
            var data = _repositorio.ObtenerTodas(id);
            if (!data.Any()) throw new NoDataExcepcion($"No se pudo obtener las tareas");
            return data;
        }

        public bool Agregar(Tarea tarea)
        {
           var estatus = _repositorio.Agregar(tarea);
           if(!estatus)throw new NoDataExcepcion($"No se pudo agregar la tarea");
           return estatus;
        }

        public bool ActualizarTarea(Tarea tarea)
        {
            var estatus = _repositorio.ActualizarTarea(tarea);
            if (!estatus) throw new NoDataExcepcion($"No se pudo agregar la tarea");
            return estatus;
        }

        public bool Eliminar(int id)
        {
            var estatus = _repositorio.Eliminar(id);
            if (!estatus) throw new NoDataExcepcion($"No se pudo eliminar la tarea");
            return estatus;
        }
        public List<TareaAlumno> ObtenerTareasAlumnos(int id)
        {
            var data = _repositorio.ObtenerTareasAlumnos(id);
            if (!data.Any()) throw new NoDataExcepcion($"No se pudo obtener a los alumnos");
            return data;
        }
        public bool ActualizarTareaAlumno(CalificarTarea tareacalificar)
        {
            var estatus = _repositorio.ActualizarTareaAlumno(tareacalificar);
            if (!estatus) throw new NoDataExcepcion($"No se pudo calificar la tarea");
            return estatus;
        }
        public bool CrearActualizarTareaAlumno(SubirTareaAlumno subirTareaAlumno)
        {
            var fechaDeTarea = _repositorio.ObtenerTareaParaEstatus(subirTareaAlumno.Id);
            if (fechaDeTarea == null) throw new NoDataExcepcion($"Error al obtener la fecha de la tarea");
            DateTime fechaActual = DateTime.Now;
            DateTime fechaEntrega;
            try
            {
                fechaEntrega = DateTime.Parse(fechaDeTarea);
            }
            catch (Exception ex) {
                throw new ValidacionExcepcion($"No se pudo convertir la fecha");
            }
            if (fechaActual > fechaEntrega)
            {
                subirTareaAlumno.Estatus = "ENTREGADO";
            } else
            {
                subirTareaAlumno.Estatus = "RETARDADO";
            }
            var estatus = _repositorio.CrearActualizarTareaAlumno(subirTareaAlumno);
            if (!estatus) throw new NoDataExcepcion($"No se pudo subir la tarea");
            return estatus;
        }
    }
}
