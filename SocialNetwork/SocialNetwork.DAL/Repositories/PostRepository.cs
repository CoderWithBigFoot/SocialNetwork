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
        public void Delete(Post item)
        {
            if (context.Posts.Find(item.Id) == null) { return; }

            //published posts from publisher
            item.Publisher.PublishedPosts.Remove(item);
            
            //comments and Comments from profile
            var linkedCommments = context.Comments.Where(x => x.Post == item);
            var linkedCommentators = linkedCommments.Select(x => new { x.Commentator, x.Id });   

            context.Comments.RemoveRange(linkedCommments);

            foreach (var mappingObject in linkedCommentators) {
                mappingObject.Commentator.Comments.Remove(context.Comments.Find(mappingObject.Id));
            }


            //RepostedPosts from profile delete
            foreach (var prof in item.Reposters) {
                prof.RepostedPosts.Remove(item);
            }
            //LikedPosts from profile delete
            foreach (var prof in item.LikeVoices) {
                prof.LikedPosts.Remove(item);
            }
            //Hashtags from hashtags delete
            foreach (var hashtag in item.Hashtags) {
                hashtag.Posts.Remove(item);
            }

        }
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
