using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace house.Data
{
    public class HouseRepository
    {

        private readonly HouseDbContext _db;
        private readonly ILogger<HouseRepository> _logger;

        public HouseRepository(HouseDbContext db
                               , ILogger<HouseRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IEnumerable<House> Houses()
        {
            return _db.House.ToList();
        }

        public House House(int id)
        {
            return _db.House
                      .Where(i => i.Id == id)
                      .FirstOrDefault();
        }

        public void Add(House house)
        {
            _db.Add(house);
            _db.SaveChanges();
        }

        public void Update(House house)
        {
            _db.Update(house);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Remove(new House{Id =id});
            _db.SaveChanges();
        }


        public void Detach(House trackedhouse)
        {
            _db.Entry(trackedhouse).State = EntityState.Detached;
        }

      
    }
}
