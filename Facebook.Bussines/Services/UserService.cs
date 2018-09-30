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
   public class UserService:IUserService
    {

        private IUserDal UserDal;

        public UserService()
        {
            UserDal = new UserDal();
        }

        public void Add(Users entity)
        {
            UserDal.Add(entity);
        }

        public void Delete(int id)
        {
            UserDal.Delete(id);
        }

        public List<Users> GetAll(Expression<Func<Users, bool>> expression = null)
        {
            return UserDal.GetAll(expression);
        }

        public Users GetBy(Expression<Func<Users, bool>> expression = null)
        {
            return UserDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return UserDal.Query<E>();
        }

        public void Update(Users entity)
        {
            UserDal.Update(entity);
        }
    }
}
