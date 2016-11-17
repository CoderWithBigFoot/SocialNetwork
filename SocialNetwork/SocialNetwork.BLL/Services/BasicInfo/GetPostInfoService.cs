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
            Mapper.Initialize(cfg => cfg.CreateMap<Post, PostDTO>());
            return Mapper.Map<PostDTO>(uow.Posts.Get(postId));
        }

        public IEnumerable<HashtagDTO> GetHashtagCollection(int postId) {
            try
            {
                Post post = uow.Posts.Get(postId);
                Mapper.Initialize(cfg => cfg.CreateMap<Hashtag, HashtagDTO>());
                return Mapper.Map<IEnumerable<HashtagDTO>>(post.Hashtags);
            }
            catch (NullReferenceException) {
                return null;
            }
        }

        public IEnumerable<CommentDTO> GetComments(int postId,int offset,int count) {
            try
            {
                Post post = uow.Posts.Get(postId);
                Mapper.Initialize(cfg=>cfg.CreateMap<Comment,CommentDTO>());
                return Mapper.Map<IEnumerable<CommentDTO>>(post.Comments).Skip(offset).Take(count);

            }
            catch (NullReferenceException) {
                return null;
            }
        }



        public int GetReposters(int postId) {
            try
            {
                Post post = uow.Posts.Get(postId);

                return post.Reposters.Count;
            }
            catch (NullReferenceException) {
                return 0;
            }
        }
        public int GetLikes(int postId) {
            try
            {
                Post post = uow.Posts.Get(postId);
                return post.LikeVoices.Count;
            }
            catch (NullReferenceException) {
                return 0;
            }
        }

        public void Dispose() {
            uow.Dispose();
        }
    }
}
