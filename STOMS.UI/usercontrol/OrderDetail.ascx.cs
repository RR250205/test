using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class OrderDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                popOrderInfo();
            }
        }

        private void popOrderInfo()
        {
            if (Session["OrderID"] != null)
            {
                List<OrderBO> oOrder = new List<OrderBO>();
                oOrder = (new OrderInvDA()).getOrder(Session["OrderID"].ToString());
                if (oOrder.Count > 0)
                {
                    ltrCustName.Text = oOrder[0].CustomerName;
                    ltrOrderDate.Text = Convert.ToString(oOrder[0].OrderDate);
                    ltrStatus.Text = oOrder[0].OrderStatus;
                    ltrSampleCount.Text = oOrder[0].SampleCount.ToString();
                    ltrOrderNo.Text = oOrder[0].OrderNumber;
                }
                PatSampleTestList1.OrderID = Session["OrderID"].ToString();
                PatSampleTestList1.popTesttracking();
            }
        }
    }
}