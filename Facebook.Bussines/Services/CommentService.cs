using Facebook.Bussines.Abstract;
using Facebook.DataAccess.Abstract;
using Facebook.DataAccess.Concrete;
using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Services
{
    public class CommentService : ICommentService
    {

        private ICommentDal commentDal;

        public CommentService()
        {
            commentDal = new CommentDal();
        }

        public void Add(Comment entity)
        {
            commentDal.Add(entity);
        }

        public void Delete(int id)
        {
            commentDal.Delete(id);
        }

        public List<Comment> GetAll(Expression<Func<Comment, bool>> expression = null)
        {
            return commentDal.GetAll(expression);
        }

        public Comment GetBy(Expression<Func<Comment, bool>> expression = null)
        {
            return commentDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return commentDal.Query<E>();
        }

        public void Update(Comment entity)
        {
            commentDal.Update(entity);
        }
    }
}
