using AsistenciasyAlumnos.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosServicio _usuariosServicio;
        public UsuariosController(IUsuariosServicio usuariosServicio)
        {
            _usuariosServicio = usuariosServicio;
        }

        [HttpGet("todos")]
        [Authorize(Roles = "ADMIN")] // SOLO EL ADMIN PEUDE VER TODOS LOS USUARIOS
        public IActionResult ObtenerTodosLosUsuarios()
        {
            var data = _usuariosServicio.ObtenerListaUsuario();

            var respuesta = new Respuesta(
                mensaje: $"Se obtuvieron a todos los usuarios",
                total_informacion: data.Count,
                datos: data
                );
            return Ok(respuesta);
        }
    }
}
