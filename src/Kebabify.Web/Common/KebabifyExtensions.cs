using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Kebabify.Web.Common
{
    public static class KebabifyExtensions
    {
        private static readonly JsonSerializerSettings Settings = new ()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static string ToJson<T>(this T item)
        {
            return JsonConvert.SerializeObject(item, Settings);
        }
    }
}
