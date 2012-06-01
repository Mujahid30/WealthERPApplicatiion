<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminGoalSIPFundingSync.ascx.cs"
    Inherits="WealthERP.SuperAdmin.SuperAdminGoalSIPFundingSync" %>
<table width="100%">
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Goal Funding Sync"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgSyncComplete" runat="server" class="success-msg" align="center"
                visible="false">
                Folio Sync successfully Completed
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td align="center">
            <div id="msgGoalSynccomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Goal SIP Funding Sync not Completed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgGoalSyncincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Goal SIP Funding Sync not Completed
            </div>
        </td>
    </tr>
</table>--%>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgSyncincomplete" runat="server" class="failure-msg" align="center"
                visible="false">
                Folio Sync not Completed
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td id="trlbladvList" runat="server" align="right">
            <asp:Label ID="lbladviserList" runat="server" Text="Please Select Adviser :" CssClass="FieldName"></asp:Label>
        </td>
        <td id="tdDdlAdviserList" runat="server" align="right">
            <asp:DropDownList ID="ddlAdviserList" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td align="left">
            <asp:CompareValidator ID="cvDDLAdviserList" runat="server" ErrorMessage="<br />Please Select Adviser"
                ValidationGroup="SyncSubmit" ControlToValidate="ddlAdviserList" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <br />
    <tr>
        <td>
            <asp:Button ID="btnSubmitSync" runat="server" Text="Sync" CssClass="PCGButton" ValidationGroup="SyncSubmit"
                OnClick="btnSubmitSync_Click" />
        </td>
        <td>
            <asp:Button ID="btnSubmitFolio" runat="server" Text="FolioSync" CssClass="PCGButton" ValidationGroup="SyncSubmit"
                 OnClick="btnSubmitfolio_Click" />
        </td>
        <td>
        </td>
    </tr>
</table>
