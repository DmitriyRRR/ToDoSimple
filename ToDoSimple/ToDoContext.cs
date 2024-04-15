using Microsoft.EntityFrameworkCore;
using ToDoSimple.Models;

namespace ToDoSimple
{
    public class ToDoContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
    }
}
