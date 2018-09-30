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
    public class MessageService : IMessageService
    {
        private IMessageDal messageDal;

        public MessageService()
        {
            messageDal = new MessageDal();
        }


        public void Add(Messages entity)
        {
            messageDal.Add(entity);
        }

        public void Delete(int id)
        {
            messageDal.Delete(id);
        }

        public List<Messages> GetAll(Expression<Func<Messages, bool>> expression = null)
        {
           return  messageDal.GetAll(expression);
        }

        public Messages GetBy(Expression<Func<Messages, bool>> expression = null)
        {
            return messageDal.GetBy(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return messageDal.Query<E>();
        }

        public void Update(Messages entity)
        {
            messageDal.Update(entity);
        }
    }
}
