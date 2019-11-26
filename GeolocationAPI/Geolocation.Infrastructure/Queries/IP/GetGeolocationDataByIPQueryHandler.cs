using Geolocation.Infrastructure.DTO;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Queries.IP
{
    public class GetGeolocationDataByIPQueryHandler : IRequestHandler<GetGeolocationDataByIPQuery, GeolocationResponseDTO>
    {
        public Task<GeolocationResponseDTO> Handle(GetGeolocationDataByIPQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
