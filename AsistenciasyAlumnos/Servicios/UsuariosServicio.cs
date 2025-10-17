using AsistenciasyAlumnos.Modelos;
using AsistenciasyAlumnos.Repositorios;
using TextAPI_D311.Excepciones;
using TextAPI_D311.Modelos;

namespace AsistenciasyAlumnos.Servicios
{
    public interface IUsuariosServicio
    {
        List<Usuario> ObtenerListaUsuario();
    }
    public class UsuariosServicio : IUsuariosServicio
    {
        private readonly IUsuariosRepositorio _usuariosRepositorio;
        public UsuariosServicio(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuariosRepositorio = usuariosRepositorio;

        }
        public List<Usuario> ObtenerListaUsuario()
        {
            var lista = _usuariosRepositorio.ObtenerListaUsuario();
            if (!lista.Any()) throw new NoDataExcepcion($"No se pudo obtner la lista de usuarios");
            /*
             var objeto = lista.SingleOrDefault(obj => obj.Id == id); SI QUEREMOS MOSTARAR A TODOS LOS USUARIOS MENOS AL AMDIN QUE INICIO SESION, FALTA PASAR EL ID POR EL METODO. SACANDO EL ID DEL TOKEN EN EL CONTROLLADOR
             if (objeto != null) lista.Remove(objeto); 
             */
            return lista;
        }
    }
}
