using Microsoft.EntityFrameworkCore.Internal;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {

        private MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }
        
        // Method responsible for returning all people,
        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        // Method responsible for returning one book by ID
        public Book FindByID(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }

        // Method responsible to crete one new book
        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }

        // Method responsible for updating one book
        public Book Update(Book book)
        {
            // We check if the book exists in the database
            // If it doesn't exist we return an empty book instance
            if (!Exists(book.Id)) return null;

            // Get the current status of the record in the database
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    // set changes and save
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return book;
        }

        // Method responsible for deleting a book from an ID
        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public bool Exists(long id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }
    }
}
