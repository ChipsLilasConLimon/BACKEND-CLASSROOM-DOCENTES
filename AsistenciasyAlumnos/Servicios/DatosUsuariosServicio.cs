using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Repositorios;
using TextAPI_D311.Excepciones;

namespace AsistenciasyAlumnos.Servicios
{
    public interface IDatosUsuariosServicio
    {
        DatosUsuario ObtenerTodosLosDatosDeUsuario(int Id);
        bool ActualizarFotoDePeril(string url, int Id);
        bool ActualizarPassword(string password, int Id);
        bool ActualizarUsername(string username, int id);
    }
    public class DatosUsuariosServicio : IDatosUsuariosServicio
    {
        private readonly IDatosUsuariosRepositorio _datosUsuariosRepositorio;

        public DatosUsuariosServicio(IDatosUsuariosRepositorio datosUsuariosRepositorio)
        {
            _datosUsuariosRepositorio = datosUsuariosRepositorio;
        }

        public bool ActualizarFotoDePeril(string url, int Id)
        {
            var validar = _datosUsuariosRepositorio.ActualizarFotoDePeril(url, Id);
            if (!validar) throw new NoDataExcepcion($"No se actualizo la foto de perfil");
            return validar;
        }

        public bool ActualizarPassword(string password, int Id)
        {
            string passwordHasheada = BCrypt.Net.BCrypt.HashPassword(password);
            var validar = _datosUsuariosRepositorio.ActualizarPassword(passwordHasheada, Id);
            if (!validar) throw new NoDataExcepcion($"No se actualizo la contraseña");
            return validar;
        }

        public bool ActualizarUsername(string username, int id)
        {
            var validar = _datosUsuariosRepositorio.ActualizarUsername(username, id);
            if (!validar) throw new NoDataExcepcion($"No se actualizo el nombre de usuario");
            return validar;
        }

        public DatosUsuario ObtenerTodosLosDatosDeUsuario(int Id)
        {
            var data = _datosUsuariosRepositorio.ObtenerTodosLosDatosDeUsuario(Id);
            if (data == null) throw new NoDataExcepcion($"No se encontro al usuario");
            return data;
        }
    }
}
