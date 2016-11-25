<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUploadNew.ascx.cs"
    Inherits="WealthERP.Uploads.CustomerUploadNew" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<style>
    .ajax__calendar_body
    {
        width: 280px;
        background-color: White;
        z-index: 10;
    }
    .ajax__calendar_container
    {
        width: 280px;
        background-color: White;
        z-index: 10;
    }
    #ctrl_CustomerUploadNew_gvbrokerageRecon_ctl00_ctl22_CalendarExtender2_daysTable
    {
        background-color: White;
    }
</style>

<script type="text/javascript">
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;

    }  
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvbrokerageRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "Ranjan";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            var chkIdAll = inputTypes[i].id;
            if (inputTypes[i].type == 'checkbox' && chkIdAll.indexOf(gvChkBoxControl) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    function checkAllRecBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvbrokerageRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkIdRec";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdRecAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            var chkIdAll = inputTypes[i].id;
            if (inputTypes[i].type == 'checkbox' && chkIdAll.indexOf(gvChkBoxControl) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
    function checkAllPayBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvbrokerageRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkIdPay";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdPayAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            var chkIdAll = inputTypes[i].id;
            if (inputTypes[i].type == 'checkbox' && chkIdAll.indexOf(gvChkBoxControl) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<style>
    .calender
    {
        visibility: hidden;
    }
</style>

<script>
    function toggle_visibility(id1, id2, id3) {
        var e1 = document.getElementById("ctrl_CustomerUploadNew_" + id1);
        var e2 = document.getElementById("ctrl_CustomerUploadNew_" + id2 + "_wrapper");
        var e3 = document.getElementById("ctrl_CustomerUploadNew_" + id3);
        e2.style.visibility = "hidden";
        e3.style.visibility = "hidden";
        e3.enabled = false;
        if (e1.checked) {
            e3.style.visibility = "visible";
            e3.enabled = true;
            e2.style.visibility = "visible";
        }
    }
    function toggle_visibility1(id1, id2) {
        var e1 = document.getElementById("ctrl_CustomerUploadNew_" + id1);
        var e2 = document.getElementById("ctrl_CustomerUploadNew_" + id2);
        e2.disabled = false;
        if (e1.checked) {
            e2.disabled = true;
        }
    }
</script>

<script type="text/javascript">
    function validation() {
        var grid = document.getElementById('<%= gvbrokerageRecon.ClientID %>');
        var inputTypes = grid.getElementsByTagName("input");
        for (var i = 0; i < inputTypes.length; i++) {

            var txtAmountReceive = $("input[id*=txtActRecBrokerage]")
            var cbRec = $("input[id*=chkIdRec]")
            var txtReceivedate = $("input[id*=txtPaybleDate]")
            document.getElementById(txtAmountReceive[i].id).readOnly = false;
            document.getElementById(txtReceivedate[i].id).value = document.getElementById(txtReceivedate[i].id).value.substring(0, 11);

            var txtpaydate = $("input[id*=txtRecDate]")

            document.getElementById(txtpaydate[i].id).value = document.getElementById(txtpaydate[i].id).value.substring(0, 11);

            if (document.getElementById(cbRec[i].id).checked == true) {
                document.getElementById(txtAmountReceive[i].id).readOnly = true;

            }
            var txtAmountPayble = $("input[id*=txtActPaybrokerage]")
            var cbPay = $("input[id*=chkIdPay]")
            document.getElementById(txtAmountPayble[i].id).readOnly = false;

            if (document.getElementById(cbPay[i].id).checked == true) {
                document.getElementById(txtAmountPayble[i].id).readOnly = true;

            }
        }
    }
</script>

<table width="100%">
    <tr>
        <td colspan="3">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Brokerage Reconciliation
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                            <asp:ImageButton ID="btnExportFilteredDupData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredDupData_OnClick"
                                Height="25px" Width="25px" Visible="false"></asp:ImageButton>
                        </td>
                      
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td align="left" width="13%" class="leftField">
            <asp:Label ID="lblSelectProduct" runat="server" Text=" Product:" CssClass="FieldName"></asp:Label>
        </td>
      <td width="20%">
            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="13%" id="tdCategory" runat="server" visible="false">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" id="tdDdlCategory" width="20%" runat="server" visible="false">
            <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProductCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlProductCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" width="13%" class="leftField">
        </td>
        <td width="20%">
        </td>
    </tr>
    <tr id="trSelectProduct" runat="server">
      
         <td  id="td1" align="left" width="13%"  runat="server" class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Commission Type:"></asp:Label>
       
        <td id="td2" width="20%" runat="server" visible="true">
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr id="trNCDIPO" runat="server" visible="false">
        <td align="left" width="13%" class="leftField">
            <asp:Label ID="lblIssueType" runat="server" CssClass="FieldName" Text="Issue Type:"></asp:Label>
        </td>
        <td width="20%">
            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlIssueType_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Closed Issues" Value="2"></asp:ListItem>
                <asp:ListItem Text="Current Issues" Value="1"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlIssueType" runat="server" ControlToValidate="ddlIssueType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue Type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select" Enabled="false"></asp:CompareValidator>
        </td>
        <td align="left" width="13%" class="leftField">
            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name:"></asp:Label>
        </td>
        <td width="53%">
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField" Width="500px">
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table width="100%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr id="trSelectMutualFund" runat="server" visible="false">
        <td align="left" width="13%" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select Product Type"></asp:CompareValidator>
        </td>
        <td width="20%">
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" width="13%" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td width="20%">
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" width="13%" class="leftField">
            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
        </td>
        <td width="20%">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
       
          <td  id="td3" align="left" width="13%"  runat="server" class="leftField">
            <asp:Label ID="lblDateFilterType" runat="server" CssClass="FieldName" Text="Date Filter Type:"></asp:Label>
       
        <td id="td4" width="20%" runat="server" visible="true">
       
            <asp:DropDownList ID="ddlDateFilterType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Order Date" Selected="True" Value="1"></asp:ListItem>
                <asp:ListItem Text="Received Date" Value="2"></asp:ListItem>
                <asp:ListItem Text="Payable Date" Value="3"></asp:ListItem>
            </asp:DropDownList>
        </td>
       </td>
        <td class="leftField" width="13%" align="left" id="tdTolbl" runat="server" visible="true">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Year:"></asp:Label>
        </td>
        <td class="rightField" width="20%" id="tdToDate" runat="server" visible="true">
            <asp:DropDownList ID="ddlYear" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlYear"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Year" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td id="tdFromDate" width="13%" class="leftField" runat="server" visible="true">
            <asp:Label ID="lblPeriod" Text="Month/Quarter:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="15%" id="tdFrom" runat="server" visible="true">
            <asp:DropDownList ID="ddlMnthQtr" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlMnthQtr"
                CssClass="rfvPCG" ErrorMessage="<br />Please Select Month" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField" width="16%">
            <asp:Label ID="lblOffline" runat="server" Text="Channel:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSelectMode" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Both" Value="2">
                </asp:ListItem>
                <asp:ListItem Text="Online-Only" Value="1"></asp:ListItem>
                <asp:ListItem Text="Offline-Only" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="rightField" width="5%">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="tblMessagee" runat="server" visible="false">
        <td align="center">
            <div id="divMessage" align="center">
            </div>
        </td>
    </tr>
</table>
<div style="margin-left: auto; margin-right: auto;">
    <%-- <div id="divMessage" align="center">
    </div>--%>
</div>
<div runat="server" style="overflow: scroll;" id="divBtnActionSection" visible="true">
    <telerik:RadGrid ID="gvbrokerageRecon" Visible="false" runat="server" GridLines="None"
        AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
        OnItemCreated="gvbrokerageRecon_ItemCreated" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AllowAutomaticInserts="false"
        OnNeedDataSource="gvbrokerageRecon_OnNeedDataSource" OnItemDataBound="gvbrokerageRecon_OnItemDataBound">
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="Brokerage Reconciliation">
        </ExportSettings>
        <MasterTableView Width="1200px" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" EditMode="PopUp" DataKeyNames="WCD_Id,IsRecLocked,IsPayLocked,RTABrokerageAmt">
            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                    HeaderStyle-Width="70px">
                    <HeaderTemplate>
                        <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="Ranjan" runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="CO_ApplicationNumber" AutoPostBackOnFilter="true"
                    HeaderText="Application Number" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="110px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="AIM_IssueName" AutoPostBackOnFilter="true"
                    HeaderText="Issue Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="AIM_IssueName" SortExpression="AIM_IssueName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="290px">
                    <ItemStyle Wrap="false" Width="290px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_AID_IssueDetailName" AutoPostBackOnFilter="true"
                    HeaderText="Series" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_AID_IssueDetailName" SortExpression="WCD_AID_IssueDetailName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="290px">
                    <ItemStyle Wrap="false" Width="290px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_AIIC_InvestorCatgeoryName" AutoPostBackOnFilter="true"
                    HeaderText="Catgeory" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_AIIC_InvestorCatgeoryName" SortExpression="WCD_AIIC_InvestorCatgeoryName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="290px">
                    <ItemStyle Wrap="false" Width="290px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_BrokerName" AutoPostBackOnFilter="true"
                    HeaderText="Broker Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_BrokerName" SortExpression="WCD_BrokerName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="200px">
                    <ItemStyle Wrap="false" Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_CustomerName" AutoPostBackOnFilter="true"
                    HeaderText="Customer Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_CustomerName" SortExpression="WCD_CustomerName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="200px">
                    <ItemStyle Wrap="false" Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="AA_ContactPersonName" AutoPostBackOnFilter="true"
                    HeaderText="Associate Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="200px">
                    <ItemStyle Wrap="false" Width="200px" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="AAC_AgentCode" AutoPostBackOnFilter="true"
                    HeaderText="Agent Code" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="CommissionType" AutoPostBackOnFilter="true"
                    HeaderText="Brokerage Type" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="CommissionType" SortExpression="CommissionType" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_Rec_rate" AutoPostBackOnFilter="true"
                    HeaderText="Brokerage Rate (Receivable)" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_Rec_rate" SortExpression="WCD_Rec_rate" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="NetRec_brokerage" AutoPostBackOnFilter="true"
                    HeaderText="System Calculated (Receivable)" ShowFilterIcon="false" Aggregate="Sum"
                    CurrentFilterFunction="Contains" UniqueName="NetRec_brokerage" SortExpression="NetRec_brokerage"
                    FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                    <ItemStyle Wrap="false" Width="150px" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="RTA_Rate" AutoPostBackOnFilter="true"
                    HeaderText="RTA Brokerage Rate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="RTA_Rate" SortExpression="RTA_Rate" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="RTABrokerageAmt" AutoPostBackOnFilter="true"
                    HeaderText="RTA Brokerage Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="RTABrokerageAmt" SortExpression="RTABrokerageAmt" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridCalculatedColumn AllowFiltering="true" DataFields="NetRec_brokerage,RTABrokerageAmt" UniqueName="Difference" HeaderText="Difference" DataType ="System.Decimal"
                                    Expression="{0}-{1}"   AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="90px"
                                    ShowFilterIcon="false" Visible="true"   Aggregate="Sum">
                                    <ItemStyle Wrap="false"  Width="" HorizontalAlign="Right"  />
                                </telerik:GridCalculatedColumn>
                <telerik:GridTemplateColumn AllowFiltering="true" DataField="WCD_Act_Rec_Brokerage"
                    AutoPostBackOnFilter="true" HeaderText="Actual received" ShowFilterIcon="false"
                    Aggregate="Sum" UniqueName="WCD_Act_Rec_Brokerage" SortExpression="WCD_Act_Rec_Brokerage"
                    FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtActRecBrokerage" CssClass="txtField" runat="server" Text='<%# Bind("WCD_Act_Rec_Brokerage") %>'></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" CssClass="rfvPCG" ControlToValidate="txtActRecBrokerage"
                            Display="Dynamic" ErrorMessage="Invalid Amount" ValidationGroup="btnSave" Operator="DataTypeCheck"
                            Type="Double">
                        </asp:CompareValidator>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="true" UniqueName="IsRecLocked" DataField="IsRecLocked"
                    HeaderStyle-Width="110px" HeaderText="Lock Received">
                    <HeaderTemplate>
                        <input id="chkIdRecAll" name="chkIdRecAll" type="checkbox" onclick="checkAllRecBoxes()" /><br /> Lock Received
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIdRec" runat="server" Checked='<%# Bind("IsRecLocked") %>' OnClick="return validation();" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actual Received Date"
                    UniqueName="actionRec" DataField="WCD_Act_Rec_BrokerageDate" ItemStyle-Width="110px"
                    HeaderStyle-Width="110px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRecDate" MaxLength="10" runat="server" Text='<%# Bind("WCD_Act_Rec_BrokerageDate") %>'
                            CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtRecDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtRecDate"
                            WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>            
         
                        <asp:CompareValidator ID="CompareValidator7" runat="server" CssClass="rfvPCG" ControlToValidate="txtRecDate"
                            Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnSave" Operator="DataTypeCheck"
                            Type="Date" >
                        </asp:CompareValidator>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_Rate" AutoPostBackOnFilter="true"
                    HeaderText="Brokerage Rate (Payable)" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_Rate" SortExpression="WCD_Rate" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="NetPay_brokerage" AutoPostBackOnFilter="true"
                    HeaderText="System Calculated (Payable)" ShowFilterIcon="false" Aggregate="Sum"
                    CurrentFilterFunction="Contains" UniqueName="NetPay_brokerage" SortExpression="NetPay_brokerage"
                    FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="100px">
                    <ItemStyle Wrap="false" Width="150px" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="true" DataField="WCD_Act_Pay_brokerage"
                    AutoPostBackOnFilter="true" HeaderText="Actual Payout" ShowFilterIcon="false"
                    Aggregate="Sum" CurrentFilterFunction="Contains" SortExpression="WCD_Act_Pay_brokerage"
                    UniqueName="WCD_Act_Pay_brokerage" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtActPaybrokerage" CssClass="txtField" runat="server" Text='<%# Bind("WCD_Act_Pay_brokerage") %>'></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator8" runat="server" CssClass="rfvPCG" ControlToValidate="txtActPaybrokerage"
                            Display="Dynamic" ErrorMessage="Invalid Amount" ValidationGroup="btnSave" Operator="DataTypeCheck"
                            Type="Double">
                        </asp:CompareValidator>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="lock Payout" UniqueName="IsPayLocked"
                    DataField="IsPayLocked" HeaderStyle-Width="110px">
                    <HeaderTemplate>
                        <input id="chkIdPayAll" name="chkIdPayAll" type="checkbox"  onclick="checkAllPayBoxes()" /><br />lock Payout
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkIdPay" Checked='<%# Bind("IsPayLocked") %>' OnClick="return validation();"
                            runat="server" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Actual Payout Date"
                    UniqueName="actionPay" DataField="WCD_Act_Pay_BrokerageDate" ItemStyle-Width="290px"
                    HeaderStyle-Width="290px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPaybleDate" MaxLength="10" Text='<%# Bind("WCD_Act_Pay_BrokerageDate") %>'
                            runat="server" CssClass="txtField"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPaybleDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" TargetControlID="txtPaybleDate"
                            WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" CssClass="rfvPCG" ControlToValidate="txtPaybleDate"
                            Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnSave" Operator="DataTypeCheck"
                            Type="Date">
                        </asp:CompareValidator>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="PerDayAsset" AutoPostBackOnFilter="true"
                    HeaderText="Per Day Asset" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="PerDayAsset" SortExpression="PerDayAsset" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="CumulativeNAV" AutoPostBackOnFilter="true"
                    HeaderText="Cumulative NAV" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="CumulativeNAV" SortExpression="CumulativeNAV" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="ClosingNAV" AutoPostBackOnFilter="true"
                    HeaderText="Closing NAV" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="ClosingNAV" SortExpression="ClosingNAV" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="TotalNAV" AutoPostBackOnFilter="true"
                    HeaderText="Total NAV" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="TotalNAV" SortExpression="TotalNAV" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<table width="90%" class="TableBackground" cellspacing="0" cellpadding="2" id="tblUpdate"
    runat="server" visible="false">
    <tr>
        <td>
            <asp:CheckBox ID="chkBulkPayble" Visible="true" Text="Copy System Calculated payout to Actual"
                CssClass="cmbFielde" runat="server" />
            <br />
            <asp:CheckBox ID="chkBulkReceived" Visible="true" Text="Copy RTA rcvd to Actual received"
                CssClass="cmbFielde" runat="server" onclick="toggle_visibility1('chkBulkReceived','chkBulkReceivedSys');" />
            <br />
            <asp:CheckBox ID="chkBulkReceivedSys" Visible="true" Text="Copy System Calculated rcvd to Actual received"
                CssClass="cmbFielde" runat="server" onclick="toggle_visibility1('chkBulkReceivedSys','chkBulkReceived');" />
        </td>
        <td>
            <asp:CheckBox ID="chkBulkReceivedDate" Visible="true" Text="Copy Received Date" onclick="toggle_visibility('chkBulkReceivedDate','rdpBulkReceivedDate','RequiredFieldValidator4');"
                CssClass="cmbFielde" runat="server" />
            <br />
            <asp:CheckBox ID="chkBulkPayableDate" Visible="true" Text="Copy Payable Date" CssClass="cmbFielde"
                onclick="toggle_visibility('chkBulkPayableDate','rdpBulkPayableDate','RequiredFieldValidator2');"
                runat="server" />
        </td>
        <td>
            <telerik:RadDatePicker ID="rdpBulkReceivedDate" DateInput-EmptyMessage="Please Select a Date"
                MinDate="01/01/1000" MaxDate="01/01/3000" Label="Received Date" CssClass="calender"
                runat="server">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="rdpBulkReceivedDate"
                ErrorMessage="<br />Please Select a Date" Display="Dynamic" runat="server" Enabled="false"
                CssClass="rfvPCG" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator10" runat="server" CssClass="rfvPCG" ControlToValidate="rdpBulkReceivedDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnSave" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <br />
            <telerik:RadDatePicker ID="rdpBulkPayableDate" Label="Payable Date" DateInput-EmptyMessage="Please Select a Date"
                MinDate="01/01/1000" MaxDate="01/01/3000" CssClass="calender" runat="server">
            </telerik:RadDatePicker>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="rdpBulkPayableDate"
                ErrorMessage="<br />Please Select a Date" Display="Dynamic" runat="server" Enabled="false"
                CssClass="rfvPCG" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator11" runat="server" CssClass="rfvPCG" ControlToValidate="rdpBulkPayableDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnSave" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
          <td>
        <asp:LinkButton ID="lnkEdit" Text="Edit" Visible="false" runat="server" CssClass="LinkButtons"  OnClick="lnkEdit_Click" Font-Bold="true" Font-Size="Large"/>
          </td>
        <td>
            <asp:Button ID="btnSave" CssClass="PCGButton" runat="server" Text="Update" Visible="true"
                OnClick="btnSave_Click" ValidationGroup="btnSave" />
        </td>
    </tr>
</table>

<script>

    $(document).ready(function() {
        validation();

    });
</script>

