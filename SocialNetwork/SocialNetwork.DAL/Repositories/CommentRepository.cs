using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.DAL.EF;
using SocialNetwork.DAL.Interfaces;
namespace SocialNetwork.DAL.Repositories
{
    public class CommentRepository : ICanBeDeletedRepository<Comment>
    {
        private SocialNetworkContext context { set; get; }
        public CommentRepository(SocialNetworkContext context) {
            this.context = context;
        }


        public void Create(Comment item)
        {
            context.Comments.Add(item);
        }

        public void Delete(Comment item)
        {
            if (context.Comments.Find(item.Id) == null) { return; }

            item.Commentator.Comments.Remove(item);
            item.Post.Comments.Remove(item);

            context.Comments.Remove(item);
            

        }
        public IEnumerable<Comment> Find(Func<Comment, bool> predicate)
        {
            return context.Comments.Where(predicate);
        }
        public Comment Get(int id)
        {
            return context.Comments.Find(id);
        }
        public IEnumerable<Comment> GetAllItems()
        {
            return context.Comments;
        }
    }
}
