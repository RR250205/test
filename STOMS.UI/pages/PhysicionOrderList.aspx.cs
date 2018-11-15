using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;
using System.Data.SqlClient;
namespace STOMS.UI.pages
{
    public partial class PhysicionOrderList : System.Web.UI.Page
    {
        private string strErrorMsg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                Session["PgContentTitle"] = "Physicion Order List";
                getPhyOrderList();

            }

           }

      
        //public string CustID { get; private set; }
        //public string CustomerID { get; private set; }

        public void getPhyOrderList()
        {
      

           
            try
            {

                if(Convert.ToString(Session["UserMail"]) != "")
                {
                    List<OrderBO> Appuser = (new CustomerDA()).GetAppEmail(Convert.ToString(Session["UserMail"]));

                    //List<CustomerBO> customersMail = (new CustomerDA()).GetCustomer(Convert.ToInt32(Session["OrgID"]));

                    if (Appuser.Count != 0)
                    {
                        rpPhyOrderList.DataSource = Appuser.OrderByDescending(x => x.OrderNumber);
                        // rpPhyOrderList.DataSource = Appuser[0].objCustomer.CustID;
                        rpPhyOrderList.DataBind();
                    }

                    else
                    {
                        ltrSampReceiveCount.Text = "No Order";

                    }


                }




            }
            catch (Exception ex)
            {
                strErrorMsg = ex.Message;
                
                throw ;
            }


        }

        protected void rpPhyOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Action")
                Response.Redirect("~/pages/PhysicionOrderview?si=" + Convert.ToString(e.CommandArgument));
        }

        protected void rpPhyOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

                OrderBO objorderlist = (OrderBO)e.Item.DataItem;
                Label lblSpecimenNumber = (Label)e.Item.FindControl("lblSpecimenNumber");
                Label lblCreatedOn = (Label)e.Item.FindControl("lblCreatedOn");
                Label lblSpecimenStatus = (Label)e.Item.FindControl("lblStatus");

                lblSpecimenNumber.Text = objorderlist.specimenInfoBO.SpecimenNumber;
                lblCreatedOn.Text = objorderlist.specimenInfoBO.CreatedOn;
                lblSpecimenStatus.Text = objorderlist.specimenInfoBO.SpecimenStatus;
                //lblSpecimenStatus.Text = Common.Constant.GetStatusFormat(oSamp.SpecimenStatus);
            }
        }
    }
}