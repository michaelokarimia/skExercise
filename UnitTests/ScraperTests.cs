using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PageScraper;

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
        public void ScraperCanGetHTMLGivenAValidUrl()
        {
            Assert.NotNull(scraper.Document);
        }

        [Test]
        public void CanFindTenEventsPerPage()
        {
            Assert.AreEqual(10, scraper.GetAllVisbleEventListings().Count);
        }

        [Test]
        public void GetsPriceOfFirstEvent()
        {
            //Assert.AreEqual("£10.00", scraper.GetAllVisbleEventListings()[0].Price);
        }
    }
}
