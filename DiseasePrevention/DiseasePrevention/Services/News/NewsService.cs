using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.Practices.Unity;
using DiseasePrevention.Models.News;

namespace DiseasePrevention.Services.News
{
    public class NewsService
    {
        public NewsService(NetService netService)
        {
            _netService = netService;
        }

        private readonly NetService _netService;

        public string NewsType { get; set; }

        public async Task<List<RssFeed>> GetRssReedsAsync()
        {
            string url;

            switch (NewsType)
            {
                case "一般民眾版":
                    url = @"http://www.cdc.gov.tw/rss.aspx?v=0&type=news";
                    break;
                case "專業人士版":
                    url = @"http://www.cdc.gov.tw/rss.aspx?v=1&type=news";
                    break;
                case "致醫界通函":
                    url = @"http://www.cdc.gov.tw/rss.aspx?v=1&type=Medical";
                    break;
                default:
                    url = @"http://www.cdc.gov.tw/rss.aspx?v=0&type=news";
                    break;
            }

            var str = await _netService.GetStringAsync(new Uri(url));

            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<RssFeed>();
            }

            var xml = str.Replace("a10:updated", "pubDate");

            var doc = XDocument.Parse(xml);

            var feeds = (from item in doc.Element("rss").Element("channel").Elements("item")
                         select new RssFeed
                         {
                             Title = item.Element("title").Value,
                             Link = item.Element("link").Value,
                             Description = item.Element("description").Value,
                             PublicationDate = DateTime.Parse(item.Element("pubDate").Value),
                             Guid = item.Element("guid").Value
                         }).ToList();

            return feeds;
        }
    }
}
