namespace MinimalAPIPeliculas.DTOs
{
    public class CrearActorDTO
    {
         
        public string nombre { get; set; } = null!;
        public DateTime fechaNacimiento { get; set; }
        public IFormFile? foto { get; set; }

    }
}
