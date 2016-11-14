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
        ICanBeDeletedRepository<Hashtag> Hashtags {  get; }
        ICanBeDeletedRepository<Comment> Comments {  get; }

        int Save();
    }
}
