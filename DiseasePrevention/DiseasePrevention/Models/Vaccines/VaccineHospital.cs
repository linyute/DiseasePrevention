using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models.Vaccines
{
    public class VaccineHospital
    {
        public string Id { get; set; }
        public string 縣市 { get; set; }
        public string 鄉鎮市區 { get; set; }
        public string 機構類別 { get; set; }
        public string 合約醫療院所名稱 { get; set; }
        public string 接種時間 { get; set; }
        public string 連絡電話 { get; set; }
        public string 地址 { get; set; }
        public string 公費疫苗 { get; set; }
        public string 自費疫苗 { get; set; }
    }
}
