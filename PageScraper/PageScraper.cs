using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;


namespace PageScraper
{
    public class PageScraper
    {
        private string URL;
        private    Func<HtmlNode, IEnumerable<HtmlAttribute>> EventListingsSelector;
        private readonly string SingleEventListing;

        public PageScraper()
        {
            EventListingsSelector = x =>
                                    x.ChildNodes.SelectMany(
                                        y => y.Attributes.AttributesWithName("class")
                                                 .Where(z => z.Value == "ListingOuter"));

            URL = "http://www.wegottickets.com/searchresults/page/1/all";

            SingleEventListing = "//*[@class='ListingOuter']";
        }

        public void Scrape()
        {
           var htmlWeb = new HtmlWeb();
           HtmlDocument doc = htmlWeb.Load(URL);
           Document = doc;
        }

        public HtmlDocument Document { get; set; }

        public List<HtmlNode> GetAllVisbleEventListings()
        {
            return
                Document.DocumentNode.SelectNodes(SingleEventListing).ToList();
        }

        
    }
}
