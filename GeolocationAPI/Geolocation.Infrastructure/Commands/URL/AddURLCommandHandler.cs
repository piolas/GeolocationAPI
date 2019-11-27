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

        public AddURLCommandHandler(ILogger<AddURLCommandHandler> logger, IService ipStackService)
        {
            _logger = logger;
            _ipStackService = ipStackService;
        }

        public async Task<CommandResult> Handle(AddURLCommand request, CancellationToken cancellationToken)
        {
            var response = await _ipStackService.GetDataFromRemoteAPI(request.URLParameter);

            if (response is null)
            {
                _logger.LogInformation($"Adding geolocation request for parameter {request.URLParameter} was unsuccessfull");
                return new CommandResult(null, "Adding new entry based on paramter was unsuccessfull", false);
            }
            else
            {
                _logger.LogInformation($"Adding geolocation request for parameter {request.URLParameter} was successfull");
                return new CommandResult(null, "Adding new entry based on paramter was successfull", true);
            }
        }
    }
}
