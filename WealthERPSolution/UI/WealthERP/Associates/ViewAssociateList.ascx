<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAssociateList.ascx.cs" Inherits="WealthERP.Associates.ViewAssociateList" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<telerik:RadStyleSheetManager ID="RdStylesheet" runat="server">
</telerik:RadStyleSheetManager>
<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }

    function ShowIsa() {

        var hdn = document.getElementById("<%=hdnIsSubscripted.ClientID%>").value;
    }
    function GetRealInvester(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= hdnIsRealInvester.ClientID %>").value = eventArgs.get_value();

        return false;
    }
    function GetReqId(source, eventArgs) {
        isItemSelected = true;
        //         document.getElementById("lblgetPan").innerHTML = "";
        document.getElementById("<%= txtRequestId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>



<table width="100%" class="TableBackground">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table table cellspacing="0" cellpadding="2" width="100%">
                    <tr>
                        <td align="left">
                            View Edit Associate
                        </td>
                        <td align="right" style="padding-bottom: 2px;">
                        </td>
                    </tr>
                </table>
                </div>
                </td>
                </tr>
                </table>
                
                
            
                <table>
    <tr id="trSearchtype" runat="server">
        <td align="leftField">
            <asp:Label ID="lblIskyc" runat="server" Text="Select:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCOption" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlCOption_SelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select" Selected="true" />
                <asp:ListItem Text="Name" Value="Name" />
                <asp:ListItem Text="PAN" Value="PAN" />
                <asp:ListItem Text="Code" Value="Code" />
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rFVddlCOption" runat="server" ErrorMessage="</br>Please Select Filter"
                CssClass="rfvPCG" ControlToValidate="ddlCOption" ValidationGroup="btnGo" Display="Dynamic"
                InitialValue="Select"></asp:RequiredFieldValidator>
        </td>
      
        <td align="left" id="tdtxtCustomerName" runat="server" visible="false">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True" onclientClick="ShowIsa()" Width="250px">  </asp:TextBox>
          
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetAssociateAllCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="3" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td align="left" id="tdRequestId" runat="server" visible="false">
            <asp:TextBox ID="txtRequestId" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="false" onclientClick="ShowIsa()" Width="250px"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtRequestId_water" TargetControlID="txtRequestId"
                WatermarkText="Enter a Request Id" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtRequestId"
                ErrorMessage="<br />Please Enter Request Id" Display="Dynamic" runat="server"
                CssClass="rfvPCG" ValidationGroup="btnGo"></asp:RequiredFieldValidator>
        </td>
        <td>
            <asp:Button ID="btngo" runat="server" CssClass="PCGButton" OnClick="click_Go" Text="Go"
                ValidationGroup="btnGo" />
        </td>
    </tr>
</table>

                
           <asp:Panel ID="pnlAdviserAssociateList" runat="server" ScrollBars="Both" Width="98%"
                Height="400Px" Visible="true">
           
                <table width="80%">
                    <tr>
                        <td>
                            <div runat="server" id="divAdviserAssociateList" style="width: 80%;">
                                <telerik:radgrid id="gvAdviserAssociateList" runat="server" allowautomaticdeletes="false"
                                    pagesize="10" enableembeddedskins="false" allowfilteringbycolumn="true" autogeneratecolumns="False"
                                    showstatusbar="false" showfooter="false" allowpaging="true" allowsorting="true"
                                    gridlines="none" allowautomaticinserts="false" skin="Telerik" enableheadercontextmenu="true"
                                    onneeddatasource="gvAdviserAssociateList_OnNeedDataSource" onitemdatabound="gvAdviserAssociateList_ItemDataBound" Visible="false">
                                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="ViewAssociateList"
                                        Excel-Format="ExcelML">
                                    </ExportSettings>
                                    <MasterTableView Width="80%" DataKeyNames="AA_AdviserAssociateId,WelcomeNotePath" AllowMultiColumnSorting="True"
                                        AutoGenerateColumns="false" CommandItemDisplay="None" GroupsDefaultExpanded="false"
                                        ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client" ShowGroupFooter="true">
                                        <Columns>
                                            <telerik:GridTemplateColumn  ItemStyle-Width="80Px" AllowFiltering="false">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlMenu" OnSelectedIndexChanged="ddlMenu_SelectedIndexChanged"
                                                        CssClass="cmbField" runat="server" EnableEmbeddedSkins="false" Skin="Telerik"
                                                        AllowCustomText="true" Width="120px" AutoPostBack="true">
                                                        <Items>
                                                            <asp:ListItem Text="Select" Value="0" Selected="true" />
                                                            <asp:ListItem Text="View" Value="View" />
                                                            <asp:ListItem Text="Edit" Value="Edit" />
                                                        </Items>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="SubBroker Code" DataField="SubBrokerCode"
                                                UniqueName="SubBrokerCode" SortExpression="SubBrokerCode" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Associate Name" DataField="AA_ContactPersonName"
                                                UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="PAN" DataField="AA_PAN"
                                                UniqueName="AA_PAN" SortExpression="AA_PAN" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                  
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Mobile" DataField="AA_Mobile"
                                                UniqueName="AA_Mobile" SortExpression="AA_Mobile" AutoPostBackOnFilter="true"
                                                AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Email Id" DataField="AA_Email"
                                                UniqueName="AA_Email" SortExpression="AA_Email" AutoPostBackOnFilter="true" AllowFiltering="true"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Address" DataField="Address"
                                                UniqueName="Address" SortExpression="Address" AutoPostBackOnFilter="true" AllowFiltering="false"
                                                ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            
                                           <telerik:GridBoundColumn DataField="AA_AMFIregistrationNo" SortExpression="AA_AMFIregistrationNo" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="AMFI Registration No" UniqueName="AA_AMFIregistrationNo">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="150px" HeaderText="Branch" DataField="AB_BranchName"
                                                UniqueName="AB_BranchName" SortExpression="AB_BranchName" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WERPBM_BankName" SortExpression="WERPBM_BankName" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Bank Name" UniqueName="WERPBM_BankName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                          <telerik:GridBoundColumn DataField="AA_AccountNum" SortExpression="AA_AccountNum" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Bank Account No" UniqueName="AA_AccountNum">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CB_MICR" SortExpression="CB_MICR" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="MICR No" UniqueName="CB_MICR">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ReportingManagerName" SortExpression="ReportingManagerName" AutoPostBackOnFilter="true"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AllowFiltering="true"
                                                HeaderText="Channel Manager Name" UniqueName="ReportingManagerName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                          
                                               <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="AAC_AssociateCategoryName" SortExpression="AAC_AssociateCategoryName"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Associate Category" UniqueName="AAC_AssociateCategoryName">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="IsActive" SortExpression="IsActive"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Is Active" UniqueName="IsActive">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn HeaderStyle-Width="130px" DataField="IsDummyAssociate" SortExpression="IsDummyAssociate"
                                                AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                AllowFiltering="true" HeaderText="Is Dummy" UniqueName="IsDummy">
                                                <ItemStyle Width="" HorizontalAlign="left" Wrap="true" VerticalAlign="Top" />
                                            </telerik:GridBoundColumn>
                                      <telerik:GridTemplateColumn UniqueName="Welcome" ItemStyle-Width="100Px" AllowFiltering="false" HeaderText="Welcome Letter">
                                         
                                                <ItemTemplate>
                                                  <asp:LinkButton ID="lbtnWelcomeletter" OnClientClick="window.document.forms[0].target='_blank'; setTimeout(function(){window.document.forms[0].target='';}, 500);"
                                                   runat="server"  OnClick="lbtnWelcomeletter_OnClick" >WelcomeLetter</asp:LinkButton>
                                                      <%--   Visible='<%# Eval("WelcomeNotePath") != DBNull.Value %>' --%>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    </ClientSettings>
                                </telerik:radgrid>
                            </div>
                        </td>
                    </tr>
                </table>
</asp:Panel>


<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="hdnCustomerId_ValueChanged" />
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hdnIsSubscripted" runat="server" />
<asp:HiddenField ID="hdnIsMFKYC" runat="server" />
<asp:HiddenField ID="hdnIsActive" runat="server" />
<asp:HiddenField ID="hdnIsProspect" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnSystemId" runat="server" />
<asp:HiddenField ID="hdnClientId" runat="server" />
<asp:HiddenField ID="hdnName" runat="server" />
<asp:HiddenField ID="hdnGroup" runat="server" />
<asp:HiddenField ID="hdnPAN" runat="server" />
<asp:HiddenField ID="hdnBranch" runat="server" />
<asp:HiddenField ID="hdnArea" runat="server" />
<asp:HiddenField ID="hdnCity" runat="server" />
<asp:HiddenField ID="hdnProcessId" runat="server" />
<asp:HiddenField ID="hdnSystemAddDate" runat="server" />
<asp:HiddenField ID="hdncustomerCategoryFilter" runat="server" />
<asp:HiddenField ID="hdnPincode" runat="server" />
<asp:HiddenField ID="hdnIsRealInvester" runat="server" />
<asp:HiddenField ID="hdnRequestId" runat="server" />
