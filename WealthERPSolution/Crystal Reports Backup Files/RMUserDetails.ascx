<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RMUserDetails.ascx.cs"
    Inherits="WealthERP.Advisor.RMUserDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<script type="text/javascript">
    

    function checkAllBoxes() {

        var totalChkBoxes = parseInt('<%= gvUserMgt.Items.Count %>');
        var gvControl = document.getElementById('<%= gvUserMgt.ClientID %>');


        var gvChkBoxControl = "cbRecons";


        var mainChkBox = document.getElementById("chkBxWerpAll");

        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    
   
</script>

<%--<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Staff user Management:"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <div class="failure-msg" align="center" id="ErrorMessage" runat="server">
            <asp:Label ID="lblMessage" runat="server" ></asp:Label>
           
            </div>
        </td>
    </tr>
</table>--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                          Staff user Management
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
       <div id="dvUserMgt" runat="server" style="width: 840px;">
                    <telerik:RadGrid ID="gvUserMgt" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" OnNeedDataSource="gvAssoMgt_OnNeedDataSource"
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false">
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
                                <telerik:GridBoundColumn DataField="RMName" HeaderText="Staff" AllowFiltering="true"
                                    SortExpression="RMName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="RMName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="LoginId" HeaderText="Login Id" AllowFiltering="true"
                                    SortExpression="LoginId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="LoginId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left"  Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EmailId" HeaderText="Email Id" SortExpression="EmailId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="EmailId" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn HeaderText="" AllowFiltering="false">
                                    <ItemStyle />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkReset" runat="server"  Text="Reset Password"
                                            OnClick="lnkReset_Click">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                 <telerik:GridBoundColumn DataField="UserId" HeaderText="User Id" SortExpression="UserId"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="UserId" FooterStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                    </div>
       </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
     
    <tr>
        <td>
            <asp:Label ID="lblStatus" class="FieldName" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Reset & Send Login Details" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_RMUserDetails_btnGenerate', 'L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_RMUserDetails_btnGenerate', 'L');" 
                CssClass="loadme PCGLongButton" />
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
<asp:Button ID="btnNameSearch" runat="server" Text="" 
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnNameFilter" runat="server" />
