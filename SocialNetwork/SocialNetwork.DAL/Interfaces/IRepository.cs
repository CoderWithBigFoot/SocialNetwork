using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.DAL.Interfaces
{
   public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAllItems();
        T Get(int id);
        IEnumerable<T> Find(Func<T,bool> predicate);
        void Create(T item);
    }
}
