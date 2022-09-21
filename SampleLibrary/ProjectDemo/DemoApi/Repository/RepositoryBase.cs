using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL.Model;

namespace DemoApi.Repository
{
    public abstract class RepositoryBase<T> where T : class, new()
    {
        public LibraryDBContext entityContext;
        protected abstract T AddEntity(LibraryDBContext entityContext, T entity);
        protected abstract T AddOrUpdateEntity(LibraryDBContext entityContext, T entity);
        protected abstract IQueryable<T> GetEntities();
        protected abstract IQueryable<T> GetEntitiesWithoutTracking();
        protected abstract T GetEntity(LibraryDBContext entityContext, int id);
        protected abstract T UpdateEntity(LibraryDBContext entityContext, T entity);


        public int Add(T entity)
        {
            int result = 0;
            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                var obj = AddEntity(entityContext, entity);
                result = entityContext.SaveChanges();

            }
            return result;
        }
        public T AddWithGetObj(T entity)
        {

            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                var obj = AddEntity(entityContext, entity);
                if (entityContext.SaveChanges() > 0)
                {
                    return obj;
                }
            }
            return null;
        }
        public int Remove(T entity)
        {
            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Remove(int id)
        {
            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                T entity = GetEntity(entityContext, id);
                entityContext.Entry<T>(entity).State = EntityState.Deleted;
                return entityContext.SaveChanges();
            }
        }

        public int Update(T entity)
        {
            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                return entityContext.SaveChanges();
            }
        }
        public T UpdatewithObj(T entity)
        {
            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                entityContext.Entry<T>(entity).State = EntityState.Modified;
                if (entityContext.SaveChanges() > 0)
                {
                    return entity;
                }
                return null;
            }
        }

        public IQueryable<T> Get()
        {
            return GetEntities();
        }
        public IQueryable<T> GetWithoutTracking()
        {
            return GetEntitiesWithoutTracking();
        }

        public T Get(int id)
        {
            using (LibraryDBContext entityContext = new LibraryDBContext())
            {
                return GetEntity(entityContext, id);
            }
        }


    }
}