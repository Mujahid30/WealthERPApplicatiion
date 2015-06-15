<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerUploadNew.ascx.cs"
    Inherits="WealthERP.Uploads.CustomerUploadNew" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">
    function isNumberKey(evt) { // Numbers only
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            alert('Only Numeric');
            return false;
        }
        return true;

    }  
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvbrokerageRecon.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkId";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkIdAll");

        //get an array of input types in the gridview
        var inputTypes = gvControl.getElementsByTagName("input");

        for (var i = 0; i < inputTypes.length; i++) {
            //if the input type is a checkbox and the id of it is what we set above
            //then check or uncheck according to the main checkbox in the header template
            if (inputTypes[i].type == 'checkbox' && inputTypes[i].id.indexOf(gvChkBoxControl, 0) >= 0)
                inputTypes[i].checked = mainChkBox.checked;
        }
    }
</script>

<table width="90%" class="TableBackground" cellspacing="0" cellpadding="2">
    <tr>
        <td align="left" class="leftField" width="20%">
            <asp:Label ID="lblSelectProduct" runat="server" Text=" Product:" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField" width="20%" id="tdCategory" runat="server" visible="false">
            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right" id="tdDdlCategory" runat="server" visible="false">
            <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlProductCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlProductCategory"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trSelectProduct" runat="server">
        <td id="td1" align="left" runat="server" class="leftField" width="16%" visible="true">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Commission Type:"></asp:Label>
        </td>
        <td id="td2" runat="server" visible="true">
            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Upfront" Value="UF" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlCommType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
    </tr>
    <tr id="trNCDIPO" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueType" runat="server" CssClass="FieldName" Text="Issue Type:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlIssueType_OnSelectedIndexChanged"
                AutoPostBack="true">
                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                <asp:ListItem Text="Closed Issues" Value="2"></asp:ListItem>
                <asp:ListItem Text="Current Issues" Value="1"></asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlIssueType" runat="server" ControlToValidate="ddlIssueType"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue Type"
                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select" Enabled="false"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
    </tr>
    <tr id="trSelectMutualFund" runat="server" visible="false">
        <td align="left" class="leftField">
            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProduct"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select Product Type"></asp:CompareValidator>
        </td>
        <td>
            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
        </td>
        <td align="left" class="leftField">
            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="left" class="leftField">
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
    <tr>
        <td class="leftField" id="tdTolbl" runat="server" visible="true">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Year:"></asp:Label>
        </td>
        <td class="rightField" id="tdToDate" runat="server" visible="true">
            <asp:DropDownList ID="ddlYear" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlYear"
                CssClass="rfvPCG" ErrorMessage="<br />Please select a Year" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td id="tdFromDate" class="leftField" runat="server" visible="true">
            <asp:Label ID="lblPeriod" Text="Month/Quarter:" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" id="tdFrom" runat="server" visible="true">
            <asp:DropDownList ID="ddlMnthQtr" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlMnthQtr"
                CssClass="rfvPCG" ErrorMessage="<br />Please Select Month" Display="Dynamic"
                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
        </td>
        <td class="rightField" style="padding-right: 50px">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                Text="GO" ValidationGroup="vgbtnSubmit" />
        </td>
        <td>
        </td>
    </tr>
    <tr>
    </tr>
    <tr>
        <td colspan="6">
            <%--   <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <asp:Image ID="imgProgress" ImageUrl="~/Images/ajax-loader.gif" AlternateText="Processing"
                                    runat="server" />
                            </td>
                        </tr>
                    </table>
                    <%--<img alt="Processing" src="~/Images/ajax_loader.gif" style="width: 200px; height: 100px" />
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
        </td>
    </tr>
</table>
<div runat="server" style="overflow: scroll;" id="divBtnActionSection" visible="true">
    <telerik:RadGrid ID="gvbrokerageRecon" Visible="false" runat="server" GridLines="None"
        AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
        ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
        AllowFilteringByColumn="true" Width="100%" AllowAutomaticInserts="false" OnNeedDataSource="gvbrokerageRecon_OnNeedDataSource">
        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
        </ExportSettings>
        <MasterTableView Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
            CommandItemDisplay="None" EditMode="PopUp" DataKeyNames="WCD_Id">
            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                ShowExportToCsvButton="false" ShowAddNewRecordButton="false" ShowRefreshButton="false" />
            <Columns>
                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                    HeaderStyle-Width="70px">
                    <HeaderTemplate>
                        <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkId" runat="server" />
                    </ItemTemplate>
                    <FooterTemplate>
                        <%--  --%>
                    </FooterTemplate>
                </telerik:GridTemplateColumn>
                <%--  <telerik:GridBoundColumn AllowFiltering="true" DataField="RTABrokerageAmt" AutoPostBackOnFilter="true"
                Visible="true" HeaderText="RTA Brokerage Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                UniqueName="RTABrokerageAmt" SortExpression="ReqId" FooterStyle-HorizontalAlign="Right"
                HeaderStyle-Width="90px">
                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn AllowFiltering="true" DataField="RTABrokerageAmt" AutoPostBackOnFilter="true"
                Visible="true" HeaderText="RTA Brokerage Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                UniqueName="RTABrokerageAmt" SortExpression="ReqId" FooterStyle-HorizontalAlign="Right"
                HeaderStyle-Width="90px">
                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
            </telerik:GridBoundColumn>--%>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="CO_ApplicationNumber" AutoPostBackOnFilter="true"
                    HeaderText="Application Number" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="CO_ApplicationNumber" SortExpression="CO_ApplicationNumber" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_CustomerName" AutoPostBackOnFilter="true"
                    HeaderText="Customer Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="WCD_CustomerName" SortExpression="WCD_CustomerName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="AA_ContactPersonName" AutoPostBackOnFilter="true"
                    HeaderText="Associate Name" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="AAC_AgentCode" AutoPostBackOnFilter="true"
                    HeaderText="Agent Code" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="CommissionType" AutoPostBackOnFilter="true"
                    HeaderText="Brokerage Type" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="CommissionType" SortExpression="CommissionType" FooterStyle-HorizontalAlign="Right"
                    HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_Rec_Expectedamount"
                    AutoPostBackOnFilter="true" HeaderText="System Calculated(Receivable)" ShowFilterIcon="false"
                    Aggregate="Sum" CurrentFilterFunction="Contains" UniqueName="WCD_Rec_Expectedamount"
                    SortExpression="WCD_Rec_Expectedamount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="RTABrokerageAmt" AutoPostBackOnFilter="true"
                    HeaderText="RTA file" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    UniqueName="RTABrokerageAmt" SortExpression="RTABrokerageAmt" FooterStyle-HorizontalAlign="Right"
                    Aggregate="Sum" HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="true" DataField="WCD_Act_Rec_Brokerage"
                    AutoPostBackOnFilter="true" HeaderText="Actual received" ShowFilterIcon="false"
                    Aggregate="Sum" UniqueName="WCD_Act_Rec_Brokerage" SortExpression="WCD_Act_Rec_Brokerage"
                    FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtActRecBrokerage" CssClass="txtField" runat="server" Text='<%# Bind("WCD_Act_Rec_Brokerage") %>'></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--   <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="actionRec" DataField="actionRec"
                HeaderStyle-Width="70px" HeaderText="Lock Received">
                <ItemTemplate>
                    <asp:CheckBox ID="chkIdRec" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>--%>
                <telerik:GridBoundColumn AllowFiltering="true" DataField="WCD_Pay_Expectedamount"
                    AutoPostBackOnFilter="true" HeaderText="System Calculated" ShowFilterIcon="false"
                    Aggregate="Sum" CurrentFilterFunction="Contains" UniqueName="WCD_Pay_Expectedamount"
                    SortExpression="WCD_Pay_Expectedamount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                    <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn AllowFiltering="true" DataField="WCD_Act_Pay_brokerage"
                    AutoPostBackOnFilter="true" HeaderText="Actual Payout" ShowFilterIcon="false"
                    Aggregate="Sum" CurrentFilterFunction="Contains" SortExpression="WCD_Act_Pay_brokerage"
                    UniqueName="WCD_Act_Pay_brokerage" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                    <ItemTemplate>
                        <asp:TextBox ID="txtActPaybrokerage" CssClass="txtField" runat="server" Text='<%# Bind("WCD_Act_Pay_brokerage") %>'></asp:TextBox>
                    </ItemTemplate>
                </telerik:GridTemplateColumn>
                <%--<telerik:GridTemplateColumn AllowFiltering="false" HeaderText="lock Payout" UniqueName="actionPay"
                DataField="actionPay" HeaderStyle-Width="70px">
                <ItemTemplate>
                    <asp:CheckBox ID="chkIdPay" runat="server" />
                </ItemTemplate>
            </telerik:GridTemplateColumn>--%>
            </Columns>
        </MasterTableView>
        <ClientSettings>
            <Resizing AllowColumnResize="true" />
            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        </ClientSettings>
    </telerik:RadGrid>
</div>
<asp:Button ID="btnSave" CssClass="PCGButton" runat="server" Text="Update" Visible="false" />