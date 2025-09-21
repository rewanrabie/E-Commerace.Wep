using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.InterFaceRepostory_Contracts_
{
    public interface IUnitOfWork
    {
        //public IGenaricRepository<Product,int> ProductRepository { get; }
        //public IGenaricRepository<Employee, int> EmployeeRepository { get; }
        IGenaricRepository<IEntity, IKey> GetRepository<IEntity, IKey>() where IEntity : BaseEntity<IKey>;
        Task<int> SaveChangesAsync();
    }
}
