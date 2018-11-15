using STOMS.BO;
using STOMS.DA;
using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace STOMS.UI.pages
{
    public partial class orderMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetWizStep(Convert.ToInt16(hStep.Value));
                popOrderDetails();

                ddPhy.DataSource = (new CustomerDA()).GetCustomer(Convert.ToInt32(Session["OrgID"]));
                ddPhy.DataBind();
            }
        }

        protected void lbtnNew_Click(object sender, EventArgs e)
        {

        }

        protected void lbtnExisting_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveConfirm_Click(object sender, EventArgs e)
        {

        }

        protected void btnInitOrder_Click(object sender, EventArgs e)
        {
            showMessage1.setErrorMsg("");
            OrderBO oOrder = new OrderBO();
            switch (hStep.Value)
            {
                case "1":
                    if (Convert.ToInt32(hOrderID.Value) == 0)
                    {
                        CustomerBO objCust = new CustomerBO();
                        objCust.CustomerID = Convert.ToInt32(hCustID.Value);
                        objCust.CustomerName = txtPhyName.Text.Trim();
                        objCust.Address1 = txtPhyAddress1.Text.Trim();
                        objCust.Address2 = "";
                        objCust.City = txtPhyCity.Text.Trim();
                        objCust.State = txtPhyState.Text.Trim();
                        objCust.Zipcode = txtPhyPCode.Text.Trim();
                        objCust.Country = ddCountry.SelectedValue;
                        objCust.Email = txtPhyEmail.Text.Trim();
                        objCust.Phone = txtPhyPhone.Text.Trim();
                        objCust.Fax = txtPhyFax.Text.Trim();
                        objCust.Status = "Active";
                        hCustID.Value = ((new OrderInvDA()).SaveOrderDA(objCust)).ToString();

                        dvSel.Visible = false;

                        oOrder.OrderID = Convert.ToInt32(hOrderID.Value);
                        oOrder.OrderDate = DateTime.Now;
                        oOrder.CustomerID = Convert.ToInt32(hCustID.Value);
                        oOrder.OrderStatus = "Initiate";
                        oOrder.ShipOption = ddResultDel.SelectedValue;
                        oOrder.DeliveryEmail = txtDelEmail.Text.Trim();
                        oOrder.ShipName = txtDelAddressName.Text.Trim();
                        oOrder.ShipAddress1 = txtDelAddress.Text.Trim();
                        oOrder.ShipAddress2 = "";
                        oOrder.ShipCity = txtDelCity.Text.Trim();
                        oOrder.ShipState = txtDelState.Text.Trim();
                        oOrder.ShipZipCode = txtDelZip.Text.Trim();
                        oOrder.ShipCountry = "USA";

                        hOrderID.Value = ((new OrderInvDA()).SaveOrderDA(oOrder)).ToString();
                        PatSampleTestList1.OrderID = hOrderID.Value;
                        Session["OrderID"] = hOrderID.Value;
                    }
                    break;
                case "2":
                    if (chkBind.Checked || chkBlock.Checked)
                    {
                        List<PatientBO> oPat = new List<PatientBO>();
                        if (dvRow1.Visible) oPat.Add(AddPatToCollection("1"));
                        if (dvRow2.Visible) oPat.Add(AddPatToCollection("2"));
                        if (dvRow3.Visible) oPat.Add(AddPatToCollection("3"));
                        if (dvRow4.Visible) oPat.Add(AddPatToCollection("4"));
                        if (dvRow5.Visible) oPat.Add(AddPatToCollection("5"));
                        if (dvRow6.Visible) oPat.Add(AddPatToCollection("6"));
                        if (dvRow7.Visible) oPat.Add(AddPatToCollection("7"));
                        if (dvRow8.Visible) oPat.Add(AddPatToCollection("8"));
                        if (dvRow9.Visible) oPat.Add(AddPatToCollection("9"));
                        if (dvRow10.Visible) oPat.Add(AddPatToCollection("10"));
                        (new OrderInvDA()).SaveOrderDA(oPat, chkBind.Checked, chkBlock.Checked);
                    }
                    else
                    {
                        showMessage1.setErrorMsg("Atleast one of the test option need to be selected");
                    }
                    break;
                case "3":
                    oOrder.OrderID = Convert.ToInt32(hOrderID.Value);
                    oOrder.OrderDate = DateTime.Now;
                    oOrder.CustomerID = Convert.ToInt32(hCustID.Value);
                    oOrder.OrderStatus = "Delivery Info";
                    oOrder.ShipOption = ddResultDel.SelectedValue;
                    oOrder.DeliveryEmail = txtDelEmail.Text;
                    oOrder.ShipName = txtDelAddressName.Text;
                    oOrder.ShipAddress1 = txtDelAddress.Text;
                    oOrder.ShipAddress2 = "";
                    oOrder.ShipCity = txtDelCity.Text;
                    oOrder.ShipState = txtDelState.Text;
                    oOrder.ShipZipCode = txtDelState.Text;
                    oOrder.ShipCountry = ddDelCountry.SelectedValue;
                    (new OrderInvDA()).SaveOrderDA(oOrder);

                    break;
                case "4":
                    break;
            }
            hStep.Value = (Convert.ToInt16(hStep.Value) + 1).ToString();
            SetWizStep(Convert.ToInt16(hStep.Value));
        }

        private PatientBO AddPatToCollection(string dvNo)
        {
            Control cntrl = dvPat.FindControl("dvRow" + dvNo);
            PatientBO oP1 = new PatientBO
            {
                PatientID = Convert.ToInt32(((HiddenField)(cntrl.FindControl("hPatID" + dvNo))).Value),
                PatientName = ((TextBox)(cntrl.FindControl("txtPatName" + dvNo))).Text,
                Gender = ((DropDownList)(cntrl.FindControl("ddGender" + dvNo))).SelectedValue,
                DOB = ((TextBox)(cntrl.FindControl("txtDOB" + dvNo))).Text,
                Diagnosis = ((TextBox)(cntrl.FindControl("txtDiag" + dvNo))).Text,
                OrderID = Convert.ToInt32(hOrderID.Value)
            };
            return oP1;
        }

        protected void tgrdAddItems_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {

        }

        protected void btnStatusUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void lnkAction_Click(object sender, EventArgs e)
        {

        }

        protected void hActValue_ValueChanged(object sender, EventArgs e)
        {

        }

        protected void ddNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddNew.SelectedValue == "New")
                ddPhy.Visible = false;
            else
                ddPhy.Visible = true;
        }

        protected void ddPhy_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CustomerBO> objCust = (new CustomerDA()).GetCustomer(ddPhy.SelectedValue);
            if (objCust.Count > 0)
            {
                txtPhyName.Text = objCust[0].CustomerName;
                txtPhyAddress1.Text = objCust[0].Address1;
                txtPhyCity.Text = objCust[0].City;
                txtPhyState.Text = objCust[0].State;
                txtPhyEmail.Text = objCust[0].Email;
                txtPhyFax.Text = objCust[0].Fax;
                txtPhyPCode.Text = objCust[0].Zipcode;
                txtPhyPhone.Text = objCust[0].Phone;
                hCustID.Value = objCust[0].CustomerID.ToString();
            }
        }

        private void SetWizStep(int step)
        {
            btnInitOrder.Visible = true;
            if (step < 1 || step > 4)
            {
                step = 1;
                hStep.Value = "1";
            }
            dvPhy.Visible = dvPat.Visible = dvShip.Visible = dvPayment.Visible = false;
            li1.Attributes.Remove("class");
            li2.Attributes.Remove("class");
            li3.Attributes.Remove("class");
            li4.Attributes.Remove("class");
            switch (step)
            {
                case 1:
                    li1.Attributes.Add("class", "active");
                    dvPhy.Visible = true;
                    btnInitOrder.Text = "Save Physician/Company Info";
                    btnPrev.Visible = false;
                    btnNext.Visible = (hOrderID.Value == "0" ? false : true);
                    break;
                case 2:
                    li2.Attributes.Add("class", "active");
                    dvPat.Visible = true;
                    btnInitOrder.Text = "Save patient Info";
                    btnPrev.Visible = true;
                    btnNext.Visible = true;
                    break;
                case 3:
                    li3.Attributes.Add("class", "active");
                    dvShip.Visible = true;
                    btnInitOrder.Text = "Save Results";
                    btnPrev.Visible = true;
                    btnNext.Visible = true;
                    break;
                case 4:
                    btnInitOrder.Visible = false;
                    li4.Attributes.Add("class", "active");
                    dvPayment.Visible = true;
                    btnInitOrder.Text = "Save Payment Details";
                    btnPrev.Visible = true;
                    btnNext.Visible = false;
                    break;
            }
        }

        protected void btnMore_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            //string currRow = btn.CommandArgument.ToString();
            int currRow = Convert.ToInt32(btn.CommandArgument);
            btn.Visible = false;
            //HtmlGenericControl dv = (HtmlGenericControl)this.FindControl("dvRow" + (currRow + 1).ToString());
            switch (currRow)
            {
                case 1:
                    dvRow2.Visible = true;
                    break;
                case 2:
                    dvRow3.Visible = true;
                    break;
                case 3:
                    dvRow4.Visible = true;
                    break;
                case 4:
                    dvRow5.Visible = true;
                    break;
                case 5:
                    dvRow6.Visible = true;
                    break;
                case 6:
                    dvRow7.Visible = true;
                    break;
                case 7:
                    dvRow8.Visible = true;
                    break;
                case 8:
                    dvRow9.Visible = true;
                    break;
                case 9:
                    dvRow10.Visible = true;
                    break;
            }
        }

        protected void ddResultDel_SelectedIndexChanged(object sender, EventArgs e)
        {
            dvDelEmail.Visible = false;
            dvDelMail.Visible = false;
            switch (ddResultDel.SelectedIndex)
            {
                case 0:
                    dvDelEmail.Visible = true;
                    break;
                case 1:
                    dvDelMail.Visible = true;
                    break;
                case 2:
                    dvDelEmail.Visible = true;
                    dvDelMail.Visible = true;
                    break;
            }
        }

        private void popOrderDetails()
        {
            if (Session["OrderID"] != null)
            {
                btnNext.Visible = true;
                hOrderID.Value = Convert.ToString(Session["OrderID"]);
                CustomerDA objOrder = new CustomerDA();
                List<CustomerBO> oCust = objOrder.GetCustomerByOrderID(hOrderID.Value);
                if (oCust.Count > 0)
                {
                    txtPhyName.Text = oCust[0].CustomerName;
                    txtPhyAddress1.Text = oCust[0].Address1;
                    txtPhyCity.Text = oCust[0].City;
                    txtPhyState.Text = oCust[0].State;
                    txtPhyEmail.Text = oCust[0].Email;
                    txtPhyFax.Text = oCust[0].Fax;
                    txtPhyPCode.Text = oCust[0].Zipcode;
                    txtPhyPhone.Text = oCust[0].Phone;
                    hCustID.Value = oCust[0].CustomerID.ToString();
                    dvSel.Visible = false;
                }

                List<TestResultBO> objPat = (new OrderInvDA()).GetPatientByOrderID(hOrderID.Value);
                if (objPat.Count > 0)
                {
                    int dv = 1;
                    foreach (TestResultBO oP in objPat)
                    {
                        SetPatRecord(dv.ToString(), oP.oPatient);
                        dv++;
                    }
                    chkBind.Checked = objPat[0].IsFolateBinding;
                    chkBlock.Checked = objPat[0].IsFolateBlocking;
                }
                PatSampleTestList1.OrderID = hOrderID.Value;
                PatSampleTestList1.popTesttracking();

                List<OrderBO> oOrder = (new OrderInvDA()).getOrder(hOrderID.Value);
                if (oOrder.Count > 0)
                {
                    ddResultDel.SelectedValue = oOrder[0].ShipOption;
                    txtDelEmail.Text = oOrder[0].DeliveryEmail;
                    txtDelAddressName.Text = oOrder[0].ShipName;
                    txtDelAddress.Text = oOrder[0].ShipAddress1;
                    txtDelCity.Text = oOrder[0].ShipCity;
                    txtDelState.Text = oOrder[0].ShipState;
                    txtDelZip.Text = oOrder[0].ShipZipCode;
                }
            }
        }

        private void SetPatRecord(string dvNo, PatientBO oPat)
        {
            if (Convert.ToInt16(dvNo) < 11)
            {
                Control cntrl = dvPat.FindControl("dvRow" + dvNo);
                ((TextBox)(cntrl.FindControl("txtPatName" + dvNo))).Text = oPat.PatientName;
                ((DropDownList)(cntrl.FindControl("ddGender" + dvNo))).SelectedValue = oPat.Gender;
                ((TextBox)(cntrl.FindControl("txtDOB" + dvNo))).Text = oPat.DOB;
                ((TextBox)(cntrl.FindControl("txtDiag" + dvNo))).Text = oPat.Diagnosis;
                ((HiddenField)(cntrl.FindControl("hPatID" + dvNo))).Value = Convert.ToString(oPat.PatientID);
                if (Convert.ToInt16(dvNo) < 10)
                    ((Button)(cntrl.FindControl("btnMore" + dvNo))).Visible = false;
                cntrl.Visible = true;
            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            hStep.Value = (Convert.ToInt16(hStep.Value) - 1).ToString();
            SetWizStep(Convert.ToInt16(hStep.Value));
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            hStep.Value = (Convert.ToInt16(hStep.Value) + 1).ToString();
            SetWizStep(Convert.ToInt16(hStep.Value));
        }
    }
}