using Beerhall.Models.Domain;
using System;
using Xunit;


namespace Beerhall.Tests.Models.Domain {
    public class BeerTest {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(" \t \n \r \t   ")]
        public void NewBeer_WrongName_ThrowsException(string name) {
            Assert.Throws<ArgumentException>(() => new Beer(name));
        }

        [Fact]
        public void AlcoholKnown_AlcoholByVolumeSet_ReturnsTrue() {
            double alcoholByVolume = 8.5D;
            Beer beer = new Beer("New beer") { AlcoholByVolume = alcoholByVolume };
            Assert.True(beer.AlcoholKnown);
        }

        [Fact]
        public void AlcoholKnown_AlcoholByVolumeNotSet_ReturnsFalse() {
            Beer beer = new Beer("New beer");
            Assert.False(beer.AlcoholKnown);
        }
    }
}