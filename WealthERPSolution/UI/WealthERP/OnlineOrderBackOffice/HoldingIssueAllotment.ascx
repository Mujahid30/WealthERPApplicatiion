<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HoldingIssueAllotment.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.HoldingIssueAllotment" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Issue Allotment
                        </td>
                        <%--<td align="right">
                            <asp:ImageButton ID="btnTradeBusinessDate" ImageUrl="~/Images/Export_Excel.png" runat="server"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="true" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnTradeBusinessDate_Click"></asp:ImageButton>
                        </td>--%>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table width="100%">
    <tr>
        <td align="right">
            <asp:Label ID="lblproduct" CssClass="FieldName" runat="server" Text="Select Product"
                valign="top"></asp:Label>
        </td>
        <td>
            <%--<asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="false">
            <asp:DropDownList ID="ddlPrduct" runat="server" CssClass="cmbField" AutoPostBack="true">
                <Items>
                    <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    <asp:ListItem Text="Mutual Funds" Value="MF" />
                    <asp:ListItem Text="Bonds" Value="BO"/>
                </Items>
            </asp:DropDownList>
        </td>
        <td align="right">
           
        </td>
        <td id="tdRandTMF" runat="server" visible="false">
         <asp:Label ID="lbltoSrt" CssClass="FieldName" runat="server" Text="Select R&T:"></asp:Label>
            <asp:DropDownList ID="ddlRandTMF" runat="server" CssClass="cmbField" >
                <Items>
                    <asp:ListItem Text="CAMS" Value="CA" />
                    <asp:ListItem Text="KARVY" Value="KV" />
                    <asp:ListItem Text="SUNDARAM" Value="SM" />
                    <asp:ListItem Text="TEMPLETON" Value="TN" />
                </Items>
            </asp:DropDownList>
        </td>
 <td id="tdRndTBond" runat="server" visible="false">
  <asp:Label ID="Label1" CssClass="FieldName" runat="server" Text="Select R&T:"></asp:Label>
            <asp:DropDownList ID="ddlRndTBond" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="BSE" Value="BE" />
                <asp:ListItem Text="NSC" Value="NC" />
            </asp:DropDownList>
       </td>
        <td align="left">
            <asp:Button ID="btngo" runat="server" Text="Go" CssClass="PCGButton" />
        </td>
    </tr>
</table>
--%>
<table>
    <tr>
        <td>
            <asp:Label ID="lblType" CssClass="FieldName" runat="server" Text="Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField">
                <asp:ListItem Selected="True" Value="0">--SELECT--</asp:ListItem>
                <asp:ListItem Text="NCD" Value="FI" />
                <asp:ListItem Text="IPO" Value="IP" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvType" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a Type"
                ControlToValidate="ddlType" Display="Dynamic" InitialValue="0" ValidationGroup="Issueallotment">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Label ID="lblissuer" runat="server" Text="Issuer:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblNfostartdate" runat="server" Text="From Date" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <%--<span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="appRecidRequiredFieldValidator" ControlToValidate="txtNFOStartDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>--%>
        </td>
        <td align="right">
            <asp:Label ID="lblNfoEnddate" runat="server" Text="To Date" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                MinDate="1900-01-01" TabIndex="5">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNFOStartDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO End Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>--%>
        </td>
        <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtToDate"
            ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
            ControlToCompare="txtFromDate" CssClass="cvPCG" ValidationGroup="btnsubmit" Display="Dynamic">
        </asp:CompareValidator>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" OnClick="Go_OnClick"
                ValidationGroup="Issueallotment" />
        </td>
    </tr>
</table>
<asp:Panel ID="AdviserIssueList" runat="server" ScrollBars="Horizontal" Width="100%"
    Visible="false">
    <table width="100%" cellspacing="0" cellpadding="1">
        <tr>
            <td>
                <telerik:RadGrid ID="gvAdviserIssueList" runat="server" fAllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="AIA_Id" Width="99%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" PageSize="20">
                        <Columns>
                            <telerik:GridBoundColumn DataField="AIA_AllotmentDate" UniqueName="AIA_AllotmentDate"
                                HeaderText="AllotmentDate" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="90px" SortExpression="AIA_AllotmentDate"
                                FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_OrderId" UniqueName="CO_OrderId" HeaderText="OrderId"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="90px"
                                SortExpression="CO_OrderId" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_IssueName" UniqueName="AIM_IssueName" HeaderText="Issue Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="AIM_IssueName" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_Quantity" UniqueName="AIA_Quantity" HeaderText="Quantity"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                SortExpression="AIA_Quantity" FilterControlWidth="50px" CurrentFilterFunction="Contains"
                                Visible="true">
                                <ItemStyle Width="67px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_Price" UniqueName="AIA_Price" HeaderText="Price"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="80px"
                                SortExpression="AIA_Price" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="70px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="CO_ApplicationNo" UniqueName="CO_ApplicationNo"
                                HeaderText="ApplicationNo" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                AllowFiltering="true" HeaderStyle-Width="100px" SortExpression="CO_ApplicationNo"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CEDA_DPId" UniqueName="CEDA_DPId" HeaderText="DPId"
                                SortExpression="CEDA_DPId" AutoPostBackOnFilter="true" HeaderStyle-Width="100px"
                                FilterControlWidth="80px" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                <ItemStyle Width="80px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_CustomerId" UniqueName="C_CustomerId" HeaderText="CustomerId"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="C_CustomerId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="C_CustomerId" UniqueName="C_CustomerId" HeaderText="Customer Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="100px"
                                SortExpression="C_CustomerId" FilterControlWidth="80px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
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
