using System;
using System.Collections.Generic;
using System.Linq;

namespace Beerhall.Models.Domain
{
    public class Brewer
    {

        #region Fields
        private string _name;
        private int? _turnover;
        #endregion

        #region Properties
        public int BrewerId { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? DateEstablished { get; set; }
        public string Description { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A brewer musr have a name");
                if (value.Length > 50) throw new ArgumentException("Name of a brewer may not exceed 50 characters");

                _name = value;
            }
        }

        public int NrOfBeers => Beers.Count;
        public string Street { get; set; }
        public int? Turnover
        {
            get { return _turnover; }
            set
            {
                if (value.GetValueOrDefault() < 0) throw new ArgumentException("Value must be above 0");
                _turnover = value;
            }
        }

        public ICollection<Beer> Beers { get; }
        public Location Location { get; set; }
        #endregion


        #region Constructor
        public Brewer()
        {
            Beers = new HashSet<Beer>();
        }
        public Brewer(string name) : this()
        {
            Name = name;
        }

        public Brewer(string name, Location location, string street) : this(name)
        {
            Location = location;
            Street = street;
        }
        #endregion

        #region Methods
        public Beer AddBeer(string name, double? alcoholByVolume = null, decimal price = 0, string description = null)
        {
            if (name != null && Beers.Any(b => b.Name == name))
                throw new ArgumentException($"Brewer {Name} has already a beer by the name of {name}");

            Beer newBeer = new Beer(name)
            {
                AlcoholByVolume = alcoholByVolume,
                Description = description,
                Price = price
            };

            Beers.Add(newBeer);
            return newBeer;
        }

        public void DeleteBeer(Beer beer)
        {
            if (!Beers.Contains(beer))
                throw new ArgumentException($"{beer.Name} is not a {Name} beer");
            Beers.Remove(beer);
        }

        public Beer GetBy(int beerId)
        {
            return Beers.FirstOrDefault(e => e.BeerId == beerId);
        }

        public Beer GetBy(string name)
        {
            return Beers.FirstOrDefault(e => e.Name == name);
        }
        #endregion
    }
}
