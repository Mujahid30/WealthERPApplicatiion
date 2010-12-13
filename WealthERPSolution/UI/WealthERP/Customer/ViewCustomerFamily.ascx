<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerFamily.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerFamily" %>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            document.getElementById("ctrl_ViewCustomerFamily_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewCustomerFamily_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerFamily_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewCustomerFamily_hiddenDelete").click();
            return true;
        }
    }

    function showassocation() {

        var bool = window.confirm('Customer is associated to himself/herself, cannot be dissociated');
        if (bool) {
            document.getElementById("ctrl_ViewCustomerFamily_hdnassociation").value = 1;
            document.getElementById("ctrl_ViewCustomerFamily_hiddenassociationfound").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerFamily_hdnassociation").value = 0;
            document.getElementById("ctrl_ViewCustomerFamily_hiddenassociationfound").click();
            return true;
        }
    }
   
</script>

<table class="TableBackground" style="width: 100%;">
    <tr>
        <td colspan="3">
            <asp:Label ID="lblHeader" runat="server" Text="Customer Associations" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found.."></asp:Label>
            <asp:GridView ID="gvCustomerFamily" runat="server" CellPadding="4" HorizontalAlign="Center"
                CssClass="GridViewStyle" AutoGenerateColumns="False" DataKeyNames="AssociationId"
                ShowFooter="True" OnRowCommand="gvCustomerFamily_RowCommand" 
                AllowPaging="true" onpageindexchanging="gvCustomerFamily_PageIndexChanging" OnRowDataBound="gvCustomerFamily_RowDataBound">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                  <asp:TemplateField HeaderText="Select">
                     <HeaderTemplate>
                            <asp:Label ID="LblSelect" runat="server" Text=""></asp:Label>
                            <br />
                            <%--<asp:Button ID="lnkSelectAll" Text="All" runat="server"  OnClientClick="return CheckAll();" />--%>
                          <%-- <input id="chkBoxAll" class="CheckField" name="CheckAllCustomer" value="Customer" type="checkbox" onclick="checkAllBoxes('CurrentPage')" />--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parent Customer" ShowHeader="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustNameHeader" runat="server" Text="Parent Customer"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" Text='<%# hdnNameFilter.Value %>' runat="server"
                                CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_ViewCustomerFamily_btnNameSearch');"  />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="EditDetails"
                                Text='<%# Eval("ParentCustomer") %>' CommandArgument='<%# Eval("AssociationId") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MemberCustomer" HeaderText="Member Customer" />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
        <tr>
    <td>
    
    </td>
        <td align="left">        
        <asp:Button ID="btnDissociate" runat="server"  
                 CssClass="PCGMediumButton" OnClick="Deactive_Click" 
                Text="Disassociate"  />            
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>

    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
<asp:Button ID="btnNameSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent"
    OnClick="btnNameSearch_Click1" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
