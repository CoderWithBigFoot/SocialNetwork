using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Interfaces
{
   public interface IHashtagRepository<T> : ICanBeDeletedRepository<T> where T:class
    {
        T FindByName(string name);
    }
}
