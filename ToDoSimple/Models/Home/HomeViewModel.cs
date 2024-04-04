using System.ComponentModel.DataAnnotations;

namespace ToDoSimple.Models.Home
{
    public class HomeViewModel
    {
        [Required(ErrorMessage = "Required data")]
        public string NoteName { get; set; }

        [Required(ErrorMessage = "Required data")]
        public string NoteDescription { get; set; }
        
        public IEnumerable<Note>? Notes { get; set; }
    }
}
