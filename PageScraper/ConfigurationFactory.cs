using System.Configuration;

namespace PageScraper
{
    public static class ConfigurationFactory
    {
        public static ScraperConfig GetConfiguration(string configLocation)
        {
            switch (configLocation)
            {
                case "configFile":
                    return GetFromConfigfile();
                default :
                    return GetDefaultSettings();
            }
        }

        private static ScraperConfig GetFromConfigfile()
        {
            var appSettings =
                ConfigurationManager.AppSettings;

            return new ScraperConfig(appSettings.Get("url"),
                                     appSettings.Get("singleEventListItemXpath"),
                                     appSettings.Get("priceXpath"),
                                     appSettings.Get("dateXPath"),
                                     appSettings.Get("venueCity"),
                                     appSettings.Get("venueName"),
                                     appSettings.Get("eventName")
                );
        }

        private static ScraperConfig GetDefaultSettings()
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
}