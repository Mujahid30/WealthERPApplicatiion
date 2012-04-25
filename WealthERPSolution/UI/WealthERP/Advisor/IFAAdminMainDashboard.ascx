<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFAAdminMainDashboard.ascx.cs"
    Inherits="WealthERP.Advisor.IFAAdminMainDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Src="../General/Pager.ascx" TagName="Pager" TagPrefix="Pager" %>
<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script type="text/javascript">
    $(document).ready(function() {
    $(".panel").show();

        $(".flip").click(function() { $(".panel").slideToggle(); });
     });
</script>

<table style="width: 100%;" padding-top="1px" class="TableBackground" cellspacing="0"
    cellpadding="0">
    <tr>
        <td align="left">
            <asp:Label ID="Label1" runat="server" Text="Branch AUM" CssClass="HeaderTextSmall">
            </asp:Label>
        </td>
        <td>
            <asp:Label ID="lblNewMessages" runat="server" CssClass="HeaderTextSmall" Visible="false">
            </asp:Label>
        </td>
        <td align="right">
            <asp:ImageButton ID="imgRefresh" runat="server" ImageUrl="../Images/refresh-Dashboard.png"
                OnClick="imgRefresh_Click" ToolTip="If you finding the data is not in sync,click here to get updated data" />
        </td>
    </tr>
    <tr style="height: 2px">
        <td colspan="3" style="height: 2px">
            <hr style="height: 1px; line-height: 1px;" />
        </td>
    </tr>
</table>
<table style="width: 100%;" padding-top="0" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell" colspan="3">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Advisor Dashboard"></asp:Label>
        </td>
    </tr>--%>
    <%--  <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td align="left" colspan="2" style="padding-left: 2px; padding-right: 2px">
            <%--<asp:GridView ID="gvrAdminBranchPerform" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                CellPadding="4" DataKeyNames="Branch Id" EnableViewState="false" AllowPaging="True" ShowFooter="true"
                CssClass="GridViewStyle" OnRowDataBound="gvrAdminBranchPerform_RowDataBound">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <EditRowStyle CssClass="EditRowStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:BoundField DataField="Branch Name" HeaderText="Branch Name"  />
                    <asp:BoundField DataField="Branch Code" HeaderText="Branch Code"  />
                    <asp:BoundField DataField="Equity" HeaderText="Equity (Rs)" 
                        DataFormatString="{0:b}" HtmlEncode="false" ApplyFormatInEditMode="True" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="MF" HeaderText="MF (Rs)"  HtmlEncode="false"
                        DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Insurance" HeaderText="Insurance (Rs)"
                        DataFormatString="{0:c}" HtmlEncode="false" ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfCustomers" HeaderText="No. of Customers" HtmlEncode="false" ItemStyle-HorizontalAlign="Right" />
                </Columns>
            </asp:GridView>--%>
            <telerik:RadGrid ID="gvrAdminBranchPerform" runat="server" AllowAutomaticInserts="false"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EnableEmbeddedSkins="false"
                GridLines="None" PageSize="10" ShowFooter="true" ShowStatusBar="True" Skin="Telerik"
                Width="100%" OnNeedDataSource="gvrAdminBranchPerform_NeedDataSource">
                <%--<PagerStyle Mode="NumericPages"></PagerStyle>--%>
                <MasterTableView AllowMultiColumnSorting="true" AutoGenerateColumns="false" CommandItemDisplay="None"
                    Width="100%">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Branch Name" HeaderText="Branch Name" UniqueName="Branch Name"
                            FooterText="Total:" FooterStyle-HorizontalAlign="Left">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Branch Code" HeaderText="Branch Code" UniqueName="Branch Code">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="Equity" DataType="System.Decimal"
                            DataFormatString="{0:n2}" HeaderText="Equity (Rs)" UniqueName="Equity" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="MF" DataType="System.Decimal"
                            DataFormatString="{0:n2}" HeaderText="MF (Rs)" UniqueName="MF" FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="Insurance" DataType="System.Decimal"
                            DataFormatString="{0:n2}" HeaderText="Insurance (Rs)" UniqueName="Insurance"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn Aggregate="Sum" DataField="NoOfCustomers" DataType="System.Int32"
                            DataFormatString="{0:0}" HeaderText="No. of Customers" UniqueName="NoOfCustomers"
                            FooterStyle-HorizontalAlign="Right">
                            <ItemStyle HorizontalAlign="right" VerticalAlign="Top" Width="" Wrap="false" />
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <%--<Scrolling AllowScroll="false" UseStaticHeaders="True" SaveScrollPosition="true">
                        </Scrolling>--%>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    <%-- <Resizing AllowColumnResize="True"></Resizing>--%>
                </ClientSettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label ID="lblNote" Text="Note:If you are finding the data is not in sync please click on Refresh"
                runat="server" CssClass="FieldName">
            </asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblGTT" runat="server" CssClass="FieldName" Text="Grand Total (Rs):"></asp:Label>
            <asp:Label ID="lblGT" runat="server" CssClass="Field" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
    <td colspan="2"></td>
    </tr>
    <tr>
        <td colspan="2">
  <table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <img src="../Images/helpImage.png" height="25px" width="25px" style="float: right;"
                class="flip" />
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextSmall" Text="Super Admin Message"></asp:Label>
            <hr />
        </td>
    </tr>
</table>      
        
 <table width="100%">
    <tr>
        <td colspan="3">
              <div class="panel">
                <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
            </div>
        </td>
    </tr>
</table>
        
<%--<table width="100%">
    <tr>
        <td align="center">
            <div class="information-msg" id="MessageReceived" runat="server" visible="false"
                align="center">
               <br />
                <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
                <br />
            </div>
        </td>
    </tr>
</table>--%>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
            <asp:HiddenField ID="hdnRecordCount" runat="server" />
        </td>
    </tr>
</table>
<table style="width: 100%;" cellpadding="0" cellspacing="0">
    <tr>
        <td align="left">
            <asp:Label ID="lblRMPerformChart" runat="server" Text="RM AUM" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
        <td>
            <asp:Label ID="lblBranchPerformChart" runat="server" Text="Branch AUM" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td align="left" style="padding-left: 2px; padding-right: 2px">
            <asp:Chart ID="ChartRMPerformance" runat="server" BackColor="Transparent" Width="400px"
                Height="200px">
                <Series>
                    <asp:Series Name="Series1">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea BackColor="#EBEFF9" Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </td>
        <td style="float: right;" style="padding-left: 2px; padding-right: 2px">
            <asp:Chart ID="ChartBranchPerformance" runat="server" BackColor="#F1EDED" Height="300px"
                Width="450px">
                <Series>
                    <asp:Series Name="Series1" XValueMember="Branch Code" YValueMembers="Aggr">
                    </asp:Series>
                </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
            </asp:Chart>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>

