<%@ Page Language="C#" AutoEventWireup="true" Buffer="false" CodeBehind="EmailReport.aspx.cs" Inherits="WealthERP.Reports.EmailReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../App_Themes/Maroon/GridViewCss.css" rel="stylesheet" type="text/css" />
<head id="Head1" runat="server">
    <title>WealthERP Reports</title>
    <style>
        body
        {
            width: 90%;
        }
        .HeaderTextSmall
        {
            font-family: Arial;
            font-weight: bold;
            font-size: small;
            color: #942627;
        }
        input, textarea, select
        {
            border: 1px solid #BDC7D8;
            font-size: 11px;
            padding: 3px;
            border-color: #96A6C5;
        }
        .yellow-box
        {
            background-color: #FFFFE5;
            border: 1px solid #F5E082;
            padding: 10px;
        }
        .PageBackGround
        {
         background-color:Gray;
         filter: alpha(opacity=100);
         opacity: 0.7;
        }
        .pageBack
        {
            background-color:#EBEFF9; 
        }
        
    </style>

    <script>
        function ShowProcesssing(btn) {
            document.getElementById('btnSend').value = "Sending Email.Please wait..";
            document.getElementById('btnSend').disabled = true;

        }
        function ShowProcesss() {
            document.getElementById('Button1').value = "Sending Email.Please wait..";
            document.getElementById('Button1').disabled = true;

        }
        function sendMail() {
            
            if (document.getElementById("txtTo").value == "") {
                alert("Please enter To Email Address.");
                return false;
            }
           
            if (document.getElementById("txtCC").value != "") {                
                if (document.getElementById("txtCC").value.indexOf("@") < 2 || document.getElementById("txtCC").value.indexOf(".") < 4) {
                    alert("Please enter  a valid CC Email Address.");
                    document.getElementById("txtCC").focus();
                    return false;
                }
            }
            
            if (document.getElementById("txtTo").value.indexOf("@") < 2 || document.getElementById("txtTo").value.indexOf(".") < 4) {
                alert("Please enter  a valid To Email Address.");
                document.getElementById("txtTo").focus();
                return false;
            }

            document.getElementById("hidTo").value = document.getElementById("txtTo").value
            document.getElementById("hidSubject").value = document.getElementById("txtSubject").value
            document.getElementById("hidCC").value = document.getElementById("txtCC").value
            document.getElementById("hidBody").value = document.getElementById("txtBody").value
            document.getElementById("hidCCMe").value = document.getElementById("chkCopy").checked

            document.getElementById("btnSendMail").click()

        }
        function replaceSpecialChars() {

            while (document.getElementById("txtBody").value.indexOf("<br/>") > -1) {
                document.getElementById("txtBody").value = document.getElementById("txtBody").value.replace("<br/>", "\n");
                document.getElementById("hidBody").value = document.getElementById("hidBody").value.replace("<br/>", "\n");
            }
        }
        function ConvertnlTobr() {

            document.getElementById("txtBody").value = document.getElementById("txtBody").value.replace(/\n/g, '<br/>');
            document.getElementById("hidBody").value = document.getElementById("hidBody").value.replace(/\s/g, ' ').replace(/  ,/g, '<br/>'); ;

        }
    </script>

    <link type="text/css" media="screen" rel="stylesheet" href="Colorbox/colorbox.css" />

    <script type="text/javascript" src="../Scripts/jquery.js"></script>

    <script type="text/javascript" src="Colorbox/jquery.colorbox-min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
           
           if(<%= isMail %> == "1")
           {
             $(".sendEmail").colorbox({ width: "700px", inline: true, open:true, href: "#divEmail" });
             replaceSpecialChars();
           }
           else
           {
             $(".sendEmail").colorbox({ width: "50%", inline: true, href: "#divEmail",onClosed:function(){
                ConvertnlTobr(); 
              } 
              });
              }
        });
        
    </script>
    
    <script language="JavaScript">

        var Page;

        var postBackElement;

        function pageLoad() {

            Page = Sys.WebForms.PageRequestManager.getInstance();

            Page.add_beginRequest(OnBeginRequest);

            Page.add_endRequest(endRequest);

        }

        function OnBeginRequest(sender, args) {

            $get("IMGDIV").style.display = "";

        }

        function endRequest(sender, args) {

            $get("IMGDIV").style.display = "none";

        }
      
        function hideProcessImage(){
        
        $("#abc").hide();
        }
        
 

  </script>

</head>
<body class="pageBack" onload="hideProcessImage();">
<div id="abc"  style="width:100%; position:relative; top:200px;left:430px;opacity: 0.6;">

<img id="Img1" src="images/MailSend-loader.gif" />
</div>
 
 <% sendMailFunction(); %>   
    <form id="form1" runat="server">
    <table width="100%" border="0">
        <tr>
            <td>
                <table border="0" width="910px">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSendEmail" runat="server" class='sendEmail ButtonField' Text="Send Report by Email" style="display:none" />
                            
                            <div style="display: none">
                                <div id='divEmail' style='padding: 10px; background: #fff;'>
                                    <table border="0">
                                        <tr>
                                            <td colspan="2">
                                                <h3 class="HeaderTextSmall">
                                                    Send Report by Email</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Subject
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSubject" runat="server" Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                To
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtTo" runat="server" Width="500px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                CC
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCC" runat="server" Width="500px"></asp:TextBox>
                                                   
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtBody" runat="server" Rows="10" Columns="50" Width="500px" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBox ID="chkCopy" runat="server" Text="Send a copy to me" Checked="true" />
                                                <asp:HiddenField ID="hidRMEmailId" runat="server" />
                                                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="PCGButton" OnClientClick="sendMail();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div runat="server" id="divMessage" class="yellow-box" visible="false" enableviewstate="false" style="width:100%">
                                <asp:Label ID="lblEmailStatus" runat="server" Text="" EnableViewState="false" Style="font-weight: bolder;
                                    color: Green;"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr id="trCustomerlist" runat="server" visible="false">
                    <td align="center">
                    <div runat="server" id="divCustomerlist" class="yellow-box" visible="true" enableviewstate="false">
                                <asp:Label ID="Label1" runat="server" 
                                    Text="Report not send to the following customers as E-Mail Id is not available in profile" 
                                    EnableViewState="False" Style="color: Red;text-align:center" Font-Bold="True"></asp:Label>
                    </div>
                    
                    </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="gvEmailCustomerList" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" CellPadding="4" CssClass="GridViewStyle" 
                            HorizontalAlign="Center" ShowFooter="True" EnableViewState="true" Width="200px">
                             <FooterStyle CssClass="FooterStyle" />
                             <Columns>
                             <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" CssClass="GridViewCmbField" 
                                            Text='<%#Eval("CustometName") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                             </Columns>
                             <HeaderStyle CssClass="HeaderStyle" />
                            <PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblNoRecords" runat="server" CssClass="HeaderTextSmall" Text="No records found."
                    Visible="false" EnableViewState="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <DIV id="IMGDIV" style="display:none;position:absolute;left: 35%;top: 25%;vertical-align:middle;border-style:inset;border-color:black;background-color:White;z-index:40;">
                   
                    <img src="images/loading.gif" />               

                </DIV>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSendMail" runat="server" Text="" OnClick="btnSendEmail_Click" OnClientClick="ShowProcesssing(this)"
        BorderStyle="None" BackColor="Transparent" />
    <asp:HiddenField ID="hidFormat" runat="server" />
    <asp:HiddenField ID="hidTo" runat="server" />
    <asp:HiddenField ID="hidCC" runat="server" />
    <asp:HiddenField ID="hidBody" runat="server" />
    <asp:HiddenField ID="hidSubject" runat="server" />
    <asp:HiddenField ID="hidCCMe" runat="server" />
    </form>
</body>

           

<script>
    ConvertnlTobr();
</script>


   
                   

  

</html>
