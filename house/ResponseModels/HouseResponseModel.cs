using System.Collections.Generic;
using System.Linq;

namespace house.ResponseModels
{

    public class HouseResponseModel
    {
        public int Id { get; set; }
        public string Zip { get; set; }

        public HouseResponseModel(house.Data.House house)
        {
            Id = house.Id;
            Zip = house.Zip;
        }
    }
}
