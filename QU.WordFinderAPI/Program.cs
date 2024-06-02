using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using QU.WordFinderAPI;
using QU.WordFinderAPI.Cache;
using QU.WordFinderAPI.Domain.Models;
using QU.WordFinderAPI.Interfaces;
using QU.WordFinderAPI.Services;
using QU.WordFinderAPI.Services.Cache;
using QU.WordFinderAPI.Services.Validator;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Business WordFinderAPI.Services.
builder.Services.AddScoped<IWordFinderService, WordFinderService>();

// Fluent Validation.
builder.Services.AddScoped<IValidator<WordFinder>, WordFinderValidator>();

// Cache
builder.Services.Configure<RedisOptions>(options => builder.Configuration.GetSection(RedisOptions.Redis).Bind(options));
builder.Services.AddScoped<ICacheHelper, RedisCacheHelper>();
builder.Services.AddScoped<IWordFinderCache, WordFinderCache>();

// JWT Bearer Token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("JWT").GetValue<string>("Issuer"),
            ValidAudience = builder.Configuration.GetSection("JWT").GetValue<string>("Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetValue<string>("Key")))
        };
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature.Error;
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new ErrorResponse(exception));
    });
});

app.MapControllers();

app.Run();

public partial class Program { }