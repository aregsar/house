using System.ComponentModel.DataAnnotations;

namespace house.ViewModels.Signup
{
    public class NewViewModel
    {       
        public SignupViewModel SignupForm { get; set; }

        public class SignupViewModel
        {
            [Required]
            [MaxLength(50,ErrorMessage = "Maximumn 50 characters allowed")]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required][MinLength(8,ErrorMessage = "Password requires at least 8 characters")]
            public string Password { get; set; }
        }

        public NewViewModel() => SignupForm = new SignupViewModel();
    }
}
