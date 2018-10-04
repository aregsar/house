using System.ComponentModel.DataAnnotations;

namespace house.ViewModels.House
{
    public class EditViewModel
    {
        public HomeViewModel House { get; set; }

        public class HomeViewModel
        {
            public int Id { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid zip code")]
            [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "requires five digits")]
            public string Zip { get; set; }
           
        }


        public EditViewModel(house.ActionModels.House.UpdateActionModel updateActionModel)
        {
            House = new HomeViewModel()
            {
                Id = updateActionModel.House.Id,
                Zip = updateActionModel.House.Zip
            };
        }

        public EditViewModel(house.Data.House house)
        {
            House = new HomeViewModel(){
                Id = house.Id,
                Zip = house.Zip
            };
        }
    
    }
}
