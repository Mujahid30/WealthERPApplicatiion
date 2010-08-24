<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserCustomerSMSAlerts.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserCustomerSMSAlerts" %>


<%--Used to create Popups and Alert Box--%>

<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link href="/Scripts/colorbox.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<script type="text/javascript">
    var TargetBaseControl = null;

    window.onload = function() {
        try {
            //get target base control.
            TargetBaseControl = document.getElementById('<%= this.gvCustomerSMSAlerts.ClientID %>');
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

        //get all the control of the type INPUT in the base control.
        var Inputs = TargetBaseControl.getElementsByTagName("input");
        var totalChecked=0;

        for (var n = 0; n < Inputs.length; ++n) {
            if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 && Inputs[n].checked) {
                totalChecked++;                             
            }
        }
        if (totalChecked > 0) {
            return totalChecked;
        }
        else {
            alert('Select at least one checkbox!');
            return false;
        }
    }
    function MobileNumberCheck() {
        var customerwithoutmobilenumber = document.getElementById("<%= hdnCustomerIdWithoutMobileNumber.ClientID %>").value;
        var customerlist = new Array();
        customerlist = customerwithoutmobilenumber.split(',');
        var customerlistlength=0;
        //=customerlist.length;
        if (customerlist != "" && customerlist!=null) {
            for (var j = 0; j < customerlist.length; j++) {
                if (customerlist[j].value != 0 && customerlist[j].value != "") {
                    customerlistlength++;
                }
            }
        }
        var numberofcustomerschecked = TestCheckBox();
        alert(numberofcustomerschecked);
        
        if (numberofcustomerschecked != false) {
            if (numberofcustomerschecked >= 0) {
                if (customerlistlength != numberofcustomerschecked) {
                    //alert(customerlistlength + ' ' + numberofcustomerschecked);
                    var confirmation = confirm('Do you wish to add Customer Mobile number');
                    if (confirmation == true) {
                        return true;                     
                    }
                    else {
                        return false;
                    }
                }
                else {
                    return true;
                }
            }
        }
        else
            return false;
        
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
    <tr>
        <td>
            <asp:Panel ID="pnlCustomerSMSAlerts" runat="server" Height="300px" Width="100%" ScrollBars="Vertical"
                Visible="false" HorizontalAlign="Left">
                <asp:Label ID="lblNoRecords" Text="No Alert Exists" runat="server" Visible="false"
                    CssClass="FieldName"></asp:Label>
                <asp:GridView ID="gvCustomerSMSAlerts" DataKeyNames="CustomerId,AlertId" runat="server"
                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" Width="100%"
                    Font-Size="Small" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg"
                    CssClass="GridViewStyle" ShowFooter="true" OnRowDataBound="gvCustomerSMSAlerts_RowDataBound"
                    AllowPaging="True">
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
                                <asp:CheckBox ID="chkCustomerSMSAlert" runat="server" Visible="false" CssClass="Field"
                                     />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                        <asp:BoundField DataField="Name" HeaderText="Details" />
                        <asp:BoundField DataField="AlertMessage" HeaderText="Alert Message" />
                        <asp:BoundField DataField="TimesSMSSent" HeaderText="No of Times SMS Sent" />
                        <asp:BoundField DataField="LastSMSDate" HeaderText="Last SMS Date" />
                        <asp:BoundField DataField="AlertDate" HeaderText="Alert Date" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Button ID="btnSend" Text="Send SMS" runat="server" CssClass="openQuickMobileAdd PCGButton" OnClick="btnSend_Click" OnClientClick="return TestCheckBox();" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerIdWithoutMobileNumber" runat="server" />
<asp:HiddenField ID="hdnCustomerNameWithoutMobileNumber" runat="server" />
