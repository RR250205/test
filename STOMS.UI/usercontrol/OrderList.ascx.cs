using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace STOMS.UI.usercontrol
{
    public partial class OrderList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //string abPath = HttpContext.Current.Request.Url.AbsolutePath;
                //if (abPath.IndexOf("pages/order") > 0)
                //{
                //    btnNew.Visible = true;
                //}
                populateList();
                //Session.Remove("DBVal");
            }
        }

        private void populateList()
        {
            OrderInvDA objOrder = new OrderInvDA();

            if (Session["SearchText"] != null)
            {
                tgrdOrder.DataSource = objOrder.getOrderSarchResult(Convert.ToString(Session["SearchText"]));
                Session.Remove("SearchText");
            }
            else
                 tgrdOrder.DataSource = objOrder.getOrderList(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["ActMonth"]), Convert.ToInt32(Session["ActYear"]));
            tgrdOrder.DataBind();

            if(Session["OrgID"] != null)
            {
                tgrdOrder.DataSource = objOrder.getOrderList(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["ActMonth"]), Convert.ToInt32(Session["ActYear"]));
                tgrdOrder.DataBind();
                //tgrdOrder.DataSource = objOrder.SampleCount = 1;
            }
        }

        protected void lbtnPatientID_Command(object sender, CommandEventArgs e)
        {
            Session["PatientID"] = Convert.ToString(e.CommandArgument);
            Response.Redirect("~/pages/activity");
        }

        protected void tgrdOrder_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HtmlGenericControl sp1 = (HtmlGenericControl)e.Item.FindControl("spOrderDate");
                DateTime dtOrderDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "OrderDate"));
                sp1.InnerText = dtOrderDate.ToString("dd MMM, yyyy");

                HtmlGenericControl sp2 = (HtmlGenericControl)e.Item.FindControl("spSamplescount");
                int dt = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Samplescount"));                
                sp2.InnerText = dt.ToString("1");

                //sp1 = (HtmlGenericControl)e.Item.FindControl("spOrderStatus");
                //if (Convert.ToString(DataBinder.Eval(e.Item.DataItem, "OrderStatus")) == "")
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Session.Remove("OrderID");
            Response.Redirect("/pages/mgmtOrder");
        }

        protected void hOrderID_ValueChanged(object sender, EventArgs e)
        {
            Session["OrderID"] = hOrderID.Value;
            switch (hAct.Value)
            {
                case "T":
                    Session["TTrackID"] = ((new OrderInvDA()).getSampleTestList(hOrderID.Value))[0].TTrackID;
                    Response.Redirect("~/pages/tt");
                    break;
                case "E":
                    if (hOrderStatus.Value == "Result Track" || hOrderStatus.Value == "Result Sent")
                        Response.Redirect("~/pages/OrderDetail");
                    else
                        Response.Redirect("~/pages/mgmtOrder");
                    break;
                case "I":
                    Response.Redirect("~/pages/invdet");
                    break;
            }
        }
    }
}