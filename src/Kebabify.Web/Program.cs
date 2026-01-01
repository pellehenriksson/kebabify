using Kebabify.Web.Common;
using Kebabify.Web.Domain.Commands;
using Kebabify.Web.Domain.Services;
using Microsoft.OpenApi;
using System.Reflection;

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

            // OpenAPI / Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Kebabify API", Version = "v1" });

                // Include XML comments if available
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // Swagger UI (enabled in Development and Staging)
            if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kebabify API v1"));
            }

            app.MapRazorPages();
            app.MapControllers();

            app.Run();
        }
    }
}
