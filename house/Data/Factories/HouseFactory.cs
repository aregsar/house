using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace house.Data.Factories
{
    public class HouseFactory
    {
        public static void CreateHouses(IServiceProvider serviceProvider)
        {
            using (var context = new HouseDbContext(
                 serviceProvider.GetRequiredService<DbContextOptions<HouseDbContext>>()))
            {
               
                if (!context.House.Any())
                {

                    context.House.AddRange(
                         new House
                         {
                             Zip = "91202",

                         },

                         new House
                         {
                             Zip = "91203",
  
                         },

                         new House
                         {
                             Zip = "91204",

                         }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
