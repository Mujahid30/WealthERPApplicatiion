﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineNCDOrderMatchExceptionHandling.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineNCDOrderMatchExceptionHandling" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%--<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>--%>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 25%;
        text-align: right;
    }
    .rightData
    {
        width: 20%;
        text-align: left;
    }
    .txtField
    {
        margin-left: 0px;
    }
</style>
<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            NCD and IPO Order Match
                        </td>
                        <td align="right">
                            <asp:ImageButton ID="btnNcdIpoExport" runat="server" ImageUrl="~/Images/Export_Excel.png"
                                AlternateText="Excel" ToolTip="Export To Excel" Visible="false" OnClientClick="setFormat('excel')"
                                Height="25px" Width="25px" OnClick="btnNcdIpoExport_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%" align="center">
    <tr id="trOrderDates" runat="server">
        <td class="leftLabel">
            <asp:Label ID="lblFrom" runat="server" Text=" Order From Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rvFromdate" ControlToValidate="txtFromDate" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a  Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="btnGo"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="rfvPCG" ControlToValidate="txtFromDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnGo" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
        </td>
        <td class="leftLabel" colspan="2">
            <asp:Label ID="lblTo" runat="server" Text="Order To Date: " CssClass="FieldName"></asp:Label>
            <%-- </td>
        <td class="rightData"  >--%>
            <telerik:RadDatePicker ID="txtToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
            <span id="Span4" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="rvtoDate" ControlToValidate="txtToDate" CssClass="rfvPCG"
                ErrorMessage="<br />Please select a Date" Display="Dynamic" runat="server" InitialValue=""
                ValidationGroup="btnGo"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" CssClass="rfvPCG" ControlToValidate="txtToDate"
                Display="Dynamic" ErrorMessage="Invalid Date" ValidationGroup="btnGo" Operator="DataTypeCheck"
                Type="Date">
            </asp:CompareValidator>
            <%--<asp:CompareValidator ID="cvtodate" runat="server" ErrorMessage="<br/>To Date should not less than From Date"
                Type="Date" ControlToValidate="txtTo" ControlToCompare="txtFrom" Operator="GreaterThanEqual"
                CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>--%>
        </td>
        <td width="5px">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Label ID="Label2" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="NCD">NCD</asp:ListItem>
                <asp:ListItem Value="IP">IPO</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Product Type"
                CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
        <td class="rightLabelData">
            <asp:Label ID="lb1Issue" runat="server" Text="Issue:" CssClass="FieldName"></asp:Label>
         </td>  <%--  </td>
        <td class="rightLabelData">--%>
        <td>
            <asp:DropDownList ID="ddlIssue" runat="server" CssClass="cmbLongField" AutoPostBack="true" Width="190%">
            </asp:DropDownList>
           <%-- <span id="Span10" class="spnRequiredField">*</span>--%>
          <span id="Span10" class="spnRequiredField" >*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Issue"
                CssClass="rfvPCG" ControlToValidate="ddlIssue" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trOrderStatus" runat="server">
        <td class="leftLabel">
            <asp:Label ID="lblOrderStatus" runat="server" Text="Order Status: " CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
            </asp:DropDownList>
            <span id="Span5" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Status"
                CssClass="rfvPCG" ControlToValidate="ddlOrderStatus" ValidationGroup="btnGo"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftLabel">
            <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="PCGButton" ValidationGroup="btnGo"
                OnClick="btnGo_Click" />
        </td>
        <%-- <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
        </td>--%>
    </tr>
</table>
<asp:Panel ID="pnlGrid" runat="server" ScrollBars="Horizontal" Visible="false" CssClass="table">
    <table width="100%" align="left">
        <tr>
            <td>
                <telerik:RadGrid ID="gvOrders" runat="server" GridLines="None" AutoGenerateColumns="False"
                    PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                    Skin="Telerik" EnableEmbeddedSkins="false" AllowFilteringByColumn="false" AllowAutomaticInserts="false"
                    ExportSettings-FileName="NCD Order Recon" OnItemDataBound="gvOrders_ItemDataBound"
                    OnItemCommand="gvOrders_ItemCommand" OnNeedDataSource="gvOrders_OnNeedDataSource">
                    <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                        filename="Order" excel-format="ExcelML">
                    </exportsettings>
                    <mastertableview datakeynames="CO_OrderId,WOS_OrderStepCode,AIM_IssueId" width="100%"
                        allowmulticolumnsorting="True" autogeneratecolumns="false" commanditemdisplay="None"
                        editmode="PopUp">
                        <Columns>
                            <telerik:GridTemplateColumn HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                     <asp:CheckBox ID="cbOrderSelect" runat="server" Checked="false" OnCheckedChanged="cbOrderSelect_changed" AutoPostBack="true" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbAutoMatch" runat="server" Checked="false" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridBoundColumn DataField="CO_OrderId" HeaderText="Order No" SortExpression="CO_OrderId"
                                ShowFilterIcon="true" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left" AllowSorting="True">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            
                            
                            
                        <%--    <telerik:GridTemplateColumn DataField="CO_OrderId" HeaderText="Order/Transaction No."
                                SortExpression="CO_OrderId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="CO_OrderId" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkOrderNo" Font-Underline="false" runat="server" CssClass="cmbFielde" Text='<%# Eval("CO_OrderId") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridTemplateColumn HeaderText="OrderDetID" AllowFiltering="false" DataField="COID_DetailsId"
                                Visible="false">
                                <ItemStyle />
                            </telerik:GridTemplateColumn>
                            <telerik:GridDateTimeColumn DataField="CO_OrderDate" HeaderText="Order Date" SortExpression="CO_OrderDate"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_OrderDate" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Center" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer" SortExpression="Customer_Name"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CO_ApplicationNo" HeaderText="App No" SortExpression="CO_ApplicationNo"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CO_ApplicationNo" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridTemplateColumn DataField="WOS_OrderStep" HeaderText="Status" SortExpression="WOS_OrderStep"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    UniqueName="WOS_OrderStep" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblOrderStep" runat="server" Text='<%#Eval("WOS_OrderStep").ToString() %>'> </asp:Label>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>--%>
                            <telerik:GridBoundColumn DataField="CEDA_DPId" HeaderText="Dp Id" SortExpression="CEDA_DPId"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="CEDA_DPId" FooterStyle-HorizontalAlign="Left" DataFormatString="{0:n}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_Quantity" HeaderText="Qty" SortExpression="COID_Quantity"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="COID_Quantity" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderText="Face value" SortExpression="AIM_FaceValue"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AIM_FaceValue" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="purchaseAmt" HeaderText="Purchase Amt" SortExpression="purchaseAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="purchaseAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COID_Price" HeaderText="Price" SortExpression="COID_Price"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="COID_Price" FooterStyle-HorizontalAlign="Left" Visible="false">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_AllotmentDate" HeaderText="Allotment Date"
                                SortExpression="AIA_AllotmentDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIA_AllotmentDate" FooterStyle-HorizontalAlign="Left"
                                DataFormatString="{0:d}">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AIA_Quantity" HeaderText="Alloted Qty" SortExpression="AIA_Quantity"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AIA_Quantity" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn Visible="false" DataField="AIA_Price" HeaderText="Alloted Price"
                                SortExpression="AIA_Price" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" UniqueName="AIA_Price" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="AllotmentAmt" HeaderText="Alloted Amt" SortExpression="AllotmentAmt"
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                UniqueName="AllotmentAmt" FooterStyle-HorizontalAlign="Left">
                                <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="Match"
                                EditText="Match" CancelText="Cancel" UpdateText="OK">
                            </telerik:GridEditCommandColumn>
                            <%-- <telerik:GridEditCommandColumn Visible="true" HeaderStyle-Width="60px" UniqueName="MarkAsReject"
                                    EditText="Mark As Reject" CancelText="Cancel" UpdateText="OK">
                                </telerik:GridEditCommandColumn>--%>
                        </Columns>
                        <EditFormSettings EditFormType="Template" PopUpSettings-Height="200px" PopUpSettings-Width="550px">
                            <FormTemplate>
                                <table>
                                    <tr id="trUnMatchedddl" runat="server">
                                        <td class="leftLabel">
                                            <asp:Label ID="lb1Issue" runat="server" Text="Issue:" CssClass="FieldName"></asp:Label>
                                        </td>
                                        <td class="rightData">
                                            <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbField">
                                            </asp:DropDownList>
                                            <span id="Span1" class="spnRequiredField">*</span>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please Select Issuer"
                                                CssClass="rfvPCG" ControlToValidate="ddlIssuer" Display="Dynamic" InitialValue="Select"
                                                ValidationGroup="btnManualMatchGo"></asp:RequiredFieldValidator>
                                        </td>
                                        <td class="rightData">
                                            <asp:Button ID="btnManualMatchGo" runat="server" Text="GO" CssClass="PCGButton" OnClick="btnManualMatchGo_Click"
                                                ValidationGroup="btnManualMatchGo" />
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trUnMatchedGrd">
                                        <td colspan="3">
                                            <telerik:RadGrid ID="gvUnmatchedAllotments" runat="server" AllowSorting="True" enableloadondemand="True"
                                                PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                                Skin="Telerik" AllowFilteringByColumn="true" DataKeyNames="AIA_Id" OnNeedDataSource="gvUnmatchedAllotments_OnNeedDataSource">
                                                <%--OnNeedDataSource="gvUnmatchedAllotments_OnNeedDataSource"--%>
                                                <MasterTableView>
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false">
                                                            <HeaderTemplate>
                                                                <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbUnMatched" runat="server" Checked="false" />
                                                            </ItemTemplate>
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="AIA_Id" HeaderText="Alotment No" SortExpression="AIA_Id"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIA_Id" FooterStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderText="Issue Name" SortExpression="AIM_IssueName"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIM_IssueName" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Customer_Name" HeaderText="Customer Name" SortExpression="Customer_Name"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="Customer_Name" FooterStyle-HorizontalAlign="Left" Visible="false">
                                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CEDA_DPId" HeaderText="DPId" SortExpression="CEDA_DPId"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="CEDA_DPId" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CO_ApplicationNo" HeaderText="Application No"
                                                            SortExpression="CO_ApplicationNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                                            AutoPostBackOnFilter="true" UniqueName="CO_ApplicationNo" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AIA_Quantity" HeaderText="Quantity" SortExpression="AIA_Quantity"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIA_Quantity" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AIA_Price" HeaderText="Price" SortExpression="AIA_Price"
                                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                                            UniqueName="AIA_Price" FooterStyle-HorizontalAlign="Left">
                                                            <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                                        </telerik:GridBoundColumn>
                                                        <%--  <telerik:GridBoundColumn DataField="Co_OrderId" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="LookupId" UniqueName="AIA_AllotmentDate"
                                                                SortExpression="AIA_AllotmentDate" Visible="false">
                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                            </telerik:GridBoundColumn>--%>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr runat="server" id="trUnMatchedBtns">
                                        <td class="leftLabel">
                                            <asp:Button ID="btnOK" Text="OK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                CausesValidation="True" ValidationGroup="btnOK" />
                                        </td>
                                        <td class="rightData">
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </FormTemplate>
                        </EditFormSettings>
                    </mastertableview>
                    <%--<clientsettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                        </clientsettings>--%>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="pnlBtns" runat="server" Width="70%" Visible="false" Height="50px"
    BorderStyle="Solid" BorderWidth="0px">
    <table>
        <tr>
            <td class="leftField">
                <asp:Button ID="btnAutoMatch" runat="server" Text="Auto Match" CssClass="PCGMediumButton"
                    OnClick="btnAutoMatch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnMannualMatch" runat="server" Text="Manual Match" CssClass="PCGMediumButton"
                    Visible="false" />
            </td>
        </tr>
    </table>
</asp:Panel>
