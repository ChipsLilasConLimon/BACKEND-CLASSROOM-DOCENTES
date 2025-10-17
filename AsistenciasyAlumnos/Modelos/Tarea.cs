namespace AsistenciasyAlumnos.Modelos
{
    public class Tarea
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string  FechaEntrega { get; set; }
        public string MaterialAdicional { get; set; }
        public int Puntuacion { get; set; }
        public int Id_Grupo { get; set; }
    }
}
