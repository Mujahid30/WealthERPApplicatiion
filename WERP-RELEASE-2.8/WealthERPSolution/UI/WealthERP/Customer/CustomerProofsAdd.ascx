<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerProofsAdd.ascx.cs"
    Inherits="WealthERP.Customer.CustomerProofsAdd" %>

<table style="width: 100%;" class="TableBackground">
    <tr>
        <td class="HeaderCell" colspan="3">
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Customer Proofs" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <%-- <tr>
        <td colspan="2">
            <asp:CheckBox ID="chkKYCFlag" runat="server" Text="Please Check if you are KYC Compliant"
                OnCheckedChanged="chkKYCFlag_CheckedChanged" AutoPostBack="true" />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Show" CssClass="PCGButton" />
        </td>
        <td>
        </td>
    </tr>--%>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Address Proofs :" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="3" class="SubmitCell">
            <asp:GridView ID="gvAddressProofList" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                CellPadding="4" DataKeyNames="ProofCode" AllowSorting="True" CssClass="GridViewStyle">
                <FooterStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Proof Name" HeaderText="Proof Name" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label3" runat="server" Font-Bold="True" Text="ID Proofs :" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="3" class="SubmitCell">
            <asp:GridView ID="gvIDProofList" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowFooter="true"
                DataKeyNames="ProofCode" Font-Size="Small" CssClass="GridViewStyle">
                <FooterStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Proof Name" HeaderText="Proof Name" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Other Proofs :" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="3" class="SubmitCell">
            <asp:GridView ID="gvOtherProofList" runat="server" AutoGenerateColumns="False" CellPadding="4" ShowFooter="true"
                ForeColor="#333333" DataKeyNames="ProofCode" AllowSorting="True" CssClass="GridViewStyle">
                <FooterStyle Font-Bold="True" ForeColor="White" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Proof Name" HeaderText="Proof Name" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerProofsAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerProofsAdd_btnSubmit', 'S');"
                OnClick="btnSubmit_Click" />
        </td>
    </tr>
</table>
