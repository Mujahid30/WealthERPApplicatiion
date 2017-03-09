<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAUM.ascx.cs" Inherits="WealthERP.BusinessMIS.CustomerAUM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%=hdnCustomerId.ClientID %>").value = eventArgs.get_value();
       
        //alert(document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value());
        return false;
    };

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }
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

<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0" cellpadding="3" width="100%">
        <tr>
        <td align="left">Customer AUM</td>
        <td  align="right">
        <asp:ImageButton ID="btnMultiProductMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                    OnClientClick="setFormat('excel')" OnClick="btnMultiProductMIS_Click" Height="20px" Width="25px"></asp:ImageButton>
                          
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>

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
                Style="vertical-align: middle" >
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 35%">
        </td>
    </tr>
    <%--<asp:UpdatePanel ID="UPPickCustomer" runat="server">
    <ContentTemplate>--%>
    <tr>
        <td class="leftField" style="width: 15%">
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS:"></asp:Label>
        </td>
        <td class="rightField" style="width: 15%">
            <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                <asp:ListItem Value="All Customer" Text="All Customer" Enabled="false"></asp:ListItem>
                  <asp:ListItem Value="Select" Text="Select" Enabled="true"></asp:ListItem>
                <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic"
                ControlToValidate="ddlSelectCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the MIS"
                runat="server" ValidationGroup="CustomerValidation" InitialValue="Select">
            </asp:RequiredFieldValidator>
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
    <tr id="trCustomerSearch" runat="server" align="left">
       <%-- <td class="leftField" style="width: 15%">
            &nbsp;
        </td>
        <td class="rightField" style="width: 15%">
            &nbsp;
        </td>--%>
        <td  class="leftField" style="width: 10%">
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
    <tr id="trWrongCustomer" runat="server">
        <td align="center" colspan="4">
            <asp:Label ID="lblWrongCustomer" runat="server" CssClass="failure-msg" Visible="false"></asp:Label>
        </td>
    </tr>   
    <tr>
   <td>
     <asp:Button ID="btnGo" runat="server"  Text="Go" CssClass="PCGButton"
      ValidationGroup="CustomerValidation" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMMultipleTransactionView_btnGo', 'S');"
   
           onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMMultipleTransactionView_btnGo', 'S');" 
           onclick="btnGo_Click" />
     </td>
    </tr>

</table>
<table width="100%">
    <tr>
        <td>
        <asp:Panel ID="pnlMultiProductMIS" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
        <table>
          <tr><td>
            <div runat="server" id="divGvMultiProductMIS" visible="false" style="margin: 2px;width: 640px;">
            <telerik:RadGrid ID="rgvMultiProductMIS" runat="server" Skin="Telerik" CssClass="RadGrid" Visible="true" 
                GridLines="None" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                ShowStatusBar="true" AllowAutomaticDeletes="false" FooterStyle-CssClass="FooterStyle" ShowFooter="true" 
                AllowAutomaticInserts="false" AllowAutomaticUpdates="false" HorizontalAlign="NotSet" 
                DataKeyNames="C_CustomerId,Customer_Name" onItemCommand="rgvMultiProductMIS_ItemCommand"
                EnableEmbeddedSkins="false" Width="120%" OnNeedDataSource="rgvMultiProductMIS_OnNeedDataSource" 
                onDataBound="rgvMultiProductMIS_DataBound">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true" FileName="MultiProductMIS Details" Excel-Format="ExcelML">
                </ExportSettings>                
                <MasterTableView DataKeyNames="C_CustomerId,Customer_Name" Width="100%" AllowMultiColumnSorting="True"
                    AutoGenerateColumns="false" CommandItemDisplay="none">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false" ShowRefreshButton="false" 
                    ShowExportToCsvButton="false" ShowAddNewRecordButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn UniqueName="Customer_Name" HeaderText="Customer" DataField="Customer_Name" 
                        HtmlEncode="false" FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn UniqueName="RMName" HeaderText="RM" DataField="RmName" 
                        HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </telerik:GridBoundColumn>
                        
                         <telerik:GridBoundColumn UniqueName="BranchName" HeaderText="Branch" DataField="BranchName" 
                        HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Left" Wrap="false"/>
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Equity" HeaderText="Equity" DataField="Equity"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="Mutual_Fund" HeaderText="Mutual Fund" Groupable="False"
                            ItemStyle-Wrap="false" AllowFiltering="true" Aggregate="Sum" SortExpression="Mutual_Fund" ItemStyle-HorizontalAlign="Right"
                            DataField="Mutual_Fund" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkMF" runat="server" Text='<%# String.Format("{0:N0}", DataBinder.Eval(Container.DataItem, "Mutual_Fund")) %>' 
                                    CommandName="Redirect"  ></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Fixed_Income" HeaderText="Fixed Income" DataField="Fixed_Income"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Government_Savings" HeaderText="Government Savings"
                            DataField="Government_Savings" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Property" HeaderText="Property" DataField="Property"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Pension_and_Gratuity" HeaderText="Pension and Gratuity"
                            DataField="Pension_and_Gratuity" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Personal_Assets" HeaderText="Personal Assets"
                            DataField="Personal_Assets" DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Gold_Assets" HeaderText="Gold Assets" DataField="Gold_Assets"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Collectibles" HeaderText="Collectibles" DataField="Collectibles"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="Cash_and_Savings" HeaderText="Cash and Savings"
                            DataField="Cash_and_Savings" DataFormatString="{0:N0}" HtmlEncode="false"
                            FooterStyle-HorizontalAlign="Right" Visible="false">
                            <ItemStyle HorizontalAlign="Right"/>
                        </telerik:GridBoundColumn>
                        
                        <telerik:GridBoundColumn Aggregate="Sum" UniqueName="TotalAUM" HeaderText="Total AUM" DataField="TotalAUM"
                            DataFormatString="{0:N0}" HtmlEncode="false" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="Right" />
                        </telerik:GridBoundColumn>
                                            
                    </Columns>                       
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div> 
       </td></tr>
    </table>
    </asp:Panel>   
        </td>
    </tr>
    <%--<tr id="trLabelMessage" runat="server">
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="FieldName"
            Text="Note: The values on this screen include adjustment thus it will not match the values on other screens.">
            </asp:Label>
        </td>
    </tr>--%>
</table>

<asp:HiddenField ID="hdnCustomerId" runat="server" 
    onvaluechanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />