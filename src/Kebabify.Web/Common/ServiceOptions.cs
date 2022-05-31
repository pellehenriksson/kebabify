namespace Kebabify.Web.Common
{
    public class ServiceOptions
    {
        public static string ConfigurationSectionKey => "Service";

        public string StorageAccountName { get; set; }

        public string StorageAccountKey { get; set; }

        public bool UseEmulator { get; set; }
    }
}
