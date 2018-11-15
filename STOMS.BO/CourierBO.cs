using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
    public class CourierBO
    {
        public int CourierID { get; set; }
        public int CourierTenantID { get; set; }
        public int TenantID { get; set; }
        public string CourierName { get; set; }
    }
}
