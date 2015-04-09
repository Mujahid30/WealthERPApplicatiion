<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionReconMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.CommissionReconMIS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvCommissionReceiveRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Expected Commission/Payable MIS
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReconComplete" runat="server" class="success-msg" align="center" visible="false">
                Recon Status Marked closed
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgReconClosed" runat="server" class="failure-msg" align="center" visible="false">
                Recon staus Closed,So can't save changes
            </div>
        </td>
    </tr>
</table>
<table width="90%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td class="leftField" width="16%">
            <asp:Label ID="lblSearchType" runat="server" Text=" Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="rightField">
            <asp:DropDownList ID="ddlSearchType" AutoPostBack="true" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlSearchType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%">
            <asp:Label ID="lblSelectProduct" runat="server" Text=" Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%" id="tdCategory" runat="server" visible="false">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right" id="tdDdlCategory" runat="server" visible="false">
            <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="true" CssClass="cmbField" OnSelectedIndexChanged="ddlProductCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlProductCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSelectProduct" runat="server">
       <td align="left" class="leftField" width="20%">
            <asp:Label ID="Label2" runat="server" Text="Order Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="EXECUTED" Value="IP"> </asp:ListItem>
                <asp:ListItem Text="ORDERED" Value="IP"> </asp:ListItem>
                <asp:ListItem Text="ACCEPTED" Value="OR"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddlOrderStatus"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Order Status"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td class="leftField" width="16%">
            <asp:Label ID="lblOffline" runat="server" Text="Channel:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSelectMode" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlSelectMode_OnSelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="Both" Value="2">
                </asp:ListItem>
                <asp:ListItem Text="Online-Only" Value="1"></asp:ListItem>
                <asp:ListItem Text="Offline-Only" Value="0"></asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField" width="16%" runat="server" visible="true">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Commission Type:"></asp:Label>
        </td>
        <td runat="server" visible="true">
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
             <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trNCDIPO" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueType" runat="server" CssClass="FieldName" Text="Issue Type:"></asp:Label>
        </td>
        <td>
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
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trSelectMutualFund" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select Product Type"></asp:CompareValidator>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <%--<tr id="trIssue" runat="server"  visible="false">
    <td>
    <asp:Label ID="lblIssue" runat="server"  Text="Select Issue:" CssClass="FieldName"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="ddlIssueName" runat="server" AutoPostBack="true" CssClass="cmbField"></asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlIssueName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Issue" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
    </td>
    </tr>--%>
    <tr>
        <td id="tdFromDate" class="leftField" runat="server" visible="true">
            <asp:Label ID="lblPeriod" Text="Month/Quarter:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" id="tdFrom" runat="server" visible="true">
            <asp:DropDownList ID="ddlMnthQtr" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlMnthQtr"
                CssClass="rfvPCG" ErrorMessage="<br />Please Select Month" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" id="tdTolbl" runat="server" visible="true">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Year:"></asp:Label>
        </td>
        <td class="rightField" id="tdToDate" runat="server" visible="true">
            <asp:DropDownList ID="ddlYear" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlYear"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Year" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td class="leftField" id="td1" runat="server">
            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Authenticated:"></asp:Label>
        </td>
        <td class="rightField" id="td2" runat="server" >
            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Yes" Value="true" ></asp:ListItem>
                <asp:ListItem Text="No" Value="false" Selected="True"></asp:ListItem>
            </asp:DropDownList>
           
        </td>
        <td class="rightField" style="padding-right: 50px">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
        </td>
    </tr>
</table>
<div style="width: 100%; overflow: scroll;" runat="server" visible="false" id="dvMfMIS">
    <telerik:RadGrid ID="gvCommissionReceiveRecon" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="200%"
        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
        EnableHeaderContextMenu="true" OnItemDataBound="gvWERPTrans_ItemDataBound" EnableHeaderContextFilterMenu="true"
        OnNeedDataSource="gvCommissionReceiveRecon_OnNeedDataSource">
        <MasterTableView Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true" DataKeyNames="CMFT_MFTransId,totalNAV,perDayTrail,PerDayAssets">
            <Columns>
            <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action" HeaderStyle-Width="10%" Visible="false">
                    <HeaderTemplate>
                        <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkId" runat="server" />
                        <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFT_MFTransId").ToString()%>' />
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="btnSave" CssClass="FieldName"  runat="server"
                            Text="Update Expected Amt"  OnClick="btnSave_Click"/>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="RuleName" DataField="ACSR_CommissionStructureRuleName"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_CommissionStructureRuleName" SortExpression="ACSR_CommissionStructureRuleName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="100px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                  <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Customer Name" DataField="CustomerName"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CustomerName" SortExpression="CustomerName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Order No" DataField="CO_OrderId"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CO_OrderId" SortExpression="CO_OrderId"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>  
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Folio No" DataField="CMFA_FolioNum"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Transaction Date" DataField="transactiondate"
                    UniqueName="transactiondate" SortExpression="transactiondate" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Status" DataField="WOS_OrderStep"
                    UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Sub Broker Code" DataField="CMFT_SubBrokerCode"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="CMFT_SubBrokerCode" SortExpression="CMFT_SubBrokerCode"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Associates" DataField="AA_ContactPersonName"
                    UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Parents" DataField="ParentName" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="Parents" SortExpression="ParentName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Commission Type" DataField="commissionType" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="commissionType" SortExpression="commissionType" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="CalculatedOn" DataField="WCCO_CalculatedOn" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="WCCO_CalculatedOn" SortExpression="WCCO_CalculatedOn" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <%--<telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Commission SubType" DataField="IncentiveType" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="Commission SubType" SortExpression="IncentiveType" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <%-- <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Issue" DataField="AIM_IssueName"
                    UniqueName="AIM_IssueName" SortExpression="AIM_IssueName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
              <%-- <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="transactiondate" DataField="transactiondate"
                     HeaderStyle-HorizontalAlign="Center" UniqueName="transactiondate" SortExpression="transactiondate" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <%--<telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Holdings Amount" DataField="holdings"
                     HeaderStyle-HorizontalAlign="Center" UniqueName="Holdings Amount" SortExpression="holdings" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
              <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Brokerage Name" DataField="XB_BrokerShortName"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="XB_BrokerShortName" SortExpression="XB_BrokerShortName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
            <%-- <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Category" DataField="AIIC_InvestorCatgeoryName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AIIC_InvestorCatgeoryName" SortExpression="AIIC_InvestorCatgeoryName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                 <%--<telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Series" DataField="AID_IssueDetailName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Brokerage Rate" DataField="ACSR_BrokerageValue"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_BrokerageValue" SortExpression="ACSR_BrokerageValue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Brokerage Rate unit" DataField="WCU_UnitCode"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="WCU_UnitCode" SortExpression="WCU_UnitCode"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Service Tax(%)" DataField="ACSR_ServiceTaxValue"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_ServiceTaxValue" SortExpression="ACSR_ServiceTaxValue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="TDS(%)" DataField="ACSR_ReducedValue"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_ReducedValue" SortExpression="ACSR_ReducedValue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Expected Commission" DataField="expectedamount"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="expectedamount" SortExpression="expectedamount"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Net Commission" DataField="borkageExpectedvalue" SortExpression="borkageExpectedvalue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="SchemePlan" DataField="schemeplanname" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="schemeplanname" SortExpression="schemeplanname" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="113px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn> 
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Amount" DataField="amount"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="amount" SortExpression="amount"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Transaction Type" DataField="transactiontype"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="transactiontype" SortExpression="transactiontype"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Age" DataField="Age"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="Age" SortExpression="Age" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Current Value" DataField="currentvalue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="currentvalue" SortExpression="currentvalue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Commission Amount Received"
                    DataField="receivedamount" HeaderStyle-HorizontalAlign="Center" UniqueName="receivedamount"
                    SortExpression="receivedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                    Visible="false">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Cumulative Nav" DataField="totalNAV"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="totalNAV" SortExpression="totalNAV"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="PerDayAssets" DataField="PerDayAssets"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="PerDayAssets" SortExpression="PerDayAssets"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="PerDayTrail" DataField="perDayTrail"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="perDayTrail" SortExpression="perDayTrail"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Commission Amount Expected"
                    DataField="expectedamount" HeaderStyle-HorizontalAlign="Center" UniqueName="expectedamount"
                    SortExpression="expectedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderStyle-Width="90px" HeaderText="Commission Adjustment Amount"
                    DataField="CMFT_ReceivedCommissionAdjustment" SortExpression="CMFT_ReceivedCommissionAdjustment" HeaderStyle-HorizontalAlign="Center"
                    AutoPostBackOnFilter="true" ShowFilterIcon="false" Visible="false" UniqueName="CMFT_ReceivedCommissionAdjustment">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAdjustAmount" CssClass="txtField" runat="server" Text='<%# Eval("adjust").ToString()%>'></asp:TextBox>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAdjustAmountMultiple" CssClass="txtField" runat="server" />
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Amount Difference"
                    DataField="diff" HeaderStyle-HorizontalAlign="Center" UniqueName="diff" SortExpression="diff"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Commission Updated Expected Amount"
                    DataField="UpdatedExpectedAmount" HeaderStyle-HorizontalAlign="Center" UniqueName="UpdatedExpectedAmount"
                    SortExpression="UpdatedExpectedAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top"/>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Recon Status" DataField="ReconStatus"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ReconStatus" SortExpression="ReconStatus"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Rate" DataField="ACSR_BrokerageValue"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_BrokerageValue" SortExpression="ACSR_BrokerageValue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="90px" HeaderText="Units" DataField="units"
                    HeaderStyle-HorizontalAlign="Center" UniqueName="units" SortExpression="units"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<div id="dvNCDIPOMIS" style="width: 100%; overflow: scroll;" runat="server" visible="false">
    <telerik:RadGrid ID="rgNCDIPOMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="100%"
        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
        EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnNeedDataSource="rgNCDIPOMIS_OnNeedDataSource">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <MasterTableView Width="150%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
            GroupLoadMode="Client" ShowGroupFooter="true">
            <Columns>
            <telerik:GridBoundColumn HeaderStyle-Width="20%" HeaderText="Rule Name"
                    DataField="ACSR_CommissionStructureRuleName" HeaderStyle-HorizontalAlign="Left" UniqueName="ACSR_CommissionStructureRuleName"
                    SortExpression="ACSR_CommissionStructureRuleName" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Left">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top"/>
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="ApplicationNo." DataField="CO_ApplicationNumber"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="Application No" SortExpression="CO_ApplicationNumber"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="15%" HeaderText="Customer Name" DataField="CustomerName"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="CustomerName" SortExpression="CustomerName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Left">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Order No." DataField="CO_OrderId"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CO_OrderId" SortExpression="CO_OrderId"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Transaction Date." DataField="transactionDate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="transactionDate" SortExpression="transactionDate"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Status" DataField="WOS_OrderStep"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Sub Broker Code" DataField="AAC_AgentCode"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Associates" DataField="AA_ContactPersonName"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Parent" DataField="ParentName"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="ParentName" SortExpression="ParentName"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Commission Type" DataField="commissionType"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="commissionType" SortExpression="commissionType"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="CalculatedOn" DataField="calculatedOn"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="calculatedOn" SortExpression="calculatedOn"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
               <%-- <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Commission SubType" DataField="IncentiveType"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="IncentiveType" SortExpression="IncentiveType"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="User Type" DataField="CustomerAssociate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerAssociate" SortExpression="CustomerAssociate"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="20%" HeaderText="Issue" DataField="AIM_IssueName"
                    UniqueName="AIM_IssueName" SortExpression="AIM_IssueName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Ordered Qty\Accepted Quantity" DataField="allotedQty"
                    UniqueName="allotedQty" SortExpression="allotedQty" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AllowFiltering="false">
                    <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <%--<telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Holdings Amount" DataField="holdings"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="holdings" SortExpression="holdings"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Brokerage Name" DataField="XB_BrokerShortName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="XB_BrokerShortName" SortExpression="XB_BrokerShortName" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Category" DataField="AIIC_InvestorCatgeoryName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AIIC_InvestorCatgeoryName" SortExpression="AIIC_InvestorCatgeoryName" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Series" DataField="AID_IssueDetailName"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Mobilised Amount" DataField="ParentMobilize_Orders" 
                AllowFiltering="false" ShowFilterIcon="false"   CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Mobilised No Of Application" DataField="ParentMobilize_Amount" 
                AllowFiltering="false" ShowFilterIcon="false"   CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Brokerage Rate" DataField="rate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="rate" SortExpression="rate" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText=" Brokerage Rate unit" DataField="WCU_UnitCode"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="WCU_UnitCode" SortExpression="WCU_UnitCode" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Service Tax(%)" DataField="ACSR_ServiceTaxValue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ACSR_ServiceTaxValue" SortExpression="ACSR_ServiceTaxValue"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="TDS(%)" DataField="ACSR_ReducedValue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="ACSR_ReducedValue" SortExpression="ACSR_ReducedValue" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Expected Commission" DataField="brokeragevalue"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="brokeragevalue" SortExpression="brokeragevalue" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right"   Aggregate="Sum">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                    
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Net Commission"
                    DataField="borkageExpectedvalue" HeaderStyle-HorizontalAlign="Right" UniqueName="borkageExpectedvalue"
                    SortExpression="borkageExpectedvalue" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"  Aggregate="Sum">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 
               
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
    
</div>
<asp:HiddenField ID="hdnschemeId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnSBbrokercode" runat="server" />
<asp:HiddenField ID="hdnIssueId" runat="server" />
<asp:HiddenField ID="hdnProductCategory" runat="server" />
<div runat="server" id="divBtnActionSection" visible="false">
    <asp:Button ID="btnUpload"  runat="server" Text="Mark Recon Status"
        CssClass="PCGLongButton"  OnClick="btnUpload_OnClick"/>
   
</div>

