//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Threading;
//using System.Threading.Tasks;
//using Kebabify.Domain.Models;
//using Kebabify.Domain.Services;
//using MediatR;
//using Microsoft.CodeAnalysis.Operations;
//using Microsoft.Extensions.Logging;

//namespace Kebabify.Domain.Queries
//{
//    public class KebabById : IRequestHandler<KebabById.Parameters, KebabModel>
//    {
//        private readonly KebabHistoryService historyService;

//        private readonly ILogger<KebabById> logger;

//        public KebabById(KebabHistoryService historyService, ILogger<KebabById> logger)
//        {
//            this.historyService = historyService;
//            this.logger = logger;
//        }

//        [SuppressMessage("Microsoft.Naming", "CA1725", Justification = "This is fine")]
//        public async Task<KebabModel> Handle(Parameters parameters, CancellationToken cancellationToken)
//        {
//            if (parameters == null)
//            {
//                throw new ArgumentNullException(nameof(parameters));
//            }

//            var result = await this.historyService.GetItem(parameters.Partition, parameters.Key);
//            return result;
//        }

//        public class Parameters : IRequest<KebabModel>
//        {
//            public string Partition { get; set; }

//            public string Key { get; set; }
//        }
//    }
//}
