<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerAccountAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        if ($("#<%=txtAccountNumber.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }

        $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckInsuranceNoAvailabilityOnAdd",
            data: "{ 'InsuranceNo': '" + $("#<%=txtAccountNumber.ClientID %>").val() + "','AdviserId': '" + $("#<%=HdnAdviserId.ClientID %>").val() + "'}",

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

<script language="javascript" type="text/javascript">
    function checkDate(sender, args) {
        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
</script>

<script type="text/javascript">
    function ChkForMainPortFolio(source, args) {

        var hdnIsCustomerLogin = document.getElementById('ctrl_CustomerAccountAdd_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_CustomerAccountAdd_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {

            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }

    }    
</script>

<asp:ScriptManager ID="scrptMgr" runat="server" EnableScriptLocalization="true">
</asp:ScriptManager>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <table style="width: 100%;">
            <tr>
                <td colspan="5">
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr id="lblLifeInsurance" runat="server">
                                <td align="left">
                                    <asp:Label ID="lblCustAccountHeader" class="HeaderTextBig" runat="server"></asp:Label>
                                </td>
                                <td align="right" style="padding-bottom: 2px;">
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <asp:CustomValidator ID="cvCheckForManageOrUnManage" runat="server" ValidationGroup="AccountAdd"
                        Display="Dynamic" ClientValidationFunction="ChkForMainPortFolio" CssClass="revPCG"
                        ErrorMessage="CustomValidator">Permisssion denied for Manage Portfolio !!</asp:CustomValidator>
                </td>
            </tr>
            <tr id="trAssetGroup" runat="server">
                <td>
                    <asp:Label ID="lblAssetGroup" runat="server" CssClass="FieldName" Text="Asset Group"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAssetGroupName" runat="server" CssClass="FieldName" Text="Asset Group"></asp:Label>
                </td>
            </tr>
            <tr id="trCategory" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Please select a category"
                        ControlToValidate="ddlCategory" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                        ValidationGroup="AccountAdd" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSubcategory" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" Text="SubCategory:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please select a sub-category"
                        ControlToValidate="ddlSubCategory" Operator="NotEqual" ValueToCompare="Select"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trAccountNum" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccountNum" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox onblur="return checkInsuranceNoAvailability()" ID="txtAccountNumber"
                        runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccountNumber"
                        ValidationGroup="AccountAdd" ErrorMessage="<br />Please enter a Number" Display="Dynamic"
                        runat="server" CssClass="rfvPCG" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>
               
                    <span id="Span7" class="spnRequiredField">*</span><span id="spnLoginStatus"></span>
                </td>
            </tr>
            <%--Specialy Used for Add Cash and saving--%>
            <tr id="trAccountNoddl" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccountnumddl" runat="server" CssClass="FieldName" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAccountNum" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAccountNum_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="cvAccountNo" runat="server" ErrorMessage="<br />Please select an account number"
                        ControlToValidate="ddlAccountNum" Operator="NotEqual" ValueToCompare="Select"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trAccountSource" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccountSource" runat="server" CssClass="FieldName" Text="Account Source:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountSource" runat="server" CssClass="txtField"></asp:TextBox>
                    <%--  <span id="Span5" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAccountSource"
                        ErrorMessage="Please enter an Account Source" Display="Dynamic" runat="server"
                        CssClass="rfvPCG" SetFocusOnError="true">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <%--Specialy Used for Add Cash and saving--%>
            <tr id="trBankName" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblBankName" runat="server" CssClass="FieldName" Text=""></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span5" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Please select a BankName"
                        ControlToValidate="ddlBankName" Operator="NotEqual" ValueToCompare="Select" Display="Dynamic"
                        CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trJoingHolding" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
                </td>
                <td>
                    <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbFielde" GroupName="rbtnJointHolding"
                        Text="Yes" OnCheckedChanged="rbtnYes_CheckedChanged" AutoPostBack="true" />
                    <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbFielde" GroupName="rbtnJointHolding"
                        Text="No" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" Checked="true" />
                </td>
            </tr>
            <tr id="trModeOfHolding" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select a Mode of Holding"
                        ControlToValidate="ddlModeOfHolding" ValidationGroup="AccountAdd" Operator="NotEqual" ValueToCompare="Select"
                        Display="Dynamic" CssClass="cvPCG" SetFocusOnError="true"></asp:CompareValidator>
                </td>
            </tr>
            <%--<tr id="trAccountOpeningDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblAccOpeningDate" runat="server" CssClass="FieldName" Text="Registration date:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountOpeningDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <cc1:CalendarExtender ID="txtAccountOpeningDate_CalendarExtender" runat="server"
                        TargetControlID="txtAccountOpeningDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="txtAccountOpeningDate_TextBoxWatermarkExtender"
                        runat="server" TargetControlID="txtAccountOpeningDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <asp:RequiredFieldValidator ID="rfvAccountOpeningDate" ControlToValidate="txtAccountOpeningDate"
                        ErrorMessage="<br>Please select an Account Opening Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="<br>Please enter a valid date"
                        Type="Date" ControlToValidate="txtAccountOpeningDate" Operator="DataTypeCheck"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>--%>
            <tr id="trJoinHolders" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblJointHolders" runat="server" CssClass="HeaderTextSmall" Text="Joint Holders"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trJointHolderGrid" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvJointHoldersList" runat="server" AutoGenerateColumns="False"
                        Width="60%" CellPadding="4" DataKeyNames="MemberCustomerId, AssociationId" AllowSorting="True"
                        ShowFooter="true" CssClass="GridViewStyle">
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
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trNoJointHolders" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoJointHolders" runat="server" Text="You have no Joint Holder"
                        CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr id="trNomineeCaption" runat="server" visible="true">
                <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Nominees
                    </div>
                </td>
            </tr>
            <%-- <tr id="trNomineeCaption" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblNominees" runat="server" CssClass="HeaderTextSmall" Text="Nominees"></asp:Label>
                    <hr />
                </td>
            </tr>--%>
            <tr id="trNominees" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvNominees" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Width="60%" ShowFooter="true" DataKeyNames="MemberCustomerId, AssociationId"
                        AllowSorting="True" CssClass="GridViewStyle">
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
                                    <asp:CheckBox ID="chkId0" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="Relationship" HeaderText="Relationship" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr id="trNoNominee" runat="server" visible="false">
                <td class="Message" colspan="2">
                    <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
        <asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>
<table style="width: 100%;">
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="trError" runat="server" visible="false">
        <td colspan="2" class="SubmitCell">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="AccountAdd" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="HdnAdviserId" runat="server" />
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
