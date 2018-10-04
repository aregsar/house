using System.ComponentModel.DataAnnotations;

namespace house.ViewModels.House
{
    //This class is matched up with the House\New.cshtml view
    public class NewViewModel
    {
      
        public HomeViewModel House { get; set; }

        public class HomeViewModel
        {
            [Required]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid zip code")]
            [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "requires five digits")]
            public string Zip { get; set; }
        }

        public NewViewModel() => House = new HomeViewModel();

    }
}
