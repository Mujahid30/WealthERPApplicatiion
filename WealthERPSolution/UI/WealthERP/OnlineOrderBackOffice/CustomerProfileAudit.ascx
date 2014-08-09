<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerProfileAudit.ascx.cs" Inherits="WealthERP.OnlineOrderBackOffice.CustomerProfileAudit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                           Customer Profile Audit
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
 <table width="100%">
            
            <tr>
            <td>
            <asp:Label ID="lblCustName" runat="server" Text="Select Customer:" CssClass="FieldName" ></asp:Label> 
            </td>
               <td align="left" id="tdtxtCustomerName" runat="server" >
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" Width="250px">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName"
                WatermarkText="Enter Three Characters of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="3" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td class="leftLabel" >
        <asp:Label ID="lblModificationDate" runat="server" Text="Select date:" CssClass="FieldName" ></asp:Label>
        </td>
        <td class="rightData">
                    <telerik:RadDatePicker ID="rdpDownloadDate" CssClass="txtField" runat="server" AutoPostBack="false"
                        Skin="Telerik" EnableEmbeddedSkins="false">
                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvDOwnloadDate" runat="server" ErrorMessage="Please select a valid date"
                        Display="Dynamic" ControlToValidate="rdpDownloadDate" Text="Please select a valid date"
                        CssClass="rfvPCG" ValidationGroup="IssueExtract">Please select a valid date</asp:RequiredFieldValidator>
                </td>
                <td>
                 <asp:Button ID="btnSubmit" runat="server"  Text="Submit"/>
                 </td>
            </tr>
            <tr>
                <td class="tdSectionHeading" colspan="6">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                         <asp:Label ID="Label1" runat="server" Text="Profile Details"></asp:Label>
                        </div>
                    
                </td>
            </tr>
            </table>
            <asp:HiddenField ID="txtCustomerId" runat="server"/>
            <asp:HiddenField ID="hdnIsSubscripted" runat="server" />