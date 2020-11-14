using Beerhall.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Data.Repositories
{
    public class BeerRepository : IBeerRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Beer> _beers;

        public BeerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _beers = dbContext.Beers;
        }

        public void Add(Beer beer)
        {
            throw new NotImplementedException();
        }

        public void Delete(Beer beer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Beer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Brewer GetBy(int brewerId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
