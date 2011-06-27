<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBranchDetails.ascx.cs"
    Inherits="WealthERP.Advisor.ViewBranchDetails" %>


<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this record?');
        if (bool) {
            document.getElementById("ctrl_ViewBranchDetails_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewBranchDetails_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewBranchDetails_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewBranchDetails_hiddenDelete").click();
            return true;
        }
    }
   
</script>
<style type="text/css">
.txtGridMediumField
{
    font-family: Verdana,Tahoma;
    font-weight: normal;
    font-size: x-small;
    color: #16518A;
    width: 80px;
}
</style>

<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Branch Details"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="LinkButtons" OnClick="LinkButton1_Click">Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 30%">
            <asp:Label ID="lblBCode" runat="server" CssClass="FieldName" Text="Branch/Associate Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBranchCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBName" runat="server" CssClass="FieldName" Text="Branch/Associate Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBranchName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBType" runat="server" CssClass="FieldName" Text="Branch/Associate Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBranchType" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr id="trAssocCategory" runat="server">
        <td class="leftField">
            <asp:Label ID="lblACategory" runat="server" CssClass="FieldName" Text="Associate Category:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblAssociateCategory" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label5" runat="server" CssClass="HeaderTextSmall" Text="Branch Address"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine1" runat="server" CssClass="FieldName" Text="Line 1 (House No/ Building):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLineone" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine2" runat="server" CssClass="FieldName" Text="Line 2 (Street):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLinetwo" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblLine3" runat="server" CssClass="FieldName" Text="Line 3 (Area):"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblLineThree" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPinCode" runat="server" CssClass="FieldName" Text="Pin Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPin" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBState" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblState" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBCountry" runat="server" CssClass="FieldName" Text="Country:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCountry" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblHeadName" runat="server" CssClass="FieldName" Text="Name of the Branch Head:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblHead" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>   
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPhoneNumber" runat="server" CssClass="FieldName" Text="Telephone Number1:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhone1" runat="server" Text="Label" CssClass="Field"></asp:Label>
            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBPH2" runat="server" CssClass="FieldName" Text="Telephone Number2:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPhone2" runat="server" Text="Label" CssClass="Field"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBFax" runat="server" CssClass="FieldName" Text="Fax:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblFax" runat="server" Text="Label" CssClass="Field"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblMail" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="1">
            &nbsp; &nbsp;
            <asp:GridView ID="gvTerminalList" runat="server" AllowSorting="True" AutoGenerateColumns="True"
                CssClass="GridViewStyle" ShowFooter="true">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle " />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <%--<Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBx" runat="server" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnDeleteSelected" CssClass="FieldName" OnClick="btnDeleteSelected_Click"
                                runat="server" Text="Delete" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Terminal Id" HeaderText="Terminal Id" SortExpression="Terminal Id" />
                </Columns>--%>
            </asp:GridView>
        </td>
    </tr>
    <tr>
    <td>
    &nbsp;&nbsp;
    </td>
    </tr>
    <tr id="CommSharingStructureHdr" runat="server">
        <td colspan="4">
            <asp:Label ID="Label10" runat="server" CssClass="HeaderTextSmall" Text="Commision Sharing Structure"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2" >
            <asp:GridView ID="gvCommStructure" runat="server" AutoGenerateColumns="False" CssClass="GridViewStyle"
                ShowFooter="True" CellPadding="4">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="AssetGroup" HeaderText = "Asset Group" />
                    <asp:BoundField DataField="CommissionFee" HeaderText = "Commission Fee" />
                    <asp:BoundField DataField="RevenueUpperLimit" HeaderText = "Revenue Upper Limit" />
                    <asp:BoundField DataField="RevenueLowerLimit" HeaderText = "Revenue Lower Limit" />
                    <asp:BoundField DataField="StartDate" HeaderText = "Start Date" DataFormatString = "{0:dd/MM/yyyy}"/>
                    <asp:BoundField DataField="EndDate" HeaderText = "End Date" DataFormatString = "{0:dd/MM/yyyy}"/>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
        </td>
    </tr>
    <tr>
        <td class="SubmitCell" colspan="2">
            &nbsp;
        </td>
    </tr>
</table>
