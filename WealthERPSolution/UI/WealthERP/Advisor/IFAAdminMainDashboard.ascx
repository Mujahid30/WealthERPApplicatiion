<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFAAdminMainDashboard.ascx.cs" 
    Inherits="WealthERP.Advisor.IFAAdminMainDashboard" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register src="../General/Pager.ascx" tagname="Pager" tagprefix="Pager" %>

<table style="width: 100%;" class="TableBackground">
    <%--<tr>
        <td class="HeaderCell" colspan="3">
            <asp:Label ID="Label2" runat="server" CssClass="HeaderTextBig" Text="Advisor Dashboard"></asp:Label>
        </td>
    </tr>--%>
    <tr>
        <td align="center" colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:Label ID="Label1" runat="server" Text="Branch AUM" CssClass="HeaderTextSmall"></asp:Label>
            <hr />
        </td>
    </tr>
      <tr>
        <td class="leftField">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:GridView ID="gvrAdminBranchPerform" runat="server" AllowSorting="True" AutoGenerateColumns="False"
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
            </asp:GridView>
        </td>
        <td align="left">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
            <asp:Label ID="lblGTT" runat="server" CssClass="FieldName" Text="Grand Total (Rs):"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblGT" runat="server" CssClass="Field" Text="Label"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
                        <Pager:Pager ID="mypager" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        <asp:HiddenField ID="hdnRecordCount" runat="server" />
        </td>
    </tr>
</table>
<table style="width: 100%;">
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
        <td align="left">
            <asp:Chart ID="ChartRMPerformance" runat="server" BackColor="Transparent" 
                Width="400px" Height="200px">
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
        <td style="float: right;">
            <asp:Chart ID="ChartBranchPerformance" runat="server" BackColor="#F1EDED"
                Height="300px" Width="450px">
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
<table width="100%">
<tr><td align="center">
<div class="information-msg" id="MessageReceived" runat="server" visible="false" align="center">
<br />
    <asp:Label ID="lblSuperAdmnMessage" runat="server"></asp:Label>
    <br />
</div>
</td></tr>
</table>
