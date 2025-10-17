using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TextAPI_D311.Modelos;
using TextAPI_D311.Servicios;

namespace TextAPI_D311.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaServicio _asistenciaServicio;

        public AsistenciaController(IAsistenciaServicio asistenciaServicio)
        {
            _asistenciaServicio = asistenciaServicio;
        }

        [HttpPost("crearasistencia")]
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult RegistrarNuevaAsistencia(AsistenciaDiaria asistenciaDiaria)
        {
            var asistencia = _asistenciaServicio.CrearAsistenciaDiaria(asistenciaDiaria);

            var respuesta = new Respuesta(
                mensaje: "Se creo correctamnete la asistencia del dia",
                total_informacion: 1,
                datos: asistencia
                );
            return Ok( respuesta );

        }
        [HttpDelete("eliminar")]
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult EliminarAsistencia(int id)
        {
            var asistencia = _asistenciaServicio.EliminarAsistenciaDelDia(id);

            var respuesta = new Respuesta(
                mensaje: "Se elimino correctamente la asistencia",
                total_informacion: 1,
                datos: asistencia
                );
            return Ok(respuesta);

        }
        [HttpPost("guardarasistencia")]
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult RegistrarAsistenciaPorAlumno(AsistenciaAlumnos asistenciaAlumnos)
        {
            var asistencia = _asistenciaServicio.RegistrarAsistenciaAlumno(asistenciaAlumnos);

            var respuesta = new Respuesta(
                mensaje: "Se guardo la asistencia del alumno",
                total_informacion: 1,
                datos: asistencia
                );
            return Ok(respuesta);

        }
        [HttpGet("obtenerespecifico")]
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult ObtenerListaAsistencia(DateTime fecha, int id)
        {
            var lista = _asistenciaServicio.ObtenerListaAsistencia(fecha, id);
            var respuesta = new Respuesta(
                mensaje: "Asistencia obtenida correctamente",
                total_informacion: lista.Count,
                datos: lista
                );
            return Ok(respuesta);
        }
        [HttpGet("obtnertodas")]
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult ObtnerTodasLasAsistencias()
        {
            var lista = _asistenciaServicio.ObtenerTodasAsistencias();

            var respuesta = new Respuesta(
                mensaje: "Se obtuvieron todas las asistencias",
                total_informacion: lista.Count,
                datos: lista
                );
            return Ok(respuesta);
        }
        [HttpGet("obtenertodasprupo")]
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult ObtenerTodasAsistenciasPorGrupo(int id)
        {
            var lista = _asistenciaServicio.ObtenerTodasAsistenciasGrupo(id);

            var respuesta = new Respuesta(
                mensaje: $"Se obtuvieron todas las asistencias del grupo {id}",
                total_informacion: lista.Count,
                datos: lista
                );
            return Ok(respuesta);

        }

    }
}