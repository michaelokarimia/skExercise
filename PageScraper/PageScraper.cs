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
         
        public PageScraper()
        {
            EventListingsSelector = x =>
                       x.ChildNodes.SelectMany(
                           y => y.Attributes.AttributesWithName("class")
                                    .Where(z => z.Value == "ListingOuter"));

            URL = "http://www.wegottickets.com/searchresults/page/1/all";
        }

        public void Scrape()
        {
           var htmlWeb = new HtmlWeb();
           HtmlDocument doc = htmlWeb.Load(URL);
           Document = doc;
        }

        public HtmlDocument Document { get; set; }

        public List<HtmlAttribute> GetAllVisbleEventListings()
        {
           
            return
                Document.GetElementbyId("content").Descendants().
                    SelectMany(
                        EventListingsSelector)
                            .ToList();
        }
    }
}
