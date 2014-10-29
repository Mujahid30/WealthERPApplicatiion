<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerProfileAudit.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.CustomerProfileAudit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%=  hdnCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
    function GetSchemePlanCode(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%=  hdnschemePlanId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
    function GetStaffId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%=  hdnStaffId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
    function GetAssociateId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%=  hdnAssociateId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Customer Profile Audit
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="50%">
    <tr>
        <td class="leftField" width="25%">
            <asp:Label ID="lblType" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="select"></asp:ListItem>
                <asp:ListItem Text="Customer Profile" Value="CustomerProfile"></asp:ListItem>
                <asp:ListItem Text="Scheme" Value="Schemeplan"></asp:ListItem>
                <asp:ListItem Text="Staff" Value="StaffDetails"></asp:ListItem>
                <asp:ListItem Text="Associate" Value="AssociateDetails"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table width="90%">
    <tr id="trSchemePlan" runat="server" visible="false">
        <td class="leftField" width="14%">
            <asp:Label ID="lblSchemePlan" runat="server" Text="SchemePlan Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 40px; height: 10px">
            <asp:TextBox ID="txtSchemeName" runat="server" CssClass="txtField" AutoPostBack="True"
                AutoComplete="Off" Width="300px"></asp:TextBox>
            <%-- <span id="Span5" class="spnRequiredField">*</span>--%>
            <cc1:TextBoxWatermarkExtender ID="txtSchemeName_water" TargetControlID="txtSchemeName"
                WatermarkText="Enter Three Characters" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtSchemeName_AutoCompleteExtender" runat="server"
                TargetControlID="txtSchemeName" ServiceMethod="GetSchemeName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetSchemePlanCode" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtSchemeName"
                ErrorMessage="<br />Please Enter Scheme Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trStaff" runat="server" visible="false">
        <td class="leftField" width="14%">
            <asp:Label ID="lblStaff" runat="server" Text="Staff Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" style="width: 10px; height: 10px">
            <asp:TextBox ID="txtStaffName" runat="server" CssClass="txtField" AutoPostBack="True"
                AutoComplete="Off" Width="300px"></asp:TextBox>
            <%--<span id="Span7" class="spnRequiredField">*</span>--%>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender3" TargetControlID="txtStaffName"
                WatermarkText="Enter Three Characters" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtStaffName_AutoCompleteExtender" runat="server"
                TargetControlID="txtStaffName" ServiceMethod="GetStaffName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetStaffId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtStaffName"
                ErrorMessage="<br />Please Enter Staff Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trAssociates" runat="server" visible="false">
        <td class="leftField" width="150px" align="left">
            <asp:Label ID="lblAssociates" runat="server" Text="Select:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlAssociateOption" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlAssociateOption_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select" Selected="true" />
                <asp:ListItem Text="Name" Value="Name" />
                <asp:ListItem Text="SubBroker Code" Value="SubBrokerCode" />
            </asp:DropDownList>
            <span id="Span9" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="</br>Please Select Filter"
                CssClass="rfvPCG" ControlToValidate="ddlAssociateOption" ValidationGroup="BtnGo"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td class="rightField" id="tdtxtAssociateName" runat="server" visible="false">
            <asp:TextBox ID="txtAssociateName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" Width="300px">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender4" TargetControlID="txtAssociateName"
                WatermarkText="Enter Three Characters of Associates" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtAssociateName_AutoCompleteExtender" runat="server"
                TargetControlID="txtAssociateName" ServiceMethod="GetAssociateName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="3" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetAssociateId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtAssociateName"
                ErrorMessage="<br />Please Enter Associate Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
        <td class="rightField" id="tdtxtSubBrokerCode" runat="server" visible="false">
            <asp:TextBox ID="txtSubbrokerCode" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" Width="300px">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender5" TargetControlID="txtSubbrokerCode"
                WatermarkText="Enter Characters of Associates" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtSubBrokerCode_AutoCompleteExtender" runat="server"
                TargetControlID="txtSubbrokerCode" ServiceMethod="GetAgentCodeDetails" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetAssociateId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtSubbrokerCode"
                ErrorMessage="<br />Please Enter Subbroker Code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
        <td class="rightField" style="width: 300px">
        </td>
    </tr>
    <tr id="trCustomer" runat="server" visible="false">
        <%-- <td id="tdCustomer" runat="server" visible="false">--%>
        <td class="leftField" width="10%">
            <asp:Label ID="lblIskyc" runat="server" Text="Select:" Width="50px" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="17%">
            <asp:DropDownList ID="ddlCOption" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCOption_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select" Selected="true" />
                <asp:ListItem Text="Name" Value="Name" />
                <asp:ListItem Text="PAN" Value="Panno" />
                <asp:ListItem Text="Client Code" Value="Clientcode" />
            </asp:DropDownList>
            <span id="Span6" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rFVddlCOption" runat="server" ErrorMessage="</br>Please Select Filter"
                CssClass="rfvPCG" ControlToValidate="ddlCOption" ValidationGroup="BtnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" id="tdtxtPansearch" width="21%" runat="server" visible="false">
            <asp:TextBox ID="txtPansearch" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" TabIndex="2" Width="250px">
            </asp:TextBox><span id="Span1" class="spnRequiredField"></span>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" TargetControlID="txtPansearch"
                WatermarkText="Enter few characters of Pan" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtPansearch_autoCompleteExtender" runat="server"
                TargetControlID="txtPansearch" ServiceMethod="GetAdviserCustomerPan" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="1" CompletionInterval="0"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtPansearch"
                ErrorMessage="<br />Please Enter Pan number" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
        <td class="rightField" id="tdtxtClientCode" width="21%" runat="server" visible="false">
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
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtClientCode"
                ErrorMessage="<br />Please Enter Client Code" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
        <td class="leftField" id="tdtxtCustomerName" width="21%" runat="server" visible="false">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" Width="250px">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCustomerName_water" TargetControlID="txtCustomerName"
                WatermarkText="Enter Three Characters of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="3" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="BtnGo"></asp:RequiredFieldValidator>
        </td>
        <td id="tdCustomerAuditList" runat="server" visible="false" class="leftField" width="8%">
            <asp:Label ID="lblFilterType" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="30%">
            <asp:DropDownList ID="ddlAuditType" runat="server" CssClass="cmbField" Width="235px">
                <asp:ListItem Text="Select" Value="select"></asp:ListItem>
                <asp:ListItem Text="Customer Profile" Value="CP"></asp:ListItem>
                <asp:ListItem Text="Customer Bank" Value="CB"></asp:ListItem>
                <asp:ListItem Text="Customer Demat" Value="CD"></asp:ListItem>
                <asp:ListItem Text="Customer Demat Association" Value="CDA"></asp:ListItem>
                <asp:ListItem Text="Customer Transaction" Value="CTA"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select a Type Audit"
                Display="Dynamic" ControlToValidate="ddlAuditType" Text="Please select a Type Audit"
                CssClass="rfvPCG" ValidationGroup="BtnGo" InitialValue="select">Please select a Type Audit</asp:RequiredFieldValidator>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td id="tdFromDate" runat="server" width="13%" visible="false" class="leftField">
            <asp:Label ID="lblModificationDate" runat="server" Text="From:" CssClass="FieldName"></asp:Label>
        </td>
        <td id="tdFromDate1" runat="server" visible="false" class="rightField" style="width: 170px;
            height: 20px">
            <telerik:RadDatePicker ID="rdpFromModificationDate" CssClass="txtField" runat="server"
                AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RFVDModificationDate" runat="server" ErrorMessage="Please select a valid date"
                Display="Dynamic" ControlToValidate="rdpFromModificationDate" Text="Please select a valid date"
                CssClass="rfvPCG" ValidationGroup="BtnGo">Please select a valid date</asp:RequiredFieldValidator>
        </td>
        <td style="width: 2px;">
        </td>
        <td id="tdTodate" runat="server" visible="false" class="leftField">
            <asp:Label ID="lblToDate" runat="server" Text="To:" CssClass="FieldName"></asp:Label>
        </td>
        <td id="tdTodate1" runat="server" visible="false" class="rightField">
            <telerik:RadDatePicker ID="rdpToDate" CssClass="txtField" runat="server" AutoPostBack="false"
                Skin="Telerik" EnableEmbeddedSkins="false">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select a valid date"
                Display="Dynamic" ControlToValidate="rdpToDate" Text="Please select a valid date"
                CssClass="rfvPCG" ValidationGroup="BtnGo">Please select a valid date</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="rdpToDate"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="rdpFromModificationDate" CssClass="cvPCG" ValidationGroup="btnViewOrder"
                Display="Dynamic">
            </asp:CompareValidator>
            <asp:Button ID="btnSubmit" runat="server" Text="Go" CssClass="PCGButton" OnClick="btnSubmit_Click"
                ValidationGroup="BtnGo" Visible="false" />
        </td>
    </tr>
</table>
<table id="tblProfileHeading" runat="server" visible="false" style="width: 100%"
    cellpadding="2" cellspacing="5">
    <tr>
        <td class="tdSectionHeading">
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server" Text="Profile Audit Details"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport1" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick1"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblProfileData" runat="server" visible="false" width="250%">
    <tr>
        <td>
            <asp:Panel ID="pnlCustomerProfile" runat="server" Width="40%" ScrollBars="Horizontal"
                runat="server" Visible="true">
                <telerik:RadGrid ID="rdCustomerProfile" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
                    GridLines="Both" EnableEmbeddedSkins="false" Width="100%" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
                    OnNeedDataSource="rdCustomerProfile_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="140%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="C_CustomerId" SortExpression="C_CustomerId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="System Id" UniqueName="C_CustomerId" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_FirstName" SortExpression="C_FirstName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="First Name" UniqueName="C_FirstName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_MiddleName" SortExpression="C_MiddleName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Middle Name" UniqueName="C_MiddleName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_LastName" SortExpression="C_LastName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Last Name" UniqueName="C_LastName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_PANNum" SortExpression="C_PANNum" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="PAN" UniqueName="C_PANNum" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_Mobile1" SortExpression="C_Mobile1" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Mobile No." Visible="true" UniqueName="C_Mobile1" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RealInvestor" SortExpression="RealInvestor" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="RealInvestor" Visible="true" UniqueName="RealInvestor" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_ResPhoneNum" SortExpression="C_ResPhoneNum"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Phone No" Visible="false" UniqueName="C_ResPhoneNum"
                                HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_Email" SortExpression="C_Email" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Email Id" UniqueName="C_Email" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="[Address]" SortExpression="[Address]" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Address" UniqueName="[Address]" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_Adr1PinCode" SortExpression="C_Adr1PinCode"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="PinCode" UniqueName="C_Adr1PinCode" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="city" SortExpression="city" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="City" UniqueName="city" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="state" SortExpression="state" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="State" UniqueName="state" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_IsKYCAvailable" SortExpression="C_IsKYCAvailable"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Is KYC Available" UniqueName="C_IsKYCAvailable"
                                HeaderStyle-Width="17px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_DOB" DataFormatString="{0:dd/MM/yyyy}" AllowFiltering="true"
                                HeaderText="D.O.B" UniqueName="C_DOB" SortExpression="C_DOB" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="20px"
                                FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                AllowFiltering="true" HeaderText="Modiication Date/Time" UniqueName="ModiicationDateTime"
                                SortExpression="ModiicationDateTime" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="20px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <%-- <Scrolling AllowScroll="true" />--%>
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblCustomerBankHeading" runat="server" visible="false" style="width: 100%"
    cellpadding="2" cellspacing="5">
    <tr>
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblBankDetails" runat="server" Text="Bank Audit Details"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport2" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick2"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblCustomerBank" runat="server" visible="false" width="250%">
    <tr>
        <td>
            <asp:Panel ID="PnlCustomerBank" class="Landscape" runat="server" Width="40%" ScrollBars="Horizontal"
                Visible="true">
                <telerik:RadGrid ID="rdCustomerBank" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true" GridLines="Both"
                    EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" Width="100%" AllowFilteringByColumn="true"
                    PageSize="10" OnNeedDataSource="rdCustomerBank_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="140%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_CustomerId" SortExpression="C_CustomerId" UniqueName="C_CustomerId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="System Id" HeaderStyle-Width="10px" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_AccountNum" SortExpression="CB_AccountNum"
                                UniqueName="CB_AccountNum" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="Account Number" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Bank_Name" SortExpression="Bank_Name" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Bank Name" UniqueName="Bank_Name" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AccountType" SortExpression="AccountType" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Account Type" UniqueName="AccountType" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XMOH_ModeOfHolding" SortExpression="XMOH_ModeOfHolding"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Mode of Holding" UniqueName="XMOH_ModeOfHolding"
                                HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BranchName" SortExpression="CB_BranchName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Branch Name" UniqueName="CB_BranchName" HeaderStyle-Width="18px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BankCity" SortExpression="CB_BankCity" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Bank City" UniqueName="CB_BankCity" HeaderStyle-Width="9px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Bank_Address" SortExpression="Bank_Address" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Bank Address" UniqueName="Bank_Address" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_BranchAdrPinCode" SortExpression="CB_BranchAdrPinCode"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="PinCode" UniqueName="CB_BranchAdrPinCode"
                                HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_IFSC" SortExpression="CB_IFSC" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="IFSC" UniqueName="CB_IFSC" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_Balance" SortExpression="CB_Balance" UniqueName="CB_Balance"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Balance" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_MICR" SortExpression="CB_MICR" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="MICR" UniqueName="CB_MICR" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_IsHeldJointly" SortExpression="CB_IsHeldJointly"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="IsHeldJointly" UniqueName="CB_IsHeldJointly"
                                HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_NEFT" SortExpression="CB_NEFT" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="NEFT" UniqueName="CB_NEFT" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CB_RTGS" SortExpression="CB_RTGS" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="RTGS" UniqueName="CB_RTGS" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XBAT_BankAccountTye" SortExpression="XBAT_BankAccountTye"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Bank Account Type" UniqueName="XBAT_BankAccountTye"
                                HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modiication Date/Time" UniqueName="ModiicationDateTime"
                                HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <%-- <Scrolling AllowScroll="true" />--%>
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblCustomerDematHeading" runat="server" visible="false" style="width: 100%"
    cellpadding="2" cellspacing="5">
    <tr>
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label4" runat="server" Text="Demat Audit Details"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport3" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick3"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<%--<table id="tblCustomerDemat" runat="server" visible="false">
    <tr>
        <td>--%>
<asp:Panel ID="pnlCustomerDemat" runat="server" Width="100%" ScrollBars="Horizontal"
    Visible="false">
    <telerik:RadGrid ID="rdCustomerDemat" runat="server" AutoGenerateColumns="false"
        AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
        GridLines="Both" EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
        EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
        OnNeedDataSource="rdCustomerDemat_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="110%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            HeaderStyle-Width="120px">
            <Columns>
                <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                    HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CEDA_DPId" SortExpression="CEDA_DPId" UniqueName="CEDA_DPId"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    HeaderText="DP Id" HeaderStyle-Width="15px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CEDA_DPName" SortExpression="CEDA_DPName" UniqueName="CEDA_DPName"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    HeaderText="DP Name" HeaderStyle-Width="15px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CEDA_DepositoryName" SortExpression="CEDA_DepositoryName"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    AllowFiltering="false" HeaderText="Depository Name" UniqueName="CEDA_DepositoryName"
                    HeaderStyle-Width="20px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="AccountType" SortExpression="AccountType" AutoPostBackOnFilter="true"
                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                    HeaderText="AccountType" UniqueName="AccountType" HeaderStyle-Width="20px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CEDA_DPClientId" SortExpression="CEDA_DPClientId"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    AllowFiltering="false" HeaderText="Beneficiary Account Number" UniqueName="CEDA_DPClientId"
                    HeaderStyle-Width="20px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="XMOH_ModeOfHolding" SortExpression="XMOH_ModeOfHolding"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    AllowFiltering="false" HeaderText="Mode Of Holding" UniqueName="XMOH_ModeOfHolding"
                    HeaderStyle-Width="16px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                    AllowFiltering="false" HeaderText="Modiication Date/Time" UniqueName="ModificationBy"
                    HeaderStyle-Width="20px">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
        <FilterMenu EnableEmbeddedSkins="false">
        </FilterMenu>
    </telerik:RadGrid>
</asp:Panel>
<%--    </td>
    </tr>
</table>--%>
<table id="tblCustomerDematAssociatesHeading" runat="server" visible="false" style="width: 100%"
    cellpadding="2" cellspacing="5">
    <tr>
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label3" runat="server" Text="Demat Association Audit Details"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport4" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick4"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tblCustomerDematAssociates" runat="server" width="110%" visible="false">
    <tr>
        <td>
            <asp:Panel ID="pnlCustomerDematAssociates" runat="server" Width="90%" ScrollBars="Horizontal"
                Visible="true">
                <telerik:RadGrid ID="rdCustomerDematAssociates" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
                    GridLines="Both" EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
                    OnNeedDataSource="rdCustomerDematAssociates_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="110%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="C_CustomerId" SortExpression="C_CustomerId" UniqueName="C_CustomerId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" Visible="false"
                                ShowFilterIcon="false" HeaderText="System Id " HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_Name" SortExpression="CDAA_Name" UniqueName="CDAA_Name"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Name" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_PanNum" SortExpression="CDAA_PanNum" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="PAN" UniqueName="CDAA_PanNum" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_Sex" SortExpression="CDAA_Sex" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Gender" UniqueName="CDAA_Sex" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_IsKYC" SortExpression="CDAA_IsKYC" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="IsKYC" UniqueName="CDAA_IsKYC" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XR_Relationship" SortExpression="XR_Relationship"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Relationship" UniqueName="XR_Relationship"
                                HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_GaurdianName" SortExpression="CDAA_GaurdianName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" Visible="false" HeaderText="GaurdianName" UniqueName="CDAA_GaurdianName"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociationType" SortExpression="AssociationType"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Associate Type" UniqueName="AssociationType"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CDAA_DOB" SortExpression="CDAA_DOB" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="D.O.B" UniqueName="CDAA_DOB" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modiication Date/Time" UniqueName="ModiicationDateTime"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tableCustomerTransaction" runat="server" visible="false" style="width: 100%"
    cellpadding="2" cellspacing="5">
    <tr>
        <td class="tdSectionHeading" colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblCustomerTransaction" runat="server" Text="Customer Transaction Audit"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport5" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick5"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tableTransaction" runat="server" width="100%" visible="false">
    <tr>
        <td>
            <asp:Panel ID="pnlTransaction" runat="server" Width="85%" ScrollBars="Horizontal"
                Visible="true">
                <telerik:RadGrid ID="rdTransaction" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true" GridLines="Both"
                    EnableEmbeddedSkins="false" ShowFooter="true" Width="100%" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
                    OnNeedDataSource="rdTransaction_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="140%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode"
                                UniqueName="PASP_SchemePlanCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="SchemePlan Code " HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                                UniqueName="PASP_SchemePlanName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="SchemePlan Name" HeaderStyle-Width="35px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFA_AccountId" SortExpression="CMFA_AccountId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Account Id" UniqueName="CMFA_AccountId" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_TransactionDate" SortExpression="CMFT_TransactionDate"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Transaction Date" UniqueName="CMFT_TransactionDate"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_BuySell" SortExpression="CMFT_BuySell" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="BuySell" UniqueName="CMFT_BuySell" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_Units" SortExpression="CMFT_Units" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Units" UniqueName="CMFT_Units" HeaderStyle-Width="10px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_Amount" SortExpression="CMFT_Amount" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Amount" UniqueName="CMFT_Amount" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFT_UserTransactionNo" SortExpression="CMFT_UserTransactionNo"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="UserTransactionNo" UniqueName="CMFT_UserTransactionNo"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WTS_TransactionStatus" SortExpression="WTS_TransactionStatus"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="TransactionStatus" UniqueName="WTS_TransactionStatus"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modiication Date/Time" UniqueName="ModiicationDateTime"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <%-- <Scrolling AllowScroll="true" />--%>
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblSchemePlan" runat="server" visible="false" style="width: 100%" cellpadding="2"
    cellspacing="5">
    <tr>
        <td class="tdSectionHeading">
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label2" runat="server" Text="SchemePlan Audit"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport6" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick6"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="taSchemeAudit" runat="server" width="120%" visible="false">
    <tr>
        <td>
            <asp:Panel ID="pnlSchemeAudit" runat="server" Width="85%" ScrollBars="Horizontal"
                Visible="true">
                <telerik:RadGrid ID="rdSchemeAudit" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true" GridLines="Both"
                    EnableEmbeddedSkins="false" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
                    OnNeedDataSource="rdSchemeAudit_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="110%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode"
                                UniqueName="PASP_SchemePlanCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="SchemePlan Code " HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName"
                                UniqueName="PASP_SchemePlanName" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="SchemePlan Name" HeaderStyle-Width="25px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PA_AMCName" SortExpression="PA_AMCName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="AMC Name" UniqueName="PA_AMCName" HeaderStyle-Width="33px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="XES_SourceName" SortExpression="XES_SourceName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="RNT" UniqueName="XES_SourceName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PASP_Status" SortExpression="PASP_Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Status" UniqueName="PASP_Status" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified On" UniqueName="ModiicationDateTime"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <%-- <Scrolling AllowScroll="true" />--%>
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblStaffAudit" runat="server" visible="false" style="width: 100%" cellpadding="2"
    cellspacing="5">
    <tr>
        <td class="tdSectionHeading">
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label5" runat="server" Text="Staff Audit Details"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport7" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick7"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tbStaffAudit" runat="server" visible="false" width="250%">
    <tr>
        <td>
            <asp:Panel ID="pnlStaffAudit" runat="server" Width="40%" ScrollBars="Horizontal"
                Visible="true">
                <telerik:RadGrid ID="rdStaffAudit" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                    AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true" GridLines="Both"
                    EnableEmbeddedSkins="false" ShowFooter="true" Width="100%" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
                    OnNeedDataSource="rdStaffAudit_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="140%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_RMId" SortExpression="AR_RMId" UniqueName="AR_RMId"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Staff Id" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_StaffCode" SortExpression="AR_StaffCode" UniqueName="AR_StaffCode"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                HeaderText="Staff Code" HeaderStyle-Width="25px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Staff Agent Code" Visible="false" UniqueName="AAC_AgentCode"
                                HeaderStyle-Width="33px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_FirstName" SortExpression="AR_FirstName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="FirstName" UniqueName="AR_FirstName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_MiddleName" SortExpression="AR_MiddleName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="MiddleName" UniqueName="AR_MiddleName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_LastName" SortExpression="AR_LastName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="LastName" UniqueName="AR_LastName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_Email" SortExpression="AR_Email" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Email Id" UniqueName="AR_Email" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AR_Mobile" SortExpression="AR_Mobile" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Mobile No" UniqueName="AR_Mobile" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TitleName" SortExpression="TitleName" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Title Name" UniqueName="TitleName" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified On" UniqueName="ModiicationDateTime"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <%-- <Scrolling AllowScroll="true" />--%>
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="tblAssociateAudit" runat="server" visible="false" style="width: 100%"
    cellpadding="2" cellspacing="5">
    <tr>
        <td class="tdSectionHeading">
            <div class="divSectionHeading" style="vertical-align: text-bottom;">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label6" runat="server" Text="Staff Audit Details"></asp:Label>
                        </td>
                        <td align="right" style="width: 10px">
                            <asp:ImageButton Visible="true" ID="btnExport8" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick8"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table id="tbAssociateAudit" runat="server" visible="false" width="140%">
    <tr>
        <td>
            <asp:Panel ID="pnlAssociateAudit" runat="server" Width="73%" ScrollBars="Horizontal"
                Visible="true">
                <telerik:RadGrid ID="rdAssociateAudit" runat="server" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" Skin="Telerik" EnableHeaderContextMenu="true"
                    GridLines="Both" EnableEmbeddedSkins="false" Width="100%" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" AllowFilteringByColumn="true" PageSize="10"
                    OnNeedDataSource="rdAssociateAudit_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView Width="110%" AllowMultiColumnSorting="True" AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Status" SortExpression="Status" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText=" Audit Status" UniqueName="Status" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AA_AdviserAssociateId" SortExpression="AA_AdviserAssociateId"
                                UniqueName="AA_AdviserAssociateId" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="Associate Id" HeaderStyle-Width="15px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                UniqueName="AAC_AgentCode" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                ShowFilterIcon="false" HeaderText="SubBroker Code" HeaderStyle-Width="25px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AA_ContactPersonName" SortExpression="AA_ContactPersonName"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Associate Name" UniqueName="AA_ContactPersonName"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AA_Email" SortExpression="AA_Email" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Email Id" UniqueName="AA_Email" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AA_Mobile" SortExpression="AA_Mobile" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="Mobile No" UniqueName="AA_Mobile" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AA_PAN" SortExpression="AA_PAN" AutoPostBackOnFilter="true"
                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="false"
                                HeaderText="PAN No" UniqueName="AA_PAN" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModificationBy" SortExpression="ModificationBy"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified By" UniqueName="ModificationBy" HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ModiicationDateTime" SortExpression="ModiicationDateTime"
                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                AllowFiltering="false" HeaderText="Modified On" UniqueName="ModiicationDateTime"
                                HeaderStyle-Width="20px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <%-- <Scrolling AllowScroll="true" />--%>
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="false">
                    </FilterMenu>
                </telerik:RadGrid>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="hdnschemePlanId" runat="server" />
<asp:HiddenField ID="hdnStaffId" runat="server" />
<asp:HiddenField ID="hdnAssociateId" runat="server" />
