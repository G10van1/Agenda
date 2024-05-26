using Microsoft.AspNetCore.Mvc;
using Agenda.Context;
using Agenda.Infrastructure.Models;

namespace Agenda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContactController(AgendaContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Contact = _context.Contacts.Find(id);

            if (Contact == null) 
                return NotFound();

            return Ok(Contact);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Contacts = _context.Contacts.ToList();
            return Ok(Contacts);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByTitle(string name)
        {
            var Contacts = _context.Contacts.Where(x => x.Name == name);
            return Ok(Contacts);
        }

        [HttpPost]
        public IActionResult Create(Infrastructure.Models.Contact Contact)
        {
            _context.Contacts.Add(Contact);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = Contact.Id }, Contact);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Infrastructure.Models.Contact Contact)
        {
            var ContactDatabase = _context.Contacts.Find(id);

            if (ContactDatabase == null)
                return NotFound();

            ContactDatabase.Name = Contact.Name;
            ContactDatabase.Phone = Contact.Phone;            
            ContactDatabase.Email = Contact.Email;
            _context.Contacts.Update(ContactDatabase);
            _context.SaveChanges();
            return Ok(Contact);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ContactDatabase = _context.Contacts.Find(id);

            if (ContactDatabase == null)
                return NotFound();

            _context.Contacts.Remove(ContactDatabase);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
