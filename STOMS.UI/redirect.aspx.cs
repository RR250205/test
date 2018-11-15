using KoSoft.Entitlement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI
{
    public partial class redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (Request["ID"] != null)
                {

                    KoAccess objAccess = new KoAccess(Common.Constant.DBConnectionString);
                    List<EntitleServiceBO> objSrv = objAccess.GetServiceDetail(Convert.ToInt32(Request["ID"]));
                    if (objSrv.Count > 0)
                    {
                        Session["serviceID"] = Request["ID"].ToString();
                        Response.Redirect(objSrv[0].ResourceAction);
                    }
                }
            }
        }
    }

}