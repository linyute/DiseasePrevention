using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Services;
using DiseasePrevention.Services.News;
using Xunit;
using Xunit.Abstractions;

namespace DiseasePrevention.Tests.Services.News
{
    public class NewsServiceTests
    {
        private readonly ITestOutputHelper _output;

        public NewsServiceTests(ITestOutputHelper output)
        {
            this._output = output;
            this._httpClient = new HttpClient();
            this._netService = new NetService(_httpClient);
        }

        private readonly HttpClient _httpClient;

        private readonly NetService _netService;

        [Fact]
        public void ParseRssFeedsTest()
        {
            // Arrange
            var newsService = new NewsService(_netService);
            #region xml

            var xml = @"<?xml version='1.0' encoding='utf-8'?>
<rss xmlns:a10='http://www.w3.org/2005/Atom' version='2.0'>
  <channel>
    <title>衛生福利部疾病管制署一般民眾版</title>
    <link>http://61.57.41.133/</link>
    <description>衛生福利部疾病管制署一般民眾版</description>
    <lastBuildDate>Thu, 06 Oct 2016 21:26:59 +0800</lastBuildDate>
    <a10:id>衛生福利部疾病管制署一般民眾版</a10:id>
    <item>
      <guid isPermaLink='false'>49378</guid>
      <link>http://61.57.41.133/info.aspx?treeid=45da8e73a81d495d&amp;nowtreeid=1bd193ed6dabaee6&amp;tid=F0271BE66DA5EE91</link>
      <title>一週國內外疫情摘要及預測</title>
      <description>&lt;p&gt;&lt;span&gt;　　上週（&lt;/span&gt;&lt;span lang='EN-US'&gt;9&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;25&lt;/span&gt;&lt;span&gt;日至&lt;/span&gt;&lt;span lang='EN-US'&gt;10&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;1&lt;/span&gt;&lt;span&gt;日）無新增本土登革熱確定病例，今年本土病例累計&lt;/span&gt;&lt;span lang='EN-US'&gt;377&lt;/span&gt;&lt;span&gt;例；另新增&lt;/span&gt;&lt;span lang='EN-US'&gt;4&lt;/span&gt;&lt;span&gt;例境外移入病例，感染國家分別為越南、印尼、菲律賓及緬甸各&lt;/span&gt;&lt;span lang='EN-US'&gt;1&lt;/span&gt;&lt;span&gt;例。&lt;/span&gt;&lt;span&gt;東南亞近期登革熱疫情處相對高點，寮國、菲律賓、越南及柬埔寨今年累計病例數均高於去年同期，泰國及馬來西亞則低於去年同期；新加坡近期疫情下降，惟今年累計病例數高於去年全年總數。由於東南亞疫情升溫，境外移入病例持續發生；南部地區有局部短暫雷陣雨，另因上週颱風降雨積水，孳生源增加，本土疫情風險持續。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span lang='EN-US'&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/span&gt;&lt;span&gt;近期恙蟲病通報數雖呈下降趨勢，惟疫情仍處流行期，流行區域以花東及離島縣市為主。另腸病毒疫情持平，上週全國門、急診就診人次（&lt;/span&gt;&lt;span lang='EN-US'&gt;8,488&lt;/span&gt;&lt;span&gt;）較前一週（&lt;/span&gt;&lt;span lang='EN-US'&gt;9,038&lt;/span&gt;&lt;span&gt;）略降&lt;/span&gt;&lt;span lang='EN-US'&gt;6.1%&lt;/span&gt;&lt;span&gt;；惟颱風來襲醫院休診可能影響就診人次，疫情仍需持續觀察。社區仍以感染克沙奇&lt;/span&gt;&lt;span lang='EN-US'&gt;A&lt;/span&gt;&lt;span&gt;型之輕症為&lt;/span&gt;&lt;span&gt;主，腸病毒&lt;/span&gt;&lt;span lang='EN-US'&gt;71&lt;/span&gt;&lt;span&gt;型仍有散發個案。上週無新增重症確定個案；今年累計&lt;/span&gt;&lt;span lang='EN-US'&gt;21&lt;/span&gt;&lt;span&gt;例重症確定病例，其中&lt;/span&gt;&lt;span lang='EN-US'&gt;20&lt;/span&gt;&lt;span&gt;例感染腸病毒&lt;/span&gt;&lt;span lang='EN-US'&gt;71&lt;/span&gt;&lt;span&gt;型、&lt;/span&gt;&lt;span lang='EN-US'&gt;1&lt;/span&gt;&lt;span&gt;例感染克沙奇&lt;/span&gt;&lt;span lang='EN-US'&gt;A5&lt;/span&gt;&lt;span&gt;型病毒。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span&gt;　　美國佛羅里達州茲卡本土病例數有升高情形，上週新增&lt;/span&gt;&lt;span lang='EN-US'&gt;34&lt;/span&gt;&lt;span&gt;例，自&lt;/span&gt;&lt;span lang='EN-US'&gt;7&lt;/span&gt;&lt;span&gt;月底至&lt;/span&gt;&lt;span lang='EN-US'&gt;10&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;3&lt;/span&gt;&lt;span&gt;日累計&lt;/span&gt;&lt;span lang='EN-US'&gt;149&lt;/span&gt;&lt;span&gt;例，分布於邁阿密郡（&lt;/span&gt;&lt;span lang='EN-US'&gt;Miami-Dade&lt;/span&gt;&lt;span&gt;）、棕櫚灘郡（&lt;/span&gt;&lt;span lang='EN-US'&gt;Palm Beach&lt;/span&gt;&lt;span&gt;）、皮尼拉斯郡（&lt;/span&gt;&lt;span lang='EN-US'&gt;Pinellas&lt;/span&gt;&lt;span&gt;）及布勞沃德郡（&lt;/span&gt;&lt;span lang='EN-US'&gt;Broward&lt;/span&gt;&lt;span&gt;）共&lt;/span&gt;&lt;span lang='EN-US'&gt;4&lt;/span&gt;&lt;span&gt;郡；尚有&lt;/span&gt;&lt;span lang='EN-US'&gt;4&lt;/span&gt;&lt;span&gt;例感染地不明，調查中。新加坡茲卡本土疫情略為趨緩，上週新增&lt;/span&gt;&lt;span lang='EN-US'&gt;8&lt;/span&gt;&lt;span&gt;例，排除&lt;/span&gt;&lt;span lang='EN-US'&gt;2&lt;/span&gt;&lt;span&gt;個群聚區，自&lt;/span&gt;&lt;span lang='EN-US'&gt;8&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;27&lt;/span&gt;&lt;span&gt;日至&lt;/span&gt;&lt;span lang='EN-US'&gt;10&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;3&lt;/span&gt;&lt;span&gt;日確診&lt;/span&gt;&lt;span lang='EN-US'&gt;399&lt;/span&gt;&lt;span&gt;例，包含&lt;/span&gt;&lt;span lang='EN-US'&gt;16&lt;/span&gt;&lt;span&gt;名孕婦，病例分布於阿裕尼灣、沈氏通道等&lt;/span&gt;&lt;span lang='EN-US'&gt;7&lt;/span&gt;&lt;span&gt;個群聚地區。馬來西亞上週新增&lt;/span&gt;&lt;span lang='EN-US'&gt;1&lt;/span&gt;&lt;span&gt;例，今年&lt;/span&gt;&lt;span lang='EN-US'&gt;9&lt;/span&gt;&lt;span&gt;月累計&lt;/span&gt;&lt;span lang='EN-US'&gt;7&lt;/span&gt;&lt;span&gt;例，包含&lt;/span&gt;&lt;span lang='EN-US'&gt;2&lt;/span&gt;&lt;span&gt;名孕婦，病例分布於東北部砂拉越州、沙巴州及鄰近新加坡之新山地區。泰國近兩週公布&lt;/span&gt;&lt;span lang='EN-US'&gt;113&lt;/span&gt;&lt;span&gt;名茲卡病例，今年累計&lt;/span&gt;&lt;span lang='EN-US'&gt;392&lt;/span&gt;&lt;span&gt;例，包含&lt;/span&gt;&lt;span lang='EN-US'&gt;39&lt;/span&gt;&lt;span&gt;名孕婦；另公布該國首&lt;/span&gt;&lt;span lang='EN-US'&gt;2&lt;/span&gt;&lt;span&gt;例茲卡相關小頭症病例，亦為東南亞首度報告茲卡相關小頭症個案。越南及印尼持續對他國輸出病例。近期東南亞國家茲卡疫情持續，且現值病媒蚊活動期，我國發生境外移入病例及本土疫情風險均升高。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span lang='EN-US'&gt;&amp;nbsp;&amp;nbsp;&amp;nbsp; &lt;/span&gt;&lt;span&gt;世界衛生組織（&lt;/span&gt;&lt;span lang='EN-US'&gt;WHO&lt;/span&gt;&lt;span&gt;）&lt;/span&gt;&lt;span lang='EN-US'&gt;9&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;29&lt;/span&gt;&lt;span&gt;日公布，全球累計&lt;/span&gt;&lt;span lang='EN-US'&gt;70&lt;/span&gt;&lt;span&gt;國家&lt;/span&gt;&lt;span lang='EN-US'&gt;/&lt;/span&gt;&lt;span&gt;屬地出現茲卡本土病例，中南美洲及加勒比海地區整體疫情趨緩，惟仍有少數國家疫情持續；東南亞國家近期陸續傳出茲卡疫情，須提高警覺。疾管署已將具流行疫情或可能有本土傳播之&lt;/span&gt;&lt;span lang='EN-US'&gt;62&lt;/span&gt;&lt;span&gt;國或屬地，包括亞洲泰國、菲律賓、越南、印尼、新加坡、馬來西亞及馬爾地夫&lt;/span&gt;&lt;span lang='EN-US'&gt;7&lt;/span&gt;&lt;span&gt;國旅遊疫情建議列為第二級警示。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span&gt;　　疾管署本期《疫情報導》摘要：&lt;/span&gt;&lt;span lang='EN-US'&gt;10&lt;/span&gt;&lt;span&gt;月&lt;/span&gt;&lt;span lang='EN-US'&gt;4&lt;/span&gt;&lt;span&gt;日刊出由魏欣怡防疫醫師所著「&lt;/span&gt;&lt;span lang='EN-US'&gt;2016&lt;/span&gt;&lt;span&gt;年臺灣首例恙蟲病死亡病例報告」與國防醫學院金遠凡研究助理所著「&lt;/span&gt;&lt;span lang='EN-US'&gt;2003&lt;/span&gt;&lt;span&gt;至&lt;/span&gt;&lt;span lang='EN-US'&gt;2015&lt;/span&gt;&lt;span&gt;年澎湖縣恙蟲病病媒採樣調查結果與恙蟲病年病例數之相關性分析」兩篇文章，詳細內容請上疾管署全球資訊網&lt;/span&gt;&lt;span lang='EN-US'&gt;/&lt;/span&gt;&lt;span&gt;出版品&lt;/span&gt;&lt;span lang='EN-US'&gt;/&lt;/span&gt;&lt;span&gt;疫情報導查閱。&lt;/span&gt;&lt;/p&gt;</description>
      <a10:updated>2016-10-04T13:29:50Z</a10:updated>
    </item>
    <item>
      <guid isPermaLink='false'>49376</guid>
      <link>http://61.57.41.133/info.aspx?treeid=45da8e73a81d495d&amp;nowtreeid=1bd193ed6dabaee6&amp;tid=5D5C93933575AB35</link>
      <title>疾管署公布第2例境外移入萊姆病確定病例，民眾至流行地區請加強防護，返家儘速更衣沐浴</title>
      <description>&lt;p&gt;&lt;span&gt;　　疾病管制署公布新增&lt;/span&gt;1&lt;span&gt;例境外移入萊姆病確定病例，為&lt;/span&gt;60&lt;span&gt;歲本國籍女性，與家人長期居住於美國麻州，&lt;/span&gt;9&lt;span&gt;月&lt;/span&gt;11&lt;span&gt;日出現右臉麻痺、手腳有皮疹等情形於當地就醫，&lt;/span&gt;14&lt;span&gt;日返台，&lt;/span&gt;19&lt;span&gt;日因症狀未改善至醫院就醫，並自述於美國就醫時曾抽血確診為萊姆病，經醫院通報採檢後確認感染；目前個案已出院。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span&gt;　　疾管署表示，個案於萊姆病潛伏期間（&lt;/span&gt;8&lt;span&gt;月&lt;/span&gt;11&lt;span&gt;日至&lt;/span&gt;9&lt;span&gt;月&lt;/span&gt;8&lt;span&gt;日）均在美國，並自述&lt;/span&gt;8&lt;span&gt;月中旬曾於麻州一處草堆中接觸野鹿，並於草堆遭蜱叮咬傷，依據個案潛伏期間活動史，及萊姆病盛行地區研判感染地為美國。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span&gt;　　我國今（&lt;/span&gt;2016&lt;span&gt;）年截至目前共&lt;/span&gt;2&lt;span&gt;例萊姆病確定病例，均為境外移入，個案分別為瑞典籍與本國籍，感染國家為瑞典及美國。近&lt;/span&gt;10&lt;span&gt;年（自&lt;/span&gt;2007&lt;span&gt;年起迄今）國內萊姆病確定病例數共計&lt;/span&gt;12&lt;span&gt;例，均為境外移入病例，包括本國籍&lt;/span&gt;7&lt;span&gt;例、美國籍&lt;/span&gt;4&lt;span&gt;例及瑞典籍&lt;/span&gt;1&lt;span&gt;例；感染國家以美國&lt;/span&gt;8&lt;span&gt;例最多，其次為歐洲國家包括丹麥、英國、德國及瑞典各&lt;/span&gt;1&lt;span&gt;例；無死亡病例。萊姆病主要分布於全球溫帶區域，如美國（尤其是東北部）、北亞、中亞、加拿大、南美洲，以及鄰近的日本、韓國及中國大陸等地。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span&gt;　　萊姆病是一種人畜共通傳染病，藉由被感染的蜱（俗稱壁蝨）叮咬而傳播，不會人傳人，在自然界中，蜱主要寄生在野外的鼠類身上，人類會因為被感染的蜱叮咬而遭到感染。萊姆病潛伏期為&lt;/span&gt;3&lt;span&gt;至&lt;/span&gt;32 &lt;span&gt;天（平均&lt;/span&gt;7&lt;span&gt;至&lt;/span&gt;10&lt;span&gt;天），&lt;/span&gt;70%&lt;span&gt;至&lt;/span&gt;80%&lt;span&gt;感染者會出現遊走性紅斑，感染初期會有頭痛、發燒、淋巴腺腫大、肌肉疼痛、喉嚨痛、頸部僵硬等類似感冒的症狀，若無妥適治療，後期可能會出現心臟或神經系統異常。&lt;/span&gt;&lt;/p&gt; &lt;p&gt;&lt;span&gt;　　疾管署呼籲，民眾至流行地區或野外踏青應加強防護措施，請著淺色長袖衣褲、手套及長靴等保護性衣物，將褲管紮入襪內，並於皮膚裸露處塗抹衛福部核可的防蚊蟲藥劑；返家前應檢查是否遭蜱叮咬或附著，並儘快沐浴及換洗衣物；如發現遭硬蜱叮咬，應用鑷子夾住蜱的口器，小心將蜱摘除，避免口器斷裂殘留於體內，並立刻用肥皂沖洗叮咬處，降低感染風險。如出現疑似症狀，請儘速就醫，並告知旅遊接觸史以利醫師診斷治療。相關資訊可至疾管署全球資訊網（&lt;/span&gt;http://www.cdc.gov.tw&lt;span&gt;）或撥打免付費防疫專線&lt;/span&gt;1922&lt;span&gt;（或&lt;/span&gt;0800-001922&lt;span&gt;）洽詢。&lt;/span&gt;&lt;/p&gt;</description>
      <a10:updated>2016-10-04T12:06:41Z</a10:updated>
    </item>
  </channel>
</rss>
";

            #endregion

            // Act
            var items = newsService.ParseRssFeeds(xml);

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Title);
        }


        [Fact]
        public async void GetRssReedsAsyncTest()
        {
            // Arrange
            var newsService = new NewsService(_netService);

            // Act
            var items = await newsService.GetRssReedsAsync();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Title);
        }

        [Fact]
        public void GetDiseaseListTest()
        {
            // Arrange
            var newsService = new NewsService(_netService);
            newsService.DiseaseType = "食物或飲水傳染";

            // Act
            var items = newsService.GetDiseaseList();

            // Assert
            Assert.True(items.Any());

            this._output.WriteLine(items.First().Value);
        }

        [Fact]
        public async void GetDiseaseFeedAsyncTest()
        {
            // Arrange
            var newsService = new NewsService(_netService);
            var diseaseId = "900059B505FD76DF";

            // Act
            var items = await newsService.GetDiseaseFeedAsync(diseaseId);

            // Assert
            Assert.True(items.Title == "腸病毒感染併發重症");

            this._output.WriteLine(items.Title);
        }
    }
}
