using STOMS.DA;
using STOMS.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace STOMS.UI.usercontrol
{
    public partial class PatSampleTestList : System.Web.UI.UserControl
    {

        public String OrderID
        {
            get { return hOrderID.Value; }
            set { hOrderID.Value = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                popTesttracking();
            }
        }

        public void popTesttracking()
        {
            tgrdSampleTest.DataSource = (new OrderInvDA()).getSampleTestList(Convert.ToString(hOrderID.Value));
            tgrdSampleTest.DataBind();
        }

        protected void tgrdSampleTest_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HtmlGenericControl sp1 = (HtmlGenericControl)e.Item.FindControl("spPatName");
                sp1.InnerHtml = ((TestResultBO)(e.Item.DataItem)).oPatient.PatientName;
                //sp1 = (HtmlGenericControl)e.Item.FindControl("spOrderDate");
                //DateTime dtOrderDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "OrderDate"));
                //sp1.InnerText = dtOrderDate.ToString("dd MMM, yyyy");

                //sp1 = (HtmlGenericControl)e.Item.FindControl("spOrderStatus");
                //if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderStatus")) == "Active")
                //{
                //    sp1.Attributes.Add("class", "label label-success");
                //    sp1.InnerText = "Active";
                //}
                //else
                //{
                //    sp1.Attributes.Add("class", "label label-warning");
                //    sp1.InnerText = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderStatus"));
                //}

                //sp1.InnerText = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PatientFirstName")) + " " + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PatientLastName"))
                //+ " " + Convert.ToString(DataBinder.Eval(e.Item.DataItem, "PatientMiddleName"));

                //sp1 = (HtmlGenericControl)e.Item.FindControl("spServiceMin");
                //string dur = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Duration"));
                //sp1.InnerText = dur;
                //if (Convert.ToInt32(dur) < 20)
                //    sp1.Attributes.Add("class", "btn btn-sm btn-danger");
                //else
                //    sp1.Attributes.Add("class", "btn btn-sm btn-success");



                //sp1 = (HtmlGenericControl)e.Item.FindControl("spPatAge");
                //sp1.InnerText = (new DateTime((DateTime.Now - dtDOB).Ticks).Year).ToString();

                //sp1 = (HtmlGenericControl)e.Item.FindControl("spPatGen");
                //if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Gender")) == "F")
                //    sp1.Attributes.Add("class", "btn btn-danger fa fa-female");
                //else
                //    sp1.Attributes.Add("class", "btn btn-primary fa fa-male");
            }
        }

        protected void hTTackerID_ValueChanged(object sender, EventArgs e)
        {
            Session["TTrackID"] = hTTackerID.Value;
            Response.Redirect("~/pages/tt");    
        }
    }
}