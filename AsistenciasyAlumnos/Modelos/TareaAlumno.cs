namespace AsistenciasyAlumnos.Modelos
{
    public class TareaAlumno
    {
        public int Id { get; set; }
        public int IdTarea { get; set; }
        public string Estatus { get; set; }
        public string MaterialAdjunto { get; set; }
        public string Nombre { get; set; }
        public string ApellidosPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
}
