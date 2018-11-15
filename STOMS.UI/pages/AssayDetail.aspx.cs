using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;

namespace STOMS.UI.pages
{
    public partial class AssayDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request.QueryString["ai"]!= null)
                {
                    hAssayID.Value = Convert.ToString(Request.QueryString["ai"]);                   
                    popAssay();
                }
                else if (Request.QueryString["an"] != null)
                {
                    //hAssayID.Value = Convert.ToString(Session["AssayID"]);
                    getAssay(Request.QueryString["an"]);
                }
                else
                {
                    dvAssayDetails.Visible = false;
                }

                CurrentAssayInfo.PopulateCurrentAssay();

                Session["PgContentTitle"] = "Assay Information & Tracking";
            }
        }

        private void popAssay( )
        {
            dvCurrent.Visible = true;
            List<AssayGroupBO> oAssay = (new SpecimenDA()).GetAssayGroupList(Convert.ToInt32(Session["OrgID"]), "", Convert.ToInt32(hAssayID.Value));
            if (oAssay.Count > 0)
            {
                lblAssayNo.Text = oAssay[0].AssayBIN;
                ltrAssayDesc.Text = oAssay[0].AssayDesc;
                ltrAssayStatus.Text = oAssay[0].AssayStatus;
                ltrTestComplete.Text = oAssay[0].AssayCompleteDateTime.ToString();
                ltrTestStart.Text = oAssay[0].AssayLoadDateTime.ToString();
                //ltrTestComplete.Text = Common.Constant.ConvertToUSDateTime<DateTime,string>(oAssay[0].AssayCompleteDateTime).ToString();
                //ltrTestStart.Text = Common.Constant.ConvertToUSDateTime<DateTime, string>(oAssay[0].AssayLoadDateTime).ToString(); 
                ltrSpecimenCount.Text = Convert.ToString(oAssay[0].SampleCount);
                ltrAssayType.Text = oAssay[0].AssayType;
                rpSpecimenList.DataSource = oAssay[0].oSample;
                rpSpecimenList.DataBind();
                
                switch (ltrAssayStatus.Text)
                {
                    case "Current":
                        dvCompDate.Visible = false;
                        ltrTestComplete.Text = "-";
                        if (oAssay.Count == 20)
                        {
                            btnUpdateAssay.Text = "Load for Testing";
                        } 
                        else
                        { 
                        btnUpdateAssay.Text = "Close & Load for Testing";
                        }
                        break;
                    case "Ready for Testing":
                        dvCompDate.Visible = false;
                        ltrTestComplete.Text = "-";
                        btnUpdateAssay.Text = "Load for Testing";
                        break;
                    case "In Testing":
                        dvStartDate.Visible = false;
                        dvCompDate.Visible = true;
                        ltrTestStart.Text = oAssay[0].AssayLoadDateTime;
                        btnUpdateAssay.Text = "Testing Complete";                         
                        break;
                    case "Test Completed":
                        dvCompDate.Visible = false;
                        dvStartDate.Visible = false;
                        ltrTestStart.Text = oAssay[0].AssayLoadDateTime;
                        ltrTestComplete.Text = oAssay[0].AssayCompleteDateTime;
                        btnUpdateAssay.Visible = false;
                        break;
                }
            }
            else
            {
                lblAssayNum.Text = "Invalid Assay Number";
                dvAssayDetails.Visible = false;
                dvCurrent.Visible = false;
            }
        }

        protected void rpSpecimenList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SpecimenInfoBO oSpec = (SpecimenInfoBO)e.Item.DataItem;
                Literal ltr = (Literal)e.Item.FindControl("ltrPatName");
                ltr.Text = oSpec.oPatient.FirstName + " " + oSpec.oPatient.LastName;
                
                ltr = (Literal)e.Item.FindControl("ltrPhyName");
                ltr.Text = oSpec.oCustomer.CustomerName;

                ltr = (Literal)e.Item.FindControl("ltrSpecimenType");
                ltr.Text = oSpec.SpecimentType;
                
                ltr = (Literal)e.Item.FindControl("ltrBIN");
                ltr.Text = oSpec.oResult.ResultBIN;

                ltr = (Literal)e.Item.FindControl("ltrBL");
                ltr.Text = oSpec.oResult.ResultBL;
            }
        }
       

        protected void rpSpecimenList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //Session["SpecimenID"] = Convert.ToString(e.CommandArgument);
           // Session["AssayID"] = hAssayID.Value;
            Response.Redirect("~/pages/Specimen?si="+ Convert.ToString(e.CommandArgument)+"&&ai="+hAssayID.Value);
        }

        protected void btnUpdateAssay_Click(object sender, EventArgs e)
        {
            if (btnUpdateAssay.Text == "Close & Load for Testing" || btnUpdateAssay.Text== "Load for Testing")
            {
                if (Convert.ToInt32(ltrSpecimenCount.Text) > 7)
                {
                    if ( txtStartDate.Text != "")
                    {
                        dvError.Visible = false;
                        new SpecimenDA().UpdateAssayStatus(Convert.ToInt32(hAssayID.Value), Convert.ToDateTime(txtStartDate.Text), "In Testing");
                        Session.Remove("AssayID");
                        popAssay();
                    }
                    else
                    {
                        dvError.Visible = true;
                        errorText.InnerHtml = "Test Start date cannot be empty";
                    }
                }
                else
                {
                    btnUpdateAssay.Text = "Specimen Count is less than 8, Confirm to Load test";
                }

            }
            else if(btnUpdateAssay.Text=="Testing Complete")
            {
                if(txtCompDate.Text=="")
                {
                    ltrError.Text = "Required Test Complete Date Value";
                }
                else
                {
                    string EDate = txtCompDate.Text.Replace("/", "-");

                   // recDate = Common.Constant.ConvertToUSDateTime<string,string>(recDate);
                    DateTime Edate = Common.Constant.ConvertToUSDateTime<DateTime, string>(EDate);
                    DateTime Sdate = Common.Constant.ConvertToUSDateTime<DateTime, string>(ltrTestStart.Text);
                   // DateTime date1 = Common.Constant.ConvertToUSDateTime<DateTime, DateTime>(new DateTime());


                    if (Sdate <= Edate)
                    {
                        ltrError.Text = "";
                        dvError.Visible = false;
                        new SpecimenDA().UpdateAssayStatus(Convert.ToInt32(hAssayID.Value), 
                            Edate, "Test Completed");
                        popAssay();
                    }
                    else
                    {
                        dvError.Visible = false;
                        errorText.InnerHtml = "Test End date cannot be empty";
                    }

                    if (Sdate >= Edate)
                    {
                        ltrError.Text = "Test Start date should be before the Complete date";
                       // new SpecimenDA().UpdateAssayStatus(Convert.ToInt32(hAssayID.Value), Convert.ToDateTime(ltrTestStart.Text), "In Testing");
                    }
                }                
            }
            else if(btnUpdateAssay.Text== "Specimen Count is less than 8, Confirm to Load test")
            {
                if (txtStartDate.Text == "" )
                {
                    dvError.Visible = true;
                    errorText.InnerHtml = "Test Start date cannot be empty";
                }
                else
                {
                    dvError.Visible = false;
                    new SpecimenDA().UpdateAssayStatus(Convert.ToInt32(hAssayID.Value), Convert.ToDateTime(txtStartDate.Text), "In Testing");
                    popAssay();                    
                }
            }
        }

        protected void btnAssay_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/assay");
        }

        protected void btnAssayNum_Click(object sender, EventArgs e)
        {
            getAssay(txtAssayNum.Text);
            dvCurrent.Visible = true;
            //popAssay();
        }
        private void getAssay(string Assaynumber)
        {
            dvAssayDetails.Visible = true;
            List<AssayGroupBO> oAssay = (new SpecimenDA()).GetAssayNumberList(Convert.ToInt32(Session["OrgID"]), "", Assaynumber);
            if (oAssay.Count > 0)
            {
                hAssayID.Value = oAssay[0].AssayID.ToString();
                lblAssayNo.Text = oAssay[0].AssayBIN;
                ltrAssayDesc.Text = oAssay[0].AssayDesc;
                ltrAssayStatus.Text = oAssay[0].AssayStatus;
                ltrTestComplete.Text = oAssay[0].AssayCompleteDateTime;
                ltrTestStart.Text = oAssay[0].AssayLoadDateTime;
                ltrSpecimenCount.Text = Convert.ToString(oAssay[0].SampleCount);
                ltrAssayType.Text = oAssay[0].AssayType;
                rpSpecimenList.DataSource = oAssay[0].oSample;
                lblAssayNum.Text = "";
                rpSpecimenList.DataBind();

                switch (ltrAssayStatus.Text)
                {
                    case "Current":
                        dvCompDate.Visible = false;
                        ltrTestComplete.Text = "-";
                        if (oAssay.Count == 20)
                        {
                            btnUpdateAssay.Text = "Load for Testing";
                        }
                        else
                        {
                            btnUpdateAssay.Text = "Close & Load for Testing";
                        }
                        break;
                    case "Ready for Testing":
                        dvCompDate.Visible = false;
                        ltrTestComplete.Text = "-";
                        btnUpdateAssay.Text = "Load for Testing";
                        break;
                    case "In Testing":
                        dvStartDate.Visible = false;
                        dvCompDate.Visible = true;
                        ltrTestStart.Text = oAssay[0].AssayLoadDateTime;
                        btnUpdateAssay.Text = "Testing Complete";
                        break;
                    case "Test Completed":
                        dvCompDate.Visible = false;
                        dvStartDate.Visible = false;
                        ltrTestStart.Text = oAssay[0].AssayLoadDateTime;
                        ltrTestComplete.Text = oAssay[0].AssayCompleteDateTime;
                        btnUpdateAssay.Visible = false;
                        break;
                }
            }
            else
            {
                lblAssayNum.Text = "Invalid  Assay Number";
                dvAssayDetails.Visible = false;
            }
        }
    }
}
