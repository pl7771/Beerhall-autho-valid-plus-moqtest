using Beerhall.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Beerhall.Data.Mapping
{
    class LocationRepository : ILocationRepository
    {

        private readonly DbSet<Location> _locations;

        public LocationRepository(ApplicationDbContext dbContext)
        {
            _locations = dbContext.Locations;
        }

        public IEnumerable<Location> GetAll()
        {
            return _locations.ToList();
        }

        public Location GetBy(string postalCode)
        {
            return _locations.SingleOrDefault(e => e.PostalCode == postalCode);
        }
    }
}
