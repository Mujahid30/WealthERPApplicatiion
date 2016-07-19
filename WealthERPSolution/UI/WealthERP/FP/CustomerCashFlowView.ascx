<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCashFlowView.ascx.cs"
    Inherits="WealthERP.FP.CustomerCashFlowView" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Cash Flow Recommendation Details
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlCashFlowDetails" runat="server" ScrollBars="both" Width="100%"
                                    Visible="true">
                                    <telerik:RadGrid ID="gvCashFlowDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                                        AllowAutomaticInserts="false" OnNeedDataSource="gvCashFlowDetails_OnNeedDataSource">
                                        <ExportSettings HideStructureColumns="true">
                                        </ExportSettings>
                                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" DataKeyNames="CCRL_ID" AutoGenerateColumns="false"
                                            CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                                                    <ItemTemplate>
                                                           <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged"
                                                                Width="110px">
                                                                <Items>
                                                                    <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                                                    <asp:ListItem Text="View" Value="View" />
                                                                    <asp:ListItem Text="Edit" Value="Edit"/> 
                                                                   <asp:ListItem Text="Delete" Value="Delete" />

                                                                </Items>
                                                            </asp:DropDownList>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn HeaderText="Product"  DataField="CRPL_ID" UniqueName="Product"
                                                    SortExpression="Product" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Scheme" DataField="CCRL_SourceId" UniqueName="CCRL_SourceId"
                                                    SortExpression="CCRL_SourceId" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Buy/Sell" DataField="CCRL_BuyType" UniqueName="CCRL_BuyType"
                                                    SortExpression="CCRL_BuyType" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Start Date" DataField="CCRL_StartDate" UniqueName="CCRL_StartDate"
                                                    SortExpression="CCRL_StartDate" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:dd-MMM-yy}">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="End Date" DataField="CCRL_EndDate" UniqueName="CCRL_EndDate"
                                                    SortExpression="CCRL_EndDate" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:dd-MMM-yy}">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Frequency" DataField="CCRL_FrequencyMode" UniqueName="CCRL_FrequencyMode"
                                                    SortExpression="CCRL_FrequencyMode" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Amount" DataField="CCRL_Amount" UniqueName="CCRL_Amount"
                                                    SortExpression="CCRL_Amount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Sum Assure" DataField="CCRL_SumAssured" UniqueName="CCRL_SumAssured"
                                                    SortExpression="CCRL_SumAssured" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnStatusValue" runat="server" />

     <asp:HiddenField ID="hdnIsMainPortfolio" runat="server"/>
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />

