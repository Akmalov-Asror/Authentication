﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Teaching.Common.SwaggerVersioning;
using Teaching.V1.Auth.CQRS.Services;
using Teaching.V1.Auth.Services.AuthServices;
using Teaching.V1.Auth.Services.Interfaces;
using Teaching.V1.Auth.TokenGenerator;

namespace Teaching.V1.Auth.Services.Extensions;

public static class AddExtensionServices
{
    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserServiceCQRS,UserServiceCQRS>();


        return services;
    }
    public static void AddSwaggerService(
    this IServiceCollection services,
    IConfiguration configuration
)
    {
        var jwtSettings = configuration.GetSection("Jwt");
        services.AddSwaggerGen(p =>
        {
            p.EnableAnnotations();
            //p.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            p.OperationFilter<SwaggerDefaultValues>();
            p.ResolveConflictingActions(ad => ad.First());
            p.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                }
            );
            p.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
                }
            );
        });

        services
            .AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecretKey"])
                    )
                };
            });
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services
            .AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}