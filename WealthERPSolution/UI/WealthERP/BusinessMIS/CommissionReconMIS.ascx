<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionReconMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.CommissionReconMIS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

 <table width="100%">
            <tr>
                <td>
                    <div class="divPageHeading">
                        <table cellspacing="0" cellpadding="3" width="100%">
                            <tr>
                                <td align="left">
                                    Commission Recon MIS
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>

<table width="70%">
    <tr id="trSelectMutualFund" runat="server">
        <td align="right">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select AMC Code"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trNavCategory" runat="server">
        <td align="left" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <%--<tr id="trNavSubCategory" runat="server">
                <td align="right">
                    <asp:Label ID="lblNAVSubCategory" runat="server" CssClass="FieldName" 
                        Text="Sub Category:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNAVSubCategory" runat="server" AutoPostBack="true" 
                        CssClass="cmbField" 
                        OnSelectedIndexChanged="ddlNAVSubCategory_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>--%>
    <tr id="trSelectSchemeNAV" runat="server">
        <td align="right">
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
    <%-- <tr runat="server">
                         <td align="right">
                         <asp:Button ID="Button1" runat="server" Text="GO" runat="server" OnClick="GdBind_Click"/>
</td>
                        </tr>--%>
</table>
<%-- </div>--%>
<%--<div>--%>
<table width="80%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td id="tdFromDate" runat="server">
            <td align="Left">
                <asp:Label ID="Label10" Text="From Date:" runat="server" CssClass="FieldName"></asp:Label>
            </td>
            <td align="left">
                <telerik:RadDatePicker ID="txtFrom" CssClass="txtField" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <calendar id="Calendar1" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                        viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                    </calendar>
                    <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    <dateinput id="DateInput1" runat="server" displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy">
                    </dateinput>
                </telerik:RadDatePicker>
                <%-- <asp:CompareValidator ID="cvChkFutureDate" runat="server" ControlToValidate="txtFrom"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Date Can't be in future" Operator="LessThanEqual"
                                        Type="Date" ValidationGroup="vgbtnSubmit">
                                    </asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFrom"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="please enter from date" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>--%>
            </td>
        </td>
        <td id="tdToDate" runat="server">
            <td align="left">
                <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="To Date:"></asp:Label>
            </td>
            <td align="left">
                <telerik:RadDatePicker ID="txtTo" CssClass="txtTo" runat="server" Culture="English (United States)"
                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                    <calendar id="Calendar2" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                        viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                    </calendar>
                    <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                    <dateinput id="DateInput2" runat="server" displaydateformat="dd/MM/yyyy" dateformat="dd/MM/yyyy">
                    </dateinput>
                </telerik:RadDatePicker>
                <%--<asp:CompareValidator ID="compDateValidator" runat="server" ControlToValidate="txtTo"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="Date Can't be in future" Operator="LessThanEqual"
                                        Type="Date" ValidationGroup="vgbtnSubmit"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTo"
                                        CssClass="cvPCG" Display="Dynamic" ErrorMessage="please enter to date" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>
                                    <br />
                                   <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToCompare="txtFrom"
                                        ControlToValidate="txtTo" CssClass="cvPCG" Display="Dynamic" ErrorMessage="ToDate should be greater than FromDate"
                                        Operator="GreaterThanEqual" Type="Date" ValidationGroup="vgbtnSubmit">
                                    </asp:CompareValidator>--%>
            </td>
        </td>
        <td align="left" style="padding-right: 50px">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <tr>
            <td align="right">
                <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
            </td>
        </tr>
    </tr>
</table>
<%--  </div>--%>
<%-- <div> --%>
<%--<asp:GridView runat="server" ID="Gridview1" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Column0" HeaderText="Comm Type" 
                                    SortExpression="Column0" />
                                <asp:BoundField DataField="Folio" HeaderText="Folio" SortExpression="Folio" />
                                <asp:BoundField DataField="Scheme" HeaderText="Scheme" 
                                    SortExpression="Scheme" />
                                <asp:BoundField DataField="TransDate" HeaderText="Trans Date" 
                                    SortExpression="TransDate" />
                                <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                <asp:BoundField DataField="Amt" HeaderText="Amt" SortExpression="Amt" />
                                <asp:BoundField DataField="BAmt" HeaderText="Brokerage Amt" 
                                    SortExpression="BAmt" />
                            </Columns>
                            
                        
                        </asp:GridView>--%>
<div style="width: 100%; overflow: scroll;">
    <telerik:RadGrid ID="gvEQMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="150%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" EnableHeaderContextMenu="true"
        EnableHeaderContextFilterMenu="true">
        <%--<exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                    filename="Company/Sector" excel-format="ExcelML">--%>
        <%-- </exportsettings>--%>
        <mastertableview width="100%" allowmulticolumnsorting="True" autogeneratecolumns="false"
            commanditemdisplay="None" groupsdefaultexpanded="false" expandcollapsecolumn-groupable="true"
            grouploadmode="Client" showgroupfooter="true">
        <Columns>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Comm Type" DataField="CommType"
                UniqueName="CommType" SortExpression="CommType" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                CurrentFilterFunction="Contains">
                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Product" DataField="Product"
                UniqueName="Product" SortExpression="Product" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                CurrentFilterFunction="Contains">
                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Category" DataField="Category"
                HeaderStyle-HorizontalAlign="Right" UniqueName="Category" SortExpression="Category"
                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="SubCategory" DataField="SubCategory"
                HeaderStyle-HorizontalAlign="Right" UniqueName="SubCategory" SortExpression="SubCategory"
                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Issuer" DataField="Issuer"
                HeaderStyle-HorizontalAlign="Right" UniqueName="Issuer" SortExpression="Issuer"
                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="300px" HeaderText="Scheme" DataField="Scheme"
                HeaderStyle-HorizontalAlign="Right" UniqueName="Scheme" SortExpression="Scheme"
                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="ExpectedAmt" DataField="ExpectedAmt"
                HeaderStyle-HorizontalAlign="Right" UniqueName="ExpectedAmt" SortExpression="ExpectedAmt"
                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="Days of Holding" DataField="DofHolding"
                HeaderStyle-HorizontalAlign="Right" UniqueName="DofHolding" SortExpression="DofHolding"
                AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn HeaderStyle-Width="100px" HeaderText="CNFP" DataField="CNFP"
                HeaderStyle-HorizontalAlign="Right" UniqueName="CNFP" SortExpression="CNFP" AutoPostBackOnFilter="true"
                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                FooterStyle-HorizontalAlign="Right">
                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
            </telerik:GridBoundColumn>
        </Columns>
    </mastertableview>
        <clientsettings>
        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        <Resizing AllowColumnResize="true" />
    </clientsettings>
    </telerik:RadGrid>
</div>
