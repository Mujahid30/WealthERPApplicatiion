<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEQAccountView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerEQAccountView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function ShowAlertToDelete() {

        var bool = window.confirm('Are you sure you want to delete this Trade Account?');

        if (bool) {
            document.getElementById("ctrl_CustomerEQAccountView_hdnStatusValue").value = 1;
            document.getElementById("ctrl_CustomerEQAccountView_btnTradeNoAssociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_CustomerEQAccountView_hdnStatusValue").value = 0;
            document.getElementById("ctrl_CustomerEQAccountView_btnTradeNoAssociation").click();
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
                            View Equity Trade Account
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                Height="20px" Width="25px" OnClick="btnExportFilteredData_OnClick"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            <telerik:RadComboBox ID="ddlPortfolio" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged"
                CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                AllowCustomText="true" Width="120px" AutoPostBack="true">
            </telerik:RadComboBox>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <telerik:RadGrid ID="gvEQAcc" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" ExportSettings-FileName="Equity Equity Trade Account Details"
                OnNeedDataSource="gvEQAcc_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="AccountId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                            <ItemTemplate>
                                <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged"
                                    CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                    AllowCustomText="true" Width="120px" AutoPostBack="true">
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
                        <telerik:GridBoundColumn HeaderText="Broker Name" DataField="Broker Name" UniqueName="BrokerName"
                            SortExpression="Broker Name" AutoPostBackOnFilter="true" AllowFiltering="true"
                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Trade Acc No" DataField="Trade No" UniqueName="TradeNo"
                            SortExpression="Trade No" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="A/C Opening Date" SortExpression="A/C Opening Date"
                            AutoPostBackOnFilter="true" CurrentFilterFunction="EqualTo" AllowFiltering="true"
                            HeaderText="Account Opening date" UniqueName="A/COpeningDate" DataFormatString="{0:dd-MMM-yy}"
                            ShowFilterIcon="false">
                            <ItemStyle Width="110px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                           
                        </telerik:GridBoundColumn>
                         <%--<telerik:GridBoundColumn HeaderText="Bank"  DataField="CB_CustBankAccId" UniqueName="CB_CustBankAccId"
                            SortExpression="CB_CustBankAccId" AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false"
                            CurrentFilterFunction="Contains">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
            
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnStatusValue" runat="server" />
<asp:Button ID="btnTradeNoAssociation" runat="server" BorderStyle="None" BackColor="Transparent"
    OnClick="btnTradeNoAssociation_Click" />
     <asp:HiddenField ID="hdnIsMainPortfolio" runat="server"/>
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />

