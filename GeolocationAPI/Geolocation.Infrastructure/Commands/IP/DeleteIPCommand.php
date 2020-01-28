using MediatR;

namespace Geolocation.Infrastructure.Commands.IP
{
    public class DeleteIPCommand : IRequest<CommandResult>
    {
        public string IPParameter { get;}

        public DeleteIPCommand(string ip)
        {
            IPParameter = ip;
        }
    }
}
