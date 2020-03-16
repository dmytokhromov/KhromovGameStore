using System.ComponentModel.DataAnnotations;

namespace KhromovGameStore.Models
{
    public class LoginView
    {
        [Required(ErrorMessage = "Required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
}
