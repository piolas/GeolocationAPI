using Geolocation.Infrastructure.Services;
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
        private readonly IService _ipStackService;

        public AddIPCommandHandler(ILogger<AddIPCommandHandler> logger, IService ipStackService)
        {
            _logger = logger;
            _ipStackService = ipStackService;
        }

        public Task<CommandResult> Handle(AddIPCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
