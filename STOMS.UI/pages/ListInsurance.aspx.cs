using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using System.Text;

namespace STOMS.UI.pages
{
    public partial class ListInsurance : System.Web.UI.Page
    {
        public string strErrorMsg;

        protected void Page_Load(object sender, EventArgs e)

        
{
            if (!this.IsPostBack)
            {
                Session["PgContentTitle"] = "List of Pre-Authorization";
                if (Convert.ToInt32(Session["OrgID"]) != 0)
                {
                    GetTenantOrderList(Convert.ToInt32(Session["OrgID"]));
                }
               
                
            }
               


        }



        public void GetInsuranceOrderList()
        {
            try
            {
                List<PreInsurance> preInsurances = (new CustomerDA().GetPreInsurance());

                rpInsuranceOrderList.DataSource = preInsurances.OrderByDescending(x => x.PatientName);
                rpInsuranceOrderList.DataBind();


                }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;

                throw;
            }
        }

        public void GetTenantOrderList(int tenant )
        {
            try
            {
                List<PreInsurance> preInsurances = (new CustomerDA().GetTenantInsurance(Convert.ToInt32(tenant)));
                ddlInsurancelist.DataSource = preInsurances;
                ddlInsurancelist.DataTextField = "Tenantname";
                ddlInsurancelist.DataValueField = "TenantID";
                
                ddlInsurancelist.DataBind();

                ddlInsurancelist.SelectedValue = Convert.ToString(Session["ddlInsurancelist"]);

                Session["ddlInsurancelist"] = ddlInsurancelist.SelectedValue;

                List <PreInsurance> preInsurTenant = (new CustomerDA().GetPreInsuTenant(ddlInsurancelist.SelectedValue));
                //list

                if (preInsurTenant.Count != 0)
                {
                   rpInsuranceOrderList.DataSource = preInsurTenant.OrderByDescending(x => x.PatientName);
                   rpInsuranceOrderList.DataBind();
                }
                else
                {
                    rpInsuranceOrderList.Visible = false;
                    lblpreAuthorization.Visible = true;
                }
               
            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;

                throw;
            }
        }

        public void GetInsurance(int insTenantID)

        {

        }

        protected void rpInsuranceOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Action")

                //Session["PreInsuranceNo"] =
                 Session["InsuranceNo"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/Pages/InsuranceListView");

        }

        protected void rpInsuranceOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                PreInsurance objpreins = new PreInsurance();

                objpreins = (PreInsurance)e.Item.DataItem;


                Label lblPrimaryInsName = (Label)e.Item.FindControl("lblPrimaryInsName");
                Label lblCreatedOn = (Label)e.Item.FindControl("lblCreatedOn");
                Label lblInsuranceCard_IDno = (Label)e.Item.FindControl("lblInsuranceCard_IDno");
                Label lblPolicyName = (Label)e.Item.FindControl("lblPolicyName");
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");

               
                    

                lblPrimaryInsName.Text = objpreins.PrimaryInsName;
                lblCreatedOn.Text = objpreins.CreatedOn.ToString();
                lblInsuranceCard_IDno.Text = objpreins.InsuranceCard_IDno;
                lblPolicyName.Text = objpreins.PolicyName;
                

                if (objpreins.Status == "Received")
                {
                    lblStatus.Text= objpreins.Status;

                   // lblStatus.Attributes.Add("style", "background-color:#f39c12;");

                    lblStatus.Attributes.Add("class", "label label-sm label-warning arrowed arrowed-right;");
                       
                }
                if (objpreins.Status == "Submitted")
                {
                    lblStatus.Text = objpreins.Status;

                    lblStatus.Attributes.Add("class", "label label-sm label-success arrowed arrowed-right;");

                }


            }
        }

        protected void ddlInsurancelist_SelectedIndexChanged(object sender, EventArgs e)
                                                {


            Session["ddlInsurancelist"] = ddlInsurancelist.SelectedValue;
            List<PreInsurance> preInsurances = (new CustomerDA().GetPreInsuTenant(Convert.ToString(Session["ddlInsurancelist"])));
            rpInsuranceOrderList.DataSource = preInsurances.OrderByDescending(x => x.PatientName);

            // rpPhyOrderList.DataSource = Appuser[0].objCustomer.CustID;
            rpInsuranceOrderList.DataBind();
        }

        protected void btnInsuranceAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/InsuranceAdd");
            
        }
    }
}