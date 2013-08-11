<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AssociateCustomerList.ascx.cs"
    Inherits="WealthERP.Advisor.AssociateCustomerList" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>

<script language="javascript" type="text/javascript">
    function GridCreated(sender, args) {
        var scrollArea = sender.GridDataDiv;
        var dataHeight = sender.get_masterTableView().get_element().clientHeight;
        if (dataHeight < 380) {
            scrollArea.style.height = dataHeight + 17 + "px";
        }
    }

    // window.onresize = window.onload = Resize;
    function showmessage() {
        var bool = window.confirm('Are you sure you want to delete this profile?');

        if (bool) {
            document.getElementById("ctrl_AssociateCustomerList_hdnMsgValue").value = 1;
            document.getElementById("ctrl_AssociateCustomerList_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_AssociateCustomerList_hdnMsgValue").value = 0;
            document.getElementById("ctrl_AssociateCustomerList_hiddenassociation").click();
            return true;
        }
    }
    function OnClientItemsRequestedHandler(sender, eventArgs) {
        //set the max allowed height of the combo  
        var MAX_ALLOWED_HEIGHT = 220;
        //this is the single item's height  
        var SINGLE_ITEM_HEIGHT = 22;

        var calculatedHeight = sender.get_items().get_count() * SINGLE_ITEM_HEIGHT;

        var RadComboRM = sender.get_dropDownElement();

        if (calculatedHeight > MAX_ALLOWED_HEIGHT) {
            setTimeout(
            function() {
                RadComboRM.firstChild.style.height = MAX_ALLOWED_HEIGHT + "px";
            }, 20
        );
        }
        else {
            setTimeout(
            function() {
                RadComboRM.firstChild.style.height = calculatedHeight + "px";
            }, 20
        );
        }
    }

    function showassocation() {

        var bool = window.confirm('Customer has associations, cannot be deleted');
        if (bool) {
            document.getElementById("ctrl_AssociateCustomerList_hdnassociation").value = 1;
            document.getElementById("ctrl_AssociateCustomerList_hiddenassociationfound").click();
            return false;
        }
        else {
            document.getElementById("ctrl_AssociateCustomerList_hdnassociation").value = 0;
            document.getElementById("ctrl_AssociateCustomerList_hiddenassociationfound").click();
            return true;
        }
    }
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=200,height=100,toolbar=0,scrollbars=0,status=0,resizable=0,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }
    function getScrollBottom(p_oElem) {
        return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
    }
</script>

<script language="javascript" type="text/javascript">
    function semicolon(text, e) {
        var regx, flg;
        regx = /[;]/
        flg = regx.test(text.value);
        if (flg) {
            var val = text.value;
            val = val.substr(0, (val.length) - 1)
            text.value = val;
        }
    }
</script>
<style id="Style1" type="text/css" runat="server">
    .rgDataDiv
    {
        height: auto;
        width: 101.5% !important;
        margin-right: 15%;
    }
</style>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            Associate Customer List
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="imgexportButton" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                Visible="false" runat="server" AlternateText="Excel" ToolTip="Export To Excel"
                                OnClick="btnExportData_OnClick" OnClientClick="setFormat('excel')" Height="22px"
                                Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>

<table class="TableBackground" cellpadding="2">
    <tr id="trDdlAdviser" runat="server">
        <td id="tdLblAdviser" runat="server" align="right">
            <asp:Label ID="lblAdviser" CssClass="FieldName" runat="server" Text="Please Select Adviser:"></asp:Label>
        </td>
        <td id="tdDdlAdviser" runat="server" align="left">
            <asp:DropDownList ID="ddlAdviser" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAdviser_OnSelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
</table>

<table width="100%" cellspacing="0" cellpadding="1">
    <tr>
        <td>
            <div id="DivCustomerList" runat="server" style="width: 52.8%" visible="false">
                <telerik:RadGrid ID="gvAssocCustList" runat="server" AllowAutomaticDeletes="false"
                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                    ShowStatusBar="false" ShowFooter="false" AllowPaging="true" AllowSorting="true"
                    GridLines="none" AllowAutomaticInserts="false" OnItemCreated="gvAssocCustList_ItemCreated"
                    OnItemDataBound="gvAssocCustList_ItemDataBound" Skin="Telerik" EnableHeaderContextMenu="true"
                    OnNeedDataSource="gvAssocCustList_OnNeedDataSource" OnPreRender="gvAssocCustList_PreRender">
                    <ExportSettings HideStructureColumns="true">
                    </ExportSettings>
                    <MasterTableView DataKeyNames="CustomerId,UserId,RMId" Width="99%" AllowMultiColumnSorting="True"
                        AutoGenerateColumns="false">
                        <Columns>
                            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Action" DataField="Action"
                                HeaderStyle-Width="140px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlAction" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChanged"
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" AutoPostBack="true"
                                        Width="120px" AppendDataBoundItems="true">
                                        <Items>
                                            <asp:ListItem Text="Select" Value="Select" Selected="true" />
                                            <asp:ListItem Text="ShortCuts" Value="QuickLinks" />
                                            <asp:ListItem Text="Dashboard" Value="Dashboard" />
                                            <asp:ListItem Text="Profile" Value="Profile" />
                                            <asp:ListItem Text="Assets" Value="Portfolio" />
                                            <asp:ListItem Text="Alerts" Value="Alerts" />
                                            <asp:ListItem Text="Delete Profile" Value="Delete Profile" />
                                            <asp:ListItem Text="Financial Planning" Value="FinancialPlanning" />
                                        </Items>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="CustomerName" UniqueName="CustomerName" HeaderText="Name"
                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="140px"
                                SortExpression="CustomerName" FilterControlWidth="120px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AA_ContactPersonName" UniqueName="AA_ContactPersonName" HeaderText="Agent Associate"
                                AutoPostBackOnFilter="true" SortExpression="AA_ContactPersonName" ShowFilterIcon="false" AllowFiltering="true"
                                HeaderStyle-Width="90px" FilterControlWidth="70px" CurrentFilterFunction="Contains">
                                <ItemStyle Width="90px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="AA_StartDate" ReadOnly="true"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px"
                                HeaderText="From" SortExpression="AA_StartDate" UniqueName="AA_StartDate">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="AA_EndDate"
                                DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px" 
                                HeaderText="Till" SortExpression="AA_EndDate"  UniqueName="AA_EndDate">
                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="100px" Wrap="false" />
                            </telerik:GridDateTimeColumn>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="true" UseStaticHeaders="true" ScrollHeight="380px" />
                        <ClientEvents OnGridCreated="GridCreated" />
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </div>
        </td>
    </tr>
</table>
<asp:Button ID="hiddenassociation" runat="server" OnClick="hiddenassociation_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdnassociationcount" runat="server" />
