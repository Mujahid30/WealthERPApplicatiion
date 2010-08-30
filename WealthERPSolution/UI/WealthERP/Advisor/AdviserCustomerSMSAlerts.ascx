<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomerSMSAlerts.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserCustomerSMSAlerts" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%--Used to create Popups and Alert Box--%>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<link href="/Scripts/colorbox.css" rel="stylesheet" type="text/css" />

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<script type="text/javascript">
    var TargetBaseControl = null;

    window.onload = function() {
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= gvCustomerSMSAlerts.ClientID %>');            
            HideColumn(7);            
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
        var countWithMobileNumber=0;      
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
                    return true;
                }
            }
        }
    }

    function HideColumn(columnNo) {
        var dgTest = document.getElementById("<%=gvCustomerSMSAlerts.ClientID %>");
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
        var dgTest = document.getElementById("<%=gvCustomerSMSAlerts.ClientID %>");
        try {
            for (var i = 0; i < dgTest.rows.length; i++) {
                dgTest.rows[i].cells[columnNo].style.display = "block";
            }
        }
        catch (e)
        { }
    }
   
</script>

<%--<script type="text/javascript">
    $(document).ready(function() {
        $(".openQuickMobileAdd").colorbox({ width: "700px", inline: true, href: "/Alerts/QuickMobileNumberAdd.aspx" });
    });
</script>--%>
<table width="100%">
<tr>
<td align="center"><div class="success-msg" id="SuccessMessage" runat="server" visible="false" align="center">
    SMS Sent Successfully
</div></td></tr>
</table>

<table width="100%">
    <tr>
        <td>
            <asp:Label ID="lblCustomerSMSAlerts" Text="Customer SMS Alerts" CssClass="HeaderTextSmall"
                runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblLicenceName" Text="SMS Licence Left:" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLincenceValue" Text="" runat="server" CssClass="Field"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
        <tr align="center">
        <td colspan="2" class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID="pnlCustomerSMSAlerts" runat="server" Height="400px" Width="100%" ScrollBars="Vertical"
                Visible="false" HorizontalAlign="Left">
                <asp:Label ID="lblNoRecords" Text="No Alert Exists" runat="server" Visible="false"
                    CssClass="FieldName"></asp:Label>
                <asp:GridView ID="gvCustomerSMSAlerts" DataKeyNames="CustomerId,AlertId" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="100%"
                    Font-Size="Small" CssClass="GridViewStyle" ShowFooter="True" OnRowDataBound="gvCustomerSMSAlerts_RowDataBound">
                    <PagerStyle BorderStyle="Solid" />
                    <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                    <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                    <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                    <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                    <PagerSettings Mode="NextPreviousFirstLast" />
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkCustomerSMSAlert" runat="server" Visible="false" CssClass="Field" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" ReadOnly="true" />
                        <asp:BoundField DataField="Name" HeaderText="Details" ReadOnly="true" />
                        <asp:BoundField DataField="AlertMessage" HeaderText="Alert Message" ReadOnly="true" />
                        <asp:BoundField DataField="TimesSMSSent" HeaderText="No of Times SMS Sent" ReadOnly="true" />
                        <asp:BoundField DataField="LastSMSDate" HeaderText="Last SMS Date" ReadOnly="true" />
                        <asp:BoundField DataField="AlertDate" HeaderText="Alert Date" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Mobile No">
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
                </div>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnSend" Text="Send SMS" runat="server" CssClass="openQuickMobileAdd PCGButton"
                OnClick="btnSend_Click" OnClientClick="return TestCheckBox();"/>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerIdWithoutMobileNumber" runat="server" />
<asp:HiddenField ID="hdnCustomerNameWithoutMobileNumber" runat="server" />
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
