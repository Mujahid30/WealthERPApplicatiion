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
        //alert(document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value());
        return false;
    };

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

//    function ClearText() {
//        alert(document.getElementById("<%= txtIndividualCustomer.ClientID %>").value);
//        document.getElementById('ctrl_MultiProductMIS_txtIndividualCustomer').value = ""; 
//    };
</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .fk-lres-header
    {
        font-size: 13px;
        margin-bottom: 10px;
        padding: 10px 7px;
    }
</style>
<table style="width: 100%;">
    <tr>
        <td class="HeaderTextBig">
            <asp:Label ID="lblMultiAssetMIS" runat="server" CssClass="HeaderTextBig" Text="Multi Product MIS"></asp:Label>
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
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle" onselectedindexchanged="ddlRM_SelectedIndexChanged">
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
    <tr id="trCustomerSearch" runat="server">
        <td class="leftField" style="width: 15%">
            &nbsp;
        </td>
        <td class="rightField" style="width: 15%">
            &nbsp;
        </td>
        <td class="leftField" style="width: 10%">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
       <td align="left" width="10%" onkeypress="return keyPress(this, event)">
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
                UseContextKey="True" DelimiterCharacters="" OnClientItemSelected="GetCustomerId"
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="CustomerValidation">
            </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>    
    <tr>
        <td align="right" style="padding-top:2px">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select MIS:"></asp:Label>
        </td>
        <td colspan="4">
            <div style="padding-top:2px"> 
                <asp:LinkButton ID="lnkBtnMultiProductMIS" Text="Multi-Product" CssClass="LinkButtonsWithoutUnderLine"
                    runat="server" OnClick="lnkBtnMultiProductMIS_Click" ValidationGroup="CustomerValidation"></asp:LinkButton>
                <span>|</span>
                <asp:LinkButton ID="lnkBtnLifeInsuranceMIS" Text="LifeInsurance" CssClass="LinkButtonsWithoutUnderLine"
                    runat="server" OnClick="lnkBtnLifeInsuranceMIS_OnClick" ValidationGroup="CustomerValidation"></asp:LinkButton>
                <span>|</span>
                <asp:LinkButton ID="lnkBtnGeneralInsuranceMIS" Text="General Insurance" CssClass="LinkButtonsWithoutUnderLine"
                    runat="server" OnClick="lnkBtnGeneralInsuranceMIS_OnClick" ValidationGroup="CustomerValidation"></asp:LinkButton>
                <span>|</span>
                <asp:LinkButton ID="lnkBtnInvestmentMIS" Text="Fixed Income" CssClass="LinkButtonsWithoutUnderLine"
                    runat="server" onclick="lnkBtnInvestmentMIS_Click" ValidationGroup="CustomerValidation"></asp:LinkButton>
            </div>
        </td>
    </tr>
    <tr>
    <td></td>
        <td colspan="4">
            <%--<asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Click on the MIS you would like to see"></asp:Label>   --%>
        </td>
    </tr>
    <%--</ContentTemplate>
    </asp:UpdatePanel> --%>
</table>




<table class="TableBackground" width="100%">
<tr>
    <td>
        <div class="divSectionHeading" style="vertical-align: text-bottom">
        <asp:Label ID="lblMISType" runat="server" CssClass="LinkButtons"></asp:Label>    
        </div>
    </td>
</tr>
    <tr id="trMISType" runat="server">
        <td>
                    
        </td>
    </tr>
    <tr id="trWrongCustomer" runat="server">
        <td align="center">
            <asp:Label ID="lblWrongCustomer" runat="server" CssClass="failure-msg" Visible="false"></asp:Label>
        </td>
    </tr>
</table>


<table class="TableBackground" width="100%">
    <tr id="trMultiProduct" runat="server">
        <td>
        <div style='overflow-x:scroll;overflow-y:hidden; width:97%'>
            <telerik:RadGrid ID="rgvMultiProductMIS" runat="server" Skin="Telerik" CssClass="RadGrid"
                GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" AllowAutomaticDeletes="false" FooterStyle-CssClass="FooterStyle" ShowFooter="true" 
                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet" DataKeyNames="C_CustomerId,Customer_Name" onItemCommand="rgvMultiProductMIS_ItemCommand"
                EnableEmbeddedSkins="false" Width="98%" OnNeedDataSource="rgvMultiProductMIS_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                </ExportSettings>                
                <MasterTableView DataKeyNames="C_CustomerId,Customer_Name" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true" ShowRefreshButton="false" 
                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Customer_Name" HeaderText="Customer Name" DataField="Customer_Name" 
                        HtmlEncode="false" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Equity" HeaderText="Equity" DataField="Equity"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:gridbuttonColumn ButtonType="LinkButton" CommandName="Redirect" UniqueName="Mutual_Fund" HeaderText="Mutual Fund" DataTextField="Mutual_Fund">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:gridbuttonColumn>
                    <%--    <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Mutual_Fund" HeaderText="Mutual Fund" DataField="Mutual_Fund"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Fixed_Income" HeaderText="Fixed Income" DataField="Fixed_Income"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Government_Savings" HeaderText="Government Savings"
                            DataField="Government_Savings" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Property" HeaderText="Property" DataField="Property"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Pension_and_Gratuity" HeaderText="Pension and Gratuity"
                            DataField="Pension_and_Gratuity" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Personal_Assets" HeaderText="Personal Assets"
                            DataField="Personal_Assets" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Gold_Assets" HeaderText="Gold Assets" DataField="Gold_Assets"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Collectibles" HeaderText="Collectibles" DataField="Collectibles"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Cash_and_Savings" HeaderText="Cash and Savings"
                            DataField="Cash_and_Savings" DataFormatString="{0:N0}" HtmlEncode="false"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right"/>
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn UniqueName="Assets_Total" HeaderText="Assets Total" DataField="Assets_Total"
                            DataFormatString="{0:F0}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>--%>                                              
                    </Columns>                       
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
                </ClientSettings>
            </telerik:RadGrid>
        </div>    
        </td>
    </tr>
    <tr id="trLabelMessage" runat="server">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="FieldName"
            Text="Note: The values on this screen include adjustmenet thus it will not match the values on other screens.">
            </asp:Label>
        </td>
    </tr>
    <tr id="trFixedIncome" runat="server">
        <td>
        <div style='overflow-x:scroll;overflow-y:hidden;width:97%'>
            <telerik:RadGrid ID="rgvFixedIncomeMIS" runat="server" Skin="Telerik" CssClass="RadGrid"
                GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" AllowAutomaticDeletes="false" FooterStyle-CssClass="FooterStyle" ShowFooter="true" 
                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet" Width="98%" 
                DataKeyNames="CustomerId" EnableEmbeddedSkins="false" OnNeedDataSource="rgvFixedIncomeMIS_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="CustomerId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <%--<CommandItemTemplate>
                        <div>
                            <asp:Label ID="lblMISType66" runat="server" Text="Fixed Income MIS"></asp:Label> 
                        </div>
                    </CommandItemTemplate>--%>
                    <CommandItemSettings AddNewRecordText="Fixed Income MIS" ShowExportToWordButton="true" ShowExportToExcelButton="true" ShowRefreshButton="false" 
                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false"/>
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Customer_Name" HeaderText="Customer Name" 
                        DataField="Customer_Name" HtmlEncode="false" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Left"/>
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn UniqueName="PAIC_AssetInstrumentCategoryName" HeaderText="Instrument Category" 
                        DataField="PAIC_AssetInstrumentCategoryName" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_Name" HeaderText="Asset Particulars" DataField="CFINP_Name"
                             HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_PurchaseDate" HeaderText="Purchase Date" DataField="CFINP_PurchaseDate"
                             HtmlEncode="false" DataFormatString="{0:d}" DataType="System.DateTime">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_MaturityDate" HeaderText="Maturity Date" DataFormatString="{0:d}"
                            DataField="CFINP_MaturityDate"  HtmlEncode="false" DataType="System.DateTime">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_PrincipalAmount" HeaderText="Deposit Amount" Aggregate="Sum"
                         DataField="CFINP_PrincipalAmount" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_InterestRate" HeaderText="Interest Rate"
                            DataField="CFINP_InterestRate" DataFormatString="{0:n2}" HtmlEncode="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_CurrentValue" HeaderText="Current Value" Aggregate="Sum"
                            DataField="CFINP_CurrentValue" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CFINP_MaturityValue" HeaderText="Maturity Value" Aggregate="Sum" DataField="CFINP_MaturityValue"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <%--<ClientEvents OnRowDblClick="RowDblClick" />--%>
                </ClientSettings>
            </telerik:RadGrid>
            </div>
        </td>
    </tr>
    <tr id="trGeneralInsurance" runat="server">
        <td>
        <div style='overflow-x:scroll;overflow-y:hidden;width:97%'>
            <telerik:RadGrid ID="rgvGeneralInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="98%"
                AllowAutomaticInserts="false" OnNeedDataSource="rgvGeneralInsurance_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="GenInsuranceNPId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="false" 
                         FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right" HeaderText="Customer Name">
                            <ItemStyle Width=""  HorizontalAlign="Left"  Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PolicyIssuerName" AllowFiltering="false" HeaderText="Policy Issuer">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Particulars" AllowFiltering="false" HeaderText="Particulars">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="InsuranceType" AllowFiltering="false" HeaderText="Insurance Type">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="SumAssured" AllowFiltering="false" 
                        HeaderText="Sum Assured" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="PremiumAmount" AllowFiltering="false" 
                        HeaderText="Premium Amount" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumFrequency" AllowFiltering="false" HeaderText="Premium Frequency">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="Commencement Date" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="maturity value">
                            <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="MaturityDate" AllowFiltering="false" HeaderText="Maturity Date" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
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
            </div>
        </td>
    </tr>
    <tr id="trLifeInsurance" runat="server">
        <td>
        <div style='overflow-x:scroll;overflow-y:hidden;width:97%'>
            <telerik:RadGrid ID="rgvLifeInsurance" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="98%"
                AllowAutomaticInserts="false" OnNeedDataSource="rgvLifeInsurance_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="InsuranceNPId" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="false"
                        FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right" HeaderText="Customer Name">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PolicyIssuerName" AllowFiltering="false" HeaderText="Policy Issuer">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Particulars" AllowFiltering="false" HeaderText="Particulars">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="InsuranceType" AllowFiltering="false" HeaderText="Insurance Type">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="SumAssured" AllowFiltering="false" HeaderText="Sum Assured"
                            DataFormatString="{0:N0}" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="PremiumAmount" AllowFiltering="false" HeaderText="premium amount"
                        FooterStyle-HorizontalAlign="Right" DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PremiumFrequency" AllowFiltering="false" HeaderText="Premium Frequency">
                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CommencementDate" AllowFiltering="false" HeaderText="Commencement Date" 
                        DataType="System.DateTime" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn  DataField="NextPremiumDate" HeaderText="Next Due Date" AllowFiltering="false"
                        UniqueName="NextPremiumDate" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="MaturityValue" AllowFiltering="false" HeaderText="Maturity Value"
                         FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MaturityDate" AllowFiltering="false" HeaderText="Maturity Date"
                        DataType="System.DateTime" DataFormatString="{0:d}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
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
            </div>
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="lblErrorMsg" runat="server" CssClass="failure-msg" Visible="false">
            </asp:Label>
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnCustomerId" runat="server" 
    onvaluechanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
