<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineNCDIssueSetup.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineNCDIssueSetup" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>

<script type="text/javascript">
    var popUp;
    function PopUpShowing(sender, eventArgs) {
        popUp = eventArgs.get_popUp();
        var gridWidth = sender.get_element().offsetWidth;
        var gridHeight = sender.get_element().offsetHeight;
        var popUpWidth = popUp.style.width.substr(0, popUp.style.width.indexOf("px"));
        var popUpHeight = popUp.style.height.substr(0, popUp.style.height.indexOf("px"));
        popUp.style.left = ((gridWidth - popUpWidth) / 2 + sender.get_element().offsetLeft).toString() + "px";
        popUp.style.top = ((gridHeight - popUpHeight) / 2 + sender.get_element().offsetTop).toString() + "px";
    } 
</script>

<script type="text/javascript" language="javascript">
    function CheckTextBoxes() {

        var SelectedVal;
        var txtbox1 = document.getElementById('<%=txtNSECode.ClientID %>');
        var txtbox2 = document.getElementById('<%=txtBSECode.ClientID %>');
        var IndexValuebond = document.getElementById('<%=ddlProduct.ClientID %>');
        var SelectedValbond = IndexValuebond.value;

        if (SelectedValbond == 'NCD') {
            var IndexValue = document.getElementById('<%=ddlSubInstrCategory.ClientID %>');
            SelectedVal = IndexValue.value;
        }
        if (SelectedVal == "FICGCG") {
            return true;
        }
        else if (txtbox1.value == "" && txtbox2.value == "") {
            alert('Please Fill One Of The Fields NSE or BSE Code.');
            return false;
        }
        else {
            return true;
        }
    }

</script>

<script type="text/javascript" language="javascript">
    function CheckBoxes(sender, args) {
        if (document.getElementById("<%=chkMultipleApplicationAllowed.ClientID %>").checked == false && document.getElementById("<%=chkMultipleApplicationNotAllowed.ClientID %>").checked == false) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

</script>

<script type="text/javascript" language="javascript">
    function ChkIscancelallowedornot(sender, args) {
        if (document.getElementById("<%=chkIScancelAllowed.ClientID %>").checked == false && document.getElementById("<%=chkIsCancelNotAllowed.ClientID %>").checked == false) {
            args.IsValid = false;
        }
        else {
            args.IsValid = true;
        }
    }

</script>

<script type="text/javascript">
    var crnt = 0;
    function PreventClicks() {

        if (typeof (Page_ClientValidate('SetUpSubmit')) == 'function') {
            Page_ClientValidate();
        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
    function Calculate() {
        var facevalue = document.getElementById('<%=txtFaceValue.ClientID %>').value;
        var Qty = document.getElementById('<%=txtIssueSizeQty.ClientID %>').value;
        if (Qty != "" && Qty != 0) {
            document.getElementById('<%=txtIssueSizeAmt.ClientID %>').value = facevalue * Qty;

        }
        else {
            document.getElementById('<%=txtIssueSizeAmt.ClientID %>').value = "";
        }
    }
</script>

<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    .leftLabel
    {
        width: 18%;
        text-align: right;
    }
    .rightData
    {
        width: 18%;
        text-align: left;
    }
</style>
<%-- <asp:Panel ID="Panel2" runat="server" CssClass="Landscape" Width="100%" ScrollBars="Horizontal">--%>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Add Issue
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkBtnEdit" runat="server" CssClass="LinkButtons" Text="Edit"
                                OnClick="lnkBtnEdit_Click" Visible="false"></asp:LinkButton>
                            &nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnlBack" CssClass="LinkButtons" Text="Back" Visible="false"
                                OnClick="lnlBack_Click"></asp:LinkButton>&nbsp; &nbsp;
                            <asp:LinkButton runat="server" ID="lnkDelete" CssClass="LinkButtons" Text="Delete"
                                Visible="false" OnClientClick="javascript: return confirm('Are you sure you want to Delete the Order?')"></asp:LinkButton>&nbsp;
                            <%-- OnClick="lnkDelete_Click"--%>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="updNCDIPO" runat="server">
    <ContentTemplate>
        <telerik:RadWindow ID="radAplicationPopUp" runat="server" VisibleOnPageLoad="false"
            Height="30%" Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false"
            Behaviors="Resize, Close, Move" Title="Add New Active Range">
            <contenttemplate>
                <div style="padding: 20px">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgAplication" runat="server" AllowSorting="True" enableloadondemand="True"
                                    PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                                    ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="false" ShowStatusBar="True"
                                    Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgAplication_OnNeedDataSource"
                                    OnItemCommand="rgAplication_ItemCommand" OnItemDataBound="rgAplication_ItemDataBound">
                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIFR_Id"
                                        AutoGenerateColumns="false" Width="100%" EditMode="PopUp" CommandItemSettings-AddNewRecordText="Create Active Range"
                                        CommandItemDisplay="Top">
                                        <Columns>
                                            <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                                UpdateText="Update">
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="AIFR_Id" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer Name" UniqueName="AIFR_Id"
                                                SortExpression="AIFR_Id" AllowFiltering="true" Visible="false">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AIM_IssueId" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer Name" UniqueName="AIM_IssueId"
                                                SortExpression="AIM_IssueId" AllowFiltering="true" Visible="false">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AIFR_From" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="From" UniqueName="AIFR_From"
                                                SortExpression="AIFR_From" AllowFiltering="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AIFR_To" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="To" UniqueName="AIFR_To"
                                                SortExpression="AIFR_To" AllowFiltering="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="AIFR_IsActive" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Active" UniqueName="AIFR_IsActive"
                                                SortExpression="AIFR_IsActive" AllowFiltering="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                                ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                Text="Delete" Visible="false">
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                            <%--<telerik:GridEditCommandColumn EditText="Delete" UniqueName="DeleteIssuer" CancelText="Cancel"
                                            UpdateText="Delete">
                                        </telerik:GridEditCommandColumn>--%>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template" PopUpSettings-Height="150px" PopUpSettings-Width="330px">
                                            <FormTemplate>
                                                <table width="75%" cellspacing="2" cellpadding="2">
                                                    <tr>
                                                        <td class="leftField" style="width: 10%">
                                                            <asp:Label ID="lb1FromRange" runat="server" Text="From: " CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightField" style="width: 25%" colspan="2">
                                                            <asp:TextBox ID="txtFrom" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                                                            <span id="spnFrom" class="spnRequiredField">*</span>
                                                            <%--<asp:RegularExpressionValidator ID="regFrom" ControlToValidate="txtFrom" runat="server"
                                                        Display="Dynamic" ErrorMessage="<br/>Please Enter Integer Value" CssClass="cvPCG"
                                                        ValidationExpression="[1-9]\d*$" ValidationGroup="rgApllOk">     
                                                    </asp:RegularExpressionValidator>--%>
                                                            <asp:RegularExpressionValidator ID="regFrom" ControlToValidate="txtFrom" runat="server"
                                                                Display="Dynamic" ErrorMessage="<br/>Enter Numeric Value Between 8-10 Digit"
                                                                CssClass="cvPCG" ValidationExpression="[0-9]{8,10}$" ValidationGroup="rgApllOk" />
                                                            <asp:CompareValidator ID="cmpFrom" ControlToValidate="txtFrom" runat="server" ControlToCompare="txtTo"
                                                                Display="Dynamic" ErrorMessage="<br/>From  Should Be less Than To" Type="integer"
                                                                Operator="LessThan"></asp:CompareValidator>
                                                            <%--   <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="txtFrom" ErrorMessage="Please enter From Range"
                                                        ValidationGroup="rgApllOk" Display="Dynamic" runat="server" CssClass="rfvPCG">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" style="width: 10%">
                                                            <asp:Label ID="Label22" runat="server" Text="To: " CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightField" style="width: 25%" colspan="2">
                                                            <asp:TextBox ID="txtTo" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                                                            <span id="Span37" class="spnRequiredField">*</span>
                                                            <%-- <asp:RegularExpressionValidator ID="regTo" ControlToValidate="txtTo" runat="server"
                                                        Display="Dynamic" ErrorMessage="<br/>Please Enter Integer Value" CssClass="cvPCG"
                                                        ValidationExpression="[1-9]\d*$" ValidationGroup="rgApllOk">     
                                                    </asp:RegularExpressionValidator>--%>
                                                            <asp:RegularExpressionValidator ID="regTo" ControlToValidate="txtTo" runat="server"
                                                                Display="Dynamic" ErrorMessage="<br/>Enter Numeric Value Between 8-10 Digit"
                                                                CssClass="cvPCG" ValidationExpression="[0-9]{8,10}$" ValidationGroup="rgApllOk" />
                                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator31" ControlToValidate="txtTo" ErrorMessage="Please enter To Range"
                                                        ValidationGroup="rgApllOk" Display="Dynamic" runat="server" CssClass="rfvPCG">
                                                    </asp:RequiredFieldValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftLabel">
                                                            <asp:Button ID="btnOK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' CausesValidation="True"
                                                                ValidationGroup="rgApllOk"></asp:Button>
                                                        </td>
                                                        <td class="rightData">
                                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                        </td>
                                                        <td class="leftLabel" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="RightData">
                                <asp:Button ID="BtnActivRangeClose" runat="server" Text="Close" CssClass="PCGButton"
                                    OnClick="BtnActivRangeClose_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </contenttemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="radIssuerPopUp" runat="server" VisibleOnPageLoad="false" Height="30%"
            Width="500px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
            Title="Add New Issuer" RestrictionZoneID="radWindowZone" OnClientShow="setCustomPosition"
            Top="10" Left="20">
            <contenttemplate>
                <div style="padding: 20px">
                    <table width="100%">
                        <tr>
                            <td colspan="2">
                                <telerik:RadGrid ID="rgIssuer" runat="server" AllowSorting="True" enableloadondemand="True"
                                    PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                                    ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="true" ShowStatusBar="True"
                                    Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgIssuer_OnNeedDataSource"
                                    OnItemCommand="rgIssuer_ItemCommand" OnItemDataBound="rgIssuer_ItemDataBound">
                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="PI_IssuerId,PAISC_AssetInstrumentSubCategoryCode,PI_IssuerName"
                                        AutoGenerateColumns="false" Width="100%" CommandItemSettings-AddNewRecordText="Create Issuer"
                                        CommandItemDisplay="Top">
                                        <Columns>
                                            <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                                UpdateText="Update">
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="PI_IssuerName" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer Name" UniqueName="PI_IssuerName"
                                                SortExpression="PI_IssuerName" AllowFiltering="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PI_IssuerCode" HeaderStyle-Width="20px" CurrentFilterFunction="Contains"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Issuer Code" UniqueName="PI_IssuerCode"
                                                SortExpression="PI_IssuerCode" AllowFiltering="true" Visible="false">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PAISC_AssetInstrumentSubCategoryCodes" HeaderStyle-Width="20px"
                                                CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                HeaderText="Category" UniqueName="PAISC_AssetInstrumentSubCategoryCodes" SortExpression="PAISC_AssetInstrumentSubCategoryCodes"
                                                AllowFiltering="true">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                                ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                Text="Delete">
                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                            </telerik:GridButtonColumn>
                                            <%--<telerik:GridEditCommandColumn EditText="Delete" UniqueName="DeleteIssuer" CancelText="Cancel"
                                            UpdateText="Delete">
                                        </telerik:GridEditCommandColumn>--%>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template" PopUpSettings-Height="150px" PopUpSettings-Width="330px">
                                            <FormTemplate>
                                                <table width="75%" cellspacing="2" cellpadding="2">
                                                    <tr>
                                                        <td class="leftField" style="width: 10%">
                                                            <asp:Label ID="lb1IssuerName" runat="server" Text="Issuer:" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtIssuerName" runat="server" CssClass="txtField"></asp:TextBox><br />
                                                            <span id="Span21"></span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" ControlToValidate="txtIssuerName"
                                                                ErrorMessage="Please enter Issuer name" ValidationGroup="rgIssuerOk" Display="Dynamic"
                                                                runat="server" CssClass="rfvPCG">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr id="trcategory" runat="server">
                                                        <td class="leftField" style="width: 10%">
                                                            <asp:Label ID="lb1IssuerCode" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                                                        </td>
                                                        <td class="rightField" style="width: 25%">
                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="cmbField">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtIssuerCode" runat="server" CssClass="txtField" Visible="false"></asp:TextBox><br />
                                                            <span id="spnNewFolioValidation"></span>
                                                            <asp:RequiredFieldValidator ID="rfvName" ControlToValidate="ddlCategory" ErrorMessage="Please Select Category"
                                                                ValidationGroup="rgIssuerOk" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                                InitialValue="Select">
                                                            </asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <%-- <tr>
                                                <td class="leftField" style="width: 10%">
                                                    <asp:Label ID="Label23" runat="server" Text="SubCategory: " CssClass="FieldName"></asp:Label>
                                                </td>
                                                <td class="rightField" style="width: 25%">
                                                    <asp:DropDownList ID="ddlNcdSubCategory" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                                                        Width="200px">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator51" ControlToValidate="txtIssuerCode"
                                                        ErrorMessage="Please Select SubCategory" ValidationGroup="rgIssuerOk" Display="Dynamic"
                                                        runat="server" CssClass="rfvPCG">
                                                    </asp:RequiredFieldValidator>
                                                </td>
                                            </tr>--%>
                                                    <tr>
                                                        <td class="leftLabel">
                                                            <asp:Button ID="btnOK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>' CausesValidation="True"
                                                                ValidationGroup="rgIssuerOk" />
                                                            </asp:Button>
                                                        </td>
                                                        <td class="rightData">
                                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                        </td>
                                                        <td class="leftLabel" colspan="2">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td class="RightData">
                                <asp:Button ID="btnIssuerPopClose" runat="server" Text="Close" CssClass="PCGButton"
                                    OnClick="btnIssuerPopClose_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </contenttemplate>
        </telerik:RadWindow>
        <table width="100%" runat="server" id="tbIssue">
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1Product" runat="server" Text="Product:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px" OnSelectedIndexChanged="ddlProduct_Selectedindexchanged">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
                        <asp:ListItem Value="IP">IPO</asp:ListItem>
                        <asp:ListItem Value="NCD">Bonds</asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please Select Product"
                        CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel" id="tdlblCategory" runat="server">
                    <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                </td>
                <td align="rightData" id="tdddlCategory" runat="server">
                    <asp:DropDownList ID="ddlSubInstrCategory" runat="server" CssClass="cmbLongField"
                        AutoPostBack="true" Width="500px" OnSelectedIndexChanged="ddlSubInstrCategory_Selectedindexchanged">
                        <%-- <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="NCD">NCD</asp:ListItem>
                <asp:ListItem Value="IB">Infrastructure bonds</asp:ListItem>--%>
                    </asp:DropDownList>
                    <%--    <span id="Span4" class="spnRequiredField">*</span>--%>
                    <asp:Label ID="lblcategoryerror" runat="server" Text="*" Visible="true" CssClass="Error"></asp:Label><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddlSubInstrCategory" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlInstrCat" runat="server" CssClass="cmbLongField" Width="500px"
                        Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1Name" runat="server" Text="Issue Name:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span12" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Enter Name"
                        CssClass="rfvPCG" ControlToValidate="txtName" ValidationGroup="SetUpSubmit" Display="Dynamic"
                        InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1Issuer" runat="server" Text="Issuer:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlIssuer" runat="server" CssClass="cmbLongField" AutoPostBack="true"
                        Width="500px">
                    </asp:DropDownList>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <asp:ImageButton ID="imgIssuer" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Issuer" OnClick="btnIssuerPopUp_Click"
                        Height="15px" Width="15px" Visible="false"></asp:ImageButton>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Issuer"
                        CssClass="rfvPCG" ControlToValidate="ddlIssuer" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trRange">
                <td class="leftLabel">
                    <asp:Label ID="lb1ActiveFormRange" runat="server" Text="Form No-Starting Series No:"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtFormRange" runat="server" CssClass="txtField" Width="200px" MaxLength="10"></asp:TextBox>
                    <span id="Span13" class="spnRequiredField">*</span>
                    <br />
                    <%-- <asp:RangeValidator ID="regtxtFormRange" runat="server" Type="Integer" MinimumValue="7"
                        CssClass="rfvPCG" MaximumValue="9" ControlToValidate="txtFormRange" ErrorMessage="you can not enter less than 8 or greate than 10 digit"
                        ValidationGroup="SetUpSubmit"></asp:RangeValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Please Enter FromRange"
                        CssClass="rfvPCG" ControlToValidate="txtFormRange" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator11" ControlToValidate="txtFormRange"
                        runat="server" Display="Dynamic" ErrorMessage="<br/>Enter Numeric Value Between 8-10 Digit"
                        CssClass="cvPCG" ValidationExpression="[0-9]{8,10}$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator2" ControlToValidate="txtFormRange" runat="server"
                        ControlToCompare="txtToRange" Display="Dynamic" ErrorMessage="<br/>From Range Less Than To Range"
                        Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1ToRange" runat="server" Text="Form No-Ending Series No:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtToRange" runat="server" CssClass="txtField" Width="200px" MaxLength="10"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <asp:ImageButton ID="ImageActivRange" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Active Range" OnClick="btnImageActivRange_Click"
                        Height="15px" Width="15px" Visible="false"></asp:ImageButton>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Enter ToRange"
                        CssClass="rfvPCG" ControlToValidate="txtToRange" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" ControlToValidate="txtToRange"
                        runat="server" Display="Dynamic" ErrorMessage="<br/>Enter Numeric Value Between 8-10 Digit"
                        CssClass="cvPCG" ValidationExpression="[0-9]{8,10}$" ValidationGroup="SetUpSubmit" />
                    <asp:CompareValidator ID="CompareValidator3" ControlToValidate="txtToRange" runat="server"
                        ControlToCompare="txtFormRange" Display="Dynamic" ErrorMessage="<br/>To Range Greater Than From Range"
                        Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                    <%-- <asp:RangeValidator ID="RantxtToRange" runat="server" Type="Integer" MinimumValue="7"
                        CssClass="rfvPCG" MaximumValue="9" ControlToValidate="txtToRange" ErrorMessage="you can not enter less than 8 or greate than 10 digit"
                        ValidationGroup="SetUpSubmit"></asp:RangeValidator>--%>
                </td>
            </tr>
            <tr id="trIssueTypes" runat="server">
                <td class="leftLabel">
                    <asp:Label ID="Label6" runat="server" Text="Issue Type:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlIssueType_Selectedindexchanged"
                        AutoPostBack="true" Width="205px">
                        <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                        <asp:ListItem Text="Book Building" Value="BookBuilding"></asp:ListItem>
                        <asp:ListItem Text="FixedPrice" Value="FixedPrice"></asp:ListItem>
                    </asp:DropDownList>
                    <%--  <span id="Span21" class="spnRequiredField">*</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ErrorMessage="Please Enter FromRange"
                CssClass="rfvPCG" ControlToValidate="ddlIssueType" ValidationGroup="SetUpSubmit"
                Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr id="trBookBuildingAndCapprices" runat="server">
                <td class="leftLabel" id="tdBookBuilding" runat="server">
                    <asp:Label ID="Label7" runat="server" Text="Book Building(%):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtBookBuildingPer" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ErrorMessage="Please Enter BookBuilding Percentage"
                        CssClass="rfvPCG" ControlToValidate="txtBookBuildingPer" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="Label9" runat="server" Text="Cap Price:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtCapPrice" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span28" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="Please Enter Cap Price"
                        CssClass="rfvPCG" ControlToValidate="txtCapPrice" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator7" ControlToValidate="txtCapPrice" ControlToCompare="txtFloorPrice"
                        runat="server" Display="Dynamic" ErrorMessage="Cap price greater than floor price"
                        Type="Integer" Operator="GreaterThan" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trFloorAndFixedPrices" runat="server">
                <td id="tdLbFloorPrice" runat="server" class="leftLabel">
                    <asp:Label ID="Label11" runat="server" Text="Floor Price:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdTxtFloorPrice" runat="server" class="rightData">
                    <asp:TextBox ID="txtFloorPrice" runat="server" CssClass="txtField" Width="200px"
                        MaxLength="10"></asp:TextBox>
                    <span id="Span27" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ErrorMessage="Please Enter Floor Price"
                        CssClass="rfvPCG" ControlToValidate="txtFloorPrice" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <%-- <asp:RangeValidator ID="RangeValidator5" Display="Dynamic" ValidationGroup="SetUpSubmit"
                runat="server" ErrorMessage="Date of Floor Price between 1 to 999999999" ControlToValidate="txtFloorPrice"
                MaximumValue="999999999" MinimumValue="1" Type="Integer" CssClass="cvPCG"></asp:RangeValidator>   --%>
                    <asp:CompareValidator ID="ComptxtFloorPrice" ControlToValidate="txtFloorPrice" ControlToCompare="txtCapPrice"
                        runat="server" Display="Dynamic" ErrorMessage="Floor price less than cap price"
                        Type="Integer" Operator="LessThan" CssClass="cvPCG"></asp:CompareValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" ControlToValidate="txtFloorPrice"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td id="tdLbFixedPrice" runat="server" class="leftLabel">
                    <asp:Label ID="Label10" runat="server" Text="Fixed Price:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtFixedPrice" runat="server" class="rightData">
                    <asp:TextBox ID="txtFixedPrice" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span29" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ErrorMessage="Please Enter Fixed Price"
                        CssClass="rfvPCG" ControlToValidate="txtFixedPrice" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtFixedPrice"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trSyndicateAndMemberCodes" runat="server">
                <td id="Td1" runat="server" class="leftLabel">
                    <asp:Label ID="Label8" runat="server" Text="Syndicate Member:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="Td2" runat="server" class="rightData">
                    <asp:TextBox ID="txtSyndicateMemberCode" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
                <td id="Td3" runat="server" class="leftLabel">
                    <asp:Label ID="Label12" runat="server" Text="Broker:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="Td4" runat="server" class="rightData">
                    <asp:TextBox ID="txtBrokerCode" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr id="trRegistrarAndNoofBidsAlloweds" runat="server">
                <td id="Td5" runat="server" class="leftLabel">
                    <asp:Label ID="Label13" runat="server" Text="Registrar:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="Td6" runat="server" class="rightData">
                    <asp:DropDownList ID="ddlRegistrar" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px">
                    </asp:DropDownList>
                    <span id="Span34" class="spnRequiredField">*</span>
                    <asp:ImageButton ID="ImageddlRegistrar" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Registrar" OnClick="ImageddlRegistrar_Click"
                        Height="15px" Width="15px"></asp:ImageButton>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ErrorMessage="Please Enter FromRange"
                        CssClass="rfvPCG" ControlToValidate="ddlRegistrar" ValidationGroup="SetUpSubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    <%--<asp:TextBox ID="txtRegistrar" runat="server" CssClass="txtField"></asp:TextBox>--%>
                </td>
                <td id="Td7" runat="server" class="leftLabel">
                    <asp:Label ID="Label14" runat="server" Text="Bids Allowed(Per App.):" CssClass="FieldName"></asp:Label>
                </td>
                <td id="Td8" runat="server" class="rightData">
                    <asp:TextBox ID="txtNoOfBids" runat="server" CssClass="txtField" Width="200px" Text="3"
                        Enabled="false"></asp:TextBox>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" ControlToValidate="txtNoOfBids"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Type" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trRegistrarAddressAndTelNo" runat="server">
                <td id="tdlb1RegistrarAddress" runat="server" class="leftLabel">
                    <asp:Label ID="lb1RegistrarAddress" runat="server" Text="Reg. Address:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtRegistrarAddress" runat="server" class="rightData">
                    <asp:TextBox ID="txtRegistrarAddress" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <br />
                    <%--<asp:TextBox ID="txtRegistrar" runat="server" CssClass="txtField"></asp:TextBox>--%>
                </td>
                <td id="tdlb1RegistrarTelNO" runat="server" class="leftLabel">
                    <asp:Label ID="lb1RegistrarTelNO" runat="server" Text="Reg. TelNo:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtRegistrarTelNO" runat="server" class="rightData">
                    <asp:TextBox ID="txtRegistrarTelNO" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr id="trRegistrarFaxNoAndInvestorGrievenceEmail" runat="server">
                <td id="tdlb1RegistrarFaxNo" runat="server" class="leftLabel">
                    <asp:Label ID="lb1RegistrarFaxNo" runat="server" Text="Reg. FaxNo:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtRegistrarFaxNo" runat="server" class="rightData">
                    <asp:TextBox ID="txtRegistrarFaxNo" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <br />
                    <%--<asp:TextBox ID="txtRegistrar" runat="server" CssClass="txtField"></asp:TextBox>--%>
                </td>
                <td id="tdlb1InvestorGrievenceEmail" runat="server" class="leftLabel">
                    <asp:Label ID="lb1InvestorGrievenceEmail" runat="server" Text="Investor Grievance-Email:"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtInvestorGrievenceEmail" runat="server" class="rightData">
                    <asp:TextBox ID="txtInvestorGrievenceEmail" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr id="trWebsiteAndContactPerson" runat="server">
                <td id="tdlb1Website" runat="server" class="leftLabel">
                    <asp:Label ID="lb1Website" runat="server" Text="Website:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtWebsite" runat="server" class="rightData">
                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <br />
                    <%--<asp:TextBox ID="txtRegistrar" runat="server" CssClass="txtField"></asp:TextBox>--%>
                </td>
                <td id="tdlb1ContactPerson" runat="server" class="leftLabel">
                    <asp:Label ID="lb1ContactPerson" runat="server" Text="ContactPerson:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtContactPerson" runat="server" class="rightData">
                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr id="trSBIRegistationNoAndISINNumber" runat="server">
                <td id="tdlb1SBIRegistationNo" runat="server" class="leftLabel">
                    <asp:Label ID="lb1SBIRegistationNo" runat="server" Text="SBI RegistationNo.:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdtxtSBIRegistationNo" runat="server" class="rightData">
                    <asp:TextBox ID="txtSBIRegistationNo" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <br />
                    <%--<asp:TextBox ID="txtRegistrar" runat="server" CssClass="txtField"></asp:TextBox>--%>
                </td>
                <td id="td1lb1ISINNo" runat="server" class="leftLabel">
                    <asp:Label ID="lb1ISINNo" runat="server" Text="ISIN:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="td1txtISINNo" runat="server" class="rightData">
                    <asp:TextBox ID="txtISINNo" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1InitialCqNo" runat="server" Text="Initial Cheque Number:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtInitialCqNo" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <%-- <span id="Span36" class="spnRequiredField">*</span>--%>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" ControlToValidate="txtInitialCqNo"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1FaceValue" runat="server" Text="Face Value:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtFaceValue" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Face Value" Display="Dynamic" ControlToValidate="txtFaceValue"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtFaceValue"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                    <%--  "[1-9]\d+(\.\d{1,2})?$"--%>
                    <%--<asp:CompareValidator ID="CompareValidator9" ControlToValidate="txtFaceValue" runat="server"
                Display="Dynamic" ErrorMessage="<br />Please enter a numeric value" Type="double"
                Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                <asp:RangeValidator ID="RangeValidator10" Display="Dynamic" ValidationGroup="SetUpSubmit"
                runat="server" ErrorMessage="<br />Please enter a numeric value" ControlToValidate="txtFaceValue"
                MaximumValue="2147483647" MinimumValue="0" Type="Double" CssClass="cvPCG"></asp:RangeValidator>--%>
                </td>
            </tr>
            <tr id="trModeofIssue" runat="server">
                <td class="leftLabel" runat="server" visible="false">
                    <asp:Label ID="lb1Price" runat="server" Text="Price (floor and fixed):" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" runat="server" visible="false">
                    <asp:TextBox ID="txtPrice" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span17" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Price" Display="Dynamic" ControlToValidate="txtPrice"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtPrice" runat="server"
                        Display="Dynamic" ErrorMessage="Please enter a numeric value" Type="Double" Operator="DataTypeCheck"
                        CssClass="cvPCG"></asp:CompareValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1ddlModeofIssue" runat="server" Text="Mode of Issue:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlModeofIssue" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
                        <asp:ListItem Value="Online">Online</asp:ListItem>
                        <asp:ListItem Value="Offline">Offline</asp:ListItem>
                        <%--       <asp:ListItem Value="Both">Both</asp:ListItem>--%>
                    </asp:DropDownList>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select Mode of Issue" Display="Dynamic" ControlToValidate="ddlModeofIssue"
                        InitialValue="Select" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr id="trRatingAndModeofTrading" runat="server">
                <td class="leftLabel" id="tdlb1Rating">
                    <asp:Label ID="lb1Rating" runat="server" Text="Rating:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" id="tdtxtRating">
                    <asp:TextBox ID="txtRating" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
                <td class="leftLabel" id="tdlb1ModeofTrading">
                    <asp:Label ID="lb1ModeOfTrading" runat="server" Text="Mode Of Trading:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" id="tdtxtModeofTrading">
                    <asp:DropDownList ID="ddlModeOfTrading" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px">
                        <asp:ListItem Value="Select">Select</asp:ListItem>
                        <asp:ListItem Value="Online">Online</asp:ListItem>
                        <asp:ListItem Value="Offline">Offline</asp:ListItem>
                        <%--<asp:ListItem Value="Both">Both</asp:ListItem>--%>
                    </asp:DropDownList>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Select Mode Of Trading" Display="Dynamic" ControlToValidate="ddlModeOfTrading"
                        InitialValue="Select" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1Opendate" runat="server" Text="Open Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="txtOpenDate" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        TabIndex="17" Width="206px" AutoPostBack="true">
                        <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                            skin="Telerik" enableembeddedskins="false">
                        </calendar>
                        <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                        <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                        </dateinput>
                    </telerik:RadDatePicker>
                    <span id="Span18" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Open Date" Display="Dynamic" ControlToValidate="txtOpenDate"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator4" ControlToValidate="txtOpenDate" runat="server"
                        ControlToCompare="txtCloseDate" Display="Dynamic" ErrorMessage="<br/>From Date be Less Than To Range"
                        Type="Date" Operator="LessThanEqual" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1CloseDate" runat="server" Text="Close Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="txtCloseDate" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        Width="206px" TabIndex="17">
                        <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                            skin="Telerik" enableembeddedskins="false">
                        </calendar>
                        <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                        <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                        </dateinput>
                    </telerik:RadDatePicker>
                    <span id="Span19" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Close Date" Display="Dynamic" ControlToValidate="txtCloseDate"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator5" ControlToValidate="txtCloseDate" runat="server"
                        ControlToCompare="txtOpenDate" Display="Dynamic" ErrorMessage="<br/>From Date be Less Than To Range"
                        Type="Date" Operator="GreaterThanEqual" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1OpenTime" runat="server" Text="Open Time:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <%-- <telerik:RadTimePicker ID="txtOpenTime" runat="server" ZIndex="30001" TimeView-TimeFormat="HH:mm"
                Width="200px">
            </telerik:RadTimePicker>--%>
                    <asp:DropDownList ID="ddlOpenTimeHours" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlOpenTimeMinutes" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlOpenTimeSeconds" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <%-- <asp:TextBox ID="txtOpenTimes" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>--%>
                    <asp:Label ID="lblSpan20" class="spnRequiredField" runat="server">*</asp:Label>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Time" Display="Dynamic" ControlToValidate="ddlOpenTimeHours"
                        InitialValue="HH" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Minutes" Display="Dynamic" ControlToValidate="ddlOpenTimeMinutes"
                        InitialValue="MM" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Seconds" Display="Dynamic" ControlToValidate="ddlOpenTimeSeconds"
                        InitialValue="SS" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <%--<asp:CompareValidator ID="CompareValidator6" ControlToValidate="ddlOpenTimeHours"
                runat="server" ControlToCompare="ddlCloseTimeHours" Display="Dynamic" ErrorMessage="<br/>Open Time be Greater Than Close Time"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>--%>
                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter Time" Display="Dynamic" ControlToValidate="ddlOpenTimeMinutes"
                InitialValue="MM" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter Time" Display="Dynamic" ControlToValidate="ddlOpenTimeSeconds"
                InitialValue="SS" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>--%>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1Closetime" runat="server" Text="Close Time:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlCloseTimeHours" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlCloseTimeMinutes" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlCloseTimeSeconds" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:Label ID="lblSpan35" runat="server" class="spnRequiredField">*</asp:Label>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter  Time" Display="Dynamic" ControlToValidate="ddlCloseTimeHours"
                        InitialValue="HH" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Minutes" Display="Dynamic" ControlToValidate="ddlCloseTimeMinutes"
                        InitialValue="MM" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Seconds" Display="Dynamic" ControlToValidate="ddlCloseTimeSeconds"
                        InitialValue="SS" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <%-- <asp:CompareValidator ID="CompareValidator7" ControlToValidate="ddlCloseTimeHours"
                runat="server" ControlToCompare="ddlOpenTimeHours" Display="Dynamic" ErrorMessage="<br/>Close Time be Less Than Open Time"
                Type="Integer" Operator="GreaterThanEqual" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>--%>
                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter  Time" Display="Dynamic" ControlToValidate="ddlCloseTimeMinutes"
                InitialValue="MM" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter  Time" Display="Dynamic" ControlToValidate="ddlCloseTimeSeconds"
                InitialValue="SS" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="leftLabel" id="tdLabel21">
                    <asp:Label ID="Label21" runat="server" Text="Online Cut-Off Time:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" id="tdcuttoffonline">
                    <asp:DropDownList ID="ddlCutOffTimeHours" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlCutOffTimeMinutes" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlCutOffTimeSeconds" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <span id="Span36" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Time" Display="Dynamic" ControlToValidate="ddlCutOffTimeHours"
                        InitialValue="HH" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Minutes" Display="Dynamic" ControlToValidate="ddlCutOffTimeMinutes"
                        InitialValue="MM" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Seconds" Display="Dynamic" ControlToValidate="ddlCutOffTimeSeconds"
                        InitialValue="SS" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator6" ControlToValidate="ddlCutOffTimeHours"
                        runat="server" ControlToCompare="ddlCloseTimeHours" Display="Dynamic" ErrorMessage="<br/>Cut-Off TIme Should be Less Than Close Time"
                        Type="Integer" Operator="LessThanEqual" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
                <td class="leftLabel" id="tdLabel24">
                    <asp:Label ID="Label24" runat="server" Text="Offline Cut-Off Time:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" id="tdcuttoffonffine">
                    <asp:DropDownList ID="ddlOffCutOffTimeHours" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="64px" />
                    <asp:DropDownList ID="ddlOffCutOffTimeMinutes" runat="server" CssClass="cmbField"
                        AutoPostBack="true" Width="64px" />
                    <asp:DropDownList ID="ddlOffCutOffTimeSeconds" runat="server" CssClass="cmbField"
                        AutoPostBack="true" Width="64px" />
                    <span id="Span44" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator54" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Time" Display="Dynamic" ControlToValidate="ddlOffCutOffTimeHours"
                        InitialValue="HH" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator55" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Minutes" Display="Dynamic" ControlToValidate="ddlOffCutOffTimeMinutes"
                        InitialValue="MM" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator56" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Seconds" Display="Dynamic" ControlToValidate="ddlOffCutOffTimeSeconds"
                        InitialValue="SS" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator11" ControlToValidate="ddlOffCutOffTimeHours"
                        runat="server" ControlToCompare="ddlCloseTimeHours" Display="Dynamic" ErrorMessage="<br/>Cut-Off TIme Should be Less Than Close Time"
                        Type="Integer" Operator="LessThanEqual" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
            </tr>
            <%-- <tr>
        <td class="leftLabel">
            <asp:Label ID="lb1RevisionDate" runat="server" Text="Revision Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <telerik:RadDatePicker ID="txtRevisionDates" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                Width="200px" TabIndex="17">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
        </td>
        <td class="leftLabel">
            <asp:Label ID="Label18" runat="server" Text="Allotment Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightData">
            <telerik:RadDatePicker ID="txtAllotmentDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                Width="200px" TabIndex="17">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
        </td>
    </tr>--%>
            <tr id="trIssueqtySize" runat="server">
                <td class="leftLabel">
                    <asp:Label ID="Label16" runat="server" Text="Issue Size Qty:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtIssueSizeQty" runat="server" CssClass="txtField" Width="200px"
                        MaxLength="9" onblur="Calculate();"></asp:TextBox>
                    <%-- <span id="Span31" class="spnRequiredField">*</span>--%>
                    <br />
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter Issue Size Qty" Display="Dynamic" ControlToValidate="txtIssueSizeQty"
                InitialValue="" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtIssueSizeQty"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="Label17" runat="server" Text="Issue Size Amt:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtIssueSizeAmt" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <%--<span id="Span33" class="spnRequiredField">*</span>--%>
                    <br />
                    <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Enter Issue Size Amt" Display="Dynamic" ControlToValidate="txtIssueSizeAmt"
                InitialValue="" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtIssueSizeAmt"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Type" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr id="trTradinglotBidding" runat="server">
                <td class="leftLabel">
                    <asp:Label ID="lb1TradingLot" runat="server" Text="Trading Lot:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtTradingLot" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span22" class="spnRequiredField">*</span>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ControlToValidate="txtTradingLot"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit">                     
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Trading Lot" Display="Dynamic" ControlToValidate="txtTradingLot"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1BiddingLot" runat="server" Text="Bidding Lot:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtBiddingLot" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Bidding Lot" Display="Dynamic" ControlToValidate="txtBiddingLot"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ControlToValidate="txtBiddingLot"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit">                     
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr runat="server" id="trAmount" visible="true" >
                <td class="leftLabel">
                    <asp:Label ID="lblMinAmt" runat="server" Text="Min Amount:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtMinAmt" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span30" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Min. Amt." Display="Dynamic" ControlToValidate="txtMinAmt"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator19" ControlToValidate="txtMinAmt"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="txtMinAmt">     
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator9" ControlToValidate="txtMinAmt" runat="server"
                        ControlToCompare="txtMaxAmt" Display="Dynamic" ErrorMessage="<br/>Min. Amount be Less Than Max. Amount"
                        Type="Double" Operator="LessThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lblMaxAmt" runat="server" Text="Max Amount:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="td9" runat="server" class="rightData">
                    <asp:TextBox ID="txtMaxAmt" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <asp:Label ID="Label26" runat="server" CssClass="spnRequiredField" Text="*"></asp:Label></span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator49" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Max. Amt." Display="Dynamic" ControlToValidate="txtMaxAmt"
                        InitialValue="" ValidationGroup="SetUpSubmit" Visible="true">
                    </asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator20" ControlToValidate="txtMaxAmt"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="^[1-9]\d*(\.\d+)?$" ValidationGroup="txtMaxAmt">     
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator12" ControlToValidate="txtMaxAmt" runat="server"
                        ControlToCompare="txtMinAmt" Display="Dynamic" ErrorMessage="<br/>Max Amount be Greater Than Min. Amount"
                        Type="Double" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"
                        Visible="true"></asp:CompareValidator>
                </td>
            </tr>
            <tr runat="server" id="trMinQty" visible="true">
                <td class="leftLabel" visible="false" id="tdlb1MinQty">
                    <asp:Label ID="lb1MinApplicationsize" runat="server" Text="Min Qty:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" visible="false" id="tdltxtMinQty">
                    <asp:TextBox ID="txtMinAplicSize" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <span id="Span23" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Min. Qty." Display="Dynamic" ControlToValidate="txtMinAplicSize"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" ControlToValidate="txtMinAplicSize"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="txtMinAplicSize">     
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator10" ControlToValidate="txtMinAplicSize"
                        runat="server" ControlToCompare="txtMaxQty" Display="Dynamic" ErrorMessage="<br/>Min. Qty. be Less Than Max Qty"
                        Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblSubBrokerCode" runat="server" Text="Sub Broker Code:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSubBrokerCode" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trMaxQty">
                <td class="leftLabel">
                    <asp:Label ID="Label15" runat="server" Text="Max Qty:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdmaxqty" runat="server">
                    <asp:TextBox ID="txtMaxQty" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <asp:Label ID="lblMaxError" runat="server" CssClass="spnRequiredField" Text="*"></asp:Label></span>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ControlToValidate="txtMaxQty"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Integer Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit" Visible="false">                     
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Max. Qty." Display="Dynamic" ControlToValidate="txtMaxQty"
                        InitialValue="" ValidationGroup="SetUpSubmit" Visible="false">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator8" ControlToValidate="txtMaxQty" runat="server"
                        ControlToCompare="txtMinAplicSize" Display="Dynamic" ErrorMessage="<br/>Max. Qty. be Greater Than Min. Qty."
                        Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"
                        Visible="false"></asp:CompareValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1IsPrefix" runat="server" Text="Is Prefix:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:CheckBox ID="chkIsPrefix" runat="server" CssClass="txtField" Text="" OnCheckedChanged="chkIsPrefix_changed"
                        AutoPostBack="true"></asp:CheckBox>
                    <asp:TextBox ID="txtIsPrefix" runat="server" CssClass="txtField" Width="170px" Visible="false"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" ControlToValidate="txtIsPrefix"
                        runat="server" Display="Dynamic" ErrorMessage="<br/>Please Enter Integer Value"
                        CssClass="cvPCG" ValidationExpression="[0-9]\d*$" ValidationGroup="SetUpSubmit">                     
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1Trading" runat="server" Text="Multiples of:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData" id="tdTradingMultipleOF" runat="server">
                    <asp:TextBox ID="txtTradingInMultipleOf" runat="server" CssClass="txtField" Width="200px"
                        Text="1" Enabled="true"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField"></span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please Enter Trading In Multiple Of" Display="Dynamic" ControlToValidate="txtTradingInMultipleOf"
                        InitialValue="" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ControlToValidate="txtTradingInMultipleOf"
                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Numeric Value" CssClass="cvPCG"
                        ValidationExpression="[1-9]\d*$" ValidationGroup="SetUpSubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td class="leftLabel">
                    <%--  <asp:Label ID="lb1ListedInExchange" runat="server" Text="Listed In Exchange:" CssClass="FieldName"></asp:Label>--%>
                </td>
                <td class="rightData">
                    <%-- <asp:DropDownList ID="ddlListedInExchange" runat="server" CssClass="cmbField" AutoPostBack="true"
                Width="205px" OnSelectedIndexChanged="ddlListedInExchange_SelectedIndexChanged">
                <asp:ListItem Value="Select">Select</asp:ListItem>
                <asp:ListItem Value="NSE">NSE</asp:ListItem>
                <asp:ListItem Value="BSE">BSE</asp:ListItem>
            </asp:DropDownList>--%>
                    <%-- <asp:TextBox ID="txtListedInExchange" runat="server" CssClass="txtField"></asp:TextBox>--%>
                </td>
            </tr>
            <tr class="leftLabel" visible="false" id="trExchangeCode">
                <td>
                    <asp:Label ID="Label19" runat="server" Text="NSE Code:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtNSECode" runat="server" CssClass="txtField" Width="200px" OnTextChanged="txtNSECode_OnTextChanged"
                        AutoPostBack="true"></asp:TextBox>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1Code" runat="server" Text="BSE Code:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtBSECode" runat="server" CssClass="txtField" Width="200px" OnTextChanged="txtBSECode_OnTextChanged"
                        AutoPostBack="true"></asp:TextBox>
                    <%--<span id="Span32" class="spnRequiredField">*</span>
            <br />
          <asp:RequiredFieldValidator ID="rfvtxtBSECode" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter BSE Code"
                Display="Dynamic" ControlToValidate="txtBSECode" InitialValue="" ValidationGroup="SetUpSubmit">
            </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td class="leftLabel">
                    <asp:Label ID="lb1BankName" runat="server" Text="Bank Name:" CssClass="FieldName"></asp:Label>
                    <asp:Label ID="lblAssetsApplication" runat="server" CssClass="FieldName" Text="Application Deposit Bank:"
                        Visible="false"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtBankName" runat="server" CssClass="txtField" Width="200px" Visible="false"></asp:TextBox>
                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px">
                    </asp:DropDownList>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="lb1BankBranch" runat="server" Text="Bank Branch:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtBankBranch" runat="server" CssClass="txtField" Width="200px"></asp:TextBox>
                    <asp:DropDownList ID="ddlBankBranch" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px" Visible="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trRevisionDate">
                <td class="leftLabel">
                    <asp:Label ID="lb1RevisionDate" runat="server" Text="Revision Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="txtRevisionDates" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        Width="208px" TabIndex="17">
                        <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                            skin="Telerik" enableembeddedskins="false">
                        </calendar>
                        <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                        <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                        </dateinput>
                    </telerik:RadDatePicker>
                    <asp:CompareValidator ID="CmptxtRevisionDates1" ControlToValidate="txtRevisionDates"
                        runat="server" ControlToCompare="txtCloseDate" Display="Dynamic" ErrorMessage="<br/>Revision Date between open date and close date"
                        Type="Date" Operator="LessThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                    <asp:CompareValidator ID="CmptxtRevisionDates" ControlToValidate="txtRevisionDates"
                        runat="server" ControlToCompare="txtOpenDate" Display="Dynamic" ErrorMessage="<br/>Revision Date between open date and close date"
                        Type="Date" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
                <td class="leftLabel">
                    <asp:Label ID="Label18" runat="server" Text="Allotment Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <telerik:RadDatePicker ID="txtAllotmentDate" CssClass="txtField" runat="server" Culture="English (United States)"
                        Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                        Width="208px" TabIndex="17">
                        <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                            skin="Telerik" enableembeddedskins="false">
                        </calendar>
                        <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                        <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                        </dateinput>
                    </telerik:RadDatePicker>
                    <asp:CompareValidator ID="cmp" ControlToValidate="txtAllotmentDate" runat="server"
                        ControlToCompare="txtCloseDate" Display="Dynamic" ErrorMessage="<br/>Allotment Date Should Be Greater Than Close Date"
                        Type="Date" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>
                </td>
            </tr>
            <tr id="trlblSyndicatet" runat="server">
                <td align="right" id="tdlblSyndicatet" runat="server" visible="false" >
                    <asp:Label ID="lblSyndicatet" runat="server" Text="Syndicate:" CssClass="FieldName"></asp:Label>
                </td>
                <td id="tdddllblSyndicatet" runat="server" visible="false" >
                    <asp:DropDownList ID="ddllblSyndicatet" runat="server" CssClass="cmbField" Width="165px">
                    </asp:DropDownList>
                    <span id="Span40" class="spnRequiredField">*</span>
                    <asp:ImageButton ID="ImageddlSyndicate" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Syndicate" OnClick="ImageddlSyndicate_Click"
                        Height="15px" Width="15px"></asp:ImageButton>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator48" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select syndicate" Display="Dynamic" ControlToValidate="ddllblSyndicatet"
                        InitialValue="0" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblBrokerCode" runat="server" CssClass="FieldName" Text="Broker:"></asp:Label>
                </td>
                <td id="tdBroker" runat="server">
                    <%--<asp:DropDownList ID="ddlBrokerCode" runat="server" CssClass="cmbField" Width="165px">
                    </asp:DropDownList>--%>
                    <asp:LinkButton ID="lbBrokerCode" runat="server" Text="Click To Select Broker" OnClick="lbBrokerCode_OnClick"
                        CssClass="LinkButtons"></asp:LinkButton>
                    <span id="Span41" class="spnRequiredField">*</span>
                    <asp:ImageButton ID="ImagddlBrokerCode" ImageUrl="~/App_Themes/Maroon/Images/user_add.png"
                        AlternateText="Add" runat="server" ToolTip="Click here to Add Broker" OnClick="ImagddlBrokerCode_Click"
                        Height="15px" Width="15px"></asp:ImageButton>
                    <br />
                    <asp:Label ID="lblBrokerIds" runat="server" CssClass="FieldName"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <%-- <td class="leftLabel">
          
        </td>--%>
                <td class="leftLabel">
                    <asp:Label ID="lblChnl" runat="server" Text="Business Channel:" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightData">
                    <asp:DropDownList ID="ddlBssChnl" runat="server" CssClass="cmbField" AutoPostBack="true"
                        Width="205px">
                    </asp:DropDownList>
                    <span id="Span42" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator50" runat="server" CssClass="rfvPCG"
                        ErrorMessage="Please select Business Channel" Display="Dynamic" ControlToValidate="ddlBssChnl"
                        InitialValue="0" ValidationGroup="SetUpSubmit">
                    </asp:RequiredFieldValidator>
                </td>
                <td class="leftLabel" colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr id="trMultipleApplicationAllowed" runat="server">
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkMultipleApplicationAllowed" runat="server" CssClass="txtField"
                        Text="Multiple applications allowed" OnCheckedChanged="chkMultipleApplicationAllowed_OnCheckedChanged"
                        AutoPostBack="true"></asp:CheckBox>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please check multiple applications allowed"
                        ClientValidationFunction="CheckBoxes" EnableClientScript="true" Display="Dynamic"
                        ValidationGroup="SetUpSubmit" CssClass="rfvPCG">
                    </asp:CustomValidator>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkMultipleApplicationNotAllowed" runat="server" CssClass="txtField"
                        Text="Multiple applications not allowed" OnCheckedChanged="chkMultipleApplicationNotAllowed_OnCheckedChanged"
                        AutoPostBack="true"></asp:CheckBox>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="<br>Please check multiple applications not allowed"
                        ClientValidationFunction="CheckBoxes" EnableClientScript="true" Display="Dynamic"
                        ValidationGroup="SetUpSubmit" CssClass="rfvPCG">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr id="trIsCancelAllowed" runat="server">
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkIScancelAllowed" runat="server" CssClass="txtField" Text="Cancellation allowed"
                        OnCheckedChanged="chkIScancelAllowed_OnCheckedChanged" AutoPostBack="true"></asp:CheckBox>
                    <asp:CustomValidator ID="custIScancelAllowed" runat="server" ErrorMessage="<br>Please check cancellation allowed"
                        ClientValidationFunction="ChkIscancelallowedornot" EnableClientScript="true"
                        Display="Dynamic" ValidationGroup="SetUpSubmit" CssClass="rfvPCG">
                    </asp:CustomValidator>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkIsCancelNotAllowed" runat="server" CssClass="txtField" Text="Cancellation is not allowed"
                        OnCheckedChanged="IsCancelNotAllowed_OnCheckedChanged" AutoPostBack="true"></asp:CheckBox>
                    <asp:CustomValidator ID="custIScancelnotAllowed" runat="server" ErrorMessage="<br>Please check cancellation is not allowed"
                        ClientValidationFunction="ChkIscancelallowedornot" EnableClientScript="true"
                        Display="Dynamic" ValidationGroup="SetUpSubmit" CssClass="rfvPCG">
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr id="trIsActiveandPutCallOption" runat="server">
                <td class="leftLabel">
                    &nbsp;
                </td>
                <td class="rightData">
                    <asp:CheckBox ID="chkIsActive" runat="server" CssClass="txtField" Text="Activate"
                        OnCheckedChanged="chkOnlineEnablement_changed" AutoPostBack="true"></asp:CheckBox>
                    <%--<asp:RequiredFieldValidator ID="rfvIsActive" ControlToValidate="chkIsActive"
                  ErrorMessage="Please check" Display="Dynamic" runat="server"
                  CssClass="rfvPCG" ValidationGroup="SetUpSubmit"></asp:RequiredFieldValidator>--%>
                    <%-- <asp:RequiredFieldValidator ID="rfvchkIsActive" CssClass="rfvPCG"
                    ErrorMessage="Please Check" Display="Dynamic" ControlToValidate="chkIsActive"
                    ValidationGroup="SetUpSubmit">
                </asp:RequiredFieldValidator>--%>
                </td>
                <%--<td class="leftLabel">
        <asp:RequiredFieldValidator ID="rfvtxtMM" ControlToValidate="txtMM"
                  ErrorMessage="<br />Please enter min" Display="Dynamic" runat="server"
                  CssClass="rfvPCG" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>
            <asp:Label ID="lb1PutCallOption" runat="server" Text="Put Call Option:" CssClass="FieldName"></asp:Label>
        </td>--%>
                <td>
                </td>
                <td class="rightData">
                    <asp:CheckBox ID="chkPutCallOption" runat="server" CssClass="txtField" Text="Put Call Option">
                    </asp:CheckBox>
                    <%--<asp:TextBox ID="txtPutCallOption" runat="server" CssClass="txtField" Width="205px"></asp:TextBox>--%>
                    <%--<asp:CompareValidator ID="CompareValidator4" ControlToValidate="txtCloseDate" runat="server"
                Display="Dynamic" ErrorMessage="<br />Please enter a Put Call Option" Type="Date"
                Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>--%>
                </td>
            </tr>
            <tr id="trNomineeReQuired" runat="server">
                <td class="leftLabel">
                    &nbsp;
                </td>
                <td class="rightData">
                    <asp:CheckBox ID="chkNomineeReQuired" runat="server" CssClass="txtField" Text="Nominee Required"
                        Visible="false"></asp:CheckBox>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkTradebleExchange" runat="server" CssClass="txtField" Text="Exchange Tradable" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr id="trBtnSubmit" runat="server">
                <td class="leftLabel">
                    &nbsp;
                </td>
                <td class="leftLabel">
                    <asp:Button ID="btnSetUpSubmit" runat="server" Text="Submit" CssClass="PCGButton"
                        ValidationGroup="SetUpSubmit" OnClick="btnSetUpSubmit_Click" Visible="false" />
                    <%-- </td>
        <td class="rightData">--%>
                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="PCGButton" ValidationGroup="SetUpSubmit"
                        OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnProspect" runat="server" Text="Prospectus" CssClass="PCGButton"
                        OnClick="btnProspect_Click" />
                    <%-- ValidationGroup="SetUpSubmit"--%>
                </td>
                <td class="rightData">
                    &nbsp;
                </td>
            </tr>
            <tr id="trIssueId" visible="false">
                <td class="leftLabel">
                    &nbsp;
                </td>
                <td class="rightData">
                    <asp:TextBox ID="txtIssueId" runat="server" CssClass="txtField" Visible="false"></asp:TextBox>
                </td>
                <td class="leftLabel" colspan="3">
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlCategory" runat="server" CssClass="Landscape" Width="100%">
            <table id="Table1" runat="server" width="80%">
                <tr>
                    <td class="leftLabel">
                        &nbsp;
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="rgEligibleInvestorCategories" runat="server" AllowSorting="True"
                                        enableloadondemand="True" PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                        GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" AllowPaging="false"
                                        ShowStatusBar="True" Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgEligibleInvestorCategories_OnNeedDataSource"
                                        OnItemCommand="rgEligibleInvestorCategories_ItemCommand" OnItemDataBound="rgEligibleInvestorCategories_ItemDataBound">
                                        <mastertableview allowmulticolumnsorting="True" allowsorting="true" datakeynames="AIM_IssueId,AIIC_InvestorCatgeoryId"
                                            autogeneratecolumns="false" width="100%" editmode="PopUp" commanditemsettings-addnewrecordtext="Create InvestorCategory"
                                            commanditemdisplay="Top">
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                            Font-Bold="true" UniqueName="DetailsCategorieslink" OnClick="btnCategoriesExpandAll_Click"
                                                            Font-Size="Medium">+</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                                    UpdateText="Update">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryName" HeaderStyle-Width="20px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Category Name" UniqueName="AIIC_InvestorCatgeoryName" SortExpression="AIIC_InvestorCatgeoryName"
                                                    AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIIC_ChequePayableTo" HeaderStyle-Width="200px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Cheque Payable To" UniqueName="AIIC_ChequePayableTo" SortExpression="AIIC_ChequePayableTo">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIIC_MInBidAmount" HeaderStyle-Width="200px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Min Bid Amount" UniqueName="AIIC_MInBidAmount" SortExpression="AIIC_MInBidAmount">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AIIC_MaxBidAmount" HeaderStyle-Width="200px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Max Bid Amount" UniqueName="AIIC_MaxBidAmount" SortExpression="AIIC_MaxBidAmount">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                    Text="Delete" Visible="false">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                </telerik:GridButtonColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="1.5%">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="3%">
                                                                <asp:Panel ID="pnlCategoriesDetailschild" runat="server" Style="display: inline"
                                                                    CssClass="Landscape" ScrollBars="Horizontal" Visible="false">
                                                                    <telerik:RadGrid ID="rgCategoriesDetails" runat="server" AutoGenerateColumns="False"
                                                                        enableloadondemand="True" PageSize="5" EnableEmbeddedSkins="False" GridLines="None"
                                                                        ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                                        AllowFilteringByColumn="true" OnNeedDataSource="rgCategoriesDetails_OnNeedDataSource"
                                                                        OnItemCommand="rgCategoriesDetails_ItemCommand" AllowPaging="false">
                                                                        <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIIC_InvestorCatgeoryId,AIICST_Id"
                                                                            AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="WCMV_Name" HeaderStyle-Width="30px" UniqueName="WCMV_Name"
                                                                                    CurrentFilterFunction="Contains" HeaderText="Investor Type" SortExpression="WCMV_Name"
                                                                                    AllowFiltering="true">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AIICST_InvestorSubTypeCode" HeaderStyle-Width="30px"
                                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                                    HeaderText="SubType Code" UniqueName="AIICST_InvestorSubTypeCode" SortExpression="AIICST_InvestorSubTypeCode">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AIICST_MinInvestmentAmount" HeaderStyle-Width="30px"
                                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                                    HeaderText="MinInvestment Amount" UniqueName="AIIC_MinInvestmentAmount" SortExpression="AIIC_MinInvestmentAmount">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AIICST_MaxInvestmentAmount" HeaderStyle-Width="30px"
                                                                                    HeaderText="MaxInvestment Amount" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                                    AutoPostBackOnFilter="true" UniqueName="AIIC_MaxInvestmentAmount" Visible="true">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                                                                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                                                    Text="Delete" Visible="false">
                                                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                                </telerik:GridButtonColumn>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                    </telerik:RadGrid>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template" PopUpSettings-Height="600px" PopUpSettings-Width="695px">
                                                <FormTemplate>
                                                    <table width="75%" cellspacing="2" cellpadding="2">
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Label ID="lb1IssueName" runat="server" Text="Issue Name:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData">
                                                                <asp:TextBox ID="txtIssueName" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span2" class="spnRequiredField">*</span>
                                                            </td>
                                                            <td colspan="2">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Craeate Issue" Display="Dynamic" ControlToValidate="txtIssueName"
                                                                    Enabled="false" ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Label ID="lb1CategoryName" runat="server" Text="Category Name:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span1" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Category Name" Display="Dynamic" ControlToValidate="txtCategoryName"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Label ID="Label1" runat="server" Text="Category Description:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:TextBox ID="txtCategoryDescription" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span4" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Category Description" Display="Dynamic" ControlToValidate="txtCategoryDescription"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Label ID="Label2" runat="server" Text="Cheque Payable To:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:TextBox ID="txtChequePayableTo" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span6" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter ChequePayableTo" Display="Dynamic" ControlToValidate="txtChequePayableTo"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Label ID="Label3" runat="server" Text="Min Bid Amount:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:TextBox ID="txtMinBidAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span7" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Min Bid Amount" Display="Dynamic" ControlToValidate="txtMinBidAmount"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" ControlToValidate="txtMinBidAmount"
                                                                    runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                    ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">   </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Label ID="Label4" runat="server" Text="Max Bid Amount:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:TextBox ID="txtMaxBidAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span8" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Max Bid Amount" Display="Dynamic" ControlToValidate="txtMaxBidAmount"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator16" ControlToValidate="txtMaxBidAmount"
                                                                    runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                    ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK"> 
                                                                </asp:RegularExpressionValidator>
                                                                <asp:CompareValidator ID="cmp" ControlToValidate="txtMaxBidAmount" runat="server"
                                                                    ControlToCompare="txtMinBidAmount" Display="Dynamic" ErrorMessage="<br/>MaxbidAmount  Should Be Greater Than Minbid"
                                                                    Type="Double" Operator="GreaterThan"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDiscountType" runat="server">
                                                            <td class="leftLabel">
                                                                <asp:Label ID="lb1DiscountType" runat="server" Text="Discount Type:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:DropDownList ID="ddlDiscountType" runat="server" CssClass="cmbField" AutoPostBack="true">
                                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                    <asp:ListItem Value="Per">Per(%)</asp:ListItem>
                                                                    <asp:ListItem Value="Amt">Amt</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span id="SpanDiscountType" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="rfvDiscountType" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Select Discount Type" Display="Dynamic" ControlToValidate="ddlDiscountType"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr id="trDiscountValue" runat="server">
                                                            <td class="leftLabel">
                                                                <asp:Label ID="lb1DiscountValue" runat="server" Text="Discount Value:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td class="rightData" colspan="2">
                                                                <asp:TextBox ID="txtDiscountValue" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="SpanDiscountValue" class="spnRequiredField">*</span>
                                                                <asp:RequiredFieldValidator ID="rfvDiscountValue" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Select Discount Value" Display="Dynamic" ControlToValidate="txtDiscountValue"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <telerik:RadGrid ID="rgSubCategories" runat="server" AllowSorting="True" enableloadondemand="True"
                                                                    PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                                                    GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                                                    Width="80%" Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgSubCategories_OnNeedDataSource"
                                                                    EnableViewState="true" OnItemDataBound="rgSubCategories_ItemDataBound" OnItemCommand="rgSubCategories_ItemCommand">
                                                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                                                        DataKeyNames="WCMV_LookupId">
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false"
                                                                                runat="server" UniqueName="chkBxSelect" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbSubCategories" runat="server" Checked="false" AutoPostBack="True"
                                                                                        OnCheckedChanged="cbSubCategories_changed" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <%-- DataField="AIICST_Id"--%>
                                                                            <%--<telerik:GridTemplateColumn HeaderText="Sub Category Id" ShowFilterIcon="false" 
                                                                        UniqueName="txtSubCategoryId" Visible="false" AllowFiltering="false">
                                                                        <HeaderTemplate>
                                                                             
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSubCategoryId" runat="server" CssClass="txtField" Width="10px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>--%>
                                                                            <telerik:GridTemplateColumn HeaderText="Sub Category id" ShowFilterIcon="false" AllowFiltering="false"
                                                                                UniqueName="SubCategoryId" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblSubCategoryId" runat="server" Text="Sub Category id"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSubCategoryId" runat="server" CssClass="txtField" Width="55px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Sub Category Code" ShowFilterIcon="false"
                                                                                AllowFiltering="false" UniqueName="SubCategoryCode">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblSubCategoryCode" runat="server" Text="Sub Category Code"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSubCategoryCode" runat="server" CssClass="txtField" Width="55px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Sub Category" ShowFilterIcon="false" AllowFiltering="false"
                                                                                runat="server" UniqueName="CustSubCategory">
                                                                                <ItemTemplate>
                                                                                    <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                                                                                        Width="205px" OnSelectedIndexChanged="ddlSubCategory_Selectedindexchanged">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <%-- <telerik:GridBoundColumn DataField="WCMV_Name" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Sub Category"
                                                                        UniqueName="WCMV_Name" SortExpression="WCMV_Name" AllowFiltering="true">
                                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                    </telerik:GridBoundColumn>--%>
                                                                            <telerik:GridBoundColumn DataField="WCMV_LookupId" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="LookupId" UniqueName="WCMV_LookupId"
                                                                                SortExpression="WCMV_LookupId" Visible="false">
                                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Min Investment Amount" ShowFilterIcon="false"
                                                                                AllowFiltering="false" UniqueName="MinInvestmentAmt">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblMinInvestmentAmount" runat="server" Text="Min Investment Amount"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtMinInvestmentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                                                    <span id="Spanamount" class="spnRequiredField">*</span>
                                                                                    <asp:CompareValidator ID="cmp" ControlToValidate="txtMinInvestmentAmount" runat="server"
                                                                                        ControlToCompare="txtMaxInvestmentAmount" Display="Dynamic" ErrorMessage="<br/>MinInvestmentAmt  Should Be less Than MaxInvestmentAmt "
                                                                                        Type="Double" Operator="LessThan"></asp:CompareValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatormin" ControlToValidate="txtMinInvestmentAmount"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">   </asp:RegularExpressionValidator>
                                                                                    <%--<asp:CompareValidator ID="cmpMinInvestmentAmt" Display="Dynamic" runat="server" Type="Integer"
                                                                                ControlToCompare="txtMinInvestmentAmount" Operator="LessThan" ControlToValidate="txtMaxInvestmentAmount"
                                                                                ErrorMessage="MinInvestmentAmount should be lessthan MaxInvestmentAmt"></asp:CompareValidator>
                                                                              "[0-9]\d*(\.\d?[0-9])?$"  "[0-9]\d*(\.\d?[0-9])?$"--%>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Max Investment Amount" ShowFilterIcon="false"
                                                                                UniqueName="MaxInvestmentAmt" AllowFiltering="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblMaxInvestmentAmount" runat="server" Text="Max Investment Amount"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtMaxInvestmentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                                                    <%--<asp:CompareValidator ID="cmptxtMaxInvestmentAmount" Display="Dynamic" runat="server" Type="Integer" ControlToCompare="txtMaxInvestmentAmount" 
                                                                            Operator="GreaterThan" ControlToValidate="txtMinInvestmentAmount" ErrorMessage="MinInvestmentAmount should be lessthan MaxInvestmentAmt"></asp:CompareValidator>--%>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidatormaa" ControlToValidate="txtMaxInvestmentAmount"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">   </asp:RegularExpressionValidator>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false"
                                                                                runat="server" Visible="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Remove"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbRemoveSubCategories" runat="server" Checked="false" AutoPostBack="True"
                                                                                        OnCheckedChanged="cbRemoveSubCategories_changed" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                                                                ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                                                Text="Delete" Visible="false">
                                                                                <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                            </telerik:GridButtonColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Button ID="btnAddMore" Text="Add More" runat="server" CssClass="PCGButton" CommandName="btnAddMore"
                                                                    CausesValidation="True" ValidationGroup="btnOK" OnClick="btnAddMore_Click" />
                                                                <asp:Button ID="btnOK" Text="Submit" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    CausesValidation="True" ValidationGroup="btnOK" />
                                                            </td>
                                                            <td class="rightData">
                                                                <asp:Button ID="btnRemove" Text="Remove" runat="server" CausesValidation="False"
                                                                    CssClass="PCGButton" OnClick="btnRemove_Click" Visible="false"></asp:Button>
                                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                    Visible="False" CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                            </td>
                                                            <td class="leftLabel" colspan="2">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </mastertableview>
                                        <clientsettings>
                                        </clientsettings>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlSeries" runat="server" Width="100%">
            <table id="tblSeries" runat="server" width="80%">
                <tr>
                    <td class="leftLabel">
                        &nbsp;
                    </td>
                    <td>
                        <table width="100%">
                            <tr>
                                <td class="leftLabel">
                                    <telerik:RadGrid ID="rgSeries" runat="server" AllowSorting="True" enableloadondemand="True"
                                        PageSize="5" AutoGenerateColumns="False" EnableEmbeddedSkins="False" GridLines="None"
                                        ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" OnItemDataBound="rgSeries_ItemDataBound"
                                        Skin="Telerik" AllowFilteringByColumn="True" OnNeedDataSource="rgSeries_OnNeedDataSource"
                                        OnItemCommand="rgSeries_ItemCommand" AllowPaging="false">
                                        <mastertableview allowmulticolumnsorting="True" allowsorting="true" datakeynames="AID_IssueDetailId"
                                            autogeneratecolumns="false" width="100%" editmode="PopUp" commanditemsettings-addnewrecordtext="Create New Series"
                                            commanditemdisplay="Top" commanditemsettings-showrefreshbutton="false">
                                            <%--   <CommandItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="raj" CommandName='<%# rgSeries.MasterTableView.IsItemInserted %>'>
                                     Add this Customer</asp:LinkButton>
                                    </CommandItemTemplate>--%>
                                            <Columns>
                                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="Detailslink">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                            Font-Bold="true" OnClick="btnExpandAll_Click" Font-Size="Medium">+</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                                    HeaderStyle-Width="70px" UpdateText="Update">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridBoundColumn DataField="AID_IssueDetailName" HeaderStyle-Width="100px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="Series Name" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName"
                                                    AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AID_Sequence" HeaderStyle-Width="100px" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Sequence No" UniqueName="AID_Sequence"
                                                    SortExpression="AID_Sequence" AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AID_Tenure" HeaderStyle-Width="100px" CurrentFilterFunction="Contains"
                                                    ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Tenure" UniqueName="AID_Tenure"
                                                    SortExpression="AID_Tenure" AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AID_BuyBackFacility" HeaderStyle-Width="20px"
                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                    HeaderText="IsBuyBack Available" UniqueName="AID_BuyBackFacility" SortExpression="AID_BuyBackFacility"
                                                    AllowFiltering="true">
                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <%--  <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                            ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                            Text="Delete">
                                            <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                        </telerik:GridButtonColumn>--%>
                                                <telerik:GridTemplateColumn AllowFiltering="false">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="1.5%">
                                                                &nbsp;
                                                            </td>
                                                            <td colspan="3%">
                                                                <asp:Panel ID="pnlchild" runat="server" Style="display: inline" CssClass="Landscape"
                                                                    Width="50%" ScrollBars="Horizontal" Visible="false">
                                                                    <telerik:RadGrid ID="rgSeriesCategories" runat="server" AutoGenerateColumns="False"
                                                                        enableloadondemand="True" PageSize="5" EnableEmbeddedSkins="False" GridLines="None"
                                                                        ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True" Skin="Telerik"
                                                                        AllowFilteringByColumn="true" OnNeedDataSource="rgSeriesCategories_OnNeedDataSource"
                                                                        OnItemCommand="rgSeriesCategories_OnItemCommand" AllowPaging="false">
                                                                        <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AID_IssueDetailId,AIDCSR_Id"
                                                                            AutoGenerateColumns="false">
                                                                            <Columns>
                                                                                <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryName" HeaderStyle-Width="30px"
                                                                                    CurrentFilterFunction="Contains" HeaderText="Investor CatgeoryName" SortExpression="AIIC_InvestorCatgeoryName"
                                                                                    AllowFiltering="true">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AIDCSR_DefaultInterestRate" HeaderStyle-Width="30px"
                                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                                    HeaderText="Coupon Rate" UniqueName="AIDCSR_DefaultInterestRate" SortExpression="AIDCSR_DefaultInterestRate">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridBoundColumn DataField="AIDCSR_AnnualizedYieldUpto" HeaderStyle-Width="30px"
                                                                                    CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                                    HeaderText="Annualized Yield" UniqueName="AIDCSR_AnnualizedYieldUpto" SortExpression="AIDCSR_AnnualizedYieldUpto">
                                                                                    <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                                </telerik:GridBoundColumn>
                                                                                <telerik:GridButtonColumn UniqueName="deleteColumn" ConfirmText="Are you sure you want to delete?"
                                                                                    ConfirmDialogType="RadWindow" ConfirmTitle="Delete" ButtonType="LinkButton" CommandName="Delete"
                                                                                    Text="Delete" Visible="false">
                                                                                    <ItemStyle HorizontalAlign="Center" CssClass="MyImageButton" />
                                                                                </telerik:GridButtonColumn>
                                                                                <%-- <telerik:GridBoundColumn DataField="AIDCSR_DefaultInterestRate" HeaderStyle-Width="30px"
                                                                            HeaderText="Interest Frequency" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIDCSR_InterestFrequency" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>--%>
                                                                            </Columns>
                                                                        </MasterTableView>
                                                                    </telerik:RadGrid>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                            <EditFormSettings EditFormType="Template" PopUpSettings-Height="350px" PopUpSettings-Width="800px">
                                                <FormTemplate>
                                                    <table width="80%">
                                                        <tr>
                                                            <td>
                                                                <%-- <table id="tblMessage" width="100%" runat="server" visible="false">
                                         <tr id="trSumbitSuccess" runat="server">
                                       <td align="center">
                                       <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                                             </div>
                                             </td>
                                             </tr>
                                             </table>--%>
                                                                <asp:Label ID="lblmessage" runat="server" Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" align="right">
                                                                <asp:Label ID="lb1SereiesName" runat="server" Text="Series Name:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td width="30%" align="left">
                                                                <asp:TextBox ID="txtSereiesName" runat="server" CssClass="txtField" Width="100px"></asp:TextBox>
                                                                <span id="Span24" class="spnRequiredField">*</span>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Sereies Name" Display="Dynamic" ControlToValidate="txtSereiesName"
                                                                    ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="" Display="Dynamic" ControlToValidate="txtSereiesName" ValidationGroup="btnOK">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="25%" align="left">
                                                                <asp:Label ID="lb1Tenure" runat="server" Text="Tenure:" CssClass="FieldName"></asp:Label>&nbsp;&nbsp;&nbsp;
                                                                <asp:TextBox ID="txtTenure" runat="server" CssClass="txtField" Width="90px"></asp:TextBox>
                                                                <span id="Span1" class="spnRequiredField">*</span>
                                                                <asp:DropDownList ID="ddlTenureUnits" runat="server" CssClass="txtField" Width="104px">
                                                                    <asp:ListItem Value="Select">Select</asp:ListItem>
                                                                    <asp:ListItem Value="DA">Days</asp:ListItem>
                                                                    <asp:ListItem Value="MN">Months</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span id="Span38" class="spnRequiredField">*</span>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Tenure Units" Display="Dynamic" ControlToValidate="ddlTenureUnits"
                                                                    ValidationGroup="btnOK" InitialValue="Select">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:Label ID="Label20" runat="server" CssClass="FieldName"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="<br />Please Enter Tenure" Display="Dynamic" ControlToValidate="txtTenure"
                                                                    ValidationGroup="btnOK" InitialValue="">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator14" ControlToValidate="txtTenure"
                                                                    runat="server" Display="Dynamic" ErrorMessage="Please Enter (+)Ve Digits" CssClass="cvPCG"
                                                                    ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">   </asp:RegularExpressionValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" align="right">
                                                                <asp:Label ID="lb1InterestFrequency" runat="server" Text="Interest Freq.:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <asp:DropDownList ID="ddlInterestFrequency" runat="server" CssClass="txtField" Width="104px">
                                                                </asp:DropDownList>
                                                                <span id="Span3" class="spnRequiredField">*</span>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Interest Frequency" Display="Dynamic" ControlToValidate="ddlInterestFrequency"
                                                                    ValidationGroup="btnOK" InitialValue="Select">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="35%" align="left">
                                                                <asp:Label ID="Label5" runat="server" Text="Sequence:" CssClass="FieldName"></asp:Label>
                                                                <%--  </td>
                                                    <td class="rightData" >--%>
                                                                <asp:TextBox ID="txtSequence" runat="server" CssClass="txtField" Enabled="true" Width="80px"></asp:TextBox>
                                                                <span id="Span32" class="spnRequiredField">*</span>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="reqSequence" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Sequence"
                                                                    Display="Dynamic" ControlToValidate="txtSequence" ValidationGroup="btnOK" InitialValue="">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" align="right">
                                                                <asp:Label ID="lb1InterestType" runat="server" Text="Interest Type:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td width="20%" align="left">
                                                                <asp:DropDownList ID="ddlInterestType" runat="server" CssClass="cmbField" Width="104px">
                                                                    <asp:ListItem Value="Fixed">Fixed</asp:ListItem>
                                                                    <asp:ListItem Value="Floating">Floating</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <span id="Span5" class="spnRequiredField">*</span>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Interest Type" Display="Dynamic" ControlToValidate="ddlInterestType"
                                                                    ValidationGroup="btnOK" InitialValue="Select">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                            <td width="50%" align="left">
                                                                <asp:CheckBox ID="chkBuyAvailability" runat="server" CssClass="cmbFielde" Text="Is Buy Back"
                                                                    OnCheckedChanged="chkBuyAvailability_changed" AutoPostBack="true"></asp:CheckBox>
                                                                <asp:CheckBox ID="chkredemptiondate" runat="server" CssClass="cmbFielde" Text="Redemption Applicable"
                                                                    AutoPostBack="true" OnCheckedChanged="chkRedemptiondate_changed" /></asp:CheckBox>
                                                                <asp:CheckBox ID="chkLockinperiod" runat="server" CssClass="cmbFielde" Text="Lock In Period"
                                                                    AutoPostBack="true" OnCheckedChanged="chkLockinperiod_changed" /></asp:CheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="20%" align="right">
                                                                <asp:Label ID="lblseriesFacevalue" runat="server" Text="Face Value:" CssClass="FieldName"></asp:Label>
                                                            </td>
                                                            <td width="30%" align="left">
                                                                <asp:TextBox ID="txtseriesFacevalue" runat="server" CssClass="txtField"></asp:TextBox>
                                                                <span id="Span39" class="spnRequiredField">*</span>
                                                                <br />
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator47" runat="server" CssClass="rfvPCG"
                                                                    ErrorMessage="Please Enter Numeric Value" Display="Dynamic" ControlToValidate="txtseriesFacevalue"
                                                                    ValidationGroup="btnOK" InitialValue="" ValidationExpression="[1-9]\d*$">
                                                                </asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <telerik:RadGrid ID="rgSeriesCat" runat="server" AllowSorting="True" enableloadondemand="True"
                                                                    PageSize="5" AllowPaging="false" AutoGenerateColumns="false" EnableEmbeddedSkins="False"
                                                                    GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true" ShowStatusBar="True"
                                                                    OnItemDataBound="rgSeriesCat_ItemDataBound" Skin="Telerik" AllowFilteringByColumn="true"
                                                                    OnNeedDataSource="rgSeriesCat_OnNeedDataSource">
                                                                    <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                                                        DataKeyNames="AIIC_InvestorCatgeoryId,AIIC_InvestorCatgeoryName">
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn HeaderText="Select" AllowFiltering="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="cbSeriesCat" runat="server" Checked="false" AutoPostBack="true"
                                                                                        OnCheckedChanged="cbSeriesCat_changed" />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryId" HeaderStyle-Width="10px"
                                                                                Visible="false" ShowFilterIcon="false" CurrentFilterFunction="Contains" HeaderText="Category ID"
                                                                                UniqueName="AIIC_InvestorCatgeoryId" SortExpression="AIIC_InvestorCatgeoryId">
                                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryName" HeaderStyle-Width="60px"
                                                                                UniqueName="AIIC_InvestorCatgeoryName" CurrentFilterFunction="Contains" HeaderText="Category Name"
                                                                                SortExpression="AIIC_InvestorCatgeoryName" AllowFiltering="true" ShowFilterIcon="false">
                                                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Coupon Rate (%)" AllowFiltering="false" HeaderStyle-Width="30px">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblInterest" runat="server" Text="Coupon Rate (%)"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtInterestRate" runat="server" CssClass="txtField" Width="40px"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="rgCouponRate" ControlToValidate="txtInterestRate"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                                    </asp:RegularExpressionValidator>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" CssClass="rfvPCG"
                                                                                        ErrorMessage="Please Enter value" Display="Dynamic" ControlToValidate="txtInterestRate"
                                                                                        ValidationGroup="btnOK" InitialValue="" Visible="false">
                                                                                    </asp:RequiredFieldValidator>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Yield At Maturity" AllowFiltering="false"
                                                                                HeaderStyle-Width="30px">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblAnnualized" runat="server" Text="Yield At Maturity (%)"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAnnualizedYield" runat="server" CssClass="txtField" Width="40px"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="rgAnnualizedYield" ControlToValidate="txtAnnualizedYield"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                                    </asp:RegularExpressionValidator>
                                                                                    <%-- <asp:RequiredFieldValidator ID="rgAnaualizedYield" runat="server" CssClass="rfvPCG"
                                                                                ErrorMessage="Please Enter value" Display="Dynamic" ControlToValidate="txtAnnualizedYield"
                                                                                ValidationGroup="btnOK" InitialValue="" >
                                                                            </asp:RequiredFieldValidator>--%>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Renewed Coupon Rate  (%)" AllowFiltering="false">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblRenCpnRate" runat="server" Text="Renewed Coupon Rate (%)"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRenCpnRate" runat="server" CssClass="txtField" Width="40px"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="rgRenCouponRate" ControlToValidate="txtRenCpnRate"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter +(ve) Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                                    </asp:RegularExpressionValidator>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <%--   <telerik:GridTemplateColumn HeaderText="Coupon Rate(%)" AllowFiltering="false" HeaderStyle-Width="30px">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblInterest" runat="server" Text="Coupon Rate(%)"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtInterestRate" runat="server" CssClass="txtField" Width="40px"></asp:TextBox>
                                                                            <asp:RegularExpressionValidator ID="rgCouponRate" ControlToValidate="txtInterestRate"
                                                                                runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                                ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                            </asp:RegularExpressionValidator>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" CssClass="rfvPCG"
                                                                                ErrorMessage="Please Enter value" Display="Dynamic" ControlToValidate="txtInterestRate"
                                                                                ValidationGroup="btnOK" InitialValue="" Visible="false">
                                                                            </asp:RequiredFieldValidator>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>--%>
                                                                            <telerik:GridTemplateColumn HeaderText="Yield At Call" AllowFiltering="false" UniqueName="YieldAtCall">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lb1YieldAtCall" runat="server" Text="Yield At Call(%)"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtYieldAtCall" runat="server" CssClass="txtField" Width="40px"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="rgYieldAtCall" ControlToValidate="txtYieldAtCall"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter +(ve) Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">   
                                                                                
                                                                                    </asp:RegularExpressionValidator>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Redemption Date Note" AllowFiltering="false"
                                                                                UniqueName="Redemptiondate">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblRedemptionDate" runat="server" Text="Redemption Date Note"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRedemptionDate" runat="server" CssClass="txtField" Width="90px"
                                                                                        Visible="false"></asp:TextBox>
                                                                                    <%--   <asp:RegularExpressionValidator ID="rgRedemptionDate" ControlToValidate="txtRedemptionDate"
                                                                                runat="server" Display="Dynamic" ErrorMessage="Please Enter letter" CssClass="cvPCG"
                                                                                ValidationExpression="[a-zA-Z ]*$" ValidationGroup="btnOK">   
                                                                                 
                                                                            </asp:RegularExpressionValidator>--%>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Redemption Amount(Per Bond)" AllowFiltering="false"
                                                                                HeaderStyle-Width="30px">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lblRedemptionAmount" runat="server" Text="Redemption Amount(Per Bond)"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRedemptionAmount" runat="server" CssClass="txtField" Width="50px"
                                                                                        Visible="false"></asp:TextBox>
                                                                                    <%--<asp:RegularExpressionValidator ID="rgRedemptionAmount" ControlToValidate="txtRedemptionAmount"
                                                                                runat="server" Display="Dynamic" ErrorMessage="Please Enter Digits" CssClass="cvPCG"
                                                                                ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                            </asp:RegularExpressionValidator>--%>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Yield At BuyBack" AllowFiltering="false"
                                                                                runat="server" UniqueName="YieldAtBuyBack">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lb1YieldAtBuyBack" runat="server" Text="Yield At BuyBack (%)"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtYieldAtBuyBack" runat="server" CssClass="txtField" Width="40px"
                                                                                        AutoPostBack="true" Visible="false"></asp:TextBox>
                                                                                    <asp:RegularExpressionValidator ID="rgYieldAtBuyBack" ControlToValidate="txtYieldAtBuyBack"
                                                                                        runat="server" Display="Dynamic" ErrorMessage="Please Enter +(ve) Digits" CssClass="cvPCG"
                                                                                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                                    </asp:RegularExpressionValidator>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Lock In Period" AllowFiltering="false" HeaderStyle-Width="40px">
                                                                                <HeaderTemplate>
                                                                                    <asp:Label ID="lb1Lockinperiod" runat="server" Text="Lock In Period"></asp:Label>
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtLockInPeriod" runat="server" CssClass="txtField" Width="40px"
                                                                                        AutoPostBack="false" Visible="false"></asp:TextBox>
                                                                                    <%--<asp:RegularExpressionValidator ID="reqtxtLockInPeriod" ControlToValidate="txtLockInPeriod"
                                                                                runat="server" Display="Dynamic" ErrorMessage="Please Enter +(ve) Digits" CssClass="cvPCG"
                                                                                ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnOK">     
                                                                            </asp:RegularExpressionValidator>--%>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="leftLabel">
                                                                <asp:Button ID="btnOK" Text="OK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    CausesValidation="True" ValidationGroup="btnOK" />
                                                            </td>
                                                            <td class="rightData">
                                                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                                    CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                            </td>
                                                            <td class="rightData">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td colspan="3">
                                                                &nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                &nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>--%>
                                                    </table>
                                                </FormTemplate>
                                            </EditFormSettings>
                                        </mastertableview>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <%--<asp:Panel ID="pnlCategory" runat="server" CssClass="Landscape" Width="100%">
    <table id="Table1" runat="server" width="80%">
        <tr>
            <td class="leftLabel">
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadGrid ID="rgEligibleInvestorCategories" runat="server" AllowSorting="True"
                                enableloadondemand="True" PageSize="5" AllowPaging="True" AutoGenerateColumns="False"
                                EnableEmbeddedSkins="False" GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="true"
                                ShowStatusBar="True" Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgEligibleInvestorCategories_OnNeedDataSource"
                                OnItemCommand="rgEligibleInvestorCategories_ItemCommand" OnItemDataBound="rgEligibleInvestorCategories_ItemDataBound">
                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,AIIC_InvestorCatgeoryId"
                                    AutoGenerateColumns="false" Width="100%" EditMode="PopUp" CommandItemSettings-AddNewRecordText="Create InvestorCategory"
                                    CommandItemDisplay="Top">
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbDetails" runat="server" CommandName="ExpandCollapse" Font-Underline="False"
                                                    Font-Bold="true" UniqueName="DetailsCategorieslink" OnClick="btnCategoriesExpandAll_Click"
                                                    Font-Size="Medium">+</asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                            UpdateText="Update">
                                        </telerik:GridEditCommandColumn>
                                        <telerik:GridBoundColumn DataField="AIIC_InvestorCatgeoryName" HeaderStyle-Width="20px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="Catgeory Name" UniqueName="AIIC_InvestorCatgeoryName" SortExpression="AIIC_InvestorCatgeoryName"
                                            AllowFiltering="true">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIIC_ChequePayableTo" HeaderStyle-Width="200px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="ChequePayableTo" UniqueName="AIIC_ChequePayableTo" SortExpression="AIIC_ChequePayableTo">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIIC_MInBidAmount" HeaderStyle-Width="200px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="MInBid Amount" UniqueName="AIIC_MInBidAmount" SortExpression="AIIC_MInBidAmount">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="AIIC_MaxBidAmount" HeaderStyle-Width="200px"
                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                            HeaderText="MaxBid Amount" UniqueName="AIIC_MaxBidAmount" SortExpression="AIIC_MaxBidAmount">
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="1.5%">
                                                        &nbsp;
                                                    </td>
                                                    <td colspan="3%">
                                                        <asp:Panel ID="pnlCategoriesDetailschild" runat="server" Style="display: inline"
                                                            CssClass="Landscape" ScrollBars="Horizontal" Visible="false">
                                                            <telerik:RadGrid ID="rgCategoriesDetails" runat="server" AutoGenerateColumns="False"
                                                                enableloadondemand="True" PageSize="5" AllowPaging="True" EnableEmbeddedSkins="False"
                                                                GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                                                Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgCategoriesDetails_OnNeedDataSource">
                                                                <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIIC_InvestorCatgeoryId"
                                                                    AutoGenerateColumns="false">
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="WCMV_Name" HeaderStyle-Width="30px" UniqueName="WCMV_Name"
                                                                            CurrentFilterFunction="Contains" HeaderText="Investor Type" SortExpression="WCMV_Name"
                                                                            AllowFiltering="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIICS_InvestorSubTypeCode" HeaderStyle-Width="30px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="SubType Code" UniqueName="AIIC_InvestorSubTypeCode" SortExpression="AIIC_InvestorSubTypeCode">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIICS_MinInvestmentAmount" HeaderStyle-Width="30px"
                                                                            CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                                            HeaderText="MinInvestment Amount" UniqueName="AIIC_MinInvestmentAmount" SortExpression="AIIC_MinInvestmentAmount">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="AIICS_MaxInvestmentAmount" HeaderStyle-Width="30px"
                                                                            HeaderText="MaxInvestment Amount" CurrentFilterFunction="Contains" ShowFilterIcon="false"
                                                                            AutoPostBackOnFilter="true" UniqueName="AIIC_MaxInvestmentAmount" Visible="true">
                                                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20px" Wrap="false" />
                                                                        </telerik:GridBoundColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template" PopUpSettings-Height="450px" PopUpSettings-Width="700px">
                                        <FormTemplate>
                                            <table width="75%" cellspacing="2" cellpadding="2">
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="lb1IssueName" runat="server" Text="Issue Name:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData">
                                                        <asp:TextBox ID="txtIssueName" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span2" class="spnRequiredField">*</span>
                                                    </td>
                                                    <td colspan="2">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="rfvPCG"
                                                            ErrorMessage="Please Craeate Issue" Display="Dynamic" ControlToValidate="txtIssueName"
                                                            Enabled="false" ValidationGroup="btnOK">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="lb1CategoryName" runat="server" Text="Category Name:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" colspan="2">
                                                        <asp:TextBox ID="txtCategoryName" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span1" class="spnRequiredField">*</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" CssClass="rfvPCG"
                                                            ErrorMessage="Please Enter Category Name" Display="Dynamic" ControlToValidate="txtCategoryName"
                                                            ValidationGroup="btnOK">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="Label1" runat="server" Text="Category Description:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" colspan="2">
                                                        <asp:TextBox ID="txtCategoryDescription" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span4" class="spnRequiredField">*</span>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" CssClass="rfvPCG"
                                                            ErrorMessage="Please Enter Category Description" Display="Dynamic" ControlToValidate="txtCategoryDescription"
                                                            ValidationGroup="btnOK">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="Label2" runat="server" Text="Cheque Payable To:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" colspan="2">
                                                        <asp:TextBox ID="txtChequePayableTo" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span6" class="spnRequiredField">*</span>                                                      
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" CssClass="rfvPCG"
                                                            ErrorMessage="Please Enter ChequePayableTo" Display="Dynamic" ControlToValidate="txtChequePayableTo"
                                                            ValidationGroup="btnOK">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="Label3" runat="server" Text="Min Bid Amount:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" colspan="2">
                                                        <asp:TextBox ID="txtMinBidAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span7" class="spnRequiredField">*</span>                                                       
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" CssClass="rfvPCG"
                                                            ErrorMessage="Please Enter Min Bid Amount" Display="Dynamic" ControlToValidate="txtMinBidAmount"
                                                            ValidationGroup="btnOK">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                 
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Label ID="Label4" runat="server" Text="Max Bid Amount:" CssClass="FieldName"></asp:Label>
                                                    </td>
                                                    <td class="rightData" colspan="2">
                                                        <asp:TextBox ID="txtMaxBidAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                        <span id="Span8" class="spnRequiredField">*</span>
                                                      
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" CssClass="rfvPCG"
                                                            ErrorMessage="Please Enter Max Bid Amount" Display="Dynamic" ControlToValidate="txtMaxBidAmount"
                                                            ValidationGroup="btnOK">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                   
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <telerik:RadGrid ID="rgSubCategories" runat="server" AllowSorting="True" enableloadondemand="True"
                                                            PageSize="10" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                                                            GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                                                            Skin="Telerik" AllowFilteringByColumn="true" OnNeedDataSource="rgSubCategories_OnNeedDataSource"
                                                            DataKeyNames="WCMV_LookupId">
                                                            <MasterTableView>
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn HeaderText="Select" ShowFilterIcon="false" AllowFiltering="false">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblchkBxSelect" runat="server" Text="Select"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="cbSubCategories" runat="server" Checked="false" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="WCMV_Name" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Sub Category"
                                                                        UniqueName="WCMV_Name" SortExpression="WCMV_Name" AllowFiltering="true">
                                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="WCMV_LookupId" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="LookupId" UniqueName="WCMV_LookupId"
                                                                        SortExpression="WCMV_LookupId" Visible="false">
                                                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Sub Category Code" ShowFilterIcon="false"
                                                                        AllowFiltering="false">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblSubCategoryCode" runat="server" Text="Sub Category Code"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtSubCategoryCode" runat="server" CssClass="txtField"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="MinInvestmentAmount" ShowFilterIcon="false"
                                                                        AllowFiltering="false">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblMinInvestmentAmount" runat="server" Text="MinInvestmentAmount"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtMinInvestmentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn HeaderText="Max Investment Amount" ShowFilterIcon="false"
                                                                        AllowFiltering="false">
                                                                        <HeaderTemplate>
                                                                            <asp:Label ID="lblMaxInvestmentAmount" runat="server" Text="Max Investment Amount"></asp:Label>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtMaxInvestmentAmount" runat="server" CssClass="txtField"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftLabel">
                                                        <asp:Button ID="btnOK" Text="OK" runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                            CausesValidation="True" ValidationGroup="btnOK" />
                                                        
                                                    </td>
                                                    <td class="rightData">
                                                        <asp:Button ID="btnCancel" Text="Cancel" runat="server" CausesValidation="False"
                                                            CssClass="PCGButton" CommandName="Cancel"></asp:Button>
                                                    </td>
                                                    <td class="leftLabel" colspan="2">
                                                        &nbsp;
                                                    </td>
                                                  
                                                </tr>
                                               
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </MasterTableView>
                                <ClientSettings>
                                </ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Panel>--%>
        <asp:HiddenField ID="hdnBrokerIds" runat="server" />
        <telerik:RadWindow ID="RadRegister" runat="server" VisibleOnPageLoad="false" Height="30%"
            Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
            Title="Add New Register" RestrictionZoneID="radWindowZone" Top="100px" Left="200">
            <contenttemplate>
                <table>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblRegistername" runat="server" Text="Register:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRegistername" runat="server" CssClass="txtField"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSubmitRegister" runat="server" Text="Submit" CssClass="PCGButton"
                                OnClick="btnSubmitRegister_OnClick" />
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadSyndicate" runat="server" VisibleOnPageLoad="false" Height="30%"
            Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
            Title="Add New Syndicate" RestrictionZoneID="radWindowZone" Top="400px" Left="200">
            <contenttemplate>
                <table>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblSyndicate" runat="server" Text="Syndicate:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSyndicate" runat="server" CssClass="txtField"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSyndicate" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnSyndicate_OnClick" />
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadBroker" runat="server" VisibleOnPageLoad="false" Height="30%"
            Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
            Title="Add New Broker" RestrictionZoneID="radWindowZone" Top="400px" Left="200">
            <contenttemplate>
                <table>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblBrokercodeadd" runat="server" Text="Broker Name:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBrokercodeadd" runat="server" CssClass="txtField" MaxLength="50"></asp:TextBox>
                            <span id="spntxtField" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="ReqlblBrokercodeadd" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Please Enter Broker" Display="Dynamic" ControlToValidate="txtBrokercodeadd"
                                ValidationGroup="btnOKbtnBrokercodeadd" InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                    <td align="right">
                            <asp:Label ID="lblBrokerIdentifier" runat="server" Text="Broker Code:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtBrokerIdentifier" runat="server" CssClass="txtField" MaxLength="28"></asp:TextBox>
                            <span id="Span20" class="spnRequiredField">*</span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" CssClass="rfvPCG"
                                ErrorMessage="Please Enter Broker Code" Display="Dynamic" ControlToValidate="txtBrokerIdentifier"
                                ValidationGroup="btnOKbtnBrokercodeadd" InitialValue="">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnBrokercodeadd" runat="server" Text="Submit" CssClass="PCGButton"
                                OnClick="btnBrokercodeadd_OnClick" ValidationGroup="btnOKbtnBrokercodeadd" />
                        </td>
                    </tr>
                </table>
            </contenttemplate>
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadIssueBroker" runat="server" VisibleOnPageLoad="false" Height="30%"
            Width="400px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Resize, Close, Move"
            Title="Select Broker For Issue" RestrictionZoneID="radWindowZone" Top="400px" Left="200">
            <contenttemplate>
            
                <table  style="padding-left:80px;" >
                    <tr>
                        <td align="center">
                       <asp:Panel runat="server" ID="pnlBroker"  Height="250px" Width="280px" ScrollBars="Vertical">
                           <asp:CheckBoxList ID="chblBroker" runat="server" CssClass="cmbFielde"  BorderWidth="2">
                            </asp:CheckBoxList >
                             </asp:Panel>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="padding-left:80px;">
                            <asp:Button ID="btnIssueTOBrokerMapping" runat="server" Text="Submit" CssClass="PCGButton"
                                OnClick="btnIssueTOBrokerMapping_OnClick"/>
                        </td>
                        
                    </tr>
                </table>
               
            </contenttemplate>
        </telerik:RadWindow>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnSetUpSubmit" />
    </Triggers>
</asp:UpdatePanel>
