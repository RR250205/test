using STOMS.BO;
using STOMS.DA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace STOMS.UI.usercontrol
{
    public partial class EmailConfiguration : System.Web.UI.UserControl
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                rptEmailEnablementTypes.DataSource = new EmailConfigurationDA().getEmailEnablementType();
                rptEmailEnablementTypes.DataBind();
            }            
        }

        protected void rptEmailEnablementTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    EmailEnablementBO emailEnablementBO = new EmailConfigurationDA().getEmailEnablement(Convert.ToInt32(Session["OrgID"]), ((EmailEnablementTypeBO)e.Item.DataItem).EmailEnablementTypeID);
                    CheckBox chkToEndUser = (CheckBox)e.Item.FindControl("chkToEndUser");
                    chkToEndUser.Checked = emailEnablementBO.isToEndUser;
                    CheckBox chkToTenant = (CheckBox)e.Item.FindControl("chkToTenant");
                    chkToTenant.Checked = emailEnablementBO.isToTenant;
                    TextBox txtToTenantEmails = (TextBox)e.Item.FindControl("txtToTenantEmails");
                    txtToTenantEmails.Text = emailEnablementBO.ToTenantEmails;
                LinkButton lbtn = (LinkButton)e.Item.FindControl("lbtnUpdate");
                lbtn.CommandArgument = emailEnablementBO.EmailEnablementID.ToString();
                if (((EmailEnablementTypeBO)e.Item.DataItem).EmailEnablementTypeID == 2)
                {
                    chkToTenant.Visible = false;
                    txtToTenantEmails.Visible = false;
                }
                if (((EmailEnablementTypeBO)e.Item.DataItem).EmailEnablementTypeID == 3)
                {
                    chkToEndUser.Visible = false;
                }
                if (((EmailEnablementTypeBO)e.Item.DataItem).EmailEnablementTypeID == 4)
                {
                    chkToTenant.Visible = false;
                    txtToTenantEmails.Visible = false;
                }
                if (chkToTenant.Checked)
                {
                    txtToTenantEmails.Visible = true;
                }
 
            }
            
        }

        protected void rptEmailEnablementTypes_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                if (e.CommandName != "ViewTemplate")
                {
                    HtmlGenericControl ltr = (HtmlGenericControl)e.Item.FindControl("spError");
                    if (((CheckBox)e.Item.FindControl("chkToTenant")).Visible&& ((CheckBox)e.Item.FindControl("chkToTenant")).Checked)
                    {
                     
                        string email = ((TextBox)e.Item.FindControl("txtToTenantEmails")).Text;
                        if (email==null || email == "")
                        {
                            ltr.Visible = true;
                            ltr.InnerText = "Email ID Required";
                            return;
                        }
                        else
                        {
                            try
                            {
                                var addr = new System.Net.Mail.MailAddress(email);
                                if( addr.Address == email)
                                {

                                }
                            }
                            catch
                            {
                                ltr.Visible = true;
                                ltr.InnerText = "Invalid Email";
                                return;
                            }
                        }

                    }
                    int emailEnablementID = new EmailConfigurationDA().SaveEmailEnablement(new EmailEnablementBO()
                    {
                        EmailEnablementID = Convert.ToInt32(e.CommandArgument),
                        isToEndUser = ((CheckBox)e.Item.FindControl("chkToEndUser")).Checked,
                        isToTenant = ((CheckBox)e.Item.FindControl("chkToTenant")).Checked,
                        ToTenantEmails = ((TextBox)e.Item.FindControl("txtToTenantEmails")).Text
                    },
                    Convert.ToInt32(((HiddenField)e.Item.FindControl("hEmailEnablementTypeID")).Value),
                    Convert.ToInt32(Session["OrgID"])
                    );
                    ltr.Visible = false;
                    ((HiddenField)e.Item.FindControl("hEmailEnablementTypeID")).Value = emailEnablementID.ToString();
                }
                else
                {
                    List<EmailEnablementTypeBO> emailTypeBO = new EmailConfigurationDA().getEmailEnablementType(Convert.ToInt32(((HiddenField)e.Item.FindControl("hEmailEnablementTypeID")).Value));
                    
                    if (emailTypeBO.Count > 0)
                    {
                        dvEmailContent.Visible = true;
                        ltrEmailEnablementType.Text=ltrEmailEnablementTypeTenant.Text = emailTypeBO[0].EmailEnablementType;
                        if (emailTypeBO[0].EndUserTemplate.Trim() != String.Empty)
                        {
                            ltrEndUserTemplate.Text = emailTypeBO[0].EndUserTemplate;
                            dvEndUserTempalte.Visible = true;
                        }
                        else if(emailTypeBO[0].EndUserTemplate.Trim() == String.Empty)
                        {
                            dvEndUserTempalte.Visible = false;
                        }
                        if (emailTypeBO[0].TenantTemplate.Trim() != String.Empty)
                        {
                            ltrTenantTemplate.Text = emailTypeBO[0].TenantTemplate;
                            dvTenantTemplate.Visible = true;
                        }
                        else if (emailTypeBO[0].TenantTemplate.Trim() == String.Empty)
                        {
                            dvTenantTemplate.Visible = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        protected void chkToEndUser_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            HtmlGenericControl ltr = (HtmlGenericControl)cb.Parent.FindControl("spError");
            ltr.Visible = false;
            if (cb.Text=="To Tenant"&& cb.Checked)
            {
                TextBox txt = (TextBox)cb.Parent.FindControl("txtToTenantEmails");
                txt.Visible = true;
            }
            else if(cb.Text == "To Tenant" && !cb.Checked)
            {
                TextBox txt = (TextBox)cb.Parent.FindControl("txtToTenantEmails");
                txt.Visible = false;
            }            
        }
    }
}