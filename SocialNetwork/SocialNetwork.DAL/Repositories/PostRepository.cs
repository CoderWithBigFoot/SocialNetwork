using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.EF;

namespace SocialNetwork.DAL.Repositories
{
    public class PostRepository : IRepository<Post>
    {
        private SocialNetworkContext context { set; get; }
        public PostRepository(SocialNetworkContext context) {
            this.context = context;
        }

        public void Create(Post item)
        {
            context.Posts.Add(item);
        }
        /*
        public void Delete(Post item)
        {
            if (context.Posts.Find(item.Id) == null) { return; }
            //remove hashtags
            foreach (var hashtag in context.Hashtags.Where(x => x.Posts.Contains(item))) {
                item.Hashtags.Remove(hashtag);
            }
            item.Hashtags.Remove(context.Hashtags.Where(x=>x.Name == "cat").FirstOrDefault());
            //context.SaveChanges();
            //maybe autodelete linked comments  -- check after
            //maybe autodelete this post in PublishedPosts 
            foreach (var profile in item.Reposters) {
                profile.RepostedPosts.Remove(item);
            }
            foreach (var profile in item.LikeVoices) {
                profile.LikedPosts.Remove(item);
            }

            context.Posts.Remove(item);

        }*/
        public IEnumerable<Post> Find(Func<Post, bool> predicate)
        {
            return context.Posts.Where(predicate);
        }
        public Post Get(int id)
        {
            return context.Posts.Find(id);
        }
        public IEnumerable<Post> GetAllItems()
        {
            return context.Posts;
        }
    }
}
