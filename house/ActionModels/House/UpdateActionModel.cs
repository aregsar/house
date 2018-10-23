using System.ComponentModel.DataAnnotations;
using house.ViewModels.House;

namespace house.ActionModels.House
{ 
    //This class is matched up with the HouseController.Update action
    public class UpdateActionModel
    {
        //when using form post tag helpers for databinding 
        //this property name must be the same property name as the viewmodel property
        public HomeActionModel House { get; set; }
          
        public EditViewModel MapToEditViewModel() => new EditViewModel(this);  

        public class HomeActionModel
        {

            public int Id { get; set; }

            [Required]
            [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid zip code")]
            [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "requires five digits")]
            public string Zip { get; set; }


            [Required]
            [MaxLength(200)]
            public string Address { get; set; }

            public house.Data.House MapToHouse()
            {
                return new house.Data.House()
                {
                    Id = Id,
                    Zip = Zip,
                    Address = Address
                };
            }
        }
    }
}
