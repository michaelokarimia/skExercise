namespace PageScraper
{
    public interface IEventShow
    {
        string Price { get; set; }

        string EventName { get; set; }

        string VenueCity { get; set; }

        string VenueName { get; set; }

        string DateTime { get; set; }
    }
}