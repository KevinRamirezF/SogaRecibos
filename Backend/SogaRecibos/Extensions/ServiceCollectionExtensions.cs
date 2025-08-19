using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SogaRecibos.Application.Abstractions.Auth;
using SogaRecibos.Application.Abstractions.Persistence;
using SogaRecibos.Application.Receipts.Commands;
using SogaRecibos.Application.Receipts.Delete;
using SogaRecibos.Application.Receipts.Factories;
using SogaRecibos.Application.Receipts.Queries;
using SogaRecibos.Application.Receipts.Strategies;
using SogaRecibos.Infrastructure.Auth;
using SogaRecibos.Infrastructure.Persistence;
using SogaRecibos.Infrastructure.Receipts;
using SogaRecibos.Infrastructure.Receipts.Redirectors;
using SogaRecibos.Infrastructure.Receipts.Validation;
using SogaRecibos.Application.Receipts.Mapping;
using SogaRecibos.Infrastructure.Receipts.Factories;

namespace SogaRecibos.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // AutoMapper
        services.AddAutoMapper(typeof(ReceiptProfile).Assembly);

        // Use Cases / Handlers
        services.AddScoped<ICreateReceiptHandler, CreateReceiptHandler>();
        services.AddScoped<IListReceiptsHandler, ListReceiptsHandler>();
        services.AddScoped<IDeleteReceiptHandler, DeleteReceiptHandler>();
        services.AddScoped<IValidateReceiptHandler, ValidateReceiptHandler>();
        services.AddScoped<IRedirectToPayHandler, RedirectToPayHandler>();

        // Validators
        services.AddValidatorsFromAssemblyContaining<CreateReceiptCommand>();

        return services;
    }

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlServer(config.GetConnectionString("Default")));

        // Repos / UoW
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // UserResolver
        services.AddScoped<IUserResolver, UserResolver>();

        // Strategies & Factories
        services.AddSingleton<IReceiptValidator, EbsaReceiptValidator>();
        services.AddSingleton<IReceiptValidator, VantiReceiptValidator>();
        services.AddSingleton<IReceiptValidator, CoserviciosReceiptValidator>();
        services.AddSingleton<IReceiptValidatorFactory, ReceiptValidatorFactory>();

        services.AddSingleton<IRedirectUrlBuilder, EbsaRedirectUrlBuilder>();
        services.AddSingleton<IRedirectUrlBuilder, VantiRedirectUrlBuilder>();
        services.AddSingleton<IRedirectUrlBuilder, CoserviciosRedirectUrlBuilder>();
        services.AddSingleton<IRedirectUrlBuilderFactory, RedirectUrlBuilderFactory>();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<SogaRecibos.Application.Abstractions.Auth.ICurrentUserAccessor, SogaRecibos.Infrastructure.Auth.CurrentUserAccessor>();

        services.AddHttpContextAccessor();

        services.AddControllers();
        services.AddFluentValidationAutoValidation();

        // Auth (Supabase JWT via OIDC metadata → JWKS)
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var supabaseUrl = config["Supabase:Url"]!.TrimEnd('/');
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{supabaseUrl}/auth/v1",
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                options.MetadataAddress = $"{supabaseUrl}/auth/v1/.well-known/openid-configuration";
            });

        services.AddAuthorization();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "SogaRecibos API", Version = "v1" });
            var scheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            };
            c.AddSecurityDefinition("Bearer", scheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement { [scheme] = Array.Empty<string>() });
        });

        return services;
    }
}
