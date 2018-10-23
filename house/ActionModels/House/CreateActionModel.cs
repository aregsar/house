using System.ComponentModel.DataAnnotations;

namespace house.ActionModels.House
{
    //This class is matched up with the HouseController.Create action
    public class CreateActionModel
    {
        //when using form post tag helpers for databinding 
        //this property name must be the same property name as the viewmodel property
        public HomeActionModel House { get; set; }

        public house.ViewModels.House.NewViewModel MapToNewViewModel()
        {
            return new house.ViewModels.House.NewViewModel() {
                House = new house.ViewModels.House.NewViewModel.HomeViewModel(){
                    Zip = House.Zip,
                    Address = House.Address
                }
            };
        }

  
        public class HomeActionModel
        {
            //when using form post tag helpers for databinding 
            //this property name must be the same property name as the nested viewmodel property
            [Required]
            [RegularExpression(@"^[0-9]*$",ErrorMessage = "Invalid zip code")]
            [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "requires five digits")]
            public string Zip { get; set; }


            [Required]
            [MaxLength(200)]
            public string Address { get; set; }

            public house.Data.House MapToHouse()
            {
                return new house.Data.House() { Zip = Zip,
                                                Address = Address
                                              };
            }
                     
        }
    }
}
 