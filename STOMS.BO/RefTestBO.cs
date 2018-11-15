using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STOMS.BO
{

    public class RefChemParamsBO
    {
        public int RefChemParamID { get; set; }
        public string RefChemicalParam { get; set; }
        public string DataType { get; set; }
        
    }

    public class RefDiagDiseaseBO
    {
        public int RefDiagDiseaseID { get; set; }
        public int RefDiagonesisID { get; set; }
        public int RefDiseaseID { get; set; }
    }

    public class RefDiagonesisBO
    {
        public int RefDiagonesisID { get; set; }
        public string RefDiagonesisName { get; set; }
        public string Description { get; set; }
        public string DiagonesisStatus { get; set; }
        public string DiagonesisCode { get; set; }

    }
    public class RefDiseaseBO
    {
        public int RefDiseaseID { get; set; }
        public string RefDiseaseName { get; set; }
        public string DiseaseCode { get; set; }
        public string Description { get; set; }
        public string DiseaseStatus { get; set; }
    }

    public class RefResultParamsBO
    {
        public int RefResultParamID { get; set; }
        public string RefResltParam { get; set; }
        public string DataType { get; set; }
    }


    public class RefSpecimensBO
    {
        public int RefSpecimenID { get; set; }
        public int CreatedBy { get; set; }
        public string RefSpecimenName { get; set; }
        public string RefSpecimenCode { get; set; }
        public string SpecimenStatus { get; set; }
        public string Description { get; set; }
    }

    public class RefTestProfileBO
    {
        public int TestProfileID { get; set; }
        public string TestProfileName { get; set; }
        public string TestProfileCode { get; set; }
        public string TestProfileDesc { get; set; }
        public string TestProfileStatus { get; set; }
        public bool isDiagnosis { get; set; }
        public bool isPanel { get; set; }
        public string ResultParams { get; set; }
        public string ObservationParams { get; set; }
        public string CreatedBy { get; set; }
        public string ViewDefault { get; set; }


    }
    public class TPTestCollectionsBO
    {
        public int RefTPTestCollectionID { get; set; }
        public int RefTestProfileID { get; set; }
        public int RefSpecimenID { get; set; }
        public int RefDiagDiseaseID { get; set; }
        public int TenantID { get; set; }
        public bool isSolid { get; set; }
        public bool isSemiSolid { get; set; }
        public bool isLiquid { get; set; }
        public string MinML { get; set; }
        public string MaxML { get; set; }
        public string MinKG { get; set; }
        public string MaxKG { get; set; }
        public string TPTestColectionDesc { get; set; }
        public string ChemicalParams { get; set; }
        public string TPTestCollectionStatus { get; set; }
        public List<RefSpecimensBO> Specimens { get; set; }
    }








    




}

