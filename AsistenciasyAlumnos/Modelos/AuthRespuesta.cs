namespace AsistenciasyAlumnos.Modelos
{
    public class AuthRespuesta
    {
        public object token { get; set; }
        public DateTime fechaExpiracion { get; set; }
        public int Duracion { get; set; }
        public string mensaje { get; set; }

        public AuthRespuesta(object token = null, DateTime? fechaExpiracion = null , int Duracion = 0, string mensaje = "")
        {
            this.token = token;
            this.Duracion = Duracion;
            this.mensaje = mensaje;
            this.fechaExpiracion = fechaExpiracion ?? new DateTime(2000, 1, 1);
        }
    }
}
