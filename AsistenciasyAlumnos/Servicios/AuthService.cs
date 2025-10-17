using AsistenciasyAlumnos.Jwt;
using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Repositorios;
using TextAPI_D311.Excepciones;
using TextAPI_D311.Modelos;
using BCrypt.Net;

namespace AsistenciasyAlumnos.Servicios
{
    public interface IAuthService
    {
        Usuario CrearUsuario(SignUp usuario);
        String ValidarLogin(Login login);
    }
    public class AuthService : IAuthService
    {
        private readonly IAuthRepositorio _authRepositorio;
        private readonly IJwtService _jwtService;
        public AuthService(IAuthRepositorio authRepositorio, IJwtService jwtService) 
        { 
            _authRepositorio = authRepositorio;
            _jwtService = jwtService;
        }

        public Usuario CrearUsuario(SignUp usuario)
        {
            bool x = false;
            if (usuario.Nombre == null || usuario.Nombre == "") x = true;
            if (usuario.NombreUsuario == null || usuario.NombreUsuario == "") x = true;
            if (usuario.PasswordUsuario == null || usuario.PasswordUsuario == "") x = true;
            if (usuario.ApellidoPaterno == null || usuario.ApellidoPaterno == "") x = true;
            if (usuario.ApellidoMaterno == null || usuario.ApellidoMaterno == "") x = true;
            if (x) throw new ValidacionExcepcion("Valores incompletos en la peticion");
            string passwordHasheada = BCrypt.Net.BCrypt.HashPassword(usuario.PasswordUsuario);
            usuario.PasswordUsuario = passwordHasheada;
            var registro =_authRepositorio.CrearUsuario(usuario);
            return registro;
        }
        public String ValidarLogin(Login login)
        {
            var usuario =_authRepositorio.ObtenerUsuarioPorNombre(login);
            if (usuario == null) throw new NoDataExcepcion("No se encontro al usuario");
            bool verificarHash = BCrypt.Net.BCrypt.Verify(login.password, usuario.PasswordUsuario);
            if (!verificarHash) throw new ValidacionExcepcion("Campos invalidos");
            var token = _jwtService.GenerateJwtToken(login, usuario.Rol, usuario.Id);
            return token;
        }
    }
}
