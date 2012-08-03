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
            Assert.NotNull(scraper.Document);
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
    }
}
