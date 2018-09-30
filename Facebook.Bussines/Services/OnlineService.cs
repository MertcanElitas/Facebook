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
    public class OnlineService : IOnlineService
    {

        private IOnlineDal onlineDal;

        public OnlineService()
        {
            onlineDal = new OnlineDal();
        }

        public void Add(Online entity)
        {
            onlineDal.Add(entity);
        }

        public void Delete(int id)
        {
            onlineDal.Delete(id);
        }

        public List<Online> GetAll(Expression<Func<Online, bool>> expression = null)
        {
            return onlineDal.GetAll(expression);
        }

        public Online GetBy(Expression<Func<Online, bool>> expression = null)
        {
            return onlineDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return onlineDal.Query<E>();
        }

        public void Update(Online entity)
        {
            onlineDal.Update(entity);
        }
    }
}
