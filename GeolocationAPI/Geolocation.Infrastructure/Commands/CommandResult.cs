using System;

namespace Geolocation.Infrastructure.Commands
{
    public sealed class CommandResult
    {
        public Guid Id { get;}
        public string Message { get;}
        public bool IsSuccess { get; set; }

        public CommandResult(Guid id, string message, bool isSuccess)
        {
            Id = id;
            Message = message;
            IsSuccess = isSuccess;
        }
    }
}
