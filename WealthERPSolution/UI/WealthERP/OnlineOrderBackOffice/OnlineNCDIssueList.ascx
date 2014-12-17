<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineNCDIssueList.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineNCDIssueList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                           Issue List
                        </td>
                        <td>
                        <asp:ImageButton ID="btnNcdIpoExport" runat="server" ImageUrl="~/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnNcdIpoExport_Click"></asp:ImageButton>
                        
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="60%" runat="server" id="tbNcdIssueList">
    <tr runat="server" visible="false">
        <td class="leftLabel" runat="server" visible="false">
            <asp:Label ID="lb1date" runat="server" Text="Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData" runat="server" visible="false">
            <telerik:RadDatePicker ID="txtDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                TabIndex="17" Width="190px">
                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                    Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span18" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter Date" Display="Dynamic" ControlToValidate="txtDate"
                InitialValue="" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <%-- <td class="leftLabel">
                <asp:Label ID="lb1Type" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
            </td>
            <td class="rightData">
                <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true"
                    Width="205px">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="Curent">Curent Issues</asp:ListItem>
                    <asp:ListItem Value="Closed">Closed Issues</asp:ListItem>
                    <asp:ListItem Value="Future">Future Issues</asp:ListItem>
                </asp:DropDownList>
                <span id="Span4" class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Type"
                    CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                    InitialValue="Select"></asp:RequiredFieldValidator>
            </td>--%>
        <%-- <td class="leftLabel">
                <asp:Label ID="Label1" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
            </td>--%>
        <%-- <td class="rightData">
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="cmbField" AutoPostBack="true"
                    Width="205px">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="Curent">Curent Issues</asp:ListItem>
                    <asp:ListItem Value="Closed">Closed Issues</asp:ListItem>
                    <asp:ListItem Value="Future">Future Issues</asp:ListItem>
                </asp:DropDownList>
                <span id="Span1" class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Type"
                    CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                    InitialValue="Select"></asp:RequiredFieldValidator>
            </td>
               <td class="leftLabel">
                <asp:Label ID="Label2" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
            </td>--%>
        <%--   <td class="rightData">
                <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                    Width="205px">
                    <asp:ListItem Value="Select">Select</asp:ListItem>
                    <asp:ListItem Value="NCD">NCD</asp:ListItem>
                    <asp:ListItem Value="IPO">IPO</asp:ListItem>
                </asp:DropDownList>
                <span id="Span2" class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Type"
                    CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnGo" Display="Dynamic"
                    InitialValue="Select"></asp:RequiredFieldValidator>
            </td>--%>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="Label2" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="105px" OnSelectedIndexChanged="ddlProduct_OnSelectedIndexChanged">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Bonds">Bonds</asp:ListItem>
                <asp:ListItem Value="IP">IPO</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Type"
                CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right" id="tdcategory" runat="server" visible="false">
        <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category"></asp:Label>
        </td>
        <td id="tdCategorydropdown" runat="server" Visible="false">
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField"  >
        </asp:DropDownList>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lb1Type" runat="server" Text="Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="205px">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="Curent">Current Issues</asp:ListItem>
                <asp:ListItem Value="Closed">Closed Issues</asp:ListItem>
                <asp:ListItem Value="Future">Future Issues</asp:ListItem>
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Type"
                CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server">
        <td class="leftLabel">
            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
        <td class="rightData">
            &nbsp;
        </td>
        <td class="leftLabel">
            &nbsp;
        </td>
        <td class="rightData">
            &nbsp;
        </td>
    </tr>
</table>
<asp:Panel ID="pnlIssueList" runat="server" class="Landscape" Width="80%" Height="80%"
    ScrollBars="Both" Visible="false">
    <table width="100%">
        <tr>
            <td>
                <div id="dvIssueList" runat="server" style="width: auto;">
                    <telerik:RadGrid ID="gvIssueList" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" ExportSettings-FileName="Issue List" OnNeedDataSource="gvIssueList_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="MF Order Recon" Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="AIM_IssueId" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Issue No" AllowFiltering="false" DataField="AIM_IssueId">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkIssueNo" runat="server" CssClass="cmbFielde" Text='<%# Eval("AIM_IssueId") %>'
                                            OnClick="lnkIssueNo_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_OpenDate" HeaderText="Open Date" SortExpression="AIM_OpenDate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    DataFormatString="{0:d}" UniqueName="AIM_OpenDate" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIM_CloseDate" HeaderText="Close Date" SortExpression="AIM_CloseDate"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    DataFormatString="{0:d}" UniqueName="AIM_CloseDate" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderText="Issue Detail Name"
                                    SortExpression="AID_IssueDetailName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" UniqueName="AID_IssueDetailName"
                                    FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryName" HeaderText="Catgeory Name"
                                    SortExpression="AIIC_InvestorCatgeoryName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="AIIC_InvestorCatgeoryName" FooterStyle-HorizontalAlign="Left"
                                    Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
