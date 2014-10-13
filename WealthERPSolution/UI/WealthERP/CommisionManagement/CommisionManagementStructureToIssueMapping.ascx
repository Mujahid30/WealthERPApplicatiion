<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommisionManagementStructureToIssueMapping.ascx.cs"
    Inherits="WealthERP.CommisionManagement.CommisionManagementStructureToIssueMapping" %>
    
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<table runat="server" id="tbNcdIssueList">
    <tr>
        <td align="right">
            <asp:Label ID="Label2" runat="server" Text="Issue type:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="205px" OnSelectedIndexChanged="ddlIssueType_Selectedindexchanged">
                <asp:ListItem Value="OpenIssue">Open Issue</asp:ListItem>
                <asp:ListItem Value="FutureIssue">Future Issue</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Issue Type"
                CssClass="rfvPCG" ControlToValidate="ddlIssueType" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td align="right">
           &nbsp;&nbsp; <asp:Label ID="Label1" runat="server" Text="Unmmaped Issues:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlUnMappedIssues" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="205px">
            </asp:DropDownList>
            <span id="Span1" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Issue Type"
                CssClass="rfvPCG" ControlToValidate="ddlUnMappedIssues" ValidationGroup="btnGo"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trBtnSubmit" runat="server">
        <td class="leftLabel">
            <asp:Button ID="btnMAP" runat="server" Text="Map" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnMAP_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlIssueList" runat="server" class="Landscape" Width="80%" Height="80%"
    ScrollBars="Both"  >
    <table width="100%">
        <tr>
            <td>
                <div id="dvIssueList" runat="server" style="width: auto;">
                    <telerik:RadGrid ID="gvMappedIssueList" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" AllowFilteringByColumn="true"
                        AllowAutomaticInserts="false" ExportSettings-FileName="Issue List" OnNeedDataSource="gvMappedIssueList_OnNeedDataSource">
                        <%-- <ExportSettings HideStructureColumns="true" ExportOnlyData="true" IgnorePaging="true"
                            FileName="MF Order Recon" Excel-Format="ExcelML">
                        </ExportSettings>--%>
                        <MasterTableView   Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="None">
                            <Columns>
                                <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValidityFrom" HeaderText="Validity From" SortExpression="ValidityFrom"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    DataFormatString="{0:d}" UniqueName="ValidityFrom" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ValidityTo" HeaderText="Validity To" SortExpression="ValidityTo"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    DataFormatString="{0:d}" UniqueName="ValidityTo" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </td>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
    </table>
</asp:Panel>
