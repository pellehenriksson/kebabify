using Kebabify.Domain.Services;
using Kebabify.Web.Domain.Models;

using MediatR;

using System.ComponentModel.DataAnnotations;

namespace Kebabify.Web.Domain.Commands
{
    public class MakeKebab : IRequestHandler<MakeKebab.Command, KebabModel>
    {
        private readonly KebabService kebabService;

        private readonly ILogger<MakeKebab> logger;

        public MakeKebab(KebabService kebabService, ILogger<MakeKebab> logger)
        {
            this.kebabService = kebabService;
            this.logger = logger;
        }

        public Task<KebabModel> Handle(Command command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

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
