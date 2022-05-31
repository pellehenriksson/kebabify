using Kebabify.Domain.Services;
using Kebabify.Web.Domain.Commands;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

using Moq;

namespace Kebabify.Tests.Domain.Commands
{
    public class MakeKebabTests
    {


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
