using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public class Repository<TEntity>  where TEntity : class
    {
        protected readonly DbContext Context;
        protected DbSet<TEntity> DbSetEntity;
        public Repository(DbContext context)
        {
            Context = context;
            DbSetEntity = Context.Set<TEntity>();
        }
        //public void Add() {

        //    }
        //public void Save(TEntity entity) {
        //    DbSetEntity.Update();
        //    }
        public void Add(TEntity entity)
        {
            DbSetEntity.Add(entity);
            Context.SaveChanges();
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSetEntity.AddRange(entities);
        }

        public void Edit(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSetEntity.Where(predicate);
            //throw new NotImplementedException();
        }

        public TEntity Get(int id)
        {
            return DbSetEntity.Find(id);
        }

        public TEntity GetByIdNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            var item = DbSetEntity.AsNoTracking().Where(predicate).FirstOrDefault();
            Context.SaveChanges();
            return item;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return DbSetEntity.ToList();
        }
        public IEnumerable<TEntity> Getpage()
        {
            return DbSetEntity.AsNoTracking().ToList();
        }

        public void Remove(TEntity entity)
        {
            DbSetEntity.Remove(entity);
            Context.SaveChanges();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSetEntity.RemoveRange(entities);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSetEntity.SingleOrDefault(predicate);
        }
    }
}
