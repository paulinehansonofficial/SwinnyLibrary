﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web.Http.Description;
using SwinnyAPI.Models;

namespace SwinnyAPI.Controllers
{
    public class StudentsController : ApiController
    {
        private SwinnyDBEntities db = new SwinnyDBEntities();

        // GET: api/Students
        public IQueryable<StudentDTO> GetStudents()
        {
            return from s in db.Students
                   select new StudentDTO()
                   {
                       StudentID = s.StudentID,
                       FirstName = s.FirstName,
                       Surname = s.Surname,
                       Email = s.Email,
                       Mobile = s.Mobile
                   };
        }

        // GET: api/Students/5
        [ResponseType(typeof(StudentDTO))]
        public IHttpActionResult GetStudent(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            StudentDTO s = new StudentDTO()
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                Surname = student.Surname,
                Email = student.Email,
                Mobile = student.Mobile
            };

            return Ok(s);
        }

        // PUT: api/Students/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudent(string id, Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.StudentID)
            {
                return BadRequest();
            }

            db.Entry(student).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        [ResponseType(typeof(Student))]
        public async Task<IHttpActionResult> PostStudent(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Students.Add(student);
            await db.SaveChangesAsync();

            var dto = new StudentDTO()
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                Surname = student.Surname,
                Email = student.Email,
                Mobile = student.Mobile
            };

            return CreatedAtRoute("DefaultApi", new { id = student.StudentID }, dto);
        }

        // DELETE: api/Students/5
        [ResponseType(typeof(Student))]
        public IHttpActionResult DeleteStudent(string id)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            db.Students.Remove(student);
            db.SaveChanges();

            return Ok(student);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentExists(string id)
        {
            return db.Students.Count(e => e.StudentID == id) > 0;
        }
    }
}