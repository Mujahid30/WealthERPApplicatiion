<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Trace="false"
    Inherits="WealthERP.Reports.Display" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WealthERP Reports</title>
    <style>
        body
        {
            width:90%;
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
        .yellow-box {
            background-color:#FFFFE5;
            border:1px solid #F5E082;
            padding:10px;
        }

    </style>

    <script>
        function ShowProcesssing(btn) {
           
            document.getElementById('btnSend').value ="Sending Email.Please wait..";
            document.getElementById('btnSend').disabled = true;
            
        }
        function sendMail() {

            if (document.getElementById("txtTo").value == "") {
                alert("Please enter To Email Address.");
                return false;
            }
            if (document.getElementById("txtTo").value.indexOf("@") < 2 || document.getElementById("txtTo").value.indexOf(".") < 4) {
                alert("Please enter  a valid To Email Address.");
                document.getElementById("txtTo").focus();
                return false;
            }

            if (document.getElementById("txtCC").value != "") {
                    if (document.getElementById("txtCC").value.indexOf("@") < 2 || document.getElementById("txtCC").value.indexOf(".") < 4) {
                    alert("Please enter  a valid CC Email Address.");
                    document.getElementById("txtCC").focus();
                    return false;
                }
            }
            
            document.getElementById("hidTo").value = document.getElementById("txtTo").value
            document.getElementById("hidSubject").value = document.getElementById("txtSubject").value
            document.getElementById("hidFormat").value = document.getElementById("ddlFormat").options[document.getElementById("ddlFormat").selectedIndex].value
            document.getElementById("hidCC").value = document.getElementById("txtCC").value
            document.getElementById("hidBody").value = document.getElementById("txtBody").value
           // alert(document.getElementById("txtBody").value)
            
            document.getElementById("hidCCMe").value = document.getElementById("chkCopy").checked
            window.document.forms[0].action = "Display.aspx?mail=0";
            //ConvertnlTobr();
            document.getElementById("btnSendEmail").click()

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

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0">
         <tr>
            <td>
                <table border="0" width="910px">
                    <tr>
                        <%--<td>
                            &nbsp;  &nbsp;  &nbsp;  &nbsp; 
                        </td>--%>
                        <td align="center">
                            
                        </td>
                        <td align="right" valign="bottom">
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
                                            <td style="width: 150px">
                                                Format
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlFormat" runat="server">
                                                    <asp:ListItem Value="PDF">PDF </asp:ListItem>
                                                    <asp:ListItem Value="Excel">MS Excel</asp:ListItem>
                                                    <asp:ListItem Value="Word">MS Word</asp:ListItem>
                                                    <asp:ListItem Value="RTF">Rich Text Format</asp:ListItem>
                                                </asp:DropDownList>
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
                                                <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="PCGButton" OnClientClick="sendMail()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                        <div runat="server" id="divMessage" class="yellow-box" visible="false" enableviewstate="false">
                            <asp:Label ID="lblEmailStatus" runat="server" Text="" EnableViewState="false" style="font-weight:bolder;color:Green;"></asp:Label>
                        </div>
                            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td width="100%" align="center">
            <asp:Button ID="btnSendMail" runat="server" class='sendEmail ButtonField'  
                                Text="Send Report by Email" OnClientClick="replaceSpecialChars()" />
            </td>
         </tr>
         <tr>
            <td width="100%" align="left">
                <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                    BorderColor="#789FC8" BorderStyle="Solid" BorderWidth="1" EnableDatabaseLogonPrompt="True"
                     DisplayGroupTree="False" EnableViewState="true" 
                    OnNavigate="CrystalReportViewer1_Navigate" ToolbarStyle-Width="770px" Width="100%"  />
            </td>
        </tr>
         <tr>
            <td align="center">
                <asp:Label ID="lblNoRecords" runat="server" CssClass="HeaderTextSmall" Text="No records found."
                    Visible="false" EnableViewState="false"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnSendEmail" runat="server" Text="" OnClick="btnSendEmail_Click" OnClientClick="ShowProcesssing(this)"
        BorderStyle="None" BackColor="Transparent" />
 
    <asp:HiddenField ID="hidFormat" runat="server" />
    <asp:HiddenField ID="hidTo" runat="server" />
    <asp:HiddenField ID="hidCC" runat="server" />
    <asp:HiddenField ID="hidBody" runat="server" />
    <asp:HiddenField ID="hidSubject" runat="server"/>
    <asp:HiddenField ID="hidCCMe" runat="server" />
    </form>
</body>
<script>
    ConvertnlTobr();
</script>
</html>
