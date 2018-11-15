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
    public partial class GenSpecimenNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                popGenList();
            }
        }

        protected void btnTracking_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/receiving");
        }

        protected void btnSpecimenNum_Click(object sender, EventArgs e)
        {
            if (KoSoft.Utility.General.IsInteger(txtSpNos.Text))
            {
                if (rpGenNumbers.Items.Count > 0)
                    popGenList();
                List<SpecimenInfoBO> osNumber = (new SpecimenDA()).GetNextSpecimenNo(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(Session["UserID"]), Convert.ToInt32(txtSpNos.Text));
                rpGenNumbers.DataSource = osNumber;
                rpGenNumbers.DataBind();
            }
        }

        private void popGenList()
        {
            rpSpecimenNos.DataSource = (new SpecimenDA()).GetGeneratedSpecimenNos(Convert.ToInt32(Session["OrgID"]));
            rpSpecimenNos.DataBind();
        }

        protected void rpGenNumbers_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Link")
            {
                Session["SpecimenNo"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/pages/receiving");
            }
        }

        protected void rpSpecimenNos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void rpSpecimenNos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Session["SpecimenNo"] = Convert.ToString(e.CommandArgument);
                Response.Redirect("~/pages/receiving");
            }
        }
    }
}