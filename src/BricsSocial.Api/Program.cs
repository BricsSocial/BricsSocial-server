
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BricsSocial.Api;
using BricsSocial.Application;
using BricsSocial.Infrastructure;
using BricsSocial.Infrastructure.Persistence;

//namespace BricsSocial.Domain
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = ConfigureBuilder(WebApplication.CreateBuilder(args));
//            var app = ConfigureApplication(builder.Build());
//            app.Run();
//        }

//        public static WebApplicationBuilder ConfigureBuilder(WebApplicationBuilder builder)
//        {
//            // Add services to the container.
//            builder.Services.AddAuthorization();
//            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(options =>
//                {
//                    options.TokenValidationParameters = new TokenValidationParameters
//                    {
//                        // указывает, будет ли валидироваться издатель при валидации токена
//                        ValidateIssuer = true,
//                        // строка, представляющая издателя
//                        ValidIssuer = AuthOptions.ISSUER,
//                        // будет ли валидироваться потребитель токена
//                        ValidateAudience = true,
//                        // установка потребителя токена
//                        ValidAudience = AuthOptions.AUDIENCE,
//                        // будет ли валидироваться время существования
//                        ValidateLifetime = true,
//                        // установка ключа безопасности
//                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//                        // валидация ключа безопасности
//                        ValidateIssuerSigningKey = true,
//                    };
//                });



//            builder.Services.AddControllers();
//            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            return builder;
//        }

//        public static WebApplication ConfigureApplication(WebApplication app)
//        {
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();

//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.MapControllers();

//            return app;
//        }
//    }
//}


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

Directory.SetCurrentDirectory(AppContext.BaseDirectory);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Initialise and seed database
using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});

app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();