using System.Text.Json.Serialization;
using guacactings;
using guacactings.Context;
using guacactings.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


// Add DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(builder.Environment.IsDevelopment());
});

builder.Services.AddInjections();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "api", Version = "v1" });

    options.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Description = "API Key Authentication header using the Authorization scheme."
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
    options.AddPolicy("AllowLocalhostOnly", cors =>
    {
        cors.WithOrigins("http://localhost:8000")
            .AllowAnyHeader()
            .AllowAnyMethod();
    }));

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

// allow from localhost 8000 and 5177
builder.Services.AddCors(options =>
    options.AddPolicy("AllowLocalhostOnly", cors =>
    {
        cors.WithOrigins("http://localhost:8000", "http://localhost:5177")
            .AllowAnyHeader()
            .AllowAnyMethod();
    }));


builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.InjectJavascript("/swagger/api-key.js");
});

app.Use((context, next) =>
{
    var remoteIpAddress = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    var localIpAddress = context.Connection.LocalIpAddress?.ToString() ?? string.Empty;
    
    var segments1 = remoteIpAddress.Split(".");
    var segments2 = localIpAddress.Split(".");

    var remoteIpShortened = string.Join(".", segments1.Take(3));
    var localIpShortened = string.Join(".", segments2.Take(3));

    if (localIpShortened == remoteIpShortened) return next();
    
    context.Response.StatusCode = 403; // Forbidden
    return Task.CompletedTask;

});

app.UseCors("AllowLocalhostOnly");

app.UseErrorHandlingMiddleware();
app.UseApiAuthorization();

app.UseAuthorization();
app.UseApiVersioning();

app.MapControllers();

app.Run();