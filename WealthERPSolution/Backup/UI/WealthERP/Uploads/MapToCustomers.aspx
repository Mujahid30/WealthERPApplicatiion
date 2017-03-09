<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MapToCustomers.aspx.cs"
    Inherits="WealthERP.Uploads.MapToCustomers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<script language="javascript" type="text/javascript">
    var isClose = false;
    //this code will handle the F5 or Ctrl+F5 key



    //need to handle more cases like ctrl+R whose codes are not listed here
    document.onkeydown = checkKeycode
    function checkKeycode(e) {
        var keycode;
        if (window.event)
            keycode = window.event.keyCode;
        else if (e)
            keycode = e.which;
        if (keycode == 116) {
            isClose = true;
        }
    }
//    function somefunction() {
//        isClose = true;
//    }
//    function doUnload() {
//        if (!isClose) {
//            window.opener.Reprocess();
//        }
//    }


//    function ClosePopUp() {

//        window.close();
//    }

    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    }
</script>

<head runat="server">
    <title>Map To Customer</title>
    <style>
        .maroon
        {
            font-size: 12px;
            color: Maroon;
        }
    </style>
</head>
<body class="TDBackground" onbeforeunload="doUnload()" onmousedown="somefunction()">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divheader" runat="server">
        <table class="TDBackground" width="100%">
            <tr>
                <td>
                    <asp:RadioButton ID="rdbtnMapFolio" runat="server" Text="Map Folio to Existing Customer"
                        GroupName="rdbtngpMain" CssClass="txtField" OnCheckedChanged="rdbtnMapFolio_CheckedChanged"
                        AutoPostBack="true" />
                    <asp:RadioButton ID="rdbtnCreateNewCust" runat="server" GroupName="rdbtngpMain" Text="Create new Customer and Map Folio"
                        CssClass="txtField" AutoPostBack="true" OnCheckedChanged="rdbtnCreateNewCust_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblMessage" runat="server" Text="" Visible="false" class="maroon"></asp:Label>
                    <%--<asp:HyperLink ID="hlClose" runat="server" class="maroon" NavigateUrl="#" onClick="javascript:window.close();return false;"><br />Close Window</asp:HyperLink>
               --%>
                    <asp:HyperLink Visible="false" ID="hlClose" runat="server" class="maroon" NavigateUrl="#" onClick="return ClosePopUp()"><br />Close Window</asp:HyperLink>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMapToCustomer" runat="server" visible="false">
        <table class="TDBackground" width="100%">
            <tr>
                <td>
                    <table style="border: solid 1px maroon" width="60%" runat="server" id="tblSearch">
                        <tr>
                            <td class="FieldName" style="height: 40px">
                                Customer Name
                            </td>
                            <td>
                                <%--                                <asp:TextBox ID="txtCustomerName" CssClass="txtField" runat="server"></asp:TextBox>
--%>
                            </td>
                            <td>
                                <%--<asp:Button ID="btnSearch" runat="server" CssClass="PCGButton" Text="Search" OnClick="btnSearch_Click" />--%>
                            </td>
                            <td class="rightField" style="width: 20%">
                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                                    onclientClick="ShowIsa()" AutoPostBack="True">
                                </asp:TextBox><span id="spnCustomer" class="spnRequiredField">*</span>
                                <cc1:TextBoxWatermarkExtender ID="txtCustomer_water" TargetControlID="txtCustomerName"
                                    WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
                                </cc1:TextBoxWatermarkExtender>
                                <ajaxtoolkit:autocompleteextender id="txtCustomerName_autoCompleteExtender" runat="server"
                                    targetcontrolid="txtCustomerName" servicemethod="GetCustomerName" servicepath="~/CustomerPortfolio/AutoComplete.asmx"
                                    minimumprefixlength="1" enablecaching="False" completionsetcount="5" completioninterval="100"
                                    completionlistcssclass="AutoCompleteExtender_CompletionList" completionlistitemcssclass="AutoCompleteExtender_CompletionListItem"
                                    completionlisthighlighteditemcssclass="AutoCompleteExtender_HighlightedItem"
                                    usecontextkey="True" onclientitemselected="GetCustomerId" delimitercharacters=""
                                    enabled="True" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCustomerName"
                                    ErrorMessage="<br />Please Enter Customer Name" Display="Dynamic" runat="server"
                                    CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <span style="font-size: 11px;" class="maroon">Enter few characters of First Name or
                                    Last Name or Both.</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:GridView ID="gvCustomers" runat="server" CellPadding="4" CssClass="GridViewStyle"
                        AllowSorting="True" AutoGenerateColumns="False" EmptyDataText="No customers found."
                        OnRowCommand="gvCustomers_RowCommand" DataKeyNames="CustomerId">
                        <RowStyle CssClass="RowStyle" />
                        <FooterStyle CssClass="FooterStyle" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <EditRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle " />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:BoundField DataField="PANNum" HeaderText="Pan No" />
                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <ContentTemplate>
                                            <fieldset>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="mapToCusomer"
                                                    CommandArgument='<%# Bind("CustomerId") %>' Text="Map to Customer"></asp:LinkButton>
                                            </fieldset>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblRefine" class="maroon" runat="server" Text="More than 100 customers found,please refine query."
                        Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divCreateNewCustomer" runat="server" visible="false">
        <table class="TableBackground" style="width: 100%">
            <tr>
                <td colspan="2" class="tdRequiredText">
                    <label id="lbl" class="lblRequiredText">
                        Note: Fields marked with ' * ' are compulsory</label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" style="width: 20%">
                    <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:RadioButton ID="rbtnIndividual" runat="server" CssClass="txtField" Text="Individual"
                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnIndividual_CheckedChanged" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="rbtnNonIndividual" runat="server" CssClass="txtField" Text="Non Individual"
                        GroupName="grpCustomerType" AutoPostBack="true" OnCheckedChanged="rbtnNonIndividual_CheckedChanged" />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAdviserBranchList" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlAdviserBranchList_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="ddlAdviserBranchList_CompareValidator2" runat="server"
                        ControlToValidate="ddlAdviserBranchList" ErrorMessage="Please select a Branch"
                        Operator="NotEqual" ValueToCompare="Select a Branch" CssClass="cvPCG" Display="Dynamic">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="Select RM:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAdviseRMList" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span8" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAdviseRMList"
                        ErrorMessage=" " Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG"
                        Display="Dynamic">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCustomerSubType" runat="server" CssClass="cmbField">
                    </asp:DropDownList>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCustomerSubType"
                        ErrorMessage="Please select a Customer Sub-Type" Operator="NotEqual" ValueToCompare="Select a Sub-Type"
                        CssClass="cvPCG" Display="Dynamic"></asp:CompareValidator>
                </td>
            </tr>
            <%--    <tr>
        <td class="leftField">
            <asp:Label ID="lblAssetInterest" runat="server" CssClass="FieldName" Text="Asset Interest:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAssetInterest" CssClass="cmbField" runat="server">
                <asp:ListItem>Select an Asset Interest</asp:ListItem>
                <asp:ListItem>MF</asp:ListItem>
                <asp:ListItem>Equity</asp:ListItem>
                <asp:ListItem>Both</asp:ListItem>
            </asp:DropDownList>
            <span id="Span2" class="spnRequiredField">*</span>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlAssetInterest"
                ErrorMessage="Please select an Asset Interest" Operator="NotEqual" ValueToCompare="Select an Asset Interest"
                CssClass="cvPCG"></asp:CompareValidator>
        </td>
    </tr>--%>
            <tr id="trIndividualName" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblName" runat="server" CssClass="FieldName" Text="First Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFirstNameCreation" runat="server" CssClass="txtField"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtFirstNameCreation"
                        ErrorMessage="<br />Please enter the First Name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <cc1:TextBoxWatermarkExtender ID="txtWMFirstNameCreation" runat="server" TargetControlID="txtFirstNameCreation"
                        WatermarkText="Enter Middle Name">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr id="trIndividualMiddleName" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Middle Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtWMMiddleName" runat="server" TargetControlID="txtMiddleName"
                        WatermarkText="Enter Middle Name">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr id="trIndividualLastName" runat="server">
                <td class="leftField">
                    <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Last Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLastNameCreation" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtWMLastNameCreation" runat="server" TargetControlID="txtLastNameCreation"
                        WatermarkText="Enter Last Name">
                    </cc1:TextBoxWatermarkExtender>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblPanNum" runat="server" CssClass="FieldName" Text="PAN Number:"></asp:Label>
                </td>
                <td class="rightField" width="27%">
                    <asp:TextBox ID="txtPanNumber" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender runat="server" TargetControlID="txtPanNumber" WatermarkText="Enter PAN No.">
                    </cc1:TextBoxWatermarkExtender>
                    <span id="Span6" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvPanNumber" ControlToValidate="txtPanNumber" ErrorMessage="Please enter a PAN Number"
                        Display="Dynamic" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:Label ID="lblPanDuplicate" runat="server" CssClass="Error" Text="PAN Number already exists"></asp:Label>
                </td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <td>
                            <asp:CheckBox CssClass="cmbField" ID="chkUseDummyPan" runat="server" Text="Generate Dummy Pan"
                                AutoPostBack="true" OnCheckedChanged="chkUseDummyPan_CheckedChanged" />
                            <%-- <asp:RadioButton ToolTip="Pan Not Available" CssClass="txtField" runat="server" ID="rbtnNo"
                                Text="No" Checked="true" GroupName="btnDummyPan" OnCheckedChanged="rbtnHavePanNo_CheckedChanged"
                                AutoPostBack="true" />
                            <asp:RadioButton AutoPostBack="true" ToolTip="Pan Available" CssClass="txtField"
                                runat="server" ID="rbtnYes" Text="Yes" OnCheckedChanged="rbtnHavePanNo_CheckedChanged"
                                GroupName="btnDummyPan" />--%>
                        </td>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </tr>
            <tr id="trNonIndividualName" runat="server">
                <td class="leftField">
                    <asp:Label ID="lblCompanyName" runat="server" CssClass="FieldName" Text="Company Name:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyName" CssClass="txtField" runat="server"></asp:TextBox>
                    <span id="Span4" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCompanyName"
                        ErrorMessage="Please enter the Company Name" Display="Dynamic" runat="server"
                        CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="lblEmail" runat="server" CssClass="FieldName" Text="Email Id:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="txtField"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtWMEmail" runat="server" TargetControlID="txtEmail"
                        WatermarkText="Enter Email Id">
                    </cc1:TextBoxWatermarkExtender>
                    <%--  <span id="Span5" class="spnRequiredField">*</span>
            <br />
           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEmail"
                ErrorMessage="Please enter an Email Id" Display="Dynamic" runat="server" CssClass="rfvPCG">
            </asp:RequiredFieldValidator>--%>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                        ErrorMessage="Please enter a valid Email ID" Display="Dynamic" runat="server"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" CssClass="revPCG"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table class="TableBackground" style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                        CssClass="PCGButton" />
                </td>
            </tr>
        </table>
    </div>
    
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
    </form>
</body>
</html>
