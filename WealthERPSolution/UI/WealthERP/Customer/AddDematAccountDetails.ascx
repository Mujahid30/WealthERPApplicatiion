<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddDematAccountDetails.ascx.cs"
    Inherits="WealthERP.Customer.AddDematAccountDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolKit" %>

<script language="javascript" type="text/javascript">
    function addbranches() {

        ///var isSuccess = false;
        var Source = document.getElementById("<%= lstAvailableTrades.ClientID %>");
        var Target = document.getElementById("<%= lstAssocaitedTrades.ClientID %>");

        if ((Source != null) && (Target != null) && Source.selectedIndex >= 0) {
            var newOption = new Option();
            newOption.text = Source.options[Source.selectedIndex].text;
            newOption.value = Source.options[Source.selectedIndex].value;
            Target.options[Target.length] = newOption;
            Source.remove(Source.selectedIndex);

        }


        return false;

    }


    function deletebranches() {
        var Source = document.getElementById("<%= lstAssocaitedTrades.ClientID %>");
        var Target = document.getElementById("<%= lstAvailableTrades.ClientID %>");

        if ((Source != null) && (Target != null) && Source.selectedIndex >= 0) {

            var newOption = new Option();
            newOption.text = Source.options[Source.selectedIndex].text;
            newOption.value = Source.options[Source.selectedIndex].value;
            Target.options[Target.length] = newOption;
            Source.remove(Source.selectedIndex);
        }
        return false;
    }

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
    function GetSelectedBranches() {
        var Target = document.getElementById("<%= lstAssocaitedTrades.ClientID %>");
        var selectedBranches = document.getElementById("<%= hdnSelectedBranches.ClientID %>");
        for (var i = 0; i < Target.length; i++) {
            selectedBranches.value += Target.options[i].value + ",";
        }

        var ddlmodeofholdingTarget = document.getElementById("<%= ddlModeOfHolding.ClientID %>");
        var ddlModeOfHoldingSelectedMode = document.getElementById("<%= hdnSelectedModeOfHolding.ClientID %>");
        ddlModeOfHoldingSelectedMode.value = ddlmodeofholdingTarget.options[ddlmodeofholdingTarget.selectedIndex].value;
    }
    
    
</script>

<script runat="server">
    protected void RadioButton_CheckChanged(object sender, EventArgs e)
    {
        if (rbtnYes.Checked)
        {
            ddlModeOfHolding.SelectedIndex = 8;
            gvPickJointHolder.Visible = true;
            lblPickJointHolder.Visible = true;
            hrPickJointHolder.Visible = true;
            ddlModeOfHolding.Enabled = true;

        }
        else
        {
            ddlModeOfHolding.SelectedIndex = 7;
            ddlModeOfHolding.Enabled = false;
            gvPickJointHolder.Visible = false;            
            lblPickJointHolder.Visible = false;
            hrPickJointHolder.Visible = false;
        }
    }
    protected void rbtnNo_Load(object sender, EventArgs e)
    {
        if (rbtnNo.Checked)
        {
            ddlModeOfHolding.Enabled = false;
            
        }
    }
   
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table class="TableBackground" style="width: 100%;">
    <tr>
        <td colspan="6">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="lbtnBackButton" runat="server" OnClick="lbtnBackButton_Click"
                Visible="False" CssClass="LinkButtons">Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblDpName" runat="server" Text="DP Name" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDpName" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDpName"
                ErrorMessage="Name Required" CssClass="cvPCG"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            &nbsp;<asp:Label ID="lblDPId" runat="server" Text="DP Id" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDPId" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Required Dp Id"
                ControlToValidate="txtDpId" CssClass="cvPCG"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblDpClientId" runat="server" Text="Dp ClientId" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtDpClientId" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDpClientId"
                ErrorMessage="Client Id Required" CssClass="cvPCG"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
            &nbsp;<asp:Label ID="lblAccountOpeningDate" runat="server" Text="Account Opening Date"
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <!-- calAccountOpeningDate -->
            <asp:TextBox ID="txtAccountOpeningDate"  CssClass="txtField" runat="server"></asp:TextBox>
            <ajaxToolKit:CalendarExtender runat="server" Format="dd/MM/yyyy" TargetControlID="txtAccountOpeningDate"
                ID="calExeAccountOpeningDate" Enabled="true" OnClientDateSelectionChanged="checkDate">
            </ajaxToolKit:CalendarExtender>
            <ajaxToolKit:TextBoxWatermarkExtender TargetControlID="txtAccountOpeningDate" WatermarkText="dd/mm/yyyy"
                runat="server" ID="wmtxtAccountOpeningDate">
            </ajaxToolKit:TextBoxWatermarkExtender>
            <%--<span id="Span7" class="spnRequiredField">*</span>--%>
        </td>
        <td>
            
            
       
        <%--<asp:CompareValidator id="cmpCompareValidatorToDate" 
                        ControlToValidate="txtAccountOpeningDate" Operator="LessThanEqual" Type="Date" CssClass="cvPCG"
                        runat="server" ErrorMessage="Date Can't be in future" Display="Dynamic" ></asp:CompareValidator>
                        --%>
                         <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtAccountOpeningDate"
                    Type="Date" Operator="DataTypeCheck" ErrorMessage="Please Enter a Valid Date" Display="Dynamic" CssClass="cvPCG"/>
   <%--
    
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                ControlToValidate="txtAccountOpeningDate" 
                ErrorMessage="Account Opening Date Required" CssClass="cvPCG"></asp:RequiredFieldValidator>--%>
            
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblIsHeldJointly" runat="server" Text="Is Held Jointly" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:Panel ID="Panel1" runat="server" Width="127px">
                <asp:RadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="IsHeldJointly"
                    CssClass="txtField" AutoPostBack="True" 
                    OnCheckedChanged="RadioButton_CheckChanged" />
                <asp:RadioButton ID="rbtnNo" runat="server" Text="No" GroupName="IsHeldJointly" CssClass="txtField"
                    AutoPostBack="True" OnCheckedChanged="RadioButton_CheckChanged" OnLoad="rbtnNo_Load"
                    Checked="True" />
            </asp:Panel>
        </td>
        <td>
            &nbsp;
        </td>
        <td align="right">
            &nbsp;<asp:Label ID="lblBeneficiaryAcctNbr" runat="server" Text="Beneficiary Acct No"
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtBeneficiaryAcctNbr" runat="server" CssClass="txtField"></asp:TextBox>
            <%--<span id="Span4" class="spnRequiredField">*  </td>--%>
        <td>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Beneficiary Account Number Required"
                ControlToValidate="txtBeneficiaryAcctNbr" CssClass="cvPCG"></asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr>
        <td align="right">
            &nbsp;<asp:Label ID="lblModeOfHolding" runat="server" Text="Mode Of Holding" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlModeOfHolding" runat="server" AutoPostBack="True" OnPreRender="ddlModeOfHolding_PreRender"
                OnSelectedIndexChanged="ddlModeOfHolding_SelectedIndexChanged" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    &nbsp;
    <tr>
        <td colspan="6">
            <hr id="hrPickJointHolder" runat="server" />
        </td>
    </tr>
    <tr>
        <td >
            &nbsp;<asp:Label ID="lblPickJointHolder" runat="server" Text="Pick Joint Holder"
                CssClass="FieldName"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td i colspan="2">
            <asp:GridView ID="gvPickJointHolder" runat="server" DataKeyNames="CA_AssociationId"
                AutoGenerateColumns="False" CssClass="GridViewStyle">
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="PJHCheckBox" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="PJHCheckBox" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="AssociateName" />
                    <asp:BoundField HeaderText="Relationship" DataField="XR_Relationship" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    &nbsp;
    <tr>
        <td colspan="6">
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;<asp:Label ID="lblPickNominee" runat="server" Text="Pick Nominee" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="gvPickNominee" runat="server" DataKeyNames="CA_AssociationId" AutoGenerateColumns="False"
                CssClass="GridViewStyle">
                <RowStyle CssClass="RowStyle" />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="PNCheckBox" runat="server" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="PNCheckBox" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="AssociateName" />
                    <asp:BoundField HeaderText="Relationship" DataField="XR_Relationship" />
                </Columns>
                <FooterStyle CssClass="FooterStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table border="0">
                <tr>
                    <td colspan="1">
                        <asp:Label ID="lblAvailableTrade" runat="server" Text="Available Trade" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                    </td>
                    <td colspan="1">
                        <asp:Label ID="lblAssociatedTrade" runat="server" Text="Associated Trade" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lstAvailableTrades" runat="server" Height="139px" Width="107px">
                        </asp:ListBox>
                    </td>
                    <td>
                        <table border="1">
                            <tr>
                                <td>
                                    <input type="button" id="addBranch" runat="server" value=">>" onclick="addbranches();return false;" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="button" id="deleteBranch" runat="server" value="<<" onclick="deletebranches();return false;"
                                        style="height: 26px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:ListBox ID="lstAssocaitedTrades" runat="server" Height="134px" Width="114px">
                        </asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                            CssClass="PCGButton" OnClientClick="GetSelectedBranches()" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnSelectedBranches" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:HiddenField ID="hdnSelectedModeOfHolding" runat="server" />
        </td>
    </tr>
</table>
