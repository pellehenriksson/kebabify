//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Globalization;
//using System.Threading.Tasks;
//using Kebabify.Common;
//using Kebabify.Domain.Models;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using Microsoft.WindowsAzure.Storage;
//using Microsoft.WindowsAzure.Storage.Auth;
//using Microsoft.WindowsAzure.Storage.Table;

//namespace Kebabify.Domain.Services
//{
//    public class KebabHistoryService
//    {
//        private readonly ServiceOptions options;

//        private readonly ILogger<KebabHistoryService> logger;

//        public KebabHistoryService(IOptionsMonitor<ServiceOptions> options, ILogger<KebabHistoryService> logger)
//        {
//            this.logger = logger;
//            this.options = options?.CurrentValue;
//        }

//        protected KebabHistoryService()
//        {
//        }

//        public virtual async Task<(string partition, string key)> AddItem(KebabService.Result result)
//        {
//            if (result == null)
//            {
//                throw new ArgumentNullException(nameof(result));
//            }

//            try
//            {
//                var table = await this.ResolveTable();

//                var item = new KebabEntity
//                {
//                    Input = result.Input,
//                    Kebab = result.Kebab,
//                    Started = result.Started,
//                    Completed = result.Completed
//                };

//                var insertOperation = TableOperation.Insert(item);

//                this.logger.LogDebug("Save new item");
//                await table.ExecuteAsync(insertOperation);

//                return (item.PartitionKey, item.RowKey);
//            }
//            catch (Exception ex)
//            {
//                this.logger.LogError(ex, "Failed to store kebab in history.");
//                throw new KebabifyException("Failed to store kebab in history.");
//            }
//        }

//        public virtual async Task<KebabModel> GetItem(string partition, string key)
//        {
//            try
//            {
//                var table = await this.ResolveTable();

//                var fetchOperation = TableOperation.Retrieve<KebabEntity>(partition, key);
//                var results = await table.ExecuteAsync(fetchOperation);
//                var kebab = results.Result as KebabEntity;

//                var result = new KebabModel
//                {
//                    Id = new KebabModel.ItemId { Partition = kebab.PartitionKey, Key = kebab.RowKey },
//                    Input = kebab.Input,
//                    Kebab = kebab.Kebab,
//                    Started = kebab.Started,
//                    Completed = kebab.Completed
//                };

//                return result;
//            }
//            catch (Exception ex)
//            {
//                this.logger.LogError(ex, "Failed to fetch kebab from history.");
//                throw new KebabifyException("Failed to fetch kebab from history.");
//            }
//        }

//        [SuppressMessage("Microsoft.Design", "CA1031", Justification = "This is fine")]
//        public virtual async Task<(bool isHealthy, string message)> IsHealthy()
//        {
//            try
//            {
//                var table = await this.ResolveTable();
//                return (true, string.Empty);
//            }
//            catch (Exception ex)
//            {
//                return (false, ex.Message);
//            }
//        }

//        private async Task<CloudTable> ResolveTable()
//        {
//            this.logger.LogDebug($"Connect to: {this.options.StorageAccountName}");
//            var account = this.ResolveAccount();
//            var tableClient = account.CreateCloudTableClient();
//            var table = tableClient.GetTableReference("kebabs");

//            this.logger.LogDebug("Create table if not exists: 'kebabs'");
//            if (!await table.ExistsAsync())
//            {
//                await table.CreateIfNotExistsAsync();
//            }

//            return table;
//        }

//        private CloudStorageAccount ResolveAccount()
//        {
//            if (this.options.UseEmulator)
//            {
//                this.logger.LogDebug("Use storage emulator");
//                return CloudStorageAccount.Parse("UseDevelopmentStorage=true");
//            }

//            this.logger.LogDebug("Use azure storage");
//            return new CloudStorageAccount(new StorageCredentials(this.options.StorageAccountName, this.options.StorageAccountKey), true);
//        }

//        private class KebabEntity : TableEntity
//        {
//            public KebabEntity()
//            {
//                this.PartitionKey = $"{DateTime.Today.Year}{DateTime.Today.Month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0')}";
//                this.RowKey = Guid.NewGuid().ToString();
//            }

//            public string Input { get; set; }

//            public string Kebab { get; set; }

//            public DateTime Started { get; set; }

//            public DateTime Completed { get; set; }
//        }
//    }
//}
