<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NCDIssueTransact.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.NCDIssueTransact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script type="text/javascript" >
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;
    }



</script>

<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 15%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 41%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>
<%--<asp:UpdatePanel ID="upCMGrid" runat="server">
    <ContentTemplate>--%>
                
            
            
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            BONDS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ibtExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="25px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
        <tr>
        <td class="leftLabel" colspan="2">
            <asp:Label ID="lblMSG" runat="server" Text="" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData" colspan="2">
            &nbsp;
        </td>
        
        </tr>
        <tr>
        <td class="leftLabel" colspan="1">
            <asp:Label ID="lblHolderDetails" runat="server" Text="Joint Holders Name" CssClass="FieldName"></asp:Label>
            : 
        </td>
        <td class="rightData" colspan="2">
             <asp:Label ID="lblHolderTwo" runat="server" Text=" "  ></asp:Label>
            <asp:Label ID="lblHolderThird" runat="server" Text=" "  ></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        </tr>
        <tr>
        <td class="leftLabel" colspan="1">
            <asp:Label ID="lblNominee" runat="server" Text="Nominee Name" CssClass="FieldName"></asp:Label>
            : 
        </td>
        <td class="rightData" colspan="2">
              <asp:Label ID="lblNomineeTwo" runat="server" Text=" "  ></asp:Label>
            <asp:Label ID="lblNomineeThird" runat="server" Text=" "  ></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        </tr>
        
        
        
        <tr>
        <td class="leftLabel" colspan="2">
            <asp:Label ID="lblAccountCode" runat="server" Text="Account Code" CssClass="FieldName"></asp:Label>
             : 
        </td>
        <td class="rightData" colspan="2">
           <asp:Label ID="lblCustomerId" runat="server" Text="ESI123456" CssClass="FieldName"></asp:Label>
        </td>
        </tr>
        
        <tr>
        <td class="leftLabel" colspan="2">
            <asp:Label ID="lblIssuer" runat="server" Text="" CssClass="FieldName"></asp:Label>
             : 
             <asp:DropDownList ID="ddIssuerList" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
        </td>
        <td class="rightData" colspan="2">
            <asp:Button ID="btnConfirm" runat="server" Text="confirm" OnClick="btnConfirm_Click" />
        </td>
        </tr>
        <tr>
        <td   colspan="4">
        <div style="width: 950px; overflow: scroll;">
            <telerik:RadGrid ID="gvCommMgmt" AllowSorting="false" runat="server" EnableLoadOnDemand="True"
                    AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                    ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                    AllowFilteringByColumn="false" onitemdatabound="gvCommMgmt_ItemDataBound" >
                    <headercontextmenu enableembeddedskins="False"></headercontextmenu>
                    <exportsettings hidestructurecolumns="false" exportonlydata="true" filename="LiveBondList"></exportsettings>
                    <pagerstyle alwaysvisible="True" />
                    <mastertableview allowmulticolumnsorting="True" allowsorting="true" DataKeyNames="PFISD_SeriesId,PFIIM_IssuerId,PFISM_SchemeId,PFISD_DefaultInterestRate,PFISD_Tenure,AIM_FaceValue,PFISD_InMultiplesOf,PFISD_BidQty"
                        autogeneratecolumns="false" width="100%">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf" />
                                            
                                            <%--Columns>
                                               <telerik:GridBoundColumn HeaderStyle-Width="38px"></telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn   HeaderText="SportID" HeaderStyle-Width="120px" UniqueName="ID"></telerik:GridBoundColumn>
                                               <telerik:GridBoundColumn   HeaderText="Sport" UniqueName="Name"></telerik:GridBoundColumn>
                                            </Columns>--%>
                                            
                                            
                                            <Columns>                                                                                                                   
                                                 <telerik:GridBoundColumn  DataField="PFISM_SchemeId" HeaderStyle-Width="100px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Scheme" UniqueName=" SchemeId" SortExpression="SeriesId">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFIIM_IssuerId" HeaderStyle-Width="100px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Issuer Id" UniqueName="IssuerId" SortExpression="IssuerId">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_SeriesId" HeaderStyle-Width="100px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Series" UniqueName="SeriesId" SortExpression="SeriesId">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" 
                                                        Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="PFISD_Tenure" HeaderStyle-Width="200px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Tenure" UniqueName="Tenure" SortExpression="Tenure">
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="200px" Wrap="true" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_CouponRate" HeaderStyle-Width="100px" 
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Coupon Rate" UniqueName="CouponRate" SortExpression="CouponRate">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_CouponFreq" HeaderStyle-Width="100px" 
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Coupon Frequency" UniqueName="CouponFreq" SortExpression="CouponFreq">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_RenewCouponRate" HeaderStyle-Width="100px" 
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Renew Coupon Rate" UniqueName="RenewCouponRate" SortExpression="RenewCouponRate">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="150px" HeaderText="Face Value"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="FaceValue" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_DefaultInterestRate" HeaderStyle-Width="150px" HeaderText="Yield at Call"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="DefaultInterestRate" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_YieldUpto" HeaderStyle-Width="150px" HeaderText="Yield at Maturity"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="YieldUpto" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_YieldatBuyBack" HeaderStyle-Width="150px" HeaderText="Yield at BuyBack"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="YieldatBuyBack" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_LockingPeriod" HeaderStyle-Width="150px" HeaderText="Lock-in Period"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="LockingPeriod" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_CallOption" HeaderStyle-Width="150px" HeaderText="Call Option"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="CallOption" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PFISD_BuyBackFacility" HeaderStyle-Width="150px" HeaderText="Is Buy Back Facility"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="BuyBackFacility" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                
                                                
                                                <telerik:GridBoundColumn DataField="PFISD_BidQty" HeaderStyle-Width="150px" HeaderText="Minimum Bid Quantity"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="MinBidQty" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                
                                                
                                                <telerik:GridBoundColumn DataField="PFISD_InMultiplesOf" HeaderStyle-Width="150px" HeaderText="Multiple allowed"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    UniqueName="InMultiplesOf" Visible="true">
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="150px" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" DataField=""   HeaderStyle-Width="100px" UniqueName="Quantity"   HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtQuantity" runat="server" ontextchanged="txtQuantity_TextChanged" AutoPostBack="true" OnKeypress="javascript:return isNumberKey(event);" ></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridTemplateColumn AllowFiltering="false" DataField=""  HeaderStyle-Width="100px" UniqueName="Amount" HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" BackColor="Gray" ForeColor="White" Font-Bold="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px" UniqueName="Check"  HeaderText="Check Order">
                                                    <ItemTemplate>
                                                       
                                                        <asp:CheckBox ID="cbOrderCheck" runat="server" />
                                                        
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false" DataField="" Visible="false" HeaderStyle-Width="100px" UniqueName="AmountAtMaturity"  HeaderText="Amount at Maturity">
                                                    <ItemTemplate>
                                                       <asp:LinkButton ID="lbconfirmOrder" runat="server" Text="Confirm Order" OnClick="lbconfirmOrder_Click" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                
                                                
                                            </Columns>
                                            <editformsettings>
                                                <editcolumn cancelimageurl="Cancel.gif" editimageurl="Edit.gif" 
                                                    insertimageurl="Update.gif" updateimageurl="Update.gif">
                                                </editcolumn>
                                            </editformsettings>
                                            <PagerStyle AlwaysVisible="True" />
                                        </mastertableview>
                    <clientsettings>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                            <Resizing AllowColumnResize="true" />                                          
                                        </clientsettings>
                    <filtermenu enableembeddedskins="False"></filtermenu>
                </telerik:RadGrid>
            
            </div>
            
        </td>
        
        
    </tr>
</table>
<table>
                <tr>
                    <td>
                        Confirm Your Order : 
                        <asp:Button ID="btnConfirmOrder" runat="server" Text="Submit" 
                            onclick="btnConfirmOrder_Click" />
                    </td>
                </tr>
            </table>


<%--<table id="tblCommissionStructureRule" runat="server" width="100%">
         --%>
<%--<tr>
                <td>
                    <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">--%>
<%-- <table width="100%">
                            <tr>
                                <td>--%>

<%-- </td>
                            </tr>
                        </table>--%>
<%-- </asp:Panel>
                </td>
            </tr>--%>
<%--</table>--%>
<%--    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="ibtExportSummary" />
    </Triggers>
</asp:UpdatePanel>--%>