using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands.URL
{
    public class DeleteURLCommandHandler : IRequestHandler<DeleteURLCommand, CommandResult>
    {
        private readonly ILogger<DeleteURLCommandHandler> _logger;
        private readonly IRepository<RootObject> _repository;

        public DeleteURLCommandHandler(ILogger<DeleteURLCommandHandler> logger, IRepository<RootObject> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<CommandResult> Handle(DeleteURLCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Deleting geolocation data for parameter {request.URLParameter}");

            var entry = await _repository.GetByIP(request.URLParameter);

            if (entry is null)
            {
                _logger.LogInformation($"Could not find data for parameter {request.URLParameter}. Deletion failed");
                return new CommandResult(null, "Entry could not be found in DB. Deletion failed", false);
            }
            else
            {
                await _repository.Remove(entry.ip);
                _logger.LogInformation($"Data for parameter {request.URLParameter} was found in DB. Deletion completed");
                return new CommandResult(entry.Id, "Entry found in DB. Deletion completed", true);
            }
        }
    }
}
