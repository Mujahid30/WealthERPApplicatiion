<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PortfolioCashSavingsEntry.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.PortfolioCashSavingsEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

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

<link id="ThemeCSS" runat="server" rel="stylesheet" type="text/css" media="all" title="Aqua"
    href="../Scripts/Calender/skins/aqua/theme.css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="up1" runat="server">--%>
<contenttemplate>
<table style="width: 100%;">
    <tr>
        <td colspan="4" class="HeaderCell">
            <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="Cash And Savings"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" OnClick="lnkEdit_Click">Edit</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                OnClick="lnkBtnBack_Click"></asp:LinkButton>
        </td>
    </tr>
<%--    <tr>
        <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td colspan="3" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" runat="server"
                AutoPostBack="true" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>--%>
</table>
<table style="width: 100%;">
    <tr>
        <td class="leftField">
            <asp:Label ID="lbl0" runat="server" CssClass="FieldName" Text="Asset Group:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblAssetGroup" runat="server" CssClass="Field"></asp:Label>
        </td>
        <td class="leftField">
            <asp:Label ID="lblInstrumentCategory" runat="server" CssClass="FieldName" Text="Asset Category:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblInsCategory" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblTitle0" runat="server" CssClass="HeaderTextSmall" Text="Account Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr id="trAccountNo" runat="server">
        <td class="leftField">
            <asp:Label ID="lbl" runat="server" CssClass="FieldName" Text="Account No:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblAccId" runat="server" CssClass="Field" Text="Organization Name"></asp:Label>
            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
   <%-- <tr id="trAccOpenDate" runat="server">
        <td class="leftField">
            <asp:Label ID="lblOrgName2" runat="server" CssClass="FieldName" Text="Account Opening Date:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblOpeningDate" runat="server" CssClass="Field" Text="Organization Name"></asp:Label>
            <asp:TextBox ID="txtAccountOpeningDate" runat="server" CssClass="txtField"></asp:TextBox>
            <cc1:CalendarExtender ID="txtAccountOpeningDate_CalendarExtender" runat="server"
                Enabled="True" TargetControlID="txtAccountOpeningDate">
            </cc1:CalendarExtender>
        </td>
    </tr>--%>
    <tr id="trAccWithBank" runat="server">
        <td class="leftField">
            <asp:Label ID="lblOrgName4" runat="server" CssClass="FieldName" Text="Account With Bank:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblAccWith" runat="server" CssClass="Field"></asp:Label>
            <asp:TextBox ID="txtAccountWith" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblOrgName6" runat="server" CssClass="FieldName" Text="Mode Of Holding:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:Label ID="lblModeOfHolding" runat="server" CssClass="Field"></asp:Label>
            <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            &nbsp;
        </td>
    </tr>
    <tr id="trName" runat="server">
        <td class="leftField">
            <asp:Label ID="lblBorrowerName" runat="server" CssClass="FieldName" Text="Name Of the Borrower:"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtName" runat="server" CssClass="txtField"></asp:TextBox>
         <%--   <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtName" ErrorMessage="Please enter the Name Of the Borrower"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="CS">
            </asp:RequiredFieldValidator>--%>
        </td>
    </tr>
    <tr id="trDepositAmount" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblDepositAmount" runat="server" CssClass="FieldName" Text="Average A/C Balance:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtDepositAmount" runat="server" CssClass="txtField" 
                MaxLength="18"></asp:TextBox>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtDepositAmount"
                ErrorMessage="Please enter the Average A/C Balance" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="CS">
            </asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtDepositAmount"
                ValidationGroup="CS" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trDepositDate" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblDepositDate" runat="server" CssClass="FieldName" Text="Date Of Loan:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtDepositDate" runat="server" CssClass="txtField"></asp:TextBox>
           <%-- <span id="Span3" class="spnRequiredField">*</span>--%>
            <cc1:CalendarExtender ID="txtDepositDate_CalendarExtender" runat="server" Enabled="True"
                TargetControlID="txtDepositDate" Format="dd/MM/yyyy" OnClientDateSelectionChanged="checkDate">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtDepositDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtDepositDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>
           <%-- <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDepositDate"
                ErrorMessage="Please enter the Date Of Loan" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="CS">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="The date format should be dd/mm/yyyy"
                Type="Date" ControlToValidate="txtDepositDate" Operator="DataTypeCheck" CssClass="cvPCG"
                Display="Dynamic" ValidationGroup="CS"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr id="trAmountOfLoan" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblAmountOfLoan" runat="server" CssClass="FieldName" Text="Amount Of Loan:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtAmountOfLoan" runat="server" CssClass="txtField" 
                MaxLength="18"></asp:TextBox>
            <span id="Span4" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtAmountOfLoan"
                ErrorMessage="Please enter the Amount Of Loan" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="CS">
            </asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAmountOfLoan"
                ValidationGroup="CS" Display="Dynamic" runat="server" CssClass="rfvPCG"
                Operator="DataTypeCheck" ErrorMessage="Not acceptable format" ValidationExpression="^\d*(\.(\d{0,4}))?$"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr id="trInterestAmount" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblInterestAmount" runat="server" CssClass="FieldName" Text="Interest Amount:"
                ></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtInterestAmountAccum" runat="server" CssClass="txtField" 
                Text="" MaxLength="18"></asp:TextBox>
          <%--  <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtInterestAmountAccum"
                ErrorMessage="Please enter the Interest Amount" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="CS">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Please enter a numeric value"
                Type="Double" ControlToValidate="txtInterestAmountAccum" Operator="DataTypeCheck"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="CS"></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr id="trInterestPayable" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblInterestPayableFrequency" runat="server" CssClass="FieldName" Text="Frequency Of Interest Payment:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:DropDownList ID="ddlIPFrequency" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <br />
            <asp:CompareValidator ID="cvInsuranceIssuerCode" runat="server" ControlToValidate="ddlIPFrequency"
                ErrorMessage="Please select a Frequency" Operator="NotEqual" ValueToCompare="Select a Frequency"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="CS"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trRemarks" runat="server" visible="false">
        <td class="leftField">
            <asp:Label ID="lblRemarks" runat="server" CssClass="FieldName" Text="Remarks:"></asp:Label>
        </td>
        <td colspan="3">
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="txtField" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnAddDetails_Click"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover','ctrl_PortfolioCashSavingsEntry_btnSubmit');"
                onmouseout="javascript:ChangeButtonCss('out','ctrl_PortfolioCashSavingsEntry_btnSubmit');"
                ValidationGroup="CS" />
            <asp:Button ID="btnSaveChanges" runat="server" Text="Update" OnClick="btnSaveChanges_Click"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover','ctrl_PortfolioCashSavingsEntry_btnSaveChanges');"
                onmouseout="javascript:ChangeButtonCss('out','ctrl_PortfolioCashSavingsEntry_btnSaveChanges');"
                ValidationGroup="CS" />
        </td>
    </tr>
</table>
</contenttemplate>
 &nbsp;
 &nbsp;
 &nbsp;
  <tr id="trNomineeCaption" runat="server" visible="true">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px;">
            <div class="divSectionHeading" style="vertical-align:text-bottom;">
                Cash and Saving Transaction
            </div>
        </td>
    </tr>
    &nbsp;
 &nbsp;
 &nbsp;
 &nbsp;
<div>
   <%-- <asp:Label ID="Label2" runat="server" CssClass="" Text="Cash and Saving Transaction"></asp:Label>
    <hr />--%>
    <telerik:RadGrid ID="gvCashSavingTransaction" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" Skin="Telerik"
        EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
        EnableViewState="true" ShowFooter="true">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView TableLayout="Auto" DataKeyNames="CCST_TransactionId" AllowFilteringByColumn="true"
            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top"
            EditMode="InPlace">
            <Columns>
                <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                    CancelText="Cancel" ButtonType="ImageButton" CancelImageUrl="../Images/Telerik/Cancel.gif"
                    InsertImageUrl="../Images/Telerik/Update.gif" UpdateImageUrl="../Images/Telerik/Update.gif"
                    EditImageUrl="../Images/Telerik/Edit.gif">
                    <HeaderStyle Width="85px"></HeaderStyle>
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="CCST_Transactiondate" AllowFiltering="false"
                    HeaderStyle-Width="80px" HeaderText="Transaction Date" UniqueName="CCST_Transactiondate"
                    SortExpression="CCST_Transactiondate" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                    DataFormatString="{0:dd/MM/yyyy}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_Desc" HeaderText="Description" HeaderStyle-Width="70px"
                    AllowFiltering="false" UniqueName="CCST_Desc" SortExpression="CCST_Desc" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_ChequeNo" AllowFiltering="false" HeaderText="Cheque No."
                    UniqueName="CCST_ChequeNo" SortExpression="CCST_ChequeNo" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="70px" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_IsWithdrwal" AllowFiltering="false" HeaderText="Withdrwal"
                    HeaderStyle-Width="70px" UniqueName="CCST_IsWithdrwal" SortExpression="CCST_IsWithdrwal"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="8px" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_Amount" AllowFiltering="false" HeaderText="Deposit Amount"
                    HeaderStyle-Width="70px" UniqueName="" SortExpression="" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_AvailableBalance" AllowFiltering="false"
                    HeaderText="Available Balance" HeaderStyle-Width="50px" UniqueName="CCST_AvailableBalance"
                    SortExpression="CCST_AvailableBalance" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings CaptionFormatString="Edit details for employee with ID {0}" CaptionDataField="FirstName">
                <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<table>
<tr>
<td align="center">
<div>
<asp:Label ID="lable" runat="server" class="headrow" Text="Upload File :">
</asp:Label>
<asp:FileUpload ID="FileUploadTran" runat="server" />
<asp:Button ID="btnUpload" runat="server" 
            Height="21px" Text="Upload" Width="92px" onclick="btnUpload_Click"/>
</div>
</td>
</tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <%--<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>    --%>
           <%-- <asp:Button ID="btnDelete" runat="server" Text="Delete" onclick="btnDelete_Click"/>--%>
        </td>
    </tr>
</table>
<%--</asp:UpdatePanel>--%>