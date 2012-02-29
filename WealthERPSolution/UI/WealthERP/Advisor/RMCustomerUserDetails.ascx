<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerUserDetails.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerUserDeatils" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />

<script type="text/javascript">
//    $(document).ready(function() {
//        $('.loadme').click(function() {
//            $(".loadmediv").colorbox({ width: "240px",overlayClose:false, inline: true, open: true, href: "#LoadImage" });
//        });
//    });

    $(document).ready(function() {
        $('.loadme').click(function() {
        var panel = document.getElementById('<%= gvCustomers.ClientID %>');
            var chkArray = panel.getElementsByTagName("input");
            var checked = 0;
            for (var i = 0; i < chkArray.length; i++) {
                if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {
                    checked = 1;
                    break;
                }
            }

            if (checked != 1) {
                alert('Please select Customer to send Password');
                return false;
            }
            else {
                $(".loadmediv").colorbox({ width: "260px", overlayClose: false, inline: true, open: true, href: "#LoadImage" });
            }
        });

    });


  //***************************************************************************************************
    
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var totalChkBoxes = parseInt('<%= gvCustomers.Rows.Count %>');
        var gvCustomers = document.getElementById('<%= gvCustomers.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkBoxAll");

        //get an array of input types in the gridview
        var inputTypes = gvCustomers.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template            
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
  //********************************************************************************************************
</script>

<%--
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="RM Customer User Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>--%>
<table align="center" style="display:none;">
<tr><td>
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
</td></tr>

    
</table>
<table align="center" width="100%">
<tr>
        <td>
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Customer User Details"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

 <table width="100%" cellspacing="0" cellpadding="0" id="tblMessage" runat="server" visible="false">
    <tr>
    <td align="center">
    <div class="success-msg" id="SuccessMsg" runat="server" visible="false" align="center">
       
    </div>
    <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
      
    </div>
    </td>
    </tr>
 </table>

<table style="width: 100%;" class="TableBackground">
    <tr>
    <td align="right">
    <%--<asp:ImageButton ID="imgBtnExport1" ImageUrl="../App_Themes/Maroon/Images/Export_Excel.png"
                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                        OnClick="imgBtnExport1_Click" style="width: 20px" />--%>
    </td>
    </tr>

    <tr id="trPagger" runat="server" visible="true">
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" CellPadding="4"
                DataKeyNames="UserId" CssClass="GridViewStyle" ShowFooter="true" 
                AllowSorting="True" OnSorting="gvCustomers_Sort" 
                onrowcommand="gvCustomers_RowCommand" 
                onrowdatabound="gvCustomers_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <Columns>
                    <%--<asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    
                     <asp:TemplateField HeaderText="Select">
                     <HeaderTemplate>
                           <%-- <asp:Label ID="LblSelect" runat="server" Text="Select All"></asp:Label>--%>
                            <br />
                            <%--<asp:Button ID="lnkSelectAll" Text="All" runat="server"  OnClientClick="return CheckAll();" />--%>
                           <input id="chkBoxAll"  name="CheckAllCustomer" value="Customer" type="checkbox" onclick="checkAllBoxes()" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    
                    <%--<asp:BoundField DataField="Customer Name" HeaderText="Customer Name"  />--%>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustName" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" runat="server" Text = '<%# hdnNameFilter.Value %>' CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_RMCustomerUserDetails_btnNameSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                           <asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("CustomerName").ToString() %>'></asp:Label>
                            <%--<asp:LinkButton ID="lnkCustomerNames" runat="server" CausesValidation="false" CommandName="ViewDetails"
                                Text='<%# Eval("CustomerName").ToString() %>' CommandArgument='<%# Eval("UserId") %>'></asp:LinkButton>--%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                   <%-- <asp:BoundField DataField="Login Id" HeaderText="Login Id" />--%>
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblLoginId" runat="server" Text="Login Id"></asp:Label>                           
                        </HeaderTemplate>
                        <ItemTemplate>
                           <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("Login Id").ToString() %>'></asp:Label>
                           
                          <asp:LinkButton ID="lnkGenerateLogin" runat="server" CausesValidation="false" CommandName="GenerateLogin"
                                Text='Generate & Send Login Details' CommandArgument='<%# Eval("UserId") %>'>
                          </asp:LinkButton>
                          
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Email Id" HeaderText="Email Id" />
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblResetPassword" runat="server" Text="Reset Password"></asp:Label>                           
                        </HeaderTemplate>
                        <ItemTemplate>
                                                      
                          <asp:LinkButton ID="lnkResetPassword" runat="server" CausesValidation="false" CommandName="resetPassword"
                                Text='Reset Password' CommandArgument='<%# Eval("UserId") %>'>
                          </asp:LinkButton>
                          
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    
                    <%--<asp:ButtonField CommandName="resetPassword" Text="Reset Password"/>--%>
                </Columns>
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    <tr align="center">
        <td>
                        <Pager:Pager ID="mypager" runat="server" />
        </td>
    </tr>
   <%-- <tr>
        <td align="center">
            <table id="tblPager" runat="server" align="center">
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr> --%>
    <%--<tr>
        <td>
            <asp:Label ID="lblMsg" class="Error" runat="server" Text="No Records Found...!"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Reset & Send Login Details"
                CssClass="loadme PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMCustomerUserDetails_btnGenerate', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMCustomerUserDetails_btnGenerate', 'L');" />
             <div class='loadmediv'></div>
        </td>
    </tr>
</table>
<asp:Button ID="btnNameSearch" runat="server" Text="" OnClick="btnNameSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="C_FirstName ASC" />
<asp:HiddenField ID="hdnNameFilter" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />

<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />