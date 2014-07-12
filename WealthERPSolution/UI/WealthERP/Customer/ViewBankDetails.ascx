<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewBankDetails.ascx.cs"
    Inherits="WealthERP.Customer.ViewBankDetails" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
            data: "{ 'CBBankAccountNum': '" + $("#<%=ddlAccountDetails.ClientID %>").val() + "' }",
            error: function(xhr, status, error) {
                //                alert("Please select AMC!");
            },
            success: function(msg) {

                if (msg.d) {

                    $("#<%= hidValidCheck.ClientID %>").val("1");
                    $("#spnLoginStatus").html("");
                }
                else {

                    $("#<%= hidValidCheck.ClientID %>").val("0");
                    $("#spnLoginStatus").removeClass();
                    alert("Transaction is all ready Exists First delete Transactions");
                    return false;
                }

            }
        });
    }
</script>
<script type="text/javascript" language="javascript">
    function conformation() {
            var answer = confirm("Are you sure you want to delete?");
            if (answer)
                return true;
            else
                return false;
    }
    </script>
<script type="text/javascript">
    function ShowPopup() {
        //        alert(transactionId);
        var form = document.forms[0];
        var transactionId = "";
        var count = 0

        var a = document.getElementById('ctrl_ViewBankDetails_gvBankDetails_ctl00_ctl02_ctl03_ddlModeofOperation').value;
        if (a == 'JO') {
            for (var i = 0; i < form.elements.length; i++) {
                if (form.elements[i].type == 'checkbox') {
                    if (form.elements[i].checked == true) {
                        count++;
                        // alert(count);
                        hiddenField = form.elements[i].id.replace("chkId", "hdnchkBx");
                        // alert("hi");
                        hiddenFieldValues = document.getElementById(hiddenField).value;
                        // alert(hiddenFieldValues);
                        var splittedValues = hiddenFieldValues.split("-");
                        // alert(count);
                        if (count == 1) {
                            AssociationId = splittedValues[0];
                        }
                    }
                }
            }
            if (count == 0) {
                alert("Please select a joint Holder")
                return false;
            }
            // alert(transactionId);
        }
        else
            return true;
    }
</script>

<table style: width="100%;">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Bank Account Details
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png" 
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                      <%--  td style="width: 10px" align="right">
                            <img src="../Images/helpImage.png" height="20px" width="25px" style="float: right;"
                                class="flip" />
                        </td>--%>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <td>
            <asp:Label ID="lblMessage" runat="server" Text="No Records Found..." CssClass="Error"></asp:Label>
        </td>
    </tr>
</table>
<br />
<%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" IsSticky="true" runat="server" 
            EnableSkinTransparency="true" Transparency="25" BackColor="#AAAAAA"   > 
            <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Loading.gif" style="border: 0px;">  
            </asp:Image> 
        </telerik:RadAjaxLoadingPanel> --%>
<div style="width: 100%; padding-left: 4px; padding-right: 10px;">
    <telerik:RadGrid ID="gvBankDetails" runat="server" GridLines="None" Width="99%" AllowPaging="true"
        PageSize="10" AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true" ShowFooter="true"
        AllowAutomaticDeletes="True" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
        Skin="Telerik" EnableEmbeddedSkins="false"
        OnItemCommand="gvBankDetails_ItemCommand" EnableHeaderContextMenu="false" OnPreRender="gvBankDetails_PreRender"
        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="false" OnNeedDataSource="gvBankDetails_NeedDataSource"
        EditItemStyle-Width="600px">
        <%-- ,ModeOfHoldingCode,BankAccountTypeCode,CB_BranchAdrState" --%>
        <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="CB_CustBankAccId,CB_AccountNum,WCMV_BankName,CB_HoldingAmount,XMOH_ModeOfHoldingCode,WCMV_LookupId_AccType,WCMV_Lookup_BranchAddStateId,WCMV_LookupId_BankId"
            EditMode="EditForms" CommandItemDisplay="None" Width="100%">
            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                AddNewRecordText="Add New Bank Details" ShowRefreshButton="false" ShowExportToCsvButton="false"
                ShowAddNewRecordButton="true" ShowExportToPdfButton="false" />
            <%--,ModeOfHoldingCode,BankAccountTypeCode,CB_BranchAdrState--%>
            <%--  AddNewRecordText="Add New Bank Details"--%>
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action" FooterText="Grand Total:"
                    HeaderStyle-Width="80px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkId" runat="server" />
                        <%--<asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("WERPTransactionId").ToString()%>' />--%>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" EditText="View/Edit"
                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn ButtonType="LinkButton" Text="View Transaction" UniqueName="ButtonColumn1"
                    CommandName="viewTransaction">
                    <HeaderStyle Width="100px" />
                </telerik:GridButtonColumn>
                 <telerik:GridButtonColumn ButtonType="LinkButton" Text="Edit Balance" UniqueName="ButtonColumn"
                    CommandName="Editbalance">
                    <HeaderStyle Width="80px" />
                </telerik:GridButtonColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_CustBankAccId" HeaderStyle-Width="80px"
                    HeaderText="CB_CustBankAccId" DataField="CB_CustBankAccId" SortExpression="CB_CustBankAccId"
                    AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WCMV_BankName" HeaderStyle-Width="90px"
                    HeaderText="Bank" DataField="WCMV_BankName" SortExpression="WCMV_BankName"
                    AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CB_AccountNum" HeaderStyle-Width="80px" HeaderText="Account No."
                    DataField="CB_AccountNum" SortExpression="CB_AccountNum" AllowFiltering="false"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WCMV_BankAccountType" HeaderStyle-Width="80px"
                    HeaderText="Account Type" DataField="WCMV_BankAccountType" SortExpression="WCMV_BankAccountType"
                    AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="XMOH_ModeOfHolding" HeaderStyle-Width="80px"
                    HeaderText="Mode Of Operation" DataField="XMOH_ModeOfHolding" SortExpression="XMOH_ModeOfHolding"
                    AllowFiltering="false" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CB_BranchName" HeaderStyle-Width="80px" HeaderText="Branch"
                    DataField="CB_BranchName" SortExpression="CB_BranchName" AllowFiltering="false"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CB_HoldingAmount" HeaderStyle-Width="80px" HeaderText="Holding Amount"
                    DataField="CB_HoldingAmount" SortExpression="CB_HoldingAmount" AllowFiltering="false" Aggregate="Sum" DataFormatString="{0:N2}"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn Visible="false" UniqueName="details" HeaderStyle-Width="150px"
                    HeaderText="bankname" DataField="details" SortExpression="details"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn Visible="false" UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete this Record?"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    HeaderStyle-Width="80px" Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                 <telerik:GridBoundColumn DataField="CB_IsFromTransaction"  HeaderText="Flag" UniqueName="CB_IsFromTransaction" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="left"  Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_IFSC" HeaderStyle-Width="150px"
                    HeaderText="IFSC" DataField="CB_IFSC" SortExpression="CB_IFSC" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_MICR" HeaderStyle-Width="150px"
                    HeaderText="MICR" DataField="CB_MICR" SortExpression="CB_MICR" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>                
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrCountry" HeaderStyle-Width="150px"
                    HeaderText="Branch Country" DataField="CB_BranchAdrCountry" SortExpression="CB_BranchAdrCountry"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrState" HeaderStyle-Width="150px"
                    HeaderText="Branch State" DataField="CB_BranchAdrState" SortExpression="CB_BranchAdrState"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrCity" HeaderStyle-Width="150px"
                    HeaderText="Branch City" DataField="CB_BranchAdrCity" SortExpression="CB_BranchAdrCity"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrPinCode" HeaderStyle-Width="150px"
                    HeaderText="PinCode" DataField="CB_BranchAdrPinCode" SortExpression="CB_BranchAdrPinCode"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine3" HeaderStyle-Width="150px"
                    HeaderText="Branch Line3" DataField="CB_BranchAdrLine3" SortExpression="CB_BranchAdrLine3"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine2" HeaderStyle-Width="150px"
                    HeaderText="Branch Line2" DataField="CB_BranchAdrLine2" SortExpression="CB_BranchAdrLine2"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" UniqueName="CB_BranchAdrLine1" HeaderStyle-Width="150px"
                    HeaderText="Branch Line1" DataField="CB_BranchAdrLine1" SortExpression="CB_BranchAdrLine1"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
            </Columns>
            <EditFormSettings FormTableStyle-Height="100%" EditFormType="Template" PopUpSettings-Height="505px"
                PopUpSettings-Width="810px" FormMainTableStyle-Width="3500px">
                <FormTemplate>
                    <table width="100%" style="background-color: White;" border="0">
                        <tr>
                            <td colspan="4">
                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                    Bank Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlPortfolioId" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
                                </asp:DropDownList>
                                <span id="Span4" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAccountType"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAccountType" runat="server" CssClass="FieldName" Text="Account Type:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlAccountType"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Account Type"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
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
                                <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txtField" Text='<%# Bind("CB_AccountNum") %>'></asp:TextBox>
                                <span id="spAccountNumber" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvAccountNumber" ControlToValidate="txtAccountNumber"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Account Number"
                                    Display="Dynamic" runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr id="trJoingHolding" runat="server">
                            <td class="leftField">
                                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Joint Holding:"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                                    Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged " />
                                <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                                    Text="No" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged" Checked="true" />
                                <%--OnCheckedChanged="rbtnYes_CheckedChanged"--%>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblModeOfOperation" runat="server" Text="Mode of Operation:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlModeofOperation" runat="server" CssClass="cmbField" Enabled="false">
                                    <asp:ListItem Text="Select" Value="0" Selected="trues">
                                    </asp:ListItem>
                                </asp:DropDownList>
                                <span id="Span2" class="spnRequiredField">*</span>
                               <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlModeofOperation"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a ModeofHolding"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
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
                                <asp:DropDownList ID="ddlBankName" CssClass="cmbField" runat="server">
                                </asp:DropDownList>
                                <span id="Span3" class="spnRequiredField">*</span>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlBankName"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a Bank Name" Operator="NotEqual"
                                    ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                                <%--<asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BankName") %>'></asp:TextBox>
                                <span id="spBankName" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="<br />Please enter a Bank Name"
                                    Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">--%>
                                <%-- </asp:RequiredFieldValidator>--%>
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
                                <asp:TextBox ID="txtBranchName" runat="server" CssClass="txtField" Style="width: 225px;"
                                    Text='<%# Bind("CB_BranchName") %>'></asp:TextBox>
                                <span id="spBranchName" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBranchName"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>                       
                        <tr id="trNoJointHolder" runat="server" visible="false">
                            <td class="Message" colspan="2">
                                <asp:Label ID="lblNoJointHolder" runat="server" Text="You have no Joint Holder" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trheadingCaption" runat="server" visible="true">
                            <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                    Nominees &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; Joint Holders
                                </div>
                               <%-- <td>
                                  <div class="divSectionHeading" style="vertical-align: text-bottom">
                                  Joint Holders
                                  </div>
                                  </td>--%>
                            </td>
                            <%-- <td colspan="1" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px">
                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                    
                                </div>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <telerik:RadGrid ID="gvNominees" runat="server" GridLines="None" Width="100%" AllowPaging="true"
                                    AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true" AllowAutomaticDeletes="True"
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="true"
                                    EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="false">
                                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="MemberCustomerId,AssociationId">
                                        <Columns>
                                            <%--    <asp:TemplateField HeaderText="Select">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkId0" runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>--%>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Select">
                                                <%--<HeaderTemplate>
                                                    <input name="Select" />
                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkId0" runat="server" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="Name" HeaderStyle-Width="80px" HeaderText="Name"
                                                DataField="Name" SortExpression="Name" AllowFiltering="false" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Relationship" HeaderStyle-Width="90px" HeaderText="Relationship"
                                                DataField="Relationship" SortExpression="Relationship" AllowFiltering="false"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <td colspan="1">
                            </td>
                            <td>
                                <telerik:RadGrid ID="gvJointHolders" runat="server" GridLines="None" Width="100%"
                                    AllowPaging="true" AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true"
                                    AllowAutomaticDeletes="True" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                                    Skin="Telerik" EnableEmbeddedSkins="true" EnableHeaderContextFilterMenu="true"
                                    AllowFilteringByColumn="false">
                                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="MemberCustomerId,AssociationId">
                                        <Columns>
                                            <%--    <asp:TemplateField HeaderText="Select">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkId0" runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>--%>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Select">
                                                <%-- <HeaderTemplate>
                                                    <input id="chkJID" name="chkJID" type="checkbox" onclick="ShowPopup()"/>
                                                </HeaderTemplat--%>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkId" runat="server" />
                                                    <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("AssociationId").ToString()%>' />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn UniqueName="Name" HeaderStyle-Width="80px" HeaderText="Name"
                                                DataField="Name" SortExpression="Name" AllowFiltering="false" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="Relationship" HeaderStyle-Width="90px" HeaderText="Relationship"
                                                DataField="Relationship" SortExpression="Relationship" AllowFiltering="false"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                            <%-- <td>--%>
                            <%--    <tr>--%>
                            <%--<tr id="trError" runat="server" visible="false">--%>
                            <td colspan="2" class="Message">
                                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                            </td>
                            <%--   </tr>--%>
                            <%--</td>--%>
                        </tr>
                        <%-- <tr id="trNoNominee" runat="server" visible="false">--%>
                        <tr>
                            <td class="Message" colspan="2">
                                <asp:Label ID="lblNoNominee" runat="server" Text="You have no Associations" CssClass="FieldName"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <div class="divSectionHeading" style="vertical-align: text-bottom">
                                    Branch Details
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblAdrLine1" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrLine1" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BranchAdrLine1") %>'></asp:TextBox>
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
                                <asp:TextBox ID="txtBankAdrLine2" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BranchAdrLine2") %>'></asp:TextBox>
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
                                <asp:TextBox ID="txtBankAdrLine3" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BranchAdrLine3") %>'></asp:TextBox>
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
                                <asp:TextBox ID="txtBankAdrCity" runat="server" CssClass="txtField" Text='<%# Bind("CB_BranchAdrCity") %>'></asp:TextBox>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="State:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:DropDownList ID="ddlBankAdrState" runat="server" CssClass="txtField" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblPinCode" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtBankAdrPinCode" runat="server" CssClass="txtField" MaxLength="6"
                                    Text='<%# Bind("CB_BranchAdrPinCode") %>'></asp:TextBox>
                                <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                                    CssClass="rfvPCG" Type="Integer" ControlToValidate="txtBankAdrPinCode" ValidationGroup="btnSubmit"
                                    Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
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
                                <asp:TextBox ID="txtMicr" runat="server" CssClass="txtField" MaxLength="9" Text='<%# Bind("CB_MICR") %>'></asp:TextBox>
                                <asp:CompareValidator ID="cvMicr" runat="server" ErrorMessage="<br />Enter a numeric value"
                                    CssClass="rfvPCG" Type="Integer" ValidationGroup="btnSubmit" ControlToValidate="txtMicr"
                                    Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="lblIfsc" runat="server" Text="IFSC:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtIfsc" runat="server" CssClass="txtField" MaxLength="11" Text='<%# Bind("CB_IFSC") %>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                    ValidationGroup="btnSubmit" OnClientClick='<%# (Container is GridEditFormInsertItem) ?  "javascript:return ShowPopup();": "" %>'>
                                    <%-- OnClientClick='<%# (Container is GridEditFormInsertItem) ?  " javascript:return ShowPopup();": "" %>'--%>
                                </asp:Button>
                                <%-- OnClientClick=" javascript:return ShowPopup();"--%>
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
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<%--<table width="100%">--%>
<div id="DivAction" runat="server" visible="true">
    <table width="100%">
        <tr>
            <td class="SubmitCell">
                <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                    OnClick="btnDelete_Click" OnClientClick="if(conformation() == false) return false;conformation();"/>
            </td>
        </tr>
    </table>
</div>
<%--</table>--%>
<div>
    <%--<table>--%>
    <tr id="trCSTransactionCaption" runat="server" visible="false">
        <td colspan="6" style="vertical-align: text-bottom; padding-top: 6px; padding-bottom: 6px;">
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                Cash and Saving Transaction
            </div>
        </td>
    </tr>
    <%--</table>--%>
</div>
<br />
<div>
    <table>
        <tr id="trAccountDetail" runat="server" visible="false">
            <td class="leftField">
                <asp:Label CssClass="FieldName" runat="server" Text="Account Details:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="ddlAccountDetails_SelectedIndexChanged"
                    CssClass="cmbField" runat="server" ID="ddlAccountDetails" AppendDataBoundItems="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trHoldingAndTrnx" visible="false">
            <td class="leftField">
                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Amount:"></asp:Label>
            </td>
            <td>
                <asp:RadioButton ID="rbtnholding" runat="server" CssClass="cmbField" GroupName="rbtnHolding"
                    Text="Holding" OnCheckedChanged="Holding_CheckedChanged" AutoPostBack="true" />
                <asp:RadioButton ID="rbtntransaction" runat="server" CssClass="cmbField" GroupName="rbtnHolding"
                    Text="Transaction" OnCheckedChanged="Holding_CheckedChanged" AutoPostBack="true" />
                <%--OnCheckedChanged="Holding_CheckedChanged"  --%>
            </td>
        </tr>
        <tr id="trholdingamount" runat="server" visible="false">
            <td class="leftField">
                <asp:Label ID="lblamount" runat="server" CssClass="FieldName" Text="Holdindg Amount:"></asp:Label>
            </td>
            <td class="rightField">
                <asp:TextBox ID="txtholdingAmt" onblur="return chkTransactionExists()" runat="server"
                    CssClass="txtField" Text='<%# Bind("CB_HoldingAmount") %>'></asp:TextBox>
                <span id="spnLoginStatus">*</span>
                <asp:RegularExpressionValidator CssClass="rfvPCG" ErrorMessage="Please enter a valid amount"
                    Display="Dynamic" runat="server" ControlToValidate="txtholdingAmt" ValidationExpression="^([1-9]|[1-9][0-9]|[1-9][0-9][0-9])$"></asp:RegularExpressionValidator>
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                    onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerAccountAdd_btnSubmit', 'S');"
                    Text="Submit" OnClick="btnSubmit_Click" />
                <%----%>
            </td>
        </tr>
    </table>
</div>
<br />
<%--&nbsp; &nbsp; --%>
<div id="DivTransaction" runat="server" visible="false">
    <telerik:RadGrid ID="gvCashSavingTransaction" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" Skin="Telerik"
        EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
        EnableViewState="true" ShowFooter="true" OnItemCommand="gvCashSavingTransaction_ItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView TableLayout="Auto" DataKeyNames="CCST_TransactionId" AllowFilteringByColumn="true"
            Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top"
            EditMode="PopUp">
            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                AddNewRecordText="Add Transaction" ShowRefreshButton="false" ShowExportToCsvButton="false"
                ShowAddNewRecordButton="true" ShowExportToPdfButton="false" />
            <Columns>
                <%-- <telerik:GridEditCommandColumn UpdateText="Update" UniqueName="EditCommandColumn"
                    CancelText="Cancel" ButtonType="ImageButton" CancelImageUrl="../Images/Telerik/Cancel.gif"
                    InsertImageUrl="../Images/Telerik/Update.gif" UpdateImageUrl="../Images/Telerik/Update.gif"
                    EditImageUrl="../Images/Telerik/Edit.gif">
                    <HeaderStyle Width="85px"></HeaderStyle>
                </telerik:GridEditCommandColumn>--%>
                <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="50px" EditText="View/Edit"
                    UniqueName="editColumn" CancelText="Cancel" UpdateText="Update">
                </telerik:GridEditCommandColumn>
                <telerik:GridBoundColumn DataField="CCST_ExternalTransactionId" AllowFiltering="false"
                    HeaderStyle-Width="80px" HeaderText="Transaction Id" UniqueName="CCST_ExternalTransactionId"
                    SortExpression="CCST_ExternalTransactionId" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                </telerik:GridBoundColumn>
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
                <telerik:GridBoundColumn Visible="true" DataField="CCST_Amount" AllowFiltering="false"
                    HeaderText="Deposit Amount" HeaderStyle-Width="70px" UniqueName="" SortExpression=""
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" DataField="CB_HoldingAmount" AllowFiltering="false"
                    HeaderText="Available Balance" HeaderStyle-Width="50px" UniqueName="CB_HoldingAmount" DataFormatString="{0:N2}"
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
            <%--<EditFormSettings CaptionFormatString="Edit details for employee with ID {0}" CaptionDataField="FirstName">
                <FormTableItemStyle Width="100%" Height="29px"></FormTableItemStyle>
                <FormTableStyle GridLines="None" CellSpacing="0" CellPadding="2"></FormTableStyle>
                <FormStyle Width="100%" BackColor="#eef2ea"></FormStyle>
                <EditColumn ButtonType="ImageButton" />
            </EditFormSettings>--%>
            <EditFormSettings FormTableStyle-Height="100%" EditFormType="Template" PopUpSettings-Height="220px"
                PopUpSettings-Width="500px" FormMainTableStyle-Width="3000px">
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
                        <%-- <tr>
                            <td class="leftField">
                                <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Bank AccountId:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="txtField" ></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblExternalTransactionId" runat="server" CssClass="FieldName" Text="External Trans.ID:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtExternalTransactionId" runat="server" CssClass="txtField" Text='<%# Bind("CCST_ExternalTransactionId") %>'></asp:TextBox>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField">
                                <asp:Label ID="lblTransactionDate" runat="server" CssClass="FieldName" Text="Transaction Date:"></asp:Label>
                            </td>
                            <td class="rightField">
                                <telerik:RadDatePicker ID="dpTransactionDate" runat="server" Culture="English (United States)"
                                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                        Skin="Telerik" EnableEmbeddedSkins="false" runat="server">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
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
                                <%-- <span id="Span2" class="spnRequiredField">*</span>--%>
                                <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlModeofOperation"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please select a ModeofHolding"
                                    Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>--%>
                            </td>
                            <td class="leftField">
                                <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Transaction Type:"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbField" GroupName="rbtnIs_Withdrwal"
                                    Text="DR" AutoPostBack="false" />
                                <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbField" GroupName="rbtnIs_Withdrwal"
                                    Text="CR" AutoPostBack="false" Checked="true" />
                                <%--OnCheckedChanged="rbtnYes_CheckedChanged"--%>
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
                                <%-- <span id="Span3" class="spnRequiredField">*</span>--%>
                                <%--<asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" Style="width: 250px;"
                                    Text='<%# Bind("CB_BankName") %>'></asp:TextBox>
                                <span id="spBankName" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="<br />Please enter a Bank Name"
                                    Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmit">--%>
                                <%-- </asp:RequiredFieldValidator>--%>
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <%--<td class="leftField">
                                <asp:Label ID="lblAvailableBalance" runat="server" Text="Available Balance:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:TextBox ID="txtAvailableBalance" runat="server" CssClass="txtField" Text='<%# Bind("CCST_AvailableBalance") %>'></asp:TextBox>
                                <%--<span id="spBranchName" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvBranchName" ControlToValidate="txtBranchName"
                                    ValidationGroup="btnSubmit" ErrorMessage="<br />Please enter a Branch Name" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>--%>
                            <%--   </td>--%>
                            <%--<td colspan="2">
                                &nbsp;
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
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
<%--<cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="imgBtnExport" DynamicServicePath="" BackgroundCssClass="modalBackground"
                Enabled="True" OkControlID="btnOK" CancelControlID="btnCancel" Drag="true" OnOkScript="DownloadScript();">
            </cc1:ModalPopupExtender>--%>
<%--<table class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label runat="server" Text="Bank Details" CssClass="HeaderTextSmall" Font-Bold="True"></asp:Label>
        </td>
    </tr>
</table>
<br />--%>
<%--<table style="width: 100%;" class="TableBackground">
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblBankDetails" runat="server" CssClass="HeaderTextBig" Text="Bank Details"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
            <asp:Label ID="lblMsg" runat="server" CssClass="Error" Text="No Records Found"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gvCustomerBankAccounts" runat="server" AutoGenerateColumns="False"  AllowSorting="True" 
                CellPadding="4" CssClass="GridViewStyle" DataKeyNames="CustBankAccId" 
                OnSelectedIndexChanged="gvCustomerBankAccounts_SelectedIndexChanged" ShowFooter="True">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" />
                    <asp:BoundField DataField="Bank Name" HeaderText="Bank Name"  />
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                    <asp:BoundField DataField="Account Type" HeaderText="Account Type"  />
                    <asp:BoundField DataField="Mode Of Operation" HeaderText="Mode of Operation"  />
                    <asp:BoundField DataField="Account Number" HeaderText="Account Number"  />
                </Columns>
                
            </asp:GridView>
        </td>
    </tr>
</table>
--%>
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
