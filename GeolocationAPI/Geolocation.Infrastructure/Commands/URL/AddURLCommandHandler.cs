using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands
{
    public class AddURLCommandHandler : IRequestHandler<AddURLCommand, CommandResult>
    {
        public Task<CommandResult> Handle(AddURLCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
