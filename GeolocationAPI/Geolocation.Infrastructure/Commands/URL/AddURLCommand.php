using MediatR;

namespace Geolocation.Infrastructure.Commands
{
    public class AddURLCommand : IRequest<CommandResult>
    {
        public string URLParameter { get; set; }
    }
}
