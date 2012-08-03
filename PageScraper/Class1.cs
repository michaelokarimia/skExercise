using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;


namespace PageScraper
{
    public class Class1
    {
        string url = "http://www.wegottickets.com/searchresults/page/1/all";

       public void Scrape()

       {
           HtmlWeb htmlWeb = new HtmlWeb();

           HtmlDocument doc = htmlWeb.Load(url);
           Document = doc;
       }

        public HtmlDocument Document { get; set; }

        public IList<String> GetAllEventNodes()
        {
            List<string> matchedNodes = new List<string>();
            var content = Document.GetElementbyId("content");
            var nodes =  content.Descendants();
            foreach (HtmlNode node in nodes)
            {
                if (node.Attributes.Contains("class"))
                    matchedNodes.Add(node.ToString());
            }
           // nodes.Attributes

            return matchedNodes;
        }
    }
}
