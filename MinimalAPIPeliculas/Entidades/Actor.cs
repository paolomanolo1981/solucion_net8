namespace MinimalAPIPeliculas.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        public string nombre { get; set; } = null!;
        public DateTime fechaNacimiento { get; set; }
        public string? foto { get; set; }
    }
}
