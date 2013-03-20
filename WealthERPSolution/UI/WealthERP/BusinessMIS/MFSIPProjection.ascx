<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFSIPProjection.ascx.cs" Inherits="WealthERP.BusinessMIS.MFSIPProjection" %>
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
    
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 300) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }
    }       
</script>

<script src="jquery.js"></script>

<script>
    $(document).ready(function() {
        $("div").ajaxStart(function() {
        $(this).html("<img src='upload_progress.gif' />");
        });
        $("btnGo_Click").click(function() {
            $("div").load("demo_ajax_load.asp");
        });
    });
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
                            MF SIP projections
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
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        <asp:Label ID="lblGrpOrInd" runat="server" CssClass="FieldName" Text="MIS for :"></asp:Label>
                    </td>
                    <td id="tdSelectCusto" runat="server" align="left" width="10%">
                        <asp:DropDownList ID="ddlSelectCustomer" runat="server" CssClass="cmbField" Style="vertical-align: middle"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlSelectCustomer_SelectedIndexChanged">
                            <asp:ListItem Value="All Customer" Text="All Customer"></asp:ListItem>
                            <asp:ListItem Value="Pick Customer" Text="Pick Customer"></asp:ListItem>
                        </asp:DropDownList>
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
                        <asp:Label ID="lblDate" runat="server" Text="Date filter on: " CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="ddlDateFilter" Style="vertical-align: middle" runat="server"
                            CssClass="cmbField">
                            <asp:ListItem Text="SIP Start Date" Value="StartDate" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="SIP End Date" Value="EndDate"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblFromDate" Text="From:" runat="server" CssClass="FieldName">
                        </asp:Label>
                    </td>
                    <td align="left" width="10%">

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
        </td>
    </tr>
</table>
<table>
<tr>
  <td>
       <table>
          <tr>
          <td>
           <%-- <div runat="server" id="divProjection" style="margin: 2px;width: 640px;">--%>
            <telerik:RadGrid ID="reptCalenderSummaryView" runat="server" GridLines="Both" AutoGenerateColumns="False"
                    AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true" 
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="1060px" AllowFilteringByColumn="false" AllowAutomaticDeletes="True"
                    AllowAutomaticInserts="false" 
                    OnPreRender="reptCalenderSummaryView_PreRender">
                    <PagerStyle Mode="NextPrevAndNumeric" ></PagerStyle>
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                                    FileName="CalenderSummary Details" Excel-Format="ExcelML">
                    </ExportSettings>
                    <MasterTableView Width="100%" ExpandCollapseColumn-ButtonType="ImageButton">
                        <Columns>
                            <telerik:GridBoundColumn DataField="Year" HeaderText="Year" UniqueName="Year" HeaderStyle-Width="90px" FooterText="Grand Total: ">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FinalMonth" HeaderText="Month" HeaderStyle-Width="90px" UniqueName="FinalMonth">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="SIPAmount" HeaderStyle-Width="90px" DataType="System.Decimal"
                                HeaderText="SIP Amount" UniqueName="SIPAmount" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="Sum" DataField="NoOfSIP"  HeaderStyle-Width="90px" HeaderText="No. of SIPs"
                                DataFormatString="{0:N0}" UniqueName="SIPAmount" DataType="System.Int16" FooterText=""
                                FooterStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfFreshSIP" HeaderStyle-Width="90px" DataType="System.Int16"
                                HeaderText="No. of Fresh SIPs" UniqueName="SIPAmount" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="SWPAmount" HeaderStyle-Width="90px" DataType="System.Decimal"
                                HeaderText="SWP Amount" UniqueName="SIPAmount" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfSWP" HeaderStyle-Width="90px" DataType="System.Int16"
                                HeaderText="No. of SWPs" UniqueName="NoOfSWP" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Aggregate="sum" DataField="NoOfFreshSWP"  HeaderStyle-Width="90px" DataType="System.Decimal"
                                HeaderText="No. of fresh SWPs" UniqueName="NoOfFreshSWP" FooterText="" FooterStyle-HorizontalAlign="Right"
                                DataFormatString="{0:N0}">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="" Wrap="false" />
                            </telerik:GridBoundColumn>
                     </Columns>
                    </MasterTableView>       
                    <ClientSettings>
                <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                <ClientEvents OnGridCreated="GridCreated" />
                        <Resizing AllowColumnResize="True"></Resizing>
                    </ClientSettings>
                </telerik:RadGrid>
        </div> 
       </td></tr>
    </table>
 
        </td>
 </tr>
</table>
<table id="tblMessage" width="100%" cellspacing="0" cellpadding="0" runat="server" visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
            </div>
        </td>
    </tr>
</table>
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
