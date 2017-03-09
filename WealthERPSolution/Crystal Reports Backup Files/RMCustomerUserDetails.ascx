<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMCustomerUserDetails.ascx.cs"
    Inherits="WealthERP.Advisor.RMCustomerUserDeatils" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
//    $(document).ready(function() {
//        $('.loadme').click(function() {
//            $(".loadmediv").colorbox({ width: "240px",overlayClose:false, inline: true, open: true, href: "#LoadImage" });
//        });
//    });

    $(document).ready(function() {
        $('.loadme').click(function() {
        var panel = document.getElementById('<%= gvCustomer.ClientID %>');
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

        var totalChkBoxes = parseInt('<%= gvCustomer.Items.Count %>');
        var gvControl = document.getElementById('<%= gvCustomer.ClientID %>');


        var gvChkBoxControl = "cbRecons";


        var mainChkBox = document.getElementById("chkBxWerpAll");

        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
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
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                         Customer User Details
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
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
        
             <div id="dvUserMgt" runat="server" style="width: 840px;">
                    <telerik:RadGrid ID="gvCustomer" runat="server" GridLines="None" AutoGenerateColumns="False"  OnItemDataBound="ItemDataBound" 
                        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvCustomer_OnNeedDataSource"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false"     >
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="UserId" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblchkBxSelect" runat="server"></asp:Label>
                                        <input id="chkBxWerpAll" name="chkBxWerpAll" type="checkbox" onclick="checkAllBoxes()" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbRecons" runat="server" Checked="false" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer Name" AllowFiltering="true"
                                    SortExpression="CustomerName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Customer Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <%--<telerik:GridBoundColumn DataField="Login Id" HeaderText="Login Id" AllowFiltering="true"
                                    SortExpression="Login Id" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Login Id" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left"  Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn HeaderText="Login Id" AllowFiltering="false">
                                    <ItemStyle />
                                    <ItemTemplate>
                                    <asp:Label ID="lblLoginId" runat="server" Text='<%# Eval("Login Id").ToString() %>'></asp:Label>         
         
                                     <asp:LinkButton ID="lnkGenerateLogin" runat="server" CausesValidation="false"  OnClick="lnkGenerate_Click"
                                         Text='Generate & Send Login Details' CommandArgument='<%# Eval("UserId") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Email Id" HeaderText="Email Id" SortExpression="Email Id"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Email Id" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkResetPassword" runat="server"  Text="Reset Password"
                                            OnClick="lnkReset_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                
                                 <telerik:GridBoundColumn DataField="UserId" HeaderText="User Id" SortExpression="UserId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="UserId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True"  />
                            
                        </ClientSettings>
                    </telerik:RadGrid>
                    </div>
            
            
            
            
        </td>
    </tr>
    <%--<tr align="center">
        <td>
                        <Pager:Pager ID="mypager" runat="server" />
        </td>
    </tr>--%>
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
<asp:Button ID="btnNameSearch" runat="server" 
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="C_FirstName ASC" />
<asp:HiddenField ID="hdnNameFilter" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />

<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />