namespace TextAPI_D311.Modelos
{
    public class Respuesta
    {
        public object datos { get; set; }
        public int total_informacion { get; set; }
        public string mensaje { get; set; }

        public Respuesta (object datos = null, int total_informacion = 0, string mensaje = "")
        {
            this.datos = datos;
            this.total_informacion = total_informacion;
            this.mensaje = mensaje;
        }
    }
}
