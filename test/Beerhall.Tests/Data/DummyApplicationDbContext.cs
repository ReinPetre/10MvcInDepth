using System.Collections.Generic;
using System.Linq;
using Beerhall.Models.Domain;

namespace Beerhall.Tests.Data {
    public class DummyApplicationDbContext {
        public IEnumerable<Location> Locations { get; private set; }
        public IEnumerable<Brewer> Brewers { get; }
        public IEnumerable<Beer> Beers { get; private set; }
        public Brewer Bavik { get; }
        public Brewer Moortgat { get; }
        public Brewer DeLeeuw { get; }
        public Beer BavikPils { get; private set; }
        public Beer Wittekerke { get; private set; }
        public Location Bavikhove { get; }
        public Customer CustomerJan { get; }
        public Cart CartFilled { get; }

        public DummyApplicationDbContext() {
            int beerId = 1;
            int brewerId = 1;
            Bavikhove = new Location { Name = "Bavikhove", PostalCode = "8531" };
            Location puurs = new Location { Name = "Puurs", PostalCode = "2870" };
            Location leuven = new Location { Name = "Leuven", PostalCode = "3000" };

            Locations = new[] { Bavikhove, puurs, leuven };

            Bavik = new Brewer("Bavik", Bavikhove, "Rijksweg 33") { BrewerId = brewerId++ };
            Bavik.AddBeer("Bavik Pils", 5.2, 1M).BeerId = beerId++;
            Bavik.AddBeer("Wittekerke", 5.0, 2M).BeerId = beerId++;
            Bavik.Turnover = 20000000;
            BavikPils = Bavik.Beers.FirstOrDefault(b => b.Name == "Bavik Pils");
            Wittekerke = Bavik.Beers.FirstOrDefault(b => b.Name == "Wittekerke");

            Moortgat = new Brewer("Duvel Moortgat", puurs, "Breendonkdorp 28") { BrewerId = brewerId++ };
            Moortgat.AddBeer("Duvel", 8.5).BeerId = beerId;

            DeLeeuw = new Brewer("De Leeuw") { BrewerId = brewerId };
            DeLeeuw.Turnover = 50000;

            Brewers = new[] { DeLeeuw, Moortgat, Bavik };

            Beers = Brewers.SelectMany(b => b.Beers).OrderBy(be => be.Name);
            CartFilled = new Cart();
            CartFilled.AddLine(Wittekerke, 5);
            CustomerJan = new Customer {
                Email = "jan@hogent.be",
                FirstName = "Jan",
                Name = "De man",
                Location = Bavikhove,
                Street = "Bavikhovestraat"
            };
        }
    }
}
