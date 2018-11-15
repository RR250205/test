<%@ Page Title="" Language="C#" MasterPageFile="~/themes/mainMaster.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="STOMS.UI.pages.home" %>

<%@ Register Src="~/UserControl/DashboardStats.ascx" TagName="DashboardStats" TagPrefix="uc1" %>
<%@ Register Src="~/usercontrol/OrderList.ascx" TagPrefix="uc1" TagName="OrderList" %>
<%@ Register Src="~/usercontrol/search.ascx" TagPrefix="uc1" TagName="search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../style/js/plugins/morris.js/morris.css" rel="stylesheet" />
        <uc1:DashboardStats ID="DashboardStats1" runat="server" />
       <%-- <div class="row">
            <div class="col-lg-3">
                <uc1:search runat="server" ID="search" />
            </div>
            <div class="col-lg-9">
                <uc1:OrderList runat="server" ID="OrderList" />
            </div>
        </div>--%>
    <div class="content">
    <div class="row">
        <div class="col-lg-9">
            <div class="row">
          <div class="box box-solid bg-teal-gradient" style="height:50vh">
            <div class="box-header">
              <h3 class="box-title">Specimen Graph</h3>              
            </div>
            <div class="box-body border-radius-none">
              <div class="chart" id="line-example" style="height: 250px;"></div>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
           </div>

                        <div class="row">          
                            <div class="col-lg-6" style="padding-left: 0px;">
                                 <div class="content" style="border: 1px solid grey;height: 25vh;"></div>
                           </div>
                             <div class="col-lg-6" style="padding-right: 0px;">
                                  <div class="content"  style="border: 1px solid grey;height: 25vh;"> </div>
                            </div>
                        </div>
              
        </div>
         <div class="col-lg-3" style="padding-right: 0px;padding-left: 23px;"> 
            <div class="content" style="background:#e9e9e9;height: 78vh;"></div>
             </div>
        <asp:HiddenField ID="hSpecimenStats" runat="server" Value="0" />
    </div>
    </div>

    <script src="../style/js/jquery.min.js"></script>    
    <script src="../style/js/plugins/morris.js/morris.js"></script>
    <script src="../style/js/plugins/raphael/raphael.min.js"></script>
   <script type="text/javascript">
       jQuery(document).ready(function () {
           console.log($('#<%= hSpecimenStats.ClientID%>').val());
           jQuery(function () {
               "use strict";

               var month = new Array();
               month[0] = "";
               month[1] = "Jan";
               month[2] = "Feb";
               month[3] = "Mar";
               month[4] = "Apr";
               month[5] = "May";
               month[6] = "Jun";
               month[7] = "Jul";
               month[8] = "Aug";
               month[9] = "Sep";
               month[10] = "Oct";
               month[11] = "Nov";
               month[12] = "Dec";

               var stats = JSON.parse( $('#<%= hSpecimenStats.ClientID%>').val());              

               var AllMonth = '';
               stats.forEach(function (item, idx)
               {
                   item.Month = month[item.Month];
                   AllMonth = AllMonth.concat(item.Month + ',');
               });
               AllMonth = AllMonth.slice(0, -1);
               var rep = '';
               var arr = [];

               var line = new Morris.Line({
                   element: 'line-example',
                   resize: true,
                   gridTextSize: 12,
                   data: stats,
                   xkey: ['Month'],
                   ykeys: ['Count'],
                   labels: ['Count'],
                   lineColors: ['#ffffff'],
                   color: ['#000'],
                   hideHover: 'auto',
                   parseTime: false,
                   gridTextColor: ['#ffffff'],
                   hoverCallback: function (index, options, content, row) {
                       return "Month: " + row.Month + "<br> Week: " + row.Week + "<br> Count: " + row.Count;
                   },
                   xLabelFormat: function (x) {
                       x.label = x.src.Month.slice(0,-2) + ' -w' + x.src.Week;
                        return x.label;
                   },

                   //function (x)
                   //{
                   //    //console.log(x)

                   //    if (arr.indexOf(x.label) < 0)
                   //    {
                   //        console.log(x.label)
                   //        arr.push(x.label);
                   //        //x.label = x.src.Month;
                   //        return x.label;
                   //    }
                   //    else {   
                   //        //x.label = '';
                   //        return x.label = '&nbsp;';
                   //    }
                   //},
                   yLabelFormat: function (y) { return y != Math.round(y) ? '' : y; },
               });
           });
       });
      
</script>  
</asp:Content>
