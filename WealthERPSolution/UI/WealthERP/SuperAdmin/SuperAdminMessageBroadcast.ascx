<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SuperAdminMessageBroadcast.ascx.cs" Inherits="WealthERP.SuperAdmin.SuperAdminMessageBroadcast" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<style type="text/css">
    .style1
    {
        width: 463px;
    }
    .style2
    {
        width: 46px;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<script type="text/javascript" src="/Scripts/jquery.js"></script>
<script type="text/javascript" src="/Scripts/flexigrid.js"></script>
<link rel="stylesheet" type="text/css" href="/App_Themes/Purple/flexigrid.css" /> 

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvAdviserList.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvAdviserList.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBx";

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
<table width="100%">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Broadcast Message"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div class="success-msg" id="SuccessMessage" runat="server" visible="false" align="center">
                Message sent to Selected Advisors Successfully...
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center" Height="275px" ScrollBars="Horizontal">
<table width="100%" class="TableBackground">
    <tr>
        <td>
            <asp:GridView ID="gvAdviserList" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" HorizontalAlign="Center" ShowHeader="true" ShowFooter="true" DataKeyNames="AdviserId">
                <FooterStyle CssClass="FooterStyle" />
                <PagerSettings Visible="False" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <%--<PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />--%>
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>  
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            <input id="chkBoxAll"  name="vehicle" value="Bike" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AdviserId" HeaderText="AdviserId" />
                    <asp:BoundField DataField="OrgName" HeaderText="Organisation" />
                </Columns>
            </asp:GridView>            
        </td>
    </tr>
 </table>
</asp:Panel>


<table class="TableBackground" width="100%">
<tr>
    <td class="leftField">
        <asp:Label ID="lblMsgText" Text="Message Text:" runat="server" CssClass="FieldName"></asp:Label>
    </td>
    <td >
        <asp:TextBox ID="MessageBox" runat="server" Width="600px" Height="100px" TextMode="MultiLine"
        MaxLength="255"></asp:TextBox>
    </td>
</tr>
<tr>
    <td class="leftField">
        <asp:Label ID="lblExpiryDate" Text="Expiry Date:" runat="server" CssClass="FieldName"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="txtExpiryDate" CssClass="txtField" runat="server" Width="175px"></asp:TextBox><span id="Span9" class="spnRequiredField">*</span>
        <ajaxToolKit:CalendarExtender runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate"
            ID="calExeActivationDate" Enabled="true">
        </ajaxToolKit:CalendarExtender>
        <ajaxToolKit:TextBoxWatermarkExtender TargetControlID="txtExpiryDate" WatermarkText="dd/mm/yyyy"
            runat="server" ID="wmtxtActivationDate">
        </ajaxToolKit:TextBoxWatermarkExtender>        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtExpiryDate"
        ErrorMessage="Expiry Date Required" CssClass="cvPCG" Display="Dynamic"></asp:RequiredFieldValidator>
    </td>
</tr>
<tr>
    <td></td>
    <td>
        <asp:Button ID="SendMessage" runat="server" Text="Send Info" CssClass="PCGButton"
        OnClick="SendMessage_Click" />
    </td>
</tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div class="information-msg" id="MessageBroadcast" runat="server" visible="false" align="center">
                <br />
                <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
                    <br />
            </div>
        </td>
    </tr>
</table>

