using System;
using System.ComponentModel.DataAnnotations;
using house.ViewModels.Signin;

namespace house.ActionModels.Signin
{
    public class CreateActionModel
    {
        //when using form post tag helpers for databinding 
        //this property name must be the same property name as the viewmodel property
        public SigninActionModel SigninForm { get; set; }


        public class SigninActionModel
        {
            //when using form post tag helpers for databinding 
            //this property name must be the same property name as the nested viewmodel property

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }


        public NewViewModel MapToNewViewModel(string returnUrl)
        {
            return new NewViewModel()
            {
                ReturnUrl = returnUrl,
                SigninForm = new NewViewModel.SigninViewModel()
                {
                    Email = SigninForm.Email,
                    Password = String.Empty,
                }
            };
        }
    }
}
