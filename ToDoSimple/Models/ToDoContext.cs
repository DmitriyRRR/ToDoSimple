using Microsoft.EntityFrameworkCore;

namespace ToDoSimple.Models
{
    public class ToDoContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        { 
            Database.EnsureCreated(); 
        }

    }
}
