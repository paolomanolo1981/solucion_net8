using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;
using MinimalAPIPeliculas.Repositorios;

namespace MinimalAPIPeliculas.endpoints
{
    public static class ActoresEndpoints
    {
        public static RouteGroupBuilder MapActores(this RouteGroupBuilder group)
        {
            group.MapPost("/", Crear).DisableAntiforgery();
            return group;
        }

        private static async Task<Created<ActorDTO>> Crear([FromForm] CrearActorDTO crearActorDTO, IRepositorioActores repositorioActores, IOutputCacheStore outputCacheStore, IMapper mapper)
        { 

            var actor= mapper.Map<Actor>(crearActorDTO);
            var id= await repositorioActores.Crear(actor);
            await outputCacheStore.EvictByTagAsync("actores-get", default);
            var actorDto= mapper.Map<ActorDTO>(actor);
            return TypedResults.Created($"/actores/{id}",actorDto);

        }
    }
}
