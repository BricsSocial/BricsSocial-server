using BricsSocial.Application.Common.Interfaces;
using BricsSocial.Infrastructure.Authentication;
using BricsSocial.Infrastructure.FileStorage;
using BricsSocial.Infrastructure.Identity;
using BricsSocial.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricsSocial.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // DB context
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("BricsSocialDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
                    options.UseNpgsql(connectionString,
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                    //options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                    //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    //    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });   
            }

            services.AddScoped<IApplicationDbContext>(
                provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ApplicationDbContextInitializer>();

            // File storage
            var s3Options = new S3Options();
            s3Options.ServiceUrl = Environment.GetEnvironmentVariable("S3_SERVICE_URL");
            s3Options.Region = Environment.GetEnvironmentVariable("S3_REGION");
            s3Options.BucketName = Environment.GetEnvironmentVariable("S3_BUCKET_NAME");
            s3Options.KeyId = Environment.GetEnvironmentVariable("S3_KEY_ID");
            s3Options.KeySecret = Environment.GetEnvironmentVariable("S3_KEY_SECRET");
            services.AddSingleton(s3Options);

            services.AddScoped<IFileStorage, S3FileStorage>();

            // Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IIdentityService, IdentityService>();

            var jwtOptions = new JwtOptions();
            configuration.Bind(nameof(JwtOptions), jwtOptions);
            services.AddSingleton(jwtOptions);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtOptions.GetSymmetricSecurityKey(),
                    
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IJwtProvider, JwtProvider>();

            services.AddAuthorization();

            return services;
        }
    }
}
