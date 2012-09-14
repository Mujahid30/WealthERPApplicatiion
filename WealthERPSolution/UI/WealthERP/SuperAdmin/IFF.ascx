﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IFF.ascx.cs" Inherits="WealthERP.SuperAdmin.IFF" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager  Runat="server">
</telerik:RadStyleSheetManager>
<telerik:RadScriptManager  Runat="server">
</telerik:RadScriptManager>

   <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGrid1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="gvAdvisorList" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="clrFilters">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="gvAdvisorList" />
                        <telerik:AjaxUpdatedControl ControlID="clrFilters" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
   </telerik:RadAjaxManager>



<table width="100%" class="TableBackground">
    <tr>
        <td class="HeaderCell">
            <asp:Label ID="lblTitle" runat="server" CssClass="HeaderTextBig" Text="IFA LIST"></asp:Label>
            <hr />
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td align="center">
            <div class="failure-msg" id="ErrorMessage" runat="server" visible="false" align="center">
                No Records found.....
            </div>
        </td>
    </tr>
</table>
<table class="TableBackground" width="100%">
    <%--<tr>
        <td class="HeaderCell">
            <asp:Label ID="Label1" runat="server" Text="RM List" CssClass="HeaderTextBig"></asp:Label>
        </td>
    </tr>--%>
    <%--<tr align="center">
        <td colspan="2" class="leftField" align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>--%>
</table>
<%--<asp:Panel ID="pnlIFFGrid" runat="server" Width="100%" ScrollBars="Vertical,horizontal">--%>
<table class="TableBackground" width="100%" cellpadding="0" cellspacing="0">
<tr align="left">
<td style="padding-left:4px">
    <asp:Button ID="btnExportFilteredData" CssClass="PCGLongButton" OnClick="btnExportFilteredData_OnClick" Text="Export AllPage" runat="server" />
</td>
</tr>
    <tr>
        <td>
           <asp:Panel ID="pnlMFPortfolioHoldings" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
            <div id="dvHoldings" runat="server" style="width: 650px; padding:4px">
            <telerik:RadGrid 
             ID="gvAdvisorList" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true" 
                    AllowAutomaticInserts="false" OnNeedDataSource="gvAdvisorList_OnNeedDataSource">
                    <ExportSettings ExportOnlyData="true" HideStructureColumns="true"> </ExportSettings>
                    <MasterTableView DataKeyNames="UserId" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToExcelButton="true"
                    ShowExportToCsvButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="true"/>
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="false">
                            <ItemStyle />
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                <asp:ListItem>Select </asp:ListItem>
                                   <%--<asp:ListItem Text="View Dashboard" Value="View Dashboard">View Dashboard  </asp:ListItem>--%>
                                    <asp:ListItem Text="Edit profile" Value="Edit profile">View/Edit profile </asp:ListItem>
                                    <asp:ListItem Text="Subscription" Value="Subscription">Subscription</asp:ListItem>

                                </asp:DropDownList>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                         <telerik:GridBoundColumn HeaderText="AdviserId" ItemStyle-HorizontalAlign="Right" DataField="AdviserId" AllowFiltering="true" ShowFilterIcon="false">
                            <ItemStyle />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="IFA" DataField="IFFName" AllowFiltering="true" ShowFilterIcon="false">
                            <ItemStyle />                            
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Category" DataField="Category" AllowFiltering="true" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Area" DataField="IFFAddress" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="City" DataField="IFFCity" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Contact Person" DataField=" IFFContactPerson" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Mobile" DataField="IFFMobileNumber" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Email" DataField="IFFEmailId" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Is Active" DataField="imgIFFIsActive" AllowFiltering="true" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="MF Subs" DataField="imgIFFMutualfund" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="EQ Subs" DataField="imgIFFEquity" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="LI Subs"  DataField="imgIFFInsurance" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Loan Subs"  DataField="imgIFFLiabilities" AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="PMS Subs"  DataField="imgIFFPMS"  AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Fixed Inc. Subs"  DataField="imgIFFFixedIncome"  AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Postal Subs"  DataField="imgIFFPostalSavings"  AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Commodities Subs"  DataField="imgIFFComodities"  AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn HeaderText="Real Est., Subs"  DataField="imgIFFComodities"  AllowFiltering="false" ShowFilterIcon="false">
                            <ItemStyle />
                        </telerik:GridBoundColumn>                        
                        
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            </div>
             </asp:Panel>
            <br />
            <%--<asp:Button ID="clrFilters" runat="server" Text="Clear filters" CssClass="button"
            OnClick="clrFilters_Click"></asp:Button>--%>
        </td>
    </tr>
</table>
<%--</asp:Panel>--%>

<%--<asp:Panel ID="pnlIFFGrid" runat="server" Width="100%" ScrollBars="Horizontal">
    <table class="TableBackground" width="100%">
        <tr align="center">
            <td>
                <asp:GridView ID="gvAdvisorList" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="UserId" OnSorting="gvAdvisorList_Sorting" CssClass="GridViewStyle"
                    ShowFooter="True" OnRowDataBound="gvAdvisorList_RowDataBound">
                    <RowStyle CssClass="RowStyle" />
                    <FooterStyle CssClass="FooterStyle" />
                    <PagerStyle CssClass="PagerStyle" />
                    <SelectedRowStyle CssClass="SelectedRowStyle" />
                    <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Left" />
                    <EditRowStyle CssClass="EditRowStyle" />
                    <AlternatingRowStyle CssClass="AltRowStyle" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlMenu" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged" EnableViewState="True">
                                    <asp:ListItem>Select </asp:ListItem>
                                    <asp:ListItem Text="View Dashboard" Value="View Dashboard">View Dashboard  </asp:ListItem>
                                    <asp:ListItem Text="Edit profile" Value="Edit profile">View/Edit profile </asp:ListItem>
                                    <asp:ListItem Text="Subscription" Value="Subscription">Subscription</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                            <HeaderTemplate>
                                <asp:Label ID="lblIFFName" runat="server" Text="IFF"></asp:Label>
                                 <br />
                                <asp:TextBox ID="txtIFFSearch" runat="server" CssClass="GridViewTxtField" onkeydown="return JSdoPostback(event,'ctrl_IFF_btnIFFSearch');" /> 
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblFFName1" runat="server" Text='<%# Eval("IFFName").ToString() %>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                      
                            
                        <asp:TemplateField ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                            <HeaderTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                                <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                    OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                </asp:DropDownList>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category").ToString() %>'
                                    ItemStyle-HorizontalAlign="Left"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="IFFAddress" HeaderText="Area" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFCity" HeaderText="City " ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFContactPerson" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFMobileNumber" HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="IFFEmailId" HeaderText="Email" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderText="Is Active" DataField="imgIFFIsActive" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="MF Subs" DataField="imgIFFMutualfund" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="EQ Subs" DataField="imgIFFEquity" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="LI Subs" DataField="imgIFFInsurance" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Loan Subs" DataField="imgIFFLiabilities" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="PMS Subs" DataField="imgIFFPMS" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Fixed Inc. Subs" DataField="imgIFFFixedIncome" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Postal Subs" DataField="imgIFFPostalSavings" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Commodities Subs" DataField="imgIFFComodities" ItemStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Real Est., Subs" DataField="imgIFFRealEstate" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>--%>
<%--<div id="DivPager" runat="server">
    <table style="width: 100%">
        <tr id="trPager" runat="server">
            <td align="center">
                <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>--%>
<asp:HiddenField ID="hdnCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnSort" runat="server" Value="RMName ASC" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hidIFA" runat="server" />
<%--<asp:Button ID="btnIFFSearch" runat="server" Text=""
    BorderStyle="None" BackColor="Transparent" onclick="btnIFFSearch_Click" />
    --%>