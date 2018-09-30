using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
  public  interface ILikeService
    {

        List<Likes> GetAll(Expression<Func<Likes, bool>> expression = null);

        Likes GetBy(Expression<Func<Likes, bool>> expression = null);

        void Add(Likes entity);

        void Update(Likes entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;


    }
}
