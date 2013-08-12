<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFTurnOverMISSales.ascx.cs" Inherits="WealthERP.BusinessMIS.MFTurnOverMISSales" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

<asp:UpdatePanel ID="updnTurnOverMIS" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr>
                <td style="width: 100%;">
                    <div class="divPageHeading">
                        <table cellspacing="0" width="100%">
                            <tr>
                                <td align="left" style="width: 33%; text-align: left">
                                    MF Turnover Order MIS
                                </td>
                                <td style="width: 34%;" align="center">
                                </td>
                                <td align="right" style="width: 33%; padding-bottom: 2px;">
                                    <asp:ImageButton ID="btnTurenOverOrderExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                                        runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClientClick="setFormat('excel')"
                                        Height="25px" Width="25px" OnClick="btnTurenOverOrderExport_Click"></asp:ImageButton>
                                 
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
             <table class="TableBackground" width="100%">
     
                <tr>
                <td align="left" class="rightData" style="width: 40%;" colspan="2">
                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type:"></asp:Label>
                    <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                        runat="server" GroupName="Date" />
                    <asp:Label ID="lblPickDate" runat="server" Text="Date Range" CssClass="Field"></asp:Label>
                    &nbsp;
                    <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                        runat="server" GroupName="Date" />
                    <asp:Label ID="lblPickPeriod" runat="server" Text="Period" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr id="trCategoryAction" runat="server">
             
                <td valign="top" style="width: 40%" colspan="2" align="left">
                    <div id="divDateRange" runat="server" visible="false" style="float: left;">
                        <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                        <telerik:RadDatePicker id="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <calendar id="Calendar1" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                                viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                            </calendar>
                            <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                            <dateinput id="DateInput1" runat="server" displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                            </dateinput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                        <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                        <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                            Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                            <calendar id="Calendar2" runat="server" userowheadersasselectors="False" usecolumnheadersasselectors="False"
                                viewselectortext="x" skin="Telerik" enableembeddedskins="false">
                            </calendar>
                            <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                            <dateinput id="DateInput2" runat="server" displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                            </dateinput>
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                            CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                            runat="server" InitialValue="" ValidationGroup="vgBtnGo"> </asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                            Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                            CssClass="cvPCG" Display="Dynamic" ValidationGroup="vgBtnGo"></asp:CompareValidator>
                    </div>
                    <div id="divDatePeriod" visible="false" runat="server" style="float: left;">
                        <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                        &nbsp; &nbsp;
                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <span id="Span4" class="spnRequiredField">*</span>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                            CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                            ValidationGroup="vgBtnGo"> </asp:CompareValidator>
                    </div>
                </td>
            </tr>
         <tr id="trGoButton" runat="server"> 
         <td id="tdGoBtn" runat="server" colspan="7">
          <asp:Button ID="btnGo" runat="server" CssClass="PCGButton" Text="Go" OnClick="btnGo_Click"
                ValidationGroup="vgBtnGo"/>
         </td>
         </tr>
        </table>
        
         <table width="99%">
            <tr runat="server" id="trPnlAMC">
                <td>
                    <asp:Panel ID="pnlAMC" ScrollBars="Horizontal" Width="100%" runat="server">
                        <div runat="server" id="divGvAmcWise" visible="false" style="margin: 2px; width: 640px;">
                            <telerik:RadGrid ID="gvAmcWise" runat="server" GridLines="None" AutoGenerateColumns="False"
                                PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                                Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
                                AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true" OnNeedDataSource="gvAmcWise_OnNeedDataSource"
                                OnItemCommand="gvAmcWise_OnItemCommand" EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true">
                                <exportsettings hidestructurecolumns="true" exportonlydata="true" ignorepaging="true"
                                    filename="AmcWise Details" excel-format="ExcelML">
                                </exportsettings>
                                <mastertableview datakeynames="AMCCode" width="100%" allowmulticolumnsorting="True"
                                    autogeneratecolumns="false" commanditemdisplay="None">
                                    <Columns>
                                        <telerik:GridTemplateColumn HeaderStyle-Width="100px" AllowFiltering="false" UniqueName="action"
                                            DataField="action" FooterText="Grand Total:">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details"
                                                    ItemStyle-Width="12px" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn HeaderText="AMC" DataField="AMC" UniqueName="AMC" SortExpression="AMC"
                                            AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                  
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Purchase Cnt" HeaderTooltip="Purchase Transaction"
                                            DataField="BUYCount" HeaderStyle-HorizontalAlign="Right" UniqueName="BUYCount"
                                            SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Amount"
                                            HeaderText="Purchase Amt" DataField="BUYAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BUYAmount" DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right"
                                            SortExpression="BUYAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Sell Cnt" HeaderTooltip="Sell Transaction"
                                            DataField="SELCount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELCount"
                                            SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="Sell Amt" HeaderTooltip="Sell  Amount"
                                            DataField="SELAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="SELAmount"
                                            SortExpression="SELAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="DVR Cnt" HeaderTooltip="Dividend Reinvested Count"
                                            DataField="DVRCount" HeaderStyle-HorizontalAlign="Right" UniqueName="DVRCount"
                                            SortExpression="DVRCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="DVR Amt" HeaderTooltip="Dividend Reinvested Amount"
                                            DataField="DVRAmount" HeaderStyle-HorizontalAlign="Right" UniqueName="DVRAmount"
                                            SortExpression="DVRAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderText="DVP Cnt" HeaderTooltip="Dividend Payout Count"
                                            DataField="DVPCount" HeaderStyle-HorizontalAlign="Right" UniqueName="DVPCount"
                                            SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Dividend Payout Amount"
                                            HeaderText="DVP Amt" DataField="DVPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="DVPAmount" SortExpression="DVPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Count"
                                            HeaderText="SIP Cnt" DataField="SIPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Investment Plan Amount"
                                            HeaderText="SIP Amt" DataField="SIPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Count"
                                            HeaderText="BCI Cnt" DataField="BCICount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change In Amount"
                                            HeaderText="BCI Amt" DataField="BCIAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="BCO Cnt" HeaderTooltip="Broker Change Out Count"
                                            DataField="BCOCount" HeaderStyle-HorizontalAlign="Right" UniqueName="BCOCount"
                                            SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Broker Change Out Amount"
                                            HeaderText="BCO Amt" DataField="BCOAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Count" HeaderText="STB Cnt"
                                            DataField="STBCount" HeaderStyle-HorizontalAlign="Right" UniqueName="STBCount"
                                            SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Buy Amount"
                                            HeaderText="STB Amt" DataField="STBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Count"
                                            HeaderText="STS Cnt" DataField="STSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="STP Sell Amount"
                                            HeaderText="STS Amt" DataField="STSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Count"
                                            HeaderText="SWB Cnt" DataField="SWBCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Buy Amount"
                                            HeaderText="SWB Amt" DataField="SWBAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Count"
                                            HeaderText="SWP Cnt" DataField="SWPCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Systematic Withdrawal Plan Amount"
                                            HeaderText="SWP Amt" DataField="SWPAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Count"
                                            HeaderText="SWS Cnt" DataField="SWSCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Switch Sell Amount"
                                            HeaderText="SWS Amt" DataField="SWSAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Count"
                                            HeaderText="PRJ Cnt" DataField="PRJCount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                            ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:N0}"
                                            Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderStyle-Width="80px" HeaderTooltip="Purchase Rejection Amount"
                                            HeaderText="PRJ Amt" DataField="PRJAmount" HeaderStyle-HorizontalAlign="Right"
                                            UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                                            AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                            DataFormatString="{0:N0}" Aggregate="Sum" FooterStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                </mastertableview>
                                <headerstyle width="150px" />
                                <clientsettings>
                                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    <Resizing AllowColumnResize="true" />
                                </clientsettings>
                            </telerik:RadGrid>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>