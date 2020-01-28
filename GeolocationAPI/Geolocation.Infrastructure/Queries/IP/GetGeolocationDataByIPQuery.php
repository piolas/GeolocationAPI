using Geolocation.Infrastructure.DTO;
using MediatR;

namespace Geolocation.Infrastructure.Queries.IP
{
    public class GetGeolocationDataByIPQuery : IRequest<GeolocationResponseDTO>
    {
        public string IPParameter { get; }
        public GetGeolocationDataByIPQuery(string IPParameter)
        {
            this.IPParameter = IPParameter;
        }
    }
}
