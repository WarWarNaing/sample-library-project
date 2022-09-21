using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Repository
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository()
        {
            this.entityContext = new LibraryDBContext();
        }

        public CustomerRepository(LibraryDBContext context)
        {
            this.entityContext = context;
        }

        protected override Customer AddEntity(LibraryDBContext entityContext, Customer entity)
        {
            return entityContext.Customers.Add(entity);
        }

        protected override Customer AddOrUpdateEntity(LibraryDBContext entityContext, Customer entity)
        {
            if (entity.CustomerId == default(int))
            {
                return entityContext.Customers.Add(entity);
            }
            else
            {

                return entityContext.Customers.FirstOrDefault(e => e.CustomerId == entity.CustomerId);
            }
        }

        protected override Customer UpdateEntity(LibraryDBContext entityContext, Customer entity)
        {
            return entityContext.Customers.FirstOrDefault(e => e.CustomerId == entity.CustomerId);

        }

        protected override IQueryable<Customer> GetEntities()
        {
            return entityContext.Customers.AsQueryable();
        }

        protected override IQueryable<Customer> GetEntitiesWithoutTracking()
        {
            return entityContext.Customers.AsNoTracking().AsQueryable();
        }

        protected override Customer GetEntity(LibraryDBContext entityContext, int id)
        {
            return entityContext.Customers.FirstOrDefault(e => e.CustomerId == id);
        }
    }
}