<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.AddBankDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">
    function chkTransactionExists() {

        if ($("#<%=ddlAccountDetails.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }

        $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckTransactionExistanceOnHoldingAdd",
            data: "{ 'CBBankAccountNum': '" + $("#<%=ddlAccountDetails.ClientID %>").val() + "'}",
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
                    alert("Transaction is all ready Exists First delete Transactions");
                    document.getElementById("<%= btnSubmit.ClientID %>").disabled = true;
                    return false;
                }

            }
        });
    }

   
    
</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <%-- <td align="left">
                            Add Bank Transactions/Balance
                        </td>--%>
                        <td align="left">
                            <asp:Label ID="lblheader" runat="server" Class="HeaderTextBig"></asp:Label>
                        </td>
                        <td align="right">
                            <asp:LinkButton runat="server" ID="lnkBtnBack" CssClass="LinkButtons" Text="Back"
                                OnClick="lnkBtnBack_Click"></asp:LinkButton>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportFilteredData_OnClick" OnClientClick="setFormat('excel')" Height="25px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground">
    <%--<tr>
        <td align="center" style="padding-right: 5px;">--%>
           <%-- <div>
                <table>--%>
                    <tr id="trAccount" runat="server">
                        <td class="leftField" style="width: 500px" align="right">
                            <asp:Label ID="Label1" CssClass="FieldName" runat="server" Text="Bank Name/Account No.:"></asp:Label>
                        </td>
                        <td colspan="4" style="width: 50px">
                            <asp:Label ID="lblBankName" runat="server" CssClass="Field"></asp:Label>
                            <asp:Label ID="lblAccId" runat="server" CssClass="Field"></asp:Label>
                            <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlAccountDetails_SelectedIndexChanged"
                                CssClass="cmbField" runat="server" ID="ddlAccountDetails" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="0" Selected="False">Select</asp:ListItem>
                            </asp:DropDownList>
                            <%-- <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="ddlAccountDetails"
                                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number"
                                Display="Dynamic" runat="server" CssClass="rfvPCG">
                            </asp:RequiredFieldValidator>--%>
                            <%-- <span id="Span3" class="spnRequiredField">*</span>--%>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlAccountDetails"
                                ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Bank/Account"
                                Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                        </td>
                        <%-- <td class="leftField" align="left">
                            <asp:Label ID="lb" CssClass="FieldName" runat="server" Text="Bank Name:"></asp:Label>
                        </td>--%>
                        <td>
                            <%--<asp:Label ID="lblBankName" runat="server" CssClass="Field"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr runat="server" id="trHoldingAndTrnx" visible="false">
                        <td class="leftField">
                            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Edit:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlAccountSelect_SelectedIndexChanged"
                                CssClass="cmbField" runat="server" ID="ddlAccountSelect" AppendDataBoundItems="true">
                                <asp:ListItem Text="Select" Value="Select">Select</asp:ListItem>
                                <asp:ListItem Text="TotalBalance" Value="TB">Total Balance </asp:ListItem>
                                <asp:ListItem Text="IndividualTransaction" Value="IT">Individual Transaction</asp:ListItem>
                            </asp:DropDownList>
                            <%--  <span id="Span5" class="spnRequiredField">*</span>
                             <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlAccountSelect"
                                    ErrorMessage="<br />Please select a Edit modes"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
                        </td>
                        <%-- <td>
                <asp:RadioButton ID="rbtnholding" runat="server" CssClass="cmbField" GroupName="rbtnHolding"
                    Text="Holding" OnCheckedChanged="Holding_CheckedChanged" AutoPostBack="true" />
                <asp:RadioButton ID="rbtntransaction" runat="server" CssClass="cmbField" GroupName="rbtnHolding"
                    Text="Transaction" OnCheckedChanged="Holding_CheckedChanged" AutoPostBack="true" />
                <%--OnCheckedChanged="Holding_CheckedChanged"  --%>
                        <%--</td>--%>
                    </tr>
                    <tr id="trholdingamount" runat="server" visible="false">
                        <td class="leftField">
                            <asp:Label ID="lblamount" runat="server" CssClass="FieldName" Text="Holdings Amount:"></asp:Label>
                        </td>
                        <td class="rightField">
                            <asp:TextBox ID="txtholdingAmt" onblur="return chkTransactionExists()" runat="server"
                                CssClass="txtField" Text='<%# Bind("CB_HoldingAmount") %>'></asp:TextBox>
                            <span id="spnLoginStatus" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtholdingAmt"
                                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Amount" Display="Dynamic"
                                runat="server" CssClass="rfvPCG">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="rfvPCG"
                                ErrorMessage="Please enter a valid amount" Display="Dynamic" runat="server" ControlToValidate="txtholdingAmt"
                                ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 400px" align="right">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                                Text="Submit" Visible="false" OnClick="btnSubmit_Click" ValidationGroup="btnSubmit" />
                            <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                                Text="Update" Visible="false" OnClick="btnUpdate_Click" ValidationGroup="btnSubmit" />
                        </td>
                    </tr>
              <%--  </table>
            </div>--%>
       <%-- </td>
    </tr>--%>
</table>
<%--<table  width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                        Customer Bank Transaction
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportFilteredData_OnClick" OnClientClick="setFormat('excel')" Height="25px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>--%>
<table width="100%">
    <tr id="trAddTransaction" runat="server" visible="false">
        <td>
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Customer Bank Transaction
                        </td>
                    </tr>
                </table>
            </div>
            <%--      </td>
    </tr>
</table>--%>
            <table width="100%" class="TableBackground">
                <tr>
                    <td align="center" colspan="5">
                        <div id="DIVAddtransaction" visible="false">
                            <table>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="lblExternalTransactionId" runat="server" CssClass="FieldName" Text="External Trans.ID:"></asp:Label>
                                    </td>
                                    <td class="rightField" colspan="4">
                                        <asp:TextBox ID="txtExternalTransactionId" runat="server" CssClass="txtField" Text='<%# Bind("CCST_ExternalTransactionId") %>'></asp:TextBox>
                                        <%-- <span id="Span2" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtExternalTransactionId"
                                            ValidationGroup="btnSubmitTransaction" ErrorMessage="<br />Please enter a Extrnl Tran. No."
                                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="lblTransactionDate" runat="server" CssClass="FieldName" Text="Transaction Date:"></asp:Label>
                                    </td>
                                    <td class="rightField" colspan="4">
                                        <telerik:RadDatePicker ID="dpTransactionDate" runat="server" Culture="English (United States)"
                                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                                ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                                            </Calendar>
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy" runat="server">
                                            </DateInput>
                                        </telerik:RadDatePicker>
                                        <span id="Span1" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="dpTransactionDate"
                                            ValidationGroup="btnSubmitTransaction" ErrorMessage="<br />Please enter a Date"
                                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                                            Type="Date" ControlToValidate="dpTransactionDate" CssClass="cvPCG" Operator="DataTypeCheck"
                                            Display="Dynamic"></asp:CompareValidator>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="lblDescription" runat="server" CssClass="FieldName" Text="Description:"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="txtDescripton" runat="server" CssClass="txtField" Text='<%# Bind("CCST_Desc") %>'></asp:TextBox>
                                        <%-- <span id="Span5" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtDescripton"
                                            ValidationGroup="btnSubmitTransaction" ErrorMessage="<br />Please enter Description"
                                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>--%>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="txtChequeNo" runat="server" CssClass="txtField" Text='<%# Bind("CCST_ChequeNo") %>'></asp:TextBox>
                                        <%--  <span id="Span3" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtChequeNo"
                                            ValidationGroup="btnSubmitTransaction" ErrorMessage="<br />Please enter a ChequeNo"
                                            Display="Dynamic" runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList AutoPostBack="false" CssClass="cmbField" runat="server" ID="ddlCFCCategory"
                                            AppendDataBoundItems="false">
                                        </asp:DropDownList>
                                        <span id="Span2" class="spnRequiredField">*</span>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCFCCategory"
                                            ValidationGroup="btnSubmitTransaction" ErrorMessage="<br />Please select a Category"
                                            Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Transaction Type:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButton ID="rbtnY" runat="server" CssClass="cmbField" GroupName="rbtnIs_Withdrwal"
                                            Text="DR" AutoPostBack="false" />
                                        <asp:RadioButton ID="rbtnN" runat="server" CssClass="cmbField" GroupName="rbtnIs_Withdrwal"
                                            Text="CR" AutoPostBack="false" Checked="true" />
                                        <%--OnCheckedChanged="rbtnYes_CheckedChanged"--%>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftField">
                                        <asp:Label ID="Label3" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                                    </td>
                                    <td class="rightField">
                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" MaxLength="20" Text='<%# Bind("CCST_Amount") %>'></asp:TextBox>
                                        <span id="Span4" class="spnRequiredField">*</span>
                                        <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" ValidationGroup="btnSubmitTransaction"
                                            ErrorMessage="<br />Please enter a Amount" Display="Dynamic" runat="server" CssClass="rfvPCG">
                                        </asp:RequiredFieldValidator>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="rfvPCG"
                                            ErrorMessage="Please enter a valid amount" Display="Dynamic" runat="server" ControlToValidate="txtAmount"
                                            ValidationGroup="btnSubmitTransaction" ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                                        <%--<asp:RangeValidator runat="server" ID="RangeValidator1" ControlToValidate="txtAmount"
                                                Type="Integer" MinimumValue="10" MaximumValue="100" CssClass="rfvPCG" ErrorMessage="Page Range between 10-100 Acceptable"
                                                ValidationGroup="BtnSubmitpage">
                                            </asp:RangeValidator>--%>
                                        <%--ValidationExpression="^[0-9]+$"--%>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Button ID="btnSubmitTransaction" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                            Text="Submit" OnClick="btnSubmitTransaction_Click" ValidationGroup="btnSubmitTransaction" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div id="DivTransaction" style="overflow:scroll" runat="server" visible="false">
    <telerik:RadGrid ID="gvCashSavingTransaction" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" Skin="Telerik"
        EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
        EnableViewState="true" ShowFooter="true" OnItemCommand="gvCashSavingTransaction_ItemCommand"
        OnItemDataBound="gvCashSavingTransaction_ItemDataBound" OnNeedDataSource="gvCashSavingTransaction_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView TableLayout="Auto" DataKeyNames="CCST_TransactionId,WERP_CFCCode,CCST_IsWithdrwal"
            AllowFilteringByColumn="true" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" EditMode="EditForms">
            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                AddNewRecordText="Add Transaction" ShowRefreshButton="false" ShowExportToCsvButton="false"
                ShowAddNewRecordButton="true" ShowExportToPdfButton="false" />
            <Columns>
                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="WERPBDTM_BankName" AllowFiltering="true" HeaderStyle-Width="80px"
                    HeaderText="Bank" UniqueName="WERPBDTM_BankName" SortExpression="WERPBDTM_BankName"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CB_AccountNum" AllowFiltering="false" HeaderStyle-Width="80px"
                    HeaderText="Account No." UniqueName="CB_AccountNum" SortExpression="CB_AccountNum"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_ExternalTransactionId" AllowFiltering="false"
                    HeaderStyle-Width="80px" HeaderText="Transaction Id" UniqueName="CCST_ExternalTransactionId"
                    SortExpression="CCST_ExternalTransactionId" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_Transactiondate" AllowFiltering="false"
                    HeaderStyle-Width="80px" HeaderText="Transaction Date" UniqueName="CCST_Transactiondate"
                    SortExpression="CCST_Transactiondate" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                    DataFormatString="{0:d}">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_Desc" HeaderText="Description" HeaderStyle-Width="70px"
                    AllowFiltering="false" UniqueName="CCST_Desc" SortExpression="CCST_Desc" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_ChequeNo" AllowFiltering="false" HeaderText="Cheque No."
                    UniqueName="CCST_ChequeNo" SortExpression="CCST_ChequeNo" AutoPostBackOnFilter="true"
                    HeaderStyle-Width="70px" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CCST_IsWithdrwal" AllowFiltering="false" HeaderText="Type"
                    HeaderStyle-Width="70px" UniqueName="CCST_IsWithdrwal" SortExpression="CCST_IsWithdrwal"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="8px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="WERP_CFCName" AllowFiltering="false" HeaderText="Category"
                    HeaderStyle-Width="70px" UniqueName="WERP_CFCName" SortExpression="WERP_CFCName"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="8px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="true" DataField="CCST_Amount" AllowFiltering="false"
                    HeaderText="Deposit Amount" HeaderStyle-Width="70px" UniqueName="CCST_Amount"
                    SortExpression="CCST_Amount" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="CB_HoldingAmount" AllowFiltering="false"
                    HeaderText="Available Balance" HeaderStyle-Width="50px" UniqueName="CB_HoldingAmount"
                    SortExpression="CB_HoldingAmount" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                    CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn Visible="true" UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Record?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    HeaderStyle-Width="80px" Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <EditFormSettings FormTableStyle-Height="100%" EditFormType="Template" PopUpSettings-Height="180px"
                PopUpSettings-Width="600px" FormMainTableStyle-Width="1000px">
                <FormTemplate>
                    <table width="100%" style="background-color: White;" border="0">
                        <%-- <tr class="EditFormHeader">
                            <td colspan="2" style="font-size: small">
                                <asp:Label ID="EditFormHeader" runat="server" CssClass="HeaderTextSmall" Text="Customer Bank Transaction"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="4">
                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                    Customer Bank Transaction
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblExternalTransactionId" runat="server" CssClass="FieldName" Text="External Trans.ID:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtExternalTransactionId" runat="server" CssClass="txtField" Text='<%# Bind("CCST_ExternalTransactionId") %>'></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblTransactionDate" runat="server" CssClass="FieldName" Text="Transaction Date:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <telerik:RadDatePicker ID="dpTransactionDate" runat="server" Culture="English (United States)"
                                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                    <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="dpTransactionDate"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Date" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />The date format should be dd/mm/yyyy"
                                    Type="Date" ControlToValidate="dpTransactionDate" CssClass="cvPCG" Operator="DataTypeCheck"
                                    Display="Dynamic"></asp:CompareValidator>
                            </td>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblDescription" runat="server" CssClass="FieldName" Text="Description:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtDescripton" runat="server" CssClass="txtField" Text='<%# Bind("CCST_Desc") %>'></asp:TextBox>
                            </td>
                            <%-- <td colspan="2">
                                &nbsp;
                            </td>--%>
                            <%-- </tr>
                        <tr>--%>
                            <td class="leftField">
                                <asp:Label ID="lblChequeNo" runat="server" Text="Cheque No:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtChequeNo" runat="server" CssClass="txtField" Text='<%# Bind("CCST_ChequeNo") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Transaction Type:"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnIs_Withdrwal"
                                    Text="DR" AutoPostBack="false" />
                                <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnIs_Withdrwal"
                                    Text="CR" AutoPostBack="false"/>
                                <%--OnCheckedChanged="rbtnYes_CheckedChanged"--%>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList AutoPostBack="false" CssClass="cmbField" runat="server" ID="ddlCFCCategory"
                                    AppendDataBoundItems="false">
                                </asp:DropDownList>
                                <span id="Span2" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlCFCCategory"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Category" Operator="NotEqual"
                                    ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAmount" runat="server" Text="Amount:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" Text='<%# Bind("CCST_Amount") %>'></asp:TextBox>
                                <span id="Span4" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" ValidationGroup="btnSubmit"
                                    ErrorMessage="<br />Please enter a Amount" Display="Dynamic" runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="rfvPCG"
                                    ErrorMessage="Please enter a valid amount" Display="Dynamic" runat="server" ControlToValidate="txtAmount"
                                    ValidationExpression="\d+\.?\d*"></asp:RegularExpressionValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" Text='<%# (Container is GridEditFormInsertItem) ? "Insert":"Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                    ValidationGroup="btnSubmit"></asp:Button>
                                &nbsp;&nbsp;
                            </td>
                            <td visible="false">
                                <asp:Button Visible="false" ID="btnYes" runat="server" Text="Submit and Addmore"
                                    CssClass="PCGLongButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnYes','L');"
                                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnYes','L');"
                                    ValidationGroup="btnSubmit" />
                            </td>
                            <td>
                                <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                    CommandName="Cancel"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </FormTemplate>
            </EditFormSettings>
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="false" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<%--<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table class="TableBackground" width="100%">
    <tr>
        <td colspan="4">
            <asp:Label ID="lblHeader" runat="server" Text="Add Bank Details" CssClass="HeaderTextBig"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td colspan="4" class="tdRequiredText">
            <label id="lbl" class="lblRequiredText">
                Note: Fields marked with ' * ' are mandatory</label>
        </td>
    </tr>
    <%--<tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextBig" Text="Add Bank Details"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>--%>
<%-- <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAccountType"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type" Operator="NotEqual"
                ValueToCompare="Select" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountNumber" runat="server" CssClass="FieldName" Text="Account Number:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField"></asp:TextBox>
            <span id="spAccountNumber" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblModeOfOperation" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlModeOfOperation" CssClass="cmbField" runat="server">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlModeOfOperation"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a mode of holding" Operator="NotEqual"
                ValueToCompare="Select" CssClass="cvPCG"></asp:CompareValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
            <span id="spBankName" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="<br />Please enter a Bank Name"
                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblBranchName" runat="server" Text="Branch Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
            <span id="spBranchName" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBranchName"
                ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>--%>
<%--<tr>
        <td class="leftField">
            <asp:Label ID="lblBalance" runat="server" Text="Balance:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBalance" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>--%>
<%--  <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="Label26" runat="server" Text="Branch Details" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" style="width:250px;"></asp:TextBox>
        </td>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCity" runat="server" CssClass="FieldName" Text="City:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"></asp:TextBox>
            <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlBankAdrCountry" runat="server" CssClass="cmbField">
                <asp:ListItem>India</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblMicr" runat="server" Text="MICR:" CssClass="FieldName"></asp:Label>
           
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9"></asp:TextBox>
            <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr" Operator="DataTypeCheck"
                Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField">
            <asp:Label ID="lblIfsc" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField" MaxLength="11"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;
        </td>
    </tr>

</table>--%>
<%--<table class="TableBackground" width="60%">
    <tr>
     
        <td align="center">
            <asp:Button ID="btnNo" runat="server" Text="Submit" CssClass="PCGMediumButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnNo','M');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnNo','M');"
                ValidationGroup="btnSubmit" OnClick="btnNo_Click" />
        
            <asp:Button ID="btnYes" runat="server" Text="Submit and Addmore" CssClass="PCGLongButton"  onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_AddBankDetails_btnYes','L');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_AddBankDetails_btnYes','L');"
                ValidationGroup="btnSubmit" OnClick="btnYes_Click" />
        </td>
       
        <td>
            &nbsp;
        </td>
    </tr>
</table>--%>
