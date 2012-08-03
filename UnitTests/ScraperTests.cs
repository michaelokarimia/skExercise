using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ScraperTests
    {
        PageScraper.PageScraper scraper;

        [SetUp]
        public void Setup()
        {
            scraper = new PageScraper.PageScraper();
            scraper.Scrape();

        }

        [Test]
        public void ScraperCanGetHtmlGivenAValidUrl()
        {
            Assert.NotNull(scraper.HasPageBeenScraped);
        }

        [Test]
        public void CanFindTenEventsPerPage()
        {
            Assert.AreEqual(10, scraper.EventListings.Count);
        }

        [Test]
        public void GetsPriceOfFirstEvent()
        {
            Assert.AreEqual("£10.00", scraper.EventListings[0].Price);
        }

        [Test]
        public void GetsNameOfFirstEvent()
        {
            Assert.AreEqual("4 TOP STAND UPS", scraper.EventListings[0].EventName);
        }

        [Test]
        public void GetVenueCityOfFirstEvent()
        {
            Assert.AreEqual("LONDON: ", scraper.EventListings[0].VenueCity);
        }

        [Test]
        public void GetVenueNameOfFirstEvent()
        {
            Assert.AreEqual("Headliners Comedy Club @ George IV", scraper.EventListings[0].VenueName);
        }

        [Test]
        public void GetEventTimeOfFirstEvent()
        {
            Assert.AreEqual("Bob Mills, Addy Borgh, Earl Okin, Simon Bligh", scraper.EventListings[0].DateTime);
        }
    }
}
