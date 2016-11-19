using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.BLL.DTO;
using SocialNetwork.DAL.Repositories;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.EF;
using SocialNetwork.BLL.Interfaces.BasicInfo;
using SocialNetwork.BLL.Infrastructure.Exceptions;
using AutoMapper;

namespace SocialNetwork.BLL.Services.BasicInfo
{
   public class GetPostInfoService : IGetPostInfo
    {
        private IUnitOfWork uow;
        public GetPostInfoService(IUnitOfWork uow) {
            this.uow = uow;
        }

        public PostDTO GetPost(int postId) {
            SocialNetwork.DAL.EF.Post post = uow.Posts.Get(postId);
            if (post == null) { throw new PostNotFoundException("Post was not found"); }
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<PostDTO>(post);
        }

        public IEnumerable<HashtagDTO> GetHashtagCollection(int postId) {
           
                Post post = uow.Posts.Get(postId);
                if (post == null) { throw new PostNotFoundException("Post was not found"); }

                Mapper.Initialize(cfg => cfg.CreateMap<Hashtag, HashtagDTO>());
                return Mapper.Map<IEnumerable<HashtagDTO>>(post.Hashtags);
            
            
        }
        public IEnumerable<CommentDTO> GetComments(int postId,int offset,int count=10) { 
            
                Post post = uow.Posts.Get(postId);
                if (post == null) { throw new PostNotFoundException("Post was not found"); }

                Mapper.Initialize(cfg=>cfg.CreateMap<Comment,CommentDTO>());
                return Mapper.Map<IEnumerable<CommentDTO>>(post.Comments).Skip(offset).Take(count);

           
        }
        public int GetRepostsCount(int postId) {
           
                Post post = uow.Posts.Get(postId);
                if (post == null) { throw new PostNotFoundException("Post was not found"); }
                return post.Reposters.Count;     
        }
        public int GetLikesCount(int postId) {
            
            Post post = uow.Posts.Get(postId);
            if (post == null) { throw new PostNotFoundException("Post was not found"); }
            return post.LikeVoices.Count;          
        }

        public void Dispose() {
            uow.Dispose();
        }
    }
}
