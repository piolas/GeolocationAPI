using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Queries.IP
{
    public class GetGeolocationDataByIPQueryHandler : IRequestHandler<GetGeolocationDataByIPQuery, GeolocationResponseDTO>
    {
        private readonly ILogger<GetGeolocationDataByIPQueryHandler> _logger;
        private readonly IService _ipStackService;

        public GetGeolocationDataByIPQueryHandler(ILogger<GetGeolocationDataByIPQueryHandler> logger, IService ipStackService)
        {
            _logger = logger;
            _ipStackService = ipStackService;
        }

        public async Task<GeolocationResponseDTO> Handle(GetGeolocationDataByIPQuery request, CancellationToken cancellationToken)
        {
            return await _ipStackService.GetDataFromRemoteAPI(request.IPParameter);
        }
    }
}
