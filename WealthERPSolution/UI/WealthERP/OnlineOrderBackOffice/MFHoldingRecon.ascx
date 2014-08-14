<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFHoldingRecon.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.MFHoldingRecon" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            MF Holding Recon
                        </td>
                        <td align="right">
                            <%-- <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td align="right">
            <asp:Label ID="lblSelect" runat="server" CssClass="FieldName" Text="Select:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssue" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span26" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlIssue" runat="server" ErrorMessage="Please Select Request"
                CssClass="rfvPCG" ControlToValidate="ddlIssue" ValidationGroup="btnbasicsubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" OnClick="btnGo_OnClick" ValidationGroup="btnbasicsubmit"
                Text="Go" />
        </td>
    </tr>
    <tr id="trSynch" visible="false">
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select:"></asp:Label>
        </td>
        <td>
             <telerik:RadDatePicker ID="txtTo" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                    </Calendar>
                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                    </DateInput>
                </telerik:RadDatePicker>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select A Date"
                CssClass="rfvPCG" ControlToValidate="txtTo" ValidationGroup="btnSynch"
                Display="Dynamic"></asp:RequiredFieldValidator>
        </td>   
        </td>
        <td>
            <asp:Button ID="btnSynch" runat="server" CssClass="PCGButton" OnClick="btnSync_OnClick" ValidationGroup="btnSynch"
                Text="Sync" />
        </td>
    </tr>
    <tr>
</table>
<asp:Panel ID="pnlMFHoldingRecons" runat="server" ScrollBars="Horizontal" Height="100%" 
    Width="100%" Visible="false">
    <table width="100%">
        <tr>
            <td>
                <div id="MFHoldingRecons" runat="server" style="width: 100%; padding-left: 0px;"
                    visible="false">
                    <telerik:RadGrid ID="gvMFHoldinfRecon" runat="server" AllowAutomaticDeletes="false"
                        PageSize="20" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="true"
                        ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                        GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true" 
                        Width="120%" Height="400px" OnNeedDataSource="gvMFHoldinfRecon_OnNeedDataSource">
                        <MasterTableView DataKeyNames="" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="true" AllowFilteringByColumn="true">
                        </MasterTableView>
                        <ClientSettings>
                            <Resizing AllowColumnResize="false" />
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
