﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Beerhall.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Beerhall.Models.ViewModels.CartViewModels {
    public class CheckOutViewModel {
        public SelectList Locations { get; private set; }
        public ShippingViewModel ShippingViewModel { get; set; }
        public CheckOutViewModel(IEnumerable<Location> locations, ShippingViewModel shippingViewModel) {
            Locations = new SelectList(locations,
                nameof(Location.PostalCode),
                nameof(Location.Name),
                shippingViewModel?.PostalCode);
            ShippingViewModel = shippingViewModel;
        }
    }
    public class ShippingViewModel {
        [DataType(DataType.Date)]
        [Display(Name = "Delivery date")]
        public DateTime? DeliveryDate { get; set; }
        [Display(Name = "Gift wrapping")]
        public bool Giftwrapping { get; set; }
        public string Street { get; set; }
        [Display(Name = "Location")]
        public string PostalCode { get; set; }
    }

}
