using Microsoft.AspNetCore.Mvc;
using Agenda.Context;
using Agenda.Infrastructure.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace Agenda.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AgendaContext _context;

        private readonly IValidator<User> _userValidator;

        public UserController(AgendaContext context, IValidator<User> userValidator)
        {
            _context = context;
            _userValidator = userValidator;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var User = _context.Users.Find(id);

            if (User == null)
                return NotFound();

            return Ok(User);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Users = _context.Users.ToList();
            return Ok(Users);
        }

        [HttpGet("GetByName")]
        public IActionResult GetByTitle(string name)
        {
            var Users = _context.Users.Where(x => x.Name == name);
            return Ok(Users);
        }

        [HttpPost]
        public IActionResult Create(Infrastructure.Models.User User)
        {
            ValidationResult result = _userValidator.Validate(User);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            _context.Users.Add(User);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = User.Id }, User);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Infrastructure.Models.User User)
        {
            ValidationResult result = _userValidator.Validate(User);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var UserDatabase = _context.Users.Find(id);

            if (UserDatabase == null)
                return NotFound();

            UserDatabase.Name = User.Name;
            UserDatabase.UserName = User.UserName;
            UserDatabase.Password = User.Password;
            UserDatabase.Email = User.Email;
            _context.Users.Update(UserDatabase);
            _context.SaveChanges();
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var UserDatabase = _context.Users.Find(id);

            if (UserDatabase == null)
                return NotFound();

            _context.Users.Remove(UserDatabase);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
