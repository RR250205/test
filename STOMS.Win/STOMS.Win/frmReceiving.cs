using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using STOMS.DA;
using STOMS.BO;
using KoSoft.Entitlement;
namespace STOMS.Win
{
    public partial class frmReceiving : Form
    {
        public frmReceiving()
        {
            InitializeComponent();
        }

        private void btnGenerator_Click(object sender, EventArgs e)
        {

            dgvSpecimenNo.DataSource = null;
            dgvSpecimenNo.AutoGenerateColumns = false;
            dgvSpecimenNo.ColumnCount = 1;
            dgvSpecimenNo.RowHeadersVisible = false;

            dgvSpecimenNo.Columns[0].HeaderText = "Specimen Number";
            dgvSpecimenNo.Columns[0].Name = "SpecimenNumber";
            dgvSpecimenNo.Columns[0].DataPropertyName = "SpecimenNumber";
            dgvSpecimenNo.Columns[0].Width = 240;
            dgvSpecimenNo.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSpecimenNo.ColumnHeadersVisible = false;

            List<SpecimenInfoBO> osNumber = (new SpecimenDA()).GetNextSpecimenNo(STOMS.Common.Global.TenantID, STOMS.Common.Global.UserID, Convert.ToInt32(txtNoOfValues.Text));
            dgvSpecimenNo.DataSource = osNumber;
            UpdateFont();
            setRowHeight();
        }

        private void UpdateFont()
        {
            //Change cell font
            foreach (DataGridViewColumn c in dgvSpecimenNo.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Arial", 24.5F, GraphicsUnit.Pixel);
            }
        }

        private void setRowHeight()
        {
            foreach (DataGridViewRow row in dgvSpecimenNo.Rows)
            {
                row.Height = 50;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            List<AppUserBO> objUsr = (new KoAccess(STOMS.Common.Constant.DBConnectionString)).GetAppUser(txtUserName.Text.Trim(), KoSoft.Utility.KoCrypt.Encrypt(txtPassword.Text.Trim()), "Win-" + txtUserName.Text.Trim(), Convert.ToInt32(STOMS.Common.Constant.ProductID));
            if (objUsr.Count > 0)
            {
                if (objUsr[0].ErrorMsg.ErrorCode == 0)
                {

                    STOMS.Common.Global.UserName = objUsr[0].FullName;
                    STOMS.Common.Global.UserID = objUsr[0].AppUserID;
                    STOMS.Common.Global.TenantID = objUsr[0].Company.TenantID;

                    gbxLeft.Visible = true;
                    gbxRight.Visible = true;
                    gbxLogin.Visible = false;
                    //Session["FullName"] = objUsr[0].FullName; 
                    //Session["OrgName"] = objUsr[0].Company.TenantName;
                    //Session["OrgID"] = objUsr[0].Company.TenantID;
                    //Session["ActYear"] = DateTime.Now.Year;
                    //Session["ActMonth"] = DateTime.Now.Month;
                    //Session["menuID"] = 0;
                    //Session["UserID"] = objUsr[0].AppUserID;
                }
                else
                    ltrMsg.Text = objUsr[0].ErrorMsg.ErrorMsg;
            }
            else
                ltrMsg.Text = "Authentication failed...";
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
