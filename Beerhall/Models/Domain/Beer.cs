using System;

namespace Beerhall.Models.Domain
{
    public class Beer
    {

        private string _name;

        #region Properties
        public double? AlcoholByVolume { get; set; }
        public bool AlcoholKnown => AlcoholByVolume.HasValue;
        public int BeerId { get; set; }
        public string Description { get; set; }

        public int BrewerId { get; set; } 
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("A beer must have a name");
                _name = value;
            }
        }
        public decimal Price { get; set; }



        #endregion

        #region Constructor
        public Beer() { }

        public Beer(string name) : this()
        {
            Name = name;
        }
        #endregion


    }
}
