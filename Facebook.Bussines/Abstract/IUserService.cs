using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
   public  interface IUserService
    {

        List<Users> GetAll(Expression<Func<Users, bool>> expression = null);

        Users GetBy(Expression<Func<Users, bool>> expression = null);

        void Add(Users entity);

        void Update(Users entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;

    }
}
