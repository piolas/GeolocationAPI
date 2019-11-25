using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands.IP
{
    public class AddIPCommandHandler : IRequestHandler<AddIPCommand, CommandResult>
    {
        private readonly ILogger<AddIPCommandHandler> _logger;

        public AddIPCommandHandler()
        {

        }

        public Task<CommandResult> Handle(AddIPCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
