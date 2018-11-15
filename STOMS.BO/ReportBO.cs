using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{
    public class DashboardStatBO
    {
        public int TotalOrders { get; set; }
        public int SampleTesting { get; set; }
        public int ReceivedSpecimens { get; set; }
        public int ReadyforAssaySpecimes { get; set; }
        public int AssigntoAssaySpecimens { get; set; }
        public int ClientCount { get; set; }
        public int OutStandingInv { get; set; }
    }
   
    public class SpecimenReportBO
    {
        public SpecimenInfoBO specimenBO { get; set; }
        public PatientBO patientBO { get; set; }
        public CustomerBO customerBO { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }        
    }
}


