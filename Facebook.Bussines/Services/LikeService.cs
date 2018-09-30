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
    public class LikeService : ILikeService
    {

        private ILikedDal likedDal;

        public LikeService()
        {
            likedDal = new LikedDal();
        }

        public void Add(Likes entity)
        {
            likedDal.Add(entity);
        }

        public void Delete(int id)
        {
            likedDal.Delete(id);
        }

        public List<Likes> GetAll(Expression<Func<Likes, bool>> expression = null)
        {
            return likedDal.GetAll(expression);
        }

        public Likes GetBy(Expression<Func<Likes, bool>> expression = null)
        {
            return likedDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return likedDal.Query<E>();
        }

        public void Update(Likes entity)
        {
            likedDal.Update(entity);
        }
    }
}
