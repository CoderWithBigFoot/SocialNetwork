using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.DAL.Repositories
{
    public class ProfileRepository : IProfileRepository<Profile>
    {
        private SocialNetworkContext context { set; get; }

        public ProfileRepository(SocialNetworkContext context) {
            this.context = context;
        }

        public void Create(Profile item)
        {
            context.Profiles.Add(item);
        }

        //while incorrect
        /*
        public void Delete(Profile item)
        {

            if (context.Profiles.Find(item.Id) == null) { return; }
            //удалить публикованые посты еще у тех, кто их репостил или лайкал
            //remove published posts
            context.Posts.RemoveRange(item.PublishedPosts);
            //remove this user from Reposters
            var repostedPosts = context.Posts.Where(x => x.Reposters.Contains(item));
            if (repostedPosts != null) {
                foreach (var post in repostedPosts) {
                    post.Reposters.Remove(item);
                }
            }
            //remove this user from LikeVoices
            var likedPosts = context.Posts.Where(x => x.LikeVoices.Contains(item));
            if (likedPosts != null) {
                foreach (var post in likedPosts) {
                    post.LikeVoices.Remove(item);
                }
            }



            //remove this user from subscribedOn's
            foreach (var user in item.Followers) {
                user.SubscribedOn.Remove(item);
            }
            //remove this user from Followers
            foreach (var user in item.SubscribedOn) {
                user.Followers.Remove(item);
            }
            //remove comments of that user
            foreach (var comment in item.Comments) {
                comment.Post.Comments.Remove(comment);
            }
            context.Comments.RemoveRange(item.Comments);

            context.Profiles.Remove(item);
        }
        */


        public IEnumerable<Profile> Find(Func<Profile, bool> predicate)
        {
            return context.Profiles.Where(predicate);
        }
        public Profile FindByIdentityName(string identityName)
        {
            return context.Profiles.FirstOrDefault(x=>x.IdentityName == identityName);
        }
        public Profile Get(int id)
        {
            return context.Profiles.Find(id);
        }
        public IEnumerable<Profile> GetAllItems()
        {
            return context.Profiles;
        }
    }
}
