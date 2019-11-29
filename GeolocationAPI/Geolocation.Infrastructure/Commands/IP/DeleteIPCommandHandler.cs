using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands.IP
{
    public class DeleteIPCommandHandler : IRequestHandler<DeleteIPCommand, CommandResult>
    {
        private readonly ILogger<DeleteIPCommandHandler> _logger;
        private readonly IRepository<RootObject> _repository;

        public DeleteIPCommandHandler(ILogger<DeleteIPCommandHandler> logger, IRepository<RootObject> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<CommandResult> Handle(DeleteIPCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting geolocation data for parameter {request.IPParameter}");

            var entry = await _repository.GetByIP(request.IPParameter);

            if (entry is null)
            {
                _logger.LogInformation($"Could not find data for parameter {request.IPParameter}. Deletion failed");
                return new CommandResult(null, "Entry could not be found in DB. Deletion failed", false);
            }
            else
            {
                await _repository.Remove(entry.ip);
                _logger.LogInformation($"Data for parameter {request.IPParameter} was found in DB. Deletion completed");
                return new CommandResult(entry.Id, "Entry found in DB. Deletion completed", true);
            }
        }
    }
}
