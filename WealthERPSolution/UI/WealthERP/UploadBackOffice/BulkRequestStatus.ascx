<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkRequestStatus.ascx.cs" Inherits="WealthERP.UploadBackOffice.BulkRequestStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<style type="text/css">
.RadGrid_Telerik .rgGroupHeader
{
    background:0 -6489px repeat-x url('Grid/sprite.gif');
    
    line-height:21px;
	color:#000;	
 font-weight: BOLD;
}
div.RadGrid_Telerik .rgFooter td {

	background-image: url('ImageHandler.ashx?mode=get&suite=aspnet-ajax&control=Grid&skin=Telerik&file=rgCommandRow.gif&t=1437799218');
	color: #000;

}

</style>
<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Associate Commission Consolidated Report
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnExportFilteredDupData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredDupData_OnClick"
                                OnClientClick="setFormat('CSV')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblMenu" cellspacing="0" width="100%">
    <tr>
     <td align="right" >
            <asp:Label ID="lblReportType" runat="server" Text="Select Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="rbAssocicatieAll" runat="server" Text="ALL" GroupName="AssociationSelection" CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbAssocicatieAll_AssociationSelection" Checked="true"/>
            <asp:RadioButton ID="rdAssociateInd" runat="server" Text="Individual" GroupName="AssociationSelection" CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbAssocicatieAll_AssociationSelection"/>
        </td>
        <td align="right" id="tdlblAgentCode" runat="server" visible="false">
            <asp:Label ID="lblAgentCode" runat="server" Text="AgentCode:" CssClass="FieldName"></asp:Label>
        </td>
        <td id="tdtxtAgentCode" runat="server" visible="false">
            <asp:TextBox ID="txtAgentCode" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAgentCode"
                ErrorMessage="<br />Please Enter AgentCode" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td align="right" >
            <asp:Label ID="lblFrom" runat="server" Text="From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField">
            </asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtFromDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a  Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ControlToValidate="txtFromDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnGo" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblTo" runat="server" Text="To Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtToDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
            <%--<asp:RegularExpressionValidator ID="rev1" runat="server" CssClass="rfvPCG" ValidationExpression="[0-9][0-9]/[0-9][0-9]/[0-9][0-9][0-9][0-9]"
            ControlToValidate="txtTo" Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="MFSubmit">
            </asp:RegularExpressionValidator>--%>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG" ControlToValidate="txtToDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnGo" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
        </td>
        <td  align="left">
            <asp:Button ID="btnSubmit" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="btnGo" OnClick="btnSubmit_OnClick"
                  />
        </td>
    </tr>
</table>

<asp:Panel ID="pnlOrderList" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Horizontal" Visible="false" >
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rdAssociatePayout" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false" OnNeedDataSource="rdAssociatePayout_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="AssociatePayOutReport">
                    </ExportSettings>
                    <MasterTableView ShowGroupFooter="true" Width="102%">
                        <GroupByExpressions>
                           
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="AgentCode" FieldName="AgentCode"   />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="AgentCode" SortOrder="Ascending"  />
                                    
                                    
                                </GroupByFields>
                                
                            </telerik:GridGroupByExpression>
                           
                        </GroupByExpressions>
                        <Columns>
                     
                         <telerik:GridBoundColumn DataField="AgentName" AllowFiltering="true" HeaderText="AgentName"
                                UniqueName="AgentName" SortExpression="AgentName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAG_AssetGroupName" AllowFiltering="true" HeaderText="Product"
                                UniqueName="PAG_AssetGroupName" SortExpression="PAG_AssetGroupName" ShowFilterIcon="false"
                                CurrentFilterFunction="EqualTo" AutoPostBackOnFilter="true" HeaderStyle-Width="85px"
                                FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" AllowFiltering="true"
                                HeaderText="Category " UniqueName="PAISC_AssetInstrumentSubCategoryName" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="85px" FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PayOut" AllowFiltering="true" HeaderText="Pay Out"
                                UniqueName="PayOut" SortExpression="PayOut" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px" Aggregate="Sum"  FooterText="Total:">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TDS" AllowFiltering="true" HeaderText="TDS" UniqueName="TDS"
                                SortExpression="TDS" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px" Aggregate="Sum" FooterText="Total:
                                ">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ServiceTax" AllowFiltering="true" HeaderText="Service Tax"
                                UniqueName="ST" SortExpression="ServiceTax" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" Aggregate="Sum" FooterText="Total:">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NetPayOut" AllowFiltering="true" HeaderText="NetPayOut"
                                UniqueName="NetPayOut" SortExpression="NetPayOut" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" Aggregate="Sum" FooterText="Total:">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                           
                        </Columns>
                        <FooterStyle  ForeColor="Black" />
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>