using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Repository
{
   public interface IEntityRepository<T> where T:class,new()
    {

        List<T> GetAll(Expression<Func<T, bool>> expression=null);

        T GetBy(Expression<Func<T, bool>> expression = null);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;


    }
}
