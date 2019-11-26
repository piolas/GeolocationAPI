using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Geolocation.Infrastructure.Commands.URL
{
    public class DeleteURLCommandHandler : IRequestHandler<DeleteURLCommand, CommandResult>
    {
        public Task<CommandResult> Handle(DeleteURLCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
