using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
   public interface IPostService
    {

        List<Post> GetAll(Expression<Func<Post, bool>> expression = null);

        Post GetBy(Expression<Func<Post, bool>> expression = null);

        void Add(Post entity);

        void Update(Post entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;
    }
}
