<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvestorOnline.aspx.cs"
    Inherits="WealthERP.InvestorOnline" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Base/CSS/demo.css" rel="stylesheet" type="text/css" />
    <link href="../Base/CSS/jquery-te-1.4.0.css" rel="stylesheet" type="text/css" />
    
    
</head>
<body>
    <form id="form1" runat="server">
   
    <div style="width: 900px;">
        <div style="width: 600px; float: left;">
            <div id="divEmail" runat="server">
               
                    <asp:RadioButton ID="Subject" runat="server" Checked="true" AutoPostBack="true" GroupName="Email" Text="Email Subject" OnCheckedChanged="RadioBtn_OnCheckedChanged" />
                    <asp:RadioButton ID="Body" runat="server" AutoPostBack="true" GroupName="Email" Text="Email Body" OnCheckedChanged="RadioBtn_OnCheckedChanged" />
                <div id="divEmailSubject" runat="server" visible="true">
                    <div style="width: 700px; margin:10px;padding:10px;">
                        <asp:TextBox ID="txtEmailSubject"  TextMode="MultiLine" runat="server" Width="600px" Height="150px" 
                            onblur="getEmailSubject()"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnEmailSubjectPreview" runat="server" OnClick="previewButton_Onclick"
                        Text="Preview" />
                    <asp:Button ID="btnEmailSubjectSave" runat="server" OnClick="SaveButton_Onclick"
                        Text="Save" />
                    <div style="border:solid 2px Blue; margin:20px;padding:20px;">
                        <asp:Label ID="lblSampleEmailSubject" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="divEmailBody" runat="server" visible="false">
                    <div style="width: 700px;">
                        <asp:TextBox ID="txtEmailBody" TextMode="MultiLine" runat="server" CssClass="textEditor"
                            onblur="getEmailBody()"></asp:TextBox>
                    </div>
                    <asp:Button ID="btnEmailBodyPreview" runat="server" OnClick="previewButton_Onclick"
                        Text="Preview" />
                    <asp:Button ID="btnEmailBodySave" runat="server" OnClick="SaveButton_Onclick" Text="Save" />
                    <div style="border:solid 2px Blue; margin:20px;padding:20px;">
                        <asp:Label ID="lblSampleEmailBody" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div id="divSMS" runat="server">
                <div style="width: 400px; margin:10px;padding:10px;">
                    <asp:TextBox ID="txtSMSBody" TextMode="MultiLine" runat="server" Width="600px" Height="150px"
                        onblur="getSMSBody()"></asp:TextBox>
                </div>
                <asp:Button ID="btnSMSBodyPreview" runat="server" OnClick="previewButton_Onclick"
                    Text="Preview" />
                <asp:Button ID="btnSMSBodySave" runat="server" OnClick="SaveButton_Onclick" Text="Save" />
                <div style="border:solid 2px Blue; margin:20px;padding:20px;">
                    <asp:Label ID="lblSampleSMSBody" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div style="width: 300px; float: left; margin-top:50px;">
        <div style="float:right;">
            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnEmailSubject" runat="server" />
    <asp:HiddenField ID="hdnEmailBody" runat="server" />
    <asp:HiddenField ID="hdnSMSBody" runat="server" />
    <asp:HiddenField ID="hdnCurrentTextEditor" runat="server" />
    
<script src="../Scripts/jquery-1.10.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-te-1.4.0.min.js" type="text/javascript"></script>

 <script language="javascript" type="text/javascript">
     $('.textEditor').jqte();
     function getEmailSubject() {
         document.getElementById('<%=hdnEmailSubject.ClientID %>').value = document.getElementById('<%=txtEmailSubject.ClientID %>').value;
         
     }
     function getEmailBody() {
         document.getElementById('<%=hdnEmailBody.ClientID %>').value = document.getElementById('<%=txtEmailBody.ClientID %>').value;
     }
     function getSMSBody() {
         document.getElementById('<%=hdnSMSBody.ClientID %>').value = document.getElementById('<%=txtSMSBody.ClientID %>').value;
     }
     function SetParameter(id) {
         var x = document.getElementById('<%=hdnCurrentTextEditor.ClientID %>').value;
         var y = document.getElementById(x).value + id;
         //        document.getElementById('<%=txtEmailSubject.ClientID %>').value = document.getElementById(x).value + id;
         document.getElementById(x).value = y;
         document.getElementById(x).setfocus();
     }
    
</script>
    </form>
</body>


</html>
