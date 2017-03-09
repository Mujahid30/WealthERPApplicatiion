<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFAccountValuation.ascx.cs" Inherits="WealthERP.OnlineOrderBackOffice.MFAccountValuation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>
<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script type = "text/javascript">
$(window).bind('load',function(){
    var headerChk =   $(".chkHeader input");
    var itemChk = $(".chkItem input");
    headerChk.bind("click",function(){
        itemChk.each(function(){this.checked = headerChk[0].checked;})
    });
    itemChk.bind("click",function(){if($(this).checked==false)headerChk[0].checked =false;});  
});
</script>
<script type="text/javascript" language="javascript">
    function ConfirmValuation() {
          var bool = window.confirm('Are you sure you want to Change Valuation ?');
        if (bool) {
            document.getElementById("ctrl_MFAccountValuation_hdnMsgValue").value = 1;
            document.getElementById("ctrl_MFAccountValuation_hiddenUpdate").click();
            return false;
        }
        else {
            document.getElementById("ctrl_MFAccountValuation_hdnMsgValue").value = 0;
            document.getElementById("ctrl_MFAccountValuation_hiddenUpdate").click();
            return true;
        }
    }

</script>

<table width="100%" class="TableBackground">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">MF Account Valuation</td>
        <td  align="right"style="padding-bottom:2px;">
           <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
             runat="server" AlternateText="Excel" ValidationGroup="sd" ToolTip="Export To Excel" 
               Height="25px" Width="25px"></asp:ImageButton>
        </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
<table width="100%">

<tr>
        <td style="width:30px">
            <asp:Label ID="lblFilter" runat="server" Text="Select:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width:60px">
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="cmbLongField">
                <asp:ListItem Value="S">Select</asp:ListItem>
                <asp:ListItem Value="0">PreProcessing</asp:ListItem>
                <asp:ListItem Value="1">InProcess</asp:ListItem>
                <asp:ListItem Value="2">Processed</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnGo_Click"/>
        </td>
        
</tr>
</table>
<table width="100%">
    <tr id="trPnlValuation" runat="server">
    
        <td>
        <telerik:RadGrid  ID="gvMFAccounts" runat="server"  AutoGenerateColumns="false"
                    PageSize="10"  AllowSorting="true" AllowPaging="True" 
                ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                 OnNeedDataSource="gvMFAccounts_NeedDataSource" >
                    <MasterTableView DataKeyNames="MFAIV_Id" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None" >
                        <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="20px" UniqueName="chkBoxColumn">
                        <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" runat="server"  CssClass="chkHeader" />
                        </HeaderTemplate>                        
                        <ItemTemplate>
                        <asp:CheckBox ID="chkItem" runat="server" CssClass="chkItem"></asp:CheckBox>
                        </ItemTemplate>
                        <ItemStyle Width="20px" />
                        </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="C_CustCode" SortExpression="C_CustCode" UniqueName="C_CustCode" AllowFiltering= "true" HeaderText="Customer Code"
                                ShowFilterIcon="false"  AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="Name" SortExpression="Name" UniqueName="Name" AllowFiltering= "true" HeaderText="Customer Name"
                                ShowFilterIcon="false"  AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="CMFA_FolioNum" SortExpression="CMFA_FolioNum" UniqueName="CMFA_FolioNum" AllowFiltering= "true" HeaderText="Folio Number"
                                 ShowFilterIcon="false"  AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" UniqueName="PASP_SchemePlanName" AllowFiltering= "true" HeaderText="Scheme Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="MFAIV_IsValued" SortExpression="MFAIV_IsValued" UniqueName="MFAIV_IsValued" AllowFiltering="true" HeaderText="Valuation Status"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="ValuationDate" SortExpression="ValuationDate" UniqueName="ValuationDate" AllowFiltering="true"  HeaderText="Last Valuation On"
                                 ShowFilterIcon="false"  CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                        </MasterTableView>
                       
        </telerik:RadGrid>
        </td>
        <td style="width:60px"></td>
    </tr>
    <tr id="trReProcess" runat="server">
        <td style="width:90px; ">
            <asp:Button ID="btnUpdate" runat="server" Text="ReProcess" CssClass="PCGButton" Visible="false" OnClick="btnUpdate_Click" Width="80px" />
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
            <asp:Button ID="hiddenUpdate" runat="server" OnClick="hiddenUpdate_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
</table>