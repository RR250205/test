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
    public partial class CourierIntegretion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["KitOrderID"]!="")
            {
                OrderKitBO orderKitBO = new OrderKitDA().GetOrderKitDetails(Request.QueryString["KitOrderID"]);
                if (orderKitBO.KitOrderID > 0)
                {
                    ltrName.Text = orderKitBO.FirstName + " " + orderKitBO.LastName;
                    ltrAddress1.Text = orderKitBO.Address;
                    ltrCity.Text = orderKitBO.City;
                    ltrState.Text = orderKitBO.State;
                    ltrCountry.Text = orderKitBO.Country;
                    ltrTelephone.Text = orderKitBO.Phone;
                    ltrZip.Text = orderKitBO.Zip;
                }
            }
        }
    }
}