<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LiabilityView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.LiabilityView" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');

        if (bool) {
            document.getElementById("ctrl_LiabilityView_hdnMsgValue").value = 1;
            document.getElementById("ctrl_LiabilityView_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_LiabilityView_hdnMsgValue").value = 0;
            document.getElementById("ctrl_LiabilityView_hiddenassociation").click();
            return true;
        }
    }
</script>
<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Liabilities
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Liabilities"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr id="trErrorMessage" align="center" style="width: 100%" runat="server">
        <td align="center" style="width: 100%">
            <div class="failure-msg" style="text-align: center" align="center">
                No Liabilities Records found!!!!
            </div>
        </td>
    </tr>
    <tr id="trExportFilteredData" runat="server">
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center" visible="false">
                Record has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <telerik:RadGrid ID="gvLiabilities" runat="server" GridLines="None" AutoGenerateColumns="False"
                Width="100%" PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True"
                ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-Excel-Format="ExcelML" OnNeedDataSource="gvLiabilities_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="Liabilities Details">
                </ExportSettings>
                <MasterTableView AllowFilteringByColumn="false" Width="100%" DataKeyNames="LiabilityId"
                    AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn ItemStyle-Width="80Px" AllowFiltering="false">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged"
                                    CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                    AllowCustomText="true" Width="120px" AutoPostBack="true">
                                    <Items>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                        </telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                            runat="server"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Loan Type" AllowFiltering="false" HeaderText="Loan Type"
                            FooterText="Grand Total:" UniqueName="Loan Type">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Lender" AllowFiltering="false" HeaderText="Lender"
                            UniqueName="Lender">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="CL_AssetParticular" AllowFiltering="false" HeaderText="Asset Particular">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Amount" DataFormatString="{0:N0}" AllowFiltering="false"
                            HeaderText="Amount (Rs)" UniqueName="Amount" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Sum">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Rate of Interest" AllowFiltering="false" HeaderText="Rate of Interest (%)"
                            UniqueName="Rate of Interest (%)">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PaymentType" AllowFiltering="false" HeaderText="Payment Type"
                            UniqueName="PaymentType">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LumpsusmInstallment" DataFormatString="{0:N0}"
                            AllowFiltering="false" HeaderText="Lumpsum/ Installment" UniqueName="LumpsusmInstallment"
                            FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LoanOutstanding" AllowFiltering="false" DataFormatString="{0:N0}"
                            HeaderText="Loan Outstanding" UniqueName="LoanOutstanding" FooterStyle-HorizontalAlign="Right"
                            Aggregate="Sum">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       
                        <telerik:GridBoundColumn DataField="Frequency" AllowFiltering="false" HeaderText="Frequency"
                            UniqueName="Frequency">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="NextInstallmentDate" AllowFiltering="false" DataFormatString="{0:d}"
                            HeaderText="Next Installment Date">
                            <ItemStyle Width="" HorizontalAlign="center" Wrap="false" VerticalAlign="Top" />
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
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />