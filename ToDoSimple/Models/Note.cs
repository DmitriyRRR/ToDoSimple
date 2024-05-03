using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoSimple.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required data")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Required data")]
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public int UserId { get; set; }
        //public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime ExpireDate { get; set; }
        //public DateTime? CreatedTimestamp { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
