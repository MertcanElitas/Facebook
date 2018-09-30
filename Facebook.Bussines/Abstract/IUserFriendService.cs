using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
   public  interface IUserFriendService
    {

        List<UserFriend> GetAll(Expression<Func<UserFriend, bool>> expression = null);

        UserFriend GetBy(Expression<Func<UserFriend, bool>> expression = null);

        void Add(UserFriend entity);

        void Update(UserFriend entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;


    }
}
