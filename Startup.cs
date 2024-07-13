using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CLIENTE.Data;

namespace CLIENTE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurar DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configurar servicios de controladores
            services.AddControllers();

            // Configurar CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Agregar servicio de autorización si es necesario
            services.AddAuthorization();

            // Otros servicios necesarios pueden ser agregados aquí
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Usar middleware de enrutamiento
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Usar CORS
            app.UseCors("AllowAll");

            // Usar middleware de autorización
            app.UseAuthorization();

            // Configurar endpoints de controladores
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Usar archivos estáticos si es necesario
            app.UseStaticFiles();
        }
    }
}
