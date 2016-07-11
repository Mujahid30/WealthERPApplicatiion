<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerOfflineBondOrder.ascx.cs"
    Inherits="WealthERP.OffLineOrderManagement.CustomerOfflineBondOrder" %>
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
                            Bond Order
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td align="right">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCategory_Selectedindexchanged"
                AutoPostBack="true" Width="210px">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlCategory"
                ErrorMessage="<br /> select  Category" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="0" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblIssue" runat="server" Text="Issue:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssue" Width="270px" runat="server" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssue_OnSelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlIssue"
                ErrorMessage="<br />select Issue" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="0" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlIssue"
                ErrorMessage="<br />select Issue" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue=" " ValidationGroup="btnaddSeries">
            </asp:RequiredFieldValidator>
            <asp:LinkButton ID="lnkbuttonAddSeries" runat="server" OnClick="OnClick_lnkbuttonAddSeries"
                Text="Add Issuer" CssClass="LinkButtons" ValidationGroup="btnaddSeries"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="right" id="tdIssuerCategory" runat="server">
            <asp:Label ID="lblIssueCategory" runat="server" Text="Issue Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td id="tdIssueCategory" runat="server">
            <asp:DropDownList ID="ddlIssueCategory" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlIssueCategory"
                ErrorMessage="<br />select Category" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="0" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="ddlIssueCategory"
                ErrorMessage="<br />select issue Category" CssClass="cvPCG" Display="Dynamic"
                runat="server" InitialValue=" " ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblSeries" runat="server" CssClass="FieldName" Text="Series:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSeries" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="ddlSeries"
                ErrorMessage="<br />select Series" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="0" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="ddlSeries"
                ErrorMessage="<br />select Series" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue=" " ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblQuentity" runat="server" Text="Quentity:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtQuentity" runat="server"></asp:TextBox>
            <span id="Span7" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtQuentity"
                ErrorMessage="<br />add Quentity" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblPrice" runat="server" CssClass="FieldName" Text="Price:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="textPrice" runat="server"></asp:TextBox>
            <span id="Span8" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="textPrice"
                ErrorMessage="<br />add Price" CssClass="cvPCG" Display="Dynamic" runat="server"
                InitialValue="" ValidationGroup="btnViewOrder">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trdate" runat="server">
        <td align="right">
            <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName" Text="Order Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="dvTransactionDate" runat="server" class="dvInLine">
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvtxtTransactionDate" ControlToValidate="txtOrderFrom"
                    ErrorMessage="<br />Please select a From Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderFrom" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
        </td>
        <td align="right">
            <asp:Label ID="lblTo" runat="server" CssClass="FieldName" Text="Allotment Date:"></asp:Label>
        </td>
        <td>
            <telerik:RadDatePicker ID="txtOrderTo" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <div id="Div1" runat="server" class="dvInLine">
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtOrderTo"
                    ErrorMessage="<br />Please select a To Date" CssClass="cvPCG" Display="Dynamic"
                    runat="server" InitialValue="" ValidationGroup="btnViewOrder">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                    Type="Date" ControlToValidate="txtOrderTo" CssClass="cvPCG" Operator="DataTypeCheck"
                    Display="Dynamic"></asp:CompareValidator>
            </div>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtOrderTo"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtOrderFrom" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                Display="Dynamic">
            </asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button runat="server" CssClass="PCGButton" ID="btnGo" Text="Go" OnClick="btnGo_OnClick"
                ValidationGroup="btnViewOrder" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Panel ID="pnlGrid" runat="server" CssClass="Landscape" Width="99%" ScrollBars="Horizontal"
                Visible="false">
                <table>
                    <tr>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <telerik:RadGrid ID="gvBondOrderList" runat="server" GridLines="None" AutoGenerateColumns="False"
                                            PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvBondOrderList_OnNeedDataSource"
                                            ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
                                            AllowAutomaticInserts="false" Width="120%" Height="400px">
                                            <MasterTableView DataKeyNames="SeriesId,Quentity,Price,issuecategory" Width="100%"
                                                AllowMultiColumnSorting="True" AutoGenerateColumns="false" AllowFilteringByColumn="true"
                                                EditMode="PopUp">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="SeriesId" SortExpression="SeriesId" AutoPostBackOnFilter="true"
                                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                        HeaderStyle-Width="160px" HeaderText="Series Id" UniqueName="SeriesId">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Quentity" AllowFiltering="true" HeaderText="Quentity"
                                                        UniqueName="Quentity" SortExpression="Quentity" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                                        AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="75px"
                                                        DataType="System.Int64">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Price" AllowFiltering="true" HeaderText="Price"
                                                        UniqueName="Price" SortExpression="Price" ShowFilterIcon="false" CurrentFilterFunction="EqualTo"
                                                        AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="75px"
                                                        DataType="System.Int64">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="issuecategory" SortExpression="issuecategory"
                                                        AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                        AllowFiltering="true" HeaderStyle-Width="160px" HeaderText="Series Id" UniqueName="issuecategory"
                                                        Visible="false">
                                                        <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnCreateAllotment" runat="server" Text="Create Allotment" CssClass="PCGButton"
                OnClick="OnClick_btnCreateAllotment" Visible="false" />
        </td>
    </tr>
</table>
