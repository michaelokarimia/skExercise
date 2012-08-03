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
        private readonly string SingleEventListing;
        private string priceXpath;

        public PageScraper()
        {
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

                var eventShowList = new List<EventShow>();
                
                foreach (HtmlNode listing in eventListings)
                {
                    eventShowList.Add(new EventShow
                               {
                                   Price = WebUtility.HtmlDecode(listing.SelectSingleNode(priceXpath).InnerText)
                               });
                }


                return eventShowList;

            }
        }

        private List<HtmlNode> GetAllVisbleEventListings()
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
