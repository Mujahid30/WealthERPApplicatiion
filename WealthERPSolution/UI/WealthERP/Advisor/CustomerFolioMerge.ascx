<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFolioMerge.ascx.cs" Inherits="WealthERP.Advisor.CustomerFolioMerge" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="/Scripts/jquery.colorbox-min.js" type="text/javascript"></script>

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

<script type="text/javascript">
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 300) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }
    }

    function CheckFolioSelected() {
        var Count = 0;
        Parent = document.getElementById("<%=gvCustomerFolioMerge.ClientID %>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].checked) {
                Count++
            }
        }
        if (Count == 0) {
            alert("Please select a folio");
            return false;
        }
    }

    function CheckOtherIsCheckedByGVID(spanChk) {

        var IsChecked = spanChk.checked;
        var CurrentRdbID = spanChk.id;
        var Chk = spanChk;
        //var n = document.getElementById("gvCustomerFolioMerge").rows.length;
        Parent = document.getElementById("<%=gvCustomerFolioMerge.ClientID %>");
        var items = Parent.getElementsByTagName('input');
        for (i = 0; i < items.length; i++) {
            if (items[i].id != CurrentRdbID && items[i].type == "checkbox") {
                if (items[i].checked) {
                    items[i].checked = false;
                    alert("Please select one customer at a time.");
                }
            }
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
<link href="/CSS/colorbox.css" rel="stylesheet" type="text/css" />
<style type="text/css">
    .CheckField
    {
        font-family: Verdana,Tahoma;
        font-weight: normal;
        font-size: 11px;
        color: #16518A;
    }
</style>
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Accounts
                        </td>
                        <td align="right">
                            <asp:ImageButton Visible="false" ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="20px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td colspan="2">
            <table class="TableBackground" width="100%">
                <tr id="trBranchRM" runat="server">
                    <td align="right" valign="top" class="leftLabel">
                        <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                    </td>
                    <td valign="top" class="rightData">
                        <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                            CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 1%">
                        &nbsp;&nbsp;&nbsp
                    </td>
                    <td align="right" valign="top" class="leftLabel">
                        <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
                    </td>
                    <td valign="top" class="rightData">
                        <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" Style="vertical-align: middle">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trZCCS" runat="server" visible="false">
                    <td align="right">
                        <asp:Label ID="lblBrokerCode" runat="server" Text="Sub Broker Code:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField">
                            <%--  <asp:ListItem Text="SubBroker Code" Value="0" Selected="true"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr colspan="2">
        <td id="tdGoBtn" runat="server" colspan="7">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnGo_Click"
                ValidationGroup="vgBtnGo" />
        </td>
    </tr>
</table>
<table width="70%">
    <tr id="trSelect" runat="server" visible="false">
        <td style="width: 100px">
        </td>
        <td style="width: 120px" align="right">
            <asp:Label ID="lblSelect" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 300px">
            <asp:DropDownList ID="ddlSelect" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSelect_SelectedIndexChanged">
                <asp:ListItem Value="S">Select</asp:ListItem>
                <asp:ListItem Value="1">Online</asp:ListItem>
                <asp:ListItem Value="0">Offline</asp:ListItem>
                <asp:ListItem Value="2">All</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 200px">
            
        </td>
    </tr>
    <tr id="trAction" runat="server" visible="false">
        <td style="width: 100px">
        </td>
        <td style="width: 120px" align="right">
            <asp:Label ID="lblAction" runat="server" Text="Action:" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width: 300px">
            <asp:DropDownList ID="ddlMovePortfolio" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlMovePortfolio_SelectedIndexChanged">
                <asp:ListItem Value="S">Select</asp:ListItem>
                <asp:ListItem Value="Merge">Folio merge</asp:ListItem>
                <asp:ListItem Value="MtoAC">Folio transfer to another customer</asp:ListItem>
                <asp:ListItem Value="MtoAP">Folio Transfer to another portfolio</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width: 200px">
            <%--<asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClientClick="return CheckFolioSelected();"
        onclick="btnGo_Click" />--%>
        </td>
    </tr>
    <tr id="trMergeToAnotherAMC" runat="server" visible="false">
        <td style="width: 100px">
        </td>
        <td align="right">
            <asp:Label ID="lblMergeTo" Text="Merge to folio:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAdvisorBranchList" runat="server" CssClass="cmbLongField">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:Button ID="btnmerge" CssClass="PCGButton" runat="server" ValidationGroup="vgBtn"
                OnClick="btnEdit_Click" Text="Submit" OnClientClick="return CheckFolioSelected();" />
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgBtn" ControlToValidate="ddlAdvisorBranchList" ValueToCompare="null" ErrorMessage="Field is required" Operator="NotEqual" ></asp:CompareValidator>--%>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
            <asp:Label ID="lblerror" Text="No Folios to merge" CssClass="rfvPCG" Visible="false"
                runat="server"></asp:Label>
        </td>
    </tr>
    <tr id="trPickCustomer" runat="server" visible="false">
        <td>
        </td>
        <td align="right">
            <asp:Label ID="lblPickCustomer" Text="Customer:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <%--<asp:TextBox ID="txtPickCustomer" runat="server" CssClass="txtField" OnValueChanged="txtPickCustomer_ValueChanged" ></asp:TextBox>--%>
            <asp:TextBox ID="txtPickCustomer" runat="server" CssClass="txtLongAddField" AutoComplete="Off"
                AutoPostBack="True" OnTextChanged="txtPickCustomer_TextChanged"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtPickCustomer_water" TargetControlID="txtPickCustomer"
                WatermarkText="Enter few characters" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtPickCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtPickCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <span id="Span1" class="spnRequiredField">*</span>
            <%--<span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                few characters of customer name.</span>--%>
        </td>
        <td>
            <asp:RequiredFieldValidator ID="rfvPickCustomer" ControlToValidate="txtPickCustomer"
                ErrorMessage="Please select a customer." Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trPickPortfolio" runat="server" visible="false">
        <td style="width: 100px">
        </td>
        <td align="right">
            <asp:Label ID="lblPickPortfolio" Text="Portfolio:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbLongField">
                <asp:ListItem Value="0">Select </asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">* </span>
        </td>
        <td>
            <span id="Span4" class="spnRequiredField"><span id="Span3" class="spnRequiredField">
                <asp:RequiredFieldValidator ID="rfvddlPortfolio" ControlToValidate="ddlPortfolio"
                    ErrorMessage="Please pick a portfolio" Display="Dynamic" runat="server" CssClass="rfvPCG"
                    ValidationGroup="btnSubmit">
                </asp:RequiredFieldValidator>
            </span></span>
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server" visible="false">
        <td>
        </td>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSubmitPortfolio" CssClass="PCGButton" runat="server" ValidationGroup="btnSubmit"
                OnClientClick="return CheckFolioSelected();" Text="Submit" OnClick="btnSubmitPortfolio_Click" />
        </td>
        <td>
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="trFolioStatus" runat="server">
        <td align="center">
            <div id="msgFolioStatus" runat="server" class="success-msg" align="center">
                Folio Moved Successfully
            </div>
        </td>
    </tr>
    <tr id="trMergeFolioStatus" runat="server">
        <td align="center">
            <div id="msgMergeFolio" runat="server" class="success-msg" align="center">
                Folio Merged Successfully
            </div>
        </td>
    </tr>
</table>
<table width="100%" cellspacing="0" cellpadding="2">
    <%-- <td class="leftField" align="right">
        <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
        <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
    </td>--%>
    <tr>
        <td>
            <asp:Label ID="Label1" Visible="false" class="Field" Text="Note: Select the folios below that needs to be transferred/merged."
                runat="server"></asp:Label>
        </td>
    </tr>
    <%--<td colspan="3" allign="center">
            <asp:GridView ID="gvCustomerFolioMerge" runat="server" CellPadding="4" CssClass="GridViewStyle"
                AllowSorting="True" ShowFooter="true" AutoGenerateColumns="False" DataKeyNames="CustomerId,AMCCode,Count,portfilionumber"
                OnSelectedIndexChanged="gvCustomerFolioMerge_SelectedIndexChanged">
                <RowStyle CssClass="RowStyle" />
                <FooterStyle CssClass="FooterStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" VerticalAlign="Middle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle " />
                <Columns>
                    <asp:TemplateField HeaderText="Select">
                        <HeaderTemplate>
                            <asp:Label ID="LblSelect" runat="server" Text=""></asp:Label>
                            <br />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="rdbGVRow" OnCheckedChanged="rdbGVRow_CheckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                runat="server" AutoPostBack="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="GroupHead" HeaderText="Group Head">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblCustomerName" runat="server" Text="Name"></asp:Label>
                            <br />
                            <asp:TextBox ID="txtCustNameSearch" Width="50%" runat="server" CssClass="GridViewTxtField"
                                onkeydown="return JSdoPostback(event,'ctrl_AdvisorCustomerAccounts_btnCustomerSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="hypCustomerName" runat="server" OnClick="lnkCustomerName_Click"
                                Text='<%# Eval("CustomerName").ToString() %>'>HyperLink</asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblAMCName" runat="server" Text="AMC Name"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAMC" runat="server" Text='<%# Eval("AMCName").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblcount" runat="server" Text="Folios"></asp:Label><br />
                            <asp:TextBox ID="txtFolioSearch" Width="50%" runat="server" CssClass="GridViewTxtField"
                                onkeydown="return JSdoPostback(event,'ctrl_AdvisorCustomerAccounts_btnFolioNumberSearch');" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="hypFolioNo" runat="server" CssClass="CmbField" OnClick="hypFolioNo_Click"
                                Text='<%# Eval("Count").ToString() %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:BoundField DataField="FolioName" HeaderText="Folio Name">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblmergeStatus" runat="server" Text="Merged To"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblmergerstatus" ItemStyle-HorizontalAlign="Right" runat="server"
                                Text='<%# Eval("mergerstatus").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Wrap="false" HeaderStyle-Wrap="false">
                        <HeaderTemplate>
                            <asp:Label ID="lblprocessId" runat="server" Text="ProcessId"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblprocessId" runat="server" Text='<%# Eval("processId").ToString() %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <ItemStyle Wrap="False"></ItemStyle>
                    </asp:TemplateField>
                </Columns>OnItemCommand="rgvMultiProductMIS_ItemCommand"
            </asp:GridView>
        </td>--%>
    <tr>
        <td>
            <div id="DivCustomerFolio" style="width: 42%;" visible="false" runat="server">
                <telerik:RadGrid ID="gvCustomerFolioMerge" runat="server" CssClass="RadGrid" GridLines="None"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="false" ShowStatusBar="true"
                    AllowAutomaticDeletes="True" AllowAutomaticInserts="false" AllowAutomaticUpdates="false"
                    Skin="Telerik" EnableEmbeddedSkins="false" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true"
                    AllowFilteringByColumn="true" OnNeedDataSource="gvCustomerFolioMerge_NeedDataSource">
                    <exportsettings hidestructurecolumns="false" exportonlydata="true" filename="ExistMFInvestlist">
                    </exportsettings>
                    <mastertableview datakeynames="CustomerId,AMCCode,Count,portfilionumber,CMFA_AccountId"
                        commanditemdisplay="None" width="100%">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="40px" UniqueName="rdbGVRowId">
                                <ItemTemplate>
                                    <asp:CheckBox ID="rdbGVRow" OnCheckedChanged="rdbGVRow_CheckedChanged"   onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                        runat="server" AutoPostBack="true" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="Group Head" HeaderText="Group Head" DataField="GroupHead"
                                HeaderStyle-Width="100px" SortExpression="GroupHead" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CustomerName" HeaderText="Customer" DataField="CustomerName"
                                HeaderStyle-Width="100px" SortExpression="CustomerName" AllowFiltering="true"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="AMCName" HeaderText="AMC" DataField="AMCName"
                                HeaderStyle-Width="225px" SortExpression="AMCName" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <%-- <telerik:GridTemplateColumn DataField="Count" AllowFiltering="true" HeaderText="Folios" HeaderStyle-Width="100px" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            <ItemTemplate>
                         <asp:LinkButton ID="hypFolioNo" runat="server" CssClass="CmbField" OnClick="hypFolioNo_Click"
                                Text='<%# Eval("Count").ToString() %>'>
                            </asp:LinkButton>
                              </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                          <telerik:GridBoundColumn UniqueName="Count" HeaderText="Folios" DataField="Count"
                                HeaderStyle-Width="225px" SortExpression="Count" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                      <%--      <telerik:GridTemplateColumn UniqueName="Count" HeaderText="Folios" HeaderStyle-Width="50px"
                                AllowFiltering="false" SortExpression="Count" ItemStyle-HorizontalAlign="left"
                                DataField="Count" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkMF" runat="server" Text='<%#Eval("Count")%>' CommandName="Redirect" OnClick="LinkButton1_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <%-- <telerik:GridTemplateColumn UniqueName="Count" HeaderText="Folios" Groupable="False"
                            ItemStyle-Wrap="false" AllowFiltering="true"  SortExpression="Count" ItemStyle-HorizontalAlign="Right"
                            DataField="Count" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate >
                                <asp:LinkButton ID="lnkMF" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Count")%>' 
                                    CommandName="Redirect"  ></asp:LinkButton>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>--%>
                            <%--<telerik:GridBoundColumn UniqueName="Count" HeaderText="Folios" DataField="Count"
                                HeaderStyle-Width="100px" SortExpression="Count" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                                 <ItemTemplate >
                                <asp:LinkButton ID="lnkMF" runat="server" Text='<%# String.Format("{0:N0}", DataBinder.Eval(Container.DataItem, "Mutual_Fund")) %>' 
                                    CommandName="Redirect"  >
                                    </asp:LinkButton>
                            </ItemTemplate>
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="FolioName" HeaderText="Folio Name" DataField="FolioName"
                                HeaderStyle-Width="100px" SortExpression="FolioName" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="mergerstatus" HeaderText="Merged To" DataField="mergerstatus"
                                HeaderStyle-Width="100px" SortExpression="mergerstatus" AllowFiltering="true"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="IsOnline" HeaderText="Is Online" DataField="IsOnline"
                                HeaderStyle-Width="100px" SortExpression="IsOnline" AllowFiltering="true"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            
                            <%-- <telerik:GridBoundColumn UniqueName="CMFA_BROKERCODE" HeaderText="BrokerCode"
                                DataField="CMFA_BROKERCODE" HeaderStyle-Width="50px" SortExpression="CMFA_BROKERCODE"
                                AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="processId" HeaderText="Request Id" DataField="processId"
                                HeaderStyle-Width="108px" SortExpression="processId" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Nominee" HeaderStyle-Width="100px" HeaderText="Nominee"
                                DataField="Nominee" SortExpression="Nominee" AllowFiltering="true" ShowFilterIcon="false"
                                AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ModeOfHolding" HeaderStyle-Width="80px" HeaderText="Mode Of Holding"
                                DataField="ModeOfHolding" SortExpression="ModeOfHolding" AllowFiltering="true"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true">
                                <HeaderStyle></HeaderStyle>
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CMFA_SubBrokerCode" HeaderText="SubBrokerCode"
                                DataField="CMFA_SubBrokerCode" HeaderStyle-Width="120px" SortExpression="CMFA_SubBrokerCode"
                                AllowFiltering="true" ShowFilterIcon="false" AutoPostBackOnFilter="true">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AssociatesName" AllowFiltering="true" HeaderText="SubBroker Name"
                                Visible="true" UniqueName="AssociatesName" SortExpression="AssociatesName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ChannelName" AllowFiltering="true" HeaderText="Channel"
                                UniqueName="ChannelName" SortExpression="ChannelName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Titles" AllowFiltering="true" HeaderText="Title"
                                Visible="true" UniqueName="Titles" SortExpression="Titles" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ReportingManagerName" AllowFiltering="true" HeaderText="Reporting Manager"
                                Visible="true" UniqueName="ReportingManagerName" SortExpression="ReportingManagerName"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="120px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="UserType" AllowFiltering="true" HeaderText="Type"
                                Visible="true" UniqueName="UserType" SortExpression="UserType" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="70px"
                                FilterControlWidth="50px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="true" DataField="ClusterManager" AllowFiltering="true"
                                HeaderText="Cluster Manager" UniqueName="ClusterManager" SortExpression="ClusterManager"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AreaManager" AllowFiltering="true" HeaderText="Area Manager"
                                UniqueName="AreaManager" SortExpression="AreaManager" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ZonalManagerName" AllowFiltering="true" HeaderText="Zonal Manager"
                                UniqueName="ZonalManagerName" SortExpression="ZonalManagerName" ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DeuptyHead" AllowFiltering="true" HeaderText="Deupty Head"
                                UniqueName="DeuptyHead" SortExpression="DeuptyHead" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="130px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                        </Columns>
                    </mastertableview>
                    <clientsettings reordercolumnsonclient="True" allowcolumnsreorder="True" enablerowhoverstyle="true">
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="300px" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="true" />
                    </clientsettings>
                </telerik:RadGrid>
            </div>
        </td>
    </tr>
</table>
<table id="ErrorMessage" width="100%" cellspacing="0" cellpadding="0" runat="server"
    visible="false">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage1" runat="server" visible="true" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <%-- <tr style="width: 100%">
        <td colspan="3">
            <table width="100%">
                <tr id="trPager" runat="server" width="100%">
                    <td align="right">
                        <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                    </td>
                </tr>
            </table>
        </td>
    </tr>--%>
    <tr id="trReassignBranch" runat="server">
        <td class="SubmitCell" align="left">
            &nbsp &nbsp &nbsp
        </td>
        <td>
            &nbsp &nbsp
            <%--<span id="spanAdvisorBranch" class="spnRequiredField" runat="server">*</span>--%>
        </td>
        <td>
            &nbsp &nbsp
        </td>
    </tr>
</table>
<%--  <table>
    <tr>
        <td style="width:150px">
            &nbsp;</td>
        <td>    
        <asp:RadioButton ID="rdbMerge" runat="server" Text="Merge" GroupName="FolioMove" Class="FieldName" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:RadioButton ID="rdbMoveFolioToCustomer" runat="server" GroupName="FolioMove" Class="FieldName"
            Text="Move Folio to another Customer" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <asp:RadioButton ID="rdbMoveFolioToPortfolio" runat="server" Checked="true" GroupName="FolioMove" Class="FieldName"
            Text="Move Folio to another Portfolio"/>
        </td>
    </tr>
    
    <tr>
        <td></td>
        <td></td>
    </tr>
  </table>--%>
<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAgentId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAgentCode" runat="server" Visible="false" />
<asp:HiddenField ID="hdnIsassociate" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server" />
<asp:HiddenField ID="hdnCustomerId" runat="server" Value="0" OnValueChanged="txtPickCustomer_TextChanged" />
<%--<asp:HiddenField ID="hdnCurrentPage" runat="server" />--%>
<asp:Button ID="btnCustomerSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent" />
<%--<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnBranchFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNameFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />--%>
<asp:Button ID="btnFolioNumberSearch" runat="server" Text="" BorderStyle="None" BackColor="Transparent" />
