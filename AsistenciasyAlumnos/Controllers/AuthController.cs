using AsistenciasyAlumnos.Jwt;
using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Servicios;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] Login dto)
        {
            int duracion = 60;
            var token = _authService.ValidarLogin(dto);
            DateTime fechaExpi = DateTime.Now.AddMinutes(duracion);
            var response = new AuthRespuesta(
                token: token,
                mensaje: "Inicio de sesion exitoso",
                fechaExpiracion: fechaExpi,
                Duracion: duracion
                );
            return Ok(response);
        }
        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUp dto)
        {
           var registro = _authService.CrearUsuario(dto);
            var respuesta = new Respuesta(
                 mensaje: "Tu solicitud ha sido enviada, espera una respuesta de los administradores",
                 total_informacion: 1,
                 datos: registro
                 );
            return Ok(respuesta);
        }
    }
}
