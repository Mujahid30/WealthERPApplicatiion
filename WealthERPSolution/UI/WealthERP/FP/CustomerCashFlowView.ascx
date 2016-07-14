<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerCashFlowView.ascx.cs" Inherits="WealthERP.FP.CustomerCashFlowView" %>
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
                                         Skin="Telerik" EnableEmbeddedSkins="false"
                                        Width="100%" AllowFilteringByColumn="true" AllowAutomaticInserts="false">
                                        <ExportSettings HideStructureColumns="true">
                                        </ExportSettings>
                                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" DataKeyNames=""
                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="Product" Visible="false" DataField="" UniqueName="Product"
                                                    SortExpression="Product" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Payment Type" DataField="CCRL_SourceId" UniqueName="CCRL_SourceId"
                                                    SortExpression="CCRL_SourceId" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Start Date" DataField="CCRL_StartDate" UniqueName="CCRL_StartDate" SortExpression="CCRL_StartDate"
                                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="End Date" DataField="CCRL_EndDate" UniqueName="CCRL_EndDate"
                                                    SortExpression="CCRL_EndDate" AutoPostBackOnFilter="true"  AllowFiltering="false" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Frequency" DataField="CCRL_FrequencyMode" UniqueName="CCRL_FrequencyMode"
                                                    SortExpression="CCRL_FrequencyMode" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Amount" DataField="CCRL_Amount"
                                                    UniqueName="CCRL_Amount" SortExpression="CCRL_Amount" AutoPostBackOnFilter="true"
                                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Sum Assure" DataField="CCRL_SumAssured"
                                                    UniqueName="CCRL_SumAssured" SortExpression="CCRL_SumAssured" AutoPostBackOnFilter="true"
                                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
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
