using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoSimple.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
