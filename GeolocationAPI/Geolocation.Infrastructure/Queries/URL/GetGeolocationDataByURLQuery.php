using Geolocation.Infrastructure.DTO;
using MediatR;

namespace Geolocation.Infrastructure.Queries.URL
{
    public class GetGeolocationDataByURLQuery : IRequest<GeolocationResponseDTO>
    {
        public string URLParameter { get; }
        public GetGeolocationDataByURLQuery(string URLParameter)
        {
            this.URLParameter = URLParameter;
        }
    }
}
