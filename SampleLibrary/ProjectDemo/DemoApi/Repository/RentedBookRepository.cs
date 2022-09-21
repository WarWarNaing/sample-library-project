using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Repository
{
    public class RentedBookRepository : RepositoryBase<RentedBook>
    {
        public RentedBookRepository()
        {
            this.entityContext = new LibraryDBContext();
        }

        public RentedBookRepository(LibraryDBContext context)
        {
            this.entityContext = context;
        }

        protected override RentedBook AddEntity(LibraryDBContext entityContext, RentedBook entity)
        {
            return entityContext.RentedBooks.Add(entity);
        }

        protected override RentedBook AddOrUpdateEntity(LibraryDBContext entityContext, RentedBook entity)
        {
            if (entity.Id == default(int))
            {
                return entityContext.RentedBooks.Add(entity);
            }
            else
            {

                return entityContext.RentedBooks.FirstOrDefault(e => e.Id == entity.Id);
            }
        }

        protected override RentedBook UpdateEntity(LibraryDBContext entityContext, RentedBook entity)
        {
            return entityContext.RentedBooks.FirstOrDefault(e => e.Id == entity.Id);

        }

        protected override IQueryable<RentedBook> GetEntities()
        {
            return entityContext.RentedBooks.AsQueryable();
        }

        protected override IQueryable<RentedBook> GetEntitiesWithoutTracking()
        {
            return entityContext.RentedBooks.AsNoTracking().AsQueryable();
        }

        protected override RentedBook GetEntity(LibraryDBContext entityContext, int id)
        {
            return entityContext.RentedBooks.FirstOrDefault(e => e.Id == id);
        }
    }
}