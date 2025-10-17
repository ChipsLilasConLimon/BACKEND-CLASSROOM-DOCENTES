namespace AsistenciasyAlumnos.Modelos
{
    public class Comprobaciones
    {
        public bool PropiedadesVacias(object obj)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                var value = prop.GetValue(obj);
                if (value == null) return true;
                if (value is string s && string.IsNullOrWhiteSpace(s)) return true;
            }
            return false;
        }
    }
}
