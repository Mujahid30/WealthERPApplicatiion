<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Download.ascx.cs" Inherits="WealthERP.Admin.Download" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    
    function checkdateFromDate(sender, args) {
        
    //create a new date var and set it to the
    //value of the senders selected date
    var selectedDate = new Date();
    selectedDate = sender._selectedDate;
    //create a date var and set it's value to today
    var todayDate = new Date();
    var mssge = "";

    if(selectedDate > todayDate) {
        
        //set the senders selected date to today
        sender._selectedDate = todayDate;
        //set the textbox assigned to the cal-ex to today
        sender._textbox.set_Value(sender._selectedDate.format(sender._format));
        //alert the user what we just did and why
        alert("Warning! - Date Cannot be in the future. Date value is reset to the current date");
     }
 }

 function checkdateToDate(sender, args) {
     
     //create a new date var and set it to the
     //value of the senders selected date
     var selectedDate = new Date();
     selectedDate = sender._selectedDate;
     //create a date var and set it's value to today
     var todayDate = new Date();
     var mssge = "";

     if (selectedDate > todayDate) {
         //set the senders selected date to today
         sender._selectedDate = todayDate;
         //set the textbox assigned to the cal-ex to today
         sender._textbox.set_Value(sender._selectedDate.format(sender._format));
         //alert the user what we just did and why
         alert("Warning! - Date Cannot be in the future.Date value is reset to the current date");
     }
 }
    </script> 
<div>
    <asp:ScriptManager ID="DownloadScripManager" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td class="HeaderCell">
                <label id="lblheader" class="HeaderTextBig" title=" Download Screen">
                    Download Screen</label>
            </td>
        </tr>
    </table>
</div>
<div id="MainDiv">
    <table width="100%">
        <tr>
            <td class="leftField">
                <label id="lbl1" class="FieldName" title="Choose To Upload">
                    Choose To Upload:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlUploadType" runat="server">
                    <asp:ListItem Text="Price" Value="Price"></asp:ListItem>
                    <asp:ListItem Text="Corp Action" Value="Corp Action"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label1" class="FieldName" title=" Asset Group">
                    Asset Group:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlAssetGroup" AutoPostBack="true" OnSelectedIndexChanged="ddlAssetGroup_OnSelectedChange" runat="server">
                    <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                    <asp:ListItem Text="Equity" Value="Equity"></asp:ListItem>
                    <asp:ListItem Text="MF" Value="MF"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label2" class="FieldName" title="Source">
                    Source:</label>
            </td>
            <td class="rightField">
                <asp:DropDownList CssClass="cmbField" ID="ddlSource" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label3" class="FieldName" title="FromDate">
                    FromDate:</label>
                    
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtFromDate" runat="server" CssClass="txtField"></asp:TextBox><cc1:CalendarExtender
                    ID="FrmDate" TargetControlID="txtFromDate" runat="server" OnClientDateSelectionChanged="checkdateFromDate" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <%--<asp:RequiredFieldValidator ID="FrmdateValidater"  runat="server" ControlToValidate="txtFromDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <label id="Label4" class="FieldName" title="ToDate">
                    ToDate:</label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtToDate" CssClass="txtField" runat="server"></asp:TextBox><cc1:CalendarExtender
                    ID="TDate" TargetControlID="txtToDate" runat="server" OnClientDateSelectionChanged = "checkdateToDate" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
                <%--<asp:RequiredFieldValidator ID="TodateValidater" runat="server" ControlToValidate="txtToDate" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td class="SubmitCell">
                <asp:Button ID="btnDownload" Text="Download" CssClass="PCGButton" runat="server"
                    OnClick="OnClick_Download" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Upload_btnDownload');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Upload_btnDownload');" />
            &nbsp;&nbsp;
                <asp:Button ID="btnDownloadCurrent" Text="Download Current" 
                    CssClass="PCGLongButton" runat="server"
                    onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_Upload_btnDownloadCurrent');"
                    
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_Upload_btnDownloadCurrent');" 
                    Visible="false" onclick="btnDownloadCurrent_Click" />
            
                
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="errDateNull" runat="server" Text="" CssClass="Error" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gvResult" runat="server"  CssClass="GridViewStyle" AllowSorting="True"
                    AutoGenerateColumns="False"  Font-Size="Small"
                    AllowPaging="true">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:BoundField DataField="Date" HeaderText="Date" />
                        <asp:BoundField DataField="Result" HeaderText="Result" />
                        <asp:BoundField DataField="NumofRecords" HeaderText="Number of Records" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnAssetGroup" runat="server" />