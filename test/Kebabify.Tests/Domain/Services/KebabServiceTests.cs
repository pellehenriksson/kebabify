using Kebabify.Web.Common;
using Kebabify.Web.Domain.Services;

using Microsoft.Extensions.Logging;

using Moq;

namespace Kebabify.Tests.Domain.Services
{
    public class KebabServiceTests
    {
        [Fact]
        public void Make_Should_Handle_Null_Message()
        {
            var service = Testable.Create();
            var result = service.Make(null);

            Assert.Equal(string.Empty, result.Kebab);
            Assert.Equal(string.Empty, result.Input);
        }

        [Fact]
        public void Make_Should_Handle_Empty_Message()
        {
            var service = Testable.Create();
            var result = service.Make(new KebabService.Parameters { Input = string.Empty });

            Assert.Equal(string.Empty, result.Kebab);
            Assert.Equal(string.Empty, result.Input);
        }

        [Fact]
        public void Make_Should_Handle_Spaces_Only_Message()
        {
            var service = Testable.Create();
            var result = service.Make(new KebabService.Parameters { Input = "  " });

            Assert.Equal(string.Empty, result.Kebab);
            Assert.Equal("  ", result.Input);
        }

        [Theory]
        [InlineData("Hej alla barn, jag heter Rulle!", "hej-alla-barn-jag-heter-rulle")]
        [InlineData("-", "")]
        [InlineData("   -    ", "")]
        [InlineData("A raised fist was used as a logo by the Industrial Workers of the World[3]", "a-raised-fist-was-used-as-a-logo-by-the-industrial-workers-of-the-world3")]
        [InlineData("djqwi 141 fewf uif23rm99 &/I(e 452675367", "djqwi-141-fewf-uif23rm99-ie-452675367")]
        public void Make_Should_Make_Kebab(string original, string kebab)
        {
            var service = Testable.Create();
            var result = service.Make(new KebabService.Parameters { Input = original });

            Assert.Equal(kebab, result.Kebab);
            Assert.Equal(original, result.Input);
        }

        private class Testable : KebabService
        {
            private Testable(Mock<SystemClock> clock, Mock<ILogger<KebabService>> logger)
                : base(clock.Object, logger.Object)
            {
                this.Clock = clock;
            }

            public Mock<SystemClock> Clock { get; }

            public static Testable Create()
            {
                return new Testable(new Mock<SystemClock>(), new Mock<ILogger<KebabService>>());
            }
        }
    }
}
