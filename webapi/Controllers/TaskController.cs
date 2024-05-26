using Microsoft.AspNetCore.Mvc;
using Agenda.Context;
using Agenda.Infrastructure.Models;

namespace Agenda.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AgendaContext _context;

        public TaskController(AgendaContext context)
        {
            _context = context;
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
            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            if (task.Date < DateTime.Now)
                return BadRequest(new { Erro = "A data da tarefa não pode anterior a data atual" });

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

            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da task não pode ser vazia" });

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
