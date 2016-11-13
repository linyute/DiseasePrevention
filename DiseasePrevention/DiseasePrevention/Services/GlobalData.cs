using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiseasePrevention.Models.Serums;
using DiseasePrevention.Models.Vaccines;

namespace DiseasePrevention.Services
{
    public class GlobalData
    {
        public static List<VaccineHospital> VaccineHospitals = new List<VaccineHospital>();

        public static List<SerumHospital> SerumHospitals = new List<SerumHospital>();
    }
}
