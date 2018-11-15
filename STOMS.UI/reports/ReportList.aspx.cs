using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using STOMS.BO;
using STOMS.DA;
using System.IO;

namespace STOMS.UI.reports
{
    public partial class ReportList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                Session["PgContentTitle"] = "Reports";
                ddlStatus.SelectedValue = "0";
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            //required to avoid the runtime error "  
            //Control 'gdvwSpecimenReport' of type 'GridView' must be placed inside a form tag with runat=server."  
        }

        //Passing Parametrs and Data Bind
        private void getSpecimenReport()
        {
            btnSpecimenExportToExcel.Visible = false;
            lblReport.Text = "";
            gdvwSpecimenReport.DataSource = null;
            gdvwSpecimenReport.DataBind();

            if (txtFromDate.Text == "" && txtToDate.Text == "" && ddlStatus.SelectedValue == "0")
            {
                lblReport.Text = "Invalid input value";
            }
            else if ((txtFromDate.Text != "" && txtToDate.Text == "" && ddlStatus.SelectedValue == "0") || (txtFromDate.Text != "" && txtToDate.Text == "" && ddlStatus.SelectedValue != null))
            {
                lblReport.Text = "Invalid ToDate value";
            }
            else if ((txtFromDate.Text == "" && txtToDate.Text != "" && ddlStatus.SelectedValue == "0") || (txtFromDate.Text == "" && txtToDate.Text != "" && ddlStatus.SelectedValue != null))
            {
                lblReport.Text = "Invalid From Date value";
            }
            else
            {
                btnSpecimenExportToExcel.Visible = true;
                List<SpecimenReportBO> specimenReportBO = (new OrderKitDA()).getSpecimenReport(Convert.ToInt32(Session["OrgId"]), txtFromDate.Text, txtToDate.Text, Convert.ToString(ddlStatus.SelectedItem.Text));
                if (specimenReportBO.Count > 0)
                {
                    gdvwSpecimenReport.DataSource = specimenReportBO;
                    gdvwSpecimenReport.DataBind();
                }
                else
                {
                    lblReport.Text = "No Data is Available";
                    btnSpecimenExportToExcel.Visible = false;
                }
            }
        }         
        
        protected void btnGetSpecimenReport_Click(object sender, EventArgs e)
        {
            specimenTab.Attributes.Add("class", "active");
            tabSpecimenReporting.Attributes.Add("class", "tab-pane active");
            ActiveClientTab.Attributes.Add("class", "Inactive");
            tabActiveClients.Attributes.Add("class", "tab-pane fade");
            getSpecimenReport();
        }

        protected void btnSpecimenExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Specimen Report" + " " + DateTime.Now + ".xls";
            StringWriter strwriter = new StringWriter();
            HtmlTextWriter htmltextwriter = new HtmlTextWriter(strwriter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gdvwSpecimenReport.GridLines = GridLines.Both;
            gdvwSpecimenReport.HeaderStyle.Font.Bold = true;
            gdvwSpecimenReport.AllowPaging = false;
            getSpecimenReport();
            gdvwSpecimenReport.RenderControl(htmltextwriter);
            Response.Write(strwriter.ToString());
            Response.End();
        }  

        protected void btnGetActiveclient_Click(object sender, EventArgs e)
        {
            specimenTab.Attributes.Add("class","Inactive");
            tabSpecimenReporting.Attributes.Add("class", "tab-pane fade");
            ActiveClientTab.Attributes.Add("class", "active");
            tabActiveClients.Attributes.Add("class", "tab-pane active");
            ltrTotalClients.Visible = true;
            btnActiveExportToExcel.Visible = true;
            getActiveClientReport();            
        }

        private void getActiveClientReport()
        {
            ConfigurationBO clientBO = new ActiveClientConfigurationDA().GetConfiguration(Convert.ToInt32(Session["OrgID"]), "ActiveClients");
            if (clientBO.ConfigValue == "All Clients" || clientBO.ConfigValue == "Current Year")
            {
                List<OrderKitBO> ordBO = new ActiveClientConfigurationDA().ActiveClientReport(Convert.ToInt32(Session["OrgID"]), clientBO.ConfigValue);
                gdvwActiveClient.DataSource = ordBO;
                gdvwActiveClient.DataBind();
                ltrTotalCount.Text = Convert.ToString(ordBO.Count);
            }
        }

        protected void btnActiveExportToExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Active Client Report" + " " + DateTime.Now + ".xls";
            StringWriter strwriter = new StringWriter();
            HtmlTextWriter htmltextwriter = new HtmlTextWriter(strwriter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gdvwActiveClient.GridLines = GridLines.Both;
            gdvwActiveClient.HeaderStyle.Font.Bold = true;
            gdvwActiveClient.AllowPaging = false;
            getActiveClientReport();
            gdvwActiveClient.RenderControl(htmltextwriter);
            Response.Write(strwriter.ToString());
            Response.End();
        }

        protected void gdvwSpecimenReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvwSpecimenReport.PageIndex = e.NewPageIndex;
            //gdvwSpecimenReport.DataBind();
            getSpecimenReport();
        }

        protected void gdvwActiveClient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdvwActiveClient.PageIndex = e.NewPageIndex;
            //gdvwActiveClient.DataBind();           
            getActiveClientReport();
        }
        
    }
}











