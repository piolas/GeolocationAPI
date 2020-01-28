using MediatR;

namespace Geolocation.Infrastructure.Commands.URL
{
    public class DeleteURLCommand : IRequest<CommandResult>
    {
        public string URLParameter { get;}

        public DeleteURLCommand(string url)
        {
            URLParameter = url;
        }
    }
}
