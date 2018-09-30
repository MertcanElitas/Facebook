using Facebook.Domains.Concrete.Base;
using Facebook.Domains.Concrete.Ctx;
using Facebook.Domains.Repository.SingletonPattern;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domains.Repository.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity> : IEntityRepository<TEntity> where TEntity : class,IBaseEntity, new()
    {
        private DbContext context;

        public EfEntityRepositoryBase()
        {
            context = Singleton.CreateContex();
        }



        public void Add(TEntity entity)
        {
            var addEntity = context.Entry(entity);
            addEntity.State = EntityState.Added;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = GetBy(x => x.Id == id);

            var deleteEntity = context.Entry(entity);

            deleteEntity.State = EntityState.Deleted;
            context.SaveChanges();



        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(expression).ToList();
        }

        public TEntity GetBy(Expression<Func<TEntity, bool>> expression = null)
        {
            return context.Set<TEntity>().FirstOrDefault(expression);
        }

        public IQueryable<E> Query<E>() where E : class
        {
            return context.Set<E>();
        }

        public void Update(TEntity entity)
        {
            context.Set<TEntity>().AddOrUpdate(entity);
            context.SaveChanges();
        }
    }
}
