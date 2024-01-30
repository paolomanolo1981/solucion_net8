using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;

namespace MinimalAPIPeliculas.endpoints
{
    public static class GeneroEndpoints
    {
        public static RouteGroupBuilder MapGeneros(this RouteGroupBuilder group)
        {
            group.MapGet("/", ObtenerGeneros).CacheOutput(c =>
            {
                c.Expire(TimeSpan.FromSeconds(60)).Tag("generos-get");
            });
            group.MapGet("/{id:int}", ObtenerGeneroXID);
            group.MapPost("/", CrearGenero);
            group.MapPut("/{id:int}", ActualizarGenero);
            group.MapDelete("/{id:int}", BorrarGenero);


            return group;
        }

        static async Task<Results<Ok<List<GeneroDTO>>, NotFound>> ObtenerGeneros(IRepositorioGeneros repositorioGeneros, IMapper mapper)
        {
            try
            {
                var generos = await repositorioGeneros.ObtenerTodos();
                //   var generosDTO = mapper.Map<List<GeneroDTO>>(generos);// generos.Select(x => new GeneroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
                var generosDTO = mapper.Map<List<GeneroDTO>>(generos);//   var generosDTO = mapper.Map<List<GeneroDTO>>(generos);
                return TypedResults.Ok(generosDTO);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return TypedResults.NotFound();
            }

        }

        static async Task<Results<NotFound, Ok<GeneroDTO>>> ObtenerGeneroXID(int id, IRepositorioGeneros repositorioGeneros, IMapper mapper)
        {
            var genero = await repositorioGeneros.ObtenerXId(id);
            if (genero is null)
            {
                return TypedResults.NotFound();
            }
            else
            {
               /* var generoDTO = new GeneroDTO
                {
                    Id = id,
                    Nombre = genero.Nombre
                };*/

                var generoDTO = mapper.Map<GeneroDTO>(genero);
                return TypedResults.Ok(generoDTO);
            }
        }

        static async Task<Created<GeneroDTO>> CrearGenero(CrearGeneroDTO crearGeneroDTO, IRepositorioGeneros repositorioGeneros, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            /* var genero = new Genero
             {

                 Nombre = crearGeneroDTO.Nombre
             };*/
            

            /*var generoDTO = new GeneroDTO
            {
                Id = id,
                Nombre = genero.Nombre
            };*/
            
            try
            {
                var genero = mapper.Map<Genero>(crearGeneroDTO);
                var id = await repositorioGeneros.CrearGenero(genero);
                await outputCacheStore.EvictByTagAsync("generos-get", default);
                var generoDTO = mapper.Map<GeneroDTO>(genero);
                return TypedResults.Created($"/generos/{genero.Id}", generoDTO);
            }
            catch(Exception ex)
            {
                ex.ToString();
                var genero = mapper.Map<Genero>(crearGeneroDTO);
                var generoDTO = mapper.Map<GeneroDTO>(genero);
                return TypedResults.Created($"/generos/", generoDTO);
            }
            

            
        }

        static async Task<Results<NotFound, NoContent>> ActualizarGenero(int id, CrearGeneroDTO crearGeneroDTO, IRepositorioGeneros repositorioGeneros, IOutputCacheStore outputCacheStore, IMapper mapper)
        {
            var existe = await repositorioGeneros.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }
            /*var genero = new Genero
            {
                Id = id,
                Nombre = crearGeneroDTO.Nombre
            };*/
            var genero=mapper.Map<Genero>(crearGeneroDTO);
            genero.Id= id;

            await repositorioGeneros.Actualizar(genero);
            await outputCacheStore.EvictByTagAsync("generos-get", default);
            return TypedResults.NoContent();

        }

        static async Task<Results<NotFound, NoContent>> BorrarGenero(int id, IRepositorioGeneros repositorioGeneros, IOutputCacheStore outputCacheStore)
        {
            var existe = await repositorioGeneros.Existe(id);
            if (!existe)
            {
                return TypedResults.NotFound();
            }

            await repositorioGeneros.Borrar(id);
            await outputCacheStore.EvictByTagAsync("generos-get", default);
            return TypedResults.NoContent();
        }

        /*
static async Task<IResult> ObtenerGeneros2(IRepositorioGeneros repositorioGeneros)
{


    var generos = await repositorioGeneros.ObtenerTodos();
    return Results.Ok(generos);


}*/

    }
}
