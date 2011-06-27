<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioPersonalEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioPersonalEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<script type="text/javascript">
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

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="up1" runat="server">
    <ContentTemplate>
        <table style="width: 100%;" class="TableBackground">
          <tr>
                <td colspan="3" class="HeaderCell">
                    <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Add Personal Asset"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td colspan="3" class="HeaderCell">
                    <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="View Personal Asset"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td colspan="3" class="HeaderCell">
                    <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Edit Personal Asset"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="3" class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with a ' * ' are compulsory</label>
                </td>
            </tr>
            <tr id="trEditLink" runat="server">
                <td colspan="2">
                    <asp:LinkButton ID="lnkEdit" Text="Edit" CssClass="LinkButtons" runat="server" OnClick="lnkEdit_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:LinkButton ID="lnkBack" Text="Back" CssClass="LinkButtons" runat="server" OnClick="lnkBtnBack_Click"></asp:LinkButton>
                </td>
            </tr>
            <tr id="trPortfolio" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                </td>
                <td colspan="3" class="rightField">
                    <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trCategory" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="ddlCategory"
                        ErrorMessage="Please select a Category" Operator="NotEqual" ValueToCompare="Select a Category"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trSubCategory" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblSubCategory" runat="server" CssClass="FieldName" Text="Sub Category:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlSubCategory"
                        ErrorMessage="Please select a Sub-Category" Operator="NotEqual" ValueToCompare="Select a Sub-Category"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trAssetIdentifier" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="Asset Identifier :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtName" 
                        ErrorMessage="Please enter the Name" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trQuantity" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblQuantity" runat="server" CssClass="FieldName" Text="Quantity:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtField" MaxLength="5"></asp:TextBox>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtQuantity"
                        ErrorMessage="Please enter the Quantity" Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtQuantity" CssClass="cvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Enter a numeric value" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trPurchaseDate" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurchaseDate" runat="server" CssClass="FieldName" Text="Purchase Date:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchaseDate" runat="server" CssClass="txtField">
                    </asp:TextBox>
                     <span id="Span23" class="spnRequiredField">*</span>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPurchaseDate"
                        ErrorMessage="Please enter the Purchase Date" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                         </asp:RequiredFieldValidator>
                    <cc1:CalendarExtender ID="CalendarExtender_txtPurchaseDate" runat="server" TargetControlID="txtPurchaseDate"
                        Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
                    </cc1:CalendarExtender>
                    <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender_txtPurchaseDate" runat="server"
                        TargetControlID="txtPurchaseDate" WatermarkText="dd/mm/yyyy">
                    </cc1:TextBoxWatermarkExtender>
                    <%-- <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtPurchaseDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr id="trPurchasePrice" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurchasePrice" runat="server" CssClass="FieldName" Text="Purchase Price:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchasePrice" runat="server" CssClass="txtField" MaxLength="18"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtPurchasePrice"
                        ErrorMessage="Please enter the Purchase Price" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtPurchasePrice" CssClass="cvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trPurchaseValue" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblPurchaseValue" runat="server" CssClass="FieldName" Text="Purchase Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtPurchaseValue" runat="server" CssClass="txtField"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtPurchaseValue"
                        ErrorMessage="Please enter the Purchase Value" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtPurchaseValue" CssClass="cvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trCurrentPrice" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCurrentPrice" runat="server" CssClass="FieldName" Text="Current Price:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCurrentPrice" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtCurrentPrice" CssClass="cvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trCurrentValue" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCurrentValue" runat="server" CssClass="FieldName" Text="Current Value:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtCurrentValue" CssClass="cvPCG"
                        Display="Dynamic" runat="server" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,5}))?$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table style="width: 100%;">
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" Text="Submit" OnClick="btnSubmit_Click"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioPersonalEntry_btnSubmit');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioPersonalEntry_btnSubmit');" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" OnClick="btnUpdate_Click"
                        onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioPersonalEntry_btnUpdate');"
                        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioPersonalEntry_btnUpdate');" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<%--<asp:Panel ID="pnlViewEdit" runat="server" Visible="false">
    <%--<asp:UpdatePanel ID="up2" runat="server">
        <ContentTemplate>--%>
<%-- <table style="width: 100%;">
        <tr id="trEdit" runat="server" visible="true">
            <td colspan="2" class="EditCell">
                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewName" runat="server" CssClass="FieldName" Text="Name:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewName" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span1" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" ErrorMessage="Please enter the Policy Particulars"
                    Display="Dynamic" runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlEditViewCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                    OnSelectedIndexChanged="ddlEditViewCategory_SelectedIndexChanged">
                </asp:DropDownList>
                <span id="Span2" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlEditViewCategory"
                    ErrorMessage="Please select a Category" Operator="NotEqual" ValueToCompare="Select a Category"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr id="trEditViewSubCategory" runat="server">
            <td class="leftField">
                <asp:Label ID="lblEditViewSubCategory" runat="server" CssClass="FieldName" Text="Sub Category:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:DropDownList ID="ddlEditViewSubCategory" runat="server" CssClass="cmbField">
                </asp:DropDownList>
                <span id="Span3" class="spnRequiredField">*</span>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlEditViewSubCategory"
                    ErrorMessage="Please select a Sub-Category" Operator="NotEqual" ValueToCompare="Select a Sub-Category"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewQuantity" runat="server" CssClass="FieldName" Text="Quantity:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewQuantity" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span4" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtName"
                    ErrorMessage="Please enter the Policy Particulars" Display="Dynamic" runat="server"
                    CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Please enter an integer value"
                    Type="Integer" ControlToValidate="txtEditViewQuantity" Operator="DataTypeCheck"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewPurchaseDate" runat="server" CssClass="FieldName" Text="Purchase Date:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewPurchaseDate" runat="server" CssClass="txtField">
                </asp:TextBox>
                <cc1:CalendarExtender ID="txtEditViewPurchaseDate_CalendarExtender" runat="server"
                    TargetControlID="txtEditViewPurchaseDate">
                </cc1:CalendarExtender>
                <cc1:TextBoxWatermarkExtender ID="txtEditViewPurchaseDate_TextBoxWatermarkExtender"
                    runat="server" TargetControlID="txtEditViewPurchaseDate" WatermarkText="mm/dd/yy">
                </cc1:TextBoxWatermarkExtender>
                <span id="Span5" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="rfvEditViewPurchaseDate" ControlToValidate="txtEditViewPurchaseDate"
                    ErrorMessage="Please select a Purchase Date" Display="Dynamic" runat="server"
                    CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="The date format should be mm/dd/yyyy"
                    Type="Date" ControlToValidate="txtEditViewPurchaseDate" Operator="DataTypeCheck"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewPurchasePrice" runat="server" CssClass="FieldName" Text="Purchase Price:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewPurchasePrice" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span6" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtEditViewPurchasePrice"
                    ErrorMessage="Please enter the Purchase Price" Display="Dynamic" runat="server"
                    CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Please enter a numeric value"
                    Type="Double" ControlToValidate="txtEditViewPurchasePrice" Operator="DataTypeCheck"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewPurchaseValue" runat="server" CssClass="FieldName" Text="Purchase Value:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewPurchaseValue" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span7" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEditViewPurchaseValue"
                    ErrorMessage="Please enter the Purchase Value" Display="Dynamic" runat="server"
                    CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please enter a numeric value"
                    Type="Double" ControlToValidate="txtEditViewPurchaseValue" Operator="DataTypeCheck"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewCurrentPrice" runat="server" CssClass="FieldName" Text="Current Price:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewCurrentPrice" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span8" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtEditViewCurrentPrice"
                    ErrorMessage="Please enter the Current Price" Display="Dynamic" runat="server"
                    CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please enter a numeric value"
                    Type="Double" ControlToValidate="txtEditViewCurrentPrice" Operator="DataTypeCheck"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblEditViewCurrentValue" runat="server" CssClass="FieldName" Text="Current Value:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtEditViewCurrentValue" runat="server" CssClass="txtField"></asp:TextBox>
                <span id="Span9" class="spnRequiredField">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtEditViewCurrentValue"
                    ErrorMessage="Please enter the Current Value" Display="Dynamic" runat="server"
                    CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Please enter a numeric value"
                    Type="Double" ControlToValidate="txtEditViewCurrentValue" Operator="DataTypeCheck"
                    CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>--%>
<%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
<%--  <table style="width: 100%;">
        <tr id="trButton" runat="server" visible="false">
            <td class="SubmitCell" colspan="2">
                <asp:Button ID="btnEditViewSubmit" runat="server" CssClass="PCGButton" Text="Update"
                    OnClick="btnEditViewSubmit_Click" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_PortfolioPersonalEntry_btnEditViewSubmit');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_PortfolioPersonalEntry_btnEditViewSubmit');" />
            </td>
        </tr>
    </table>--%>
<%--</asp:Panel>--%>
