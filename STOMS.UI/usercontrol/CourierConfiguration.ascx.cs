using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.usercontrol
{
    public partial class CourierConfiguration : System.Web.UI.UserControl
    {
        //private object chkCourierList;
        private bool _onLoad;
        public void Refresh()
        {
            if (this._onLoad)
            {
           
                rptcourierConfiguration.DataSource = new EmailConfigurationDA().GetCourierType();
                rptcourierConfiguration.DataBind();
            }
        }
       public bool onLoad
        {
            get { return _onLoad; }
            set
            {
                _onLoad = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
           
         }

        protected void rptcourierConfiguration_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                CourierConfigurationBO courierconfigurationBO = new EmailConfigurationDA().GetCourierTenant(Convert.ToInt32(Session["OrgID"]), ((CourierConfigurationBO)e.Item.DataItem).CourierID);
                           

                if (((CourierConfigurationBO)e.Item.DataItem).CourierID == courierconfigurationBO.CourierID)
                {
                   
                    dvCourierTypes.Visible = true;
                    CheckBox chk = (CheckBox)e.Item.FindControl("chkCourierType");
                    HiddenField hf = (HiddenField)e.Item.FindControl("hCourierTenantId");
                    hf.Value = Convert.ToString( courierconfigurationBO.CourierTenantID);
                    chk.Checked = true;
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    //li.ID = "tabFedex";
                    //if(e.Item.ItemIndex==0)
                        //li.Attributes.Add("class","active");
                    HtmlGenericControl a = new HtmlGenericControl("a");
                    a.Attributes.Add("href", "#tab"+ ((CourierConfigurationBO)e.Item.DataItem).CourierName.Trim());
                    a.Attributes.Add("data-toggle", "tab");
                    a.InnerText = ((CourierConfigurationBO)e.Item.DataItem).CourierName;
                    li.Controls.Add(a);
                    nvtbcourier.Controls.Add(li);
                    
                    ((HtmlGenericControl)nvtbcourier.Controls[1]).Attributes.Add("class", "active");
                    //li.Visible = true;
                    CourierConfigBO courierBO = new EmailConfigurationDA().getcourierdetail(Convert.ToInt32(Session["OrgID"]));

                    if (courierBO.CourierConfigID != null && courierBO.CourierConfigID>0)
                    {
                        txtFedexAcno.Text = courierBO.FedexACNo.ToString();
                        txtMeterno.Text = courierBO.FedexMeterNo.ToString();
                        txtParentKey.Text = courierBO.FedexParentKey.ToString();
                        txtParentPassword.Text = courierBO.FedexParentPassword.ToString();
                        txtUserkey.Text = courierBO.FedexUserKey.ToString();
                        txtDefaltWeight.Text = courierBO.DefaultWeight.ToString();
                        chkSignature.Checked = Convert.ToString(courierBO.SignatureOn) == "True" ? true : false;
                        txtUserPassword.Text = courierBO.FedexUserPassword.ToString();


                        hCourierConfigID.Value = Convert.ToString(courierBO.CourierConfigID);
                    }

                }
                else
                {
                    // dvCourierTypes.Visible = false;
                    lblinstruct.Visible = false;
                }
                //else if (((CourierConfigurationBO)e.Item.DataItem).CourierID == courierconfigurationBO.CourierID)
                //{
                //    CheckBox chk = (CheckBox)e.Item.FindControl("chkCourierType");
                //    chk.Checked = true;
                //    HtmlGenericControl li = new HtmlGenericControl("li");
                //    li.ID = "tabUsps";
                //    HtmlGenericControl a = new HtmlGenericControl("a");
                //    a.Attributes.Add("href", "#tabUsps");
                //    a.Attributes.Add("data-toggle", "tab");
                //    a.InnerText = ((CourierConfigurationBO)e.Item.DataItem).CourierName;
                //    li.Controls.Add(a);
                //    nvtbcourier.Controls.Add(li);
                //    li.Visible = true;
                //}
            }
        }
        protected void chkCourierType_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox cb = (CheckBox)sender;
            int count = 0;
            nvtbcourier.Controls.Clear();
            foreach (RepeaterItem item in rptcourierConfiguration.Items)
            {
                lblinstruct.Visible = false;
                CheckBox rptChk = (CheckBox) item.FindControl("chkCourierType");
                if (rptChk!=null&& rptChk.Checked)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    //li.ID = "tabFedex";
                    if(count==0)
                        li.Attributes.Add("class", "active");
                    HtmlGenericControl a = new HtmlGenericControl("a");
                    a.Attributes.Add("href", "#tab"+rptChk.Text.Trim());
                    a.Attributes.Add("data-toggle", "tab");
                    a.InnerText = rptChk.Text.Trim();
                    li.Controls.Add(a);
                   
                    nvtbcourier.Controls.Add(li);
                   // ((HtmlGenericControl)nvtbcourier.Controls[1]).Attributes.Add("class", "active");
                    //li.Visible = true;
                    count++;
                    HiddenField hfCourierID= (HiddenField)item.FindControl("hCourierID");
                    HiddenField hfCourierTenantID = (HiddenField)item.FindControl("hCourierTenantID");

                    CourierConfigurationBO cbo = new CourierConfigurationBO();
                    cbo.CourierID = Convert.ToInt32(hfCourierID.Value);
                    cbo.CourierTenantID = Convert.ToInt32(hfCourierTenantID.Value);

                    int rtnData=new EmailConfigurationDA().SaveCourierTenant(Convert.ToInt32(Session["OrgID"]), cbo);
                    if (rtnData > 0)
                    {
                        hfCourierTenantID.Value = Convert.ToString(rtnData);
                    }
                    
                }
                else if (rptChk != null && !rptChk.Checked)
                {
                    HiddenField hf = (HiddenField)item.FindControl("hCourierID");
                    HiddenField hfCourierTenantID = (HiddenField)item.FindControl("hCourierTenantID");
                    new EmailConfigurationDA().deleteCourierConfig(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hf.Value));
                    // hfCourierTenantID.Value = Convert.ToString(0);


                    //if(hCourierConfigID.Value>0)            
                    //HiddenField hfCourierConfigID = (HiddenField)item.FindControl("hCourierConfigID");                    

                    if (rptChk.Text == "FedEx")
                    {
                        new EmailConfigurationDA().deleteCourierConfiguration(Convert.ToInt32(Session["OrgID"]), Convert.ToInt32(hCourierConfigID.Value));
                        hCourierConfigID.Value = "0";
                    }
                }
               
            }
            if (count == 0)
            {
                lblinstruct.Visible = true;
                dvCourierTypes.Visible = false;
            }
            else
            {
                lblinstruct.Visible = false;
                dvCourierTypes.Visible = true;
            }
           
            
        }

        protected void rptcourierConfiguration_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int courierconfig = (new EmailConfigurationDA()).SaveCourierConfiguration(new CourierConfigBO
                {
                    CourierConfigID =Convert.ToInt32( hCourierConfigID.Value),
                    FedexMeterNo = Convert.ToInt32(txtMeterno.Text),
                    FedexACNo = Convert.ToInt32(txtFedexAcno.Text),
                    FedexUserKey= txtUserkey.Text.Trim(),
                    FedexUserPassword= txtUserPassword.Text.Trim(),
                    FedexParentKey= txtParentKey.Text.Trim(),
                    FedexParentPassword= txtParentPassword.Text.Trim(),
                    DefaultWeight= txtDefaltWeight.Text.Trim(),
                    SignatureOn= chkSignature.Checked,
                    TenantID= Convert.ToInt32(Session["OrgID"]),
                });
                hCourierConfigID.Value = courierconfig.ToString();
                int count = 0;
                nvtbcourier.Controls.Clear();
                foreach (RepeaterItem item in rptcourierConfiguration.Items)
                {
                    lblinstruct.Visible = false;
                    CheckBox rptChk = (CheckBox)item.FindControl("chkCourierType");
                    if (rptChk != null && rptChk.Checked)
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        //li.ID = "tabFedex";
                        if(count==0)
                            li.Attributes.Add("class", "active");
                        HtmlGenericControl a = new HtmlGenericControl("a");
                        a.Attributes.Add("href", "#tab" + rptChk.Text.Trim());
                        a.Attributes.Add("data-toggle", "tab");
                        a.InnerText = rptChk.Text.Trim();
                        li.Controls.Add(a);

                        nvtbcourier.Controls.Add(li);
                       // ((HtmlGenericControl)nvtbcourier.Controls[0]).Attributes.Add("class", "active");
                        //li.Visible = true;
                        count++;
                        
                    }
                  

                }
                if (count == 0)
                {
                    lblinstruct.Visible = true;
                    dvCourierTypes.Visible = false;
                }
                else
                {
                    lblinstruct.Visible = false;
                    dvCourierTypes.Visible = true;
                }

            }
            catch (Exception ex)
            {
                string errmsg = ex.Message;
            }

        }

        protected void btnview_Click(object sender, EventArgs e)
        {
           
            //  dvCourierconfigurations.Visible = true;


            //litEditMeterno.Text = txtMeterno.Text.Trim();
            //litEditFedexAcno.Text = txtFedexAcno.Text.Trim();
            //litEditUserkey.Text = txtUserkey.Text.Trim();
            //litEditUserPassword.Text = txtUserPassword.Text.Trim();
            //litEditParentKey.Text = txtParentKey.Text.Trim();
            //litEditParentPassword.Text = txtParentPassword.Text.Trim();
            //litEditDefaltWeight.Text = txtDefaltWeight.Text.Trim();
            //litEditSignature.Text = chkSignature.Text.Trim();




        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //tabFedEx.Visible = true;
            dvCourierconfigurations.Visible = false;

            txtMeterno.Text = litEditMeterno.Text;
           txtFedexAcno.Text = litEditFedexAcno.Text;
            txtUserkey.Text = litEditUserkey.Text;
            txtUserPassword.Text = litEditUserPassword.Text;
            txtParentKey.Text = litEditParentKey.Text;
            txtParentPassword.Text = litEditParentPassword.Text;
            txtDefaltWeight.Text = litEditDefaltWeight.Text;
            chkSignature.Text = litEditSignature.Text;
            //txtMeterno1.Text = litEditMeterno.Text;
            //txtFedexAcno1.Text = litEditFedexAcno.Text;
            //txtUserkey1.Text = litEditUserkey.Text;
            //txtUserPassword1.Text = litEditUserPassword.Text;
            //txtParentKey1.Text = litEditParentKey.Text;
            //txtParentPassword1.Text = litEditParentPassword.Text;
            //txtDefaltWeight1.Text = litEditDefaltWeight.Text;
            //txtSignature1.Text = litEditSignature.Text;


        }
    }





}




    









