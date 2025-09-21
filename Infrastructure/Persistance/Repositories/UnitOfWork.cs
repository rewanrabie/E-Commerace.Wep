using DomainLayer.InterFaceRepostory_Contracts_;
using DomainLayer.Models;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenaricRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            //Get Type Name
            var typeName = typeof(TEntity).Name;
            //Dictionary<string,object> ==> string key [name of type] -- object from genaric repository
            // if (_repositories.ContainsKey(typeName))
            //  return (IGenaricRepository<TEntity, TKey>)_repositories[typeName];
            if (_repositories.TryGetValue(typeName , out object? value))
                return (IGenaricRepository<TEntity, TKey>)value;
            else
            {
                //create object
                var Repo = new GenaricRepository<TEntity,TKey>(_dbContext);
                //store object in dictionary
                _repositories["typeName"] = Repo;
                //return object 
                return Repo;
            }
        }
        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
      
    }
}
