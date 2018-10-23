using System.ComponentModel.DataAnnotations;

namespace house.ViewModels.Signin
{

    public class NewViewModel
    {
        public string ReturnUrl { get; set; } 

        public SigninViewModel SigninForm { get; set; }

        public class SigninViewModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public NewViewModel() => SigninForm = new SigninViewModel();

        public NewViewModel(string returnUrlPath)
        {
            SigninForm = new SigninViewModel();

            ReturnUrl = returnUrlPath;
        }
    }


}
