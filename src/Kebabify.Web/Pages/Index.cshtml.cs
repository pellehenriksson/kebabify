using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Reflection;

namespace Kebabify.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Version { get; set; } = string.Empty;

        public void OnGet()
        {
            this.Version = $"Version: {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";
        }
    }
}