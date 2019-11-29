using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Geolocation.Domain.Domain;
using Geolocation.Infrastructure.Commands;
using Geolocation.Infrastructure.Commands.IP;
using Geolocation.Infrastructure.Commands.URL;
using Geolocation.Infrastructure.DTO;
using Geolocation.Infrastructure.MapperProfiles;
using Geolocation.Infrastructure.Persistence;
using Geolocation.Infrastructure.Queries.IP;
using Geolocation.Infrastructure.Queries.URL;
using Geolocation.Infrastructure.Repositories;
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
using System;

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
            services.AddMvc(opt =>
            {
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddFluentValidation(fvc =>
            //    fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddMediatR(typeof(Startup));

            //services.AddTransient<IValidator<IPDataDTO>, IPDataValidator>();
            //services.AddTransient<IValidator<URLDataDTO>, URLDataValidator>();

            services.AddDbContext<GeolocationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("GeolocationApiDatabase"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
                });
            });

            //services.AddDbContext<GeolocationDbContext>(options =>
            //{
            //    options.UseInMemoryDatabase(databaseName: "GeolocationApiDatabase");                
            //});

            services.AddTransient<IService, IPStackService>();
            services.AddTransient<IRepository<RootObject>, RootObjectRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Geolocation API",
                    Description = "Simple API to provide geolocation data based on URL or IP address"
                });

            });

            //services.AddAutoMapper(typeof(Startup));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GeolocationResponseProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(typeof(GetGeolocationDataByIPQueryHandler).Assembly,
                                typeof(GetGeolocationDataByURLQueryHandler).Assembly,
                                typeof(AddIPCommandHandler).Assembly,
                                typeof(DeleteIPCommandHandler).Assembly,
                                typeof(AddURLCommandHandler).Assembly,
                                typeof(DeleteURLCommandHandler).Assembly);
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
