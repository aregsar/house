using System;
using System.ComponentModel.DataAnnotations;
using house.ViewModels.Signup;

namespace house.ActionModels.Signup
{
    public class CreateActionModel
    {
        //when using form post tag helpers for databinding 
        //this property name must be the same property name as the viewmodel property
        public SignupActionModel SignupForm { get; set; }


        public class SignupActionModel
        {
            //when using form post tag helpers for databinding 
            //this property name must be the same property name as the nested viewmodel property

            [Required]
            //[MaxLength(50, ErrorMessage = "Maximumn 50 characters allowed")]
            public string Name { get; set; }

            [Required]
            //[EmailAddress]
            public string Email { get; set; }

            [Required]
            //[MinLength(8, ErrorMessage = "Password requires at least 8 characters")]
            public string Password { get; set; }

        }

        public NewViewModel MapToNewViewModel()
        {
            return new NewViewModel()
            {
                SignupForm = new NewViewModel.SignupViewModel()
                {
                    Name = SignupForm.Name,
                    Email = SignupForm.Email,
                    Password = SignupForm.Password,
                }
            };
        }
    }
}
