using System.ComponentModel.DataAnnotations;

namespace CoreMVC.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }

        public bool Remember { get; set; }
    }

}
