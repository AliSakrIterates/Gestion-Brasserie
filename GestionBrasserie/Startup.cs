using AspNetCore.Serilog.RequestLoggingMiddleware;
using GestionBrasserie.Data;
using GestionBrasserie.Domain;
using GestionBrasserie.Hosting.Controllers;
using GestionBrasserie.Hosting.Middlewares;
using GestionBrasserie.Services.Brewery;
using GestionBrasserie.Services.Reseller;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

namespace GestionBrasserie.Hosting
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            var handlerAssemblies = new[] { typeof(CreateBeerHandler).Assembly };
            services.AddMediatR(handlerAssemblies);
            services.AddSingleton<LoggingExceptionHandler>();
            services.AddScoped<IBreweryRepository, BreweryRepository>();
            services.AddScoped<IResellerRepository, ResellerRepository>();
            services.AddScoped<IBeersRepository, BeersRepository>();
            services.AddControllers();
            services.AddSingleton(Log.Logger);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "GestionBrasserie", Version = "v1"}); });
            services.AddDbContext<GestionBrasserieContext>();
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionBrasserie v1"));

            app.UseSerilogRequestLogging();
            app.UseMiddleware<LoggingExceptionHandler>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}