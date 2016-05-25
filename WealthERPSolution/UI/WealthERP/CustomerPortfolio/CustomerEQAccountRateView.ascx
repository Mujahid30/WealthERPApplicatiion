<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEQAccountRateView.ascx.cs" Inherits="WealthERP.CustomerPortfolio.CustomerEQAccountRateView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<script type="text/javascript">
    function ShowAlertToDelete() {

        var bool = window.confirm('Are you sure you want to delete this Rate?');

        if (bool) {
            document.getElementById("ctrl_CustomerEQAccountRateView_hdnStatusValue").value = 1;
            document.getElementById("ctrl_CustomerEQAccountRateView_btnTradeNoAssociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_CustomerEQAccountRateView_hdnStatusValue").value = 0;
            document.getElementById("ctrl_CustomerEQAccountRateView_btnTradeNoAssociation").click();
            return true;
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
                            View Rates
                        </td>
                        <td align="right">
                            <%--<asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="20px" Width="25px" OnClick="btnExportFilteredData_OnClick"></asp:ImageButton>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
  <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
<tr>
<td>
 <telerik:RadGrid ID="gvEQAccRate" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" OnNeedDataSource="gvEQAcc_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView  Width="100%" AllowMultiColumnSorting="True" DataKeyNames="CebId"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                       <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                    AllowCustomText="true" Width="120px" AutoPostBack="true" OnSelectedIndexChanged=" ddlAction_SelectedIndexChanged">
                                    <Items>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="Select"
                                            Selected="true"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                            runat="server"></telerik:RadComboBoxItem>
                                        <telerik:RadComboBoxItem ImageUrl="~/Images/delete.png" Text="Delete" Value="Delete"
                                            runat="server"></telerik:RadComboBoxItem>
                                    </Items>
                                </telerik:RadComboBox>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                       
                        <telerik:GridBoundColumn HeaderText="Trade Acc No" DataField="Trade No" UniqueName="Trade No"
                            SortExpression="Trade No" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       
                        <telerik:GridBoundColumn HeaderText="Transaction Mode" DataField="Transaction Mode"
                            UniqueName="Transaction Mode" SortExpression="Transaction Mode" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Transaction Type" DataField="Transaction Type"
                            UniqueName="Transaction Type" SortExpression="Transaction Type" AutoPostBackOnFilter="true"
                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Rate" DataField="Rate" UniqueName="Rate"
                            SortExpression="Rate" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                       <%-- <telerik:GridBoundColumn HeaderText="Other charges %" DataField="Other Charges" UniqueName="OtherCharges"
                            SortExpression="Other Charges" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn HeaderText="SEBI Turn Over Fee %" DataField="Sebi Turn Over Fee" UniqueName="Sebi Turn Over Fee"
                            SortExpression="Sebi Turn Over Fee" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                           <telerik:GridBoundColumn HeaderText="Transaction Charges %" DataField="Transaction Charges" UniqueName="Transaction Charges"
                            SortExpression="Transaction Charges" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="Stamp Charges %" DataField="Stamp Charges" UniqueName="Stamp Charges"
                            SortExpression="Stamp Charges" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="STT %" DataField="STT" UniqueName="STT"
                            SortExpression="STT" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn HeaderText="Service Tax %" DataField="Service Tax" UniqueName="Service Tax"
                            SortExpression="Service Tax" AutoPostBackOnFilter="true" AllowFiltering="false"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridDateTimeColumn DataField="Start Date" SortExpression="Start Date"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" AllowFiltering="true"
                            HeaderText="Start Date" UniqueName="Start Date" DataFormatString="{0:d}"
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            <FilterTemplate>
                                <telerik:RadDatePicker Width="250px" ID="calFilter" runat="server">
                                </telerik:RadDatePicker>
                            </FilterTemplate>
                        </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="End Date" SortExpression="End Date"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" AllowFiltering="true"
                            HeaderText="End Date" UniqueName="End Date" DataFormatString="{0:d}"
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            <FilterTemplate>
                                <telerik:RadDatePicker Width="250px" ID="calFilter" runat="server">
                                </telerik:RadDatePicker>
                            </FilterTemplate>
                        </telerik:GridDateTimeColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
            </td>
</tr>
</table>
<asp:HiddenField ID="hdnStatusValue" runat="server" />
<asp:Button ID="btnTradeNoAssociation" runat="server" BorderStyle="None" BackColor="Transparent"
    OnClick="btnTradeNoAssociation_Click" />