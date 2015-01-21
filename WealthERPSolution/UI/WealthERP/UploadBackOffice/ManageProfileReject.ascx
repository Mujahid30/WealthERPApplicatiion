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

    }  
</script>

<script type="text/javascript">
    function confirmation() {
        var masterTable = $find("<%= gvProfileIncreamenetReject.ClientID %>").get_masterTableView();
        var row = masterTable.get_dataItems();
        for (var i = 0; i < row.length; i++) {
            var chk = masterTable.get_dataItems()[i].findElement("chkId");
            var chk1 = masterTable.get_dataItems()[i].findElement("chkIdAll");
        }
        if (chk.checked || chk1.checked) {
            var retVal = confirm("Do you want to Delete?");
            if (retVal == true) {
                return true;
            }
            else {
                return false;
            }
        }
        else {
            alert('Please select record to delete!');
        }

    }
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
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="ClientCode" AutoPostBackOnFilter="true"
                                HeaderText="ClientCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ClientCode" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtClientCode" CssClass="txtField" runat="server" Text='<%# Bind("ClientCode") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtClientCodeFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="ClientName " AutoPostBackOnFilter="true"
                                HeaderText="ClientName " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ClientName " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="Address1" AutoPostBackOnFilter="true"
                                HeaderText="Address1" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Address1" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAddress1" CssClass="txtField" runat="server" Text='<%# Bind("Address1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddress1Footer" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="Address2" AutoPostBackOnFilter="true"
                                HeaderText="Address2" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Address2" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAddress2" CssClass="txtField" runat="server" Text='<%# Bind("Address2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddress2Footer" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="Address3" AutoPostBackOnFilter="true"
                                HeaderText="Address3" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Address3" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAddress3" CssClass="txtField" runat="server" Text='<%# Bind("Address3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddress3Footer" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
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
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="Country" AutoPostBackOnFilter="true"
                                HeaderText="Country" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="Country" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCountry" CssClass="txtField" runat="server" Text='<%# Bind("Country") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtCountryFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
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
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="EmailId" AutoPostBackOnFilter="true"
                                HeaderText="EmailId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="EmailId" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtEmailId" CssClass="txtField" runat="server" Text='<%# Bind("EmailId") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmailId"
                                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                                        ValidationGroup="btnSave" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="revPCG"></asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtEmailIdFooter" CssClass="txtField" runat="server" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmailIdFooter"
                                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                                        ValidationGroup="btnSave" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        CssClass="revPCG"></asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="OfficePhoneNo" AutoPostBackOnFilter="true"
                                HeaderText="OfficePhoneNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OfficePhoneNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOfficePhoneNo" CssClass="txtField" runat="server" Text='<%# Bind("OfficePhoneNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOfficePhoneNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="OfficeExtensionNo" AutoPostBackOnFilter="true"
                                HeaderText="OfficeExtensionNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OfficeExtensionNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOfficeExtensionNo" CssClass="txtField" runat="server" Text='<%# Bind("OfficeExtensionNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOfficeExtensionNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="OfficeFaxNo" AutoPostBackOnFilter="true"
                                HeaderText="OfficeFaxNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="OfficeFaxNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOfficeFaxNo" CssClass="txtField" runat="server" Text='<%# Bind("OfficeFaxNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtOfficeFaxNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="HomePhoneNo" AutoPostBackOnFilter="true"
                                HeaderText="HomePhoneNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="HomePhoneNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHomePhoneNo" CssClass="txtField" runat="server" Text='<%# Bind("HomePhoneNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtHomePhoneNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="HomeFaxNo" AutoPostBackOnFilter="true"
                                HeaderText="HomeFaxNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="HomeFaxNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtHomeFaxNo" CssClass="txtField" runat="server" Text='<%# Bind("HomeFaxNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtHomeFaxNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="MobileNo" AutoPostBackOnFilter="true"
                                HeaderText="MobileNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MobileNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="110px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtMobileNo" CssClass="txtField" runat="server" MaxLength="10" OnKeypress="javascript:return isNumberKey(event);"
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
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="AnnualIncome" AutoPostBackOnFilter="true"
                                HeaderText="AnnualIncome" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="AnnualIncome" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAnnualIncome" CssClass="txtField" runat="server" Text='<%# Bind("AnnualIncome") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAnnualIncomeFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="AccountNo" AutoPostBackOnFilter="true"
                                HeaderText="AccountNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="AccountNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAccountNo" CssClass="txtField" runat="server" Text='<%# Bind("AccountNo") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAccountNoFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
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
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="DOB3" AutoPostBackOnFilter="true"
                                HeaderText="DOB3" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB3"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="DOB1" AutoPostBackOnFilter="true"
                                HeaderText="DOB1" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB1"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDOB1" CssClass="txtField" runat="server" Text='<%# Bind("DOB1") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDOB1Footer" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="DOB2" AutoPostBackOnFilter="true"
                                HeaderText="DOB2" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB2"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDOB2" CssClass="txtField" runat="server" Text='<%# Bind("DOB2") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDOB2Footer" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="DOB3" AutoPostBackOnFilter="true"
                                HeaderText="DOB3" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="DOB3"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtDOB3" CssClass="txtField" runat="server" Text='<%# Bind("DOB3") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtDOB3Footer" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>--%>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="PANNO1" AutoPostBackOnFilter="true"
                                HeaderText="PANNO1" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="PANNO1"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPANNO1" CssClass="txtField" runat="server" Text='<%# Bind("PANNO1") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPan1" runat="server" Display="Dynamic" ValidationGroup="btnSave"
                                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPANNO1"
                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                    </asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPANNO1Footer" CssClass="txtField" runat="server" />
                                    <asp:RegularExpressionValidator ID="revPan2" runat="server" Display="Dynamic" ValidationGroup="btnSave"
                                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPANNO1Footer"
                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                    </asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="PANNO2" AutoPostBackOnFilter="true"
                                HeaderText="PANNO2" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="PANNO2"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPANNO2" CssClass="txtField" runat="server" Text='<%# Bind("PANNO2") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPan3" runat="server" Display="Dynamic" ValidationGroup="btnSave"
                                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPANNO2"
                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                    </asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPANNO2Footer" CssClass="txtField" runat="server" />
                                    <asp:RegularExpressionValidator ID="revPan4" runat="server" Display="Dynamic" ValidationGroup="btnSave"
                                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPANNO2Footer"
                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                    </asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="true" DataField="PANNO3" AutoPostBackOnFilter="true"
                                HeaderText="PANNO3" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="PANNO3"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPANNO3" CssClass="txtField" runat="server" Text='<%# Bind("PANNO3") %>'></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPan5" runat="server" Display="Dynamic" ValidationGroup="btnSave"
                                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPANNO3"
                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                    </asp:RegularExpressionValidator>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtPANNO3Footer" CssClass="txtField" runat="server" />
                                    <asp:RegularExpressionValidator ID="revPan6" runat="server" Display="Dynamic" ValidationGroup="btnSave"
                                        CssClass="rfvPCG" ErrorMessage="Please check PAN Format" ControlToValidate="txtPANNO3Footer"
                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}">
                                    </asp:RegularExpressionValidator>
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>
                            <%--                            <telerik:GridBoundColumn AllowFiltering="true" DataField="PANNO1 " AutoPostBackOnFilter="true"
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
                            </telerik:GridBoundColumn>--%>
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
                            <%--<telerik:GridTemplateColumn AllowFiltering="true" DataField="GuardianDOB" AutoPostBackOnFilter="true"
                                HeaderText="GuardianDOB" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="GuardianDOB" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtGuardianDOB" CssClass="txtField" runat="server" Text='<%# Bind("GuardianDOB") %>'></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtGuardianDOBFooter" CssClass="txtField" runat="server" />
                                </FooterTemplate>
                            </telerik:GridTemplateColumn>--%>
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
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="MicrCode" AutoPostBackOnFilter="true"
                                HeaderText="MicrCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="MicrCode" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="ModeOfHoldingCode  " AutoPostBackOnFilter="true"
                                HeaderText="Mode O fHolding Code  " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ModeOfHoldingCode  " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="IFSC" AutoPostBackOnFilter="true"
                                HeaderText="IFSC" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="IFSC"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="ProductCode " AutoPostBackOnFilter="true"
                                HeaderText="ProductCode " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="ProductCode " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="FolioNo " AutoPostBackOnFilter="true"
                                HeaderText="FolioNo " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="FolioNo " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchAdrLine1 " AutoPostBackOnFilter="true"
                                HeaderText="BranchAdrLine1 " ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchAdrLine1 " FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchAdrLine2" AutoPostBackOnFilter="true"
                                HeaderText="BranchAdrLine2" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchAdrLine2" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchAdrLine3" AutoPostBackOnFilter="true"
                                HeaderText="BranchAdrLine3" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchAdrLine3" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchAdrPinCode" AutoPostBackOnFilter="true"
                                HeaderText="BranchAdrPinCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchAdrPinCode" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchAdrState" AutoPostBackOnFilter="true"
                                HeaderText="BranchAdrState" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchAdrState" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BranchAdrCountry" AutoPostBackOnFilter="true"
                                HeaderText="BranchAdrCountry" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BranchAdrCountry" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="SchemeName" AutoPostBackOnFilter="true"
                                HeaderText="SchemeName" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="SchemeName" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BrokerCode" AutoPostBackOnFilter="true"
                                HeaderText="BrokerCode" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BrokerCode" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TransactionNum" AutoPostBackOnFilter="true"
                                HeaderText="TransactionNum" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="TransactionNum" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TransactionDate" AutoPostBackOnFilter="true"
                                HeaderText="TransactionDate" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="TransactionDate" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Price" AutoPostBackOnFilter="true"
                                HeaderText="Price" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="Price"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Units" AutoPostBackOnFilter="true"
                                HeaderText="Units" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="Units"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="Amount" AutoPostBackOnFilter="true"
                                HeaderText="Amount" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="Amount"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="STT" AutoPostBackOnFilter="true"
                                HeaderText="STT" ShowFilterIcon="false" CurrentFilterFunction="Contains" SortExpression="STT"
                                FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BrokeragePer" AutoPostBackOnFilter="true"
                                HeaderText="BrokeragePer" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BrokeragePer" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="BrokerageAmount" AutoPostBackOnFilter="true"
                                HeaderText="BrokerageAmount" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="BrokerageAmount" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="UserTransactionNo" AutoPostBackOnFilter="true"
                                HeaderText="UserTransactionNo" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="UserTransactionNo" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TransactionType" AutoPostBackOnFilter="true"
                                HeaderText="TransactionType" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="TransactionType" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TransactionNature" AutoPostBackOnFilter="true"
                                HeaderText="TransactionNature" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="TransactionNature" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TransactionHead" AutoPostBackOnFilter="true"
                                HeaderText="TransactionHead" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                SortExpression="TransactionHead" FooterStyle-HorizontalAlign="Right" HeaderStyle-Width="90px">
                                <ItemStyle Wrap="false" Width="" HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn AllowFiltering="true" DataField="TransactionDescription "
                                AutoPostBackOnFilter="true" HeaderText="TransactionDescription " ShowFilterIcon="false"
                                CurrentFilterFunction="Contains" SortExpression="TransactionDescription " FooterStyle-HorizontalAlign="Right"
                                HeaderStyle-Width="90px">
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
                OnClientClick="return confirmation();" OnClick="btnDelete_Click" />
        </td>
        <td class="ReProcessCell">
            <asp:Button ID="btnReProcess" runat="server" CssClass="PCGLongButton" Text="ReProcess"
                OnClick="btnReProcess_Click" />
        </td>
    </tr>
</table>
