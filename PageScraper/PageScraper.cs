using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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

        private void init(ScraperConfig config)
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
             init(ConfigurationFactory.getConfiguration("configFile"));
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

    public static class ConfigurationFactory
    {
        public static ScraperConfig getConfiguration(string configLocation)
        {
            switch (configLocation)
            {
                case "configFile":
                    return getFromConfigfile();
                    break;
                default :
                    return getDefaultSettings();
            }
        }

        private static ScraperConfig getFromConfigfile()
        {
           NameValueCollection appSettings =
           ConfigurationManager.AppSettings;


            var wotist = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var thingy = ConfigurationSettings.AppSettings["url"];
            return new ScraperConfig(appSettings.Get("url"),
                appSettings.Get("singleEventListItemXpath"),
                appSettings.Get("priceXpath"),
                appSettings.Get("dateXPath"),
                appSettings.Get("venueCity"),
                appSettings.Get("venueName"),
                appSettings.Get("eventName")
                );
        }

        private static ScraperConfig getDefaultSettings()
        {
            return new ScraperConfig
                (
                "http://www.wegottickets.com/searchresults/page/1/all",
                "//*[@class='ListingOuter']",
                "//div[@class='searchResultsPrice']/strong",
                "//div[@class='ListingAct']/blockquote/h3/a",
                "//*[@class='venuetown']",
                "//*[@class='venuename']",
                "//div[@class='ListingAct']/blockquote/p"
                );

        }
    }


        //url = "http://www.wegottickets.com/searchresults/page/1/all",
        //        singleEventListing = "//*[@class='ListingOuter']",
        //        priceXpath = "//div[@class='searchResultsPrice']/strong",
        //        eventNameXpath = "//div[@class='ListingAct']/blockquote/h3/a",
        //        venueCityXpath = "//*[@class='venuetown']",
        //        venueNameXpath = "//*[@class='venuename']",
        //        eventDateXpath = "//div[@class='ListingAct']/blockquote/p"

    public class ScraperConfig
    {
        public ScraperConfig(string url, string ListItemXpath, string priceXPath, string dateTimeXpath, string venueCityXpath, string venueNameXPath, string lineUpXpath)
        {
            Url = url;
            SingleEventListing = ListItemXpath;
            PriceXpath = priceXPath;
            EventDateXpath = lineUpXpath;
            VenueCityXpath = venueCityXpath;
            VenueNameXpath = venueNameXPath;
            EventNameXpath = dateTimeXpath;
        }

        public string Url { get; private set; }

        public string SingleEventListing { get; private set; }

        public string EventNameXpath { get; private set; }

        public string EventDateXpath { get; private set; }

        public string VenueNameXpath { get; private set; }

        public string VenueCityXpath { get; private set; }

        public string PriceXpath { get; private set; }
    }
}
