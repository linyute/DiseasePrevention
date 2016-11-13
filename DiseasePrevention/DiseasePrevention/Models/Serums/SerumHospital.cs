using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models.Serums
{
    public class SerumHospital
    {
        public string Id { get; set; }
        public string 縣市別 { get; set; }
        public string 區域別 { get; set; }
        public string 抗蛇毒血清種類 { get; set; }
        public string 醫療院所名稱 { get; set; }
        public string 聯絡電話 { get; set; }
    }
}
