using AutoMapper;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Repositories;
using Geolocation.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands
{
    public class AddURLCommandHandler : IRequestHandler<AddURLCommand, CommandResult>
    {
        private readonly ILogger<AddURLCommandHandler> _logger;
        private readonly IService _ipStackService;
        private readonly IRepository<RootObject> _repository;
        private readonly IMapper _mapper;

        public AddURLCommandHandler(ILogger<AddURLCommandHandler> logger, IService ipStackService, IRepository<RootObject> repository, IMapper mapper)
        {
            _logger = logger;
            _ipStackService = ipStackService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(AddURLCommand request, CancellationToken cancellationToken)
        {
            var response = await _ipStackService.GetDataFromRemoteAPI(request.URLParameter);

            if (response is null)
            {
                _logger.LogInformation($"Adding geolocation request for parameter {request.URLParameter} via external service was unsuccessfull");
                return new CommandResult(null, "Adding new entry based on paramter skipped. Null response from external service", false);
            }
            else
            {
                var mappedObject = _mapper.Map<RootObject>(response);
                mappedObject.URLValue = request.URLParameter;

                _logger.LogInformation($"Adding geolocation request for parameter {request.URLParameter} was successfull");
                await _repository.Add(mappedObject);
                
                return new CommandResult(null, "Adding new entry based on paramter was successfull", true);
            }
        }
    }
}
