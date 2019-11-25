using AutoMapper;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.DTO;

namespace Geolocation.Infrastructure.MapperProfiles
{
    public class GeolocationResponseProfile : Profile
    {
        public GeolocationResponseProfile()
        {
            CreateMap<GeolocationResponseDTO, RootObject>().ReverseMap();
        }
    }
}
