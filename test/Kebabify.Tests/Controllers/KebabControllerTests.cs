using Kebabify.Web.Controllers;
using Kebabify.Web.Domain.Commands;
using Kebabify.Web.Domain.Models;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

namespace Kebabify.Tests.Controllers
{
    public class KebabControllerTests
    {
        [Fact]
        public void Get_Should_Return_Response()
        {
            var controller = Testable.Create();

            var response = controller.Get();
            var result = (OkObjectResult)response.Result;
            var data = (string)result.Value;

            Assert.Equal("Kebabify", data);
        }

        [Fact]
        public async Task Post_With_Message_Should_Return_Response()
        {
            var controller = Testable.Create();
            controller.Mediator.Setup(x => x.Send(It.IsAny<MakeKebab.Command>(), CancellationToken.None)).ReturnsAsync(new KebabModel { Input = "x y", Kebab = "x-y" });

            var response = await controller.Post(new MakeKebab.Command { Input = "x y" });
            var result = (OkObjectResult)response.Result;
            var data = (KebabModel)result.Value;

            Assert.Equal("x y", data.Input);
            Assert.Equal("x-y", data.Kebab);
        }

        private class Testable : KebabController
        {
            private Testable(Mock<IMediator> mediator, Mock<ILogger<KebabController>> logger)
                : base(mediator.Object, logger.Object)
            {
                this.Mediator = mediator;
            }

            public Mock<IMediator> Mediator { get; }

            public static Testable Create()
            {
                return new Testable(new Mock<IMediator>(), new Mock<ILogger<KebabController>>());
            }
        }
    }
}
