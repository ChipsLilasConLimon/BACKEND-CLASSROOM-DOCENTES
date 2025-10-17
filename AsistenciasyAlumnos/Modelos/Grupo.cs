namespace AsistenciasyAlumnos.Modelos
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdDocente { get; set; }
        public string Semestre { get; set; }
        public string Descripcion { get; set; }
    }
}
