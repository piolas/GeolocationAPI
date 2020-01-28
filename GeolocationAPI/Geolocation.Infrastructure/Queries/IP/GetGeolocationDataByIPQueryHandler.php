using AutoMapper;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.Repositories;
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
        private readonly IRepository<RootObject> _repository;
        private readonly IService _ipStackService;
        private readonly IMapper _mapper;

        public GetGeolocationDataByIPQueryHandler(ILogger<GetGeolocationDataByIPQueryHandler> logger, IService ipStackService, IRepository<RootObject> repository, IMapper mapper)
        {
            _logger = logger;
            _ipStackService = ipStackService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeolocationResponseDTO> Handle(GetGeolocationDataByIPQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling geolocation GET request for parameter {request.IPParameter}");

            _logger.LogInformation($"Attempting to retrieve data from DB for parameter {request.IPParameter}");

            var dbEntry = await _repository.GetByIP(request.IPParameter);            

            if (dbEntry is null)
            {
                _logger.LogInformation($"Data was not found in DB. Sending request to IPStack service with parameter {request.IPParameter}");
                return await _ipStackService.GetDataFromRemoteAPI(request.IPParameter);
            }
            else
            {
                _logger.LogInformation($"Data was found in DB. Skipping request to IPStack service with parameter {request.IPParameter}");
                return _mapper.Map<GeolocationResponseDTO>(dbEntry);
            }
        }
    }
}
