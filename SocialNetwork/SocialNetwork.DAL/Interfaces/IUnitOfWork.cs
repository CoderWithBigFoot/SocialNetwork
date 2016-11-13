using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.EF;

namespace SocialNetwork.DAL.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
        IProfileRepository<Profile> Profiles { get; }
        IRepository<Post> Posts { get; }
        IRepository<Hashtag> Hashtags { set; get; }
        IRepository<Comment> Comments { set; get; }

        void Save();
    }
}
