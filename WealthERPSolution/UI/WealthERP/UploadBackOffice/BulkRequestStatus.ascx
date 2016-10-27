<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BulkRequestStatus.ascx.cs"
    Inherits="WealthERP.UploadBackOffice.BulkRequestStatus" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<style type="text/css">
    .RadGrid_Telerik .rgGroupHeader
    {
        background: 0 -6489px repeat-x url('Grid/sprite.gif');
        line-height: 21px;
        color: #000;
        font-weight: BOLD;
    }
    div.RadGrid_Telerik .rgFooter td
    {
        background-image: url('ImageHandler.ashx?mode=get&suite=aspnet-ajax&control=Grid&skin=Telerik&file=rgCommandRow.gif&t=1437799218');
        color: #000;
    }
</style>
<div id ="dvAssocicateReport" runat="server" visible="false">
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
        <td align="right">
            <asp:Label ID="lblReportType" runat="server" Text="Select Type: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:RadioButton ID="rbAssocicatieAll" runat="server" Text="ALL" GroupName="AssociationSelection"
                CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbAssocicatieAll_AssociationSelection"
                Checked="true" />
            <asp:RadioButton ID="rdAssociateInd" runat="server" Text="Individual" GroupName="AssociationSelection"
                CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rbAssocicatieAll_AssociationSelection" />
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
        <td>
        <asp:CheckBox ID="cbIsDummyAgent" runat="server" Text="Is Dummy Associate"  CssClass="txtField"/>
        </td>
        <td align="right">
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
        <td align="left">
            <asp:Button ID="btnSubmit" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnSubmit_OnClick" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlOrderList" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rdAssociatePayout" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowAutomaticInserts="false" OnNeedDataSource="rdAssociatePayout_OnNeedDataSource"
                    AllowFilteringByColumn="true" >
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="AssociatePayOutReport">
                    </ExportSettings>
                    <MasterTableView ShowGroupFooter="true" Width="130%" DataKeyNames="AgentCode,AAC_AdviserAgentId,PAG_AssetGroupCode,AIM_IssueId,PAISC_AssetInstrumentSubCategoryCode,WCD_CommissionType,WCD_Act_Pay_BrokerageDate ">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="AgentCode" FieldName="AgentCode" />
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="AgentCode" SortOrder="Ascending" />
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="10%">
                                <HeaderTemplate>
                                    <asp:LinkButton ID="AllDetailslink" runat="server" CommandName="ExpandAllCollapse"
                                        Font-Underline="False" Font-Bold="true" UniqueName="AllDetailslink" Font-Size="Medium"
                                        OnClick="btnExpand_Click">+</asp:LinkButton>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                        Font-Bold="true" UniqueName="Detailslink" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <%-- <telerik:GridTemplateColumn UniqueName="ReportHeader">
                     <HeaderTemplate>
                     <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="1">
                     <tr>
                     <td colspan="2">
                     <asp:Label ID="lblHeader" runat="server"   Text="Associcate Report"></asp:Label>
                     </td>
                     </tr>
                     
                     </table>
                     </HeaderTemplate>
                     </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="AgentName" HeaderText="AgentName" UniqueName="AgentName"
                                SortExpression="AgentName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px" AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAG_AssetGroupName" HeaderText="Product" UniqueName="PAG_AssetGroupName"
                                SortExpression="PAG_AssetGroupName" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="85px" FilterControlWidth="85px"
                                AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Category "
                                UniqueName="PAISC_AssetInstrumentSubCategoryName" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="85px" FilterControlWidth="85px" AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name/Scheme Name"
                                UniqueName="AIM_IssueName" SortExpression="AIM_IssueName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="15%"
                                FilterControlWidth="85px" AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PayOut" HeaderText="Pay Out" UniqueName="PayOut"
                                SortExpression="PayOut" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px"
                                Aggregate="Sum" FooterText="Total:" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TDS" HeaderText="TDS" UniqueName="TDS" SortExpression="TDS"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px" Aggregate="Sum" FooterText="Total:" DataFormatString="{0:F2}"
                                FooterAggregateFormatString="{0:F2}" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ServiceTax" HeaderText="Service Tax" UniqueName="ServiceTax"
                                SortExpression="ServiceTax" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" Aggregate="Sum" FooterText="Total:"
                                DataFormatString="{0:F2}" FooterAggregateFormatString="{0:F2}" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NetPayOut" HeaderText="Net Pay Out" UniqueName="NetPayOut"
                                SortExpression="NetPayOut" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" Aggregate="Sum" FooterText="Total:"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WCD_Act_Pay_BrokerageDate" HeaderText="Pay Out Date"
                                UniqueName="WCD_Act_Pay_BrokerageDate" SortExpression="WCD_Act_Pay_BrokerageDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" AllowFiltering="false" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WCD_CommissionType" HeaderText="Commission Type"
                                UniqueName="WCD_CommissionType" SortExpression="WCD_CommissionType" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NoOfApplication" HeaderText="No.of.Applications"
                                UniqueName="NoOfApplication" SortExpression="NoOfApplication" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <ItemTemplate>
                                    <tr>
                                        <td colspan="100%">
                                            <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                Width="100%" ScrollBars="Both" Visible="false">
                                                <%-- <div style="display: inline; position: relative; left: 25px;">--%>
                                                <telerik:RadGrid ID="rgNCDIPOMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                                                    PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                                    Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="100%"
                                                    AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
                                                    EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" >
                                                    <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                                        CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                                                        GroupLoadMode="Client" ShowGroupFooter="true">
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px"  HeaderText="ApplicationNo." DataField="CO_ApplicationNumber"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="Application No" SortExpression="CO_ApplicationNumber"
                                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Right">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Transaction Date" DataField="transactionDate"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="transactionDate" SortExpression="transactionDate"
                                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Right" DataFormatString="{0:dd/MM/yyyy}">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                           <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Ordered Qty\Accepted Qty"
                                                                DataField="allotedQty" UniqueName="allotedQty" SortExpression="allotedQty" AutoPostBackOnFilter="true"
                                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AllowFiltering="false">
                                                                <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Mobilised Amount" DataField="ParentMobilize_Amount"
                                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Mobilised No Of Application"
                                                                DataField="ParentMobilize_Orders" AllowFiltering="false" ShowFilterIcon="false"
                                                                CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Center" Visible="false">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Brokerage Rate" DataField="rate"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="rate" SortExpression="rate" AutoPostBackOnFilter="true"
                                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Right">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText=" Brokerage Rate unit"
                                                                DataField="WCU_UnitCode" HeaderStyle-HorizontalAlign="Center" UniqueName="WCU_UnitCode"
                                                                SortExpression="WCU_UnitCode" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Service Tax(%)" DataField="ACSR_ServiceTaxValue"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_ServiceTaxValue" SortExpression="ACSR_ServiceTaxValue"
                                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Right">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="TDS(%)" DataField="ACSR_ReducedValue"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_ReducedValue" SortExpression="ACSR_ReducedValue"
                                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Right">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Expected Commission"
                                                                DataField="brokeragevalue" HeaderStyle-HorizontalAlign="Center" UniqueName="brokeragevalue"
                                                                SortExpression="brokeragevalue" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                                ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                                                                Aggregate="Sum">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn HeaderStyle-Width="50px" HeaderText="Net Commission" DataField="borkageExpectedvalue"
                                                                HeaderStyle-HorizontalAlign="Center" UniqueName="borkageExpectedvalue" SortExpression="borkageExpectedvalue"
                                                                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                                FooterStyle-HorizontalAlign="Right" Aggregate="Sum">
                                                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                            </telerik:GridBoundColumn>
                                                        </Columns>
                                                    </MasterTableView>
                                                    <ClientSettings>
                                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                                        <Resizing AllowColumnResize="true" />
                                                    </ClientSettings>
                                                </telerik:RadGrid>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <FooterStyle ForeColor="Black" />
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
</div>

<div id="dvReceivable" runat="server"  visible="true" style="width:100%">
<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Commission Consolidated Report
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="IbReceibaleReport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                                OnClientClick="setFormat('CSV')" Height="25px" Width="25px" Visible="false" OnClick="btnExportReceivableReport_OnClick">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="90%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td align="left" class="leftField" width="20%">
            <asp:Label ID="lblSelectProduct" runat="server" Text=" Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%" id="tdCategory" runat="server" visible="false">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" id="tdDdlCategory" runat="server" visible="false">
            <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProductCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlProductCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%">
            <asp:Label ID="Label4" runat="server" Text="Brokerage Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBrokerageType" runat="server" AutoPostBack="false" CssClass="cmbField" >
                <asp:ListItem Text="Receivable" Value="1"></asp:ListItem>
                <asp:ListItem Text="Payable" Value="0"></asp:ListItem>
            </asp:DropDownList>
            
        </td>
    </tr>
    <tr id="trSelectProduct" runat="server">
        <td id="td1" align="left" runat="server" class="leftField" width="16%" visible="true">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Commission Type:"></asp:Label>
        </td>
        <td id="td2" runat="server" visible="true">
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trNCDIPO" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueType" runat="server" CssClass="FieldName" Text="Issue Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlIssueType_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Closed Issues" Value="2"></asp:ListItem>
                <asp:ListItem Text="Current Issues" Value="1"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlIssueType" runat="server" ControlToValidate="ddlIssueType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue Type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select" Enabled="false"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trSelectMutualFund" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
            <asp:CompareValidator ID="CompareValidator41" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select Product Type"></asp:CompareValidator>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
      
    </tr>
    <tr>
     
        
          <td align="right">
            <asp:Label ID="Label2" runat="server" Text="From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="rptTxtFromDate" runat="server" CssClass="txtField">
            </asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="rptTxtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" runat="server" TargetControlID="rptTxtFromDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="rptTxtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a  Date" Display="Dynamic" ValidationGroup="vgbtnSubmit"
                runat="server" InitialValue=""></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator6" runat="server" CssClass="rfvPCG" ControlToValidate="rptTxtFromDate"
                Display="Dynamic" ErrorMessage="Invalid Date" Operator="DataTypeCheck" ValidationGroup="vgbtnSubmit"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="Label3" runat="server" Text="To Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="2" class="rightField">
            <asp:TextBox ID="rpttxtToDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="rpttxtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" runat="server" TargetControlID="rpttxtToDate"
                WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="rpttxtToDate" ValidationGroup="vgbtnSubmit"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Date" Display="Dynamic"
                runat="server" InitialValue="" ></asp:RequiredFieldValidator>
          
            <asp:CompareValidator ID="CompareValidator8" runat="server" CssClass="rfvPCG" ControlToValidate="rpttxtToDate"
                Display="Dynamic" ErrorMessage="Invalid Date" Operator="DataTypeCheck" ValidationGroup="vgbtnSubmit"
                Type="Date">
            </asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="rpttxtToDate" ControlToCompare="rpttxtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit" ></asp:CompareValidator>
        </td>
        <td class="rightField" style="padding-right: 50px">
            <asp:Button ID="btnGO" runat="server" CssClass="PCGButton" OnClick="btnGO_OnClick"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td colspan="6">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
        </td>
    </tr>
 
</table>
<asp:Panel ID="pnlProductDetails" runat="server" class="Landscape" Width="100%" Height="80%"
    ScrollBars="Horizontal" Visible="false">
    <table width="100%">
        <tr id="tr1" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="rgReceivableReport" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowAutomaticInserts="false" OnNeedDataSource="rgReceivableReport_OnNeedDataSource"
                    AllowFilteringByColumn="true" >
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="ReceivableCommissionReport" >
                    
                    </ExportSettings>
                    <MasterTableView ShowGroupFooter="true" Width="100%">
                        <GroupByExpressions>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="AIM_IssueName" FieldName="AIM_IssueName"  HeaderText="Issue Name"/>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="AIM_IssueName" SortOrder="Ascending" />
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                            <telerik:GridGroupByExpression>
                                <SelectFields>
                                    <telerik:GridGroupByField FieldAlias="WCD_BrokerName" FieldName="WCD_BrokerName"  HeaderText="Broker Name"/>
                                </SelectFields>
                                <GroupByFields>
                                    <telerik:GridGroupByField FieldName="WCD_BrokerName" SortOrder="Ascending" />
                                </GroupByFields>
                            </telerik:GridGroupByExpression>
                        </GroupByExpressions>
                        <Columns>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="IssueName" UniqueName="AIM_IssueName"
                                SortExpression="AIM_IssueName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAG_AssetGroupName" HeaderText="Product" UniqueName="PAG_AssetGroupName"
                                SortExpression="PAG_AssetGroupName" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="85px" FilterControlWidth="85px"
                                AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryName" HeaderText="Category "
                                UniqueName="PAISC_AssetInstrumentSubCategoryName" SortExpression="PAISC_AssetInstrumentSubCategoryName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="85px" FilterControlWidth="85px" AllowFiltering="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridBoundColumn DataField="Received" HeaderText="Received Commission" UniqueName="Received"
                                SortExpression="Received" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px"
                                Aggregate="Sum" FooterText="Total:" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TDS" HeaderText="TDS" UniqueName="TDS" SortExpression="TDS"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px" Aggregate="Sum" FooterText="Total:" DataFormatString="{0:F2}"
                                FooterAggregateFormatString="{0:F2}" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ServiceTax" HeaderText="Service Tax" UniqueName="ServiceTax"
                                SortExpression="ServiceTax" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" Aggregate="Sum" FooterText="Total:"
                                DataFormatString="{0:F2}" FooterAggregateFormatString="{0:F2}" AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NetReceived" HeaderText="Net Received" UniqueName="NetReceived"
                                SortExpression="NetReceived" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="100px" Aggregate="Sum" FooterText="Total:"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WCD_Act_Rec_BrokerageDate" HeaderText="Received Date"
                                UniqueName="WCD_Act_Rec_BrokerageDate" SortExpression="WCD_Act_Rec_BrokerageDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="100px" AllowFiltering="false" DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WCD_CommissionType" HeaderText="Commission Type"
                                UniqueName="WCD_CommissionType" SortExpression="WCD_CommissionType" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="No_of_orders" HeaderText="No.of Applications"
                                UniqueName="No_of_orders" SortExpression="No_of_orders" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                AllowFiltering="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <FooterStyle ForeColor="Black" />
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
<asp:HiddenField ID="hdnschemeId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnSBbrokercode" runat="server" />
<asp:HiddenField ID="hdnIssueId" runat="server" />
<asp:HiddenField ID="hdnProductCategory" runat="server" />
<asp:HiddenField ID="hdnAgentCode" runat="server" />
</div>
