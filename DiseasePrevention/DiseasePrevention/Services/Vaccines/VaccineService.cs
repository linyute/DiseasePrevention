using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using DiseasePrevention.Models.Vaccines;
using Microsoft.Practices.ObjectBuilder2;

namespace DiseasePrevention.Services.Vaccines
{
    public class VaccineService
    {
        public VaccineService(NetService netService)
        {
            _netService = netService;
        }

        private readonly NetService _netService;

        public List<AdultVaccine> GetAdultVaccines()
        {
            var list = new List<AdultVaccine>();

            list.Add(new AdultVaccine() { Vaccine = "破傷風、白喉、百日咳相關疫苗（Td/Tdap）", Age = "19-64", Advice = "建議接種，尤其是高危險群應接種（自費）。", Vaccinate = "每10年接種一劑Td，其中一劑以Tdap取代Td", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "破傷風、白喉、百日咳相關疫苗（Td/Tdap）", Age = "65歲以上", Advice = "建議接種，尤其是高危險群應接種（自費）。", Vaccinate = "每10年追加1劑Td", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "麻疹腮腺炎德國麻疹混合疫苗（MMR）", Age = "19歲以上", Advice = "如有感染疾病之風險，可依建議接種（自費）。", Vaccinate = "2劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "季節性流感疫苗（Influenza）", Age = "19-64", Advice = "建議接種，尤其是高危險群應接種（自費）。", Vaccinate = "每年接種1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "季節性流感疫苗（Influenza）", Age = "65歲以上", Advice = "國家預防接種政策，應接種（公費）。", Vaccinate = "每年接種1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "B型肝炎疫苗（HepB）", Age = "19歲以上", Advice = "如有感染疾病之風險，可依建議接種（自費）。", Vaccinate = "3劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "A型肝炎疫苗（HepA）", Age = "19歲以上", Advice = "如有感染疾病之風險，可依建議接種（自費）。", Vaccinate = "2劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "13價結合型肺炎鏈球菌疫苗（PCV）", Age = "19-64", Advice = "如有感染疾病之風險，可依建議接種（自費）。", Vaccinate = "1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "13價結合型肺炎鏈球菌疫苗（PCV）", Age = "65歲以上", Advice = "建議接種，尤其是高危險群應接種（自費）。", Vaccinate = "1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "23價肺炎鏈球菌多醣體疫苗（PPV）", Age = "19-64", Advice = "如有感染疾病之風險，可依建議接種（自費）。", Vaccinate = "1或2劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "23價肺炎鏈球菌多醣體疫苗（PPV）", Age = "65-74", Advice = "建議接種（自費）。", Vaccinate = "1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "23價肺炎鏈球菌多醣體疫苗（PPV）", Age = "75歲以上", Advice = "國家預防接種政策，應接種（公費）。", Vaccinate = "1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "日本腦炎疫苗（JE）", Age = "19歲以上", Advice = "如有感染疾病之風險，可依建議接種（自費）。", Vaccinate = "3劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "人類乳突病毒疫苗（HPV）", Age = "19-26", Advice = "建議接種（自費）。", Vaccinate = "3劑(女性)", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });
            list.Add(new AdultVaccine() { Vaccine = "帶狀?疹疫苗（Zoster）", Age = "50歲以上", Advice = "建議接種，尤其是高危險群應接種（自費）。", Vaccinate = "1劑", Note = "詳細建議內容列於衛生福利部疾病管制署網站之預防接種專區。" });

            return list;
        }

        public async Task<List<VaccineHospital>> GetVaccineHospitalsAsync()
        {
            var url = @"http://www.cdc.gov.tw/downloadfile.aspx?fid=2AE494CD4C1EAFBD";

            var str = await _netService.GetStringAsync(new Uri(url));

            if (string.IsNullOrWhiteSpace(str))
            {
                return new List<VaccineHospital>();
            }

            List<VaccineHospital> items;

            using (var reader = new StringReader(str))
            using (var csv = new CsvHelper.CsvReader(reader))
            {
                csv.Configuration.HasHeaderRecord = true;
                csv.Configuration.RegisterClassMap<VaccineHospitalMap>();

                items = csv.GetRecords<VaccineHospital>()
                            .OrderBy(i => i.縣市)
                            .ThenBy(i => i.鄉鎮市區)
                            .ThenBy(i => i.機構類別)
                            .ThenBy(i => i.合約醫療院所名稱)
                            .ToList();
            }

            foreach (var item in items)
            {
                item.Id = item.GetHashCode().ToString();
                item.縣市 = item.縣市.Replace(" ", "");
                item.鄉鎮市區 = item.鄉鎮市區.Replace(" ", "");
                item.合約醫療院所名稱 = item.合約醫療院所名稱.Replace("\n", "");
                item.公費疫苗 = item.公費疫苗.Replace("\n", "").Replace("、", Environment.NewLine);
                item.自費疫苗 = item.自費疫苗.Replace("\n", "").Replace("、", Environment.NewLine);
            }
            
            return items;
        }
    }

    public sealed class VaccineHospitalMap : CsvClassMap<VaccineHospital>
    {
        public VaccineHospitalMap()
        {
            Map(m => m.Id).Ignore();
            Map(m => m.縣市).Index(0);
            Map(m => m.鄉鎮市區).Index(1);
            Map(m => m.機構類別).Index(2);
            Map(m => m.合約醫療院所名稱).Index(3);
            Map(m => m.接種時間).Index(4);
            Map(m => m.連絡電話).Index(5);
            Map(m => m.地址).Index(6);
            Map(m => m.公費疫苗).Index(7);
            Map(m => m.自費疫苗).Index(8);
        }
    }
}
