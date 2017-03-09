<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DailyValuationMonitor.ascx.cs"
    Inherits="WealthERP.SuperAdmin.DailyValuationMonitor" %>
<%--<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.QuickStart" %>--%>
<%--<%@ Register TagPrefix="telerik" TagName="Header" Src="~/Common/Header.ascx" %>
<%@ Register TagPrefix="telerik" TagName="HeadTag" Src="~/Common/HeadTag.ascx" %>
<%@ Register TagPrefix="telerik" TagName="Footer" Src="~/Common/Footer.ascx" %>--%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<%--<script type="text/javascript">
    function ValidateInputField() {
        var select = document.getElementById('<%=ddlMonitorfr.ClientID %>');
        if (select.options[select.selectedIndex].value == "Select") {
                alert('Please Select Monitor Type');
                return false; 
        }
        </script>--%>

<script type="text/javascript">
    function pageLoad() {
        InitDialogs();
        Loading(false);
    }

    function UpdateImg(ctrl, imgsrc) {
        var img = document.getElementById(ctrl);
        img.src = imgsrc;
    }

    // sets up all of the YUI dialog boxes
    function InitDialogs() {
        DialogBox_Loading = new YAHOO.widget.Panel("waitBox",
	{ fixedcenter: true, modal: true, visible: false,
	    width: "230px", close: false, draggable: true
	});
        DialogBox_Loading.setHeader("Processing, please wait...");
        DialogBox_Loading.setBody('<div style="text-align:center;"><img src="/Images/Wait.gif" id="Image1" /></div>');
        DialogBox_Loading.render(document.body);
    }
    function Loading(b) {
        if (b == true && Page_IsValid == true) {
            DialogBox_Loading.show();
        }
        else {
            DialogBox_Loading.hide();
        }
    }
</script>

<table width="100%" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Add Staff"></asp:Label>
            <hr />
        </td>
    </tr>--%>
    <tr>
        <td colspan="4">
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblTitle" runat="server" Text="Daily Valuation Monitor"></asp:Label>
                        </td>
                        <td colspan="4" align="right">
                            <asp:ImageButton ID="btnExportDuplicateFolioFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('CSV')"
                                Height="20px" Width="25px" Visible="false" OnClick="btnExportDuplicateFolioFilteredData_Click">
                            </asp:ImageButton>
                            <asp:ImageButton ID="btnExportDuplicateTransactionFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('CSV')"
                                Height="20px" Width="25px" Visible="false" OnClick="btnExportDuplicateTransactionFilteredData_Click">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="Left" style="width: 158px" valign="top">
            <asp:Label ID="Mntfr" runat="server" CssClass="FieldName" Text="Monitor For :" Width="100%"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMonitorfr" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlMonitorfr_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                <asp:ListItem Text="Mutual Fund" Value="MF"></asp:ListItem>
                <asp:ListItem Text="Equity" Value="EQ"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select an Action"
                ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlMonitorfr" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
            <br />
            <asp:RequiredFieldValidator ID="reqddlAdviser" runat="server" CssClass="rfvPCG" ErrorMessage="Please select an Advisor"
                Text="Please select a Field" Display="Dynamic" ValidationGroup="vgBtnSubmitTemp"
                ControlToValidate="ddlMonitorfr" InitialValue="0">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trdd1" runat="server">
        <td align="Left" style="width: 158px" valign="top">
            <asp:Label ID="lblAction" runat="server" Text="Select Type of Monitoring: " CssClass="FieldName"
                Width="100%"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Adviser Valuation" Value="AumMis"></asp:ListItem>
                <asp:ListItem Text="Duplicates" Value="DuplicateMis"></asp:ListItem>
                <asp:ListItem Text="MF Rejects" Value="mfRejects"></asp:ListItem>
                <asp:ListItem Text="NAV Change" Value="NAVChange"></asp:ListItem>
                <asp:ListItem Text="Folios" Value="DuplicateFolios"></asp:ListItem>
                <asp:ListItem Text="Transactions" Value="DuplicateTransactions"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cmpamc" runat="server" ErrorMessage="<br />Please select an Action"
                ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlAction" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <%--<tr id="trequity" runat="server" visible="false">
        <td align="left" style="width: 158px" valign="top">
            <asp:Label ID="lblEquity" runat="server" Text="Select Type Of Monitoring: " CssClass="FieldName"
                Width="100%"></asp:Label>
        </td>
        <td>
          <asp:DropDownList ID="ddlEquity" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Adviser Valuation" Value="AumMis"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Please select an Action"
                ValidationGroup="MFSubmit" ControlToValidate="ddlEquity" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
     </td>
    </tr>--%>
    <tr id="trRadioDatePeriod" runat="server">
        <td class="style1" colspan="2">
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" ValidationGroup="vgBtnSubmitTemp" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" ValidationGroup="vgBtnSubmitTemp" />
            <asp:Label ID="lblPickPeriod" runat="server" Text="Pick a Period" CssClass="Field"></asp:Label>
        </td>
    </tr>
</table>
<table id="tblRange" runat="server">
    <tr id="trRange" visible="false" runat="server">
        <td align="left" valign="top">
            <asp:Label ID="lblFromDate" runat="server" Width="50" CssClass="FieldName">From:</asp:Label>
        </td>
        <td valign="top">
            <%--<asp:TextBox ID="txtFromDate" runat="server" style="vertical-align: middle" Width="150" CssClass="txtField"></asp:TextBox>
            <span id="spnFrom" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" TargetControlID="txtFromDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtFromDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtFromDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender--%>
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" ValidationGroup="vgBtnSubmitTemp"
                MinDate="1900-01-01">
                <Calendar ID="Calendar3" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput3" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="spnFrom" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
            </asp:RequiredFieldValidator>
        </td>
        <td valign="top" align="left">
            <asp:Label ID="lblToDate" runat="server" Width="50" CssClass="FieldName">To:</asp:Label>
        </td>
        <td valign="top">
            <%-- <asp:TextBox ID="txtToDate" runat="server" CssClass="txtField" Width="150"> 
            </asp:TextBox>
            <span id="spnTo" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="txtToDate_CalendarExtender" runat="server" TargetControlID="txtToDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="txtToDate_TextBoxWatermarkExtender" runat="server"
                TargetControlID="txtToDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" ValidationGroup="vgBtnSubmitTemp"
                MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="spnTo" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgBtnSubmitTemp"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trPeriod" visible="false" runat="server">
        <td>
            <asp:Label ID="lblPeriod" runat="server" Width="50" CssClass="FieldName">Period:</asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPeriod" runat="server" Width="150" AutoPostBack="true" CssClass="cmbField">
            </asp:DropDownList>
            <span id="spnPeriod" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Please select a Period"
                ValidationGroup="vgBtnSubmitTemp" ControlToValidate="ddlPeriod" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr runat="server" id="trFolioAndTransactionDuplicateMonitor" visible="false">
        <td>
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
            <asp:CheckBox ID="chkFolioDuplicatesOnly" CssClass="cmbField" runat="server" Text="Duplicates Only" />
        </td>
    </tr>
    <tr id="trDate" runat="server">
        <td valign="top">
            <asp:Label ID="lblDate" runat="server" Width="80" CssClass="FieldName">Select Date:</asp:Label>
        </td>
        <td>
            <%-- <asp:TextBox ID="txtDate" runat="server" style="vertical-align: middle" Width="150" CssClass="txtField"></asp:TextBox>
            <span id="Span1" class="spnRequiredField">*</span>
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDate"
                Format="dd/MM/yyyy">
            </cc1:CalendarExtender>
            <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                TargetControlID="txtDate" WatermarkText="dd/mm/yyyy">
            </cc1:TextBoxWatermarkExtender>--%>
            <telerik:RadDatePicker ID="txtDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" ValidationGroup="vgBtnSubmitTemp"
                MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
            <span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a  Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="vgBtnSubmitTemp">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CVDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic" ValidationGroup="vgBtnSubmitTemp"></asp:CompareValidator>
            <asp:CompareValidator ID="cvSelectDate" runat="server" ControlToValidate="txtDate"
                CssClass="cvPCG" ErrorMessage="<br />Date should not be  greater than  Today date"
                Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
        </td>
        <td valign="top">
            <asp:Label ID="lblPercentage" runat="server" Width="50" CssClass="FieldName">Percent: </asp:Label>
        </td>
        <td valign="top">
            <%--<asp:TextBox ID="txtPercentage" runat="server" style="vertical-align: middle"  CssClass="txtField"></asp:TextBox>--%>
            <asp:DropDownList ID="ddlNavPer" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Greater than 30%" Value="30"></asp:ListItem>
                <asp:ListItem Text="Greater than 20%" Value="20" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Greater than 10%" Value="10"></asp:ListItem>
                <asp:ListItem Text="Greater than 5%" Value="5"></asp:ListItem>
                <asp:ListItem Text="Positive" Value="0.01"></asp:ListItem>
                <asp:ListItem Text="negative" Value="-0.001"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<%-- <table ID="tblPeriod" runat="server" style="display:none">
    <tr>
      <td valign="top">
          <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period: </asp:Label>
      </td>
      <td valign="top">
          <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField"></asp:DropDownList>
           <span id="Span4" class="spnRequiredField">*</span>
      </td>
  </tr>
</table>--%>
<table>
    <tr id="trSelectionForFolioOrMF" runat="server" visible="false">
        <td>
            <asp:Label ID="lblRbtnSelection" class="Field" runat="server"></asp:Label>
        </td>
        <td>
            <asp:RadioButtonList runat="server" CssClass="Field" ID="rbtnSelection" OnSelectedIndexChanged="rbtnSelection_SelectedIndexChanged"
                RepeatDirection="Horizontal">
                <asp:ListItem Text="View Folio Rejects" Value="0"></asp:ListItem>
                <asp:ListItem Text="View Transaction Rejects" Value="1"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="vgBtnSubmitTemp"
                OnClick="btnGo_Click" />
        </td>
    </tr>
    <tr id="trExportFilteredDupData" runat="server">
        <td>
            <asp:ImageButton ID="btnExportFilteredDupData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredDupData_OnClick"
                OnClientClick="setFormat('CSV')" Height="25px" Width="25px"></asp:ImageButton>
        </td>
    </tr>
    <tr id="tr1" runat="server">
        <td>
        </td>
    </tr>
    <tr id="tr2" runat="server">
        <td>
        </td>
    </tr>
    <%-- <tr>
        <td class="leftField" align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td>
            <telerik:RadGrid ID="gvDuplicateCheck" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="true" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" OnNeedDataSource="gvDuplicateCheck_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="DuplicateRecordslist">
                </ExportSettings>
                <MasterTableView DataKeyNames="A_AdviserId,CMFNP_ValuationDate,PASP_SchemePlanCode,CMFA_AccountId,CMFNP_NetHoldings"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <%--<telerik:GridTemplateColumn ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Valuation Date">
                            <%--<HeaderTemplate>
                                            <asp:Label ID="lblAdviserNameDate" runat="server" Text="Valuation Date"></asp:Label>
                                            <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblValuationDate"  Width="" runat="server" Text='<%# Eval("CMFNP_ValuationDate","{0:D}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="CMFNP_ValuationDate" AllowFiltering="false" HeaderText="Valuation Date"
                            DataFormatString="{0:D}" UniqueName="Valuation Date" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblAdviserIdData" runat="server" Text="AdviserID"></asp:Label>
                                <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAdviserID"   Width="" runat="server" Text='<%# Eval("A_AdviserId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="A_AdviserId" AllowFiltering="false" HeaderText="AdviserID"
                            UniqueName="AdviserID" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrganizationData"  Width="" runat="server" Text="Adviser Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="A_OrgName" AllowFiltering="false" HeaderText="Adviser Name"
                            UniqueName="Adviser Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%-- <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" HeaderText="No of Duplicates"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px">
                            <%--<HeaderTemplate>
                                            <asp:Label ID="lblOrganizationData" runat="server" Text="Adviser Name"></asp:Label>
                                            <asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblDuplicate"  Width="" runat="server" Text='<%# Eval("Duplicate").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="Duplicate" AllowFiltering="false" HeaderText="No of Duplicates"
                            UniqueName="No of Duplicates" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblFolioNoData" runat="server" Text="Folio Number"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFolioNoData2"  Width="" runat="server" Text='<%# Eval("CMFA_FolioNum").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="CMFA_FolioNum" AllowFiltering="false" HeaderText="Folio Number"
                            UniqueName="Folio Number" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblSchemeName" runat="server" Text="Scheme Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSchemeName2"  Width="" runat="server" Text='<%# Eval("PASP_SchemePlanName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="PASP_SchemePlanName" AllowFiltering="false" HeaderText="Scheme Name"
                            UniqueName="Scheme Name" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%-- <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" HeaderText="Holdings"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px">
                            <ItemTemplate>
                                <asp:Label ID="lblHoldings"  Width="" runat="server" Text='<%# Eval("CMFNP_NetHoldings","{0:n}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="CMFNP_NetHoldings" AllowFiltering="false" HeaderText="Holdings"
                            DataFormatString="{0:n}" UniqueName="Holdings" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <%-- <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" HeaderText="Creation Date" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Width="10px">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreation" runat="server" Text='<%# Eval("CMFNP_CreatedOn","{0:d}").ToString() %>' DataFormatString="{0:D}"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Wrap="true"></HeaderStyle>
                                        <ItemStyle Wrap="true"></ItemStyle>
                                    </telerik:GridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <%-- <asp:GridView ID="gvDuplicateCheck" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True" AllowFilteringByColumn="true" ShowFooter="true" AutoGenerateColumns="False"
                DataKeyNames="A_AdviserId,CMFNP_ValuationDate,PASP_SchemePlanCode,CMFA_AccountId,CMFNP_NetHoldings">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkDelete" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Valuation Date"
                        ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblValuationDate" runat="server" Text='<%# Eval("CMFNP_ValuationDate","{0:d}").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-Width="10px">
                        <HeaderTemplate>
                            <asp:Label ID="lblAdviserIdData" runat="server" Text="AdviserID"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlAdviserId" AutoPostBack="true" CssClass="cmbField" Width="80px"
                                runat="server" OnSelectedIndexChanged="ddlAdviserIdDuplicate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAdviserID" runat="server" Text='<%# Eval("A_AdviserId").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label ID="lblOrganizationData" runat="server" Text="Adviser Name"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlOrganization" AutoPostBack="true" CssClass="cmbField" runat="server"
                                OnSelectedIndexChanged="ddlOrganization_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true"></HeaderStyle>
                        <ItemStyle Wrap="true"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="No of Duplicates"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblDuplicate" runat="server" Text='<%# Eval("Duplicate").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label ID="lblFolioNoData" runat="server" Text="Folio Number"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlFolioNo" AutoPostBack="true" CssClass="cmbField" Width="80px"
                                runat="server" OnSelectedIndexChanged="ddlFolioNo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFolioNo" runat="server" Text='<%# Eval("CMFA_FolioNum").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="true"></HeaderStyle>
                        <ItemStyle Wrap="true"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Left">
                        <HeaderTemplate>
                            <asp:Label ID="lblSchemeName" runat="server" Text="Scheme Name"></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlScheme" AutoPostBack="true" CssClass="cmbField" runat="server"
                                OnSelectedIndexChanged="ddlScheme_SelectedIndexChanged">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblScheme" runat="server" Text='<%# Eval("PASP_SchemePlanName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Holdings"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblHoldings" runat="server" Text='<%# Eval("CMFNP_NetHoldings","{0:n}").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <%--  <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Creation Date">
                        <ItemTemplate>
                             <asp:Label ID="lblCreation" runat="server" Text='<%# Eval("CMFNP_CreatedOn","{0:d}").ToString() %>' DataFormatString="{0:d}"></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>--%>
        </td>
    </tr>
    <tr id="trbtnDelete" runat="server">
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" OnClick="btnDelete_Click" />
            <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" CssClass="PCGButton"
                OnClick="btnDeleteAll_Click" />
        </td>
    </tr>
</table>
<table>
    <tr id="trExportFilteredAumData" runat="server">
        <td>
            <asp:ImageButton ID="btnExportFilteredAumData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredAumData_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
        </td>
    </tr>
    <%--<tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblPage" class="Field" runat="server"></asp:Label>
                        <%-- <asp:Label ID="lblTotalPage" class="Field" runat="server"></asp:Label>
                    </td>
                </tr>--%>
    <tr>
        <td>
            <telerik:RadGrid ID="gvAumMis" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" OnNeedDataSource="gvAumMis_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="AumMislist">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CMFNP_ValuationDate" AllowFiltering="false" HeaderText="Valuation Date"
                            DataFormatString="{0:D}" UniqueName="Valuation Date" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="A_AdviserId" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Adviser Id" UniqueName="Adviser Id">
                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn AllowFiltering="true" DataField="A_OrgName">
                            <HeaderTemplate>
                                <asp:Label ID="lblAdviserNameDate" runat="server" Text="Adviser Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAdviserNameDate" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="AUM" AllowFiltering="false" HeaderText="AUM"
                            UniqueName="AUM" DataFormatString="{0:n}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
</table>
<asp:Panel ID="pnlReject" runat="server" class="Landscape" Width="99%" ScrollBars="Horizontal">
    <%-- <asp:Panel ID="pnlReject" runat="server" class="Landscape" ScrollBars="Horizontal">--%>
    <table>
        <tr id="trExportFilteredRejData" runat="server">
            <td>
                <asp:ImageButton ID="btnExportFilteredRejData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                    runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredRejData_OnClick"
                    OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td>
                <div id="dvAll" runat="server" style="width: 640px">
                    <telerik:RadGrid ID="gvMFRejectedDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" OnNeedDataSource="gvMFRejectedDetails_OnNeedDataSource">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="MFRejectedDetailslist">
                        </ExportSettings>
                        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="None">
                            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                                ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="CMFTS_CreatedOn" AllowFiltering="false" HeaderText="Process Date"
                                    DataFormatString="{0:D}" UniqueName="CMFTS_CreatedOn" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="A_AdviserId" HeaderText="AdviserId"
                                    UniqueName="AdviserId" HeaderStyle-HorizontalAlign="left">
                                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="A_OrgName" AllowFiltering="false" HeaderStyle-HorizontalAlign="Left"
                                    HeaderText="Adviser Name" UniqueName="A_OrgName">
                                    <ItemStyle Width="" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="WUXFT_XMLFileName" AllowFiltering="false" HeaderText="File NameSource Type"
                                    UniqueName="File NameSource Type">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_FolioNum" AllowFiltering="false" HeaderText="Folio number"
                                    UniqueName="Folio number">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_SchemeCode" AllowFiltering="false" HeaderText="Scheme"
                                    UniqueName="Scheme">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASP_SchemePlanName" AllowFiltering="false" HeaderText="Scheme name"
                                    UniqueName="Scheme name">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_TransactionClassificationCode" AllowFiltering="false"
                                    HeaderText="Transaction type" UniqueName="Transaction type">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_TransactionDate" AllowFiltering="false"
                                    HeaderText="Transaction date" DataFormatString="{0:D}" UniqueName="Transaction date">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_Price" AllowFiltering="false" HeaderText="Price"
                                    DataFormatString="{0:n}" UniqueName="Price">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_Units" AllowFiltering="false" HeaderText="Units"
                                    DataFormatString="{0:n}" UniqueName="Units">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CMFTS_Amount" AllowFiltering="false" HeaderText="Amount"
                                    DataFormatString="{0:n}" UniqueName="Transaction type">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="WRR_RejectReasonDescription"
                                    HeaderText="Reject Reason" UniqueName="Reject Reason" HeaderStyle-HorizontalAlign="left">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AllowFiltering="true" DataField="Adul_ProcessId" HeaderText="ProcessId"
                                    UniqueName="ProcessId" HeaderStyle-HorizontalAlign="right">
                                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>
<%--</asp:Panel>--%>
<table>
    <tr id="trExportFilteredNavData" runat="server">
        <td>
            <asp:ImageButton ID="btnExportFilteredNavData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredNavData_OnClick"
                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
        </td>
    </tr>
    <%-- <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblNAVCount" class="Field" runat="server"></asp:Label>
                        <asp:Label ID="lblNAVTotal" class="Field" runat="server"></asp:Label>
                    </td>
                </tr>--%>
    <tr>
        <td>
            <telerik:RadGrid ID="gvNavChange" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-Excel-Format="ExcelML" OnNeedDataSource="gvNavChange_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="NavChangelist">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="None">
                    <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                        ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="SchemeCode" AllowFiltering="false" HeaderText="Scheme Code"
                            UniqueName="Scheme Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PASP_SchemePlanName" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Scheme Name" UniqueName="Scheme Name">
                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Todays_NAV" AllowFiltering="false" HeaderText="Current NAV(AsOn)"
                            UniqueName="Current NAV(AsOn)">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="lastday_Nav" AllowFiltering="false" HeaderText="Previous Day NAV"
                            UniqueName="Previous Day NAV">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PerDiff" AllowFiltering="false" HeaderText="Percentage ChangeNAV"
                            UniqueName="Percentage ChangeNAV" DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <ClientEvents />
                </ClientSettings>
            </telerik:RadGrid>
            <%--<asp:GridView ID="gvNavChange" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                            CellPadding="4" CssClass="GridViewStyle" HeaderStyle-Width="100%" ShowFooter="True">
                            <FooterStyle CssClass="FooterStyle" />
                            <RowStyle CssClass="RowStyle" />
                            <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                            <HeaderStyle CssClass="HeaderStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" />
                            <Columns>
                                <asp:BoundField DataField="SchemeCode" HeaderText="Scheme Code" ItemStyle-HorizontalAlign="right"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="SchemeName" HeaderText="Scheme Name" ItemStyle-HorizontalAlign="left"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="CurrentNAV" HeaderText="Current NAV(AsOn)" ItemStyle-HorizontalAlign="right"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="PreviousNAV" HeaderText="Previous Day NAV" ItemStyle-HorizontalAlign="right"
                                    ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                <asp:BoundField DataField="PercentChange" HeaderText="Percentage ChangeNAV" ItemStyle-HorizontalAlign="right"
                                    ItemStyle-Wrap="false" DataFormatString="{0:n2}" HeaderStyle-Wrap="false" />
                            </Columns>
                       </asp:GridView>
                    </td>
                </tr>
            </table>--%>
        </td>
    </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<br />
<div id="divGvFolioDuplicates" style="padding: 8px;" runat="server" visible="false">
    <telerik:RadGrid ID="gvFolioDuplicates" runat="server" CssClass="RadGrid" GridLines="None"
        Width="1000px" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" OnItemCommand="gvTransactionDuplicates_ItemCommand"
        EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" OnItemDataBound="gvFolioDuplicates_ItemDataBound"
        EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true" OnNeedDataSource="gvFolioDuplicates_OnNeedDataSource">
        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
        </ExportSettings>
        <MasterTableView DataKeyNames="CMFA_FolioNum" CommandItemDisplay="None" CommandItemSettings-ShowRefreshButton="false">
            <Columns>
                <telerik:GridBoundColumn UniqueName="aname" HeaderText="Adviser Name" HeaderStyle-Width="200px"
                    DataField="aname" SortExpression="aname" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="A_AdviserId" HeaderText="AdviserId" HeaderStyle-Width="100px"
                    DataField="A_AdviserId" SortExpression="A_AdviserId" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="OrgName" HeaderText="OrgName" HeaderStyle-Width="200px"
                    DataField="A_OrgName" SortExpression="A_OrgName" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFA_FolioNum" HeaderText="FolioNum" DataField="CMFA_FolioNum"
                    HeaderStyle-Width="120px" SortExpression="CMFA_FolioNum" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PA_AMCCode" HeaderText="AMCCode" DataField="PA_AMCCode"
                    HeaderStyle-Width="100px" SortExpression="PA_AMCCode" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PA_AMCName" HeaderText="AMCName" DataField="PA_AMCName"
                    HeaderStyle-Width="250px" SortExpression="PA_AMCCode" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="TotalDuplicates" HeaderText="TotalDuplicates"
                    HeaderStyle-Width="100px" DataField="TotalDuplicates" SortExpression="TotalDuplicates"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="deleteColumn" HeaderStyle-Width="100px" ConfirmText="This will delete all the duplicates folios for this folio number"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
                <%--<telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                    DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                        DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                        DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                        DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn UniqueName="PASC_AMC_ExternalCode" HeaderText="ExternalCode"
                        DataField="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode" AllowFiltering="true"
                        ShowFilterIcon="false" AutoPostBackOnFilter="true">
                        
                    </telerik:GridBoundColumn>
                </telerik:GridBoundColumn>--%>
            </Columns>
        </MasterTableView>
        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
            <Scrolling AllowScroll="false" />
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<br />
<div id="divGvTransactionDuplicates" runat="server" style="overflow: scroll;" visible="false">
    <telerik:RadGrid ID="gvTransactionDuplicates" runat="server" CssClass="RadGrid" GridLines="None"
        Width="100%" AllowPaging="True" PageSize="20" AllowSorting="True" AutoGenerateColumns="false"
        ShowStatusBar="true" AllowAutomaticDeletes="True" AllowAutomaticInserts="false"
        OnItemCommand="gvTransactionDuplicates_ItemCommand" AllowAutomaticUpdates="false"
        Skin="Telerik" EnableEmbeddedSkins="false" OnItemDataBound="gvTransactionDuplicates_ItemDataBound"
        EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" AllowFilteringByColumn="true"
        OnNeedDataSource="gvTransactionDuplicates_OnNeedDataSource">
        <ExportSettings HideStructureColumns="false" ExportOnlyData="true" FileName="ExistMFInvestlist">
        </ExportSettings>
        <MasterTableView DataKeyNames="CMFA_AccountId" CommandItemDisplay="None" CommandItemSettings-ShowRefreshButton="false">
            <Columns>
                <telerik:GridBoundColumn UniqueName="A_AdviserId" HeaderText="AdviserId" HeaderStyle-Width="100px"
                    DataField="A_AdviserId" SortExpression="A_AdviserId" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="aname" HeaderText="Adviser Name" HeaderStyle-Width="200px"
                    DataField="aname" SortExpression="aname" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="cname" HeaderText="Customer Name" HeaderStyle-Width="200px"
                    DataField="cname" SortExpression="cname" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFA_AccountId" HeaderText="AccountId" DataField="CMFA_AccountId"
                    SortExpression="CMFA_AccountId" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFA_FolioNum" HeaderText="Folio" HeaderStyle-Width="200px"
                    DataField="CMFA_FolioNum" SortExpression="CMFA_FolioNum" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="PASP_SchemePlanName" HeaderText="Scheme" HeaderStyle-Width="200px"
                    DataField="PASP_SchemePlanName" SortExpression="PASP_SchemePlanName" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WMTT_TransactionClassificationCode" HeaderText="Type"
                    DataField="WMTT_TransactionClassificationCode" SortExpression="WMTT_TransactionClassificationCode"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderStyle-Width="178px">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFT_Units" HeaderText="Units" DataField="CMFT_Units"
                    SortExpression="CMFT_Units" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFT_Price" HeaderText="Price" DataField="CMFT_Price"
                    SortExpression="CMFT_Price" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFT_Amount" HeaderText="Amount" DataField="CMFT_Amount"
                    SortExpression="CMFT_Amount" AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="200px" UniqueName="CMFT_TransactionNumber"
                    HeaderText="Transaction No." DataField="CMFT_TransactionNumber" SortExpression="CMFT_TransactionNumber"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="CMFT_TransactionDate" HeaderText="TransactionDate"
                    DataField="CMFT_TransactionDate" SortExpression="CMFT_TransactionDate" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" DataFormatString="{0:D}" HeaderStyle-Width="130px">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WTS_TransactionStatus" HeaderText="TransactionStatus"
                    DataField="WTS_TransactionStatus" SortExpression="WTS_TransactionStatus" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderStyle-Width="140px">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="WMTT_TransactionClassificationName" HeaderText="Transaction Type"
                    DataField="WMTT_TransactionClassificationName" SortExpression="WMTT_TransactionClassificationName"
                    AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderStyle-Width="140px">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="XES_SourceCode" HeaderText="SourceCode" DataField="XES_SourceCode"
                    SortExpression="XES_SourceCode" AllowFiltering="true" ShowFilterIcon="false"
                    AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="totalDuplicates" HeaderText="TotalDuplicates"
                    DataField="totalDuplicates" SortExpression="totalDuplicates" AllowFiltering="true"
                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderStyle-Width="100px">
                    <ItemStyle HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridButtonColumn UniqueName="deleteColumn" HeaderStyle-Width="100px" ConfirmText="This will delete all the duplicates for this transaction"
                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                    Text="Delete">
                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                </telerik:GridButtonColumn>
            </Columns>
            <HeaderStyle Width="200px" />
        </MasterTableView>
        <ClientSettings ReorderColumnsOnClient="True" AllowColumnsReorder="True" EnableRowHoverStyle="true">
            <Scrolling AllowScroll="false" />
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div id="divRGVGridViewForFolioRejects" runat="server" style="overflow: scroll;"
    visible="false">
    <div>
        <asp:ImageButton ID="btnExportRGVGridViewForFolioRejects" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
            runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportRGVGridViewForFolioRejects_Click"
            OnClientClick="setFormat('CSV')" Height="20px" Width="25px"></asp:ImageButton>
    </div>
    <br />
    <telerik:RadGrid ID="RGVGridViewForFolioRejects" runat="server" CssClass="RadGrid"
        GridLines="None" Width="100%" AllowPaging="True" PageSize="10" AllowSorting="True"
        AutoGenerateColumns="false" ShowStatusBar="true" AllowAutomaticInserts="false"
        AllowAutomaticUpdates="false" Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="true"
        OnNeedDataSource="RGVGridViewForFolioRejects_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None">
            <Columns>
             <telerik:GridBoundColumn HeaderStyle-Width="130px" HeaderText="Adviser" DataField="A_OrgName"
                    UniqueName="AdvName" SortExpression="A_OrgName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="130px" HeaderText="Inv Name" DataField="CMFFS_INV_NAME"
                    UniqueName="InvName" SortExpression="CMFFS_INV_NAME" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="160px" HeaderText="PAN Number" DataField="CMFSFS_PANNum"
                    SortExpression="PANNumber" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="160px" HeaderText="Folio" DataField="CMFSFS_FolioNum"
                    SortExpression="CMFSFS_FolioNum" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Broker Code" DataField="CMFSS_BrokerCode"
                    SortExpression="CMFSS_BrokerCode" AutoPostBackOnFilter="true" ShowFilterIcon="false">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="AMC Name" DataField="PA_AMCName"
                    UniqueName="PA_AMCName" SortExpression="PA_AMCName" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="AMC Code" DataField="PA_AMCCode"
                    UniqueName="PA_AMCCode" SortExpression="PA_AMCCode" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="116px" HeaderText="Product Code"
                    DataField="PASC_AMC_ExternalCode" UniqueName="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="116px" HeaderText="Scheme Code"
                    DataField="PASP_SchemePlanCode" UniqueName="PASP_SchemePlanCode" SortExpression="PASP_SchemePlanCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="220px" HeaderText="Reject Reason" DataField="RejectReason"
                    UniqueName="RejectReason" SortExpression="RejectReason" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="322px" HeaderText="WUXFT_XMLFileTypeId"
                    DataField="WUXFT_XMLFileTypeId" UniqueName="WUXFT_XMLFileTypeId" SortExpression="WUXFT_XMLFileTypeId"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="322px" HeaderText="CPS_Id"
                    DataField="CPS_Id" UniqueName="CPS_Id" SortExpression="CPS_Id" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn Visible="false" HeaderStyle-Width="322px" HeaderText="XUET_ExtractTypeCode"
                    DataField="XUET_ExtractTypeCode" UniqueName="XUET_ExtractTypeCode" SortExpression="XUET_ExtractTypeCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="ProcessId" HeaderStyle-Width="100px" DataField="ADUL_ProcessId"
                    UniqueName="ProcessId" SortExpression="ADUL_ProcessId" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Bank Name" DataField="CMFFS_BANK_NAME"
                    UniqueName="CMFFS_BANK_NAME" SortExpression="CMFFS_BANK_NAME" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Add1" DataField="CMGCXP_ADDRESS1"
                    UniqueName="CMGCXP_ADDRESS1" SortExpression="CMGCXP_ADDRESS1" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Add2" DataField="CMGCXP_ADDRESS2"
                    UniqueName="CMGCXP_ADDRESS2" SortExpression="CMGCXP_ADDRESS2" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Add3" DataField="CMGCXP_ADDRESS3"
                    UniqueName="CMGCXP_ADDRESS3" SortExpression="CMGCXP_ADDRESS3" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="City" DataField="CMGCXP_CITY"
                    UniqueName="CMGCXP_CITY" SortExpression="CMGCXP_CITY" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Pin Code" DataField="CMGCXP_PINCODE"
                    UniqueName="CMGCXP_PINCODE" SortExpression="CMGCXP_PINCODE" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Ph. Off" DataField="CMGCXP_PHONE_OFF"
                    UniqueName="CMGCXP_PHONE_OFF" SortExpression="CMGCXP_PHONE_OFF" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="Ph. Res" DataField="CMGCXP_PHONE_RES"
                    UniqueName="CMGCXP_PHONE_RES" SortExpression="CMGCXP_PHONE_RES" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="116px" HeaderText="DOB" DataField="CMGCXP_DOB"
                    UniqueName="CMGCXP_DOB" SortExpression="CMGCXP_DOB" AutoPostBackOnFilter="true"
                    AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<%--<table width="100%">
    <tr style="width: 100%">
        <td colspan="3">
            <table width="100%">
                <tr id="trpagerDuplicate" runat="server" width="100%">
                    <td align="right">
                        <Pager:Pager ID="mypagerDuplicate" runat="server"></Pager:Pager>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>
<%--<table width="50%">
    <tr style="width: 100%">
        <td colspan="3">
           <table width="100%">
                <tr id="trmypagerAUM" runat="server" width="100%">
                    <td align="right">
                        <Pager:Pager ID="mypagerAUM" runat="server"></Pager:Pager>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>
<%--<table width="100%">
    <tr style="width: 100%">
        <td colspan="3">
            <table width="100%">
                <tr id="trPagerReject" runat="server" width="100%">
                    <td align="right">
                        <Pager:Pager ID="pgrReject" runat="server"></Pager:Pager>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>
<%--<table width="60%">
    <tr style="width: 100%">
        <td colspan="3">
            <table width="100%">
                <tr id="trPagerNAV" runat="server" width="100%">
                    <td align="right">
                        <Pager:Pager ID="myPagerNAV" runat="server"></Pager:Pager>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>--%>
<asp:HiddenField ID="hidDateType" Value="" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSelectDate" runat="server" />
<asp:HiddenField ID="hdnAdviserNameAUMFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnProcessIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRejectReasonFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAdviserIdFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAdviserIdDupli" runat="server" Visible="false" />
<asp:HiddenField ID="hdnOrgNameDupli" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioiNoDupli" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeDupli" runat="server" Visible="false" />
