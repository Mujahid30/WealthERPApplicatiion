<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioGeneralInsuranceAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioGeneralInsuranceAccountAdd" %>


<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script>
    function checkInsuranceNoAvailability() {
        if ($("#<%=txtPolicyNumber.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }
        
        $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckGenInsuranceNoAvailabilityOnAdd",
            data: "{ 'InsuranceNo': '" + $("#<%=txtPolicyNumber.ClientID %>").val() + "','AdviserId': '" + $("#<%=HdnAdviserId.ClientID %>").val() + "'}",

            error: function(xhr, status, error) {
                //                alert("Please select AMC!");
            },
            success: function(msg) {

                if (msg.d) {

                    $("#<%= hidValidCheck.ClientID %>").val("1");
                    $("#spnLoginStatus").html("");
                    document.getElementById("<%= btnSubmit.ClientID %>").disabled = false;
                }
                else {

                    $("#<%= hidValidCheck.ClientID %>").val("0");
                    $("#spnLoginStatus").removeClass();
                    alert("Policy number is already Exists");
                    document.getElementById("<%= btnSubmit.ClientID %>").disabled = true;
                    return false;
                }

            }
        });
    }
</script>


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

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            General Insurance Add Account Screen
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%;">
    <tr>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblAssetCategory" runat="server" Text="Asset Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 50%">
            <asp:DropDownList ID="ddlAssetCategory" runat="server" Style="width: 35%" CssClass="cmbField"
                OnSelectedIndexChanged="ddlAssetCategory_SelectedIndexChanged" AutoPostBack="true">
                <%--<asp:ListItem Text="Select Asset Category" Value="Select Asset Category"></asp:ListItem>--%>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblAssetSubCategory" runat="server" Text="Asset Sub Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 50%">
            <asp:DropDownList ID="ddlAssetSubCategory" Style="width: 35%" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select Asset Sub-Category" Value="Select Asset Sub-Category"></asp:ListItem>
            </asp:DropDownList>
            <span id="span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cv_ddlAssetSubCategory" runat="server" ErrorMessage="<br />Please select Asset Sub-Category"
                ControlToValidate="ddlAssetSubCategory" Operator="NotEqual" ValueToCompare="Select Asset Sub-Category"
                Display="Dynamic" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right" style="width: 15%">
            <asp:Label ID="lblPolicyNumber" runat="server" Text=" Policy Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" style="width: 50%">
            <asp:TextBox ID="txtPolicyNumber" onblur="return checkInsuranceNoAvailability()"
                runat="server" Style="width: 50%" CssClass="txtField">
            </asp:TextBox>
             <span id="Span7" class="spnRequiredField">
                *</span><span id="spnLoginStatus"></span>
            <asp:RequiredFieldValidator ID="rfv_txtPolicyNumber" ControlToValidate="txtPolicyNumber"
                ErrorMessage="<br />Please enter a Policy Number" Display="Dynamic" runat="server"
                CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr id="trNominees" runat="server">
        <td colspan="4">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Nominees
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" DataKeyNames="MemberCustomerId, AssociationId"
                AllowSorting="False" CssClass="GridViewStyle" Width="60%">
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
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioGeneralInsuranceAccountAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioGeneralInsuranceAccountAdd_btnSubmit', 'S');"
                CausesValidation="true" OnClientClick="if(Page_ClientValidate()){return ValidateNominee()};"
                Text="Submit" OnClick="btnSubmit_Click" Style="height: 26px" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="HdnAdviserId" runat="server" />
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
