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
using NotebookAPI.Models;

namespace NoteBookAPI.Controllers
{
    public class ContactsController : ApiController
    {
        private PersonContext db = new PersonContext();

        // GET: api/Contacts
        public IQueryable<Contact> GetContacts()
        {

            return db.Contacts;
        }

        // GET: api/Contacts/5
        //[ResponseType(typeof(Contact))]
        //public IHttpActionResult GetContact(int id)
        //{
        //    Contact contact = db.Contacts.Find(id);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(contact);
        //}

        //TODO  Добавить реализацию  запрос контактов от персоны
        public List<Contact> GetContacts(int id)
        {
            //List<Contact> contacts = db.Contacts.Where(x => x.PersonId == id).
            //    Join(db.ContactTypes,p=>p.PersonId ,t=>t.Id ,(p,t)=>
            //    new Contact
            //        { Id=t.Id,
            //        ContactType  =p.ContactType,
            //        Value =p.Value   }).ToList();
            List<Contact> contacts = db.Contacts.Include("ContactType").Where(x=>x.PersonId ==id).ToList();
            return contacts;
        }
         

        // PUT: api/Contacts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContact(int id, Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            db.Entry(contact).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contacts
        [ResponseType(typeof(Contact))]
        public IHttpActionResult PostContact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contacts.Add(contact);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(Contact))]
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            db.SaveChanges();

            return Ok(contact);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactExists(int id)
        {
            return db.Contacts.Count(e => e.Id == id) > 0;
        }
    }
}