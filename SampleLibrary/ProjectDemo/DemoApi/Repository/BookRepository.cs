using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoApi.Repository
{
    public class BookRepository : RepositoryBase<Book>
    {
        public BookRepository()
        {
            this.entityContext = new LibraryDBContext();
        }

        public BookRepository(LibraryDBContext context)
        {
            this.entityContext = context;
        }

        protected override Book AddEntity(LibraryDBContext entityContext, Book entity)
        {
            return entityContext.Books.Add(entity);
        }

        protected override Book AddOrUpdateEntity(LibraryDBContext entityContext, Book entity)
        {
            if (entity.BookId == default(int))
            {
                return entityContext.Books.Add(entity);
            }
            else
            {
                return entityContext.Books.FirstOrDefault(e => e.BookId == entity.BookId);
            }
        }

        protected override Book UpdateEntity(LibraryDBContext entityContext, Book entity)
        {
            return entityContext.Books.FirstOrDefault(e => e.BookId == entity.BookId);
        }

        protected override IQueryable<Book> GetEntities()
        {
            return entityContext.Books.AsQueryable();
        }

        protected override IQueryable<Book> GetEntitiesWithoutTracking()
        {
            return entityContext.Books.AsNoTracking().AsQueryable();
        }

        protected override Book GetEntity(LibraryDBContext entityContext, int id)
        {
            return entityContext.Books.FirstOrDefault(e => e.BookId == id);
        }
    }
}