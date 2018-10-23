using System.Collections.Generic;
using System.Linq;


namespace house.ViewModels.House
{
    //This class is matched up with the House\Index.cshtml view
    public class IndexViewModel
    {
        public IEnumerable<HomeViewModel> Houses { get; set; } = new List<HomeViewModel>();

        public class HomeViewModel
        {
            public int Id { get; set; }

            public string Zip { get; set; }

            public string Address { get; set; }


            public HomeViewModel(house.Data.House house)
            {
                Id = house.Id;
                Zip = house.Zip;
                Address = house.Address;
            }
        }

        public IndexViewModel(IEnumerable<house.Data.House> houses) => Houses = houses.Select(house => new HomeViewModel(house));
        
    }
}
