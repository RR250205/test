using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using STOMS.Common;

namespace STOMS.UI.pages
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                 int SearchCount = 0;

                Session["PgContentTitle"] = "Search";
                if (Request.QueryString["searchkey"] != null)
                {
                    ucSpecimen.SearchText = Request.QueryString["searchkey"];
                    /* Search By Specimen */
                    ucSpecimen.SearchType = "By Specimen";
                    SearchProfileBO searchProfSpecBO = new SearchProfileBO()
                    {
                        TenantID = Convert.ToInt32(Session["OrgID"]),
                        SeaProfileName = "Specimen"
                    };
                    SearchProfileBO searchSpecimen = new SearchDA().GetSearchProfileDetails(searchProfSpecBO);
                        
                    ucSpecimen.popSearchResult(searchSpecimen.SeaProfileID);
                    hSearchCount.Value = ucSpecimen.ResultCount.ToString();
                    SearchCount += ucSpecimen.ResultCount;
                    if (Convert.ToInt32(hSearchCount.Value) == 0)
                        ucSpecimen.Visible = false;


                /*Search By Assay*/
                    ucAssay.SearchType = "By Assay";
                    SearchProfileBO searchProfAssBO = new SearchProfileBO()
                    {
                        TenantID = Convert.ToInt32(Session["OrgID"]),
                        SeaProfileName = "Assay"
                    };
                    SearchProfileBO searchAssay = new SearchDA().GetSearchProfileDetails(searchProfAssBO);
                    ucAssay.popSearchResult(searchAssay.SeaProfileID);
                    hSearchCount.Value = ucAssay.ResultCount.ToString();
                    SearchCount += ucAssay.ResultCount;
                    if (Convert.ToInt32(hSearchCount.Value) == 0)
                        ucAssay.Visible = false;

                    /* No Record Found*/
                    if (SearchCount == 0)
                        dvNoRec.Visible = true;
                }
            }
        }
    }
}
