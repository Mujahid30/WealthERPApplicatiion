<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiProductMIS.ascx.cs" Inherits="WealthERP.BusinessMIS.MultiProductMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .fk-lres-header {
    font-size: 13px;
    margin-bottom: 10px;
    padding: 5px 7px;
    border:solid 1px;
    }
</style>
<table style="width:100%;">
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
        <td class="leftField" style="width:15%">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
        </td>
        <td class="rightField" style="width:15%">
            <asp:DropDownList ID="ddlBranch" runat="server" style="vertical-align: middle" AutoPostBack="true"
            CssClass="cmbField" onselectedindexchanged="ddlBranch_SelectedIndexChanged" >
         </asp:DropDownList> 
        </td>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
        </td>
        <td class="rightField" style="width:25%">
            <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" style="vertical-align: middle" >
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width:35%"></td>
    </tr>
    <%--<asp:UpdatePanel ID="UPPickCustomer" runat="server">
    <ContentTemplate>--%>
    <tr>        
        <td class="leftField" style="width:15%">
            <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
        </td>
        <td class="rightField" style="width:15%">
            <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="cmbField" style="vertical-align: middle" AutoPostBack="true"  onselectedindexchanged="ddlCustomerType_SelectedIndexChanged">
                <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
            </asp:DropDownList>
            <%--<asp:RadioButton runat="server" ID="rdoAllCustomer" Text="All Customers" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" Checked="True" oncheckedchanged="rdoAllCustomer_CheckedChanged"/> --%>  
        </td>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
        </td>
        <td class="rightField" style="width:25%">
            <%--<asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" oncheckedchanged="rdoPickCustomer_CheckedChanged"/>--%>
            <asp:DropDownList ID="ddlSelectCustomer" style="vertical-align: middle" runat="server" CssClass="cmbField" AutoPostBack="true" onselectedindexchanged="ddlSelectCustomer_SelectedIndexChanged">
                <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Group Head" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
            </asp:DropDownList>   
        </td>
        <td class="leftField" style="width:35%"></td>
    </tr>
    <tr>        
        <td class="leftField" style="width:15%">
            &nbsp;</td>
        <td class="rightField" style="width:15%">
            &nbsp;</td>
        <td class="leftField" style="width:10%">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
        <td class="rightField" style="width:25%">
                <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="True">  </asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer" WatermarkText="Enter few chars of Customer"
                runat="server" EnableViewState="false"></cc1:TextBoxWatermarkExtender>
                <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters="" Enabled="True"  />
                  
         <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic" ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer" runat="server" ValidationGroup="btnGo">
         </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" style="width:35%"></td>
    </tr>
    <%--</ContentTemplate>
    </asp:UpdatePanel> --%>   
</table>
<div class="fk-lres-header" style="margin-left:10%; padding-bottom:5px;width:43%;text-align:center">
    <asp:LinkButton ID="lnkBtnMultiProductMIS" Text="Multi-Product" CssClass="LinkButtons" runat="server"></asp:LinkButton>
    <span>|</span>

    <asp:LinkButton ID="lnkBtnLifeInsuranceMIS" Text="LifeInsurance" CssClass="LinkButtons" runat="server"></asp:LinkButton>
    <span>|</span>

    <asp:LinkButton ID="lnkBtnGeneralInsuranceMIS" Text="General Insurance" CssClass="LinkButtons" runat="server"></asp:LinkButton>
    <span>|</span>

    <asp:LinkButton ID="lnkBtnInvestmentMIS" Text="Investment" CssClass="LinkButtons" runat="server"></asp:LinkButton>       
</div>

<div class="divSectionHeading" style="vertical-align: text-bottom">
</div>
<table class="TableBackground">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnCustomerId" runat="server"/>
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />