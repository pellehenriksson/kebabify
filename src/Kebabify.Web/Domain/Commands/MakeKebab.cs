using Kebabify.Web.Domain.Models;
using Kebabify.Web.Domain.Services;

using MediatR;

using System.ComponentModel.DataAnnotations;

namespace Kebabify.Web.Domain.Commands
{
    public class MakeKebab(KebabService kebabService, ILogger<MakeKebab> logger) : IRequestHandler<MakeKebab.Command, KebabModel>
    {
        private readonly KebabService kebabService = kebabService;

        private readonly ILogger<MakeKebab> logger = logger;

        public Task<KebabModel> Handle(Command command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);

            this.logger.LogDebug("Make kebab");
            var kebab = this.kebabService.Make(new KebabService.Parameters { Input = command.Input });

            var result = new KebabModel
            {
                Id = { Partition = "x", Key = "y" },
                Kebab = kebab.Kebab,
                Input = kebab.Input,
                Started = kebab.Started,
                Completed = kebab.Completed
            };

            this.logger.LogDebug("Return results");

            return Task.FromResult(result);
        }

        public class Command : IRequest<KebabModel>
        {
            [Required]
            public string Input { get; set; } 
        }
    }
}
