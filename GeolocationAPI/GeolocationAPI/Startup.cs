using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.Persistence;
using Geolocation.Infrastructure.Queries.IP;
using Geolocation.Infrastructure.Services;
using GeolocationAPI.Validation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace GeolocationAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("azurekeyvault.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation();
            services.AddMediatR(typeof(Startup));
            services.AddTransient<IValidator<IPDataDTO>, IPDataValidator>();
            services.AddTransient<IValidator<URLDataDTO>, URLDataValidator>();

            services.AddDbContext<GeolocationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SimpleApiDatabase")), ServiceLifetime.Transient);

            services.AddTransient<IService, IPStackService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Geolocation API",
                    Description = "Simple API to provide geolocation data based on URL or IP address"
                });

            });

            services.AddAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(GetGeolocationDataByIPQueryHandler).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSerilogRequestLogging();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Geolocation API V1");
            });
        }
    }
}
