using System.ComponentModel.DataAnnotations;

namespace ProductShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage ="Password don't match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string PasswordConfirm { get; set; }
    }
}
