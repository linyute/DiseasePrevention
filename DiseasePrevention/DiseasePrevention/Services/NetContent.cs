using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiseasePrevention.Services
{
    public class NetContent<T>
    {
        public long ResponsLength { get; set; }

        public T Content { get; set; }
    }
}
