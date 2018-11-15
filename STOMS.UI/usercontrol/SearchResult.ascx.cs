using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using STOMS.BO;
using STOMS.DA;

namespace STOMS.UI.usercontrol
{
    public partial class SearchResult : System.Web.UI.UserControl
    {
        private bool _isLoad;
        private string _searchText;
        public bool IsLoad
        {
            get { return _isLoad; }
            set { _isLoad = value; }
        }
        public string SearchText
        {
           // get { return _searchText; }
            set { _searchText = value; }
        } 
        public string SearchType
        {
            //get { return ltrSearchType.Text; }
            set { ltrSearchType.Text = value; }
        }
        private int _resultCount=0;
        public int ResultCount
        {
            get { return _resultCount; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
               
            }
        }

        public void popSearchResult(int ProfileID)
        {
            dvNoRec.Visible = false;
            List<SearchBO> searBO = new SearchDA().GetSearchValues(Request.QueryString["searchkey"], ProfileID, Convert.ToInt32(Session["OrgID"]), false);
            rpSearchResult.DataSource = searBO;
            rpSearchResult.DataBind();
            _resultCount = searBO.Count;
            if (rpSearchResult.Items.Count == 0)
                dvNoRec.Visible = true;
        }     

        protected void rpSearchResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(ltrSearchType.Text == "By Specimen")
                {
                    SearchBO oSamp = (SearchBO)e.Item.DataItem;
                    Literal ltr = (Literal)e.Item.FindControl("ltrSpecDetails");
                    if (oSamp.Sub1.Trim() != "1/1/1900")
                    {
                        ltr.Text = "Drawn Date :" + " " + oSamp.Sub1 + "<br /> Specimen Status :" + " " + oSamp.Sub2;
                    }
                    else
                    {
                        ltr.Text = "Drawn Date :" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + "-" + "<br /> Specimen Status :" + " " + oSamp.Sub2;
                    }
                }

                if(ltrSearchType.Text == "By Assay")
                {
                    SearchBO oSamp = (SearchBO)e.Item.DataItem;
                    Literal ltr = (Literal)e.Item.FindControl("ltrSpecDetails");
                    if (oSamp.Sub1 != "" && oSamp.Sub2 != "")
                        ltr.Text = "Assay Start Date :" + " " + oSamp.Sub1.ToString() + "<br /> Assay Complete Date :" + " " + Convert.ToString(Convert.ToDateTime(oSamp.Sub2).ToShortDateString()) + "<br /> Assay Status :" + " " + oSamp.Sub3.ToString();
                    else if (oSamp.Sub1 != "" || oSamp.Sub2 != "") {
                        if (oSamp.Sub1 != "")
                            ltr.Text = "Assay Start Date :" + " " + oSamp.Sub1.ToString() + "<br /> Assay Complete Date :" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-" + "<br /> Assay Status :" + " " + oSamp.Sub3.ToString();                        
                    }
                    else
                        ltr.Text = "Assay Start Date :" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- " + "<br /> Assay Complete Date :" + " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;-" + "<br /> Assay Status :" + " " + oSamp.Sub3.ToString();
                }  
            }
        }

        protected void rpSearchResult_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (ltrSearchType.Text)
            {
                case "By Specimen":
                    //Session["SpecimenID"] = Convert.ToString(e.CommandArgument);
                    Response.Redirect("~/pages/Specimen?sn=" + Convert.ToString(e.CommandArgument));
                    break;
                case "By Assay":
                    // Session["AssayID"] = Convert.ToString(e.CommandArgument);
                    Response.Redirect("~/pages/assaydetail?an=" + Convert.ToString(e.CommandArgument));
                    break;
                default:
                    break;
            }
        }
    }
}
