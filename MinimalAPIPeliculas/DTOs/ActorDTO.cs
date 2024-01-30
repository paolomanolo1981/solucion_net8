namespace MinimalAPIPeliculas.DTOs
{
    public class ActorDTO
    {
        public int Id { get; set; }
        public string nombre { get; set; } = null!;
        public DateTime fechaNacimiento { get; set; }
        public IFormFile? foto { get; set; }
    }
}
