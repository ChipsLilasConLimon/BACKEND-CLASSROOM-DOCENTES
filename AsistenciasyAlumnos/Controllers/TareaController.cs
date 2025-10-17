using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        private readonly ITareaServicio _tareaServicio; 
        
        public TareaController(ITareaServicio tareaServicio) 
        {
            _tareaServicio = tareaServicio;
        }
        // ENDPOINTS PARA OBTENER
        [HttpGet]
        [Authorize(Roles = "DOCENTE,ADMIN")] // OBTENER TODAS LAS TAREAS DEJADAS EN EL GRUPO
        public IActionResult ObtenerTodas(int id) // ID DEL GRUPO
        {
            var data = _tareaServicio.ObtenerTodas(id);

            var respuesta = new Respuesta(
                datos: data,
                total_informacion: data.Count,
                mensaje: $"Se ecnontraron {data.Count} de tareas"
                );
            return Ok(respuesta);
        }

        [HttpGet("id")]
        [Authorize(Roles = "DOCENTE,ADMIN")] //OBTENER TAREA ESPECIFICA DEL GRUPO
        public IActionResult ObtenerTareaId(int id) // ID DE LA TAREA
        {
            var tarea = _tareaServicio.ObtenerPorId(id);
            if (tarea == null)
                return NotFound();
            var respuesta = new Respuesta(
                datos: tarea,
                total_informacion: 1,
                mensaje: "Tarea Encontrada"
                );
            return Ok(respuesta);
        }

        [HttpGet("tareasalumnos")]
        [Authorize(Roles = "DOCENTE,ADMIN")] //OBTENER LISTA DE TAREAS DE LOS ALUMNOS
        public IActionResult ObtenerTareasDeAlumnos(int id) // ID DE LA TAREA
        {
            var lista = _tareaServicio.ObtenerTareasAlumnos(id);
            if (lista == null)
                return NotFound();
            var respuesta = new Respuesta(
                datos: lista,
                total_informacion: lista.Count,
                mensaje: "Tareas Encontradas"
                );
            return Ok(respuesta);
        }
        // ENDPOINTS PARA ACTUALIZAR, AGREGAR
        [HttpPost]
        [Authorize(Roles = "DOCENTE,ADMIN")] // AGREGAR TAREA ESPCIFICA 
        public IActionResult AgregarTarea([FromBody] Tarea nuevaTarea) //TAREA NUEVA
        {
            if (string.IsNullOrEmpty(nuevaTarea.Titulo))
                return BadRequest();

            else if (string.IsNullOrEmpty(nuevaTarea.Descripcion))
                return BadRequest();

            _tareaServicio.Agregar(nuevaTarea);

            return Ok("Tarea agregada correctamente.");
        }

        [HttpPut("update")]
        [Authorize(Roles = "DOCENTE,ADMIN")] // ACTUALIZA UNA TAREA YA CREADA
        public IActionResult ActualizarTarea([FromBody] Tarea update)
        {
            if (string.IsNullOrEmpty(update.Titulo))
                return BadRequest();

            else if (string.IsNullOrEmpty(update.Descripcion))
                return BadRequest();
            if (_tareaServicio.ActualizarTarea(update))
            {
                return Ok("Tarea actualizada correctamente");
            } else {
                return NotFound();
            }
        }
        [HttpPut("calificar")] //CALIFICAR TAREA DEL ALUMNO
        [Authorize(Roles = "DOCENTE,ADMIN")]
        public IActionResult CalificarTareaDeAlumno([FromBody]CalificarTarea tarea) // CLASE QUE SOLO PIDE EL ID, CALIF Y COMENTARIOS
        {
            if (!_tareaServicio.ActualizarTareaAlumno(tarea)) return NotFound();
            return Ok("Tarea actualizada correctamente");
        }

        // ENDPOINTS PARA ELIMINAR
        [HttpDelete("delete")]
        [Authorize(Roles = "DOCENTE,ADMIN")] //BORRAR LA TAREA DEL GRUPO PERO SI ESTA VINCULADA O TIENE ENTREGAS LIGADAS NO SE PEUDE 
        public IActionResult ObtenerBorrar(int id) // ID DE LA TAREA
        {
            if (_tareaServicio.Eliminar(id))
                return Ok("Tarea eliminada correctamente");
            else
                return NotFound();
        }
    }
}
