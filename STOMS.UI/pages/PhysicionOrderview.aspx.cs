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
    public partial class PhysicionOrderview : System.Web.UI.Page
    {
        string OrderID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["si"] != null)
            {
                Session["PgContentTitle"] = "Physicion Order View";
                OrderID = Convert.ToString(Request.QueryString["si"]);
                GetOrderListView(OrderID);
            }
        }


        public void GetOrderListView(string OrderID)
         {
            List<OrderBO> ViewOrder = (new OrderInvDA().getViewOrder(OrderID));
            {
                ltrCustomerName.Text = ViewOrder[0].objCustomer.CustomerName;
                ltrPatientEmail.Text = ViewOrder[0].Ordpatient.EmailID;
                ltrPhyPatientName.Text = ViewOrder[0].Ordpatient.FirstName + ViewOrder[0].Ordpatient.LastName;
                ltrPhySpecimenNumber.Text = ViewOrder[0].specimenInfoBO.SpecimenNumber;
                ltrPhyOrderNumber.Text = ViewOrder[0].OrderNumber;
                ltrPhyOrderStatus.Text = ViewOrder[0].specimenInfoBO.SpecimenStatus;
                ltrSpecimenType.Text = ViewOrder[0].specimenInfoBO.SpecimentType;
                ltrTestType.Text = ViewOrder[0].specimenInfoBO.TestType;
                ltrPaymentMode.Text = ViewOrder[0].specimenInfoBO.PaymentMode;

                if (ViewOrder[0].OrdDocument.DocNumber!= null)
                {
                    ancReqViewCopy.InnerText = ViewOrder[0].OrdDocument.DocNumber;
                    string filesource = @"\Docs\RequestForm\" + ViewOrder[0].OrdDocument.DocNumber + "." + ViewOrder[0].OrdDocument.DocType;
                    string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                    ancReqViewCopy.HRef = url;

                }

                if(ViewOrder[0].specimenInfoBO.SpecimenID!=0)
                {
                    List<TestResultsBO> testResultsid = (new OrderInvDA().getTestResult(ViewOrder[0].specimenInfoBO.SpecimenID));
                    if(testResultsid.Count!=0)
                    { 
                     lblPaymentStatus.Text = testResultsid[0].objpayment.PaymentStatus;

                    if (testResultsid[0].IsReleased == true)
                    {

                        
                        aMitResultView.InnerHtml = testResultsid[0].objDocumentBO.DocNumber;
                        string filesource = @"\Docs\Results\" + testResultsid[0].objDocumentBO.DocNumber + "." + "pdf";
                        string url = HttpContext.Current.Request.Url.Scheme + @":\\" + HttpContext.Current.Request.Url.Authority + filesource;
                        aMitResultView.HRef = url;
                        aMitResultView.Visible = true;
                    }
                    else
                    {
                        ltrSpecimenResult.Visible = true;
                        ltrSpecimenResult.Text = "Specimen Result not Released";
                    }
                    }
                }
                
                //fileuploadcopy();

            }

        }

        protected void Resultremove_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/PhyOrderList");
        }
    }
}