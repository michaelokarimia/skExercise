using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;

namespace PageScraper
{
    public class PageScraper
    {
        private string url;
        private string singleEventListing;
        private string priceXpath;
        private string eventNameXpath;
        private string venueCityXpath;
        private string venueNameXpath;
        private string eventDateXpath;

        private void Initalise(ScraperConfig config)
        {
            url = config.Url;
            singleEventListing = config.SingleEventListing;
            eventNameXpath = config.EventNameXpath;
            eventDateXpath = config.EventDateXpath;
            venueNameXpath = config.VenueNameXpath;
            venueCityXpath = config.VenueCityXpath;
            priceXpath = config.PriceXpath;

        }

        public PageScraper()
        {
             Initalise(ConfigurationFactory.GetConfiguration("configFile"));
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
