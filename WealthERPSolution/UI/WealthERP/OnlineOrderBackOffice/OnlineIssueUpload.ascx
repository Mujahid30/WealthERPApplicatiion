<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineIssueUpload.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineIssueUpload" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 249px;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<%--<script type="text/javascript">

    function ValidateFileUpload(Source, args) {
        var fuData = document.getElementById('<%=clientId.ClientID %>');
        var FileUploadPath = fuData.value;

        if (FileUploadPath == '') {
            // There is no file selected
            args.IsValid = false;
        }
        else {
            var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

            if (Extension == "csv") 
            {
                args.IsValid = true; // Valid file type  || Extension == "docx" || Extension == "pdf"
                
            }
            else {
                args.IsValid = false; // Not valid file type
            }
        }
    }
</script>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Online Issue Upload
                        </td>
                        <%--<td align="right">
                            <asp:ImageButton ID="btnTradeBusinessDate" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="true" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnTradeBusinessDate_Click"></asp:ImageButton>
                        </td>--%>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" runat="server" visible="false">
    <tr id="trSumbitSuccess">
        <td align="center">
            <div id="msgRecordStatus" class="success-msg" align="center" runat="server"></div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="6" class="fltlftStep">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">1</div>
            </div>
        </td>
    </tr>
    <tr>
        <td align="right" class="leftLabel">
            <asp:Label ID="lblProduct" runat="server" CssClass="FieldName" Text="Select Product:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlProduct_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="NCD/Bond" Value="FI" />
                <asp:ListItem Text="IPO" Value="IP" />
            </asp:DropDownList></br>
            <asp:RequiredFieldValidator ID="rfvProduct" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a product"
                ControlToValidate="ddlProduct" Display="Dynamic" InitialValue="0" ValidationGroup="OnlineIssueUpload">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblSourceData" runat="server" CssClass="FieldName" Text="Source Data:"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlSourceData" runat="server" CssClass="cmbField" 
                AutoPostBack="true" onselectedindexchanged="ddlSourceData_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="BSE" Value="BSE" />
                <asp:ListItem Text="NSE" Value="NSE" />
            </asp:DropDownList><br />
            <asp:RequiredFieldValidator ID="rfvSourceData" runat="server" ErrorMessage="Please select Source Data"
                ControlToValidate="ddlSourceData" CssClass="rfvPCG" Display="Dynamic" InitialValue="0"
                ValidationGroup="OnlineIssueUpload"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblFileType" runat="server" CssClass="FieldName" Text="File Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlFileType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="BSE" Value="1" />
                <asp:ListItem Text="NSE" Value="2" />
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfgFileType" runat="server" ErrorMessage="Please select a File Type"
                ControlToValidate="ddlFileType" CssClass="rfvPCG" Display="Dynamic" InitialValue="0"
                ValidationGroup="OnlineIssueUpload"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="fltlftStep">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    2</div>
            </div>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:FileUpload ID="FileUpload" runat="server" />
        </td>
        <td>
            <asp:Button ID="btnFileUpload" CssClass="PCGButton" Text="Upload" 
                runat="server" onclick="btnFileUpload_Click"
                />
        </td>
    </tr>
    <tr>
        <td colspan="6" class="fltlftStep">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber fltlftStep">
                    3</div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" ValidationGroup="OnlineIssueUpload" CssClass="PCGButton" runat="server"
                Text="Go" />
        </td>
    </tr>
</table>
