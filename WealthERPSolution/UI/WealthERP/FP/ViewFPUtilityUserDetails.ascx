<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFPUtilityUserDetails.ascx.cs"
    Inherits="WealthERP.FP.ViewFPUtilityUserDetails" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>
<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            View Leads
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtngvLeadList" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtngvLeadList_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
       <td align="right">
            <asp:Label ID="Label1" runat="server" Text="From Date" CssClass="FieldName"></asp:Label>
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
            <asp:CompareValidator ID="txtFromDatqe" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtFromDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <span id="Span10" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                ErrorMessage="<br />Please select From Date" CssClass="cvPCG" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="Submit">
            </asp:RequiredFieldValidator>
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
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtToDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                ErrorMessage="<br />Please select To Date" CssClass="cvPCG" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="Submit">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="Button1" Text='Submit' ValidationGroup='Submit' CausesValidation="true"
                CssClass="PCGButton" runat="server" OnClick="Submit_OnClick"></asp:Button>
        </td>
    </tr>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <asp:Panel ID="paqwnel1" runat="server" Width="95%">
                <telerik:RadGrid ID="gvLeadList" runat="server" AllowAutomaticDeletes="false" PageSize="20"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    OnNeedDataSource="gvLeadList_OnNeedDataSource" GridLines="none" AllowAutomaticInserts="false"
                    OnItemCommand="gvLeadList_ItemCommand" Skin="Telerik" EnableHeaderContextMenu="true"
                    Width="95%" Visible="false">
                    <MasterTableView Width="100%" DataKeyNames="FPUUD_UserId,C_CustomerId,FPUUD_Name"
                        AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                        GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client"
                        ShowGroupFooter="true">
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="Customer" DataField="FPUUD_Name" UniqueName="FPUUD_Name"
                                SortExpression="FPUUD_Name" AutoPostBackOnFilter="true" AllowFiltering="false"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="PAN" DataField="FPUUD_PAN" UniqueName="FPUUD_PAN"
                                SortExpression="FPUUD_PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FPUUD_EMail" SortExpression="FPUUD_EMail" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Email" UniqueName="FPUUD_EMail">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FPUUD_MobileNo" SortExpression="FPUUD_MobileNo"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderText="Mobile" UniqueName="FPUUD_MobileNo">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FPUUD_DOB" SortExpression="FPUUD_DOB" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Date of birth" UniqueName="FPUUD_DOB">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FPUUD_CreatedOn" SortExpression="FPUUD_CreatedOn"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderText="Lead Date" UniqueName="FPUUD_CreatedOn">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FPUUD_ModifiedOn" SortExpression="FPUUD_ModifiedOn"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderText="Last Login" UniqueName="FPUUD_ModifiedOn">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="stage" SortExpression="stage" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderText="Stage" UniqueName="stage">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="IsCustomerAvailable" SortExpression="IsCustomerAvailable"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="true" HeaderText="Customer Status" UniqueName="IsCustomerAvailable">
                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="RiskClass" HeaderText="Risk Class"
                                Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewProspect1" CommandName="OpenQnA" runat="server" Text='<%# Eval("RiskClass") %>'
                                        Visible="true"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="ActionForProspect"
                                HeaderText="View Profile" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkViewProspect" OnClick="ddlActionForProspect_OnSelectedIndexChanged"
                                        runat="server" Text="View Profile" Visible='<%# Eval("FPUUD_IsProspectmarked") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                    </ClientSettings>
                </telerik:RadGrid>
                <telerik:RadWindowManager RenderMode="Lightweight" ID="RadWindowManager1" runat="server"
                    EnableShadow="true">
                    <Windows>
                        <telerik:RadWindow RenderMode="Lightweight" ID="UserListDialog1" runat="server" Title="Editing record"
                            Height="600px" Width="950px" OnClientShow="setCustomPosition" Left="30" Top="25"
                            ReloadOnShow="true" ShowContentDuringLoad="false" Modal="true">
                            <ContentTemplate>
                                <asp:PlaceHolder ID="placeholder" runat="server"></asp:PlaceHolder>
                            </ContentTemplate>
                        </telerik:RadWindow>
                    </Windows>
                </telerik:RadWindowManager>

                <script type="text/javascript">
                    function setCustomPosition(sender, args) {
                        sender.moveTo(sender.get_left(), sender.get_top());
                    }
                </script>

            </asp:Panel>
        </td>
        <td>
        </td>
    </tr>
</table>
