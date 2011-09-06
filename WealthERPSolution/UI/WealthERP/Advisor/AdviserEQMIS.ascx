<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserEQMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserEQMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 25px;
    }
    .style2
    {
        height: 26px;
    }
    .style3
    {
        width: 599px;
    }
    .style4
    {
        height: 49px;
    }
    .style5
    {
        width: 363px;
    }
</style>
<script type="text/javascript">

    function checkdate(sender, args) {
       
        var selectedDate = new Date();
        selectedDate = sender._selectedDate;
        
        var todayDate = new Date();
        var mssge = "";

        if (selectedDate > todayDate) {

            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future. Date value is reset to the current date");
        }
    }
</script>
<asp:ScriptManager ID="scptMgr" runat="server">
</asp:ScriptManager>

<table style="width: 100%;">
<tr>
        <td class="HeaderTextBig" colspan="2">
            <asp:Label ID="lblMfMIS" runat="server" CssClass="HeaderTextBig" Text="EQ MIS"></asp:Label>
            <hr />
        </td>
</tr>
</table>
<table>
<tr id="trEQMISTypeSelection" runat="server">
    <td>
    <asp:Label ID="lblMISType" runat="server" Width="90" CssClass="FieldName">MIS Type:</asp:Label>
    <asp:DropDownList ID="ddlMISType" style="vertical-align:middle" runat="server" 
            CssClass="cmbField" AutoPostBack="true" 
            onselectedindexchanged="ddlMISType_SelectedIndexChanged">
    <asp:ListItem Value="TurnOverSummery" Text="Turn Over Summary"></asp:ListItem>
    <asp:ListItem Value="CompanyWise" Text="Company Wise"></asp:ListItem>
    <asp:ListItem Value="SectorWise" Text="Sector Wise"></asp:ListItem>
    </asp:DropDownList>
    </td>
</tr>



    <tr runat="server" id="trRbtnPickDate">
        <td class="style1">
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
        </td>
        <td class="style1">
            &nbsp;
        </td>
    </tr>
</table>
<table runat="server" id="tblfromTo">
    <tr id="trRange" visible="false" runat="server">
        <td align="left" valign="top"  >
            <asp:Label ID="lblFromDate" runat="server" Width="90" CssClass="FieldName">From:</asp:Label>
            </td>
            <td valign="top">
            <asp:TextBox ID="txtFromDate" Width="180" runat="server" style="vertical-align: middle" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td valign="top" align="left">
            <asp:Label ID="lblToDate" runat="server" Width="60" CssClass="FieldName">To:</asp:Label>
            </td>
            <td valign="top">
            <asp:TextBox ID="txtToDate" runat="server" Width="180" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
        </td>
        <td class="style4">
        </td>
    </tr>
     <tr id="trPeriod" visible="false" runat="server">
        <td colspan="2">
            <asp:Label ID="lblPeriod" runat="server" Width="90" CssClass="FieldName">Period:</asp:Label>
            <asp:DropDownList ID="ddlPeriod" runat="server" Width="180" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
  
    </table>
    <table runat="server" id="tableComSecWiseOptions">
    <tr runat="server" id="trComSecWiseOptions">
        <td valign="top">
            <asp:Label ID="lblEQDate" runat="server" Width="90" CssClass="FieldName">As on Date:</asp:Label>
            <asp:TextBox ID="txtEQDate" runat="server" Width="180" style="vertical-align:middle" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="EQcalEX" runat="server" TargetControlID="txtEQDate"
                OnClientDateSelectionChanged="checkdate" Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="EQTxtWatermark" runat="server"
                TargetControlID="txtEQDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEQDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
          
        </td>
        
        <td >
             <asp:Label ID="lblEQPortfolio" runat="server" Text="Portfolio :"  CssClass="FieldName"></asp:Label>
                  <asp:DropDownList ID="ddlPortfolioGroup" runat="server" 
                 CssClass="cmbField" Width="180" AutoPostBack="true"
                 onselectedindexchanged="ddlPortfolioGroup_SelectedIndexChanged">
                        <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                        <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
                  </asp:DropDownList>
            </td>
        
    </tr>
    </table>
    <table>
    
    <tr runat="server" id="trBranchRmDpRow">
    <td align="left">
    <asp:Label ID="lblChooseBranchBM" runat="server" Font-Bold="true" Width="89" CssClass="FieldName" align="left"  Text="Branch:     "></asp:Label>
    <asp:DropDownList ID="ddlBranchForEQ" style="vertical-align: middle" Width="185" CssClass="cmbField" runat="server" 
            AutoPostBack="true" 
            onselectedindexchanged="ddlBranchForEQ_SelectedIndexChanged">
    </asp:DropDownList>
    </td>
    
    <td>
    <asp:Image Visible="false" runat="server" Width="10px" />
    <asp:Label ID="lblChooseRM" runat="server" Font-Bold="true" Width="60" CssClass="FieldName"  Text="RM: "></asp:Label>
    <asp:DropDownList ID="ddlRMEQ" style="vertical-align: middle" Width="180"  CssClass="cmbField" runat="server" 
            AutoPostBack="true" 
            onselectedindexchanged="ddlRMEQ_SelectedIndexChanged">
    </asp:DropDownList>
    </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" CssClass="PCGButton"
                ValidationGroup="btnGo" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AdviserEQMIS_btnGo', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AdviserEQMIS_btnGo', 'S');" />
        </td>
    </tr>
</table>

<table style="width: 100%">
    
    
                <tr id="ErrorMessage" align="center" style="width: 100%" runat="server">
                    <td align="center" style="width: 100%">
                        <div class="failure-msg" style="text-align:center" align="center">
                            No Equity Records found, please do valuation...
                        </div>
                    </td>
                </tr>
            
    <tr>
        <td colspan="2" align="center" style="width: 100%" class="style3">
            <asp:GridView ID="gvEQMIS" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" EnableViewState="false" CssClass="GridViewStyle" GridLines="Both" ShowFooter="True">
                <RowStyle CssClass="RowStyle" />
                
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
               <%--Previous one--%>
                
               <%-- End--%>
               
                 <Columns>
                    <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustIndDelby" runat="server" Text=""></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemCustIndDelby" runat="server" Text='<%# Eval("CName_Industry_Delby").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalText" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label ID="lblIndMValueDelSell" runat="server" Text=""></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemIndMValueDelSell" runat="server" Text='<%# Eval("Industry_MValue_DelSell").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblSectorWiseTotalText" runat="server" CssClass="Field" Font-Bold="true" 
                                            ForeColor="White" Text="">
                                        </asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                        <HeaderTemplate>
                            <asp:Label ID="lblMValuePerCSpecSell" runat="server" Text=""></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemMValuePerCSpecSell" CssClass="GridViewCmbFieldforBM"  style="text-align: right" runat="server" Text='<%# Eval("MValue_Percentage_SpecSell").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblFooterItemMValue" style="text-align: right" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Right">
                        <HeaderTemplate>
                            <asp:Label ID="lblMvalueBlankSpecbuy" runat="server" Text=""></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblItemMvalueBlankSpecbuy" CssClass="GridViewCmbFieldforBM"  runat="server" style="text-align: right" Text='<%# Eval("MValue_Blank_SpecBuy").ToString() %>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate>
                            <asp:Label ID="lblFooterItemMValueBlankSpecBuy"  style="text-align: right" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
    <tr id="ValuationNotDoneErrorMsg" align="center" style="width: 100%" runat="server">
                    <td align="center" style="width: 100%">
                        <div class="failure-msg" style="text-align:center" align="center">
                            Valuation not done for this adviser....
                        </div>
                    </td>
                </tr>
</table>

<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" />
<asp:HiddenField ID="hdnall" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />

<asp:HiddenField ID="hdnValuationDate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnEQMISType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnPortfolioType" runat="server" Visible="false" />
