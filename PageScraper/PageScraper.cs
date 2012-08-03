using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace PageScraper
{
    public class PageScraper
    {
        private string URL;
        private readonly string SingleEventListing;
        private string priceXpath;
        private string eventNameXpath;
        private string venueCityXpath;
        private string venueNameXpath;
        private string eventDateXpath;

        public PageScraper()
        {
            URL = "http://www.wegottickets.com/searchresults/page/1/all";

            SingleEventListing = "//*[@class='ListingOuter']";
            priceXpath = "//div[@class='searchResultsPrice']/strong";
            eventNameXpath = "//div[@class='ListingAct']/blockquote/h3/a";
            venueCityXpath = "//*[@class='venuetown']";
            venueNameXpath = "//*[@class='venuename']";
            eventDateXpath = "//div[@class='ListingAct']/blockquote/p";
        }

        public void Scrape()
        {
           var htmlWeb = new HtmlWeb();
           var doc = htmlWeb.Load(URL);
           Document = doc;
           HasPageBeenScraped = true;
        }

        private HtmlDocument Document { get; set; }

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
                                   VenueCity = WebUtility.HtmlDecode(listing.SelectSingleNode(venueCityXpath).InnerText),
                                   VenueName = WebUtility.HtmlDecode(listing.SelectSingleNode(venueNameXpath).InnerText),
                                   DateTime = WebUtility.HtmlDecode(listing.SelectSingleNode(eventDateXpath).LastChild.InnerText)
                               });
                }


                return eventShowList;

            }
        }

        public bool HasPageBeenScraped { get; private set; }

        private IEnumerable<HtmlNode> GetAllVisbleEventListings()
        {
            return
                Document.DocumentNode.SelectNodes(SingleEventListing).ToList();
        }   

        
    }

   public class EventShow
    {

       public string Price { get; set; }

       public string EventName { get; set; }

       public string VenueCity { get; set; }

       public string VenueName { get; set; }

       public string DateTime { get; set; }
    }
}
