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
        private string eventNameXpath;
        private string venueCityXpath;

        public PageScraper()
        {
            URL = "http://www.wegottickets.com/searchresults/page/1/all";

            SingleEventListing = "//*[@class='ListingOuter']";
            priceXpath = "//div[@class='searchResultsPrice']/strong";
            eventNameXpath = "//div[@class='ListingAct']/blockquote/h3/a";
            venueCityXpath = "//*[@class='venuetown']";
        }

        public void Scrape()
        {
           var htmlWeb = new HtmlWeb();
           var doc = htmlWeb.Load(URL);
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
                                   Price = WebUtility.HtmlDecode(listing.SelectSingleNode(priceXpath).InnerText),
                                   EventName = WebUtility.HtmlDecode(listing.SelectSingleNode(eventNameXpath).InnerText),
                                   VenueCity = WebUtility.HtmlDecode(listing.SelectSingleNode(venueCityXpath).InnerText)
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

       public String EventName { get; set; }

       public string VenueCity { get; set; }
    }
}
