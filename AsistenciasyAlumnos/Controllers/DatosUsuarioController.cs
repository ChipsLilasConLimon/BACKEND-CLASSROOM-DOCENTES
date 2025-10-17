using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TextAPI_D311.Modelos;
using AsistenciasyAlumnos.Servicios;

namespace AsistenciasyAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosUsuarioController : Controller
    {
        private readonly IDatosUsuariosServicio _datosUsuarios;
        public DatosUsuarioController(IDatosUsuariosServicio datosUsuarios)
        {
            _datosUsuarios = datosUsuarios;
        }

        [HttpGet]
        [Authorize(Roles = "DOCENTE,ADMIN")]  // VER TODOS LOS DATOS DEL DOCENTE
        public IActionResult ObtenerTodosDatosDeUsuario()
        {
            var userId = User.FindFirst("userId")?.Value;
            int id = int.Parse(userId);
            var datos = _datosUsuarios.ObtenerTodosLosDatosDeUsuario(id);
            var respuesta = new Respuesta(
            datos: datos,
                total_informacion: 0,
                mensaje: $"Exito en obtener todos los datos del usuario"
                );
            return Ok(respuesta);
        }
        [HttpPost("imgperfil")]
        [Authorize(Roles = "DOCENTE,ADMIN")]  // EL DOCENTE PEUDE CAMBIAR SU FOTO DE PERFIL
        public IActionResult ActualizarFotoDePeril(string url)
        {
            var userId = User.FindFirst("userId")?.Value;
            int id = int.Parse(userId);
            _datosUsuarios.ActualizarFotoDePeril(url, id);
            return Ok("Se actualizo correctamente la foto de perfil");
        }

        [HttpPost("password")]
        [Authorize(Roles = "DOCENTE,ADMIN")] // EL DOCENTE PEUDE CAMBIAR SU PASSWORD
        public IActionResult ActualizarPassword(string password)
        {
            var userId = User.FindFirst("userId")?.Value;
            int id = int.Parse(userId);
            _datosUsuarios.ActualizarPassword(password, id);
            return Ok("Se actualizo correctamente el password");
        }

        [HttpPost("username")]
        [Authorize(Roles = "ADMIN")] // SOLO EL ADMIN PEUDE CAMBIAR EL USERNAME A LOS USUARIOS
        public IActionResult ActualizarUsername(int id, string username)
        {
            _datosUsuarios.ActualizarUsername(username, id);
            return Ok("Se actualizo correctamente el username");
        }
    }
}
