using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Beerhall.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Beerhall.Data {
    public class BeerhallDataInitializer {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public BeerhallDataInitializer(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager) {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData() {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated()) {
                Location bavikhove = new Location { Name = "Bavikhove", PostalCode = "8531" };
                Location roeselare = new Location { Name = "Roeselare", PostalCode = "8800" };
                Location puurs = new Location { Name = "Puurs", PostalCode = "2870" };
                Location leuven = new Location { Name = "Leuven", PostalCode = "3000" };
                Location oudenaarde = new Location { Name = "Oudenaarde", PostalCode = "9700" };
                Location affligem = new Location { Name = "Affligem", PostalCode = "1790" };
                Location[] locations = { bavikhove, roeselare, puurs, leuven, oudenaarde, affligem };
                _dbContext.Locations.AddRange(locations);
                _dbContext.SaveChanges();

                Brewer bavik = new Brewer("Bavik", bavikhove, "Rijksweg 33");
                _dbContext.Brewers.Add(bavik);
                bavik.AddBeer("Bavik Pils", 5.2, 0.80M,
                    "De Bavik Premium Pils wordt gebrouwen met de beste mout en hop en verdient koel geschonken te worden.");
                bavik.AddBeer("Wittekerke", 5.0, 0.90M, "Wittekerke 1/4");
                bavik.AddBeer("Wittekerke Speciale", 5.8, 2.35M);
                bavik.AddBeer("Wittekerke Rosé", 4.3, 1.79M);
                bavik.AddBeer("Ezel Wit", 5.8, 1.79M);
                bavik.AddBeer("Ezel Bruin", 6.5, 1.69M);
                bavik.Turnover = 20000000;
                bavik.DateEstablished = new DateTime(1990, 12, 26);
                bavik.ContactEmail = "info@bavik.be";
                bavik.Description =
                    "Brouwerij De Brabandere kan terugblikken op een rijke geschiedenis, maar kijkt met evenveel vertrouwen naar de toekomst. De droom die stichter Adolphe De Brabandere op het eind van de negentiende eeuw koestert wanneer hij in Bavikhove de fundamenten legt van zijn brouwerij, is realiteit geworden in de succesvolle onderneming van vandaag.Met een rijk assortiment bieren dat gesmaakt wordt door kenners tot ver buiten onze landsgrenzen.Brouwen was, is, en blijft een kunst bij Brouwerij De Brabandere. Beschouw onze talrijke karaktervolle bieren gerust als erfgoed: gemaakt met traditioneel vakmanschap, met authentieke ingrediënten en… veel liefde. Het creëren van een unieke smaaksensatie om te delen met vrienden, dat drijft ons dag in dag uit.  Zonder compromissen.";

                Brewer palm = new Brewer("Palm Breweries");
                _dbContext.Brewers.Add(palm);
                palm.AddBeer("Estimanet", 5.2, 1.39M);
                palm.AddBeer("Steenbrugge Blond", 6.5, 1.80M);
                palm.AddBeer("Palm", 5.4, 0.90M);
                palm.AddBeer("Dobbel Palm", 6.0, 1.15M);
                palm.Turnover = 500000;

                Brewer duvelMoortgat = new Brewer("Duvel Moortgat", puurs, "Breendonkdorp 28");
                _dbContext.Brewers.Add(duvelMoortgat);
                duvelMoortgat.AddBeer("Duvel", 8.5, 1.78M);
                duvelMoortgat.AddBeer("Vedett", price: 1.79M);
                duvelMoortgat.AddBeer("Maredsous", price: 1.69M);
                duvelMoortgat.AddBeer("Liefmans Kriekbier", price: 2.35M);
                duvelMoortgat.AddBeer("La Chouffe", 8.0, 1.69M);
                duvelMoortgat.AddBeer("De Koninck", 5.0, 0.79M);

                Brewer inBev = new Brewer("InBev", leuven, "Brouwerijplein 1");
                _dbContext.Brewers.Add(inBev);
                inBev.AddBeer("Jupiler", price: 1.19M);
                inBev.AddBeer("Stella Artois", price: 1.19M);
                inBev.AddBeer("Leffe", price: 1.89M);
                inBev.AddBeer("Belle-Vue", price: 1.25M);
                inBev.AddBeer("Hoegaarden", price: 0.89M);

                Brewer roman = new Brewer("Roman", oudenaarde, "Hauwaart 105");
                _dbContext.Brewers.Add(roman);
                roman.AddBeer("Sloeber", 7.5, 1.20M);
                roman.AddBeer("Black Hole", 5.6, 1.68M);
                roman.AddBeer("Ename", 6.5, 2.19M);
                roman.AddBeer("Romy Pils", 5.1, 0.65M);

                Brewer deGraal = new Brewer("De Graal");
                _dbContext.Brewers.Add(deGraal);

                Brewer deLeeuw = new Brewer("De Leeuw");
                _dbContext.Brewers.Add(deLeeuw);

                _dbContext.SaveChanges();

                await InitializeUsers();

            }
        }

        private async Task InitializeUsers() {
            string eMailAddress = "beermaster@hogent.be";
            ApplicationUser user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "admin"));

            eMailAddress = "jan@hogent.be";
            user = new ApplicationUser { UserName = eMailAddress, Email = eMailAddress };
            await _userManager.CreateAsync(user, "P@ssword1");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "customer"));
        }
    }
}

