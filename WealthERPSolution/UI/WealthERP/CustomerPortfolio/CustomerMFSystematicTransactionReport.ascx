<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFSystematicTransactionReport.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMFSystematicTransactionReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<style type="text/css">
    .style1
    {
        width: 138px;
    }
    #tblPickDate
    {
        width: 29%;
    }
</style>

<script type="text/javascript" language="javascript">
    
</script>

<%--
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>--%>
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblSystematicTransactionsReport" runat="server" Text="MF SystematicTransactionsReport"
                CssClass="HeaderTextSmall"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr id="tr1" runat="server">
                    <td id="Td1" runat="server">
                        <asp:Label ID="lblGroupPortfolio" runat="server" CssClass="FieldName" Text="Portfolio :"></asp:Label>
                    </td>
                    <td id="Td2" runat="server">
                        <asp:DropDownList ID="ddlGroupPortfolioGroup" runat="server" CssClass="cmbField"
                            OnSelectedIndexChanged="ddlGroupPortfolioGroup_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Text="Managed" Value="MANAGED" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="UnManaged" Value="UN_MANAGED"></asp:ListItem>
                            <asp:ListItem Text="All" Value="ALL"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td id="Td3" runat="server">
                        <asp:Label ID="lblReportType" runat="server" CssClass="FieldName" Text="View:"></asp:Label>
                    </td>
                    <td id="Td4" runat="server">
                        <asp:DropDownList ID="ddlViewType" runat="server" CssClass="cmbLongField" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlViewType_SelectedIndexChanged">
                            <asp:ListItem Text="All" Value="ALL" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Actual Transactions Not Found" Value="NAT"></asp:ListItem>
                            <asp:ListItem Text="Systematic Transactions Not Found" Value="NST"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table id="tblPickDate" border="0">
                <tr>
                    <td class="style1">
                        <asp:RadioButton ID="rbtnPickDate" Checked="true" runat="server" GroupName="Date"
                            Text="Pick a date range" OnCheckedChanged="rbtnPickDate_CheckedChanged" AutoPostBack="true"
                            CssClass="FieldName" />
                    </td>
                    <td>
                        <asp:RadioButton ID="rbtnPickPeriod" runat="server" GroupName="Date" Text="Pick a Period"
                            OnCheckedChanged="rbtnPickDate_CheckedChanged" AutoPostBack="true" CssClass="FieldName" />
                    </td>
                </tr>
            </table>
            <table id="trRange" runat="server">
                <tr>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                            Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                    </td>
                    <td valign="middle" align="left">
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                            Format="dd/MM/yyyy">
                        </ajaxToolkit:CalendarExtender>
                        <ajaxToolkit:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
                        </ajaxToolkit:TextBoxWatermarkExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtToDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                            Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
                    </td>
                </tr>
            </table>
            <table id="trPeriod" runat="server" visible="false">
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName" Text="Period:"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <br />
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                            ValidationGroup="btnGo">
                        </asp:CompareValidator>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnGen" runat="server" Text="Go" CssClass="PCGButton" OnClick="btnGen_Click" />
        </td>
    </tr>
    <tr id="trErrorMessage" runat="server" align="left" visible="false">
        <td>
            <asp:Label ID="lblErrorMessage" CssClass="HeaderTextSmaller" runat="server" Text="No Records Matching the Selected Criteria"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlSystematicTransactions" runat="server" Height="500px" Width="100%"
                ScrollBars="Vertical" Visible="false" HorizontalAlign="Left">
                <div id="dvHoldings" runat="server" style="width: 650px; padding:4px">
                <telerik:RadGrid ID="gvSystematicTransactions" runat="server" GridLines="None" AutoGenerateColumns="False"
                    AllowSorting="true" AllowPaging="false" ShowStatusBar="True" ShowFooter="true" PageSize="10"
                    OnItemCreated="gvSystematicTransactions_ItemCreated"
                    OnItemDataBound="gvSystematicTransactions_ItemDataBound" OnNeedDataSource="gvSystematicTransactions_OnNeedDataSource"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true" 
                    AllowAutomaticInserts="false">
                    <exportsettings HideStructureColumns="true" ExportOnlyData="true">
                    </exportsettings>
                    <mastertableview datakeynames="RowId" width="100%" allowmulticolumnsorting="True"
                        autogeneratecolumns="false" commanditemdisplay="Top">
                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                            ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false"/>
                        <Columns>   
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSystematicTransaction" runat="server" Visible="false" CssClass="Field" 
                                    AutoPostBack="true" OnCheckedChanged="chkSystematicTransaction_CheckedChanged" />
                                </ItemTemplate> 
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />                                
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridBoundColumn DataField="CustomerName" AllowFiltering="true" HeaderText="CustomerName"
                                UniqueName="CustomerName">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn> 
                                                
                            <telerik:GridBoundColumn DataField="Folio" AllowFiltering="false" HeaderText="Folio"
                                UniqueName="Folio">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>  
                            <telerik:GridBoundColumn DataField="Scheme" AllowFiltering="true" HeaderText="Scheme"
                                UniqueName="Scheme">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                                                        
                            <telerik:GridTemplateColumn AllowFiltering="false">
                                <HeaderTemplate>
                                    <asp:Label CssClass="label" ID="lblTransactionType" runat="server" Text='Trans Type'></asp:Label>
                                    <asp:DropDownList ID="ddlTranType" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                        OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label CssClass="label" ID="lblContainer" runat="server" Text='<%# Eval("SystematicType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridTemplateColumn> 
                                    
                            <telerik:GridBoundColumn DataField="SystematicAmount" AllowFiltering="false" HeaderText="SystematicAmount"
                                UniqueName="SystematicAmount">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>   
                            <telerik:GridBoundColumn DataField="SystematicDate" AllowFiltering="false" HeaderText="SystematicDate"
                                UniqueName="SystematicDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>   
                            <telerik:GridBoundColumn DataField="ActualAmount" AllowFiltering="false" HeaderText="ActualAmount"
                                UniqueName="ActualAmount">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>  
                            <telerik:GridBoundColumn DataField="ActualDate" AllowFiltering="false" HeaderText="ActualDate"
                                UniqueName="ActualDate">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>                                                
                        </Columns>
                    </mastertableview>
                    <clientsettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </clientsettings>
                </telerik:RadGrid>
                </div>
                <%--<asp:GridView ID="gvSystematicTransactions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyNames="RowId" Width="624px" Height="78px" 
                        Font-Size="Small" CssClass="GridViewStyle" 
                        OnRowDataBound="gvSystematicTransactions_RowDataBound" ShowFooter="true">
                        <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                        <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                        <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                        <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <Columns>                      

                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                   <asp:CheckBox ID="chkSystematicTransaction" runat="server" Visible="false" CssClass="Field" AutoPostBack="true" OnCheckedChanged="chkSystematicTransaction_CheckedChanged" />
                                </ItemTemplate>
                                
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                    <asp:TextBox ID="txtCustomerSearch" runat="server" CssClass="GridViewtxtField" onkeydown="return JSdoPostback(event,'ctrl_CustomerMFSystematicTransactionReport_btnSysTransactionSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCustomerNameHeader" runat="server" Text='<%# Eval("CustomerName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Folio" />
                            
                            <asp:TemplateField ItemStyle-Wrap="false">
                                <HeaderTemplate>
                                    <asp:Label ID="lblScheme" runat="server" Text="Scheme"></asp:Label>
                                    <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="GridViewtxtField" onkeydown="return JSdoPostback(event,'ctrl_CustomerMFSystematicTransactionReport_btnSysTransactionSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeHeader" runat="server" Text='<%# Eval("Scheme").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                             <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Type">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTranType" runat="server" Text="Trans Type"></asp:Label>
                                    <asp:DropDownList ID="ddlTranType" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                      OnSelectedIndexChanged="ddlTranType_SelectedIndexChanged"   >
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTranTypeHeader" runat="server" Text='<%# Eval("SystematicType").ToString() %>'></asp:Label>
                                </ItemTemplate>
                                
                                <ItemStyle Wrap="False"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SystematicAmount" HeaderText="SystematicAmount" ItemStyle-HorizontalAlign="Right" />
                            <asp:BoundField DataField="SystematicDate" HeaderText="SystematicDate" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ActualAmount" HeaderText="ActualAmount" ItemStyle-HorizontalAlign="Right"  />
                            <asp:BoundField DataField="ActualDate" HeaderText="ActualDate" ItemStyle-HorizontalAlign="Center" />

                        </Columns>
                        
                    </asp:GridView>--%>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnRegister" runat="server" CssClass="PCGLongButton" Text="Register Systematic Setup"
                OnClick="btnRegister_Click" Visible="false" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSysTransactionSearch" runat="server" Text="" BorderStyle="None"
                BackColor="Transparent" OnClick="btnSysTransactionSearch_Click" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnDdlTranTypeSelectedValue" runat="server" />
