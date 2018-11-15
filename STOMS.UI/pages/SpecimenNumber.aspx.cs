using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.pages
{
    public partial class SpecimenNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       


        protected void btnSpecimenNum_Click(object sender, EventArgs e)
        {
            SpceimenNumBO obspn = new SpceimenNumBO();
            obspn.SpecimenNum = Convert.ToString(new OrderInvDA().CreateSpecimenNum());
            ltrSpecimenNumber.Text = obspn.SpecimenNum;

            dvShowNumber.Visible = true;
            dvTodaySpecimenlist.Visible = false;
        }

        protected void btnTodaySpecimenList_Click(object sender, EventArgs e)
        {
            popTodaySpecimenList();

            dvShowNumber.Visible = false;
            dvTodaySpecimenlist.Visible = true;
        }

        protected void tgrdSpmlist_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

        }

        protected void tgrdSpmlist_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {

        }

        protected void tgrdSpmlist_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {

        }

        protected void tgrdSpmlist_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }
        private void popTodaySpecimenList()
        {

            tgrdSpmlist.DataSource = (new OrderInvDA()).getSpecimenNum();
            tgrdSpmlist.DataBind();

        }

        protected void BtnNextSpecimen_Click(object sender, EventArgs e)
        {

        }
    }
}