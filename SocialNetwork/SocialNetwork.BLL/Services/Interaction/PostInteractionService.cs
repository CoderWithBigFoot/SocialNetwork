using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.BLL.Interfaces.Interaction;
using SocialNetwork.BLL.Infrastructure.Exceptions;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.Repositories;

namespace SocialNetwork.BLL.Services.Interaction
{
   public class PostInteractionService : IPostInteraction
    {
        private IUnitOfWork uow;
        public PostInteractionService(IUnitOfWork uow) {
            this.uow = uow;
        }
/// <summary>
/// True if added; false if didnt
/// </summary>
/// <param name="postId"></param>
/// <param name="identityName"></param>
/// <returns></returns>
        public bool Like(int postId, string identityName) {

            SocialNetwork.DAL.EF.Post post = uow.Posts.Get(postId);
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            bool result = false;

            if (post == null) { throw new PostNotFoundException("Post was not found"); }
            if(profile == null) { throw new ProfileNotFoundException("Profile was not found"); }

            if (post.LikeVoices.Contains(profile)) {
                post.LikeVoices.Remove(profile);
                result = false;
            }
            if (!post.LikeVoices.Contains(profile)) {
                post.LikeVoices.Add(profile);
                result = true;
            }

            uow.Save();
            return result;
        }
        /// <summary>
        /// True if reposted;false if didnt
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="identityName"></param>
        /// <returns></returns>
        public bool Repost(int postId, string identityName) {
            SocialNetwork.DAL.EF.Post post = uow.Posts.Get(postId);
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            bool result = false;

            if (post == null) { throw new PostNotFoundException("Post was not found"); }
            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }

            if (profile.RepostedPosts.Contains(post)) {
                //we can delete the post from reposts only from the home page
                result = false;
            }

            if (!profile.RepostedPosts.Contains(post)) {
                profile.RepostedPosts.Add(post);
                result = true;
            }
        
            uow.Save();
            return result;
        }

        public bool PublishPost(PostForPublicateDTO newPost, IEnumerable<HashtagDTO> hashtags) {
            SocialNetwork.DAL.EF.Profile publisher = uow.Profiles.FindByIdentityName(newPost.PublisherIdentityName);

        }


        public void Dispose() {
            uow.Dispose();
        }
    }
}
