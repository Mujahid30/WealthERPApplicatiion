<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Upload.ascx.cs" Inherits="WealthERP.Admin.Upload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<div>
    <asp:ScriptManager ID="UploadScripManager" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td class="HeaderCell">
                <label id="lblheader" class="HeaderTextBig" title="Upload Screen">
                    Upload Screen</label>
            </td>
        </tr>
    </table>
</div>
<div>
 <%--   <table width="100%">
        <tr>
            <td>
                <asp:Button Style="display: none" ID="btnUploadReport" CssClass="PCGMediumButton"
                    Text="UploadReport" runat="server" OnClick="OnClick_UploadReport" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Upload_btnUploadReport','M');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Upload_btnUploadReport', 'M');" />
            </td>
            <td>
                <asp:Button Style="display: none" ID="btnRejectedRecord" CssClass="PCGMediumButton"
                    Text="Rejected Record" runat="server" OnClick="OnClick_RejectedRecord" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Upload_btnRejectedRecord','M');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Upload_btnRejectedRecord','M');" />
            </td>
            <td>
                <asp:Button Style="display: none" ID="btnViewInsertedRecord" CssClass="PCGButton"
                    Text="Inserted Record" Width="120px" runat="server" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Upload_btnViewInsertedRecord','S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Upload_btnViewInsertedRecord','S');" />
               
            </td>
        </tr>
    </table>--%>
</div>
<div id="MainDiv" runat="server" style="text-align:center;" >
    <table width="50%">
        <tr>
            <td class="leftField">
                <label id="lbl1" class="FieldName" title="Choose To Upload">
                    Choose To Upload:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlUploadType" runat="server">
                    <asp:ListItem Text="Price" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Corp Action" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="lblAssetGroup" class="FieldName" title=" Asset Group">
                    Asset Group:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlAssetGroup" runat="server">
                    <asp:ListItem Text="Equity" Value="1"></asp:ListItem>
                    <asp:ListItem Text="MF" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="SubmitCell">
                <asp:Button ID="btnUpload" Text="Upload" CssClass="PCGButton" runat="server" OnClick="OnClick_Upload"
                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Upload_btnUpload');" onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Upload_btnUpload');" />
            </td>
        </tr>
    </table>
</div>
<div id="divUploadReport" runat="server"  visible="true" enableviewstate="false">
   
</div>
<div id="RejectedRecordDiv" runat="server">
   <%-- <table style="width: 75%">
        <tr align="center">
            <td class="leftField">
                <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            </td>
            <td class="rightField">
                <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnRejectedRecords" runat="server" Text="Rejected Records" 
        onclick="btnRejectedRecords_Click" /> --%>
        <asp:Button ID="Button1" runat="server" Text="View Process Log" 
        onclick="Button1_Click" Width="129px" />

</div>
