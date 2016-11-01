using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Models
{
    public class MasterMenuItem
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public string ActionType { get; set; }

        public Uri Uri { get; set; }
    }
}
