using Beerhall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Models.Domain
{
    public interface IBrewerRepository
    {
        Brewer GetBy(int brewerId);
        IEnumerable<Brewer> GetAll();
        void Add(Brewer brewer);
        void Delete(Brewer brewer);
        void SaveChanges();
        public Brewer GetByWithBeers(int brewerId);

        public IEnumerable<Brewer> GetAllWithBeers();
    }
}
