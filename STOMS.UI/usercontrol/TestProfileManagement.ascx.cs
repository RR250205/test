using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.DA;
using STOMS.BO;
using STOMS.Common;

namespace STOMS.UI.usercontrol
{
    public partial class TestProfileManagement : System.Web.UI.UserControl
    {
        private string _activeParentTab = "";

        public string ActiveParentTab
        {
            get { return _activeParentTab; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void SaveTestProfiles()
        {

        }

        protected void chklstTestProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _activeParentTab = "TestProfile";

        }
    }
}