using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Repositorios
{
    public interface IRepositorioGeneros
    {
        Task<int> CrearGenero(Genero genero);
        Task<List<Genero>> ObtenerTodos();
        Task<Genero?> ObtenerXId(int id);
        Task<bool> Existe(int id);
        Task Actualizar(Genero genero); 
        Task Borrar(int id);

    }
}