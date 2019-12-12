using FluentValidation;
using Geolocation.Infrastructure.DTO;

namespace GeolocationAPI.Validation
{
    public sealed class URLDataValidator : AbstractValidator<URLDataDTO>
    {
        public URLDataValidator()
        {
            RuleFor(data => data.URLParameter)
                .NotEmpty()
                .NotNull()
                .Matches(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$")
                .WithMessage("Please provide valid URL");
        }
    }
}
