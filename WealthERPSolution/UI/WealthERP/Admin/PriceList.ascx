<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PriceList.ascx.cs" Inherits="WealthERP.Admin.PriceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<div>
    <div>
        <asp:ScriptManager ID="UploadScripManager" runat="server">
        </asp:ScriptManager>
        <table style="width: 100%">
            <tr>
                <td class="HeaderCell">
                    <label id="lblheader" class="HeaderTextBig" title="Upload Screen">
                        Price Screen</label>
                </td>
            </tr>
        </table>
    </div>
    <div id="MainDiv" runat="server">
        <table width="50%">
            <tr>
                <td class="leftField">
                    <label id="Label1" class="FieldName" title=" Asset Group">
                        Asset Group:</label>
                </td>
                <td class="rightField">
                    <asp:DropDownList CssClass="cmbField" ID="ddlAssetGroup" runat="server">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
                        <asp:ListItem Text="MF" Value="MF"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td align="right" colspan="2">
                    <asp:Label ID="lblIllegal" Text="" runat="server" CssClass="Error" />
                </td>
            </tr>
            <tr id="trFromDate" runat="server">
                <td class="leftField">
                    <label id="lblFromDate" class="FieldName" runat="server" title="FromDate">
                        FromDate:</label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><cc1:CalendarExtender
                        ID="FrmDate" TargetControlID="txtFromDate" runat="server" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <%--<asp:RequiredFieldValidator ID="frmdatevalid" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr id="trToDate" runat="server">
                <td class="leftField">
                    <label id="lblToDate" class="FieldName" runat="server" title="ToDate">
                        ToDate:</label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtToDate" CssClass="txtField" runat="server"></asp:TextBox><cc1:CalendarExtender
                        ID="TDate" TargetControlID="txtToDate" runat="server" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:RadioButton ID="rbtnHistorical" Text="Historical Data" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" CssClass="cmbField" OnCheckedChanged="rbtnHistorical_CheckedChanged" />
                </td>
                <td>
                    <asp:RadioButton ID="rbtnCurrent" Text="Current Snapshot" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" OnCheckedChanged="rbtnCurrent_CheckedChanged" CssClass="cmbField" />
                </td>
                <td align="center">
                    &nbsp;
                </td>
                <td class="SubmitCell">
                    <asp:Button ID="btnSubmit" Text="Submit" CssClass="PCGButton" runat="server" OnClick="OnClick_Submit" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivMF" runat="server" style="display: none">
        <table style="width: 100%">
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblMFCurrentPage" class="Field" runat="server"></asp:Label>
                    <asp:Label ID="lblMFTotalRows" class="Field" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvMFRecord" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                        ShowFooter="true" Font-Size="Small">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <%--<asp:BoundField DataField="SchemePlanName" HeaderText="Scheme Plan Name" SortExpression="PASP_SchemePlanName" />--%>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" align="center" runat="server" Text="Scheme Plan Name"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblSchemeName" runat="server" Text='<%# Eval("SchemePlanName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NetAssetValue" HeaderText="Net AssetValue" />
                            <asp:BoundField DataField="RepurchasePrice" HeaderText="Repurchase Price" />
                            <asp:BoundField DataField="SalePrice" HeaderText="Sale Price" />
                            <asp:BoundField DataField="PostDate" HeaderText="Post Date" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivEquity" runat="server" style="display: none">
        <table style="width: 100%">
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                    <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvEquityRecord" runat="server" CssClass="GridViewStyle" ShowFooter="true"
                        AutoGenerateColumns="False" Font-Size="Small">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblEquityName" align="center" runat="server" Text="Company Name"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtCompanySearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName").ToString() %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="PEM_CompanyName" />--%>
                            <asp:BoundField DataField="Exchange" HeaderText="Exchange" />
                            <asp:BoundField DataField="Series" HeaderText="Series" />
                            <asp:BoundField DataField="OpenPrice" HeaderText="Open Price" />
                            <asp:BoundField DataField="HighPrice" HeaderText="High Price" />
                            <asp:BoundField DataField="LowPrice" HeaderText="Low Price" />
                            <asp:BoundField DataField="ClosePrice" HeaderText="Close Price" />
                            <asp:BoundField DataField="LastPrice" HeaderText="Last Price" />
                            <asp:BoundField DataField="PreviousClose" HeaderText="Previous Close" />
                            <asp:BoundField DataField="TotalTradeQuantity" HeaderText="Total Trade Quantity" />
                            <asp:BoundField DataField="TotalTradeValue" HeaderText="Total Trade Value" />
                            <asp:BoundField DataField="NoOfTrades" HeaderText="No Of Trades" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div id="DivPager" runat="server" style="display: none">
        <table style="width: 100%">
            <tr>
                <td>
                    <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                </td>
            </tr>
        </table>
    </div>
    <table width="100%">
        <tr>
        </tr>
    </table>
    <asp:HiddenField ID="hdnFromDate" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnMFCount" runat="server" />
    <asp:HiddenField ID="hdnAssetGroup" runat="server" />
    <asp:HiddenField ID="hdnEquityCount" runat="server" />
    <asp:HiddenField ID="hdnSchemeSearch" runat="server" />
    <asp:HiddenField ID="hdnCurrentPage" runat="server" />
    <asp:HiddenField ID="hdnCompanySearch" runat="server" />
    <asp:HiddenField ID="hdnSort" runat="server" />
    <asp:Button ID="btnSearch" runat="server" Text="" OnClick="btnSearch_Click" BorderStyle="None"
        BackColor="Transparent" />
</div>
