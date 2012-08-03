using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;


namespace PageScraper
{
    public class PageScraper
    {
        string url = "http://www.wegottickets.com/searchresults/page/1/all";

       public void Scrape()

       {
           HtmlWeb htmlWeb = new HtmlWeb();

           HtmlDocument doc = htmlWeb.Load(url);
           Document = doc;
       }

        public HtmlDocument Document { get; set; }

        public List<HtmlAttribute> GetAllVisbleEventListings()
        {
            List<string> matchedNodes = new List<string>();
            var content = Document.GetElementbyId("content");
            var contentDescendants =  content.Descendants();

            var inners = contentDescendants.SelectMany(x => x.ChildNodes);

            return
                inners.SelectMany(x => x.Attributes.AttributesWithName("class").Where(y => y.Value == "ListingOuter")).
                    ToList();
            //.Where(x=>x.Name.Equals("div")).ToList();

            //   divsWithClass.
            //      SelectMany(x => x.Attributes.AttributesWithName("class").Where(y=> y.Value == "ListingOuter")).ToList();





        }
    }
}
