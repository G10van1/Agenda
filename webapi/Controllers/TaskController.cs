using Microsoft.AspNetCore.Mvc;
using Agenda.Context;
using Agenda.Infrastructure.Models;
using FluentValidation;
using Agenda.Infrastructure.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace Agenda.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AgendaContext _context;
        
        private readonly IValidator<Infrastructure.Models.Task> _taskValidator;

        public TaskController(AgendaContext context, IValidator<Infrastructure.Models.Task> taskValidator)
        {
            _context = context;
            _taskValidator = taskValidator;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _context.Tasks.Find(id);

            if (task == null) 
                return NotFound();

            return Ok(task);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var tasks = _context.Tasks.Where(x => x.Title == title);
            return Ok(tasks);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime date)
        {
            var task = _context.Tasks.Where(x => x.Date.Date == date.Date);
            return Ok(task);
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(EnumStatusTask status)
        {
            var tasks = _context.Tasks.Where(x => x.Status == status);
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Create(Infrastructure.Models.Task task)
        {
            ValidationResult result = _taskValidator.Validate(task);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Infrastructure.Models.Task task)
        {
            var taskDatabase = _context.Tasks.Find(id);

            if (taskDatabase == null)
                return NotFound();

            ValidationResult result = _taskValidator.Validate(task);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            taskDatabase.Title = task.Title;
            taskDatabase.Description = task.Description;            
            taskDatabase.Date = task.Date;
            taskDatabase.Status = task.Status;
            _context.Tasks.Update(taskDatabase);
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskDatabase = _context.Tasks.Find(id);

            if (taskDatabase == null)
                return NotFound();

            _context.Tasks.Remove(taskDatabase);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
