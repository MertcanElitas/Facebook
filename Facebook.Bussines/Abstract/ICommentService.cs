using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
  public  interface ICommentService
    {


        List<Comment> GetAll(Expression<Func<Comment, bool>> expression = null);

        Comment GetBy(Expression<Func<Comment, bool>> expression = null);

        void Add(Comment entity);

        void Update(Comment entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;



    }
}
