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

        /// <summary>
        /// 最新消息 RSS
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<RssFeed> ParseRssFeeds(string str)
        {
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

        /// <summary>
        /// 最新消息
        /// </summary>
        /// <param name="newsType">新聞類型</param>
        /// <returns></returns>
        public async Task<List<RssFeed>> GetRssReedsAsync(string newsType)
        {
            string url;

            switch (newsType)
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

            var feeds = ParseRssFeeds(str);

            return feeds;
        }

        /// <summary>
        /// 傳染病介紹
        /// </summary>
        /// <param name="diseaseId">傳染病ID</param>
        /// <returns></returns>
        public async Task<RssFeed> GetDiseaseFeedAsync(string diseaseId)
        {
            var url = $"http://www.cdc.gov.tw/rss.aspx?v=0&type=disease&infoid={diseaseId}";

            var str = await _netService.GetStringAsync(new Uri(url));

            if (string.IsNullOrWhiteSpace(str))
            {
                return new RssFeed();
            }

            var feed = ParseRssFeeds(str).First();

            return feed;
        }

        /// <summary>
        /// 傳染病介紹
        /// </summary>
        /// <param name="diseaseType">傳染病類型</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDiseaseList(string diseaseType)
        {
            var dic = new Dictionary<string, string>();

            switch (diseaseType)
            {
                case "食物或飲水傳染":
                    #region 食物或飲水傳染
                    dic.Add("900059B505FD76DF", "腸病毒感染併發重症");
                    dic.Add("49589DADC469AE67", "腸道出血性大腸桿菌感染症");
                    dic.Add("2463B5769DDC8E07", "傷寒");
                    dic.Add("86E7C9F4BE08D402", "副傷寒");
                    dic.Add("D03CCE3DF521E46A", "沙門氏菌感染症");
                    dic.Add("0AC35BE524316F6E", "桿菌性痢疾");
                    dic.Add("7D1D150DFB066004", "阿米巴性痢疾");
                    dic.Add("CD09974C1FE4B929", "霍亂");
                    dic.Add("A664CCFBD51288B0", "肉毒桿菌中毒");
                    dic.Add("FC684A1C772115D7", "庫賈氏病");
                    dic.Add("9FA12F779BF31729", "病毒性腸胃炎");
                    dic.Add("5848FEA154B21703", "細菌性腸胃炎");
                    dic.Add("1DB4C438EA3B1E76", "急性病毒性Ａ型肝炎");
                    dic.Add("4775659102DE26C1", "急性病毒性Ｅ型肝炎");
                    dic.Add("2CF06E0F40B546B8", "小兒麻痺症/急性無力肢體麻痺");
                    dic.Add("16533556D6300F4F", "弓形蟲感染症");
                    dic.Add("D4090DC3C44F755E", "李斯特菌症");
                    dic.Add("40A13713A346C838", "布氏桿菌病");
                    dic.Add("109B9B14D881EED2", "第二型豬鏈球菌感染症");
                    dic.Add("A3088CE5CC1F6B14", "常見腸道寄生蟲病簡介");
                    dic.Add("4E79822E96B2FFE9", "中華肝吸蟲感染症");
                    dic.Add("6AF06C6D63FBD8FE", "旋毛蟲感染症");
                    dic.Add("4C24959402FFB812", "肺吸蟲感染症");
                    dic.Add("9E2A51093406A2F4", "廣東住血線蟲感染症");
                    dic.Add("A6D83F837BA318BA", "人芽囊原蟲感染");
                    #endregion
                    break;
                case "空氣或飛沫傳染":
                    #region 空氣或飛沫傳染
                    dic.Add("6CB2EE526856976F", "新型A型流感");
                    dic.Add("30438A9E5363E7E0", "水痘併發症");
                    dic.Add("BAB48CF8772C3B05", "結核病");
                    dic.Add("2204DE11B176D590", "多重抗藥性結核病");
                    dic.Add("3013B7FC8F965336", "流感");
                    dic.Add("1B5D2BF9B5B96C06", "中東呼吸症候群冠狀病毒感染症");
                    dic.Add("3687AF99CCB7AABA", "嚴重急性呼吸道症候群");
                    dic.Add("EB2BECD7208073E4", "麻疹");
                    dic.Add("E496B051C5DB941B", "德國麻疹");
                    dic.Add("2E2B87449C3583AC", "先天性德國麻疹症候群");
                    dic.Add("166ADF19B748B86A", "流行性腮腺炎");
                    dic.Add("7AE2FF6AD0360546", "白喉");
                    dic.Add("BA1E4B695CF4C89F", "百日咳");
                    dic.Add("0FCA7A8669C8AC95", "侵襲性肺炎鏈球菌感染症");
                    dic.Add("8A5ED86E1F900BF2", "侵襲性ｂ型嗜血桿菌感染症");
                    dic.Add("5D72E07679B5057C", "流行性腦脊髓膜炎");
                    dic.Add("16CD29A279936655", "退伍軍人病");
                    dic.Add("4B84885EB6795AC7", "天花");
                    dic.Add("3448E1B7E360BA50", "漢他病毒症候群");
                    dic.Add("45328EDA04F7EB12", "肺囊蟲肺炎");
                    dic.Add("67CCCCD371D8DD79", "隱球菌症");
                    dic.Add("91F0941F403EE107", "Ｑ熱");
                    dic.Add("5EADA9389EF93DE4", "鸚鵡熱");
                    #endregion
                    break;
                case "蟲媒傳染":
                    #region 蟲媒傳染
                    dic.Add("77BFF3D4F9CB7982", "登革熱");
                    dic.Add("59FB45DE0CD3F8A0", "屈公病");
                    dic.Add("AC308F5F5E7F4299", "瘧疾");
                    dic.Add("CFB77829C7ABD761", "日本腦炎");
                    dic.Add("8F87A67697D2278B", "鼠疫");
                    dic.Add("5976EE7146BF5A34", "恙蟲病");
                    dic.Add("1C7BF6D5F52F478C", "西尼羅熱");
                    dic.Add("3C994A4FA28416FC", "地方性斑疹傷寒");
                    dic.Add("778876767EA9B06C", "流行性斑疹傷寒");
                    dic.Add("DED62714A07C973A", "萊姆病");
                    dic.Add("39EEA2430F0D193B", "黃熱病");
                    dic.Add("2560135C8787ACC9", "茲卡病毒感染症");
                    dic.Add("BF8212C8B091475E", "裂谷熱");
                    dic.Add("B3EFBE47CB5B528B", "發熱伴血小板減少綜合症");
                    dic.Add("DAB2C8BFA4E99546", "淋巴絲蟲病");
                    #endregion
                    break;
                case "性接觸或血液傳染":
                    #region 性接觸或血液傳染
                    dic.Add("0D62EE0F6D4EBF8C", "人類免疫缺乏病毒感染");
                    dic.Add("A7AF893FD64D1AAB", "梅毒");
                    dic.Add("4F860B4BBF6BE67C", "先天性梅毒");
                    dic.Add("BD8D809CEFD7C723", "淋病");
                    dic.Add("5CA41646EE28F677", "急性病毒性Ｂ型肝炎");
                    dic.Add("D47D6C4244E0E715", "急性病毒性Ｃ型肝炎");
                    dic.Add("11498A962D46A726", "急性病毒性Ｄ型肝炎");
                    #endregion
                    break;
                case "接觸傳染":
                    #region 接觸傳染
                    dic.Add("2FC8BD0F385B4B23", "福氏內格里阿米巴腦膜腦炎");
                    dic.Add("9D2E1B3A862F06FB", "狂犬病");
                    dic.Add("E6EF6B00003631B5", "炭疽病");
                    dic.Add("C0883117AE98CBAC", "類鼻疽");
                    dic.Add("110FA1639DEF7957", "鉤端螺旋體病");
                    dic.Add("37DAE22507FE1341", "破傷風");
                    dic.Add("B7A99E685C850D64", "新生兒破傷風");
                    dic.Add("4DC6CDD1D4B8972D", "疥瘡感染症");
                    dic.Add("4E9C3E6AC79BFB9D", "頭蝨感染症");
                    dic.Add("93A90D21A8A6D479", "漢生病");
                    dic.Add("9618DBD92CAD7F8F", "貓抓病");
                    dic.Add("D40B2E6AC7484C3B", "兔熱病");
                    dic.Add("F81306B23916A286", "疱疹B病毒感染症");
                    dic.Add("7539A23334280253", "亨德拉病毒及立百病毒感染症");
                    dic.Add("3D5FAA7DEE4D73DB", "拉薩熱");
                    dic.Add("D4B63062FD6D1963", "馬堡病毒出血熱");
                    dic.Add("A1D7910CD9970140", "伊波拉病毒感染");
                    #endregion
                    break;
                case "其他類":
                    #region 其他類
                    dic.Add("EF5AF2A9D2E6CC3A", "棘狀阿米巴");
                    dic.Add("11F61CDA6C98C1D1", "NDM-1腸道菌感染症");
                    dic.Add("D7A8974B83F1BF28", "CRE抗藥性檢測");
                    dic.Add("62DDA453867679D2", "VISA/VRSA抗藥性檢測");
                    #endregion
                    break;
                default:
                    break;
            }

            return dic;
        }
    }
}
