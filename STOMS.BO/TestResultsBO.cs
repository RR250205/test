using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
    public class TestResultsBO
    {
        public int ResultID { get; set; }
        public int SpecimenID { get; set; }
        public int AssayID { get; set; }
        public int TenantID { get; set; }
        public int ResultDocID { get; set; }
        public string TotalBuccalProteinyield { get; set; }
        public string CitrateSynthase { get; set; }
        public string RC_IV { get; set; }
        public string RC_I { get; set; }
        public string analysisReveals { get; set; }
        public string Interpretation { get; set; }
        public string Notes { get; set; }
        public string PerformedBy { get; set; }
        public bool IsReleased { get; set; }
        public DocumentBO objDocumentBO { get; set; }
        public PaymentBO objpayment { get; set;}
    }
}
