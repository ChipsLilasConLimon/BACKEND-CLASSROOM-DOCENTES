namespace TextAPI_D311.Excepciones
{
    public class NoDataExcepcion : Exception
    {
        public NoDataExcepcion(string mensaje) : base(mensaje) { }
    }
}
