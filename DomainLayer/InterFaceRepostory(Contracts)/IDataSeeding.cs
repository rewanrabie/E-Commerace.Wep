using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.InterFaceRepostory_Contracts_
{
    public interface IDataSeeding
    {
        Task DataSeedAsync();
        Task IdentityDataSeedAsync();
    }
}
