<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="STOMS.UI.index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <title>Specimen Testing Order and Reporting Management System (STORMS)</title>
   <%--  <title>Laboratory Information Management System (LIMS)</title>--%>
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'>
    <link href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Theme style -->
    <link href="/style/css/AdminLTE.css" rel="stylesheet" type="text/css" />

     <link href="../style/fonts/Arima-Madurai-ExtraBold/arimamadurai/ArimaMadurai-ExtraBold.ttf" rel="stylesheet" />
    
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    <link href='http://fonts.googleapis.com/css?family=Roboto+Condensed' rel='stylesheet' type='text/css'>

    <style>
 .profile-user-info-striped {
    border-top: 1px solid #DCEBF7;
    border-right:1px solid #DCEBF7;
}

.customthing {
    border: 1px solid #DCEBF7;
    border-top:0px;
      border-right:0px;

}



.profile-user-info {
    display: table;
    width: 100%;
    margin: 0 auto;
}
.profile-info-row {
    display: table-row;
    border: 1px solid #DCEBF7
  
}
.profile-info-row:first-child .profile-info-name {
    border-top: none
}
.profile-user-info-striped .profile-info-name {
    color: #336199;
    background-color: #EDF3F4;
    border-top: 1px solid #F7FBFF;
}
.profile-info-name {
    padding: 6px 10px 6px 4px;
    font-weight: normal;
    color: #667E99;
    background-color: transparent;
    border-top: 1px dotted #D5E4F1;
}
.profile-info-row:first-child .profile-info-value {
    border-top: none
}
.profile-user-info-striped .profile-info-value {
    border-top: 1px dotted #DCEBF7;
    padding-left: 12px;
}
.profile-info-value {
    padding: 6px 4px 6px 6px;
    border-top: 1px dotted #D5E4F1;
}
        @media (min-width:768px) {
            .profile-info-name {
                text-align: right;
                display: table-cell;
                width: 110px;
                vertical-align: middle;
            }

            .profile-info-name {
                width: 180px
            }

            .profile-info-value {
                display: table-cell
            }

            .no-right-padding {
                padding-right: 0
            }

            .no-padding {
                padding: 0
            }


            .profile-user-info {
                width: 50%;
                float: left;
            }

            .form-box {
                width: 360px;
                margin: 53px auto 0 auto;
            }

            #img {
                
                filter: alpha(opacity=50); /* For IE8 and earlier */
            }

            li {
                list-style: none;
                font-size: 20px;
                opacity: 1;
            }

            .anger {
                font-size: 20px;
                margin-left: 100px;
                color: black;
            }

            div.transbox {
                background-color: #030b13;
                /*#4f545c;*/
                opacity: 0.6;
                filter: alpha(opacity=60); /* For IE8 and earlier */
            }

                div.transbox h2 {
                    font-weight: bold;
                    color: #ffffff;
                }

                div.transbox li {
                    font-weight: bold;
                    color: #ffffff;
                }

                div.transbox a {
                    font-weight: bold;
                    color: #ffffff;
                }
        }


        /*commant*/

        .caption-wrapper {
	position: absolute;
        margin-top: 35px;
        margin-bottom:35px;
	/*bottom: 100px;*/
    /*margin:10px;*/
	/*left: 0;
	right: 0;*/
	/*width: 53%;*/
}
.caption-wrapper .em-container {
	margin: 0 auto;
	width: 750px;
}
.slider-caption {
    background: rgba(125, 134, 138, 0.15) none repeat scroll 0 0;
    /*padding: 30px;
    width: 70%;*/

}
.slider-title {
	color: #fff;
    display: inline-block;
    font-family: "Open Sans",sans-serif;
    font-size: 35px;
    font-weight: 700;
    line-height: 40px;
    margin-top: 25px;
    margin-left: 35px;
    margin-bottom: 30px;

}

.slider-caption h2 {
	color:#ffffff;
    font-size: 16px;
    font-weight: 400;
    line-height: 30px;
    margin: 0;
}
 li{
	color: #ffffff;
    font-size: 18px;
    font-weight: 400;
    line-height: 40px;
    margin: 0;
}

/*.slider-caption p {
	color: #fff;
    font-size: 18px;
    font-weight: 400;
    line-height: 30px;
    margin: 0;
}*/
.slider-caption a {
	color: #fff;
    font-size: 18px;
    font-weight: 400;
    line-height: 30px;
    margin: 0;
    margin-left:100px;
}

.imgstresh{
    border: 2px solid black;
    padding: 25px;
    background: url(mountain.jpg);
    background-repeat: no-repeat;
    background-size: auto;
}

/*.slider-caption ul{
    margin-bottom:20px;
}*/
/*.caption-wrapper p {
	margin-bottom: 0;
	margin-top: 10px;
}*/

.parent{
    width:100%;
    height:100%;
    /*position:relative;*/
    background-attachment: fixed;
    
}
.parent:after{
    content:'';
    background:url(images/bgimage.jpg);
    width:100%;
    height:100%;
    position:absolute;
     
    top:0;
    left:0;
     /*width:100%;
    height:100vh;*/
    background-position:center;
    background-repeat: no-repeat;
    background-size: cover;
  
}
.child{
 /*background:#3f51b573;*/
   background:#0709174f;
    position:absolute;
    z-index:1;
    width:100%;
    height:100%;
    background-attachment: fixed;
}

a{
    
    font-family:ArimaMadurai;
    font-size:14px;

   

}
.anger{
     color: #ffffff;
     margin-left: 5px;
     line-height: 40px;
}

@font-face {
    font-family: myFirstFont;
    src: url("../style/fonts/Arima-Madurai-ExtraBold/arimamadurai/ArimaMadurai-ExtraBold.ttf");
}

@font-face {
    font-family: ArimaMadurai;
    src: url("../style/fonts/Arima-Madurai-ExtraBold/arimamadurai/ArimaMadurai-Medium.ttf");
}

@font-face {
    font-family: PoppinsBold;
    src: url("../style/fonts/Poppins-Medium/poppins/Poppins-Bold.ttf");
}

@font-face {
    font-family: PoppinsMedium;
    src: url("../style/fonts/Poppins-Medium/poppins/Poppins-Medium.ttf");
}


</style>
   <%-- <a href="style/fonts/Poppins-Medium/">style/fonts/Poppins-Medium/</a>
    <a href="style/fonts/Poppins-Medium/poppins/Poppins-BoldItalic.ttf">style/fonts/Poppins-Medium/poppins/Poppins-BoldItalic.ttf</a>--%>
    </head>

   
<body class="parent">
   <%--<div style=" background-repeat: no-repeat;background-size: 100% 100%;">--%>
       <div class="child" >
         <div class="row">
        

        <div class="col-lg-7  " id="img" >
            <div style="height:100px;margin-bottom:0px;"></div>

             <div style="height:200px;margin-bottom:0px;">
           <%-- <div class="caption-wrapper">  
		<div class="em-container">
		<div class="slider-caption">
		<div class="mid-content">
		<div class="slider-title" > </div>
	
		</div>
		</div>
		</div>
		</div>--%>

                 <div class="row">
                     <div class="col-md-2">

                     </div>
                     <div class="col-md-6">
                         <div style="padding-top:20px;">
            <h2 style="font-family:myFirstFont;font-size:38px;color:#ffbd4a;">SUPPORTED TESTS</h2>
          
                             <span style="font-family:ArimaMadurai;font-size:20px;color: #ffffff; line-height:40px;margin-left:3px;">
                                 FRAT<br />
                             </span>
                             <span style="font-family:ArimaMadurai;font-size:20px;color: #ffffff;line-height:40px; margin-left:3px;">Mitochondrial Dysfunction Test</span>


               <%--<ul style="font-family:ArimaMadurai;font-size:14px;color: #ffffff;">
                      <li>FRAT</li>
                      <li>Mitochondrial Dysfunction Test</li>
             </ul>--%>
		<%--<p><a class="slider-btn" href="https://8degreethemes.com/wordpress-themes" target="_blank">DOWNLOAD NOW</a></p>--%>
		 </div>
                     </div>
                     <div class="col-md-3">

                     </div>

                 </div>


                 	
                 </div>
         
            <div style="height:290px;margin-bottom:20px;">

             <%--    <div class="caption-wrapper">  
		<div class="em-container">
		<div class="slider-caption">
		<div class="mid-content">
		<div class="slider-title"></div>
		
		</div>
		</div>
		</div>
		</div>--%>
                <div class="row">
                     <div class="col-md-2">

                     </div>
                     <div class="col-md-9">
                              <div style="padding-bottom: 45px;">
                <h2 style="font-family:myFirstFont;font-size:38px;color:#ffbd4a;">COMMON DOWNLOAD FORM</h2>
                <a class="anger" href="#"> Order Requisition Form  </a> <br/>
                <a class="anger" href="#">Insurance Pre-Authorization Form</a><br/>
                <a class="anger" href="#">LOMN (Letter of Medical Necessity Form) </a><br/>
		
		 </div>
                     </div>
                     <div class="col-md-1">

                     </div>


                </div>

           
               
                   <%-- <h2 style="font-weight:bold;margin-left: 59px;padding-top: 34px; margin-bottom:26px;">Common Download Form</h2>

                <a class="anger" href="#"> Order Requisition Form  </a> <br/>
                <a class="anger" href="#">Insurance Pre-Authorization Form</a><br/>
                <a class="anger" href="#">LOMN (Letter of Medical Necessity Form) </a><br/>
              --%>
                
            </div>

        
            </div>

           

              <div  class="col-lg-5" >

                  <div class="row">
                       <div  class="col-lg-3" >
                </div>
                       <div  class="col-lg-8" >
                            <div style="margin-left: 42px; margin-right: auto; margin-top: 50px; text-align: center;">
            <span class="Roboto-Condensed-normal-400" style="font-size:24px; color: #10ffef; font-family:PoppinsBold; font-size: 30px;">KO-LIMS</span>
            <br />
            <span class="Roboto-Condensed-normal-400" style="font-size: 13px; font-family:PoppinsBold; color: #ffffff;">Laboratory Information Management System</span>
        </div>
                </div>

                  </div>
                    
            
                  <div class="row">
                       <div  class="col-lg-4" >
                </div>
                      <div class="col-lg-8">


                            <div style="margin-top:50px;width:85%; border-radius:0px 0px 200px 200px; background-color:#b1b0abb5;  border:1px solid #b1b0abad;">
                              
                              <div class="header" style="color:#07ec48; text-align:center; font-size: 18px; font-family:PoppinsBold; margin:20px;margin-bottom:30px;"><b>SIGN IN</b></div>
                              <form id="form1" runat="server" style="margin:50px; margin-top:0px;">
                                  
                                   
                                   

                                          <div>
                <div>
                    <asp:Label ID="ltrMsg" ForeColor="Red" runat="server" Text="Username or Password is incorrect!" Visible="false"></asp:Label>
                </div>
                <%--<a href="#sathish">sathish</a>--%>
                <div class="form-inline">
                    <span style="color:#ffffff;font-family:PoppinsMedium; font-size:11px;">Login as :</span>

                    <select name="forma" class="form-control dropdown"  onchange="location = this.value;" style="width: 72%; margin-left: 1px;font-family:PoppinsMedium;font-size:11px;" id="stlStromsUser" runat="server">
                        <option value="0">Select User</option>
                        <option value="#labuser" data-icon="fa fa-heart">Lab User</option>
                        <option value="#physician">Physician</option>
                        <option value="#Insuranceagent">Insurance Agent</option>
                    </select>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="stlStromsUser" ForeColor="Red" InitialValue="0" runat="server" ErrorMessage="Please select user"></asp:RequiredFieldValidator>
                </div>
                <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic" runat="server" ControlToValidate="stlStromsUser" Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>

                <asp:HiddenField ID="husertype" runat="server" Value="0" />
                <div class="form-group">
                    <asp:TextBox runat="server" ID="txtUserName" type="email" name="userid" Style="margin-top:20px;"  class="form-control" placeholder="User ID" required="" />
                </div>
                <div class="form-group">
                    <asp:TextBox runat="server" TextMode="Password" ID="txtPassword" Style="margin-top:32px;" type="password" name="password" class="form-control" placeholder="Password" required="" />
                </div>
                <div class="form-group">
                    <input type="checkbox"   name="remember_me" />
                    <span style="color:#ffffff;font-family:PoppinsMedium;">
                        Remember me
                    </span> 
                </div>
            </div>
                                    <div class="footer">
                <asp:Button ID="btnLogin" class="btn bg-lime btn-block" Style="border-radius:12px;font-family:PoppinsMedium;" Text="Sign Me In" runat="server" OnClick="btnLogin_Click" />
<div style="text-align:center; margin-top:10px;">
<asp:LinkButton ID="PasswordChange" runat="server" Style="text-align:center; font-size:16px;color:#ffbd4a;font-family:PoppinsBold; line-height:2;"  OnClick="PasswordChange_Click1" Text="Forgot Password"></asp:LinkButton>
     &nbsp;
                
</div>
                
                <%-- <p><a href="#" >I forgot my password</a></p>--%>
               
            </div>
                                     <div class="margin text-center hide alert alert-warning alert-dismissable" id="divAlert">
                <i class="fa fa-warning"></i>
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <b>Alert!</b><label></label>
            </div>
            <div class="margin text-center hide">
                <span>Sign in using social networks</span>
                <br />
                <button class="btn bg-light-blue btn-circle"><i class="fa fa-facebook"></i></button>
                <button class="btn bg-aqua btn-circle"><i class="fa fa-twitter"></i></button>
                <button class="btn bg-red btn-circle"><i class="fa fa-google-plus"></i></button>

            </div>

                                   </form>
                  
                          
                          </div>
                       
                      </div>

                       <div  class="col-lg-0" >
                </div>

                  </div>

                  
                 </div>


     
            

    </div>
   </div>

      
   
  <%-- <div class="row" >
          <div  class="col-lg-5" style="height:620px; ">
            
   
      
        <div class="form-box" id="login-box">
           
     
           


          
        </div>
  
        </div>

   </div>--%>


    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js" type="text/javascript"></script>

</body>
</html>

