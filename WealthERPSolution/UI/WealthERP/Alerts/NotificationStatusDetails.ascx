<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NotificationStatusDetails.ascx.cs"
    Inherits="WealthERP.Alerts.NotificationStatusDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Notification Status
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr id="trAddCategory">
        <td class="leftField">
            <asp:Label ID="lblAssetGroup1" runat="server" Text="AssetGroup:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlAssetGroupName1" runat="server" CssClass="cmbField"
           AutoPostBack="true"     OnSelectedIndexChanged="ddlAssetGroupName_OnSelectedIndexChanged">
                <asp:ListItem Selected="True" Value="0">SELECT</asp:ListItem>
                <asp:ListItem Selected="False" Value="IP">IPO</asp:ListItem>
                <asp:ListItem Selected="False" Value="FI">BOND</asp:ListItem>
                <asp:ListItem Selected="False" Value="MF">Mutual Fund</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator3" runat="server" ControlToValidate="ddlAssetGroupName1"
                ErrorMessage="Please,Select an AssetGroup." InitialValue="0" ValidationGroup='Submit'
                SetFocusOnError="true" Enabled="true"></asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label6" runat="server" Text="Notification Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="NotificationType_OnSelectedIndexChanged"
                CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span8" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator8" runat="server" ControlToValidate="DropDownList1"
                ErrorMessage="Please,Select an Notification Type." InitialValue="0" ValidationGroup='Submit'
                SetFocusOnError="true" Enabled="true"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" Text="Notification Header:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="NotificationHeader_OnSelectedIndexChanged"
                CssClass="cmbLongField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" ControlToValidate="DropDownList1"
                ErrorMessage="Please,Select a Notification Header." InitialValue="0" ValidationGroup='Submit'
                SetFocusOnError="true" Enabled="true"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" Text="Communication Channel:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="false" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" ControlToValidate="DropDownList3"
                ErrorMessage="Please,Select a field." InitialValue="0" ValidationGroup='Submit'
                SetFocusOnError="true" Enabled="true"></asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
            <asp:Button ID="Button1" Text='Submit' ValidationGroup='Submit' CausesValidation="true"
                CssClass="PCGButton" runat="server" OnClick="Submit_OnClick"></asp:Button>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server">
    <telerik:RadGrid ID="RadGrid3" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        AllowAutomaticDeletes="false" AllowAutomaticInserts="false" PageSize="10" 
        OnNeedDataSource="RadGrid3_NeedDataSource" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
        DataKeyNames="CTNEE_Id">
        <MasterTableView >
            <Columns>
               
                <telerik:GridBoundColumn UniqueName="CustCode" HeaderText="Client Code" DataField="CustCode">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn UniqueName="Name" HeaderText="Customer Name" DataField="Name">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CommId" HeaderText="EMailId/MobileNo" DataField="CommId">
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn UniqueName="[Subject]" HeaderText="Content" DataField="[Subject]">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CTNEE_CreatedOn" HeaderText="Created On" DataField="CTNEE_CreatedOn">
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn UniqueName="[Status]" HeaderText="Status" DataField="[Status]">
                </telerik:GridBoundColumn>
                
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <ClientEvents />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Panel>
