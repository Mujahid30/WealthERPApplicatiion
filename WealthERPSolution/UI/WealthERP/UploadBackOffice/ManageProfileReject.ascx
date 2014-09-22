<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ManageProfileReject.ascx.cs"
    Inherits="WealthERP.UploadBackOffice.ManageProfileReject" %>
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
</script>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvProfileIncreamenetReject.ClientID %>');

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

<table width="100%">
    <tr id="tr1" runat="server">
        <td class="Cell" align="right">
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="PCGLongLongButton" Text="Data Translation Mapping"
                OnClick="btnDataTranslationMapping_Click" />
        </td>
    </tr>
</table>
<asp:Panel ID="pnlProfileReject" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
    Visible="true">
    <table width="100%">
        <tr>
            <td align="center">
                <div id="msgReprocessComplete" runat="server" class="success-msg" align="center"
                    visible="false">
                    Reprocess successfully Completed
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td align="center">
                <div id="msgReprocessincomplete" runat="server" class="failure-msg" align="center"
                    visible="false">
                    Reprocess Failed!
                </div>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr id="trExportFilteredDupData" runat="server">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadGrid ID="gvProfileIncreamenetReject" Visible="true" runat="server" GridLines="None"
                    AutoGenerateColumns="False" PageSize="10" AllowSorting="true" AllowPaging="True"
                    ShowStatusBar="True" ShowFooter="true" Skin="Telerik" EnableEmbeddedSkins="false"
                    AllowFilteringByColumn="true" Width="100%" AllowAutomaticInserts="false" OnNeedDataSource="gvProfileIncreamenetReject_OnNeedDataSource">
                    <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                    </ExportSettings>
                    <MasterTableView Width="102%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                        CommandItemDisplay="None" EditMode="PopUp" DataKeyNames="ID,TableNo">
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
                                    <asp:Button ID="btnSave" CssClass="FieldName" OnClick="btnSave_Click" runat="server"
                                        Text="Save" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="RejectedReasonDescription "
                                AutoPostBackOnFilter="true" Visible="true" HeaderText="RejectedReasonDescription "
                                ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="RejectedReasonDescription "
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="250px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ClientCode" AllowFiltering="true" HeaderText="ClientCode"
                                UniqueName="ClientCode" SortExpression="ClientCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                AutoPostBackOnFilter="true" HeaderStyle-Width="120px" FilterControlWidth="60px">
                                <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="ClientName " AutoPostBackOnFilter="true"
                                HeaderText="ClientName " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ClientName " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Address1" AutoPostBackOnFilter="true"
                                HeaderText="Address1" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Address1" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Address2" AutoPostBackOnFilter="true"
                                HeaderText="Address2" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Address2" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Address3" AutoPostBackOnFilter="true"
                                HeaderText="Address3" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Address3" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="City" AutoPostBackOnFilter="true"
                                HeaderText="City" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="City"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCity" CssClass="txtField" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCityFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OtherCity " AutoPostBackOnFilter="true"
                                HeaderText="OtherCity " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OtherCity " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="State" AutoPostBackOnFilter="true"
                                HeaderText="State" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="State"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtState" CssClass="txtField" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtStateFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OtherState" AutoPostBackOnFilter="true"
                                HeaderText="OtherState" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OtherState" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Country" AutoPostBackOnFilter="true"
                                HeaderText="Country" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Country" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OtherCountry" AutoPostBackOnFilter="true"
                                HeaderText="OtherCountry" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OtherCountry" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="PinCode" AutoPostBackOnFilter="true"
                                HeaderText="PinCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="PinCode" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPinCode" CssClass="txtField" runat="server" Text='<%# Bind("PinCode") %>'
                                        OnKeypress="javascript:return isNumberKey(event);" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPinCodeFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="EmailId" AutoPostBackOnFilter="true"
                                HeaderText="EmailId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="EmailId" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OfficePhoneNo" AutoPostBackOnFilter="true"
                                HeaderText="OfficePhoneNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OfficePhoneNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OfficeExtensionNo" AutoPostBackOnFilter="true"
                                HeaderText="OfficeExtensionNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OfficeExtensionNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OfficeFaxNo" AutoPostBackOnFilter="true"
                                HeaderText="OfficeFaxNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OfficeFaxNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="HomePhoneNo" AutoPostBackOnFilter="true"
                                HeaderText="HomePhoneNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="HomePhoneNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="HomeFaxNo" AutoPostBackOnFilter="true"
                                HeaderText="HomeFaxNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="HomeFaxNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="MobileNo" AutoPostBackOnFilter="true"
                                HeaderText="MobileNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MobileNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMobileNo" CssClass="txtField" runat="server" OnKeypress="javascript:return isNumberKey(event);"
                                        AutoPostBack="true" Text='<%# Bind("MobileNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtMobileNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="Occupation" AutoPostBackOnFilter="true"
                                HeaderText="Occupation" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Occupation" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOccupation" CssClass="txtField" runat="server" Text='<%# Bind("Occupation") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOccupationFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="AnnualIncome" AutoPostBackOnFilter="true"
                                HeaderText="AnnualIncome" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="AnnualIncome" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="AccountNo" AutoPostBackOnFilter="true"
                                HeaderText="AccountNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="AccountNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="AccountType" AutoPostBackOnFilter="true"
                                HeaderText="AccountType" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="AccountType" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAccountType" CssClass="txtField" runat="server" Text='<%# Bind("AccountType") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAccountTypeFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="BankName" AutoPostBackOnFilter="true"
                                HeaderText="BankName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BankName" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBankName" CssClass="txtField" runat="server" Text='<%# Bind("BankName") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtBankNameFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="OtherBank" AutoPostBackOnFilter="true"
                                HeaderText="OtherBank" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OtherBank" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BankBranch" AutoPostBackOnFilter="true"
                                HeaderText="BankBranch" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BankBranch" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BankBranchCode" AutoPostBackOnFilter="true"
                                HeaderText="BankBranchCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BankBranchCode" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BankCity" AutoPostBackOnFilter="true"
                                HeaderText="BankCity" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BankCity" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="PersonalStatus" AutoPostBackOnFilter="true"
                                HeaderText="PersonalStatus" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="PersonalStatus" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPersonalStatus" CssClass="txtField" runat="server" Text='<%# Bind("PersonalStatus") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPersonalStatusFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TaxStatus" AutoPostBackOnFilter="true"
                                HeaderText="TaxStatus" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="TaxStatus" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Category" AutoPostBackOnFilter="true"
                                HeaderText="Category" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Category" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DepositoryName" AutoPostBackOnFilter="true"
                                HeaderText="DepositoryName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="DepositoryName" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DPName" AutoPostBackOnFilter="true"
                                HeaderText="DPName" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DPName"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DPID" AutoPostBackOnFilter="true"
                                HeaderText="DPID" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DPID"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BenficiaryAccountNo" AutoPostBackOnFilter="true"
                                HeaderText="BenficiaryAccountNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BenficiaryAccountNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="SecondAppName" AutoPostBackOnFilter="true"
                                HeaderText="SecondAppName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="SecondAppName" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="ThirdAppName" AutoPostBackOnFilter="true"
                                HeaderText="ThirdAppName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ThirdAppName" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DOB1" AutoPostBackOnFilter="true"
                                HeaderText="DOB1" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB1"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DOB2" AutoPostBackOnFilter="true"
                                HeaderText="DOB2" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB2"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DOB3 " AutoPostBackOnFilter="true"
                                HeaderText="DOB3 " ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB3 "
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="PANNO1 " AutoPostBackOnFilter="true"
                                HeaderText="PANNO1 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="PANNO1 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="PANNO2 " AutoPostBackOnFilter="true"
                                HeaderText="PANNO2 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="PANNO2 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="PANNO3 " AutoPostBackOnFilter="true"
                                HeaderText="PANNO3 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="PANNO3 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="MINNO1 " AutoPostBackOnFilter="true"
                                HeaderText="MINNO1 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MINNO1 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="MINNO2 " AutoPostBackOnFilter="true"
                                HeaderText="MINNO2 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MINNO2 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="MINNO3 " AutoPostBackOnFilter="true"
                                HeaderText="MINNO3 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MINNO3 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="ESCNo  " AutoPostBackOnFilter="true"
                                HeaderText="ESCNo  " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ESCNo  " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="UINNo " AutoPostBackOnFilter="true"
                                HeaderText="UINNo " ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="UINNo "
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="FatherHusbandNo " AutoPostBackOnFilter="true"
                                HeaderText="FatherHusbandNo " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="FatherHusbandNo " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="GuardianName " AutoPostBackOnFilter="true"
                                HeaderText="GuardianName " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="GuardianName " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Relation " AutoPostBackOnFilter="true"
                                HeaderText="Relation " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Relation " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="GuardianDOB " AutoPostBackOnFilter="true"
                                HeaderText="GuardianDOB " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="GuardianDOB " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="GuardianPANNo " AutoPostBackOnFilter="true"
                                HeaderText="GuardianPANNo " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="GuardianPANNo " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="GuardianMINNo " AutoPostBackOnFilter="true"
                                HeaderText="GuardianMINNo " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="GuardianMINNo " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="NomineeName" AutoPostBackOnFilter="true"
                                HeaderText="NomineeName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="NomineeName" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="NomineeRelation" AutoPostBackOnFilter="true"
                                HeaderText="NomineeRelation" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="NomineeRelation" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="POA" AutoPostBackOnFilter="true"
                                HeaderText="POA" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="POA"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="SubBroker " AutoPostBackOnFilter="true"
                                HeaderText="SubBroker " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="SubBroker " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchCode " AutoPostBackOnFilter="true"
                                HeaderText="BranchCode " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchCode " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Gender " AutoPostBackOnFilter="true"
                                HeaderText="Gender " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Gender " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField=" KYCComplaint1stHolder "
                                AutoPostBackOnFilter="true" HeaderText="KYCComplaint1stHolder " ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" SortExpression=" KYCComplaint1stHolder " FooterStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="KYCComplaint2ndHolder "
                                AutoPostBackOnFilter="true" HeaderText="KYCComplaint2ndHolder " ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" SortExpression="KYCComplaint2ndHolder " FooterStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="KYCComplaint3rdHolder"
                                AutoPostBackOnFilter="true" HeaderText="KYCComplaint3rdHolder " ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" SortExpression="KYCComplaint3rdHolder " FooterStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="NeftCode " AutoPostBackOnFilter="true"
                                HeaderText="NeftCode " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="NeftCode " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="RtgsCode " AutoPostBackOnFilter="true"
                                HeaderText="RtgsCode " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="RtgsCode " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="MicrCode " AutoPostBackOnFilter="true"
                                HeaderText="MicrCode " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MicrCode " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <%--  <telerik:GridBoundColumn AllowFiltering="true" DataField="WRR_RejectReasonCodes "
                                AutoPostBackOnFilter="true" HeaderText="WRR_RejectReasonCodes " ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" SortExpression="WRR_RejectReasonCodes " FooterStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>--%>
                        </Columns>
                    </MasterTableView>
                    <ClientSettings>
                        <Resizing AllowColumnResize="true" />
                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                    </ClientSettings>
                </telerik:RadGrid>
            </td>
        </tr>
    </table>
</asp:Panel>
<table width="100%">
    <tr id="trReprocess" runat="server">
        <td class="SubmitCell">
            <asp:Button ID="btnDelete" runat="server" CssClass="PCGLongButton" Text="Delete Records"
                OnClick="btnDelete_Click" />
        </td>
        <td class="ReProcessCell">
            <asp:Button ID="btnReProcess" runat="server" CssClass="PCGLongButton" Text="ReProcess"
                OnClick="btnReProcess_Click" />
        </td>
    </tr>
    
</table>
