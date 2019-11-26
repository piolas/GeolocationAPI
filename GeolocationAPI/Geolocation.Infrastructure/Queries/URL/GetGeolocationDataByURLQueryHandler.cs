using Geolocation.Infrastructure.DTO;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Queries.URL
{
    public class GetGeolocationDataByURLQueryHandler : IRequestHandler<GetGeolocationDataByURLQuery, GeolocationResponseDTO>
    {
        public Task<GeolocationResponseDTO> Handle(GetGeolocationDataByURLQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
