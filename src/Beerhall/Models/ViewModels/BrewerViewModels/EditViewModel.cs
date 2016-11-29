using System;
using System.ComponentModel.DataAnnotations;
using Beerhall.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Beerhall.Models.ViewModels.BrewerViewModels {
    public class EditViewModel {
        [HiddenInput]
        public int BrewerId {
            get; set;
        }
        [Required]
        [StringLength(50, ErrorMessage = "{0} may not contain more than 50 characters")]
        public string Name {
            get; set;
        }
        public string Street {
            get; set;
        }
        [Display(Name = "Postal code")]
        public string PostalCode {
            get; set;
        }
        [DataType(DataType.Currency)]
        [Range(0, int.MaxValue, ErrorMessage = "{0} may not be a negative value.")]
        public int? Turnover {
            get; set;
        }
        public string Description {
            get; set;
        }
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email address is not valid")]
        public string ContactEmail {
            get; set;
        }
        [Display(Name = "Date established")]
        [DataType(DataType.Date)]
        public DateTime? DateEstablished {
            get; set;
        }

        public EditViewModel() {
        }

        public EditViewModel(Brewer brewer) : this() {
            BrewerId = brewer.BrewerId;
            Name = brewer.Name;
            Street = brewer.Street;
            PostalCode = brewer.Location?.PostalCode;
            ContactEmail = brewer.ContactEmail;
            Description = brewer.Description;
            DateEstablished = brewer.DateEstablished;
            Turnover = brewer.Turnover;
        }
    }
}