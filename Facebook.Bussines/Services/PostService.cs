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
   public class PostService:IPostService
    {

        private IPostDal postDal;
        public PostService()
        {
            postDal = new PostDal();

        }

        public void Add(Post entity)
        {
            postDal.Add(entity);
        }

        public void Delete(int id)
        {
            postDal.Delete(id);
        }

        public List<Post> GetAll(Expression<Func<Post, bool>> expression = null)
        {
            return postDal.GetAll(expression);
        }

        public Post GetBy(Expression<Func<Post, bool>> expression = null)
        {
            return postDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return postDal.Query<E>();
        }

        public void Update(Post entity)
        {
            postDal.Update(entity);
        }
    }
}
