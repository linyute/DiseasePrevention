using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models.Vaccines
{
    public class AdultVaccine
    {
        /// <summary>
        /// 疫苗種類
        /// </summary>
        public string Vaccine { get; set; }

        /// <summary>
        /// 年齡
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 建議
        /// </summary>
        public string Advice { get; set; }

        /// <summary>
        /// 接種劑次
        /// </summary>
        public string Vaccinate { get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        public string Note { get; set; }
    }
}
