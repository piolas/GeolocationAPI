using FluentValidation.TestHelper;
using GeolocationAPI.Validation;
using NUnit.Framework;

namespace Geolocation.UnitTests.Validators
{
    [TestFixture]
    public class IPValidatorTester
    {
        private IPDataValidator validator;

        [SetUp]
        public void Setup()
        {
            validator = new IPDataValidator();
        }

        [Test]
        public void Should_have_error_when_IPParameter_is_null()
        {
            validator.ShouldHaveValidationErrorFor(ip => ip.IPParameter, null as string);
        }

        [Test]
        public void Should_have_error_when_IPParameter_is_empty()
        {
            validator.ShouldHaveValidationErrorFor(ip => ip.IPParameter, "");
        }

        [Test]
        public void Should_have_error_when_IPParameter_contains_letters()
        {
            validator.ShouldHaveValidationErrorFor(ip => ip.IPParameter, "a9.64.27.223");
        }
    }
}
