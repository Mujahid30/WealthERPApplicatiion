<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvisorDashBoard.ascx.cs"
    Inherits="WealthERP.Advisor.AdvisorDashBoard" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="Advisor DashBoard"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

<div id="divAdvisorDashBoard" runat="server" align="left" style="vertical-align: top;
    float: left;">
    <table class="TableBackground">
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td>
                <cc1:TabContainer ID="TabContainer1" runat="server" CssClass="google" ActiveTabIndex="1"
                    Width="590px">
                    <cc1:TabPanel runat="server" HeaderText="LOB List" ID="TabPanel1">
                        <ContentTemplate>
                            <div id="div1" runat="server" style="width: auto; height: auto" class="grid">
                                <asp:Label ID="lblLOBList" runat="server" CssClass="FieldName" Text="LOB List is Empty.."></asp:Label>
                                <asp:GridView ID="gvAdvisorLOB" runat="server" CellPadding="4" ForeColor="#333333"
                                    Height="77px" Width="590px" AllowPaging="True" PageSize="2" OnPageIndexChanging="gvAdvisorLOB_PageIndexChanging"
                                    BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" CssClass="GridViewStyle">
                                    <RowStyle CssClass="RowStyle" />
                                    <AlternatingRowStyle BackColor="White" CssClass="AltRowStyle" />
                                    <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                                    <FooterStyle ForeColor="White" Font-Bold="True" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="HeaderStyle" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" Font-Bold="True" CssClass="PagerStyle" />
                                    <SelectedRowStyle ForeColor="#333333" CssClass="SelectedRowStyle" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Branch List">
                        <HeaderTemplate>
                            Branch List
                        </HeaderTemplate>
                        <ContentTemplate>
                            <asp:Label ID="lblBranchList" runat="server" CssClass="FieldName" Text="Branch List is Empty.."></asp:Label>
                            <div id="divBranch" runat="server" style="width: 590px; height: auto">
                                <asp:GridView ID="gvAdvisorBranch" runat="server" CellPadding="4" ForeColor="#333333"
                                    Height="77px" Width="590px" AllowPaging="True" PageSize="2" OnPageIndexChanging="gvAdvisorBranch_PageIndexChanging"
                                    BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" CssClass="GridViewStyle">
                                    <RowStyle CssClass="RowStyle" />
                                    <AlternatingRowStyle BackColor="White" CssClass="AltRowStyle" />
                                    <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                                    <FooterStyle Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="HeaderStyle" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                                    <SelectedRowStyle Font-Bold="True" ForeColor="#333333" CssClass="SelectedRowStyle" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="RM List">
                        <ContentTemplate>
                            <asp:Label ID="lblRMList" runat="server" CssClass="FieldName" Text="RM List is Empty.."></asp:Label>
                            <div id="divRM" runat="server" style="width: auto; height: auto">
                                <asp:GridView ID="gvAdvisorRm" runat="server" CellPadding="4" ForeColor="#333333"
                                    Height="112px" Width="590px" BorderColor="Black" AllowPaging="True" PageSize="2"
                                    OnPageIndexChanging="gvAdvisorRm_PageIndexChanging" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg"
                                    CssClass="GridViewStyle">
                                    <RowStyle CssClass="RowStyle" />
                                    <AlternatingRowStyle BackColor="White" BorderColor="Black" BorderStyle="None" CssClass="AltRowStyle" />
                                    <EditRowStyle Font-Size="Small" CssClass="EditRowStyle" />
                                    <FooterStyle Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle Font-Bold="True" ForeColor="White" CssClass="HeaderStyle" />
                                    <PagerStyle ForeColor="White" HorizontalAlign="Center" CssClass="PagerStyle" />
                                    <SelectedRowStyle Font-Bold="True" ForeColor="#333333" CssClass="SelectedRowStyle" />
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </td>
        </tr>
    </table>
</div>
