<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderList.ascx.cs" Inherits="WealthERP.OPS.OrderList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script type="text/javascript">
    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
</script>

<table width="100%">
    <tr>
        <td style="width: 30%">
            <asp:Label ID="lblOrderList" runat="server" CssClass="HeaderTextBig" Text="Order List"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table width="80%" onkeypress="return keyPress(this, event)">
    <tr>
        <td align="right" valign="top">
            <asp:Label ID="lblFrom" runat="server" Text=" Order FromDate: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField">
            </asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFrom"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFrom"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFrom" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ControlToValidate="txtFrom"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="right" valign="top">
            <asp:Label ID="lblTo" runat="server" Text="Order ToDate: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTo"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtTo"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtTo" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="MFSubmit"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG" ControlToValidate="txtTo"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblBranch" runat="server" Text="Select The Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblRM" runat="server" Text="Select the RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="MFSubmit"
                OnClick="btnGo_Click" />
        </td>
        <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td colspan="2" align="left">
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOrderList" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Horizontal">
    <table width="100%">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvOrderList_OnNeedDataSource" OnItemCommand="gvOrderList_ItemCommand">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CO_OrderId,C_CustomerId" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="Top">
                        <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                            ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                        <Columns>
                            <telerik:GridButtonColumn ButtonType="LinkButton" CommandName="Redirect" UniqueName="CO_OrderId"
                                HeaderText="Order No." DataTextField="CO_OrderId" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridButtonColumn>
                            <%--<telerik:GridBoundColumn DataField="C_CustomerId" AllowFiltering="false" HeaderText="CustomerId"
                                UniqueName="C_CustomerId">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="IS_SchemeName" AllowFiltering="false" HeaderText="Scheme Name"
                                UniqueName="IS_SchemeName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XII_InsuranceIssuerName" AllowFiltering="false"
                                HeaderText="Issuer Name" UniqueName="XII_InsuranceIssuerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAG_AssetGroupName" AllowFiltering="false" HeaderText="Asset Name"
                                UniqueName="PAG_AssetGroupName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Order Date" UniqueName="CO_OrderDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationReceivedDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Application Received Date" UniqueName="CO_ApplicationReceivedDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_PolicyMaturityDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Maturity Date" UniqueName="CIOD_PolicyMaturityDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_PaymentDate" DataFormatString="{0:dd/MM/yyyy}"
                                AllowFiltering="false" HeaderText="Payment Date" UniqueName="CO_PaymentDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOSR_SourceName" AllowFiltering="false" HeaderText="Source Type"
                                UniqueName="WOSR_SourceName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNumber" AllowFiltering="false"
                                HeaderText="Application Number" UniqueName="CO_ApplicationNumber">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XPM_PaymentMode" AllowFiltering="false" HeaderText="Payment Mode"
                                UniqueName="XPM_PaymentMode">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ChequeNumber" AllowFiltering="false" HeaderText="Cheque Number"
                                UniqueName="CO_ChequeNumber">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BankName" AllowFiltering="false" HeaderText="Bank Name"
                                UniqueName="CB_BankName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CIOD_SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                                UniqueName="CIOD_SumAssured">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XF_Frequency" AllowFiltering="false" HeaderText="Frequency"
                                UniqueName="XF_Frequency">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:HiddenField ID="hdnBranchId" runat="server" />
<asp:HiddenField ID="hdnRMId" runat="server" />
<asp:HiddenField ID="hdnFromdate" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
