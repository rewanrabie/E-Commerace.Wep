
using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models.IdentityModule;
using E_Commerace.Wep.CustamMiddelWares;
using E_Commerace.Wep.Extensions;
using E_Commerace.Wep.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistance;
using Persistance.Data;
using Persistance.Identity;
using Persistance.Repositories;
using Serives;
using Serives.MappingProfiles;
using ServicesAbstraction;
using Shared.ErrorModels;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;

namespace E_Commerace.Wep
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

        #region //Add Services To the Containter

            builder.Services.AddControllers();
            builder.Services.AddCors(Options=>
                {
                Options.AddPolicy("AllowAll", builder=>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
                });
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWepApplicationServices();
            // builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            // .AddEntityFrameworkStores<StoreIdentityDbContext>();

            builder.Services.AddJWTService(builder.Configuration);

            #endregion

            var app = builder.Build();

           await app.SeedDataBaseAsync();


            #region // Configure the HTTP request pipeline.

            app.UseCustomExceptionMiddleWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(Options=> 
                {
                    Options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration=true,
                    };

                    Options.DocumentTitle = "My E-Commerce API";

                    /*Options.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };*/

                    Options.DocExpansion(DocExpansion.None);
                    Options.EnableFilter();
                 
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}
