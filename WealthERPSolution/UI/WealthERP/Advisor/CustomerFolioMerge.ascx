<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFolioMerge.ascx.cs" Inherits="WealthERP.Advisor.CustomerFolioMerge" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

<script type="text/javascript">



    function CheckOtherIsCheckedByGVID(spanChk) {

        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        var Chk = spanChk;
        //var n = document.getElementById("gvCustomerFolioMerge").rows.length;
        Parent = document.getElementById("<%=gvCustomerFolioMerge.ClientID %>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {

            if (items[i].id != CurrentRdbID && items[i].type == "checkbox") {

                if (items[i].checked) {
                    items[i].checked = false;
                    alert("Please select one customer at a time.");
                }

            }

        }


    } 

              

</script>

<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />
<style type="text/css">
.CheckField
{
    font-family: Verdana,Tahoma;
    font-weight: normal;
    font-size: 11px;
    color: #16518A;
    
}
</style>

<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Customer Folio Merge"></asp:Label>
            <hr />
        </td>
    </tr>
   
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>

<table width="100%">
 <td class="leftField" align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
     </td>

  <tr>
        <td colspan="3" allign="center" >
            <asp:GridView ID="gvCustomerFolioMerge" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True"  ShowFooter="true" AutoGenerateColumns="False"
                DataKeyNames="CustomerId,AMCCode,Count,portfilionumber" 
                onselectedindexchanged="gvCustomerFolioMerge_SelectedIndexChanged">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                
                 <asp:TemplateField HeaderText="Select">
                     <HeaderTemplate>
                            <asp:Label ID="LblSelect" runat="server" Text=""></asp:Label>
                            <br />
                            <%--<asp:Button ID="lnkSelectAll" Text="All" runat="server"  OnClientClick="return CheckAll();" />--%>
                           <%--<input id="chkBoxAll" class="CheckField" name="CheckAllCustomer" value="Customer" type="checkbox"  />--%>
                        </HeaderTemplate>
                        <ItemTemplate>

                     <asp:checkbox ID="rdbGVRow" oncheckedchanged="rdbGVRow_CheckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);"  runat="server" AutoPostBack="true" />

                  </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" Width="50%" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_AdvisorCustomerAccounts_btnCustomerSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                           <asp:HyperLink ID="hypCustomerName"  runat="server" OnClick="lnkCustomerName_Click" Text='<%# Eval("CustomerName").ToString() %>'>HyperLink</asp:HyperLink>                         
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblAMCName" runat="server" Text="AMC Name"></asp:Label>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAMC" runat="server" Text='<%# Eval("AMCName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblcount" runat="server" Text="Folios"></asp:Label>
                            
                        </HeaderTemplate>
                        <ItemTemplate>
                         <asp:LinkButton ID="hypFolioNo" runat="server" CssClass="GridViewCmbField" OnClick="hypFolioNo_Click" Text='<%# Eval("Count").ToString() %>'>
                         </asp:LinkButton>
                           <%-- <asp:Label ID="lblFolioCount" runat="server" Text='<%# Eval("Count").ToString() %>'></asp:Label>--%>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField> 
                    
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblmergeStatus" runat="server" Text="Merged To"></asp:Label>                            
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblmergerstatus" ItemStyle-HorizontalAlign="Right" runat="server" Text='<%# Eval("mergerstatus").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>                 
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
<td>
<tr>
</tr>
</td>
<td>
<tr>
</tr>
</td>

 <tr style="width:100%">
    <td colspan="3">
    <table width="100%">
    <tr id="trPager" runat="server" width="100%" >
        <td align="right">
            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
        </td>
       
    </tr>
    </table>
    </td>
    </tr>
    
   
    <tr id="trReassignBranch" runat="server">
        <td class="SubmitCell" align="left">
       &nbsp
       &nbsp
       &nbsp
            <asp:Label ID="Label2" Text="Merge To" CssClass="FieldName" runat="server"></asp:Label> 
        </td>
      
        
        <td>
        &nbsp
        &nbsp
            <asp:DropDownList ID="ddlAdvisorBranchList" Width="10.4%" runat="server" CssClass="cmbField" 
                 >
            </asp:DropDownList>
            <%--<span id="spanAdvisorBranch" class="spnRequiredField" runat="server">*</span>--%>
            
        </td>
        <td>
        &nbsp
        &nbsp
        <asp:Label ID="lblerror" Text="No Folios to merge" CssClass="rfvPCG" Visible="false" runat="server"></asp:Label> 
        </td>
    </tr>

   <p>
   &nbsp
   &nbsp
   &nbsp
   &nbsp
    &nbsp
   &nbsp
   &nbsp
   &nbsp
    &nbsp
     &nbsp  
  <asp:Button ID="btnmerge" CssClass="PCGMediumButton" Width="10%" runat="server" OnClick="btnEdit_Click" Text="Merge" />
</p>
     
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:Button ID="btnCustomerSearch" runat="server" Text="" onclick="btnCustomerSearch_Click" BorderStyle="None" BackColor="Transparent"/> 
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnBranchFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />