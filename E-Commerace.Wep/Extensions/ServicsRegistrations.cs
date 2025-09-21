using E_Commerace.Wep.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.OpenApi.Models;
namespace E_Commerace.Wep.Extensions
{
    public static class ServicsRegistrations
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(Options =>
            {
                Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In= ParameterLocation.Header,
                    Name="Authorization",
                    Type=SecuritySchemeType.ApiKey,
                    Scheme="Bearer",
                    Description="Enter 'Bearer' Follwed By Space And Your Token"
                });

                Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Id="Bearer",
                                Type=ReferenceType.SecurityScheme
                            }
                        },
                        new string[] {}
                    }
                });

            });

            return Services;
        }

        public static IServiceCollection AddWepApplicationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;

            });
            return Services;
        }

        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer=true,
                    ValidIssuer= configuration["JWTOptions:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],
                    ValidateLifetime=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"]))
                };
            });


            return services;
        }
    }
}
