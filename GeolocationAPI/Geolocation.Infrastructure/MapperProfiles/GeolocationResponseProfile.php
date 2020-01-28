using AutoMapper;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.DTO;

namespace Geolocation.Infrastructure.MapperProfiles
{
    public class GeolocationResponseProfile : Profile
    {
        public GeolocationResponseProfile()
        {
            CreateMap<GeolocationResponseDTO, RootObject>().ForMember(dest => dest.location, opt => opt.MapFrom(src => src.location))
                                                           .ForMember(dest => dest.time_zone, opt => opt.MapFrom(src => src.time_zone))
                                                           .ForMember(dest => dest.currency, opt => opt.MapFrom(src => src.currency))
                                                           .ForMember(dest => dest.connection, opt => opt.MapFrom(src => src.connection));

            CreateMap<RootObject, GeolocationResponseDTO>().ForMember(dest => dest.location, opt => opt.MapFrom(src => src.location))
                                                           .ForMember(dest => dest.time_zone, opt => opt.MapFrom(src => src.time_zone))
                                                           .ForMember(dest => dest.currency, opt => opt.MapFrom(src => src.currency))
                                                           .ForMember(dest => dest.connection, opt => opt.MapFrom(src => src.connection));

            CreateMap<Domain.Domain.Location, DTO.Location>().ReverseMap();
            CreateMap<Domain.Domain.Connection, DTO.Connection>().ReverseMap();
            CreateMap<Domain.Domain.TimeZone, DTO.TimeZone>().ReverseMap();
            CreateMap<Domain.Domain.Currency, DTO.Currency>().ReverseMap();
            CreateMap<Domain.Domain.Language, DTO.Language>().ReverseMap();

        }
    }
}
