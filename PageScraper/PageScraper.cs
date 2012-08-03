using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace PageScraper
{
    public class PageScraper
    {
        private readonly string url;
        private readonly string singleEventListing;
        private readonly string priceXpath;
        private readonly string eventNameXpath;
        private readonly string venueCityXpath;
        private readonly string venueNameXpath;
        private readonly string eventDateXpath;

        public PageScraper()
        {
            url = "http://www.wegottickets.com/searchresults/page/1/all";

            singleEventListing = "//*[@class='ListingOuter']";
            priceXpath = "//div[@class='searchResultsPrice']/strong";
            eventNameXpath = "//div[@class='ListingAct']/blockquote/h3/a";
            venueCityXpath = "//*[@class='venuetown']";
            venueNameXpath = "//*[@class='venuename']";
            eventDateXpath = "//div[@class='ListingAct']/blockquote/p";
        }

        public void Scrape()
        {
           var htmlWeb = new HtmlWeb();
           var doc = htmlWeb.Load(url);
           Document = doc;
           HasPageBeenScraped = true;
        }

        private HtmlDocument Document { get; set; }

        public IList<IEventShow> EventListings
        {
            get
            {
                var eventListings = GetAllVisbleEventListings();

                var eventShowList = new List<IEventShow>();
                
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
                Document.DocumentNode.SelectNodes(singleEventListing).ToList();
        }   

        
    }
}
