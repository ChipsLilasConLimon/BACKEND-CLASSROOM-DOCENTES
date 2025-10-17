using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TextAPI_D311.Modelos;
using AsistenciasyAlumnos.Servicios;

namespace AsistenciasyAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : Controller
    {
        private readonly IGruposService _gruposService;
        public GruposController(IGruposService gruposService)
        {
            _gruposService = gruposService;

        }

        [HttpGet("gruposdocente")]
        [Authorize(Roles = "DOCENTE,ADMIN")]  //OBTENER TODOS LOS GRUPOS DEL DOCENTE
        public IActionResult ObtenerTodosLosGruposDocente()
        {
            var userId = User.FindFirst("userId")?.Value;
            int id = int.Parse(userId);
            var data = _gruposService.ObtenerTodosGruposDeDocente(id);
            var respuesta = new Respuesta(
            datos: data,
                total_informacion: data.Count,
                mensaje: $"Se obtuvieron todos los grupos del docente"
                );
            return Ok(respuesta);
        }

    }
}