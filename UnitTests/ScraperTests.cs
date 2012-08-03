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
        public void ScraperCanGetDocumentWithValidUrl()
        {
            Assert.NotNull(scraper.Document);
        }

        [Test]
        public void CanfindNode()
        {
            Assert.Equals(10, scraper.GetAllEventNodes().Count);
        }
    }
}
