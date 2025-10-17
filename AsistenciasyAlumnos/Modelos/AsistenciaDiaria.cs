namespace TextAPI_D311.Modelos
{
    public class AsistenciaDiaria
    {
        public int Id { get; set; }
        public int IdMaestro { get; set; }
        public int IdGrupo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
