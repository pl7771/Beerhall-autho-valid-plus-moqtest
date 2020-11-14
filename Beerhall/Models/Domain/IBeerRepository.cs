using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Models.Domain
{
    public interface IBeerRepository
    {

        Brewer GetBy(int brewerId);
        IEnumerable<Beer> GetAll();
        void Add(Beer beer);
        void Delete(Beer beer);
        void SaveChanges();

    }
}
