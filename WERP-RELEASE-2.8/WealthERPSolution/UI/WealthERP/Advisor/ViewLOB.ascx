<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewLOB.ascx.cs" Inherits="WealthERP.Advisor.ViewLOB" %>

<script>
    function checkIsDelete(selectObj) {
        if (selectObj.selectedIndex == 3) //3- Delete Option
        {
            if (confirm('Are you sure you want to delete ?')) {
                this.document.forms[0].submit();
            }
            else
                selectObj.selectedIndex = 0
        }
        else {
            this.document.forms[0].submit();
        }
    }
</script>
<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="LOB"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center" >
           No Records found.....
 </div>
<table class="TableBackground" style="width:100%">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="LOB List"></asp:Label>
        </td>
    </tr>--%>
    
    <tr>
        <%--<td>
           <asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label>
        </td>--%>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvLOBList" runat="server" AllowSorting="False" AutoGenerateColumns="False"
                DataKeyNames="LOBId"  OnRowEditing="gvLOBList_RowEditing" ShowFooter="true"
                AllowPaging="true" CssClass="GridViewStyle" OnPageIndexChanging="gvLOBList_PageIndexChanging">
                <FooterStyle CssClass="FieldName" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle " />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlMenu" AutoPostBack="true"  onchange="checkIsDelete(this)" runat="server" CssClass="GridViewCmbField" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged">
                                <asp:ListItem>Select </asp:ListItem>
                                <asp:ListItem Text="Edit" Value="Edit">Edit</asp:ListItem>
                                <asp:ListItem Text="View" Value="View">View</asp:ListItem>
                                <asp:ListItem Text="Delete" Value="Delete">Delete</asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Broker Name" HeaderText="Broker Name"  />
                    <asp:BoundField DataField="Business Type" HeaderText="Business Type"  />
                    <asp:BoundField DataField="Identifier" HeaderText="Identifier" />
                    <asp:BoundField DataField="Identifier Type" HeaderText="Identifier Type"  />
                    
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>

