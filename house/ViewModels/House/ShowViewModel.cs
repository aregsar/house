using System;

namespace house.ViewModels.House
{
    //This class is matched up with the House\Show.cshtml view
    public class ShowViewModel
    {
        public HomeViewModel House { get; set; }

        public class HomeViewModel
        {
            public int Id { get; set; }

            public string Zip { get; set; }

            public string Address { get; set; }

            public HomeViewModel(house.Data.House house)
            {
                if (house == null)
                    throw new ArgumentNullException(nameof(house));

                Id = house.Id;
                Zip = house.Zip;
                Address = house.Address;
            }
        }

        public ShowViewModel(house.Data.House house) => House = new HomeViewModel(house);
        
    }
}
