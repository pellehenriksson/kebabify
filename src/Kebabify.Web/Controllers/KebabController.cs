using Kebabify.Web.Domain.Commands;
using Kebabify.Web.Domain.Models;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Kebabify.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KebabController : ControllerBase
    {
        private readonly IMediator mediator;

        private readonly ILogger<KebabController> logger;

        public KebabController(IMediator mediator, ILogger<KebabController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public ActionResult<string> Get()
        {
            return this.Ok("Kebabify");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KebabModel))]
        public async Task<ActionResult<KebabModel>> Post([FromBody] MakeKebab.Command command)
        {
            var result = await this.mediator.Send(command);
            return this.Ok(result); 
        }
    }
}