using AutoMapper;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Repositories;
using Geolocation.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands.IP
{
    public class AddIPCommandHandler : IRequestHandler<AddIPCommand, CommandResult>
    {
        private readonly ILogger<AddIPCommandHandler> _logger;
        private readonly IService _ipStackService;
        private readonly IRepository<RootObject> _repository;
        private readonly IMapper _mapper;

        public AddIPCommandHandler(ILogger<AddIPCommandHandler> logger, IService ipStackService, IRepository<RootObject> repository, IMapper mapper)
        {
            _logger = logger;
            _ipStackService = ipStackService;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CommandResult> Handle(AddIPCommand request, CancellationToken cancellationToken)
        {
            var response = await _ipStackService.GetDataFromRemoteAPI(request.IPParameter);

            if (response is null)
            {
                _logger.LogInformation($"Adding geolocation request for parameter {request.IPParameter} was unsuccessfull");
                return new CommandResult(null, "Adding new entry based on paramter was unsuccessfull", false);
            }
            else
            {
                var mappedObject = _mapper.Map<RootObject>(response);

                _logger.LogInformation($"Adding geolocation request for parameter {request.IPParameter} was successfull");
                await _repository.Add(mappedObject);
                
                return new CommandResult(null, "Adding new entry based on paramter was successfull", true);
            }
        }
    }
}
