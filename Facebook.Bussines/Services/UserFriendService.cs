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
   public class UserFriendService:IUserFriendService
    {

        private IUserFriendDal userFriendDal;
        public UserFriendService()
        {
            userFriendDal = new UserFriendDal();
        }

        public void Add(UserFriend entity)
        {
            userFriendDal.Add(entity);

        }

        public void Delete(int id)
        {
            userFriendDal.Delete(id);
        }

        public List<UserFriend> GetAll(Expression<Func<UserFriend, bool>> expression = null)
        {
            return userFriendDal.GetAll(expression);
        }

        public UserFriend GetBy(Expression<Func<UserFriend, bool>> expression = null)
        {
            return userFriendDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return userFriendDal.Query<E>();
        }

        public void Update(UserFriend entity)
        {
            userFriendDal.Update(entity);
        }
    }
}
