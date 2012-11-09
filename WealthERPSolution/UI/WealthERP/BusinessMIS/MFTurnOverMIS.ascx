<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MFTurnOverMIS.ascx.cs" Inherits="WealthERP.BusinessMIS.MFTurnOverMIS" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>
<table width="100%">
<tr>
<td colspan="3" style="width: 100%;">
<div class="divPageHeading">
    <table cellspacing="0"  width="100%">
        <tr>
        <td align="left">MF TurnOver MIS</td>
        <td  align="right"  style="padding-bottom:2px;">
        <asp:ImageButton ID="btnAMCExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnAMCExport_Click"></asp:ImageButton>
            <asp:ImageButton ID="btnSchemeExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnSchemeExport_Click"></asp:ImageButton>
            <asp:ImageButton ID="btnFolioExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnFolioExport_Click"></asp:ImageButton>
            <asp:ImageButton ID="btnBranchExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnBranchExport_Click"></asp:ImageButton>
            <asp:ImageButton ID="btnCategoryExport" ImageUrl="~/Images/Export_Excel.png" Visible="false"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" 
                OnClientClick="setFormat('excel')" Height="25px" Width="25px" 
            onclick="btnCategoryExport_Click"></asp:ImageButton>
    </td>
        </tr>
    </table>
</div>
</td>
</tr>
</table>
<table class="TableBackground" width="60%">
<tr id="trBranchRM" runat="server">
                            <td align="right" valign="top">
                                <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                                CssClass="cmbField" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                             <td>
                                &nbsp;
                            </td>
                            <td align="right" valign="top">
                            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle" >
            </asp:DropDownList>
                            </td>
                        </tr>
<tr id="trCategoryAction" runat="server">
                            <td align="right" valign="top">
                                <asp:Label ID="lblCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlCategory" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                CssClass="cmbField" >
            </asp:DropDownList>
                            </td>
                            <td>
                            &nbsp;&nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="Action" runat="server" CssClass="FieldName" Text="Action:"></asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbField" AutoPostBack="true"
                Style="vertical-align: middle" 
                onselectedindexchanged="ddlAction_SelectedIndexChanged" >
                <asp:ListItem Text="Product Level" Value="Product" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Organization Level" Value="Organization"></asp:ListItem>
            </asp:DropDownList>
                            </td>
                        </tr>

<tr>
                            <td align="right" valign="top">
                                <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date Type :"></asp:Label>
                            </td>
                            <td align="left" colspan="3">
                                <asp:RadioButton ID="rbtnPickDate" AutoPostBack="true" Checked="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                                    runat="server" GroupName="Date" />
                                <asp:Label ID="lblPickDate" runat="server" Text="Date Range" CssClass="Field"></asp:Label>
                                &nbsp;
                                <asp:RadioButton ID="rbtnPickPeriod" AutoPostBack="true" OnCheckedChanged="rbtnDate_CheckedChanged"
                                    runat="server" GroupName="Date" />
                                <asp:Label ID="lblPickPeriod" runat="server" Text="Period" CssClass="Field"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
<tr id="trRange" visible="false" runat="server" onkeypress="return keyPress(this, event)">
                            <td align="right" valign="top">
                                <asp:Label ID="lblFromDate" runat="server" CssClass="FieldName">From:</asp:Label>
                            </td>
                            <td valign="top">
                                <telerik:RadDatePicker ID="txtFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                    <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput ID="DateInput1" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtFromDate"
                                    CssClass="rfvPCG" ErrorMessage="<br />Please select a From Date" Display="Dynamic"
                                    runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                            </td>
                            
                            <td>
                            &nbsp;&nbsp;
                            </td>
                            <td align="right" valign="top">
                                <asp:Label ID="lblToDate" runat="server" CssClass="FieldName">To:</asp:Label>
                            </td>
                            <td valign="top">
                                <telerik:RadDatePicker ID="txtToDate" CssClass="txtTo" runat="server" Culture="English (United States)"
                                    Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                                    <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                        ViewSelectorText="x" Skin="Telerik" EnableEmbeddedSkins="false">
                                    </Calendar>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                    <DateInput ID="DateInput2" runat="server" DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                    </DateInput>
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtToDate"
                                    CssClass="rfvPCG" ErrorMessage="<br />Please select a To Date" Display="Dynamic"
                                    runat="server" InitialValue="" ValidationGroup="btnGo"> </asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="To Date should not be less than From Date"
                                    Type="Date" ControlToValidate="txtToDate" ControlToCompare="txtFromDate" Operator="GreaterThanEqual"
                                    CssClass="cvPCG" Display="Dynamic" ValidationGroup="btnGo"></asp:CompareValidator>
                            </td>
                        </tr>

                        <tr id="trPeriod" visible="false" runat="server">
                            <td align="right" valign="top">
                                <asp:Label ID="lblPeriod" runat="server" CssClass="FieldName">Period:</asp:Label>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span4" class="spnRequiredField"></span>
                                <br />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlPeriod"
                                    CssClass="rfvPCG" ErrorMessage="Please select a Period" Operator="NotEqual" ValueToCompare="Select a Period"
                                    ValidationGroup="btnGo"> </asp:CompareValidator>
                            </td>
                            <td>
                            &nbsp;&nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                        </tr>
  </table>                      
    <div id="dvProduct" runat="server">
    <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Select MIS: "></asp:Label>
    <asp:LinkButton ID="lnkPSummary" Text="Summary" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" 
        ValidationGroup="vgBtnGo" onclick="lnkPSummary_Click"></asp:LinkButton>
    <span>-></span>
    <asp:LinkButton ID="lnkBtnAMCWISEAUM" Text="AMC Wise" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" 
        ValidationGroup="vgBtnGo" onclick="lnkBtnAMCWISEAUM_Click"></asp:LinkButton>
    <span>-></span>
    <asp:LinkButton ID="lnkBtnSCHEMEWISEAUM" Text="Scheme Wise" CssClass="LinkButtonsWithoutUnderLine"
        runat="server"  ValidationGroup="vgBtnGo" onclick="lnkBtnSCHEMEWISEAUM_Click"></asp:LinkButton>
    <span>-></span>
    <asp:LinkButton ID="lnkBtnFOLIOWISEAUM" Text="Customer/Folio Wise"  CssClass="LinkButtonsWithoutUnderLine"
        runat="server" ValidationGroup="vgBtnGo" onclick="lnkBtnFOLIOWISEAUM_Click"></asp:LinkButton>

</div>
<div>
<br />
</div>     

    <div id="dvOrganization" runat="server">
    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Select MIS: "></asp:Label>
    <asp:LinkButton ID="lnkOSummary" Text="Summary" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" 
        ValidationGroup="vgBtnGo" onclick="lnkOSummary_Click"></asp:LinkButton>
    <span>-></span>
    <asp:LinkButton ID="lnkOBranch" Text="Branch Wise" CssClass="LinkButtonsWithoutUnderLine"
        runat="server" 
        ValidationGroup="vgBtnGo" onclick="lnkOBranch_Click"></asp:LinkButton>
    <span>-></span>
    <asp:LinkButton ID="lnkOCustomer" Text="Customer/Folio Wise"  CssClass="LinkButtonsWithoutUnderLine"
        runat="server" ValidationGroup="vgBtnGo" onclick="lnkOCustomer_Click"></asp:LinkButton>

</div> 
<div class="divSectionHeading" style="vertical-align: middle; margin:2px">
    <asp:Label ID="lblMFMISType" runat="server" CssClass="LinkButtons"></asp:Label>
</div>
<table width="100%">
<tr>
<td colspan="5">
<asp:Panel ID="pnlAMC" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
<table>
<tr><td>
<div runat="server" id="divGvAmcWise" visible="false" style="margin: 2px;width: 640px;">
    <telerik:RadGrid ID="gvAmcWise" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="AmcWise Details" OnNeedDataSource="gvAmcWise_OnNeedDataSource"
        OnItemCommand="gvAmcWise_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="AMCCode" Width="100%"  AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="AMC"  DataField="AMC"
                    UniqueName="AMC" SortExpression="AMC" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Buy Count"  DataField="BUYCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Buy Amount" DataField="BUYAmount" DataFormatString="{0:N0}"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="SEL Count"  DataField="SELCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SEL Amount" DataField="SELAmount" DataFormatString="{0:N0}"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVR Count"  DataField="DVRCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVR Amount" DataField="DVRAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVP Count"  DataField="DVPCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVP Amount" DataField="DVPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SIP Count"  DataField="SIPCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SIP Amount" DataField="SIPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCI Count"  DataField="BCICount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCI Amount" DataField="BCIAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCO Count"  DataField="BCOCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCO Amount" DataField="BCOAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STB Count"  DataField="STBCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STB Amount" DataField="STBAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STS Count" HeaderStyle-Width="150px" DataField="STSCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STS Amount" DataField="STSAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWB Count"  DataField="SWBCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWB Amount" DataField="SWBAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWP Count"  DataField="SWPCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWP Amount" DataField="SWPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWS Count"  DataField="SWSCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWS Amount" DataField="SWSAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="PRJ Count"  DataField="PRJCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="PRJ Amount" DataField="PRJAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div> 
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table> 

<table width="100%">
<tr>
<td colspan="5">
<asp:Panel ID="pnlScheme" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
<table>
<tr><td>
<div runat="server" id="dvScheme"  style="margin: 2px;width: 640px;">
    <telerik:RadGrid ID="gvSchemeWise" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="Scheme Details" OnNeedDataSource="gvSchemeWise_OnNeedDataSource"
        OnItemCommand="gvSchemeWise_OnItemCommand">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView DataKeyNames="SchemeCode" Width="100%"  AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn HeaderText="Scheme"  DataField="Scheme"
                    UniqueName="Scheme" SortExpression="Scheme" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="ExternalCode" AllowFiltering="false"  DataField="ExternalCode"
                    UniqueName="ExternalCode" SortExpression="ExternalCode" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Buy Count"  DataField="BUYCount"
                    UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Buy Amount" DataField="BUYAmount"
                    UniqueName="BUYAmount" DataFormatString="{0:N0}" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="SEL Count"  DataField="SELCount"
                    UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SEL Amount" DataField="SELAmount"
                    UniqueName="SELAmount" DataFormatString="{0:N0}" SortExpression="SELAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVR Count"  DataField="DVRCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVR Amount" DataField="DVRAmount"
                    UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVP Count"  DataField="DVPCount"
                    UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVP Amount" DataField="DVPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SIP Count"  DataField="SIPCount"
                    UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SIP Amount" DataField="SIPAmount"
                    UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCI Count"  DataField="BCICount"
                    UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCI Amount" DataField="BCIAmount"
                    UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCO Count"  DataField="BCOCount"
                    UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCO Amount" DataField="BCOAmount"
                    UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STB Count"  DataField="STBCount"
                    UniqueName="STBCount" SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STB Amount" DataField="STBAmount"
                    UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STS Count"  DataField="STSCount"
                    UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STS Amount" DataField="STSAmount"
                    UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWB Count"  DataField="SWBCount"
                    UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWB Amount" DataField="SWBAmount"
                    UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWP Count"  DataField="SWPCount"
                    UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWP Amount" DataField="SWPAmount"
                    UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWS Count"  DataField="SWSCount"
                    UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWS Amount" DataField="SWSAmount"
                    UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="PRJ Count"  DataField="PRJCount"
                    UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="PRJ Amount" DataField="PRJAmount"
                    UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div> 
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table> 

<table width="100%">
<tr>
<td colspan="5">
<asp:Panel ID="pnlBranch" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
<table>
<tr><td>
<div runat="server" id="divBranch"  style="margin: 2px;width: 640px;">
    <telerik:RadGrid ID="gvBranchWise" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="Branch Details" OnNeedDataSource="gvBranchWise_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%"  AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
               <%-- <telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn HeaderText="Branch"  DataField="Branch"
                    UniqueName="Scheme" SortExpression="Branch" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Buy Count"  DataField="BUYCount"
                    UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Buy Amount" DataField="BUYAmount"
                    UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="SEL Count"  DataField="SELCount"
                    UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SEL Amount" DataField="SELAmount"
                    UniqueName="SELAmount" SortExpression="SELAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVR Count"  DataField="DVRCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVR Amount" DataField="DVRAmount"
                    UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVP Count"  DataField="DVPCount"
                    UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVP Amount" DataField="DVPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SIP Count"  DataField="SIPCount"
                    UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SIP Amount" DataField="SIPAmount"
                    UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCI Count"  DataField="BCICount"
                    UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCI Amount" DataField="BCIAmount"
                    UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCO Count"  DataField="BCOCount"
                    UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCO Amount" DataField="BCOAmount"
                    UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STB Count"  DataField="STBCount"
                    UniqueName="STBCount" SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STB Amount" DataField="STBAmount"
                    UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STS Count"  DataField="STSCount"
                    UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STS Amount" DataField="STSAmount"
                    UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWB Count"  DataField="SWBCount"
                    UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWB Amount" DataField="SWBAmount"
                    UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWP Count"  DataField="SWPCount"
                    UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWP Amount" DataField="SWPAmount"
                    UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWS Count"  DataField="SWSCount"
                    UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWS Amount" DataField="SWSAmount"
                    UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="PRJ Count"  DataField="PRJCount"
                    UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="PRJ Amount" DataField="PRJAmount"
                    UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div> 
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table> 

<table width="100%">
<tr>
<td colspan="5">
<asp:Panel ID="pnlFolio" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
<table>
<tr><td>
<div runat="server" id="divFolioWise"  style="margin: 2px;width: 640px;">
    <telerik:RadGrid ID="gvFolioWise" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="Folio Details" OnNeedDataSource="gvFolioWise_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%"  AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <%--<telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn HeaderText="Customer"  DataField="Customer"
                    UniqueName="Customer" SortExpression="Customer" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Folio"  DataField="Folio"
                    UniqueName="Folio" SortExpression="Folio" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Buy Count"  DataField="BUYCount"
                    UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Buy Amount" DataField="BUYAmount"
                    UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="SEL Count"  DataField="SELCount"
                    UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SEL Amount" DataField="SELAmount"
                    UniqueName="SELAmount" SortExpression="SELAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVR Count"  DataField="DVRCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVR Amount" DataField="DVRAmount"
                    UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVP Count"  DataField="DVPCount"
                    UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVP Amount" DataField="DVPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SIP Count"  DataField="SIPCount"
                    UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SIP Amount" DataField="SIPAmount"
                    UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCI Count"  DataField="BCICount"
                    UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCI Amount" DataField="BCIAmount"
                    UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCO Count"  DataField="BCOCount"
                    UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCO Amount" DataField="BCOAmount"
                    UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STB Count"  DataField="STBCount"
                    UniqueName="STBCount" SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STB Amount" DataField="STBAmount"
                    UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STS Count"  DataField="STSCount"
                    UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STS Amount" DataField="STSAmount"
                    UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWB Count"  DataField="SWBCount"
                    UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWB Amount" DataField="SWBAmount"
                    UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWP Count"  DataField="SWPCount"
                    UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWP Amount" DataField="SWPAmount"
                    UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWS Count"  DataField="SWSCount"
                    UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWS Amount" DataField="SWSAmount"
                    UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="PRJ Count"  DataField="PRJCount"
                    UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="PRJ Amount" DataField="PRJAmount"
                    UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div> 
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table>

<table width="100%">
<tr>
<td colspan="5">
<asp:Panel ID="pnlCategory" runat="server"  ScrollBars="Horizontal" Width="98%" Visible="true">
<table>
<tr><td>
<div runat="server" id="divCategory"  style="margin: 2px;width: 640px;">
    <telerik:RadGrid ID="gvCategoryWise" runat="server" GridLines="None" AutoGenerateColumns="False"
        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
        Skin="Telerik" EnableEmbeddedSkins="false" Width="120%" AllowFilteringByColumn="true"
        AllowAutomaticInserts="false" ExportSettings-FileName="Category Details" OnNeedDataSource="gvCategoryWise_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true">
        </ExportSettings>
        <MasterTableView Width="100%"  AllowMultiColumnSorting="True"
            AutoGenerateColumns="false" CommandItemDisplay="None">
            <Columns>
                <%--<telerik:GridTemplateColumn  AllowFiltering="false" UniqueName="action"
                    DataField="action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkprAmc" runat="server" CommandName="Select" Text="Details" ItemStyle-Width="12px" />
                    </ItemTemplate>
                </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn HeaderText="Category"  DataField="Category"
                    UniqueName="Category" SortExpression="Category" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="SubCategory"  DataField="SubCategory"
                    UniqueName="SubCategory" SortExpression="SubCategory" AutoPostBackOnFilter="true" AllowFiltering="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn HeaderText="Buy Count"  DataField="BUYCount"
                    UniqueName="BUYCount" SortExpression="BUYCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="Buy Amount" DataField="BUYAmount"
                    UniqueName="BUYAmount" SortExpression="BUYAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="SEL Count"  DataField="SELCount"
                    UniqueName="SELCount" SortExpression="SELCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SEL Amount" DataField="SELAmount"
                    UniqueName="SELAmount" SortExpression="SELAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVR Count"  DataField="DVRCount"
                    UniqueName="AUM" SortExpression="AUM" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVR Amount" DataField="DVRAmount"
                    UniqueName="DVRAmount" SortExpression="DVRAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                 <telerik:GridBoundColumn HeaderText="DVP Count"  DataField="DVPCount"
                    UniqueName="DVPCount" SortExpression="DVPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="DVP Amount" DataField="DVPAmount"
                    UniqueName="Percentage" SortExpression="Percentage" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SIP Count"  DataField="SIPCount"
                    UniqueName="SIPCount" SortExpression="SIPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SIP Amount" DataField="SIPAmount"
                    UniqueName="SIPAmount" SortExpression="SIPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCI Count"  DataField="BCICount"
                    UniqueName="BCICount" SortExpression="BCICount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCI Amount" DataField="BCIAmount"
                    UniqueName="BCIAmount" SortExpression="BCIAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="BCO Count"  DataField="BCOCount"
                    UniqueName="BCOCount" SortExpression="BCOCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="BCO Amount" DataField="BCOAmount"
                    UniqueName="BCOAmount" SortExpression="BCOAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STB Count"  DataField="STBCount"
                    UniqueName="STBCount" SortExpression="STBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STB Amount" DataField="STBAmount"
                    UniqueName="STBAmount" SortExpression="STBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="STS Count"  DataField="STSCount"
                    UniqueName="STSCount" SortExpression="STSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="STS Amount" DataField="STSAmount"
                    UniqueName="STSAmount" SortExpression="STSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWB Count"  DataField="SWBCount"
                    UniqueName="SWBCount" SortExpression="SWBCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWB Amount" DataField="SWBAmount"
                    UniqueName="SWBAmount" SortExpression="SWBAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWP Count"  DataField="SWPCount"
                    UniqueName="SWPCount" SortExpression="SWPCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWP Amount" DataField="SWPAmount"
                    UniqueName="SWPAmount" SortExpression="SWPAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="SWS Count"  DataField="SWSCount"
                    UniqueName="SWSCount" SortExpression="SWSCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="SWS Amount" DataField="SWSAmount"
                    UniqueName="SWSAmount" SortExpression="SWSAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                
                <telerik:GridBoundColumn HeaderText="PRJ Count"  DataField="PRJCount"
                    UniqueName="PRJCount" SortExpression="PRJCount" AutoPostBackOnFilter="true" AllowFiltering="false"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn  HeaderText="PRJ Amount" DataField="PRJAmount"
                    UniqueName="PRJAmount" SortExpression="PRJAmount" AutoPostBackOnFilter="true"
                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
            </Columns>
        </MasterTableView>
        <HeaderStyle Width="150px" />
        <ClientSettings>
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
            <Resizing AllowColumnResize="true" />
        </ClientSettings>
    </telerik:RadGrid>
</div> 
</td></tr>
</table>
</asp:Panel>
</td>
</tr>
</table> 

<asp:HiddenField ID="hdnbranchId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnbranchHeadId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAll" runat="server" Visible="false" />
<asp:HiddenField ID="hdnrmId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnadviserId" runat="server"/>
<asp:HiddenField ID="hdnType" runat="server"/>  
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />                              


