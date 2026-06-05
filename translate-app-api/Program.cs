using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using translate_app.application;
using translate_app.Domain.Repositories;
using translate_app.Domain.Services;
using translate_app.Infrastructure.Data;
using translate_app.Infrastructure.Repositories;
using translate_app.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddUserSecrets<Program>(optional: true)
    .AddEnvironmentVariables();

builder.Services.AddApplication();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        var jwtKey = builder.Configuration["Jwt:Key"];
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtAudience = builder.Configuration["Jwt:Audience"];

        if (string.IsNullOrWhiteSpace(jwtKey) ||
            string.IsNullOrWhiteSpace(jwtIssuer) ||
            string.IsNullOrWhiteSpace(jwtAudience))
        {
            throw new InvalidOperationException("Config JWT: Jwt:Key/Issuer/Audience.");
        }

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITranslationRepository, TranslationRepository>();

builder.Services.AddScoped<IUploadFile, UploadFile>();
builder.Services.AddSingleton<IOcrService, OcrService>();
builder.Services.AddSingleton<AIService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddSingleton<PasswordHasherService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();