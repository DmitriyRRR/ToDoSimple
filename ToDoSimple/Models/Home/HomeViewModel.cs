using System.ComponentModel.DataAnnotations;

namespace ToDoSimple.Models.Home
{
    public class HomeViewModel
    {
        [Required]

        public string NoteName { get; set; }

        [Required]
        public string NoteDescription { get; set; }
        
        public IEnumerable<Note>? Notes { get; set; }
    }
}
