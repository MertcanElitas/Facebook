using Facebook.Domains.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Bussines.Abstract
{
    public interface IMessageService
    {

        List<Messages> GetAll(Expression<Func<Messages, bool>> expression = null);

        Messages GetBy(Expression<Func<Messages, bool>> expression = null);

        void Add(Messages entity);

        void Update(Messages entity);

        void Delete(int id);

        IQueryable<E> Query<E>() where E : class;


    }
}
