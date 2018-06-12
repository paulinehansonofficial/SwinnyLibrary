using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Threading.Tasks;
using SwinnyAPI.Models;

namespace SwinnyAPI.Controllers
{
    public class BooksController : ApiController
    {
        private SwinnyDBEntities db = new SwinnyDBEntities();

        // GET: api/Books
        public IQueryable<BookDTO> GetBooks()
        {
            return from b in db.Books
                   select new BookDTO()
                   {
                       Title = b.Title,
                       ISBN = b.ISBN,
                       YearPublished = b.YearPublished,
                       AuthorName = b.Author.AuthorName,
                       AuthorSurname = b.Author.AuthorSurname
                   };
        }

        // GET: api/Books/5
        [ResponseType(typeof(BookDTO))]
        public IHttpActionResult GetBook(string id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            BookDTO b = new BookDTO()
            {
                Title = book.Title,
                ISBN = book.ISBN,
                YearPublished = book.YearPublished,
                AuthorName = book.Author.AuthorName,
                AuthorSurname = book.Author.AuthorSurname
            };

            return Ok(b);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(string id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.ISBN)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();

            var dto = new BookDTO()
            {
                Title = book.Title,
                ISBN = book.ISBN,
                YearPublished = book.YearPublished,
                AuthorName = book.Author.AuthorName,
                AuthorSurname = book.Author.AuthorSurname
                
            };

            return CreatedAtRoute("DefaultApi", new { id = book.ISBN }, dto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(string id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(string id)
        {
            return db.Books.Count(e => e.ISBN == id) > 0;
        }
    }
}