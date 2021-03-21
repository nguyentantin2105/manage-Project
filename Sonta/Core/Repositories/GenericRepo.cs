using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class GenericRepo<T> where T : class
    {
        public GenericRepo() { }

        public GenericRepo(DbContext context)
        {
            _entities = context;
        }


        private DbContext _entities;
        public DbContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }
        public virtual T GetByID(object id)
        {
            return _entities.Set<T>().Find(id);
        }

        public virtual T Insert(T entity)
        {
            _entities.Set<T>().Add(entity);
            return entity;
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = _entities.Set<T>().Find(id);
            Delete(entityToDelete);
        }
        public virtual void Delete(T entityToDelete)
        {
            if (_entities.Entry(entityToDelete).State == EntityState.Detached)
            {
                _entities.Set<T>().Attach(entityToDelete);
            }
            _entities.Set<T>().Remove(entityToDelete);
        }

        public virtual void Update(T entityToUpdate)
        {
            _entities.Set<T>().Attach(entityToUpdate);
            _entities.Entry(entityToUpdate).State = EntityState.Modified;
        }


        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _entities.Set<T>().Where(predicate);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _entities.Set<T>();
        }

        public virtual T Find(params object[] keyValues)
        {
            return _entities.Set<T>().Find(keyValues);
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }
    }
}
