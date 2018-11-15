using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
   public class AuditLogBO
    {
        public string EntityType { get; set; }
        public int EntityID { get; set; }
        public string ActionName { get; set; }
        public string ActionBy { get; set; }
        public DateTime ActionOn { get; set; }

    }
}
