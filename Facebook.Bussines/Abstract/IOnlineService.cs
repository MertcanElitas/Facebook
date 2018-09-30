using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
    public interface IOnlineService
    {


        List<Online> GetAll(Expression<Func<Online, bool>> expression = null);

        Online GetBy(Expression<Func<Online, bool>> expression = null);

        void Add(Online entity);

        void Update(Online entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;

    }
}
