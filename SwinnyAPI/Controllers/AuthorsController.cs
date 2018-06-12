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
    public class AuthorsController : ApiController
    {
        private SwinnyDBEntities db = new SwinnyDBEntities();

        // GET: api/Authors
        public IQueryable<AuthorDTO> GetAuthors()
        {
            return from a in db.Authors
                   select new AuthorDTO()
                   {
                       AuthorID = a.AuthorID,
                       AuthorName = a.AuthorName,
                       AuthorSurname = a.AuthorSurname,
                       TFN = a.TFN
                   };
        }

        // GET: api/Authors/5
        [ResponseType(typeof(AuthorDTO))]
        public IHttpActionResult GetAuthor(int id)
        {
            Author author = db.Authors.Find(id);
            
            if (author == null)
            {
                return NotFound();
            }

            AuthorDTO a = new AuthorDTO()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname,
                TFN = author.TFN
            };
            return Ok(a);

        }

        // PUT: api/Authors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.AuthorID)
            {
                return BadRequest();
            }

            db.Entry(author).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        [ResponseType(typeof(Author))]
        public async Task<IHttpActionResult> PostAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Authors.Add(author);
            await db.SaveChangesAsync();

            var dto = new AuthorDTO()
            {
                AuthorID = author.AuthorID,
                AuthorName = author.AuthorName,
                AuthorSurname = author.AuthorSurname,
                TFN = author.TFN
            };            

            return CreatedAtRoute("DefaultApi", new { id = author.AuthorID }, dto);
        }

        // DELETE: api/Authors/5
        [ResponseType(typeof(Author))]
        public IHttpActionResult DeleteAuthor(int id)
        {
            Author author = db.Authors.Find(id);
            if (author == null)
            {
                return NotFound();
            }

            db.Authors.Remove(author);
            db.SaveChanges();

            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AuthorExists(int id)
        {
            return db.Authors.Count(e => e.AuthorID == id) > 0;
        }
    }
}