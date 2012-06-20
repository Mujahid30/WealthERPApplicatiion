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

<table width="100%">
    <tr>
        <td colspan="6">
            <asp:Label ID="Label7" runat="server" CssClass="HeaderTextSmall" Text="Daily Valuation Monitor"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="Left" style="width: 158px" valign="top">
            <asp:Label ID="Mntfr" runat="server" CssClass="FieldName" Text="Monitor For" Width="100%"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlMonitorfr" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlMonitorfr_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Mutual Fund" Value="MF"></asp:ListItem>
                <asp:ListItem Text="Equity" Value="EQ"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Please select an Action"
                ValidationGroup="MFSubmit" ControlToValidate="ddlMonitorfr" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
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
            </asp:DropDownList>
            <span id="Span7" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cmpamc" runat="server" ErrorMessage="<br />Please select an Action"
                ValidationGroup="MFSubmit" ControlToValidate="ddlAction" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trequity" runat="server" visible="false">
        <td align="left" style="width: 158px" valign="top">
            <asp:Label ID="lblEquity" runat="server" Text="Select Type Of Monitoring: " CssClass="FieldName"
                Width="100%"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlEquity" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddEquity_SelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Adviser Valuation" Value="AumMis"></asp:ListItem>
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="<br />Please select an Action"
                ValidationGroup="MFSubmit" ControlToValidate="ddlEquity" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trRadioDatePeriod" runat="server">
        <td class="style1" colspan="2">
            <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
            <asp:Label ID="lblPickDate" runat="server" Text="Pick a date range" CssClass="Field"></asp:Label>
            <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                runat="server" GroupName="Date" />
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
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
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
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
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
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
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
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />To Date should not be less than From Date"
                Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="MFSubmit"></asp:CompareValidator>
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
                ValidationGroup="MFSubmit" ControlToValidate="ddlPeriod" Operator="NotEqual"
                CssClass="rfvPCG" ValueToCompare="Select" Display="Dynamic"></asp:CompareValidator>
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
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
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
                runat="server" InitialValue="" ValidationGroup="MFSubmit">
            </asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CVDate" runat="server" ErrorMessage="<br/>Please enter a valid date."
                Type="Date" ControlToValidate="txtDate" CssClass="cvPCG" Operator="DataTypeCheck"
                ValueToCompare="" Display="Dynamic"></asp:CompareValidator>
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
<table width="100%">
    <tr>
        <td colspan="2">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" ValidationGroup="MFSubmit"
                OnClick="btnGo_Click" OnClientClick="Loading(true)" />
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
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="A_AdviserId,CMFNP_ValuationDate,PASP_SchemePlanCode,CMFA_AccountId,CMFNP_NetHoldings"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkDelete" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderText="Valuation Date">
                            <%--<HeaderTemplate>
                                            <asp:Label ID="lblAdviserNameDate" runat="server" Text="Valuation Date"></asp:Label>
                                            <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblValuationDate" runat="server" Text='<%# Eval("CMFNP_ValuationDate","{0:D}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblAdviserIdData" runat="server" Text="AdviserID"></asp:Label>
                                <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblAdviserID" runat="server" Text='<%# Eval("A_AdviserId").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblOrganizationData" runat="server" Text="Adviser Name"></asp:Label>
                                <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" HeaderText="No of Duplicates"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px">
                            <%--<HeaderTemplate>
                                            <asp:Label ID="lblOrganizationData" runat="server" Text="Adviser Name"></asp:Label>
                                            <asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblDuplicate" runat="server" Text='<%# Eval("Duplicate").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblFolioNoData" runat="server" Text="Folio Number"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFolioNoData2" runat="server" Text='<%# Eval("CMFA_FolioNum").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Right"
                            HeaderStyle-Width="10px">
                            <HeaderTemplate>
                                <asp:Label ID="lblSchemeName" runat="server" Text="Scheme Name"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSchemeName2" runat="server" Text='<%# Eval("PASP_SchemePlanName").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn ItemStyle-Wrap="true" HeaderStyle-Wrap="true" HeaderText="Holdings"
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="10px">
                            <ItemTemplate>
                                <asp:Label ID="lblHoldings" runat="server" Text='<%# Eval("CMFNP_NetHoldings","{0:n}").ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Wrap="true"></HeaderStyle>
                            <ItemStyle Wrap="true"></ItemStyle>
                        </telerik:GridTemplateColumn>
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
    <tr>
        <td>
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="PCGButton" OnClick="btnDelete_Click" />
            <asp:Button ID="btnDeleteAll" runat="server" Text="Delete All" CssClass="PCGButton"
                OnClick="btnDeleteAll_Click" />
        </td>
    </tr>
    <tr>
        <td>
            <table width="50%">
                <%--  <tr>
                    <td class="leftField" align="right">
                        <asp:Label ID="lblPage" class="Field" runat="server"></asp:Label>
                        <%-- <asp:Label ID="lblTotalPage" class="Field" runat="server"></asp:Label>
                    </td>
                </tr>
     
                <tr>
                    <td>
                        <telerik:RadGrid ID="gvAumMis" runat="server" GridLines="None" AutoGenerateColumns="False"
                            PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                            Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                            AllowAutomaticInserts="false" OnNeedDataSource="gvAumMis_OnNeedDataSource">
                            <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                            </ExportSettings>
                            <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                                CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
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
                                            <%--<asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                            </asp:DropDownList>--%>
                </HeaderTemplate>
                <itemtemplate>
                                            <asp:Label ID="lblAdviserNameDate" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                                        </itemtemplate>
                <headerstyle wrap="true"></headerstyle>
                <itemstyle wrap="true"></itemstyle>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="AUM" AllowFiltering="false" HeaderText="AUM"
                    UniqueName="AUM" DataFormatString="{0:n}">
                    <itemstyle width="" horizontalalign="Right" wrap="false" verticalalign="Top" />
                </telerik:GridBoundColumn>
                <%-- <telerik:GridTemplateColumn UniqueName="TemplateColumn" SortExpression="CompanyName"
                        InitializeTemplatesFirst="false">
                                    <FooterTemplate>
                            Template column footer</FooterTemplate>
                        <FooterStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                        </telerik:GridTemplateColumn>--%>
                </Columns> </MasterTableView>
                <clientsettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            </clientsettings>
                </telerik:RadGrid>
                <%--<asp:GridView ID="gvAumMis" runat="server" CellPadding="4" CssClass="GridViewStyle"
                            AllowSorting="True" HeaderStyle-Width="50%" ShowFooter="true" AutoGenerateColumns="False">
                            <RowStyle CssClass="RowStyle" />
                            <FooterStyle CssClass="FooterStyle" />
                            <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                            <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                            <EditRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle " />
                            <Columns>
                                <asp:BoundField DataField="CMFNP_ValuationDate" HeaderText="Valuation Date" DataFormatString="{0:d}"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="A_AdviserId" HeaderText="Adviser Id" ItemStyle-HorizontalAlign="Right" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblAdviserNameDate" runat="server" Text="Adviser Name"></asp:Label>
                                        <asp:DropDownList ID="ddlAdviserNameDate" AutoPostBack="true" CssClass="cmbField"
                                            runat="server" OnSelectedIndexChanged="ddlAdviserNameDate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAdviserNameDate" runat="server" Text='<%# Eval("A_OrgName").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Wrap="true"></HeaderStyle>
                                    <ItemStyle Wrap="true"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="AUM" HeaderText="AUM" ItemStyle-HorizontalAlign="Right"
                                    DataFormatString="{0:n}" />
                            </Columns>
                        </asp:GridView>--%>
        </td>
    </tr>
</table>
<%-- <asp:Panel ID="pnlReject" runat="server" class="Landscape" ScrollBars="Horizontal">--%>
<table>
    <%--<tr>
                        <td class="leftField" align="right" width="95%">
                            <asp:Label ID="lblRejectCount" class="Field" runat="server"></asp:Label>
                            <asp:Label ID="lblRejectTotal" class="Field" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
    <tr>
        <td>
            <telerik:RadGrid ID="gvMFRejectedDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" OnNeedDataSource="gvMFRejectedDetails_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CMFTS_CreatedOn" AllowFiltering="false" HeaderText="Process Date"
                            DataFormatString="{0:D}" UniqueName="CMFTS_CreatedOn" HeaderStyle-HorizontalAlign="right">
                            <ItemStyle Width="" HorizontalAlign="right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="A_OrgName" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Adviser Name" UniqueName="A_OrgName">
                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WUXFT_XMLFileName" AllowFiltering="false" HeaderText="File NameSource Type"
                            UniqueName="File NameSource Type">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFTS_FolioNum" AllowFiltering="false" HeaderText="Folio number"
                            UniqueName="Folio number">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFTS_SchemeCode" AllowFiltering="false" HeaderText="Scheme"
                            UniqueName="Scheme">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PASP_SchemePlanName" AllowFiltering="false" HeaderText="Scheme name"
                            UniqueName="Scheme name">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CMFTS_TransactionClassificationCode" AllowFiltering="false"
                            HeaderText="Transaction type" UniqueName="Transaction type">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
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
                        <telerik:GridBoundColumn DataField="CMFTS_TransactionClassificationCode" AllowFiltering="false"
                            HeaderText="Transaction type" UniqueName="Transaction type">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
            <%--<asp:GridView ID="gvMFRejectedDetails" runat="server" AutoGenerateColumns="False"
                                AllowSorting="True" CellPadding="4" CssClass="GridViewStyle" HeaderStyle-Width="100%"
                                ShowFooter="True">
                                <FooterStyle CssClass="FooterStyle" />
                                <RowStyle CssClass="RowStyle" />
                                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <SelectedRowStyle CssClass="SelectedRowStyle" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                <HeaderStyle CssClass="HeaderStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                <Columns>
                                    <asp:BoundField DataField="CMFTS_CreatedOn" HeaderText="Process Date" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" HeaderStyle-Width="25px" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="right" HeaderStyle-Width="20px" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lbladviserId" runat="server" Text="AdviserId"></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="ddlAdviserId" CssClass="cmbLongField" AutoPostBack="true" Width="80px"
                                                runat="server" OnSelectedIndexChanged="ddlAdviserId_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lbladviser" runat="server" Text='<%# Eval("A_AdviserId").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="A_OrgName" HeaderText="Adviser Name" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="WUXFT_XMLFileName" HeaderText="File NameSource Type" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Middle" />
                                    <asp:BoundField DataField="CMFTS_FolioNum" HeaderText="Folio number" ItemStyle-HorizontalAlign="left"
                                        HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CMFTS_SchemeCode" HeaderText="Scheme" ItemStyle-HorizontalAlign="right"
                                        HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="PASP_SchemePlanName" HeaderText="Scheme name" ItemStyle-HorizontalAlign="left"
                                        ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ItemStyle-Width="225px" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="CMFTS_TransactionClassificationCode" HeaderText="Transaction type"
                                        ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                        HeaderStyle-Width="20px" />
                                    <asp:BoundField DataField="CMFTS_TransactionDate" HeaderText="Transaction date" ItemStyle-HorizontalAlign="center"
                                        HeaderStyle-Width="25px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CMFTS_Price" HeaderText="Price" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CMFTS_Units" HeaderText="Units" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:BoundField DataField="CMFTS_Amount" HeaderText="Amount" ItemStyle-HorizontalAlign="right"
                                        DataFormatString="{0:n}" HeaderStyle-Width="20px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="left" HeaderStyle-Width="10px" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblRejectReason" runat="server" Text="Reject Reason"></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="ddlRejectReason" CssClass="cmbLongField" AutoPostBack="true"
                                                Width="80%" runat="server" OnSelectedIndexChanged="ddlRejectReason_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblRejectReasonHeader" runat="server" Text='<%# Eval("WRR_RejectReasonDescription").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="right" ItemStyle-Width="10px" ItemStyle-Wrap="false"
                                        HeaderStyle-Wrap="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblProcessId" runat="server" Text="ProcessId"></asp:Label>
                                            <br />
                                            <asp:DropDownList ID="ddlProcessId" CssClass="cmbLongField" AutoPostBack="true" Width="80px"
                                                runat="server" OnSelectedIndexChanged="ddlProcessId_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Adul_ProcessId").ToString() %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>--%>
        </td>
    </tr>
</table>
<%--</asp:Panel>--%>
<table width="60%">
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
                Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                AllowAutomaticInserts="false" OnNeedDataSource="gvNavChange_OnNeedDataSource">
                <ExportSettings HideStructureColumns="true" ExportOnlyData="true">
                </ExportSettings>
                <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                    CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                        ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="SchemeCode" AllowFiltering="false" HeaderText="Scheme Code"
                            UniqueName="Scheme Code" HeaderStyle-HorizontalAlign="Center">
                            <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="SchemeName" AllowFiltering="false" HeaderStyle-HorizontalAlign="Right"
                            HeaderText="Scheme Name" UniqueName="Adviser Id">
                            <ItemStyle Width="" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CurrentNAV" AllowFiltering="false" HeaderText="Current NAV(AsOn)"
                            UniqueName="Current NAV(AsOn)">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PreviousNAV" AllowFiltering="false" HeaderText="Previous Day NAV"
                            UniqueName="Previous Day NAV">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="PercentChange" AllowFiltering="false" HeaderText="Percentage ChangeNAV"
                            UniqueName="Percentage ChangeNAV" DataFormatString="{0:n2}">
                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
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
