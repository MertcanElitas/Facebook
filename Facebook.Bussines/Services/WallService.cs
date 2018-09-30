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
    public class WallService : IWallService
    {

        private IWallDal wallDal;

        public WallService()
        {
            wallDal = new WallDal();
        }

        public void Add(Wall entity)
        {
            wallDal.Add(entity);
        }

        public void Delete(int id)
        {
            wallDal.Delete(id);
        }

        public List<Wall> GetAll(Expression<Func<Wall, bool>> expression = null)
        {
            return wallDal.GetAll(expression);
        }

        public Wall GetBy(Expression<Func<Wall, bool>> expression = null)
        {
            return wallDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return wallDal.Query<E>();
        }

        public void Update(Wall entity)
        {
            wallDal.Update(entity);
        }
    }
}
