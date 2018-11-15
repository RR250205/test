using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
    public class SearchBO
    {
        public string MainTitle { get; set; }
        public string Sub1 { get; set; }
        public string Sub2 { get; set; }
        public string Sub3 { get; set; }
        public string Sub4 { get; set; }
    }

    public class SearchProfileBO
    {
        public int SeaProfileID { get; set; }
        public string SeaProfileName { get; set; }
        public string DBObjectName { get; set; }
        public string SeaSQL { get; set; }
        public int TenantID { get; set; }
    }
}
