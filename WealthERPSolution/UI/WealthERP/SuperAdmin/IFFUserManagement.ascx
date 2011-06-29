<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFFUserManagement.ascx.cs" Inherits="WealthERP.SuperAdmin.IFFUserManagement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    $(document).ready(function() {
        $('.loadme').click(function() {
            var panel = document.getElementById('<%= gvIFFUsers.ClientID %>');
            var chkArray = panel.getElementsByTagName("input");
            var checked = 0;
            for (var i = 0; i < chkArray.length; i++) {
                if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {
                    checked = 1;
                    break;
                }
            }

            if (checked != 1) {
                alert('Please select RM to send Password');
                return false;
            }
            else {
                $(".loadmediv").colorbox({ width: "260px", overlayClose: false, inline: true, open: true, href: "#LoadImage" });
            }
        });

    });



    function CheckAll() {
        alert("abc..");
        var panel = document.getElementById('<%= gvIFFUsers.ClientID %>');
        var chkArray = gvIFFUsers.getElementsByTagName("input");
        alert(chkArray);
        for (var i = 0; i < chkArray.length; i++) {
            if (chkArray[i].type == "checkbox") {
                alert(chkArray[i]);
                if (chkArray[i].checked == false) {

                    chkArray[i].checked = true;
                }
                else
                    chkArray[i].checked = false;
            }
        }



    };

    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvIFFUsers.Rows.Count %>');
        var gvControl = document.getElementById('<%= gvIFFUsers.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkBoxChild";

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

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="IFF User Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

 <table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
        <td align="center">
            <div class="success-msg" id="SuccessMsg" runat="server" visible="false" align="center">
            </div>
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
   </tr>
 </table>
 
 <table style="width: 100%;">
    <tr id="trNoRecords" runat="server">
        <td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Visible="false" Text="No Records Found...!"></asp:Label>
        </td>
    </tr>
    <tr id="trPagger" runat="server">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:GridView ID="gvIFFUsers" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True" ShowFooter="true" AutoGenerateColumns="False" 
                DataKeyNames="UserId" onrowcommand="gvIFFUsers_RowCommand">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" >
                        <HeaderTemplate>
                            <br />
                           <input id="chkBoxAll"  name="vehicle" value="Bike" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBoxChild" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblIFFName" runat="server" Text="IFF Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtIFFNameSearch" runat="server" Text='<%# hdnNameFilter.Value %>'
                                CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_IFFUserManagement_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIFFNameHeader" runat="server" Text='<%# Eval("IFFName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LoginId" HeaderText="Login Id" />
                    <asp:BoundField DataField="EmailId" HeaderText="Email Id" />
                    <asp:ButtonField CommandName="resetPassword" Text="Reset Password" />
                </Columns>
            </asp:GridView>

            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblStatus" class="FieldName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSendEmailToIFF" runat="server" Text="Send Login Password" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_SuperAdminMessageBroadcast_btnSendEmailToIFF', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_SuperAdminMessageBroadcast_btnSendEmailToIFF', 'L');" 
                CssClass="loadme PCGLongButton" onclick="btnSendEmailToIFF_Click" />
            <div class='loadmediv'>
            </div>
        </td>
    </tr>
</table>

<table id="tblPager" runat="server" align="center">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<table align="center" style="display: none;">
    <tr>
        <td>
            <div id='LoadImage' style="width: 231px">
                <table align="center" border="1">
                    <tr>
                        <td style="background-color: #3D77CB; color: #FFFFFF; font-size: 100%; font-weight: bold">
                            Processing,please wait...
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <img src="../Images/Wait.gif" />
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

<asp:Button ID="btnNameSearch" runat="server" Text="" BorderStyle="None" OnClick="btnNameSearch_Click" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="IFFName ASC" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnNameFilter" runat="server" />



    
    
    
