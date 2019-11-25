using System;

namespace Geolocation.Infrastructure.Commands
{
    public sealed class CommandResult
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }

        public CommandResult(Guid id, string message)
        {
            Id = id;
            Message = message;
        }
    }
}
