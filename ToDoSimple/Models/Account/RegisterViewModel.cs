using System.ComponentModel.DataAnnotations;

namespace ToDoSimple.Models.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Pls input some data")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Pls input some data")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Pls input some data")]
        [Compare("Password", ErrorMessage ="Password aren't match")]
        public string RepeatPassword { get; set; }
    }
}
