using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using Persistance.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistance
{
    public class DataSeeding(StoreDbContext _dbContext,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager,
        StoreIdentityDbContext _identityDbContext) : IDataSeeding
    {
       public async Task DataSeedAsync()
        {
            try
            {
                var PendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                //Production
                if (PendingMigrations.Any())
                {
                  await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.Set<ProductBrand>().Any())
                {
                    //  var ProductBrandData =await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeed\brands .json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\brands .json");

                    //Convert Data From "String" to ---> C# Objects [ProductBrand]
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                    }
                       
                }

                if (!_dbContext.Set<ProductType>().Any())
                {
                   // var ProductTypeData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeed\types .json");
                   var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\types .json");
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                    }       
                }

                if (!_dbContext.Set<Product>().Any())
                {
                    // var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeed\products.json");
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\products.json");
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products is not null && Products.Any())
                    {
                       await _dbContext.Products.AddRangeAsync(Products);
                    }                        
                }
                if (!_dbContext.Set<DeliveryMethod>().Any())
                {
                    // var ProductData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeed\products.json");
                    var DeliveryMethodDataStream = File.OpenRead(@"..\Infrastructure\Persistance\Data\DataSeed\delivery.json");
                    var DeliveryMethod = await JsonSerializer.DeserializeAsync<List<DeliveryMethod>>(DeliveryMethodDataStream);
                    if (DeliveryMethod is not null && DeliveryMethod.Any())
                    {
                        await _dbContext.Set<DeliveryMethod>().AddRangeAsync(DeliveryMethod);
                    }
                }
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                
            }
        }

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                 {
                     await _roleManager.CreateAsync(new IdentityRole("Admin"));
                     await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                 }

                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaMohamed"
                    };

                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                   
                     await _userManager.AddToRoleAsync(User01, "Admin");
                     await _userManager.AddToRoleAsync(User02, "SuperAdmin");

                }

                await _identityDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Seeding Error: {ex.Message}");
                throw;
            }
        }
    }
}
