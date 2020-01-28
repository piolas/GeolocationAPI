using FluentValidation.TestHelper;
using GeolocationAPI.Validation;
using NUnit.Framework;

namespace Geolocation.UnitTests.Validators
{
    public class URLValidatorTester
    {
        private URLDataValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new URLDataValidator();
        }

        [Test]
        public void Should_have_error_when_URLParameter_is_null()
        {
            validator.ShouldHaveValidationErrorFor(url => url.URLParameter, null as string);
        }

        [Test]
        public void Should_have_error_when_URLParameter_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(url => url.URLParameter, "");
        }

        [Test]
        public void Should_have_error_when_URLParameter_is_without_HTTP()
        {
            validator.ShouldHaveValidationErrorFor(url => url.URLParameter, "www.onet.pl");
        }

        [Test]
        public void Should_not_have_error_when_URLParameter_is_without_WWW()
        {
            validator.ShouldNotHaveValidationErrorFor(url => url.URLParameter, "http://onet.pl");
        }
    }
}
