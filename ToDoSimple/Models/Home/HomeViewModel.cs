using System.ComponentModel.DataAnnotations;
using ToDoSimple.Models.Pagination;

namespace ToDoSimple.Models.Home
{
    public class HomeViewModel
    {
        [Required]
        public string NoteName { get; set; }

        [Required]
        public string NoteDescription { get; set; }

        [Required]
        [Range(typeof(DateTime), "1/1/2011","1/1/2030")]
        public DateTime ExpireDate { get; set; }

        public PageViewModel Page { get; set; }
        public IEnumerable<Note>? Notes { get; set; }
    }
}
