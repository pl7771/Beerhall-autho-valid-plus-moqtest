using Beerhall.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Beerhall.Models.ViewModels
{
    public class BrewerEditViewModel
    {
        [HiddenInput]
        public int BrewerId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 ")]
        public string Name { get; set; }


        [Display(Name = "Street", Prompt = "Street and house number")]
        public string Street { get; set; }



        [Display(Name = "Location")]
        public string PostalCode { get; set; }


        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "{0} may not be a negative value.")]
        public int? Turnover { get; set; }

        public BrewerEditViewModel() { }
        public BrewerEditViewModel(Brewer brewer) : this()
        {
            BrewerId = brewer.BrewerId;
            Name = brewer.Name;
            Street = brewer.Street;
            PostalCode = brewer.Location?.PostalCode;
            Turnover = brewer.Turnover;
        }

    }
}
