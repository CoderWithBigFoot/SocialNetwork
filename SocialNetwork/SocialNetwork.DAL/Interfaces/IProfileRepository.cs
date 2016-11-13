using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.DAL.Interfaces
{
   public interface IProfileRepository<T> : IRepository<T> where T:Profile
    {
        T FindByIdentityName(string identityName);
    }
}
