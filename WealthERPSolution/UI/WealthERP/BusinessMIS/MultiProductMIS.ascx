<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiProductMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.MultiProductMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        alert(document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value());
        return false;
    };
</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .fk-lres-header
    {
        font-size: 13px;
        margin-bottom: 10px;
        padding: 5px 7px;
        border: solid 1px;
    }
</style>
<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig">
            <asp:Label ID="lblMultiAssetMIS" runat="server" CssClass="HeaderTextBig" Text="Multi-Asset MIS"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<%--<div class="divSectionHeading" style="vertical-align: text-bottom">
</div>--%>
<table class="TableBackground" width="100%">
    <tr id="trBranchRM" runat="server">
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <%--<asp:UpdatePanel ID="UPPickCustomer" runat="server">
    <ContentTemplate>--%>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
            </asp:DropDownList>
            <%--<asp:RadioButton runat="server" ID="rdoAllCustomer" Text="All Customers" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" Checked="True" oncheckedchanged="rdoAllCustomer_CheckedChanged"/> --%>
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <%--<asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" oncheckedchanged="rdoPickCustomer_CheckedChanged"/>--%>
            <asp:DropDownList ID="ddlCustomerType" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Value="0" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="1" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 15%">
            &nbsp;
        </td>
        <td class="rightField" style="width: 15%">
            &nbsp;
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
       <td align="left" width="10%">
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <%--</ContentTemplate>
    </asp:UpdatePanel> --%>
</table>
<div class="fk-lres-header" style="margin-left: 10%; padding-bottom: 5px; width: 43%;
    text-align: center">
    <asp:LinkButton ID="lnkBtnMultiProductMIS" Text="Multi-Product" CssClass="LinkButtons"
        runat="server" OnClick="lnkBtnMultiProductMIS_Click"></asp:LinkButton>
    <span>|</span>
    <asp:LinkButton ID="lnkBtnLifeInsuranceMIS" Text="LifeInsurance" CssClass="LinkButtons"
        runat="server" OnClick="lnkBtnLifeInsuranceMIS_OnClick"></asp:LinkButton>
    <span>|</span>
    <asp:LinkButton ID="lnkBtnGeneralInsuranceMIS" Text="General Insurance" CssClass="LinkButtons"
        runat="server" OnClick="lnkBtnGeneralInsuranceMIS_OnClick"></asp:LinkButton>
    <span>|</span>
    <asp:LinkButton ID="lnkBtnInvestmentMIS" Text="Investment" CssClass="LinkButtons"
        runat="server" onclick="lnkBtnInvestmentMIS_Click"></asp:LinkButton>
</div>
<div class="divSectionHeading" style="vertical-align: text-bottom">
</div>
<table>
    <tr>
        <td align="center">
            <asp:Label ID="lblErrorMsg" runat="server" CssClass="failure-msg" Visible="false">
            </asp:Label>
        </td>
    </tr>
</table>
<table class="TableBackground">
    <tr>
        <td>
            <telerik:RadGrid ID="rgvMultiProductMIS" runat="server" Skin="Telerik" CssClass="RadGrid"
                GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" AllowAutomaticDeletes="false" FooterStyle-CssClass="FooterStyle" ShowFooter="true" 
                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                DataKeyNames="CustomerId">
                <MasterTableView DataKeyNames="CustomerId">
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Customer_Name" HeaderText="Customer Name" DataField="Customer_Name" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn UniqueName="Equity" HeaderText="Equity" DataField="Equity"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Mutual_Fund" HeaderText="Mutual Fund" DataField="Mutual_Fund"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Fixed_Income" HeaderText="Fixed Income" DataField="Fixed_Income"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Government Savings" HeaderText="Government Savings"
                            DataField="Government Savings" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Property" HeaderText="Property" DataField="Property"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Pension_and_Gratuity" HeaderText="Pension and Gratuity"
                            DataField="Pension_and_Gratuity" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Personal_Assets" HeaderText="Personal Assets"
                            DataField="Personal_Assets" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Gold_Assets" HeaderText="Gold Assets" DataField="Gold_Assets"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Collectibles" HeaderText="Collectibles" DataField="Collectibles"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="Cash_and_Savings" HeaderText="Cash and Savings"
                            DataField="Cash_and_Savings" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn UniqueName="Assets_Total" HeaderText="Assets Total" DataField="Assets_Total"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>

<table class="TableBackground">
    <tr>
        <td>
            <telerik:RadGrid ID="rgvFixedIncomeMIS" runat="server" Skin="Telerik" CssClass="RadGrid"
                GridLines="None" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" AllowAutomaticDeletes="false" FooterStyle-CssClass="FooterStyle" ShowFooter="true" 
                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet"
                DataKeyNames="CustomerId">
                <MasterTableView DataKeyNames="CustomerId">
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Customer_Name" HeaderText="Customer Name" 
                        DataField="Customer_Name" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn UniqueName="PAIC_AssetInstrumentCategoryName" HeaderText="Instrument Category" 
                        DataField="PAIC_AssetInstrumentCategoryName" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_Name" HeaderText="Asset Particulars" DataField="CFINP_Name"
                             HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_PurchaseDate" HeaderText="Purchase Date" DataField="CFINP_PurchaseDate"
                             HtmlEncode="false" DataFormatString="{0:d}">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_MaturityDate" HeaderText="Maturity Date" DataFormatString="{0:d}"
                            DataField="CFINP_MaturityDate"  HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_SubsequentDepositAmount" HeaderText="Deposit Amount"
                         DataField="CFINP_SubsequentDepositAmount" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_InterestRate" HeaderText="Interest Rate"
                            DataField="CFINP_InterestRate" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_CurrentValue" HeaderText="Current Value"
                            DataField="CFINP_CurrentValue" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_MaturityValue" HeaderText="Maturity Value" DataField="CFINP_MaturityValue"
                            DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>                        
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>

<table class="TableBackground">
    <tr>
        <td>
            <telerik:RadGrid ID="rgvGeneralInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="GenInsuranceNPId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="false" HeaderText="Customer Name">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PolicyIssuerName" AllowFiltering="false" HeaderText="Policy Issuer">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Particulars" AllowFiltering="false" HeaderText="Particulars">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="InsuranceType" AllowFiltering="false" HeaderText="Insurance Type">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                            DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumAmount" AllowFiltering="false" HeaderText="Premium Amount">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumFrequency" AllowFiltering="false" HeaderText="Premium Frequency">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="Commencement Date" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="maturity value">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="MaturityDate" AllowFiltering="false" HeaderText="Maturity Date" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="Next Premium due date"
                            DataFormatString="{0:n2}">
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
    <tr>
        <td>
            <telerik:RadGrid ID="rgvLifeInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="InsuranceNPId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="false" HeaderText="Customer Name">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PolicyIssuerName" AllowFiltering="false" HeaderText="Policy Issuer">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Particulars" AllowFiltering="false" HeaderText="Particulars">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="InsuranceType" AllowFiltering="false" HeaderText="Insurance Type">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                            DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumAmount" AllowFiltering="false" HeaderText="premium amount">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumFrequency" AllowFiltering="false" HeaderText="Premium Frequency">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="Commencement Date" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MaturityValue" AllowFiltering="false" HeaderText="Maturity Value" DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MaturityDate" AllowFiltering="false" HeaderText="Maturity Date" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                      <%--  <telerik:GridBoundColumn AllowFiltering="false" HeaderText="Next Premium due date"
                            DataFormatString="{0:n2}">
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

<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
