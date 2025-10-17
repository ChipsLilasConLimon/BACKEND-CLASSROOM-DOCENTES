using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Repositorios;
using TextAPI_D311.Excepciones;

namespace AsistenciasyAlumnos.Servicios
{
    public interface IGruposService
    {
        List<Grupo> ObtenerTodosGruposDeDocente(int id);
    }
    public class GruposService : IGruposService
    {
        private readonly IGruposRepositorio _gruposRepositorio;
        public GruposService(IGruposRepositorio gruposRepositorio)
        {
            _gruposRepositorio = gruposRepositorio;
        }
        public List<Grupo> ObtenerTodosGruposDeDocente(int id)
        {
           var data = _gruposRepositorio.ObtenerTodosGruposDeDocente(id);
            if (!data.Any()) throw new NoDataExcepcion($"No se pudo obtner los grupos del docente");
            return data;
        }
    }
}
