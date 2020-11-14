using Beerhall.Models.Domain;
using System;

namespace Beerhall.Models.ViewModels
{
    public class BeerEditViewModel
    {
        public int BeerId { get; set; }
        public string Name { get; set; }
        public double? Alchocol { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int BrewerId { get; set; }

        public BeerEditViewModel() { }

        public BeerEditViewModel(Beer beer): this()
        {
            BeerId = beer.BeerId;
            this.Name = beer.Name;
            this.Alchocol = beer.AlcoholByVolume;
            this.Description = beer.Description;
            this.Price = beer.Price;
            this.BrewerId = beer.BrewerId;
        }
    }
}
