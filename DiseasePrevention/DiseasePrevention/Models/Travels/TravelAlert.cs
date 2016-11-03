using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models.Travels
{
    public class TravelAlert
    {
        public string Id { get; set; }
        
        /// <summary>
        /// 警示發送時間
        /// </summary>
        public DateTime Sent { get; set; }

        /// <summary>
        /// 來源簡述
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 有效日期與時間
        /// </summary>
        public DateTime Effective { get; set; }

        /// <summary>
        /// 到期日期與時間
        /// </summary>
        public DateTime Expires { get; set; }

        /// <summary>
        /// 發送者名稱
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 標題
        /// </summary>
        public string Headline { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 描述建議採取應變方案
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// 其他資訊連結
        /// </summary>
        public string Web { get; set; }

        /// <summary>
        /// 警示標題
        /// </summary>
        public string Alert_Title { get; set; }

        /// <summary>
        /// 嚴重程度
        /// </summary>
        public string Severity_Level { get; set; }

        /// <summary>
        /// 疾病
        /// </summary>
        public string Alert_Disease { get; set; }

        /// <summary>
        /// 區域描述
        /// </summary>
        public string AreaDesc { get; set; }

        /// <summary>
        /// 英文區域描述
        /// </summary>
        public string AreaDesc_EN { get; set; }

        /// <summary>
        /// 中心點座標及半徑
        /// </summary>
        public string Circle { get; set; }

        /// <summary>
        /// 國家或地區標準代碼
        /// </summary>
        public string ISO3166 { get; set; }

        /// <summary>
        /// 省市
        /// </summary>
        public string AreaDetail { get; set; }

        /// <summary>
        /// 省市代碼
        /// </summary>
        public string ISO3166_2 { get; set; }
    }

}
