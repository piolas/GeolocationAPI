using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands.IP
{
    public class DeleteIPCommandHandler : IRequestHandler<DeleteIPCommand, CommandResult>
    {
        private readonly ILogger<DeleteIPCommandHandler> _logger;

        public DeleteIPCommandHandler()
        {

        }

        public Task<CommandResult> Handle(DeleteIPCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
