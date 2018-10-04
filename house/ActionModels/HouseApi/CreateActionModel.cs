using System.ComponentModel.DataAnnotations;

namespace house.ActionModels.HouseApi
{
    //This class is matched up with the HouseApiController.Create action
    public class CreateActionModel
    {
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid zip code")]
        [StringLength(maximumLength: 5, MinimumLength = 5, ErrorMessage = "requires five digits")]
        public string Zip { get; set; }


        public house.Data.House MapToHouse()
        {
            return new house.Data.House() { Zip = Zip };
        }
    }
}
