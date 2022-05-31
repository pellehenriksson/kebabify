namespace Kebabify.Web.Domain.Models
{
    public class KebabModel
    {
        public ItemId Id { get; set; } = new ItemId();

        public string Kebab { get; set; } = string.Empty;

        public string Input { get; set; } = string.Empty;

        public DateTime Started { get; set; }

        public DateTime Completed { get; set; }

        public class ItemId
        {
            public string Partition { get; set; } = string.Empty;

            public string Key { get; set; } = string.Empty;
        }
    }
}
