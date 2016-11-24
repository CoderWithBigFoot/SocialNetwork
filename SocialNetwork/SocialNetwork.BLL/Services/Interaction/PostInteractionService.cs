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

        private SocialNetwork.DAL.EF.Post GetPost(int postId) {
            SocialNetwork.DAL.EF.Post post = uow.Posts.Get(postId);
            if (post == null) { throw new PostNotFoundException("Post was not found"); }
            return post;
        }
        private SocialNetwork.DAL.EF.Profile GetProfile(string identityName) {
            SocialNetwork.DAL.EF.Profile profile = uow.Profiles.FindByIdentityName(identityName);
            if (profile == null) { throw new ProfileNotFoundException("Profile was not found"); }
            return profile;
        }
        public bool Like(int postId, string identityName) {

            SocialNetwork.DAL.EF.Post post = this.GetPost(postId);
            SocialNetwork.DAL.EF.Profile profile = this.GetProfile(identityName) ;
            bool result = false;

 
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
            SocialNetwork.DAL.EF.Post post = this.GetPost(postId);
            SocialNetwork.DAL.EF.Profile profile = this.GetProfile(identityName);
            bool result = false;

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
            if (hashtags == null || hashtags.Count() == 0) { return null; }


            ICollection<SocialNetwork.DAL.EF.Hashtag> existingAndNewHashtags = new List<SocialNetwork.DAL.EF.Hashtag>();
            Mapper.Initialize(cfg => cfg.CreateMap<HashtagDTO,SocialNetwork.DAL.EF.Hashtag>());
            existingAndNewHashtags = Mapper.Map<ICollection<SocialNetwork.DAL.EF.Hashtag>>(hashtags);

            //проверку сделать на повторения хэштегов


            return existingAndNewHashtags; 
        }

        public void PublishPost(PostForPublicateDTO newPost, IEnumerable<HashtagDTO> hashtags) {
            SocialNetwork.DAL.EF.Profile publisher = this.GetProfile(newPost.PublisherIdentityName);
            SocialNetwork.DAL.EF.Post publishedPost = new DAL.EF.Post();

            ICollection<SocialNetwork.DAL.EF.Hashtag> existingAndNewHashtags = this.GetExistingAndNewHashtags(hashtags); 
            Mapper.Initialize(cfg => cfg.CreateMap<PostForPublicateDTO,SocialNetwork.DAL.EF.Post>());
            

            publishedPost = Mapper.Map<SocialNetwork.DAL.EF.Post>(newPost);
            publishedPost.Publisher = publisher;
            if (existingAndNewHashtags != null)
            {
                publishedPost.Hashtags = existingAndNewHashtags;
            }
            uow.Posts.Create(publishedPost);
            uow.Save();
        }


        public void Dispose() {
            uow.Dispose();
        }
    }
}
