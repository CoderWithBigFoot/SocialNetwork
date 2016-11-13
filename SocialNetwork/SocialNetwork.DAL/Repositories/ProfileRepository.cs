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

        public void Delete(Profile item)
        {
            if (context.Profiles.Find(item.Id) == null) { return; }

           var deletePublishedPostsCollection = context.Posts.Where(x => x.Publisher == item); //published posts
            context.Posts.RemoveRange(deletePublishedPostsCollection);

            var deleteRepostsCollection = context.Posts.Where(x => x.Reposters.Contains(item)); // reposts
            if (deleteRepostsCollection != null)
            {
                foreach (var currentRepost in deleteRepostsCollection)
                {
                    currentRepost.Reposters.Remove(item);
                }
            }

            var deleteLikedPostsCollection = context.Posts.Where(x => x.LikeVoices.Contains(item));//likes
            if (deleteLikedPostsCollection != null) {
                foreach (var likedPost in deleteLikedPostsCollection) {
                    likedPost.LikeVoices.Remove(item);
                }
            }

            var deleteCommentsCollection = context.Comments.Where(x => x.Commentator == item); //comments
            context.Comments.RemoveRange(deleteCommentsCollection);




            var deleteSubscribersOnCollection = context.Profiles.Where(x => x.SubscribedOn.Contains(item)); //subscribedOn
            if (deleteSubscribersOnCollection != null)
            {
                foreach (var currentProfile in deleteSubscribersOnCollection)
                {
                    currentProfile.SubscribedOn.Remove(item);
                }
            }

            var deleteFollowersCollection = context.Profiles.Where(x => x.Followers.Contains(item)); //followers
            if (deleteFollowersCollection != null) {
                foreach (var currentProfile in deleteFollowersCollection) {
                    currentProfile.Followers.Remove(item);
                }
            }

            context.Profiles.Remove(item);
        }

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
