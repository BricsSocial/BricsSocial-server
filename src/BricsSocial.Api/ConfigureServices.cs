using BricsSocial.Api.Services;
using BricsSocial.Application.Common.Interfaces;
using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Reflection;

namespace BricsSocial.Api
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllers();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            //services.AddScoped<FluentValidationSchemaProcessor>(provider =>
            //{
            //    var validationRules = provider.GetService<IEnumerable<FluentValidationRule>>();
            //    var loggerFactory = provider.GetService<ILoggerFactory>();

            //    return new FluentValidationSchemaProcessor(provider, validationRules, loggerFactory);
            //});

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo() { Title = "BricsSocial API", Version = "v1" });
                s.CustomSchemaIds(x => x.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? x.Name);

                //s.ExampleFilters();

                s.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
                {
                    Description = 
        "JWT Authorization header using the Bearer scheme. \r\n\r\n Try login request, copy token from response. \r\n\r\n Enter 'Bearer' [space] and then copied token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                s.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = JwtBearerDefaults.AuthenticationScheme
                       }
                      },
                      new string[] { }
                    }
                });
                //s.EnableAnnotations();

                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = AppContext.BaseDirectory + "\\" + xmlFile;
                //s.IncludeXmlComments(xmlPath);
            });

            
            services.TryAddTransient<IValidatorFactory, ServiceProviderValidatorFactory>(); // update
            services.AddFluentValidationRulesToSwagger();

            return services;
        }
    }
}
