using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using System.Web;

namespace PageScraper
{
    public class PageScraper
    {
        private string URL;
        private    Func<HtmlNode, IEnumerable<HtmlAttribute>> EventListingsSelector;
        private readonly string SingleEventListing;
        private string priceXpath;

        public PageScraper()
        {
            EventListingsSelector = x =>
                                    x.ChildNodes.SelectMany(
                                        y => y.Attributes.AttributesWithName("class")
                                                 .Where(z => z.Value == "ListingOuter"));

            URL = "http://www.wegottickets.com/searchresults/page/1/all";

            SingleEventListing = "//*[@class='ListingOuter']";
            priceXpath = "//div[@class='searchResultsPrice']/strong";
        }

        public void Scrape()
        {
           var htmlWeb = new HtmlWeb();
           HtmlDocument doc = htmlWeb.Load(URL);
           Document = doc;
        }

        public HtmlDocument Document { get; set; }

        public IList<EventShow> EventListings
        {
            get
            {
                
                var eventListings = GetAllVisbleEventListings();

                EventShow show = new EventShow();

                foreach (HtmlNode eventListing in eventListings)
                {
                    show = new EventShow
                               {
                                   Price = WebUtility.HtmlDecode(eventListing.SelectSingleNode(priceXpath).InnerText)
                               };
                }

                var eventShowList = new List<EventShow>();
                eventShowList.Add(show);

                return eventShowList;

            }
        }

        public List<HtmlNode> GetAllVisbleEventListings()
        {
            return
                Document.DocumentNode.SelectNodes(SingleEventListing).ToList();
        }   

        
    }

   public class EventShow
    {

        public String Price { get; set; }
    }
}
