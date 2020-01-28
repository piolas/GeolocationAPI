using FluentValidation;
using Geolocation.Infrastructure.DTO;

namespace GeolocationAPI.Validation
{
    public sealed class IPDataValidator : AbstractValidator<IPDataDTO>
    {
        public IPDataValidator()
        {
            RuleFor(data => data.IPParameter)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$")
                .WithMessage("Please provide valid IP address");
        }
    }
}
