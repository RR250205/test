<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sampleReport.aspx.cs" Inherits="STOMS.UI.pages.sampleReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body {
  background: rgb(204,204,204); 
}
page {
  
}
page[size="A4"] {  
  
}
        @media print {
            body, page {
                margin: 0;
                box-shadow: 0;
            }
        }
       
    </style>
     <link href="https://fonts.googleapis.com/css?family=Open+Sans|Roboto" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <page style="background: white;  display: block;  margin: 0 auto;  margin-bottom: 0.5cm;  box-shadow: 0 0 0.5cm rgba(0,0,0,0.5); width: 21cm;  height: 29.7cm;   font-family: 'Open Sans', sans-serif; font-family: 'Roboto', sans-serif; " size="A4" >
                <header style="padding-top: 20px;padding-left: 20px;padding-right:20px">
                    <img src="../images/IliadLogo.png" />
                    <aside style="float:right ;    padding-top: 20px;" >
                        <!--51110 Campus Drive Suite 190, <br />
                        Plymouth Meeting, <br />
                        PA, 19462; <br />
                        Phone: 610-441-9050;Fax:6105375075 <br />
                        www.iliadneuro.com-->
                        <b>Poluru L. Reddy, Ph.D. DABCC</b><br />
                        <i>Medical Lab Director</i>

                    </aside>
                </header>
               <hr style="    border: 4px double orange;" />

              <strong>Anti - Folate Receptor A Antibodies- Binding and Blocking Assay Report</strong>  

            </page>
             <table>
         <tr>
            <td>
               PA Name
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtPAName"/>
            </td>
        </tr>
        <tr>
            <td>
               Name
            </td>
            <td>
                <asp:TextBox runat="server"  ID="txtName"/>
            </td>
        </tr>
        <tr>
            <td>
                DOB
            </td>
            <td>
                <asp:TextBox runat="server"  ID="txtDOB"/>
            </td>
        </tr>
        <tr>
            <td>
                Binding Assay
            </td>
            <td>
                <asp:TextBox runat="server"  ID="txtBindingAssay"/>
            </td>
        </tr>
         <tr>
            <td>
                Binding Assay Comments
            </td>
            <td>
                <asp:TextBox runat="server"  ID="txtBindingAssayComments"/>
            </td>
        </tr>
        <tr>
            <td>
                Blocking Assay
            </td>
            <td>
                <asp:TextBox runat="server"  ID="txtBlockingAssay"/>
            </td>
        </tr>
        <tr>
            <td>
                Blocking Assay Comments
            </td>
            <td>
                <asp:TextBox runat="server"  ID="txtBlockingAssayComments"/>
            </td>
        </tr>
         <tr>
            <td>
               
            </td>
            <td>
                <asp:Button Text="Submit" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
                <asp:Button Text="Generate From Html" ID="btnGenerateFromHtlm" OnClick="btnGenerateFromHtlm_Click" runat="server" />
            </td>
        </tr>
    </table>
          
        </div>
    </form>
</body>
</html>
