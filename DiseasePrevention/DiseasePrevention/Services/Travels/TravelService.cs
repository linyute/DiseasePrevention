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

        public string NewsType { get; set; }

        /// <summary>
        /// 國際疫情
        /// </summary>
        /// <returns></returns>
        public async Task<List<TravelAlert>> GetTravelAlertsAsync()
        {
            string url;

            switch (NewsType)
            {
                case "國際重要疫情":
                    url = @"http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=1";
                    //url = @"http://localhost:8080/JSON//TCDCIntlEpidLast30.json";
                    break;
                case "國際旅遊疫情":
                    url = @"http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=2";
                    //url = @"http://localhost:8080/JSON//TCDCTravelAlert.json";
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

            items = items.OrderByDescending(x => x.Expires)
                         .ThenByDescending(x => x.Severity_Level)
                         .ThenByDescending(x => x.Sent)
                         .ToList();

            return items;
        }
    }
}
