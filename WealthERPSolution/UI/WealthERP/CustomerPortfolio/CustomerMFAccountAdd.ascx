<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMFAccountAdd" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script type="text/javascript">
    function ShowPopup() {
        var i = 0;
        var form = document.forms[0];
        var folioId = "";
        var count = 0;
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select one record.")
            return false;
        }
    }
</script>

<style type="text/css">
    .error
    {
        color: Red;
        font-weight: bold;
        font-size: 12px;
    }
    .success
    {
        color: Green;
        font-weight: bold;
        font-size: 12px;
    }
</style>

<script language="javascript" type="text/javascript">
    function openpopupAddBank() {
        window.open('PopUp.aspx?PageId=AddBankAccount &AddMFFolioLinkId= AddBankFromFolioScreen', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }
</script>

<script type="text/javascript">
    function UseProfileName() {
        if (document.getElementById("<%=chkUseProfileName.ClientID %>").checked == true) {
            document.getElementById("<%=txtInvestorName.ClientID %>").value = document.getElementById("ctrl_CustomerMFAccountAdd_hdnCustomerName").value;
            //            var sessionValue = '<%= Session["CustomerName"] %>';
        }
        else {
            document.getElementById("<%=txtInvestorName.ClientID %>").value = "";
        }
    }
</script>

<script type="text/javascript">
    function checkLoginId2() {
        $("#<%= hidValidCheck.ClientID %>").val("0");
        if ($("#<%=txtFolioNumber.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }
        $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckTradeNoMFAvailability",
            data: "{ 'TradeAccNo': '" + $("#<%=txtFolioNumber.ClientID %>").val() + "','BrokerCode': '" + $("#<%=ddlProductAmc.ClientID %>").val() + "','PortfolioId': '" + $("#<%=ddlPortfolio.ClientID %>").val() + "' }",
            error: function(xhr, status, error) {
                //                alert("Please select AMC!");
            },
            success: function(msg) {

                if (msg.d) {

                    $("#<%= hidValidCheck.ClientID %>").val("1");
                    $("#spnLoginStatus").html("");
                }
                else {


                    $("#<%= hidValidCheck.ClientID %>").val("0");
                    $("#spnLoginStatus").removeClass();
                    alert("Folio Number Already Exists");
                    return false;
                }
            }

        });
    }
    function isValidInUpdateCase() {
        var hdnIsCustomerLogin = document.getElementById('ctrl_CustomerMFAccountAdd_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_CustomerMFAccountAdd_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {
            alert('Permisssion denied for Manage Portfolio !!');
            return false;
        }
        else {
            return true;
        }
    }
    function isValid() {
        var hdnIsCustomerLogin = document.getElementById('ctrl_CustomerMFAccountAdd_hdnIsCustomerLogin').value;
        var hdnIsMainPortfolio = document.getElementById('ctrl_CustomerMFAccountAdd_hdnIsMainPortfolio').value;

        if (hdnIsCustomerLogin == "Customer" && hdnIsMainPortfolio == "1") {
            alert('Permisssion denied for Manage Portfolio !!');
            return false;
        }
        else {
            if ($("#<%= ddlProductAmc.ClientID %>").val() == "Select an AMC Code") {
                alert("Please select the AMC First");
                return false;
            }
            else if ($("#<%= txtFolioNumber.ClientID %>").val() == "") {
                alert('Please fill the folio No');
                return false;
            }

            if ($("#<%= hidValidCheck.ClientID %>").val() == '1') {
                return Page_IsValid;
            }
        }
    }
    function checkDate(sender, args) {
        var selectedDate = new Date();
        selectedDate = sender._selectedDate;
        var todayDate = new Date();
        var msg = "";
        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }    
</script>
<script type="text/javascript">
    function setCustomPosition(sender, args) {
        sender.moveTo(sender.get_left(), sender.get_top());
    }
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<telerik:RadWindow VisibleOnPageLoad="false" ID="radwindowForGuardian" runat="server"
    Height="30%" Width="550px" Modal="true" BackColor="#DADADA" Top="10px" Left="20px"
    Behaviors="Move,resize,close" Title="Add Guardian" OnClientShow="setCustomPosition">
    <contenttemplate>
        <div id="divForGuardian" style="width: 75%; text-align: center;" runat="server" class="failure-msg"
            align="center" visible="false">
            Records not found
        </div>
        <div style="padding: 20px">
            <telerik:RadGrid ID="gvGuardian" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="MemberCustomerId, AssociationId" NoDetailRecordsText="Records not found"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="15px" AllowFiltering="false" HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId0" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="110px" DataField="Name"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="Relationship"
                            HeaderText="Relationship" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div style="padding: 20px">
            <asp:Button ID="btnAddMinor" runat="server" CssClass="PCGButton" Text="Associate"
                OnClick="btnAddGuardian_Click" OnClientClick="return ShowPopup()" />
        </div>
    </contenttemplate>
</telerik:RadWindow>
<telerik:RadWindow VisibleOnPageLoad="false" ID="radwindowForNominee" runat="server"
    Height="30%" Width="550px" Modal="true" BackColor="#DADADA" Top="10px" Left="20px"
    Behaviors="Move,resize,close" Title="Add Nominee" OnClientShow="setCustomPosition">
    <contenttemplate>
        <div id="DivForNominee" style="width: 75%; text-align: center;" runat="server" class="failure-msg"
            align="center" visible="false">
            Records not found
        </div>
        <div style="padding: 20px">
            <telerik:RadGrid ID="gvNominees" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView DataKeyNames="MemberCustomerId, AssociationId" NoDetailRecordsText="Records not found"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="15px" AllowFiltering="false" HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId0" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="110px" DataField="Name"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="Relationship"
                            HeaderText="Relationship" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div style="padding: 20px">
            <asp:Button ID="btnAddNominee" runat="server" CssClass="PCGButton" Text="Associate"
                OnClick="btnAddNominee_Click" OnClientClick="return ShowPopup()" />
        </div>
    </contenttemplate>
</telerik:RadWindow>
<telerik:RadWindow VisibleOnPageLoad="false" ID="radwindowForJointHolder" runat="server"
    Height="30%" Width="550px" Modal="true" BackColor="#DADADA" Top="10px" Left="20px"
    Behaviors="Move,resize,close" Title="Add Joint Holder" OnClientShow="setCustomPosition">
    <contenttemplate>
        <div id="DivForJH" style="width: 75%; text-align: center;" runat="server" class="failure-msg"
            align="center" visible="false">
            Records not found
        </div>
        <div style="padding: 20px">
            <telerik:RadGrid ID="gvJointHoldersList" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <ExportSettings HideStructureColumns="true">
                </ExportSettings>
                <MasterTableView NoDetailRecordsText="Records not found" DataKeyNames="AssociationId"
                    Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None">
                    <NoRecordsTemplate>
                        <div>
                            There are no records to display</div>
                    </NoRecordsTemplate>
                    <Columns>
                        <telerik:GridTemplateColumn HeaderStyle-Width="15px" AllowFiltering="false" HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkId" runat="server" />
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderStyle-Width="110px" ShowFilterIcon="false" DataField="Name"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn HeaderStyle-Width="100px" ShowFilterIcon="false" DataField="Relationship"
                            HeaderText="Relationship" />
                    </Columns>
                </MasterTableView>
                <ClientSettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </ClientSettings>
            </telerik:RadGrid>
        </div>
        <div style="padding: 20px">
            <asp:Button ID="btnAddJointHolder" runat="server" CssClass="PCGButton" Text="Associate"
                OnClick="btnAddJointHolder_Click" OnClientClick="return ShowPopup()" />
        </div>
    </contenttemplate>
</telerik:RadWindow>
<table width="100%" class="TableBackground">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Add MF Folio
                        </td>
                        <td align="right">
                            <asp:LinkButton ID="lnkEdit" Visible="false" runat="server" CssClass="LinkButtons"
                                OnClick="lnkEdit_Click">Edit</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
    </tr>
</table>
<table width="100%" class="TableBackground">
    <tr>
        <td style="width: 300px" align="right">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name : "></asp:Label>
        </td>
        <td colspan="4" style="width: 50px">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <%--</table>
<table width="100%" class="TableBackground">--%>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber">
                    1</div>
                &nbsp;&nbsp;Folio Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFolioNum0" runat="server" CssClass="FieldName" Text="AMC Code :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:DropDownList ID="ddlProductAmc" AutoPostBack="true" runat="server" OnSelectedIndexChanged="btn_amccheck"
                CssClass="cmbField">
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="cvProductAmc" runat="server" ControlToValidate="ddlProductAmc"
                ErrorMessage="Please select an AMC Code" Operator="NotEqual" ValueToCompare="Select an AMC Code"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblFolioNum" runat="server" CssClass="FieldName" Text="Folio No. :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:TextBox ID="txtFolioNumber" runat="server" CssClass="txtField" Enabled="false"
                MaxLength="15" onblur="return checkLoginId2()"></asp:TextBox>
            <span id="Span3" class="spnRequiredField">*</span><span id="spnLoginStatus"></span>
            <asp:RequiredFieldValidator ID="rfvFolioNumber" ControlToValidate="txtFolioNumber"
                ErrorMessage="Please enter a Folio Number" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblJointHolding" runat="server" CssClass="FieldName" Text="Joint Holding :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:RadioButton ID="rbtnYes" runat="server" CssClass="cmbFielde" GroupName="rbtnJointHolding"
                Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnYes_CheckedChanged1" />
            <asp:RadioButton ID="rbtnNo" runat="server" CssClass="cmbFielde" Checked="true" GroupName="rbtnJointHolding"
                Text="No" AutoPostBack="true" OnCheckedChanged="rbtnNo_CheckedChanged" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblModeOfHolding" runat="server" CssClass="FieldName" Text="Mode of Holding :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:DropDownList ID="ddlModeOfHolding" runat="server" CssClass="cmbField" ValidationGroup="btnSubmit">
            </asp:DropDownList>
            <span id="Span3" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlModeOfHolding"
                ErrorMessage="Please select a Mode of Holding" Operator="NotEqual" ValueToCompare="0"
                CssClass="cvPCG" ValidationGroup="btnSubmit"></asp:CompareValidator>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label30" runat="server" CssClass="FieldName" Text="Is Online:"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:RadioButtonList ID="rbtnlIs_online" Width="90px" runat="server" CssClass="cmbFielde" RepeatDirection="Horizontal">
                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                <asp:ListItem Selected="True" Text="No" Value="0"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <%--<tr>
        <td>
            <asp:RadioButton ID="rbtnYesOnline" runat="server" CssClass="cmbField" GroupName="rbtnJointHolding"
                Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnYesOnline_CheckedChanged1" />
            <asp:RadioButton ID="rbtnNoOnline" runat="server" CssClass="cmbField" Checked="true"
                GroupName="rbtnJointHolding" Text="No" AutoPostBack="true" OnCheckedChanged="rbtnNoOnline_CheckedChanged" />
        </td>
    </tr>--%>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblAccountStartingDate" runat="server" CssClass="FieldName" Text="Account Starting Date :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <telerik:RadDatePicker ID="txtAccountDate" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblInvestorName" runat="server" CssClass="FieldName" Text="Investor Name:"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:TextBox ID="txtInvestorName" runat="server" CssClass="txtField"></asp:TextBox>
            <asp:CheckBox ID="chkUseProfileName" runat="server" Text="Use Profile Name" CssClass="FieldName"
                onClick="return UseProfileName()" AutoPostBack="false" />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label23" runat="server" CssClass="FieldName" Text="Broker Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtBrokerCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField">
            <asp:Label ID="lblAssociateCode" runat="server" CssClass="FieldName" Text="SubBroker Code:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAssociateCode" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <%-- <td class="rightField">
            <asp:DropDownList ID="ddlAssociateCode" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>--%>
        <td>
        </td>
    </tr>
    <tr visible="false">
        <td class="leftField">
            <asp:Label Visible="false" ID="Label25" runat="server" CssClass="FieldName" Text="Tax Status:"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:TextBox Visible="false" ID="txtTaxStatus" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblddlBankList" runat="server" CssClass="FieldName" Text="Bank :"></asp:Label>
        </td>
        <td class="rightField" colspan="4">
            <asp:DropDownList ID="ddlBankList" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlBankList_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:ImageButton ID="imgBtnAddBank" ImageUrl="~/Images/user_add.png" runat="server"
                ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()" Height="15px"
                Width="15px"></asp:ImageButton>
            <asp:ImageButton ID="imgBtnRefereshBank" ImageUrl="~/Images/refresh.png" AlternateText="Refresh"
                runat="server" ToolTip="Click here to refresh Bank List" OnClick="imgBtnRefereshBank_OnClick"
                Height="15px" Width="25px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber">
                    2</div>
                &nbsp;&nbsp;Associate Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label CssClass="FieldName" Text="Add Nominee :" runat="server"></asp:Label>
        </td>
        <td colspan="4">
            <asp:ImageButton OnClick="imgAddNominee_Click" ImageUrl="~/Images/user_add.png" runat="server"
                ToolTip="Click here to Add Nominee" Height="15px" Width="15px" ID="imgAddNominee"
                Text="AddNominee"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="4">
            <telerik:RadGrid Visible="false" ID="gvNominee2" runat="server" GridLines="None"
                AutoGenerateColumns="False" PageSize="10" AllowSorting="false" AllowPaging="True"
                ShowStatusBar="false" ShowFooter="false" Skin="Telerik" EnableEmbeddedSkins="false"
                Width="500px" AllowFilteringByColumn="false" AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <exportsettings hidestructurecolumns="true">
                </exportsettings>
                <mastertableview nodetailrecordstext="Records not found" width="100%" allowmulticolumnsorting="True"
                    autogeneratecolumns="false" commanditemdisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="110px"
                            DataField="MemberCustomerId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="50px"
                            DataField="AssociationId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="NAME"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="25px" DataField="XR_Relationship"
                            HeaderText="Relation" />
                    </Columns>
                </mastertableview>
                <clientsettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </clientsettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label CssClass="FieldName" Text="Add Guardian :" runat="server"></asp:Label>
        </td>
        <td colspan="4">
            <asp:ImageButton OnClick="imgAddGuardian_Click" ID="imgAddGuardian" Text="AddGuardian"
                runat="server" ImageUrl="~/Images/user_add.png" runat="server" ToolTip="Click here to Add Nominee"
                Height="15px" Width="15px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="4">
            <telerik:RadGrid Visible="false" ID="gvGuardian2" runat="server" GridLines="None"
                AutoGenerateColumns="False" PageSize="10" AllowSorting="false" AllowPaging="True"
                ShowStatusBar="True" ShowFooter="false" Skin="Telerik" EnableEmbeddedSkins="false"
                Width="500px" AllowFilteringByColumn="false" AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <exportsettings hidestructurecolumns="true">
                </exportsettings>
                <mastertableview nodetailrecordstext="Records not found" width="100%" allowmulticolumnsorting="True"
                    autogeneratecolumns="false" commanditemdisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="110px"
                            DataField="MemberCustomerId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="50px"
                            DataField="AssociationId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="NAME"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="25px" DataField="XR_Relationship"
                            HeaderText="Relation" />
                    </Columns>
                </mastertableview>
                <clientsettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </clientsettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr id="trAddJointHolder" runat="server" visible="false">
        <td class="leftField">
            <asp:Label CssClass="FieldName" Text="Add Joint Holder :" runat="server"></asp:Label>
        </td>
        <td colspan="4">
            <asp:ImageButton OnClick="imgAddJointHolder_Click" ID="imgAddJointHolder" Text="AddJTHolder"
                runat="server" ImageUrl="~/Images/user_add.png" runat="server" ToolTip="Click here to Add Nominee"
                Height="15px" Width="15px"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td colspan="4">
            <telerik:RadGrid Visible="false" ID="gvJoint2" runat="server" GridLines="None" AutoGenerateColumns="False"
                PageSize="10" AllowSorting="false" AllowPaging="True" ShowStatusBar="True" ShowFooter="false"
                Skin="Telerik" EnableEmbeddedSkins="false" Width="500px" AllowFilteringByColumn="false"
                AllowAutomaticInserts="false" ExportSettings-FileName="Nominee Details">
                <exportsettings hidestructurecolumns="true">
                </exportsettings>
                <mastertableview nodetailrecordstext="Records not found" width="100%" allowmulticolumnsorting="True"
                    autogeneratecolumns="false" commanditemdisplay="None">
                    <Columns>
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="110px"
                            DataField="MemberCustomerId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" Visible="false" HeaderStyle-Width="50px"
                            DataField="AssociationId" HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="100px" DataField="NAME"
                            HeaderText="Name" />
                        <telerik:GridBoundColumn ShowFilterIcon="false" HeaderStyle-Width="25px" DataField="XR_Relationship"
                            HeaderText="Relation" />
                    </Columns>
                </mastertableview>
                <clientsettings>
                    <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                </clientsettings>
            </telerik:RadGrid>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber">
                    3</div>
                &nbsp;&nbsp;Bank Details
            </div>
        </td>
    </tr>
    <tr id="trbankList" runat="server">
        <td class="leftField">
            <asp:Label ID="Label29" runat="server" CssClass="FieldName" Text="Bank Name On Ext. File :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox Enabled="false" ID="txtExternalFileBankName" CssClass="txtField" runat="server"></asp:TextBox>
        </td>
        <td colspan="3">
        </td>
    </tr>
    <asp:UpdatePanel runat="server" ID="upBankDetails">
        <ContentTemplate>
            <div id="divBankDetails" runat="server">
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label28" runat="server" CssClass="FieldName" Text="All Banks :"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlALLBankList" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgAddBankForTBC" ImageUrl="~/Images/user_add.png" runat="server"
                            ToolTip="Click here to Update Bank" Height="15px" Width="15px" OnClick="imgAddBankForTBC_Click">
                        </asp:ImageButton>
                    </td>
                    <td class="leftField">
                        <asp:Label ID="Label22" CssClass="FieldName" Text="Account Type :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlAccType" CssClass="cmbField" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label CssClass="FieldName" Text="Account No. :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtAccNo" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label10" CssClass="FieldName" Text="Mode of Operation :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlModeOfOpn" CssClass="cmbField" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label11" CssClass="FieldName" Text="Bank Name :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtBankName" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label14" CssClass="FieldName" Text="Branch Name :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBranchName" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label12" CssClass="FieldName" Text="Line1(House No./Building) :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtBLine1" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label13" CssClass="FieldName" Text="Line2(Street) :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtBLine2" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label15" CssClass="FieldName" Text="Line3(Area) :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtBLine3" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label16" CssClass="FieldName" Text="City :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtCity" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label17" CssClass="FieldName" Text="Pin Code :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtPinCode" MaxLength="6" CssClass="txtField" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="cvBankPinCode" runat="server" ErrorMessage="<br />Enter a numeric value"
                            CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPinCode" ValidationGroup="btnSubmit"
                            Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label18" CssClass="FieldName" Text="MICR :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:TextBox ID="txtMicr" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label19" CssClass="FieldName" Text="State :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:DropDownList CssClass="cmbField" ID="ddlBState" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="leftField" style="width: 20%">
                        <asp:Label ID="Label20" CssClass="FieldName" Text="Country :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField">
                        <asp:DropDownList ID="ddlBCountry" runat="server" CssClass="cmbField">
                            <asp:ListItem Text="India" Value="0" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="leftField">
                        <asp:Label ID="Label21" CssClass="FieldName" Text="IFSC :" runat="server"></asp:Label>
                    </td>
                    <td class="rightField" style="width: 25%">
                        <asp:TextBox ID="txtIfsc" CssClass="txtField" runat="server"></asp:TextBox>
                    </td>
                    <td colspan="3">
                    </td>
                </tr>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber">
                    4</div>
                &nbsp;&nbsp;Personal Details
            </div>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label26" runat="server" CssClass="FieldName" Text="Customer Type :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label27" CssClass="FieldName" Text="Customer SubType :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label24" runat="server" CssClass="FieldName" Text="PAN No.:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtPanNo" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label CssClass="FieldName" Text="Address 1 :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtPAddress1" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label1" CssClass="FieldName" Text="Address 2 :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtPAddress2" runat="server"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label2" CssClass="FieldName" Text="Address 3 :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtPAddress3" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label3" CssClass="FieldName" Text="City :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtPCity" runat="server"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label4" CssClass="FieldName" Text="Pincode :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" MaxLength="6" ID="txtPPinCode" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtPPinCode" ValidationGroup="btnSubmit"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" CssClass="FieldName" Text="Joint Holder 1 :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtCustJName1" runat="server"></asp:TextBox>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label6" CssClass="FieldName" Text="Joint Holder 2 :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtCustJName2" runat="server"></asp:TextBox>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" CssClass="FieldName" Text="Ph. No.(office) :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtCustPhNoOff" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtCustPhNoOff" ValidationGroup="btnSubmit"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="Label8" CssClass="FieldName" Text="Ph. No.(res) :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtCustPhNoRes" runat="server"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="<br />Enter a numeric value"
                CssClass="rfvPCG" Type="Integer" ControlToValidate="txtCustPhNoRes" ValidationGroup="btnSubmit"
                Operator="DataTypeCheck" Display="Dynamic"></asp:CompareValidator>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label9" CssClass="FieldName" Text="Email :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <asp:TextBox CssClass="txtField" ID="txtCustEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtCustEmail"
                ErrorMessage="<br />Please enter a valid Email ID" Display="Dynamic" runat="server"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td class="leftField" style="width: 20%">
            <asp:Label CssClass="FieldName" Text="DOB :" runat="server"></asp:Label>
        </td>
        <td class="rightField" style="width: 25%">
            <telerik:RadDatePicker ID="rdpDOB" CssClass="txtField" runat="server" Culture="English (United States)"
                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01">
                <calendar userowheadersasselectors="False" usecolumnheadersasselectors="False" viewselectortext="x"
                    skin="Telerik" enableembeddedskins="false">
                </calendar>
                <datepopupbutton imageurl="" hoverimageurl=""></datepopupbutton>
                <dateinput displaydateformat="d/M/yyyy" dateformat="d/M/yyyy">
                </dateinput>
            </telerik:RadDatePicker>
        </td>
        <td>
        </td>
    </tr>
    <tr id="trJointHolders" runat="server" visible="false">
    </tr>
    <tr id="trJointHoldersGrid" runat="server">
        <td colspan="2" align="center">
        </td>
    </tr>
    <tr id="trNomineesGrid" runat="server">
        <td colspan="2" align="center">
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr id="trJoint2" runat="server">
        <td align="center" colspan="2">
        </td>
    </tr>
    <tr id="trNominee2" runat="server">
        <td colspan="2" align="center">
        </td>
    </tr>
</table>
<br />
<div style="padding: 4px;">
    <telerik:RadMultiPage ID="AssociateMultiPage" EnableViewState="true" runat="server"
        SelectedIndex="0">
        <telerik:RadPageView ID="RadPageView2" runat="server">
            <div>
            </div>
        </telerik:RadPageView>
        <telerik:RadPageView ID="RadPageView3" runat="server">
            <div>
            </div>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
</div>
<div>
    <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerMFAccountAdd_btnSubmit', 'S');"
        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerMFAccountAdd_btnSubmit', 'S');"
        Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return isValid()" ValidationGroup="btnSubmit" />
    <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerMFAccountAdd_btnUpdate', 'S');"
        onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerMFAccountAdd_btnUpdate', 'S');"
        Text="Update" OnClick="btnUpdate_Click" OnClientClick="return isValid()" ValidationGroup="btnSubmit" />
</div>
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="hdnCustomerName" runat="server" />
<asp:HiddenField ID="hdnAssociationIdForNominee" runat="server" />
<asp:HiddenField ID="hdnAssociationIdForGuardian" runat="server" />
<asp:HiddenField ID="hdnAssociationIdForJointHolder" runat="server" />
<asp:HiddenField ID="hdnIsMainPortfolio" runat="server" />
<asp:HiddenField ID="hdnIsCustomerLogin" runat="server" />
