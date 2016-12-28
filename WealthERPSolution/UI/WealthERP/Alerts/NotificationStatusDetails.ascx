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
                         <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="true" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
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
            <asp:DropDownList ID="ddlAssetGroupName1" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAssetGroupName_OnSelectedIndexChanged">
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
        <td class="leftField" >
            <asp:Label ID="Label1" runat="server" Text="Notification Header:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" >
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
        <td align="right">
            <asp:Label ID="lblNfostartdate" runat="server" Text="From Date" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td align="right">
            <asp:Label ID="lblNfoEnddate" runat="server" Text="To Date" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <tr>
            <td align="right">
                <asp:Button ID="Button1" Text='Submit' ValidationGroup='Submit' CausesValidation="true"
                    CssClass="PCGButton" runat="server" OnClick="Submit_OnClick"></asp:Button>
            </td>
        </tr>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
    <telerik:RadGrid ID="RadGrid3" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowAutomaticDeletes="false" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" PageSize="10" OnNeedDataSource="RadGrid3_NeedDataSource"
        AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="CTNEE_Id">
        <MasterTableView>
            <Columns>
                <telerik:GridBoundColumn DataField="CustCode" AllowFiltering="true" HeaderText="Client Code"
                    UniqueName="CustCode" SortExpression="CustCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="Name" AllowFiltering="true" HeaderText="Customer Name"
                    UniqueName="Name" SortExpression="Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CommId" AllowFiltering="true" HeaderText="EMailId/MobileNo"
                    UniqueName="CommId" SortExpression="CommId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="[Subject]" AllowFiltering="true" HeaderText="Content"
                    UniqueName="[Subject]" SortExpression="[Subject]" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CTNEE_CreatedOn" AllowFiltering="true" HeaderText="Created On"
                    UniqueName="CTNEE_CreatedOn" SortExpression="CTNEE_CreatedOn" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px"
                    FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="[Status]" AllowFiltering="true" HeaderText="Status"
                    UniqueName="[Status]" SortExpression="[Status]" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <ClientEvents />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Panel>
<asp:Panel ID="pnlWelcomeLetter" runat="server" ScrollBars="Vertical">
    <telerik:RadGrid ID="rgWelcomeLetter" runat="server" Skin="Telerik" CssClass="RadGrid" GridLines="None"
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowAutomaticDeletes="false" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" PageSize="10" OnNeedDataSource="rgWelcomeLetter_NeedDataSource"
        AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="CTNEE_Id">
        <MasterTableView>
            <Columns>
              <telerik:GridBoundColumn DataField="IFACode" AllowFiltering="true" HeaderText="IFA Code" 
                    UniqueName="IFACode" SortExpression="IFACode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                      <telerik:GridBoundColumn DataField="IFAName" AllowFiltering="true" HeaderText="IFA Name" 
                    UniqueName="IFAName" SortExpression="IFAName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="EmailId" AllowFiltering="true" HeaderText="Email ID" 
                    UniqueName="EmailId" SortExpression="EmailId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn DataField="EmailSentStatus" AllowFiltering="true" HeaderText="Email Sent status" 
                    UniqueName="EmailSentStatus" SortExpression="EmailSentStatus" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="NoOfRetries" AllowFiltering="true" HeaderText="No.of Retries" 
                    UniqueName="NoOfRetries" SortExpression="NoOfRetries" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="HasAttachment" AllowFiltering="true" HeaderText="Has Attachement " 
                    UniqueName="HasAttachment" SortExpression="HasAttachment" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn DataField="CreatedOn" AllowFiltering="true" HeaderText="Created on " 
                    UniqueName="CreatedOn" SortExpression="CreatedOn" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>

 </Columns>
        </MasterTableView>
        <ClientSettings>
            <ClientEvents />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Panel>