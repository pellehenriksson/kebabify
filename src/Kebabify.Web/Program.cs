using System.Runtime.CompilerServices;

using Kebabify.Domain.Services;
using Kebabify.Web.Common;
using Kebabify.Web.Domain.Commands;

using MediatR;

namespace Kebabify.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddControllers();
            builder.Services.AddMediatR((cfg) => cfg.RegisterServicesFromAssembly(typeof(MakeKebab).Assembly));

            builder.Services.AddTransient<KebabService, KebabService>();
            builder.Services.AddSingleton(new SystemClock());

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
