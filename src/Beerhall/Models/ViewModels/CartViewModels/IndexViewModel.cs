using Beerhall.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Beerhall.Models.ViewModels.CartViewModels {

    public class IndexViewModel {
        [HiddenInput]
        public int BeerId { get; private set; }

        public int Quantity { get; private set; }

        public string Beer { get; private set; }

        public decimal Price { get; private set; }

        public decimal SubTotal { get; private set; }

        public IndexViewModel(CartLine cartLine) {
            BeerId = cartLine.Product.BeerId;
            Quantity = cartLine.Quantity;
            Beer = cartLine.Product.Name;
            Price = cartLine.Product.Price;
            SubTotal = cartLine.Total;
        }
    }
}
