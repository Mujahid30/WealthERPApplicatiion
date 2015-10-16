<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerFPGoalPlanningDetails.ascx.cs"
    Inherits="WealthERP.FP.CustomerFPGoalPlanningDetails" %>
<%@ Register TagPrefix="qsf" Namespace="Telerik" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this profile?');

        if (bool) {
            document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hdnMsgValue").value = 1;
            document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hiddenassociation").click();
            return false;
        }
        else {
            document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hdnMsgValue").value = 0;
            document.getElementById("ctrl_CustomerFPGoalPlanningDetails_hiddenassociation").click();
            return true;

        }
    }
</script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Goal List
                        </td>
                        <td align="right" >
                            <asp:ImageButton ID="imgBtnStandardGoalList" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                OnClientClick="setFormat('excel')" Height="25px" Width="25px" Visible="false">
                            </asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr id="trDeleteSuccess" runat="server" visible="false">
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="success-msg" align="center">
                Goal has been deleted Successfully.
            </div>
        </td>
    </tr>
</table>
<table width="50%">
    <tr>
        <td>
        </td>
        <td style="width: 50%">
            <asp:Label ID="lblGoalType" runat="server" CssClass="FieldName" Text="Goal Details Type:"></asp:Label>
            <telerik:RadComboBox ID="ddlActionGoalType" CssClass="cmbField" runat="server" EnableEmbeddedSkins="false"
                Skin="Telerik" AllowCustomText="true" Width="120px">
                <Items>
                    <telerik:RadComboBoxItem Text="Standard View" Value="Standard" Selected="true" runat="server">
                    </telerik:RadComboBoxItem>
                    <telerik:RadComboBoxItem Text="Funding View" Value="Advanced" runat="server"></telerik:RadComboBoxItem>
                </Items>
            </telerik:RadComboBox>
        </td>
        <td style="width: 50%">
            <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnGo_OnClick" />
        </td>
    </tr>
</table>
<table width="100%">
    <tr id="trNoRecordFound" runat="server" visible="false">
        <td align="center">
            <div id="Div1" runat="server" class="failure-msg" align="center">
                No Records Found...
            </div>
        </td>
    </tr>
</table>
<table width="100%" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
                HorizontalAlign="Left">
                <div id="dvHoldings" runat="server" style="width: 650px; padding: 4px">
                    <asp:GridView ID="gvGoalList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        HorizontalAlign="Center" CellPadding="4" EnableViewState="True" AllowPaging="false"
                        ShowFooter="true" CssClass="GridViewStyle" DataKeyNames="GoalId,GoalCode,IsGoalBehind" 
                        OnRowDataBound="gvGoalList_RowDataBound">
                        <FooterStyle CssClass="FooterStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <%--  <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle"/>--%>
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" Wrap="false" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Action" ItemStyle-Width="80Px">
                                <ItemTemplate>
                                    <%-- <asp:DropDownList ID="ddlAction" CssClass="cmbField" AutoPostBack="true" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>View</asp:ListItem>
                            <asp:ListItem>Edit</asp:ListItem>
                            <asp:ListItem>Fund</asp:ListItem>
                            <asp:ListItem>Delete</asp:ListItem>
                            </asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="ddlAction" OnSelectedIndexChanged="ddlAction_OnSelectedIndexChange"
                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                        AllowCustomText="true" Width="120px" AutoPostBack="true">
                                        <Items>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                            </telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/DetailedView.png" Text="View" Value="View"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/GoalFund.png" Text="Fund" Value="Fund"
                                                runat="server"></telerik:RadComboBoxItem>
                                            <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                                runat="server"></telerik:RadComboBoxItem>
                                        </Items>
                                    </telerik:RadComboBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goal" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="60Px"
                                ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="lnkGoalType" runat="server" CssClass="cmbField" 
                                            OnClick="lnkGoalType_Click" Text='<%# Eval("GoalName") %>'>
                                        </asp:LinkButton>--%>
                                    <asp:Label ID="lblGoalName" runat="server" CssClass="cmbField" Text='<%# Eval("GoalCode") %>'>
                                    </asp:Label>
                                    <asp:Image ID="imgGoalImage" ImageAlign="Middle" runat="server" />
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblTotalText" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="Total:">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Yr" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblGoalYear" runat="server"  Text='<%#Eval("GoalYear") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goal Amount" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblGaolAmount" runat="server"  Text='<%#Eval("GoalAmount")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblGoalAmountTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goal Start Dt" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblGoalDate" runat="server"  Text='<%#Eval("GoalPrifileDate", "{0:M-dd-yyyy}")  %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Goal Cost at Start" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCostToday" runat="server" Text='<%#Eval("CostToday")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCostTodayTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Infl.(%)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right"
                                FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblInflation" runat="server"  Text='<%#Eval("Inflation")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return on  Existing Investment(%)" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblRtnOnExistingInvestment" runat="server"  Text='<%#Eval("ROIEarned")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Return on Future Investment(%)" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblExpROI" runat="server"  Text='<%#Eval("ExpROI")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Investment Required-Lumpsum" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblLumpsumInvestment" runat="server"  Text='<%#Eval("LumpsumInvestment")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblLumpsumInvestmentTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Savings Required-Monthly" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonthlyReq" runat="server" Text='<%#Eval("SavingRequired")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblMonthlySavingReqTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt Funded(Cost)" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblAllocatedAmountToWardsGoal" runat="server" 
                                        Text='<%#Eval("AllocatedAmountToWardsGoal")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblAllocAmountToWardsGoalTotal" runat="server" CssClass="HeaderStyle"
                                        Font-Bold="true" Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt Funded(Cur Val)" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblCurrentGoalValue" runat="server"  Text='<%#Eval("CurrentGoalValue")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblCurrentGoalValueTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Monthly Investment-Commited" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblValueOfInvestment" runat="server"  Text='<%#Eval("SIPAmount")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblSIPAmountTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Projected Funds Value On Goal Year" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectedValue" runat="server"  Text='<%#Eval("ProjectedValue")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblProjectedValueTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Projected Gap" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectedGapValue" runat="server"  Text='<%#Eval("ProjectedGapValue")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblProjectedGapValueTotal" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                        Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Additional Savings Required-Monthly" HeaderStyle-HorizontalAlign="Center"
                                ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblAdditionalSavingReq" runat="server"  Text='<%#Eval("AdditionalSavingReq")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lblAdditionalSavingReqTotal" runat="server" CssClass="HeaderStyle"
                                        Font-Bold="true" Text="">
                                    </asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IsActive" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsActive" runat="server"  Text='<%#Eval("IsActive") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:DropDownList ID="ddlActiveFilter" runat="server" AutoPostBack="true" CssClass="cmbField"
                                        Visible="false">
                                        <asp:ListItem Text="Active" Value="1">
                                        </asp:ListItem>
                                        <asp:ListItem Text="InActive" Value="0">
                                        </asp:ListItem>
                                        <asp:ListItem Text="All" Value="2">
                                        </asp:ListItem>
                                    </asp:DropDownList>
                                    <br></br>
                                    <asp:Label ID="ActiveMessage" runat="server" BackColor="Transparent" 
                                        Font-Bold="true" Text="No Active Goals">
                                    </asp:Label>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Progress Indicator" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIsGoalGap" runat="server"  Text='<%#Eval("IsGoalBehind") %>'>
                                    </asp:Label>
                                    <asp:Image ID="imgGoalFundGap" ImageAlign="Middle" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="EditRowStyle" HorizontalAlign="Left" VerticalAlign="Top" />
                        <HeaderStyle CssClass="HeaderStyle" Wrap="true" />
                        <%--<PagerStyle CssClass="PagerStyle" HorizontalAlign="Center" />--%>
                        <RowStyle CssClass="RowStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" Height="100%" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            <asp:Panel ID="Panel2" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
                HorizontalAlign="Left">
                <div id="Div3" runat="server">
                    <telerik:RadGrid ID="gvStandardGoaldetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                        AlternatingItemStyle-Wrap="true" PageSize="10" AllowSorting="true" AllowPaging="True"
                        ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false" OnNeedDataSource="gvStandardGoaldetails_OnNeedDataSource"
                        AllowFilteringByColumn="true" AllowAutomaticInserts="false" OnItemDataBound="gvStandardGoaldetails_ItemDataBound">
                        <ExportSettings HideStructureColumns="false">
                        </ExportSettings>
                        <MasterTableView Width="100%" DataKeyNames="GoalId,GoalCode,IsGoalBehind" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action">
                                    <ItemTemplate>
                                        <telerik:RadComboBox ID="ddlActionSteps" OnSelectedIndexChanged="ddlActionSteps_OnSelectedIndexChange"
                                            CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                            AllowCustomText="true" Width="120px" AutoPostBack="true">
                                            <Items>
                                                <telerik:RadComboBoxItem ImageUrl="~/Images/Select.png" Text="Select" Value="0" Selected="true">
                                                </telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem ImageUrl="~/Images/DetailedView.png" Text="View" Value="View"
                                                    runat="server"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem ImageUrl="~/Images/RecordEdit.png" Text="Edit" Value="Edit"
                                                    runat="server"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem ImageUrl="~/Images/GoalFund.png" Text="Fund" Value="Fund"
                                                    runat="server"></telerik:RadComboBoxItem>
                                                <telerik:RadComboBoxItem ImageUrl="~/Images/DeleteRecord.png" Text="Delete" Value="Delete"
                                                    runat="server"></telerik:RadComboBoxItem>
                                            </Items>
                                        </telerik:RadComboBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Goal" DataField="GoalCode" AllowFiltering="false"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="60Px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%--<asp:LinkButton ID="lnkGoalType" runat="server" CssClass="cmbField" 
                                            OnClick="lnkGoalType_Click" Text='<%# Eval("GoalName") %>'>
                                        </asp:LinkButton>--%>
                                        <asp:Label ID="lblGoalName" runat="server"  Text='<%# Eval("GoalCode") %>'>
                                        </asp:Label>
                                        <asp:Image ID="imgGoalImage" ImageAlign="Middle" runat="server" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblTotalText" runat="server" CssClass="HeaderStyle" Font-Bold="true"
                                            Text="Total:">
                                        </asp:Label>
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Target Yr" DataField="GoalYear" AllowFiltering="false"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalYear" runat="server"  Text='<%#Eval("GoalYear") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Goal Amount" DataField="GoalAmount" Aggregate="Sum"
                                    FooterAggregateFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center" AllowFiltering="false"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGaolAmount" runat="server"  Text='<%#Eval("GoalAmount")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Goal Start Dt" DataField="GoalPrifileDate"
                                    AllowFiltering="false" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblGoalDate" runat="server"  Text='<%#Eval("GoalPrifileDate", "{0:M-dd-yyyy}")  %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Goal Cost at Start"
                                    DataField="CostToday" Aggregate="Sum" FooterAggregateFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCostToday" runat="server"  Text='<%#Eval("CostToday")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Infl.(%)" DataField="Inflation" AllowFiltering="false"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInflation" runat="server"  Text='<%#Eval("Inflation")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Rtn on Existing Investment(%)" DataField="ROIEarned"
                                    HeaderStyle-Width="10%" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRtnOnExistingInvestment" runat="server"  Text='<%#Eval("ROIEarned")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Rtn on Future Investment(%)" DataField="ExpROI"
                                    AllowFiltering="false" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpROI" runat="server"  Text='<%#Eval("ExpROI")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Investment Required-Lumpsum" DataField="LumpsumInvestment"
                                    Aggregate="Sum" FooterAggregateFormatString="{0:N0}" AllowFiltering="false" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLumpsumInvestment" runat="server"  Text='<%#Eval("LumpsumInvestment")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Savings Required-Monthly" DataField="SavingRequired"
                                    AllowFiltering="false" Aggregate="Sum" FooterAggregateFormatString="{0:N0}" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonthlyReq" runat="server"  Text='<%#Eval("SavingRequired")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="IsActive" HeaderStyle-HorizontalAlign="Center"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsActive" runat="server"  Text='<%#Eval("IsActive") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </asp:Panel>
        </td>
    </tr>
</table>
<table>
    <tr id="trNote1" runat="server">
        <td>
            <asp:Label ID="lblNoteHeading" runat="server"  Text="Note :"></asp:Label>
        </td>
    </tr>
    <tr id="trNote2" runat="server">
        <td>
            <asp:Label ID="trRequiedNote"  runat="server" Text="1)For retirement 'Cost At Start' is the required annual cost at today value."></asp:Label>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hidRTSaveReq" runat="server" />
<asp:HiddenField ID="hdnMsgValue" runat="server" />
<asp:HiddenField ID="hdndeleteId" runat="server" />
<asp:HiddenField ID="hdnGoalType" runat="server" />
<asp:Button ID="hiddenassociation" runat="server" BorderStyle="None" OnClick="hiddenassociation_Click"
    BackColor="Transparent" />