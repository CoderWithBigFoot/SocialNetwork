using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Interfaces;
namespace SocialNetwork.DAL.Repositories
{
    public class HashtagRepository : IRepository<Hashtag>
    {
        private SocialNetworkContext context { set; get; }
        public HashtagRepository(SocialNetworkContext context) {
            this.context = context;
        }


        public void Create(Hashtag item)
        {
            context.Hashtags.Add(item);
        }

        public void Delete(Hashtag item)
        {
            if (context.Hashtags.Find(item.Id) == null) { return; }
            
            foreach (var currentPost in item.Posts) {
                currentPost.Hashtags.Remove(item);
            }
            context.Hashtags.Remove(item);
        }



        public IEnumerable<Hashtag> Find(Func<Hashtag, bool> predicate)
        {
            return context.Hashtags.Where(predicate);
        }
        public Hashtag Get(int id)
        {
            return context.Hashtags.Find(id);
        }
        public IEnumerable<Hashtag> GetAllItems()
        {
            return context.Hashtags;
        }
    }
}
