<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PriceList.ascx.cs" Inherits="WealthERP.Admin.PriceList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%--<script language="javascript" type="text/javascript">
    function checkLastDate(sender, args) {

        var fromDateString = document.getElementById('ctrl_PriceList_txtFromDate').value;
        var fromDate = changeDate(fromDateString);
        var toDateString = document.getElementById('ctrl_PriceList_txtToDate').value;
        var toDate = changeDate(toDateString);
        var todayDate = new Date();

        if (Date.parse(toDate) < Date.parse(fromDate)) {
            //sender._selectedDate = todayDate;
            //sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            sender._textbox.set_Value('dd/mm/yyyy');
            alert("Warning! - ToDate cannot be less than the FromDate");
        }
    }
</script>--%>

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
        <table width="70%">
            <tr>
                <td class="leftField">
                    <label id="Label1" class="FieldName" title=" Asset Group">
                        Asset Group:</label>
                </td>
                <td class="rightField">
                    <asp:DropDownList CssClass="cmbField" ID="ddlAssetGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetGroup_OnSelectedIndexChanged">
                        <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
                        <asp:ListItem Text="MF" Value="MF"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvddlAssetGroup" runat="server" ControlToValidate="ddlAssetGroup"
                ErrorMessage="Please Select  Asset" Operator="NotEqual" ValueToCompare="0"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                </td>
                <td align="right" colspan="2">
                    <asp:Label ID="lblIllegal" Text="" runat="server" CssClass="Error" />
                </td>
            </tr>
          
            
       
            <tr><td></td>
                <td align="left">
                    <asp:RadioButton ID="rbtnHistorical" Text="Historical Data" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" CssClass="cmbField" OnCheckedChanged="rbtnHistorical_CheckedChanged" />
                </td>
                <td align="center">
                    <asp:RadioButton ID="rbtnCurrent" Text="Current Snapshot" runat="server" AutoPostBack="true"
                        GroupName="Snapshot" OnCheckedChanged="rbtnCurrent_CheckedChanged" CssClass="cmbField" />
                </td>
               
               
            </tr>
              <tr id="trFromDate" runat="server">
                <td align="right">
                    <label id="lblFromDate" class="FieldName" runat="server" title="FromDate">
                        FromDate:</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><cc1:CalendarExtender
                        ID="FrmDate" TargetControlID="txtFromDate" runat="server" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                     <asp:CompareValidator id="cvChkFutureDate" runat="server"
                   ControlToValidate="txtFromDate"
                   Operator="LessThanEqual" CssClass="cvPCG"
                   ErrorMessage="Date Can't be in future" Type="Date"
                   Display="dynamic" ValidationGroup="vgbtnSubmit">
                  </asp:CompareValidator>
                    <%--<asp:RequiredFieldValidator ID="frmdatevalid" runat="server" ControlToValidate="txtFromDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr id="trToDate" runat="server">
                <td align="right">
                    <label id="lblToDate" class="FieldName" runat="server" title="ToDate">
                        ToDate:</label>
                </td>
                <td align="left">
                    <asp:TextBox ID="txtToDate" CssClass="txtField" runat="server"></asp:TextBox><cc1:CalendarExtender
                        ID="TDate" TargetControlID="txtToDate" runat="server" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator id="compDateValidator" ValidationGroup="vgbtnSubmit"
                        ControlToValidate="txtToDate" Operator="LessThanEqual" Type="Date" CssClass="cvPCG"
                        runat="server" ErrorMessage="Date Can't be in future" ></asp:CompareValidator>
                        <br />

                
                  <asp:CompareValidator id="cvCompareDate" runat="server"
                   ControlToValidate="txtToDate" ControlToCompare="txtFromDate"
                   Operator="GreaterThanEqual"
                   Type="Date" CssClass="cvPCG" ValidationGroup="vgbtnSubmit"
                   ErrorMessage="ToDate should be greater than FromDate"
                   Display="dynamic">
                 </asp:CompareValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtToDate"
                        ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            
            <tr runat="server" id="trSelectMutualFund">
                <td align="right">
                    <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Select AMC Code:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelectMutualFund" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectMutualFund_OnSelectedIndexChanged" >
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlSelectMutualFund"
                ErrorMessage="Please Select AMC Code" Operator="NotEqual" ValueToCompare="Select AMC Code"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                </td>
            </tr>
             <tr runat="server" id="trSelectSchemeNAV">
                <td align="right">
                    <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Select Scheme Name:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSelectSchemeNAV" runat="server" CssClass="cmbField" >
                    </asp:DropDownList>
                     <%--<asp:CompareValidator ID="cvddlSelectSchemeNAV" runat="server" ControlToValidate="ddlSelectSchemeNAV"
                ErrorMessage="Please Select Scheme Name" Operator="NotEqual" ValueToCompare="Select Scheme Name"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr id="trbtnSubmit" runat="server">
             <td>
             <asp:Button ID="btnSubmit" Text="Submit" CssClass="PCGButton" ValidationGroup="vgbtnSubmit" runat="server" OnClick="OnClick_Submit" />
            </td>
            </tr>
        </table>
    </div>
    <div id="DivMF" runat="server" style="display: none">
        <table style="width: 100%">
            <tr id="trMfPagecount" runat="server">
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
            <tr runat="server" id="trgrMfView">
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
                            <asp:BoundField DataField="SchemePlanCode" HeaderText="Scheme Plan Code" />
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblName" align="center" runat="server" Text="Scheme Plan Name"></asp:Label>
                                    <br />
                                   <%-- <asp:TextBox ID="txtSchemeSearch" runat="server" CssClass="txtField" onkeydown="return JSdoPostback(event,'ctrl_AdminPriceList_btnSearch');" />--%>
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
                <td class="leftField" runat="server" id="trPageCount">
                    <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
                    <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr  runat="server" id="trgvEquityView">
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
            <tr id="trPager" runat="server">
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
