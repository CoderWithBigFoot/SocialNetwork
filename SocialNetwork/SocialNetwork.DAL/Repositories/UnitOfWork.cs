using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.Interfaces;
using SocialNetwork.DAL.EF;
namespace SocialNetwork.DAL.Repositories
{
   public class UnitOfWork : IUnitOfWork
    {
        private SocialNetworkContext context;

        private ProfileRepository profileRepository;
        private PostRepository postRepository;
        private HashtagRepository hashtagRepository;
        private CommentRepository commentRepository;

        public UnitOfWork(string connectionString="SocialNetworkConnection") {
            context = new SocialNetworkContext(connectionString);
        }

        public IProfileRepository<Profile> Profiles {
            get {
                if (profileRepository == null) {
                    profileRepository = new ProfileRepository(context);
                }
                return profileRepository;
            }
        }
        public IRepository<Post> Posts {
            get {
                if (postRepository == null) {
                    postRepository = new PostRepository(context);
                }
                return postRepository;
            }
        }
        public IHashtagRepository<Hashtag> Hashtags {
            get {
                if (hashtagRepository == null) {
                    hashtagRepository = new HashtagRepository(context);
                }
                return hashtagRepository;
            }
        }
        public ICanBeDeletedRepository<Comment> Comments {
            get {
                if (commentRepository == null) {
                    commentRepository = new CommentRepository(context);
                }
                return commentRepository;
            }
        }

        public int Save() {
           return context.SaveChanges();
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
