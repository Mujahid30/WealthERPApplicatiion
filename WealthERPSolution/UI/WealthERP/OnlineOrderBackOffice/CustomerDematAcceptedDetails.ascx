<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerDematAcceptedDetails.ascx.cs" Inherits="WealthERP.OnlineOrderBackOffice.CustomerDematAcceptedDetails" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Customer Details 
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                        <tr>
                            <td>
                                <asp:Panel ID="pnlCustomerDetails" runat="server" ScrollBars="both" Width="100%"
                                    Visible="true">
                                    <telerik:RadGrid ID="gvCustomerDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                        OnItemDataBound="gvCustomerDetails_ItemDataBound" Skin="Telerik" EnableEmbeddedSkins="false"
                                        Width="100%" AllowFilteringByColumn="true" AllowAutomaticInserts="false">
                                        <ExportSettings HideStructureColumns="true">
                                        </ExportSettings>
                                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" DataKeyNames="C_CustomerId,C_IsDematInvestor"
                                            AutoGenerateColumns="false" CommandItemDisplay="None">
                                            <Columns>
                                                <telerik:GridBoundColumn HeaderText="Client Code" DataField="C_CustCode" UniqueName="C_CustCode"
                                                    SortExpression="Client Code" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Customer Name" DataField="C_FirstName" UniqueName="Customer Name"
                                                    SortExpression="Customer Name" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="PAN" DataField="C_PANNum" UniqueName="PAN" SortExpression="Transaction Type"
                                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Email ID" DataField="C_Email" UniqueName="Email ID"
                                                    SortExpression="Email ID" AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false"
                                                    CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Mobile Number" DataField="C_Mobile1" UniqueName="Mobile Number"
                                                    SortExpression="Mobile Number" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Demat Accepted" DataField="C_IsDematAccepted"
                                                    UniqueName="Demat Accepted" SortExpression="Demat Accepted" AutoPostBackOnFilter="true"
                                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn HeaderText="Demat Acceptance Date" DataField="C_DematAcceptedon"
                                                    UniqueName="Demat Acceptance Date" SortExpression="Demat Acceptance Date" AutoPostBackOnFilter="true"
                                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Demat Investor" AllowFiltering="false" DataField="C_IsDematInvestor"
                                                    UniqueName="C_IsDematInvestor">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk" runat="server" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                               
                                            </Columns>
                                        </MasterTableView>
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                        </ClientSettings>
                                    </telerik:RadGrid>
                                    
                                    <asp:Button ID="Button1" runat="server" Visible="true" Text="Update" OnClick="Button1_Click" />
                                </asp:Panel>
                            </td>
                        </tr>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>



<asp:HiddenField ID="hdnIsProspect" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnSystemId" runat="server" />
<asp:HiddenField ID="hdnClientId" runat="server" />
<asp:HiddenField ID="hdnName" runat="server" />
<asp:HiddenField ID="hdnGroup" runat="server" />
<asp:HiddenField ID="hdnPAN" runat="server" />
<asp:HiddenField ID="hdnBranch" runat="server" />
<asp:HiddenField ID="hdnArea" runat="server" />
<asp:HiddenField ID="hdnCity" runat="server" />
<asp:HiddenField ID="hdnProcessId" runat="server" />
<asp:HiddenField ID="hdnSystemAddDate" runat="server" />
<asp:HiddenField ID="hdncustomerCategoryFilter" runat="server" />
<asp:HiddenField ID="hdnPincode" runat="server" />
<asp:HiddenField ID="hdnIsRealInvester" runat="server" />
<asp:HiddenField ID="hdnRequestId" runat="server" />