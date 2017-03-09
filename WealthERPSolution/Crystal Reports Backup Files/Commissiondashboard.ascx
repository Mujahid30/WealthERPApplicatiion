<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Commissiondashboard.ascx.cs"
    Inherits="WealthERP.BusinessMIS.Commissiondashboard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.bxslider.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>

<script type="text/javascript">
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;  
</script>

<div id="dvAddMandate" runat="server" visible="false">
    <table width="100%">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table cellspacing="0" cellpadding="2" width="100%">
                        <tr>
                            <td align="left">
                                Add Bank Mandate
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table class="tblMessage" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center">
                <div id="divMessage" align="center">
                </div>
                <div style="clear: both">
                </div>
            </td>
        </tr>
    </table>
    <table width="100%" style="margin-top: 3%">
        <tr>
            <td class="leftField">
                <asp:Label ID="lblCustCode" runat="server" Text="Enter CustCode:" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left" id="tdtxtClientCode" runat="server">
                <asp:TextBox ID="txtClientCode" runat="server" CssClass="txtField" AutoComplete="Off"
                    AutoPostBack="True" onclientClick="ShowIsa()" Width="250px"></asp:TextBox>
                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" TargetControlID="txtClientCode"
                    WatermarkText="Enter few characters of Client Code" runat="server" EnableViewState="false">
                </cc1:TextBoxWatermarkExtender>
                <ajaxToolkit:AutoCompleteExtender ID="txtClientCode_autoCompleteExtender" runat="server"
                    TargetControlID="txtClientCode" ServiceMethod="GetCustCode" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                    MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                    CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                    CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                    UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                    Enabled="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtClientCode"
                    ErrorMessage="<br />Please Enter Client Code" Display="Dynamic" runat="server"
                    CssClass="rfvPCG" ValidationGroup="btnMandateSubmit"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                <asp:Label ID="label2" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblgetcust" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>
            <td align="right">
                <asp:Label ID="lblPan" runat="server" Text="PAN:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblgetPan" runat="server" Text="" CssClass="FieldName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="leftField">
                <asp:Label ID="lblAmount" runat="server" Text="Enter Amount:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="txtField" AutoComplete="Off" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAmount"
                    ErrorMessage="Please Enter the amount" Display="Dynamic" ValidationGroup="btnMandateSubmit"
                    runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" ID="rexNumber" ControlToValidate="txtAmount"
                    ValidationExpression="^[1-9]\d*$" ErrorMessage="Please enter number!" Display="Dynamic"
                    ValidationGroup="btnMandateSubmit" CssClass="rfvPCG" />
            </td>
            <td class="leftField">
                <asp:Label ID="lblBankName" runat="server" Text="Enter Bank Name:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
             <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true" >
                        </asp:DropDownList>
                     
                        
             
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="ddlBankName"
                    ErrorMessage="Please Enter the Bank Name" Display="Dynamic" ValidationGroup="btnMandateSubmit"
                    runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
            </td>
            <td class="leftField">
                <asp:Label ID="lblBankBranch" runat="server" Text="Enter Bank Branch:" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBBranch" runat="server" CssClass="txtField" AutoComplete="Off" 
                    MaxLength="40" Width="220px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtBBranch"
                    ErrorMessage="Please Enter the Bank Branch Name" Display="Dynamic" ValidationGroup="btnMandateSubmit"
                    runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
            </td>
            </tr>
            <tr>
             <td  class="leftField">
                    <asp:Label ID="lblBankAccount" Text="Bank Account No.:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td  class="rightField">
                    <asp:TextBox ID="txtBankAccount" runat="server" CssClass="txtField" onkeydown="return (event.keyCode!=13);"
                        OnKeypress="javascript:return isNumberKey(event);" MaxLength="16" TabIndex="15"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegtxtBankAccount" runat="server" Display="Dynamic"
                        ValidationGroup="btnConfirmOrder" ErrorMessage="<br/>Please Enter Numeric" ControlToValidate="txtBankAccount"
                        CssClass="rfvPCG" ValidationExpression="^([0-9]*[0-9])\d*$">
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftField">
                 <asp:Label ID="lblIFSC" Text="IFSC Code:" runat="server" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                 <asp:TextBox ID="txtIFSC" runat="server" CssClass="txtField" AutoComplete="Off" Width="220px" />
                   <asp:RequiredFieldValidator ID="rfvIFSC" ControlToValidate="txtIFSC"
                    ErrorMessage="Please Enter the IFSC Code" Display="Dynamic" ValidationGroup="btnMandateSubmit"
                    runat="server" CssClass="rfvPCG">
                </asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <td class="leftField">
            </td>
            <td>
                <asp:Button ID="btnMandateSubmit" runat="server" Text="Submit" CssClass="PCGButton"
                    ValidationGroup="btnMandateSubmit" OnClick="btnMandateSubmit_Click" />
            </td>
        </tr>
        <tr id="trNewOrder" runat="server" visible="false">
            <td align="center" colspan="4">
                <asp:LinkButton ID="lnkMandateOrder" CausesValidation="false" Text="Create Another Mandate"
                    runat="server" OnClick="lnkMandateOrder_Click" CssClass="LinkButtons"></asp:LinkButton>
            </td>
        </tr>
    </table>
</div>
<div id="dvViewMandateMis" runat="server" visible="false">
    <table id="tblClusterZone" runat="server" width="100%" class="TableBackground">
        <tr>
            <td>
                <div class="divPageHeading">
                    <table cellspacing="0" cellpadding="3" width="100%">
                        <tr>
                            <td align="left">
                                Mandate Details
                            </td>
                            <td align="right">
                                <asp:ImageButton Visible="false" ID="btnExportFilteredMandatedetails" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredMandatedetails_OnClick"
                                    OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 8px;">
                <asp:Panel ID="pnlZoneCluster" ScrollBars="Horizontal" runat="server" >
                    <div runat="server" id="divZoneCluster" visible="true" style="margin: 2px; width: 640px;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                            <ContentTemplate>
                                <telerik:RadGrid ID="gvMandatedetails" runat="server" CssClass="RadGrid" GridLines="None"
                                    enableloadondemand="True" Width="195%" AllowSorting="True" PagerStyle-AlwaysVisible="true"
                                    AllowPaging="true" AutoGenerateColumns="false" ShowStatusBar="true" PageSize="10"
                                    AllowAutomaticInserts="false" AllowAutomaticUpdates="false" Skin="Telerik" OnNeedDataSource="gvMandatedetails_NeedDataSource"
                                    EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                                    AllowFilteringByColumn="true" ShowFooter="false">
                                    <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="Mandatedetails">
                                    </ExportSettings>
                                    <MasterTableView ShowGroupFooter="true" EditMode="EditForms" GroupLoadMode="Client"
                                        CommandItemSettings-ShowRefreshButton="false">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="C_CustCode" HeaderStyle-Width="120px" HeaderText="Client Code"
                                                DataField="C_CustCode" SortExpression="C_CustCode" AllowFiltering="true" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true" Visible="false">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="C_FirstName" HeaderStyle-Width="120px" HeaderText="Customer Name"
                                                DataField="C_FirstName" SortExpression="C_FirstName" AllowFiltering="true" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true" Aggregate="Count" FooterText="Row Count : ">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="C_PANNum" HeaderStyle-Width="120px" HeaderText="PAN"
                                                DataField="C_PANNum" SortExpression="C_PANNum" AllowFiltering="true" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CMFOD_Amount" HeaderStyle-Width="120px" HeaderText="Amount"
                                                DataField="CMFOD_Amount" SortExpression="CMFOD_Amount" AllowFiltering="true"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="WCMV_Name" HeaderStyle-Width="120px" HeaderText="Bank Name"
                                                DataField="WCMV_Name" SortExpression="WCMV_Name" AllowFiltering="true"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CMFOD_BranchName" HeaderStyle-Width="120px"
                                                HeaderText="Branch Name" DataField="CMFOD_BranchName" SortExpression="CMFOD_BranchName"
                                                AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CMFOD_MandateId" HeaderStyle-Width="180px" HeaderText="Mandate Id"
                                                DataField="CMFOD_MandateId" SortExpression="CMFOD_MandateId" AllowFiltering="true" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CreatedBy" HeaderStyle-Width="100px" HeaderText="Created By"
                                                DataField="CreatedBy" SortExpression="CreatedBy" AllowFiltering="true" ShowFilterIcon="false"
                                                AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CO_CreatedOn" HeaderStyle-Width="100px" HeaderText="Created On"
                                                DataField="CO_CreatedOn" SortExpression="CO_CreatedOn" AllowFiltering="true"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn UniqueName="CMFOD_IFSCCode" HeaderStyle-Width="100px" HeaderText="IFSC Code"
                                                DataField="CMFOD_IFSCCode" SortExpression="CMFOD_IFSCCode" AllowFiltering="true"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                                <HeaderStyle></HeaderStyle>
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn UniqueName="CMFOD_BankACCNo" HeaderStyle-Width="100px" HeaderText="Bank AccountNo"
                                                DataField="CMFOD_BankACCNo" SortExpression="CMFOD_BankACCNo" AllowFiltering="true"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
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
                            </ContentTemplate>
                            <Triggers>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
