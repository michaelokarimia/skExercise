﻿using System;
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

        public IList<String> GetAllEventNodes()
        {
            List<string> matchedNodes = new List<string>();
            var content = Document.GetElementbyId("content");
            var contentDescendants =  content.Descendants();

            var nodes = contentDescendants.SelectMany(x => x.ChildNodes);
            var inners = contentDescendants.SelectMany(x => x.ChildNodes);

            var divsWithClass = inners.Where(x => x.Name == "div" && x.Attributes.Contains("class"));

            var divsWithListingClass = divsWithClass.
                SelectMany(x => x.Attributes.AttributesWithName("class").Where(y=> y.Value == "ListingOuter"));

            //TODO use linq to grab the correct listings)))))
            

            foreach (HtmlNode node in nodes)
            {
                if (node.Attributes.Contains("class"))
                    matchedNodes.Add(node.ToString());
            }
          

            return matchedNodes;
        }
    }
}
