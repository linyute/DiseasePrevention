using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Models.Travels;
using Microsoft.Practices.ObjectBuilder2;
using Newtonsoft.Json;

namespace DiseasePrevention.Services.Travels
{
    public class TravelService
    {
        public TravelService(NetService netService)
        {
            _netService = netService;
        }

        private readonly NetService _netService;

        /// <summary>
        /// 國際疫情
        /// </summary>
        /// <param name="newsType">新聞類型</param>
        /// <returns></returns>
        public async Task<List<TravelAlert>> GetTravelAlertsAsync(string newsType)
        {
            string url;

            switch (newsType)
            {
                case "國際重要疫情":
                    url = @"http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=1";
                    break;
                case "國際旅遊疫情":
                    url = @"http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=2";
                    break;
                default:
                    url = @"http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=1";
                    break;
            }

            var str = await _netService.GetStringAsync(new Uri(url));

            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<TravelAlert>();
            }

            var items = JsonConvert.DeserializeObject<List<TravelAlert>>(str);

            items.ForEach(x => x.Id = x.GetHashCode().ToString());

            items = items.OrderByDescending(x => x.Sent)
                         .ThenByDescending(x => x.Expires)
                         .ToList();

            return items;
        }
    }
}
