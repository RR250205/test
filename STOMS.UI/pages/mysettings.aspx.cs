using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KoSoft.Entitlement;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.pages
{
    public partial class mysettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                popShortCuts();
            }
        }

        protected void lbtnGridAction_Click(object sender, EventArgs e)
        {

        }

        private void popShortCuts()
        {
            ddSC1.Items.Clear();
            ddSC2.Items.Clear();
            ddSC3.Items.Clear();
            ddSC4.Items.Clear();
            KoAccess objMnu = new KoAccess(STOMS.Common.Constant.DBConnectionString);
            List<EntitleServiceBO> objSrv = objMnu.GetUserAccessModulesOnly(Session["UserID"].ToString(), Convert.ToInt32(STOMS.Common.Constant.ProductID));
            ListItem li = new ListItem();
            foreach (EntitleServiceBO entSrv in objSrv)
            {
                if (entSrv.ResourceAction != "#")
                {
                    li = new ListItem(entSrv.ServiceName, entSrv.ResourceAction);
                    ddSC1.Items.Add(li);
                    ddSC2.Items.Add(li);
                    ddSC3.Items.Add(li);
                    ddSC4.Items.Add(li);
                }
                else
                {
                    if (entSrv.isChild)
                    {
                        List<EntitleServiceBO> srv = objMnu.GetUserAccessModuleServices(Session["UserID"].ToString(), entSrv.ServiceID);
                        foreach (EntitleServiceBO s1 in srv)
                        {
                            li = new ListItem(s1.ServiceName, s1.ResourceAction);
                            ddSC1.Items.Add(li);
                            ddSC2.Items.Add(li);
                            ddSC3.Items.Add(li);
                            ddSC4.Items.Add(li);
                        }
                    }
                }
            }
        }

        protected void hActionType_ValueChanged(object sender, EventArgs e)
        {
            switch (hActionType.Value)
            {
                case "SC":
                    MySettingBO objSet = new MySettingBO();
                    objSet.UserID = Convert.ToInt32(Session["UserID"]);
                    objSet.ShortCutURL1 = ddSC1.SelectedValue;
                    objSet.ShortCutURL2 = ddSC2.SelectedValue;
                    objSet.ShortCutURL3 = ddSC3.SelectedValue;
                    objSet.ShortCutURL4 = ddSC4.SelectedValue;
                    (new STOMS.DA.AdminDA()).SavingMySettings(objSet);
                    break;
                case "SB":
                    //(new CorpEntity()).UpdateMyDefaultEntity(Session["userID"].ToString(), ddEntity.SelectedValue);
                    //((SessionBO)Session["MySession"]).ActiveEntityID = Convert.ToInt32(ddEntity.SelectedValue);
                    break;
            }
        }
    }
}