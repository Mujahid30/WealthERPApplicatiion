﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomerSMSAlerts.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserCustomerSMSAlerts" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%--Used to create Popups and Alert Box--%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<script src="/Scripts/jquery.js" type="text/javascript"></script>

<link href="/Scripts/colorbox.css" rel="stylesheet" type="text/css" />

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<script type="text/javascript">
    var TargetBaseControl = null;

    window.onload = function() {
        try {
            //get target base control.
        }
        catch (err) {

            TargetBaseControl = null;
        }
    }

    function TestCheckBox() {
        if (TargetBaseControl == null) {
            return false;
        }

        //get target child control.
        var TargetChildControl = "chkCustomerSMSAlert";
        var TargetTextChildControl = "txtMobileNo";
        var totalChecked = 0;
        var countWithMobileNumber = 0;
        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");


        for (var n = 0; n < Inputs.length; ++n) {
            if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[n].checked) {
                if (Inputs[n + 1].type == 'text' && Inputs[n + 1].id.indexOf('txtMobileNo') != -1) {
                    if (Inputs[n + 1].value != 0) {
                        countWithMobileNumber++;
                    }
                }
                totalChecked++;
            }
        }
        if (totalChecked <= 0) {
            alert('Select at least one checkbox!');
            return false;
        }
        else {
            if (countWithMobileNumber != totalChecked) {
                var confirmation = confirm('Do you wish to add Customer Mobile number');
                if (confirmation == true) {
                    ShowColumn(7);
                    return false;
                }
                else {
                    return false;
                }
            }
        }
    }

    function HideColumn(columnNo) {
         
        try {
            for (var i = 0; i < dgTest.rows.length; i++) {
                //alert(dgTest.rows[i].cells[columnNo]);
                dgTest.rows[i].cells[columnNo].style.display = "none";
            }
        }
        catch (e)
        { }
    }

    function ShowColumn(columnNo) {
        
        try {
            for (var i = 0; i < dgTest.rows.length; i++) {
                dgTest.rows[i].cells[columnNo].style.display = "block";
            }
        }
        catch (e)
        { }
    }
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkCustomerSMSAlert";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBoxAll");

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

<%--<script type="text/javascript">
    $(document).ready(function() {
        $(".openQuickMobileAdd").colorbox({ width: "700px", inline: true, href: "/Alerts/QuickMobileNumberAdd.aspx" });
    });
</script>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Alert Notification
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
            <div class="success-msg" id="SuccessMessage" runat="server" visible="false" align="center">
                SMS Sent Successfully
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblCustomerSMSAlerts" Text="Customer SMS Alerts" CssClass="HeaderTextSmall"
                runat="server" Visible="false"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblLicenceName" Text="SMS Licence Left:" runat="server" CssClass="FieldName" visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLincenceValue" Text="" runat="server" CssClass="Field" visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr align="center" id="trPageCount" runat="server" visible="false">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center">
            <div id="divNoRecords" runat="server" class="failure-msg" visible="false">
                <asp:Label ID="lblNoRecords" Text="No Records found" runat="server" Visible="false"></asp:Label>
            </div>
        </td>
    </tr>
    <tr>
            <td>
                <telerik:RadGrid ID="gvAlertNotification" runat="server" AutoGenerateColumns="false"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
                    OnNeedDataSource="gvAlertNotification_OnNeedDataSource">
                    <MasterTableView DataKeyNames="" Width="100%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false" CommandItemDisplay="None">
                        <Columns>
                            
                            <telerik:GridBoundColumn DataField="CustomerName" SortExpression="CustomerName" UniqueName="CustomerName"
                                AllowFiltering="true" HeaderText="Customer Name" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AES_EventMessage" SortExpression="AES_EventMessage" UniqueName="AES_EventMessage"
                                AllowFiltering="true" HeaderText="Alert Message" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                                UniqueName="PASP_SchemePlanName" AllowFiltering="true" HeaderText="Schemeplan name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn DataField="AEL_EventDescription" SortExpression="AEL_EventDescription"
                                UniqueName="AEL_EventDescription" AllowFiltering="true" HeaderText="Event Description"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridBoundColumn DataField="TimesSMSSent" SortExpression="TimesSMSSent"
                                UniqueName="TimesSMSSent" AllowFiltering="true" HeaderText="No of Times SMS Sent" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                           <%-- <telerik:GridTemplateColumn DataField="LastSMSDate" SortExpression="LastSMSDate"
                                UniqueName="LastSMSDate" AllowFiltering="true" HeaderText="Last SMS Date"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridTemplateColumn>--%>
                               <telerik:GridBoundColumn DataField="CMFSS_NextSIPDueDate" SortExpression="CMFSS_NextSIPDueDate"
                                UniqueName="CMFSS_NextSIPDueDate" AllowFiltering="true" HeaderText="Next SIP DueDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AAECR_RuleName" SortExpression="AAECR_RuleName"
                                UniqueName="AAECR_RuleName" AllowFiltering="true" HeaderText="Rule"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" >
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                         <%--   <telerik:GridBoundColumn DataField="Mobile" SortExpression="Mobile"
                                UniqueName="Mobile" AllowFiltering="true" HeaderText="Mobile No"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                FilterControlWidth="180px" DataType="System.Double">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </td>
        
            <%--<asp:Panel ID="pnlCustomerSMSAlerts" runat="server" Height="500px" Width="100%" ScrollBars="Both" HorizontalAlign="Left">--%>
               <%-- <asp:GridView ID="gvCustomerSMSAlerts" DataKeyNames="CustomerId,AlertId" runat="server"
                    AutoGenerateColumns="False" Width="100%" CellPadding="4"
                    CssClass="GridViewStyle" ShowFooter="True" OnRowDataBound="gvCustomerSMSAlerts_RowDataBound">
                     <FooterStyle CssClass="FooterStyle" />
                    <PagerSettings Visible="False" />
                    <RowStyle CssClass="RowStyle" />
                    <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" 
                        VerticalAlign="Top" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCustomerSMSAlert" runat="server" CssClass="Field" />
                            </ItemTemplate>
                            <HeaderTemplate>
                            <input id="chkBoxAll"  name="vehicle" value="Bike" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                    runat="server" Text="Delete" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label ID="lblCustNameHeader" runat="server" Text="Customer"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtCustNameSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_AdviserCustomerSMSAlerts_btnNameSearch');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("CustomerName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Name" HeaderText="Details" ReadOnly="true" />
                        <asp:BoundField DataField="AlertMessage" HeaderText="Alert Message" ReadOnly="true" />
                        <asp:BoundField DataField="TimesSMSSent" HeaderText="No of Times SMS Sent" ReadOnly="true" />
                        <asp:BoundField DataField="LastSMSDate" Hea derText="Last SMS Date" ReadOnly="true" />
                        <asp:BoundField DataField="AlertDate" HeaderText="Alert Date" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Mobile No">
                            <HeaderTemplate>
                                <asp:Label ID="lblCustMobileNumber" runat="server" Text="Mobile No"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtMobileNo" runat="server" Text='<%# Eval("Mobile") %>' CssClass="txtField"
                                    MaxLength="10" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server"
                                    CssClass="cvPCG" ErrorMessage="<br />Please give only Numbers" ValidationExpression="\d+"
                                    ControlToValidate="txtMobileNo" Display="Dynamic"></asp:RegularExpressionValidator>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Button ID="btnUpdateMobileNo" runat="server" Text="Update" OnClick="btnUpdateMobileNo_Click" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <div id="DivPager" runat="server">
                    <table style="width: 100%">
                        <tr id="trPager" runat="server">
                            <td>
                                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                            </td>
                        </tr>
                    </table>
                </div>--%>
               <%-- <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblDisclaimer" runat="server" CssClass="FieldName" Text="Note: Change of phone number would reflect in the corresponding Customer Profile"
                                Style="display: none" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>--%>
            <%--</asp:Panel>--%>
        <%--</td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnSend" Text="Send SMS" runat="server" CssClass="openQuickMobileAdd PCGButton"
                OnClick="btnSend_Click" OnClientClick="return TestCheckBox();" />
        </td>--%>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerIdWithoutMobileNumber" runat="server" />
<asp:HiddenField ID="hdnCustomerNameWithoutMobileNumber" runat="server" />
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<%--<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />--%>
