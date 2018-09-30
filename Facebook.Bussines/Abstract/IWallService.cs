using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
    public interface IWallService
    {

        List<Wall> GetAll(Expression<Func<Wall, bool>> expression = null);

        Wall GetBy(Expression<Func<Wall, bool>> expression = null);

        void Add(Wall entity);

        void Update(Wall entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;


    }
}
