<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioGeneralInsuranceAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioGeneralInsuranceAccountAdd" %>

<script type="text/javascript">
    function ValidateNominee() {
        debugger;
        var count = 0;
        var gridViewID = "<%=gvNominees.ClientID %>";
        var gridView = document.getElementById(gridViewID);
        var gridViewControls = gridView.getElementsByTagName("input");
        for (i = 0; i < gridViewControls.length; i++) {
            // if this input type is checkbox
            if (gridViewControls[i].type == "checkbox") {
                if (gridViewControls[i].checked == true) {
                    count = count + 1;
                }
            }
        }
        if (count > 1) {
            alert('Sorry, You can select only one nominee !');
            return false;
        }
        else {
            return true;
        }
    }
</script>

<table style="width: 100%;">
    <tr>
        <td colspan="6">
            <asp:Label ID="lblGeneralInsuranceEntryHeader" class="HeaderTextBig" runat="server"
                Text="General Insurance Add Account Screen"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are compulsory</label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblAssetCategory" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
            <asp:DropDownList ID="ddlAssetCategory" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlAssetCategory_SelectedIndexChanged"
                AutoPostBack="true">
                <%--<asp:ListItem Text="Select Asset Category" Value="Select Asset Category"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
        <td colspan="2" align="left">
            <asp:Label ID="lblAssetSubCategory" runat="server" Text="Asset Sub Category:" CssClass="FieldName"></asp:Label>
            <asp:DropDownList ID="ddlAssetSubCategory" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select Asset Sub-Category" Value="Select Asset Sub-Category"></asp:ListItem>
            </asp:DropDownList>
            <span id="span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cv_ddlAssetSubCategory" runat="server" ErrorMessage="<br />Please select Asset Sub-Category"
                ControlToValidate="ddlAssetSubCategory" Operator="NotEqual" ValueToCompare="Select Asset Sub-Category"
                Display="Dynamic" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblPolicyNumber" runat="server" Text=" Policy Number:" CssClass="FieldName"></asp:Label>
            <asp:TextBox ID="txtPolicyNumber" runat="server" CssClass="txtField">
            </asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfv_txtPolicyNumber" ControlToValidate="txtPolicyNumber"
                ErrorMessage="<br />Please enter a Policy Number" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr>
&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trNominees" visible="false" runat="server">
        <td>
            <asp:Label ID="lblNominees" Text="Nominees" runat="server" CssClass="FieldName"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" DataKeyNames="MemberCustomerId, AssociationId"
                AllowSorting="False" CssClass="GridViewStyle">
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkId" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                    <asp:BoundField DataField="Relationship" HeaderText="Relationship" SortExpression="Relationship" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGeneralInsuranceAccountAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGeneralInsuranceAccountAdd_btnSubmit', 'S');"
                CausesValidation="true" OnClientClick="if(Page_ClientValidate()){return ValidateNominee()};"
                Text="Submit" OnClick="btnSubmit_Click" Style="height: 26px" />
        </td>
    </tr>
</table>
