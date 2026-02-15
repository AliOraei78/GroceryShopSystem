using FluentValidation;
using GroceryShopSystem.Application.Features.Products.Queries;
using GroceryShopSystem.Application.Features.Products.Validators;
using GroceryShopSystem.Application.Interfaces.Repositories;
using GroceryShopSystem.Application.Security;
using GroceryShopSystem.Infrastructure.Persistence;
using GroceryShopSystem.Infrastructure.Repositories;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "Grocery Shop API",
            Version = "v1",
            Description = "API for managing grocery shop operations"
        };

        // Initialize Components if null
        document.Components ??= new OpenApiComponents();

        // Define the Bearer Auth Scheme
        var scheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer", // Must be lowercase "bearer"
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter your valid JWT token."
        };

        if (document.Components.SecuritySchemes == null)
        {
            document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>();
        }
        document.Components.SecuritySchemes["Bearer"] = scheme;

        // Fix: Use 'Security' instead of 'SecurityRequirements'
        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)] = []
        });

        return Task.CompletedTask;
    });
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<JwtTokenService>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQuery).Assembly));

builder.Services.AddValidatorsFromAssemblyContaining<AddProductCommandValidator>();

// Redis Caching
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";
    options.InstanceName = "GroceryShopSystem:";
});

// Hangfire
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseRedisStorage(builder.Configuration.GetConnectionString("Redis")));

builder.Services.AddHangfireServer();

builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "GroceryShopSystem",
            ValidAudience = "GroceryShopSystem",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();
        await AppDbContextSeed.SeedAsync(dbContext, passwordHasher);
    }
}

app.UseHangfireDashboard("/hangfire");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/products", async (IProductRepository repo) =>
{
    var products = await repo.GetAllAsync();
    return Results.Ok(products);
});

app.Run();