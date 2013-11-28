<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IPOIssueList.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.IPOIssueList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div style="float: left; padding-top: 20px; padding-left: 20px;" id="divControlContainer"
            runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <telerik:RadGrid ID="RadGridIPOIssueList" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                            Skin="Telerik" AllowFilteringByColumn="false" OnItemCommand="RadGridIPOIssueList_OnItemCommand">
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId"
                                AutoGenerateColumns="false" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="AIM_IssueName"
                                        SortExpression="AIM_IssueName">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="200px" HeaderText="Face Value"
                                        ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_CapPrice" HeaderStyle-Width="200px" HeaderText="Cap Price"
                                        ShowFilterIcon="false" UniqueName="AIM_CapPrice" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Floor Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FloorPrice" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FixedPrice" HeaderStyle-Width="200px" HeaderText="Fixed Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FixedPrice" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_OpenDate" HeaderStyle-Width="200px" HeaderText="Open Date"
                                        ShowFilterIcon="false" UniqueName="AIM_OpenDate" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_CloseDate" HeaderStyle-Width="200px" HeaderText="Open Date"
                                        ShowFilterIcon="false" UniqueName="AIM_CloseDate" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="200px"
                                        HeaderText="Trading In MultipleOf" ShowFilterIcon="false" UniqueName="AIM_TradingInMultipleOf"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_MinApplicationSize" HeaderStyle-Width="200px"
                                        HeaderText="Min App Size" ShowFilterIcon="false" UniqueName="AIM_MinApplicationSize"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="200px" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="AIM_Rating" SortExpression="AIM_Rating">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Floor Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn ItemStyle-Width="140px" AllowFiltering="false" HeaderText="Action"
                                        ItemStyle-Wrap="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBuy" runat="server" CommandName="Buy" ImageUrl="~/Images/Buy-Button.png"
                                                ToolTip="BUY IPO" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
            </table>
        </div>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
