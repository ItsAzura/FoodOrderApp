using System.ComponentModel.DataAnnotations;

namespace FoodOrderApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }
        [Display(Name="Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
