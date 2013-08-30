<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUploadNew.ascx.cs" Inherits="WealthERP.Uploads.CustomerUploadNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>
<link href="/YUI/build/container/assets/container.css" rel="stylesheet" type="text/css" />
<link href="/YUI/build/menu/assets/skins/sam/menu.css" rel="stylesheet" type="text/css" />

<script src="/YUI/build/utilities/utilities.js" type="text/javascript"></script>

<script src="/YUI/build/container/container-min.js" type="text/javascript"></script>

<%--<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>--%>
<%--This are the linkrels for the Jquery files and CSS files abt the screen tips and info's--%>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<link href="../App_Themes/Maroon/Images/bubbletip.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/Maroon/Images/bubbletip-IE.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">


    function pageLoad() {
        InitDialogs();
        Loading(false);
    }

    function UpdateImg(ctrl, imgsrc) {
        var img = document.getElementById(ctrl);
        img.src = imgsrc;
    }

    // sets up all of the YUI dialog boxes
    function InitDialogs() {
        DialogBox_Loading = new YAHOO.widget.Panel("waitBox",
	{ fixedcenter: true, modal: true, visible: false,
	    width: "230px", close: false, draggable: true
	});
        DialogBox_Loading.setHeader("Processing, please wait...");
        DialogBox_Loading.setBody('<div style="text-align:center;"><img src="/Images/Wait.gif" id="Image1" /></div>');
        DialogBox_Loading.render(document.body);
    }
    function Loading(b) {
        if (b == true && Page_IsValid == true) {
            DialogBox_Loading.show();
        }
        else {
            DialogBox_Loading.hide();
        }
    }
   
</script>


<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Uploads
                        </td>
                        <td align="right">
                            <img src="../Images/helpImage.png" height="15px" width="20px" style="float: right;"
                                class="flip" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="3">
            <div class="panel">
                <p>
                    Using this feature you can upload your client profiles and other financial data
                    (MF Transactions, Equity Transactions etc.) provided by your institution or you
                    can use our standard templates to do so.
                </p>
            </div>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="msgUploadComplete" runat="server" class="success-msg" align="center" visible="false">
                Uploading successfully Completed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="uploadError" runat="server" class="failure-msg" align="center" visible="false">
                <asp:Label ID="Message_lbl" runat="server" Text="Label" Font-Bold="False"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td id="Td1" colspan="4" align="right" runat="server">
            <asp:Label ID="lblLastUploadDateText" runat="server" Text="Last Upload Date:" CssClass="UploadDateLbl"
                Visible="false"></asp:Label>
            <asp:Label ID="lblLastUploadDate" runat="server" Text="" Font-Bold="true" CssClass="UploadDateLbl"
                Visible="false"></asp:Label>
        </td>
    </tr>
    <tr id="trError" visible="false" runat="server">
        <td colspan="4">
            <asp:Label ID="lblError" runat="server" Text="" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr id="trMessage" visible="false" runat="server">
        <td colspan="4" class="Message">
            <asp:Label ID="lblMessage" runat="server" Text="" CssClass="SuccessMsg"></asp:Label>
        </td>
    </tr>
    <tr>
        <div class="divSectionHeading" style="vertical-align: text-bottom">
            Source Selection
        </div>
    </tr>
    
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" CssClass="FieldName" runat="server" Text="Extract Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlUploadType" runat="server" OnSelectedIndexChanged="ddlUploadType_SelectedIndexChanged"
                AutoPostBack="true" CssClass="cmbField">
                <asp:ListItem Value="0">Select an Extract Type</asp:ListItem>
                <%-- <asp:ListItem Value="MFF">MF Folio Only</asp:ListItem>--%>
                <asp:ListItem Value="Link">Fixed Income</asp:ListItem>
               
            </asp:DropDownList>
            <img src="../Images/help.png" class="ScreenTip1" style="height: 15px; width: 15px;" />
            <div id="div1" style="display: none;">
                <p class="tip">
                    Choose what data you intend to upload from the dropdown<br />
                    (eg: Client Profiles or MF Transactions etc)
                </p>
            </div>
            <asp:RequiredFieldValidator ID="ddlUploadType_RequiredFieldValidator" ControlToValidate="ddlUploadType"
                ValidationGroup="btn_Upload" ErrorMessage="Please select an Extract type" InitialValue="0"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
  
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="External Source:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlListCompany" runat="server" CssClass="cmbField" 
                >
            </asp:DropDownList>
            <img src="../Images/help.png" class="ScreenTip2" style="height: 15px; width: 15px;" />
            <div id="div2" style="display: none;">
                <p class="tip">
                    Choose the source of the data your Uploading, it could be from your Financial Institution
                    <br />
                    (eg: CAMS, Karvy etc) or the Standard Templates provided by the software
                </p>
            </div>
            <asp:CompareValidator ID="ddlListCompany_CompareValidator" runat="server" ControlToValidate="ddlListCompany"
                ErrorMessage="Please select an External Source" Operator="NotEqual" ValueToCompare="Select Source Type"
                Display="Dynamic" CssClass="cvPCG" ValidationGroup="btn_Upload">
            </asp:CompareValidator>
            <asp:RequiredFieldValidator ID="ddlListCompany_RequiredFieldValidator" ControlToValidate="ddlListCompany"
                ValidationGroup="btn_Upload" ErrorMessage="Please select an External Source"
                Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        
      
        <%-- </td> --%>
    </tr>
    <tr id="datevisible" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lbldate" runat="server" CssClass="FieldName" Text="Select Date:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtUploadDate" runat="server" CssClass="txtField"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtUploadDate_CalendarExtender" runat="server"
                Format="dd/MM/yyyy" TargetControlID="txtUploadDate" OnClientDateSelectionChanged="checkDate">
            </ajaxToolkit:CalendarExtender>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtUploadDate_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtUploadDate" WatermarkText="dd/mm/yyyy">
            </ajaxToolkit:TextBoxWatermarkExtender>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtUploadDate"
                CssClass="rfvPCG" ValidationGroup="btn_Upload" ErrorMessage="<br />Please select a Transaction Date"
                Display="Dynamic" runat="server" InitialValue="">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
 
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Browse:"></asp:Label>
        </td>
        <td class="rightField" colspan="3">
            <asp:FileUpload ID="FileUpload" runat="server" Height="22px" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload"
                runat="Server" ValidationGroup="btn_Upload" ErrorMessage="Only .dbf, .xls and.xlsx File allowed"
                Display="Dynamic" ValidationExpression="^.*\.((x|X)(l|L)(s|S)|(x|X)(l|L)(s|S)(x|X)|(d|D)(b|B)(f|F)|(t|T)(x|X)(t|T))$"
                CssClass="rfvPCG" />
            <br />
            <asp:RequiredFieldValidator ID="FileUpload_RequiredFieldValidator" ControlToValidate="FileUpload"
                ValidationGroup="btn_Upload" ErrorMessage="Please select a file for upload" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
   
    <tr>
        <td class="SubmitCell" colspan="4">
            <asp:Button ID="btn_Upload" runat="server" Text="Upload" OnClick="btn_Upload_Click"
                ValidationGroup="btn_Upload" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerUpload_btn_Upload','S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerUpload_btn_Upload','S');"
                OnClientClick="Page_ClientValidate();Loading(true);" />
            &nbsp;
        </td>
    </tr>
 
</table>