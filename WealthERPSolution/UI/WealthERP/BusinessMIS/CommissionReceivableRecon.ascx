<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionReceivableRecon.ascx.cs"
    Inherits="WealthERP.BusinessMIS.CommissionReceivableRecon" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvCommissionReceiveRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Commission Receivable View
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReconComplete" runat="server" class="success-msg" align="center" visible="false">
                Recon Status Marked closed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReconClosed" runat="server" class="failure-msg" align="center" visible="false">
                Recon staus Closed,So can't save changes
            </div>
        </td>
    </tr>
</table>
<table width="80%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr id="trSelectMutualFund" runat="server">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select AMC Code"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSelectType" runat="server">
        <td align="left" class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select Commission Type:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an COmmission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblRecon" runat="server" CssClass="FieldName" Text="Recon Status:"></asp:Label>
        </td>
        <td align="right">
            <asp:DropDownList ID="ddlReconStatus" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Pending" Value="Pending"></asp:ListItem>
                <asp:ListItem Text="Closed" Value="Closed"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td id="tdFromDate" runat="server" align="left">
            <asp:Label ID="Label10" Text="From Date:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right">
            <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFrom"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <%--        </td>
--        <td id="tdToDate" runat="server">--%>
        <td align="right">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="To Date:"></asp:Label>
        </td>
        <td align="left">
            <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtTo"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a to Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <%--        </td>
--%>
        <td align="left" style="padding-right: 50px">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <td>
        </td>
    </tr>
</table>
<div style="width: 100%; overflow: scroll;">
    <telerik:RadGrid ID="gvCommissionReceiveRecon" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="150%"
        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
        EnableHeaderContextMenu="true" OnItemDataBound="gvWERPTrans_ItemDataBound" EnableHeaderContextFilterMenu="true"
        OnNeedDataSource="gvCommissionReceiveRecon_OnNeedDataSource">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" DataKeyNames="CMFT_MFTransId,totalNAV,perDayTrail,
PerDayAssets">
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action" Visible="false">
                    <HeaderTemplate>
                        <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkId" runat="server" />
                        <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFT_MFTransId").ToString()%>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="btnSave" CssClass="FieldName" OnClick="btnSave_Click" runat="server"
                            Text="Update Expected Amt" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="SchemePlan" DataField="schemeplanname"
                    UniqueName="schemeplanname" SortExpression="schemeplanname" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Transaction Date"
                    DataField="transactiondate" UniqueName="transactiondate" SortExpression="transactiondate"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Amount" DataField="amount"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="amount" SortExpression="amount"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Transaction Type"
                    DataField="transactiontype" HeaderStyle-HorizontalAlign="Right" UniqueName="transactiontype"
                    SortExpression="transactiontype" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Age" DataField="Age"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Age" SortExpression="Age" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Current Value" DataField="currentvalue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="currentvalue" SortExpression="currentvalue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false" >
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Commission Amount Received"
                    DataField="receivedamount" HeaderStyle-HorizontalAlign="Right" UniqueName="receivedamount"
                    SortExpression="receivedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Cumulative Nav" DataField="totalNAV"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="totalNAV" SortExpression="totalNAV"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="PerDayAssets" DataField="PerDayAssets"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="PerDayAssets" SortExpression="PerDayAssets"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="PerDayTrail" DataField="perDayTrail"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="perDayTrail" SortExpression="perDayTrail"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Commission Amount Expected"
                    DataField="expectedamount" HeaderStyle-HorizontalAlign="Right" UniqueName="expectedamount"
                    SortExpression="expectedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="160px" HeaderText="Commission Adjustment Amount"
                    DataField="CMFT_ReceivedCommissionAdjustment" SortExpression="CMFT_ReceivedCommissionAdjustment"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" Visible="false">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAdjustAmount" CssClass="txtField" runat="server" Text='<%# Eval("adjust").ToString()%>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAdjustAmountMultiple" CssClass="txtField" runat="server" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Amount Difference"
                    DataField="diff" HeaderStyle-HorizontalAlign="Right" UniqueName="diff" SortExpression="diff"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Commission Updated Expected Amount"
                    DataField="UpdatedExpectedAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="UpdatedExpectedAmount"
                    SortExpression="UpdatedExpectedAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Recon Status" DataField="ReconStatus"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ReconStatus" SortExpression="ReconStatus"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="CalculatedDate" DataField="calculatedDate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="calculatedDate" SortExpression="calculatedDate"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Structure Name" DataField="structName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="structName" SortExpression="structName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
               
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="hdnschemeId" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
    <asp:HiddenField ID="hdnFromDate" runat="server" />
    <asp:HiddenField ID="hdnToDate" runat="server" />
    <asp:HiddenField ID="hdnrecon" runat="server" />
    <asp:HiddenField ID="hdnSBbrokercode" runat="server" />
</div>
<div runat="server" id="divBtnActionSection" visible="false">
    <asp:Button ID="btnUpload" OnClick="btnUpload_click" runat="server" Text="Mark Recon Status"
        CssClass="PCGLongButton" />
    <asp:Button ID="btngo" runat="server" CssClass="PCGLongButton" Text="Submit" OnClick="btnSubmit_click" />
</div>
