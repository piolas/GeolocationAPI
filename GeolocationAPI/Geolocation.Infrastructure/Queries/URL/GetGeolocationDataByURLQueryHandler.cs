using AutoMapper;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.Repositories;
using Geolocation.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Queries.URL
{
    public class GetGeolocationDataByURLQueryHandler : IRequestHandler<GetGeolocationDataByURLQuery, GeolocationResponseDTO>
    {
        private readonly ILogger<GetGeolocationDataByURLQueryHandler> _logger;
        private readonly IRepository<RootObject> _repository;
        private readonly IService _ipStackService;
        private readonly IMapper _mapper;

        public GetGeolocationDataByURLQueryHandler(ILogger<GetGeolocationDataByURLQueryHandler> logger, IService ipStackService, IRepository<RootObject> repository, IMapper mapper)
        {
            _logger = logger;
            _ipStackService = ipStackService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GeolocationResponseDTO> Handle(GetGeolocationDataByURLQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Handling geolocation GET request for parameter {request.URLParameter}");

            _logger.LogInformation($"Attempting to retrieve data from DB for parameter {request.URLParameter}");

            var dbEntry = await _repository.GetByIP(request.URLParameter);

            if (dbEntry is null)
            {
                _logger.LogInformation($"Data was not found in DB. Sending request to IPStack service with parameter {request.URLParameter}");
                return await _ipStackService.GetDataFromRemoteAPI(request.URLParameter);
            }
            else
            {
                _logger.LogInformation($"Data was found in DB. Skipping request to IPStack service with parameter {request.URLParameter}");
                return _mapper.Map<GeolocationResponseDTO>(dbEntry);
            }
        }
    }
}
