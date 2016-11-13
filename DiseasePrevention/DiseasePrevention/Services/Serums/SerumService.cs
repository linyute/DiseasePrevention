using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using DiseasePrevention.Models.Serums;

namespace DiseasePrevention.Services.Serums
{
    public class SerumService
    {
        public SerumService(NetService netService)
        {
            _netService = netService;
        }

        private readonly NetService _netService;

        public async Task<List<SerumHospital>> GetSerumHospitalsAsync()
        {
            var url = @"http://www.cdc.gov.tw/downloadfile.aspx?fid=F862F9788B34229E";

            var str = await _netService.GetStringAsync(new Uri(url));

            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<SerumHospital>();
            }

            List<SerumHospital> items;

            using (var reader = new StringReader(str))
            using (var csv = new CsvHelper.CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<SerumHospitalMap>();

                items = csv.GetRecords<SerumHospital>()
                            .OrderBy(i => i.縣市別)
                            .ThenBy(i => i.抗蛇毒血清種類)
                            .ThenBy(i => i.醫療院所名稱)
                            .ToList();
            }

            foreach (var item in items)
            {
                item.Id = item.GetHashCode().ToString();
            }

            return items;
        }
    }

    public sealed class SerumHospitalMap : CsvClassMap<SerumHospital>
    {
        public SerumHospitalMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.縣市別).Index(0);
            Map(m => m.區域別).Index(1);
            Map(m => m.抗蛇毒血清種類).Index(2);
            Map(m => m.醫療院所名稱).Index(3);
            Map(m => m.聯絡電話).Index(4);
        }
    }
}
