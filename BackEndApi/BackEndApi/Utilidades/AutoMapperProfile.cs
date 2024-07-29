using AutoMapper;
using BackEndApi.DTO;
using BackEndApi.Models;
using System.Globalization;

namespace BackEndApi.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            // configuracion de como convertir el modelo a DTO
            #region Genero
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            #endregion

            #region  Pelicula
            CreateMap<Pelicula, PeliculaDTO>()
            .ForMember(destino =>
            destino.NombreGenero,
            plc => plc.MapFrom(origen => origen.IdGeneroNavigation.Nombre));

            CreateMap<PeliculaDTO, Pelicula>()
                .ForMember(destino => destino.IdGeneroNavigation,
                plc => plc.Ignore());
            #endregion
        }
       

    }
}
