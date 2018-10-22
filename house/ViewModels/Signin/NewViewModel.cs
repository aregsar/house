using System.ComponentModel.DataAnnotations;

namespace house.ViewModels.Signin
{

    public class NewViewModel
    {
        public SigninViewModel SigninForm { get; set; }

        public class SigninViewModel
        {
            [Required]
            //[EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public NewViewModel() => SigninForm = new SigninViewModel();
    }


}
