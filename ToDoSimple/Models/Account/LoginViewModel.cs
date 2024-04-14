using System.ComponentModel.DataAnnotations;

namespace ToDoSimple.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "This fiel is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "This fiel is required")]
        public string Password { get; set; }
    }
}
