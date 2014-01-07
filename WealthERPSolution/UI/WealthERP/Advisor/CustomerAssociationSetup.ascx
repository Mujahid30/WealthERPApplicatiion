<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAssociationSetup.ascx.cs"
    Inherits="WealthERP.Advisor.CustomerAssociationSetup" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script language="javascript" type="text/javascript">

    function checkAllBoxes() {

        var totalChkBoxes = parseInt('<%= gvAssociations.Items.Count %>');
        var gvControl = document.getElementById('<%= gvAssociations.ClientID %>');


        var gvChkBoxControl = "cbRecons";


        var mainChkBox = document.getElementById("chkBxWerpAll");

        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    </script>

<script type="text/javascript">

 //********************************************************************************************************


    function CheckSelection(type) {

        var gvAssociation = document.getElementById("ctrl_CuCustomerAssociationSetup_gvAssociations");
        var chkArray = gvAssociation.getElementsByTagName("input");



        var checked = 0;
        for (var i = 0; i < chkArray.length; i++) {
            if (chkArray[i].type == "checkbox" && chkArray[i].checked == true) {

                checked = 1;
                break;
            }
        }
        if (checked == 0) {
            alert('Please select atleast one Customer..');
            return false;
        }
        if (type == "BranchGroupHead") {
            var ddlBranch = document.getElementById("<%=ddlAdvisorBranchList.ClientID %>").value
            if (ddlBranch == 0) {
                alert("Please select a Branch");
                return false;
            }
            var ddlBranchRM = document.getElementById("<%=ddlBranchRMList.ClientID %>").value
            if (ddlBranchRM == 0) {
                alert("Please select a RM");
                return false;
            }
            CheckBranchGroupHead();





        }
        else {
            var ddlRMBranch = document.getElementById("<%=ddlBranchList.ClientID %>").value
            if (ddlRMBranch == 0) {
                alert("Please select a Branch");
                return false;
            }

            var ddlRM = document.getElementById("<%=ddlBranchRMList.ClientID %>").value
            if (ddlRM == 0) {
                alert("Please select a RM");
                return false;
            }
            CheckRMGroupHead();
        }
    }
        
  //********************************************************************

        function CheckBranchGroupHead() {
           
            var GroupHeadValue = document.getElementById("ctrl_CuCustomerAssociationSetup_hndIsGroupHead").value;
            if (GroupHeadValue == "true") {
                if (confirm("Some of the selected customer are GroupHead,Do you want to change Branch for member customers also?"))
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        //********************************************************************

        function CheckRMGroupHead() {

            var GroupHeadValue = document.getElementById("ctrl_CuCustomerAssociationSetup_hndIsGroupHead").value;
            if (GroupHeadValue == "true") {
                if (confirm("Some of the selected customer are GroupHead,Do you want to change RM for member customers also?"))
                    return true;
                else
                    return false;
            }
            else
                return true;
        } 
    
</script>
<style type="text/css">
.CheckField
{
    font-family: Verdana,Tahoma;
    font-weight: normal;
    font-size: 11px;
    color: #16518A;
    
}
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                          Customer Branch/RM Association
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

<div class="failure-msg" id="ErrorMessage" runat="server" visible="false" 
    align="center" style="margin-left: 80px">
    No Records found.....
</div>
<div class="success-msg" id="SuccessMsg" runat="server" visible="false" 
    align="center" style="margin-left: 80px">
    Reassign successfully.....
</div>
<asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
    <table width="100%" cellspacing="0" cellpadding="2" style="margin-top:15px">
<tr>
<td colspan="3">
<table>
<tr style="width:100%" id="trHeader" runat="server" >
        <%--<td align="right">
          <asp:Label ID="lblPickRMBranch" Text="Please pick the association you want to change: "
                CssClass="FieldName" runat="server"></asp:Label>
        </td>--%>
        <td align="left" style="padding-left:4px">
            <asp:RadioButton ID="rdReassignBranch" runat="server" CssClass="FieldName"
                GroupName="BranchRM" style="text-align:center" AutoPostBack="true"
                oncheckedchanged="rdReassignBranch_CheckedChanged" />
           <asp:Label ID="lblReassignBranch" Text="Reassign Branch" class="Field" runat="server"></asp:Label>     
        </td>
        <td align="left">
            <asp:RadioButton ID="rdReassignRM" runat="server" CssClass="FieldName"
                GroupName="BranchRM" AutoPostBack="true" oncheckedchanged="rdReassignRM_CheckedChanged" />
         <asp:Label ID="lblReassignRM" Text="Reassign RM"  class="Field" runat="server"></asp:Label>     
        </td>
        <td>
      
           <%-- <asp:Label ID="lblPickBranch" Text="Pick Branch: " CssClass="FieldName" runat="server"></asp:Label>--%>
        </td>
        <td>
        &nbsp;&nbsp;
        </td>
        <td align="left" id="trReassignRMCustomer" runat="server">
            <asp:DropDownList ID="ddlBranchList" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlBranchList_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="spanddlBranchList" class="spnRequiredField" runat="server">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a branch"
                Text="Please select a branch" Display="Dynamic" ValidationGroup="btnReassign"
                ControlToValidate="ddlBranchList" InitialValue="0">
            </asp:RequiredFieldValidator>
       
        </td>
    </tr>
  <%--  <tr id="trReassignRMCustomer" runat="server" style="width:50%">
        <td align="right">
            <asp:Label ID="lblPickBranch" Text="Pick Branch: " CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td align="left" colspan="2">
            <asp:DropDownList ID="ddlBranchList" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlBranchList_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>--%>
</table>
</td>
</tr>
    
    
    <tr>
    <td colspan="2" style="padding-left:8px">
    
    <asp:CheckBox id="chkSelectAllpages"  name="Select All across pages" value="Customer" runat="server" type="checkbox" AutoPostBack="true" OnCheckedChanged="chkSelectAllpages_CheckedChanged" />
 <%--   <asp:CheckBox ID="chkSelectAllpages" runat="server" CssClass="FieldName" Text="Select All across pages"/>--%>
   <asp:Label ID="lblAllpages" class="Field" Text="Select all across pages" runat="server"></asp:Label>
    </td>
     <td class="leftField" align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
     </td>
    </tr>
    <tr>
        <td colspan="3">
            
             <div id="dvUserMgt" runat="server" style="width: 840px;">
                    <telerik:RadGrid ID="gvAssociations" runat="server" GridLines="None" AutoGenerateColumns="False" 
                        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" 
                        ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false"  OnNeedDataSource="gvAssociations_OnNeedDataSource" >
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            Excel-Format="ExcelML">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="CustomerId,UserId,RMId,BranchId" Width="100%" AllowMultiColumnSorting="True"
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
                                <telerik:GridBoundColumn DataField="Cust_Comp_Name" HeaderText="Customer Name" AllowFiltering="true"
                                    SortExpression="Cust_Comp_Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="Cust_Comp_Name" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BranchName" HeaderText="Branch Name" SortExpression="BranchName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="BranchName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                              
                                 <telerik:GridBoundColumn DataField="RMName" HeaderText="RM" SortExpression="RMName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="RMName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Area" HeaderText="Area" SortExpression="Area"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Area" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="City" HeaderText="City" SortExpression="City"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="City" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn DataField="CustomerType" HeaderText="Type" SortExpression="CustomerType"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="CustomerType" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                
                                <telerik:GridBoundColumn DataField="PAN Number" HeaderText="PAN Number" SortExpression="PAN Number"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="PAN Number" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Address" HeaderText="Address" SortExpression="Address"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="Address" FooterStyle-HorizontalAlign="Left">
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
    <tr style="width:100%">
    <td colspan="3">
    
    </td>
    </tr>
</table>
</asp:Panel>
<table width="100%">
<tr>
    <td>
    <table>
<tr id="trReassignBranch" runat="server">
        <td class="SubmitCell" align="left">
            <asp:Label ID="Label2" Text="Pick Branch: " CssClass="FieldName" runat="server"></asp:Label> 
        </td>
        <td>
            <asp:DropDownList ID="ddlAdvisorBranchList" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlAdvisorBranchList_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="spanAdvisorBranch" class="spnRequiredField" runat="server">*</span>
            <asp:RequiredFieldValidator ID="reqddlAdviser" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a Branch"
                Text="Please select a Branch" Display="Dynamic" ValidationGroup="btnReassign"
                ControlToValidate="ddlAdvisorBranchList" InitialValue="0">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trReassignRM" runat="server">
        <td class="SubmitCell" align="left">
          <asp:Label ID="Label1" Text="Pick RM: " CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlBranchRMList" runat="server" CssClass="cmbField" 
                onselectedindexchanged="ddlBranchRMList_SelectedIndexChanged" AutoPostBack="true">
            </asp:DropDownList>
            <span id="spanBranchRM" class="spnRequiredField" runat="server">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG" ErrorMessage="Please select a RM"
                Text="Please select a RM" Display="Dynamic" ValidationGroup="btnReassign"
                ControlToValidate="ddlBranchRMList" InitialValue="0">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
<tr>
    <td>
    
    </td>
        <td align="left">        
        <asp:Button ID="btnReassignBranch" runat="server"  ValidationGroup="btnReassign"              
                CssClass="PCGMediumButton"                
                Text="Reassign" onclick="btnReassignBranch_Click" />            
        </td>
    </tr>
     <tr>
     <td>
    
     </td>
        <td align="left">
           <asp:Button ID="btnReassignRM" runat="server" ValidationGroup="btnReassign" 
                CssClass="PCGMediumButton"               
                Text="Reassign" onclick="btnReassignRM_Click" />         
        </td>
    </tr>
     </table>
    <%--<tr id="trPager" runat="server" width="100%" >
        <td align="right">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>--%>
       
    </tr>
    </table>

<asp:Button ID="btnBranchSearch" runat="server" Text="" OnClick="btnBranchSearch_Click" BorderStyle="None" BackColor="Transparent"  />
<asp:Button ID="btnRMSearch" runat="server" Text="" onclick="btnRMSearch_Click" BorderStyle="None" BackColor="Transparent" />    
<asp:Button ID="btnCustomerSearch" runat="server" Text="" onclick="btnCustomerSearch_Click" BorderStyle="None" BackColor="Transparent" /> 
<asp:Button ID="btnAreaSearch" runat="server" Text="" onclick="btnAreaSearch_Click" BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnCitySearch" runat="server" Text="" onclick="btnCitySearch_Click" BorderStyle="None" BackColor="Transparent" />  
    
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnPincodeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAreaFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRMFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnParentFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnReassignRM" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="false" />
<asp:HiddenField ID="hdnBranchFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCityFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRMNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hndIsGroupHead" runat="server" Visible="false" />
<asp:HiddenField ID="hndBranchIdFilter" Value="0" runat="server" Visible="false" />

