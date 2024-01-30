using AutoMapper;
using MinimalAPIPeliculas.DTOs;
using MinimalAPIPeliculas.Entidades;

namespace MinimalAPIPeliculas.Utilidades
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CrearGeneroDTO, Genero>();
            CreateMap<GeneroDTO, Genero>().ReverseMap();// ().ReverseMap();

            //para actores
            /* CreateMap<CrearActorDTO, Actor>()
                 .ForMember(x=> x.foto, opciones=>opciones.Ignore());
             CreateMap<Actor, ActorDTO>().ReverseMap();*/

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<CrearActorDTO, Actor>()
                    .ForMember(x => x.foto, opciones => opciones.Ignore());


        }
    }
}
