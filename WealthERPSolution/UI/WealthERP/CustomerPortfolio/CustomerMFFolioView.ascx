<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFFolioView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMFFolioView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style>
    .yellow-box
    {
        background-color: #FFFFE5;
        border: 1px solid #F5E082;
        padding: 10px;
    }
</style>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
</script>

<script type="text/javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }

    function CheckSelection() {

        var form = document.forms[0];

        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select atleast one folio to transfer.")
            return false;
        }
        if (document.getElementById("<%= txtCustomerId.ClientID %>").value == "") {
            alert("Please select a customer.");
            return false;
        }
        return true;
    }
</script>

<script type="text/javascript">

    function ShowAlertToDelete() {

        var bool = window.confirm('Are you sure you want to delete this MF Folio?');

        if (bool) {
            document.getElementById("ctrl_CustomerMFFolioView_hdnStatusValue").value = 1;
            document.getElementById("ctrl_CustomerMFFolioView_btnFolioAssociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_CustomerMFFolioView_hdnStatusValue").value = 0;
            document.getElementById("ctrl_CustomerMFFolioView_btnFolioAssociation").click();
            return true;
        }
    }

</script>

<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            View MF Folio
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="false" ID="imgBtnrgHoldings" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="20px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <%--
<tr>
    <td class="HeaderCell">
        <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="MF Folio View"></asp:Label>
        <hr />
    </td>
</tr>--%>
</table>
<table>
    <tr>
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<table width="100%" cellpadding="0" cellspacing="0" id="tblMessage" runat="server">
    <tr id="trFailure" runat="server" visible="false">
        <td align="center">
            <div id="statusMsgFailure" runat="server" visible="false" class="failure-msg" align="center">
            </div>
            <div id="statusMsgSuccess" runat="server" visible="false" class="success-msg" align="center">
            </div>
        </td>
    </tr>
</table>
<div style="width: 1100px; overflow: scroll">
    <telerik:RadGrid ID="gvMFFolio" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" OnItemDataBound="gvMFFolio_ItemDataBound" OnNeedDataSource="gvMFFolio_NeedDataSource">
        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
        </ExportSettings>
        <MasterTableView DataKeyNames="FolioId,Folio No" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="40px">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkBox" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                    HeaderStyle-Width="140px">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlAction" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged" Width="120px">
                            <Items>
                                <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                <asp:ListItem Text="View" Value="View" />
                                <asp:ListItem Text="Edit" Value="Edit" />
                                <asp:ListItem Text="Delete" Value="Delete" />
                            </Items>
                        </asp:DropDownList>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%-- <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="120px">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_SelectedIndexChanged"
                            CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                            AllowCustomText="true" AutoPostBack="true">
                            <Items>
                                <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="Select"
                                    Selected="true"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem Text="View" Value="View" ImageUrl="~/Images/DetailedView.png"
                                    runat="server"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                    runat="server"></telerik:RadComboBoxItem>
                                <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                    runat="server"></telerik:RadComboBoxItem>
                            </Items>
                        </telerik:RadComboBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn UniqueName="Folio No" HeaderStyle-Width="80px" HeaderText="Folio No."
                    DataField="Folio No" SortExpression="Folio No" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="ADUL_ProcessId" HeaderStyle-Width="80px" HeaderText="ProcessId"
                    DataField="ADUL_ProcessId" SortExpression="ADUL_ProcessId" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="AMC Name" HeaderStyle-Width="180px" HeaderText="AMC"
                    DataField="AMC Name" SortExpression="AMC Name" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Name" HeaderStyle-Width="100px" HeaderText="Name"
                    DataField="CMFA_INV_NAME" SortExpression="Name" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Mode Of Holding" HeaderStyle-Width="80px" HeaderText="Mode Of Holding"
                    DataField="Mode Of Holding" SortExpression="Mode Of Holding" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="A/C Opening Date" HeaderStyle-Width="120px"
                    DataFormatString="{0:d}" HeaderText="A/C Opening Date" DataField="A/C Opening Date"
                    SortExpression="A/C Opening Date" AllowFiltering="false" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
                <%-- <telerik:GridBoundColumn UniqueName="CMFA_IsOnline" HeaderStyle-Width="120px" 
                    HeaderText="ISOnline" DataField='<%# Eval if(CMFA_IsOnline==1) ?'YES':'No' %>'>
                    SortExpression="CMFA_IsOnline" AllowFiltering="false" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderText="ISOnline" ShowFilterIcon="false" AllowFiltering="true"
                    SortExpression="CMFA_IsOnline" UniqueName="CMFA_IsOnline" HeaderStyle-Width="100px"
                    DataField="CMFA_IsOnline" AutoPostBackOnFilter="true">
                    <HeaderStyle></HeaderStyle>
                </telerik:GridBoundColumn>
            </Columns>
            <HeaderStyle Width="110px" />
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="true" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<table class="TableBackground" style="width: 100%">
    <tr>
        <td>
            <%-- <asp:GridView ID="gvMFFolio" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" DataKeyNames="FolioId" HorizontalAlign="Center" ShowFooter="True" OnRowDataBound="gvMFFolio_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                <asp:ListItem Text="Select" Value="Select" />
                                <asp:ListItem Text="View" Value="View" />
                                <asp:ListItem Text="Edit" Value="Edit" />
                                <asp:ListItem Text="Delete" Value="Delete" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Folio No" HeaderText="Folio No" />
                    <asp:BoundField DataField="AMC Name" HeaderText="AMC Name" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Mode Of Holding" HeaderText="Mode Of Holding" />
                    <asp:BoundField DataField="A/C Opening Date" HeaderText="A/C Opening Date" />
                </Columns>
            </asp:GridView>--%>
        </td>
    </tr>
    <%--   <tr>
        <td>
            <div id="DivPager" runat="server" style="display: none">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>--%>
    <tr>
        <td>
            <%--<asp:Button ID="btnTransferFolio" runat="server" Text="Transfer Folio" CssClass="PCGMediumButton"
                OnClick="btnTransferFolio_Click" />--%>
        </td>
    </tr>
    <tr>
        <td>
            <%--<asp:Button ID="btnMoveFolio" runat="server" Text="Move Folio to Another Portfolio" CssClass="PCGLongButton"
        OnClick="btnMoveFolio_Click"/>--%>
        </td>
    </tr>
</table>
<table visible="false" class="TableBackground" width="100%">
    <tr id="trErrorMsg" runat="server">
        <td align="center">
            <asp:Label ID="lblMessage" Visible="false" runat="server" CssClass="Error" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
</table>
<table style="width: 100%">
    <tr id="trSelectAction" runat="server">
        <td style="width: 150px" align="right">
            <asp:Label ID="lblSelectAction" runat="server" CssClass="FieldName" Text="Select Action:"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAction_SelectedIndexChanged1">
                <asp:ListItem Value="0">Select Action</asp:ListItem>
                <asp:ListItem Value="TF">Transfer Folio</asp:ListItem>
                <asp:ListItem Value="MFtoAP">Move Folio to another Portfolio</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 150px">
        </td>
    </tr>
</table>
<table border="0" id="tblMoveFolio" runat="server" visible="false" style="border: solid 2px #8BA0BD;
    width: 100%">
    <tr id="trPickPortfolio" runat="server">
        <td align="right" style="width: 150px">
            <asp:Label ID="lblPickPortfolio" Text="Pick a Portfolio:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:DropDownList ID="ddlPickPortfolio" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*
                <asp:RequiredFieldValidator ID="rfvddlPickPortfolio" ControlToValidate="ddlPickPortfolio"
                    ErrorMessage="Please pick a portfolio" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationGroup="btnSubmitMoveFolio">
                </asp:RequiredFieldValidator>
            </span>
        </td>
        <td style="width: 150px">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="btnSubmitMoveFolio" runat="server" Text="Submit" OnClientClick="return CheckSelection()"
                CssClass="PCGButton" OnClick="btnSubmitMoveFolio_Click" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div runat="server" id="div2">
                <asp:Label ID="Label4" runat="server" Text="" CssClass="SuccessMsg"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<table border="0" id="tblTransferFolio" runat="server" visible="false" style="border: solid 2px #8BA0BD;
    width: 100%">
    <tr>
        <td style="width: 150px" align="right">
            <asp:Label ID="Label2" runat="server" Text="Customer Name :" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
            <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="true" OnTextChanged="txtCustomerId_ValueChanged">
            </asp:TextBox>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtCustomer" WatermarkText="Type the Customer Name" EnableViewState="false" />
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <span id="Span1" class="spnRequiredField">*</span> <span style='font-size: 8px; font-weight: normal'
                class='FieldName'>Enter few characters of customer name.</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
        <td style="width: 150px">
        </td>
    </tr>
    <tr id="trReassignBranch" runat="server">
        <td align="right" style="width: 150px">
            <asp:Label ID="Label3" Text="Transfer To:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:DropDownList ID="ddlAdvisorBranchList" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
            <%--<span id="spanAdvisorBranch" class="spnRequiredField" runat="server">*</span>--%>
        </td>
        <td style="width: 150px">
        </td>
    </tr>
    <tr id="trCustomerDetails" runat="server" visible="false">
        <td style="width: 150px" align="right">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:TextBox ID="txtPanParent" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox>
        </td>
        <td style="width: 150px" align="right">
        </td>
    </tr>
    <tr id="trCustomerAddress" runat="server" visible="false">
        <td style="width: 150px" align="right">
            <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Address:"></asp:Label>
        </td>
        <td style="width: 280px">
            <asp:TextBox ID="txtAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                BorderStyle="None"></asp:TextBox>
        </td>
        <td style="width: 150px" align="right">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                OnClientClick="return CheckSelection()" CssClass="PCGButton" />
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trTransferMsg" runat="server">
        <td colspan="2">
            <div runat="server" id="divMessage">
                <asp:Label ID="lblTransferMsg" runat="server" Text="" CssClass="SuccessMsg"></asp:Label>
            </div>
        </td>
    </tr>
</table>
<table class="TableBackgrounds" width="100%">
    <tr id="trFolioStatus" runat="server">
        <td align="center">
            <div id="msgFolioStatus" runat="server" class="success-msg" align="center">
                Folio Moved Successfully
            </div>
        </td>
    </tr>
</table>
<div id="Div1" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="Pager1" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnStatusValue" runat="server" />
<asp:Button ID="btnFolioAssociation" runat="server" BorderStyle="None" BackColor="Transparent"
    OnClick="btnFolioAssociation_Click" Style="height: 22px" />
