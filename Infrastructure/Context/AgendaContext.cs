using Microsoft.EntityFrameworkCore;
using Agenda.Infrastructure.Models;
using Task = Agenda.Infrastructure.Models.Task;

namespace Agenda.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options)
        {
            
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}