<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdviserRMMFSystematicMIS.ascx.cs"
    Inherits="WealthERP.Advisor.AdviserRMMFSystematicMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="demo" Namespace="DanLudwig.Controls.Web" Assembly="DanLudwig.Controls.AspAjax.ListBox" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scptMgr" runat="server" EnablePartialRendering="true">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    };
</script>

<script src="jquery.js"></script>

<script type="text/javascript" language="javascript">
    $(document).ready(function() {
        $("div").ajaxStart(function() {
            $(this).html("<img src='upload_progress.gif' />");
        });
        $("btnGo_Click").click(function() {
            $("div").load("demo_ajax_load.asp");
        });
    });

    function DisplayDateField() {
        var type = document.getElementById("ddlDateFilter").value;
        alert(type);
        if (type == 'ActiveSIP') {

        }
        else if (type == 'StartDate') {

        }
    }
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 300) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }
    }               
</script>

<style type="text/css" runat="server">
    .rgDataDiv
    {
        height: auto;
        width: 101.5% !important;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblpageHeader" class="HeaderTextBig" runat="server"></asp:Label>
                        </td>
                        <td align="right">
                            <%--<asp:ImageButton ID="btnExportSystematicMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                         runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportSystematicMIS_OnClick"
                         Height="25px" Width="25px"></asp:ImageButton>--%>
                            <asp:ImageButton ID="btnExportSummary" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportSummary_OnClick"
                                Height="25px" Width="25px"></asp:ImageButton>
                            <asp:ImageButton ID="btnExportSystematicMIS" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" Height="20px"
                                Width="25px" Visible="false" OnClick="btnExportSystematicMIS_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table style="width: 100%;">
    <tr>
        <td style="width: 100%;">
            <table style="width: 100%;">
                <tr id="trBranchRM" runat="server">
                    <td align="right" width="10%">
                        <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                            CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%;">
                        <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle">
                        </asp:DropDownList>
                    </td>
                    <%-- <td align="left" style="width: 10%;">
                        <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField">
                            <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                            <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
                        </asp:DropDownList>--%>
                    <%--  </td>--%>
                    <td align="right">
                        <asp:Label ID="lblStatus" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="cmbField">
                            <Items>
                                <asp:ListItem Text="Online" Value="1" />
                                <asp:ListItem Text="Offline" Value="0" />
                                <asp:ListItem Text="All" Value="2"/>
                            </Items>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td id="trCustType" runat="server" align="right" width="10%">
                        <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
                    </td>
                    <td id="tdSelectCusto" runat="server" align="left" width="10%">
                        <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                            <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                            <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 10%;">
                        <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Portfolio:"></asp:Label>
                    </td>
                    <td align="left" style="width: 10%;">
                        <asp:DropDownList ID="ddlPortfolioGroup" runat="server" CssClass="cmbField">
                            <asp:ListItem Text="Managed" Value="1">Managed</asp:ListItem>
                            <asp:ListItem Text="UnManaged" Value="0">UnManaged</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <%--<asp:RadioButton runat="server" ID="rdoAllCustomer" Text="All Customers" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" Checked="True" 
              oncheckedchanged="rdoAllCustomer_CheckedChanged" />
              <br />
              <asp:RadioButton runat="server" ID="rdoPickCustomer" Text="Pick Customer" AutoPostBack="true"
              Class="cmbField" GroupName="SelectCustomer" 
              oncheckedchanged="rdoPickCustomer_CheckedChanged" />--%>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblSelectTypeOfCustomer" runat="server" CssClass="FieldName" Text="Customer Type: "></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlSelectCutomer" Style="vertical-align: middle" runat="server"
                CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCutomer_SelectedIndexChanged">
                <asp:ListItem Value="Select" Text="Select" Selected="True"></asp:ListItem>
                <asp:ListItem Value="Group Head" Text="Group Head"></asp:ListItem>
                <asp:ListItem Value="Individual" Text="Individual"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblselectCustomer" runat="server" CssClass="FieldName" Text="Search Customer: "></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:TextBox ID="txtIndividualCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True">  </asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtIndividualCustomer_water" TargetControlID="txtIndividualCustomer"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtIndividualCustomer_autoCompleteExtender"
                runat="server" TargetControlID="txtIndividualCustomer" ServiceMethod="GetCustomerName"
                ServicePath="~/CustomerPortfolio/AutoComplete.asmx" MinimumPrefixLength="1" EnableCaching="False"
                CompletionSetCount="5" CompletionInterval="100" CompletionListCssClass="AutoCompleteExtender_CompletionList"
                CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem" CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="rquiredFieldValidatorIndivudialCustomer" Display="Dynamic"
                ControlToValidate="txtIndividualCustomer" CssClass="rfvPCG" ErrorMessage="<br />Please select the customer"
                runat="server" ValidationGroup="btnGo">
            </asp:RequiredFieldValidator>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="right" width="10%">
            <asp:Label ID="lblSystematicType" runat="server" Text="Systematic Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlSystematicType" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblAMC" runat="server" Text="AMC:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlAMC" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAMC_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField" AutoPostBack="false">
            </asp:DropDownList>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblScheme" runat="server" Text="Scheme:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField" AutoPostBack="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right" width="10%">
            <asp:Label ID="lblDate" runat="server" Text="SIP filter on: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" width="10%">
            <asp:DropDownList ID="ddlDateFilter" Style="vertical-align: middle" runat="server"
                AutoPostBack="true" CssClass="cmbField" OnSelectedIndexChanged="ddlDateFilter_SelectedIndexChanged">
                <asp:ListItem Text="Active SIP" Value="ActiveSIP" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Expired SIP" Value="ExpiredSIP"></asp:ListItem>
                <asp:ListItem Text="Ceased SIP" Value="CeasedSIP"></asp:ListItem>
                <asp:ListItem Text="SIP Start Date" Value="StartDate"></asp:ListItem>
                <asp:ListItem Text="SIP End Date" Value="EndDate"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
            </asp:Label>
        </td>
        <td align="left" width="10%">
            <%--<asp:TextBox ID="txtFrom" runat="server" CssClass="txtField"></asp:TextBox>
         <span id="SpanFromDate" class="spnRequiredField">*</span>
         <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" TargetControlID="txtFrom" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
         </ajaxToolkit:CalendarExtender>
         <ajaxToolkit:TextBoxWatermarkExtender ID="txtFrom_TextBoxWatermarkExtender" runat="server" TargetControlID="txtFrom" WatermarkText="dd/mm/yyyy" Enabled="True">
         </ajaxToolkit:TextBoxWatermarkExtender>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txtFrom" CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" runat="server" ValidationGroup="btnGo">
          </asp:RequiredFieldValidator>--%>
            <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td align="right" width="10%">
            <asp:Label ID="lblToDate" runat="server" CssClass="FieldName" Text="To:"></asp:Label>
        </td>
        <td align="left" width="10%" valign="middle" colspan="2">
            <%--<asp:TextBox ID="txtTo" runat="server" CssClass="txtField"></asp:TextBox>
          <span id="SpanToDate" class="spnRequiredField">*</span>
              <ajaxToolkit:CalendarExtender ID="txtTo_CalendarExtender" runat="server" TargetControlID="txtTo" Format="dd/MM/yyyy" Enabled="True" PopupPosition="TopRight">
               </ajaxToolkit:CalendarExtender>
                <ajaxToolkit:TextBoxWatermarkExtender ID="txtTo_TextBoxWatermarkExtender" runat="server" TargetControlID="txtTo" WatermarkText="dd/mm/yyyy" Enabled="True">
                </ajaxToolkit:TextBoxWatermarkExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtTo" CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                runat="server" ValidationGroup="btnGo">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date" Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo">
               </asp:CompareValidator>--%>
            <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                    ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                </Calendar>
                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                </DateInput>
            </telerik:RadDatePicker>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <asp:Button ID="btnGo" runat="server" Text="Go" ValidationGroup="btnGo" CssClass="PCGButton"
                OnClick="btnGo_Click" />
        </td>
    </tr>
</table>
</td> </tr> </table>
<%--<asp:Panel ID="Pnlsystematic" runat="server" class="Landscape" ScrollBars="Horizontal"
    Visible="false">--%>
<table style="width: 100%" cellspacing="0" cellpadding="1">
    <tr>
        <td>
            <%--    <table id="Table1" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblNote1" runat="server" Text="NOTE: To view SIP details Please select filters and click go. To sort on a field click on its label."
                    Font-Size="Small" CssClass="cmbField"></asp:Label>
            </td>
        </tr>
    </table>--%>
            <div style="width: 77.2%;">
                <telerik:RadGrid ID="gvSystematicMIS" AllowSorting="true" runat="server" AllowAutomaticInserts="false"
                    EnableLoadOnDemand="True" AllowFilteringByColumn="true" AllowPaging="True" AutoGenerateColumns="False"
                    EnableEmbeddedSkins="false" GridLines="none" ShowFooter="true" PagerStyle-AlwaysVisible="true"
                    EnableViewState="true" ShowStatusBar="true" Skin="Telerik" ExportSettings-FileName="MF SIP MIS"
                    OnNeedDataSource="gvSystematicMIS_OnNeedDataSource">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                        Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn DataField="CustomerName" HeaderText="CustomerName" HeaderStyle-Width="100px"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" UniqueName="CustomerName"
                                FooterText="Grand Total:" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SystematicTransactionType" ShowFilterIcon="false"
                                HeaderStyle-Width="50px" FilterControlWidth="30px" AutoPostBackOnFilter="true"
                                HeaderText="Type" UniqueName="SystematicTransactionType">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="AMCname" HeaderText="AMC" 
                                       UniqueName="AMCname">
                                       <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                   </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn DataField="SchemePlaneName" HeaderStyle-Width="400px" ShowFilterIcon="false"
                                FilterControlWidth="370px" AutoPostBackOnFilter="true" HeaderText="Scheme" UniqueName="SchemePlaneName">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Wrap="true" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FolioNumber" HeaderText="Folio" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="FolioNumber">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="SubbrokerCode" HeaderText="SubBroker Code" ShowFilterIcon="false"
                                HeaderStyle-Width="80px" AutoPostBackOnFilter="true" UniqueName="SubbrokerCode"
                                SortExpression="SubbrokerCode">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridBoundColumn DataField="CustomerName" HeaderText="Customer" ShowFilterIcon="false"
                                    AutoPostBackOnFilter="true" UniqueName="CustomerName" FooterText="Grand Total:"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                </telerik:GridBoundColumn>--%>
                            <%--<telerik:RadComboBox ID="RadComboBoxSip" runat="server" width ="120px" CssClass="cmbField" OnClientSelectedIndexChanged="SipIndexChanged" AutoPostBackOnFilter="true" AutoPostBack="true" >
                                           <Items>
                                            <telerik:RadComboBoxItem Text="Select" Value="Select"
                                                Selected="true"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem Text="ACTIVE" Value="ACTIVE" 
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem  Text="EXPIRED"
                                                Value="EXPIRED" runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem  Text="CEASED" Value="CEASED"
                                                runat="server"></telerik:RadComboBoxItem>
                                        </Items>
                                 </telerik:RadComboBox>
                                <telerik:RadScriptBlock ID="RadScriptBlock2" runat="server"> 
                                <script type="text/javascript">
                                    function SipIndexChanged(sender, args) {
                                        var tableView = $find("<%# ((GridItem)Container).OwnerTableView.ClientID %>");
                                        tableView.filter("Select", args.get_item().get_text(), 1);
                                    } 
                                </script> 
                                 </telerik:RadScriptBlock> --%>
                            <telerik:GridDateTimeColumn SortExpression="StartDate" DataField="StartDate" HeaderStyle-Width="85px"
                                ShowFilterIcon="false" AllowFiltering="false" AutoPostBackOnFilter="true" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderText="Start Date" UniqueName="StartDate">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Wrap="false" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="StartDateFilter" AutoPostBack="true" runat="server">
                                    </telerik:RadDatePicker>
                                </FilterTemplate>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="EndDate" DataFormatString="{0:dd/MM/yyyy}"
                                HeaderStyle-Width="85px" ShowFilterIcon="false" AllowFiltering="false" AutoPostBackOnFilter="true"
                                HeaderText="End Date" UniqueName="EndDate" SortExpression="EndDate">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="EndDateFilter" AutoPostBack="true" runat="server">
                                    </telerik:RadDatePicker>
                                </FilterTemplate>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="CeaseDate" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="91px"
                                ShowFilterIcon="false" AllowFiltering="false" AutoPostBackOnFilter="true" HeaderText="Stopped Date"
                                UniqueName="CeaseDate" SortExpression="CeaseDate">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Wrap="false" />
                                <FilterTemplate>
                                    <telerik:RadDatePicker ID="CeaseDateFilter" AutoPostBack="true" runat="server">
                                    </telerik:RadDatePicker>
                                </FilterTemplate>
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Frequency" HeaderText="Frequency" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="Frequency">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="NextSystematicDate" HeaderText="Next Date"
                                HeaderStyle-Width="85px" ShowFilterIcon="false" AllowFiltering="false" AutoPostBackOnFilter="false"
                                DataFormatString="{0:dd/MM/yyyy}" UniqueName="NextSystematicDate" SortExpression="NextSystematicDate">
                                <ItemStyle HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn Aggregate="Sum" AllowFiltering="false" DataField="Amount"
                                HeaderStyle-Width="85px" DataType="System.Decimal" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                HeaderText="Amount" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"
                                UniqueName="Amount">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="REMARKS" HeaderText="Remarks" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="REMARKS">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CMFA_IsOnline" HeaderText="IsOnline" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true" UniqueName="CMFA_IsOnline">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Width="100px" />
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="True" ScrollHeight="300px"></Scrolling>
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        <Resizing AllowColumnResize="false" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </td>
    </tr>
</table>
<%--</asp:Panel>--%>
<%--    <table id="tblNote" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblNote" runat="server" Text="Note: The view displays the expected monthly order flow for the individual schemes displayed on the systematic set up tab."
                    Font-Size="Small" CssClass="cmbField"></asp:Label>
            </td>
        </tr>
    </table>--%>
<%--<asp:Panel ID="Panel1" runat="server" class="Landscape" ScrollBars="Horizontal" Visible="false">--%>
<%--<style id="Style1" type="text/css" runat="server">
        .rgDataDiv
        {
            height: auto;
            width: 100% !important;
        }
    </style>--%>
<table width="100%" cellspacing="0" cellpadding="0">
    <tr>
        <td>
            <div style="width: 85%;">
                <telerik:RadGrid ID="reptCalenderSummaryView" runat="server" GridLines="none" AutoGenerateColumns="False"
                    AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticDeletes="True"
                    AllowAutomaticInserts="false" OnItemDataBound="reptCalenderSummaryView_ItemDataBound"
                    OnNeedDataSource="reptCalenderSummaryView_OnNeedDataSource">
                    <PagerStyle Mode="NextPrevAndNumeric"></PagerStyle>
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                        FileName="MF SIP Projections" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView Width="101.5%" ExpandCollapseColumn-ButtonType="ImageButton">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" UniqueName="Year" HeaderStyle-Width="90px"
                                FooterText="Grand Total: ">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FinalMonth" HeaderText="Month" HeaderStyle-Width="90px"
                                UniqueName="FinalMonth">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="SIP Amount" UniqueName="SIPAmount"  ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSIPAmount" Text='<%# Eval("SIPAmount")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblSIPAmountFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="SIPAmount" HeaderStyle-Width="90px"
                                DataType="System.Decimal" HeaderText="SIP Amount" UniqueName="SIPAmount" FooterText=""
                                FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="Sum" DataField="NoOfSIP" HeaderStyle-Width="90px"
                                HeaderText="No. of SIPs" DataFormatString="{0:N0}" UniqueName="SIPAmount" DataType="System.Int16"
                                FooterText="" FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="No. of Fresh SIPs" UniqueName="NoOfFreshSIP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSIP" Text='<%# Eval("NoOfFreshSIP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSIPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfFreshSIP" HeaderStyle-Width="90px"
                                DataType="System.Int16" HeaderText="No. of Fresh SIPs" UniqueName="SIPAmount"
                                FooterText="" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="SWP Amount" UniqueName="NoOfFreshSIP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblSWPAmount" Text='<%# Eval("SWPAmount")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblSWPAmountFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="SWPAmount" HeaderStyle-Width="90px"
                                DataType="System.Decimal" HeaderText="SWP Amount" UniqueName="SIPAmount" FooterText=""
                                FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridTemplateColumn HeaderText="No. of SWPs" UniqueName="NoOfSWP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate >
                        <asp:Label runat="server" ID="lblNoOfSWP" Text='<%# Eval("NoOfSWP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfSWPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfSWP" HeaderStyle-Width="90px"
                                DataType="System.Int16" HeaderText="No. of SWPs" UniqueName="NoOfSWP" FooterText=""
                                FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn HeaderText="No. of fresh SWPs" UniqueName="NoOfFreshSWP" ItemStyle-HorizontalAlign="Right" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSWP" Text='<%# Eval("NoOfFreshSWP")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label runat="server" ID="lblNoOfFreshSWPFooter" Text=""></asp:Label>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfFreshSWP" HeaderStyle-Width="90px"
                                DataType="System.Decimal" HeaderText="No. of fresh SWPs" UniqueName="NoOfFreshSWP"
                                FooterText="" FooterStyle-HorizontalAlign="Right" DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                    <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                <img alt="" src="upload_progress.gif"
                style="width: 100px; height: 100px" />
                </ProgressTemplate>
              </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        </asp:UpdatePanel>--%>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300Px" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Resizing AllowColumnResize="false"></Resizing>
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </td>
    </tr>
</table>
<%--</asp:Panel>--%>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
<%--<%--<asp:HiddenField ID="hdnALL" runat="server" />--%>
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnAll" runat="server" />
<asp:HiddenField ID="hdnbranchId" runat="server" />
<asp:HiddenField ID="hdnbranchheadId" runat="server" />
<asp:HiddenField ID="hdnrmId" runat="server" />
<asp:HiddenField ID="hdnstartdate" runat="server" />
<asp:HiddenField ID="hdnendDate" runat="server" />
<asp:HiddenField ID="hdnamcCode" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnschemeCade" runat="server" />
<asp:HiddenField ID="hdnSystematicType" runat="server" />
<asp:HiddenField ID="hdnTodate" runat="server" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnIndividualOrGroup" runat="server" />
<asp:HiddenField ID="hdnAgentCode" runat="server" />
<asp:HiddenField ID="hdnStatus" runat="server" />
