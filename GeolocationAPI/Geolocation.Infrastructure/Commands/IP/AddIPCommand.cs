using MediatR;

namespace Geolocation.Infrastructure.Commands.IP
{
    public class AddIPCommand : IRequest<CommandResult>
    {
        public string IPParameter { get; set; }
    }
}
