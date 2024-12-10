using NUnit.Framework;
using STAREvents.Services;

namespace STAREvents.Services.Tests
{
    [TestFixture]
    public class BaseServiceTests
    {
        private BaseService _baseService;

        [SetUp]
        public void SetUp()
        {
            _baseService = new BaseService();
        }

        [Test]
        public void IsGuidValid_ShouldReturnTrue_WhenValidGuidStringIsProvided()
        {
            var input = Guid.NewGuid().ToString();
            var parsedGuid = Guid.Empty;

            var result = _baseService.IsGuidValid(input, ref parsedGuid);

            Assert.That(result, Is.True);
            Assert.That(parsedGuid.ToString(), Is.EqualTo(input));
        }

        [Test]
        public void IsGuidValid_ShouldReturnFalse_WhenInvalidGuidStringIsProvided()
        {
            var input = "invalid-guid";
            var parsedGuid = Guid.Empty;

            var result = _baseService.IsGuidValid(input, ref parsedGuid);

            Assert.That(result, Is.False);
            Assert.That(parsedGuid, Is.EqualTo(Guid.Empty));
        }

        [Test]
        public void IsGuidValid_ShouldReturnFalse_WhenInputIsNullOrEmpty()
        {
            var parsedGuid = Guid.Empty;

            var resultWithNull = _baseService.IsGuidValid(null, ref parsedGuid);
            Assert.That(resultWithNull, Is.False);
            Assert.That(parsedGuid, Is.EqualTo(Guid.Empty));

            var resultWithEmpty = _baseService.IsGuidValid(string.Empty, ref parsedGuid);
            Assert.That(resultWithEmpty, Is.False);
            Assert.That(parsedGuid, Is.EqualTo(Guid.Empty));
        }
    }

}
