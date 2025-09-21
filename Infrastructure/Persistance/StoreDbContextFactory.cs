/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using Persistance.Data;

namespace Persistance
{
    public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
    {
        public StoreDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                  .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../E-Commerace.Wep"))
                  .AddJsonFile("appsettings.json")
                  .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new StoreDbContext(optionsBuilder.Options);
        }
    }
}*/

using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Persistance.Data;

namespace Persistance
{
    public class StoreDbContextFactory : IDesignTimeDbContextFactory<StoreDbContext>
    {
        public StoreDbContext CreateDbContext(string[] args)
        {
           
            var basePath = AppContext.BaseDirectory;

           
            var projectRoot = Path.GetFullPath(Path.Combine(basePath, "..", "..", "..", ".."));

          
            var configuration = new ConfigurationBuilder()
                /*.SetBasePath(projectRoot)*/ .SetBasePath(@"C:\Users\Rewan\source\repos\E-Commerace.Wep\E-Commerace.Wep")
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

           
            var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
            var conn = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conn);

            return new StoreDbContext(optionsBuilder.Options);
        }
    }
}



