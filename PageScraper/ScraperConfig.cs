namespace PageScraper
{
    public class ScraperConfig
    {
        public ScraperConfig(string url, string listItemXpath, string priceXPath, string eventName, string venueCityXpath, string venueNameXPath, string lineUpXpath)
        {
            Url = url;
            SingleEventListing = listItemXpath;
            PriceXpath = priceXPath;
            EventDateXpath = lineUpXpath;
            VenueCityXpath = venueCityXpath;
            VenueNameXpath = venueNameXPath;
            EventNameXpath = eventName;
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