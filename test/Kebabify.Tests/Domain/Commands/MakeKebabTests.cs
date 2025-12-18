using Kebabify.Web.Domain.Commands;
using Kebabify.Web.Domain.Services;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

namespace Kebabify.Tests.Domain.Commands
{
    public class MakeKebabTests
    {
        [Fact]
        public async Task Handle_Should_Throw_When_Request_Is_Null()
        {
            var sut = Testable.Create();

            var resut = await Assert.ThrowsAsync<ArgumentNullException>(() => sut.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_Should_Make_Kebab()
        {
            var sut = Testable.Create();

            var kebab = new KebabService.Result
            {
                Input = "x",
                Kebab = "x",
                Started = DateTime.Today,
                Completed = DateTime.Today
            };

            sut.Service.Setup(x => x.Make(It.IsAny<KebabService.Parameters>())).Returns(kebab);

            var result = await sut.Handle(new MakeKebab.Command(), CancellationToken.None);

            Assert.Equal(kebab.Input, result.Input);
            Assert.Equal(kebab.Kebab, result.Kebab);
            Assert.Equal(kebab.Started, result.Started);
            Assert.Equal(kebab.Completed, result.Completed);
        }

        public class Testable : MakeKebab
        {
            public Testable(Mock<KebabService> kebabService, ILogger<MakeKebab> logger) : base(kebabService.Object, logger)
            {
                this.Service = kebabService;
            }

            public Mock<KebabService> Service { get; }

            public static Testable Create()
            {
                return new Testable(new Mock<KebabService>(), new NullLogger<MakeKebab>());
            }
        }
    }
}
