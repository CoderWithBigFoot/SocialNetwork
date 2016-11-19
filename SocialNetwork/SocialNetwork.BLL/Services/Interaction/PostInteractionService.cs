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
using AutoMapper;
namespace SocialNetwork.BLL.Services.Interaction
{
   public class PostInteractionService : IPostInteraction
    {
        private IUnitOfWork uow;
        public PostInteractionService(IUnitOfWork uow) {
            this.uow = uow;
        }

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
        //if there is no such hashtag - add it
        private ICollection<SocialNetwork.DAL.EF.Hashtag> GetExistingAndNewHashtags(IEnumerable<HashtagDTO> hashtags)
        {
            if (hashtags == null) { throw new ArgumentNullException(); }
            ICollection<SocialNetwork.DAL.EF.Hashtag> existingAndNewHashtags = new List<SocialNetwork.DAL.EF.Hashtag>();
            SocialNetwork.DAL.EF.Hashtag hashtag;

            foreach (var currentHashtag in hashtags)
            {
                hashtag = uow.Hashtags.FindByName(currentHashtag.Name);
                if (hashtag != null)
                {
                    existingAndNewHashtags.Add(hashtag);
                }
                if (hashtag == null) {
                    uow.Hashtags.Create(new DAL.EF.Hashtag() {
                        Name = hashtag.Name
                    });
                }
            }
            return existingAndNewHashtags;
        }
        public void PublishPost(PostForPublicateDTO newPost, IEnumerable<HashtagDTO> hashtags) {
            SocialNetwork.DAL.EF.Profile publisher = uow.Profiles.FindByIdentityName(newPost.PublisherIdentityName);
            SocialNetwork.DAL.EF.Post publishedPost = new DAL.EF.Post();

            if (publisher == null) { throw new ProfileNotFoundException("Publisher was not found"); }
            ICollection<SocialNetwork.DAL.EF.Hashtag> existingHashtags = this.GetExistingAndNewHashtags(hashtags); 

        

            Mapper.Initialize(cfg => cfg.CreateMap<PostForPublicateDTO,SocialNetwork.DAL.EF.Post>());
            

            publishedPost = Mapper.Map<SocialNetwork.DAL.EF.Post>(newPost);
            publishedPost.Publisher = publisher;
            publishedPost.Hashtags = existingHashtags;

            uow.Posts.Create(publishedPost);
            uow.Save();
        }


        public void Dispose() {
            uow.Dispose();
        }
    }
}
