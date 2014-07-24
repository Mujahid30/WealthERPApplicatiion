<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineSchemeSetUp.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineSchemeSetUp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        width: 28%;
    }
</style>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
    function openpopupSchemeSetUp() {

        var hdnSchemePlan = document.getElementById("<%=hdnSchemePlanCode.ClientID %>").value;
        //        alert(hdnSchemePlan);
        window.open('PopUp.aspx?PageId=AddSchemeMapping&OnlineSchemeSetupSchemecode=' + hdnSchemePlan, 'mywindow', 'width=750,height=500,scrollbars=yes,location=no');



        return false;
    }
   
</script>

<script type="text/javascript" language="javascript">
    function DateValue() {

        document.getElementById('ctrl_OnlineSchemeSetUp_gvSIPDetails_ctl00_ctl05_txtstartDate').innerHTML = 'hello';
        alert(masterTable);
    }
    function CheckOnline() {
        if (document.getElementById('<%=chkonline.ClientID%>').checked == false) {
            var con = confirm("Are you sure about the change?\n if you uncheck scheme will be offline.!!");

            if (con == true) {
                document.getElementById('<%=chkonline.ClientID%>').checked = false;
                return true;
            }
            if (con == false) {
                document.getElementById('<%=chkonline.ClientID%>').checked = true;
                return false;
            }
        }
    }

</script>

<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            Scheme Setup
                        </td>
                        <td>
                        </td>
                        <td align="right" class="style1">
                            <asp:LinkButton runat="server" ID="lbBack" CssClass="LinkButtons" Text="Edit" Visible="false"
                                OnClick="lbBack_OnClick"></asp:LinkButton>
                        </td>
                        <td style="width: 8%;">
                            <asp:LinkButton runat="server" ID="lblBack" CssClass="LinkButtons" Text="Back" Visible="false"
                                OnClick="lbBack1_OnClick"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    </table>
    <table id="tblMessage" width="100%" runat="server" visible="false" style="padding-top: 0px;">
        <tr id="trSumbitSuccess">
            <td align="center">
                <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                </div>
            </td>
        </tr>
    </table>
    <div>
        <table width="100%">
            <tr>
                <td colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Basic Details
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" width="25%">
                    <asp:Label ID="lblProduct" runat="server" Text="Product:" CssClass="FieldName"> </asp:Label>
                </td>
                <td width="25%">
                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Text="Mutual Funds" Value="MF" />
                    </asp:DropDownList>
                    <span id="Span26" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlProduct" runat="server" ErrorMessage="Please Select Product"
                        CssClass="rfvPCG" ControlToValidate="ddlProduct" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td align="right" width="10%">
                    <asp:Label ID="lblAmc" runat="server" Text="AMC:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlAmcCode_SelectedIndexChanged" Style="width: 300px;">
                        <%--  <asp:ListItem Text="Select" Value="Select" Selected="true" />--%>
                    </asp:DropDownList>
                    <span id="Span27" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlAmc" runat="server" ErrorMessage="Please Select AMC"
                        CssClass="rfvPCG" ControlToValidate="ddlAmc" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblcategory" runat="server" Text="Category:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlcategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
                        <%-- <asp:ListItem Selected="false" Value="0">--SELECT--</asp:ListItem>--%>
                    </asp:DropDownList>
                    <span id="Span28" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlcategory" runat="server" ErrorMessage="Please Select Category"
                        CssClass="rfvPCG" ControlToValidate="ddlcategory" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblScategory" runat="server" Text="Sub Category:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlScategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlScategory_OnSelectedIndexChanged" Style="width: 300px;">
                        <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    </asp:DropDownList>
                    <span id="Span29" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlScategory" runat="server" ErrorMessage="Please Select SubCategory"
                        CssClass="rfvPCG" ControlToValidate="ddlScategory" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblSScategory" runat="server" Text="Sub Sub Category:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSScategory" runat="server" CssClass="cmbField">
                        <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    </asp:DropDownList>
                    <span id="Span30" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlSScategory" runat="server" ErrorMessage="Please Select SubSubCategory"
                        CssClass="rfvPCG" ControlToValidate="ddlSScategory" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblScname" runat="server" Text="Scheme Name:" CssClass="FieldName"
                        Visible="true"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtScname" runat="server" CssClass="cmbFielde" Visible="true" Width="294"></asp:TextBox>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtScname" runat="server" ErrorMessage="Please Enter Scheme Name"
                        CssClass="rfvPCG" ControlToValidate="txtScname" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic"></asp:RequiredFieldValidator>
                    <%-- <asp:RequiredFieldValidator ID="rfvtxtScname" runat="server" ErrorMessage="Please Enter Scheme Name"
                    CssClass="rfvPCG" ControlToValidate="txtScname" ValidationGroup="btnbasicsubmit"
                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblRT" runat="server" Text="R&T:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlRT" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Text="Select" Value="Select" Selected="False" />
                        <%--<asp:ListItem Text="Select" Value="Select" Selected="true"/>--%>
                        <%-- <asp:ListItem Text="Select" Value="Select" Selected="true" />
               <asp:ListItem Text="CAMS" Value="CAMS"></asp:ListItem>
                <asp:ListItem Text="Deutsche" Value="Deutsche" Enabled="false">
                </asp:ListItem>
                <asp:ListItem Text="Templeton" Value="Templeton">
                </asp:ListItem>
                <asp:ListItem Text="KARVY" Value="KARVY">
                </asp:ListItem>
                <asp:ListItem Text="Sundaram" Value="Sundaram">
                </asp:ListItem>--%>
                    </asp:DropDownList>
                    <span id="Span31" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlRT" runat="server" ErrorMessage="Please Select R&T"
                        CssClass="rfvPCG" ControlToValidate="ddlRT" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblToadd" runat="server" Text="Do You Wish To Add:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    &nbsp;
                    <asp:CheckBox ID="chkonline" AutoPostBack="true" runat="server" Text="Online  Details"
                        CssClass="FieldName" OnCheckedChanged="oncheckedOnlin_OnCheckedChanged" onClick="CheckOnline();"/>
                    <asp:Label ID="lblProductCode" runat="server" Text="Product Code:" CssClass="FieldName"
                        Visible="false"></asp:Label>
                    <asp:TextBox ID="txtProductCode" runat="server" CssClass="cmbFielde" Width="294"
                        Visible="false"></asp:TextBox>
                    <%--  <td><asp:TextBox ID="txtAddNewScheme" runat="server"></asp:TextBox></td>--%>
            </tr>
            <tr>
                <%--  <td align="right">
                    <asp:Label ID="LbllISactive" runat="server" Text="Is Active:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <%--<asp:CheckBox ID="ChkISactive" runat="server" Text="Yes" CssClass="FieldName" />--%>
                <%--ID="imgBtnAddBank" ImageUrl="~/Images/user_add.png" runat="server"
                ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()" Height="15px"
                Width="15px"></asp:ImageButton>--%>
                <%--  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="Scheme Mapping"
                        OnClientClick="return openpopupSchemeSetUp()" Visible="false"></asp:LinkButton>
                </td>--%>
                <td align="right">
                    <asp:Label ID="Label9" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlNFoStatus" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlNFoStatus_OnSelectedIndexChanged"
                        AutoPostBack="true">
                        <asp:ListItem Text="Select" Value="Select" />
                        <asp:ListItem Text="NFO" Value="NFO">
                        </asp:ListItem>
                        <asp:ListItem Text="Active" Value="Active">
                        </asp:ListItem>
                        <asp:ListItem Text="Liquidated" Value="Liquidated" Enabled="false">
                        </asp:ListItem>
                        <asp:ListItem Text="Merged" Value="Merged" Enabled="false">
                        </asp:ListItem>
                        <asp:ListItem Text="Close NFO" Value="CloseNFO" Enabled="false">
                        </asp:ListItem>
                    </asp:DropDownList>
                    <span id="Span40" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="ReqddlNFoStatus" runat="server" ErrorMessage="Please Select Status"
                        CssClass="rfvPCG" ControlToValidate="ddlNFoStatus" ValidationGroup="btnbasicsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <%-- CssClass="FieldName" OnCheckedChanged="oncheckedOnlin_OnCheckedChanged" Checked="false"/>--%>
                <td align="right">
                    <asp:Label ID="lblAllproductcode" runat="server" CssClass="FieldName" Text="Product Code:"
                        Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:LinkButton ID="lnkProductcode" runat="server" Text="Add Productcode" CssClass="LinkButtons"
                        OnClick="lnkProductcode_OnClick" Visible="false"></asp:LinkButton>
                </td>
            </tr>
            <tr id="trNFODate" runat="server" visible="false">
                <td align="right">
                    <asp:Label ID="lblNfostartdate" runat="server" Text="NFO Start Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtNFOStartDate" CssClass="txtField" runat="server" Culture="English (United States)"
                        AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                        MinDate="1900-01-01" TabIndex="5">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span7" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="appRecidRequiredFieldValidator" ControlToValidate="txtNFOStartDate"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select NFO Date" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblNfoEnddate" runat="server" Text="NFO End Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtNFOendDate" CssClass="txtField" runat="server" Culture="English (United States)"
                        AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                        MinDate="1900-01-01" TabIndex="5">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <span id="Span1" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNFOStartDate"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select NFO End Date" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtNFOendDate"
                        ErrorMessage="<br/> NFO END Date should be greater than from date" Type="Date"
                        Operator="GreaterThanEqual" ControlToCompare="txtNFOStartDate" CssClass="cvPCG"
                        ValidationGroup="btnsubmit" Display="Dynamic">
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblSchemeStartDate" runat="server" Text="Scheme Re-Start Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtSchemeStartDate" CssClass="txtField" runat="server"
                        Culture="English (United States)" AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false"
                        ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="5">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                    <%-- <span id="Span24" class="spnRequiredField">*</span>
                    <asp:RequiredFieldValidator ID="reqSchemeStartDate" ControlToValidate="txtSchemeStartDate"
                        CssClass="rfvPCG" ErrorMessage="<br />Please select scheme start date" Display="Dynamic"
                        runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator--%>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtSchemeStartDate"
                        ErrorMessage="<br/> NFO Scheme Start Date should be greater than from NFO end date"
                        Type="Date" Operator="GreaterThan" ControlToCompare="txtNFOendDate" CssClass="cvPCG"
                        ValidationGroup="btnsubmit" Display="Dynamic">
                    </asp:CompareValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblMaturityDate" runat="server" Text="Maturity Date:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtMaturityDate" CssClass="txtField" runat="server" Culture="English (United States)"
                        AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade"
                        MinDate="1900-01-01" TabIndex="5">
                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                            Skin="Telerik" EnableEmbeddedSkins="false">
                        </Calendar>
                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                        <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                        </DateInput>
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblAcode" runat="server" Text="AMFI Code:" CssClass="FieldName" Visible="true"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAMFI" runat="server" CssClass="cmbFielde" Visible="true"></asp:TextBox>
                </td>
                <td align="right" style="width: 10%;">
                    <asp:Label ID="lblSchemeplancode" runat="server" Text="System Code:" CssClass="FieldName"
                        Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblschemeplanecodetext" runat="server" CssClass="FieldName" Visible="false"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lnkMargeScheme" runat="server" Text="Merge Scheme" OnClick="lnkMargeScheme_Click"
                        CssClass="LinkButtons" Visible="false"></asp:LinkButton>
                    <%-- <asp:CheckBox ID="chkoffline" runat="server" Text="Offline Scheme" CssClass="FieldName" Checked="false"/>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="Label4" runat="server" Text="Scheme:" CssClass="FieldName" Visible="false"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSchemeList" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlSchemeList_OnSelectedIndexChanged" Style="width: 300px;"
                        Visible="false">
                        <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:Button ID="btnBasicDSubmit" runat="server" Text="Submit" CssClass="PCGButton"
                        OnClick="btnBasicDSubmit_click" ValidationGroup="btnbasicsubmit" />
                </td>
                <td>
                    <asp:Button ID="btnBasicDupdate" runat="server" Text="Update" CssClass="PCGButton"
                        Visible="false" OnClick="btnBasicDupdate_click" ValidationGroup="btnbasicsubmit" />
                </td>
            </tr>
        </table>
    </div>
    <div id="schemedetails" runat="server" visible="false">
        <table width="100%">
            <tr>
                <td colspan="5">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        Scheme Details &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CssClass="LinkButtons" OnClick="lnkEdit_OnClick"
                            Visible="false"></asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="right" width="25%">
                    <asp:Label ID="lblESSchemecode" runat="server" Text="External System Scheme Code:"
                        CssClass="FieldName"></asp:Label>
                </td>
                <td width="25%">
                    <asp:TextBox ID="txtESSchemecode" runat="server" CssClass="cmbFielde" Enabled="true"> </asp:TextBox>
                    <%--  <span id="Span37" class="spnRequiredField">*</span>
                <br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please Enter External System Scheme Code"
                    CssClass="rfvPCG" ControlToValidate="txtESSchemecode" ValidationGroup="btnsubmit"
                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>--%>
                </td>
                <td align="right" width="10%">
                    <asp:Label ID="lblSecuritycode" runat="server" Text="Security Code:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSecuritycode" runat="server" CssClass="cmbFielde" Style="margin-left: 0px"></asp:TextBox>
                    <%-- <span id="Span24" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtSecuritycode" runat="server" ErrorMessage="Please Enter Security Code"
                        CssClass="rfvPCG" ControlToValidate="txtSecuritycode" ValidationGroup="btnsubmit"
                        Display="Dynamic"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblSctype" runat="server" Text="Scheme Type:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSctype" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Text="Select" Value="Select" />
                        <asp:ListItem Text="Open Ended" Value="OE" />
                        <asp:ListItem Text="Close Ended" Value="CE" />
                    </asp:DropDownList>
                    <span id="Span32" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlSctype" runat="server" ErrorMessage="Please Select Scheme Type"
                        CssClass="rfvPCG" ControlToValidate="ddlSctype" ValidationGroup="btnsubmit" Display="Dynamic"
                        InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblOption" runat="server" Text="Option:" CssClass="FieldName"> </asp:Label>
                    <asp:Label ID="lblDFrequency" runat="server" Text="Dividend Reinvestment flag" CssClass="FieldName"
                        Visible="false"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlOption" runat="server" CssClass="cmbField" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlOption_OnSelectedIndexChanged">
                        <%--<asp:ListItem Text="Select" Value="Select" Selected="false" />
                        <asp:ListItem Text="Dividend" Value="DV" />
                        <asp:ListItem Text="Growth" Value="GR" />--%>
                    </asp:DropDownList>
                    <span id="Span33" class="spnRequiredField">*</span>
                    <asp:DropDownList ID="ddlDFrequency" runat="server" CssClass="cmbField" AutoPostBack="false"
                        Visible="false">
                        <%--        <asp:ListItem Text="Select" Value="Select" Selected="False" />
                            <asp:ListItem Text="Dividend Reinvestment" Value="DVR" />
                            <asp:ListItem Text="Dividend Payout" Value="DVP" />--%>
                    </asp:DropDownList>
                    <asp:Label ID="lblddlDFrequency" runat="server" Text="*" CssClass="spnRequiredField"
                        Visible="false"></asp:Label>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlOption" runat="server" ErrorMessage="Please Select any option"
                        CssClass="rfvPCG" ControlToValidate="ddlOption" ValidationGroup="btnsubmit" Display="Dynamic"
                        InitialValue="Select"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please Select Dividend type"
                        CssClass="rfvPCG" ControlToValidate="ddlDFrequency" ValidationGroup="btnsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblBname" runat="server" Text="Bank Name:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlBname" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <%--<asp:ListItem Text="Select" Value="Select" Selected="true" />--%>
                    </asp:DropDownList>
                    <span id="Span9" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlBname" runat="server" ErrorMessage="Please Select Bank Name"
                        CssClass="rfvPCG" ControlToValidate="ddlBname" ValidationGroup="btnsubmit" Display="Dynamic"
                        InitialValue="Select"></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblBranch" runat="server" Text="Bank Branch:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBranch" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <span id="Span10" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtBranch" runat="server" ErrorMessage="Please Enter Bank Branch"
                        CssClass="rfvPCG" ControlToValidate="txtBranch" ValidationGroup="btnsubmit" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblACno" runat="server" Text="Bank Account Number:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtACno" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <span id="Span11" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtACno" runat="server" ErrorMessage="Please Enter A/C no"
                        CssClass="rfvPCG" ControlToValidate="txtACno" ValidationGroup="btnsubmit" Display="Dynamic"
                        InitialValue=""></asp:RequiredFieldValidator>
                </td>
                <td align="right">
                    <asp:Label ID="LalISnfo" runat="server" Text="Is NFO:" CssClass="FieldName" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkInfo" runat="server" Text="Yes" CssClass="FieldName" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblLIperiod" runat="server" Text="Lock In Period:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLIperiod" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <%-- <span id="Span23" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtLIperiod" runat="server" ErrorMessage="Please Enter Lock in period"
                        CssClass="rfvPCG" ControlToValidate="txtLIperiod" ValidationGroup="btnsubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtLIperiod" ControlToValidate="txtLIperiod"
                        runat="server" ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*$" ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>--%>
                </td>
                <td align="right">
                    <asp:Label ID="lblCOtime" runat="server" Text="Cut Off Time:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtHH" runat="server" CssClass="cmbFielde" Width="20px" AutoPostBack="false"></asp:TextBox>
                    <asp:Label ID="Label1" runat="server" Text="HH" CssClass="FieldName"></asp:Label>
                    <asp:TextBox ID="txtMM" runat="server" CssClass="cmbFielde" Width="20px" AutoPostBack="false"></asp:TextBox>
                    <asp:Label ID="Label2" runat="server" Text="MM" CssClass="FieldName"></asp:Label>
                    <asp:TextBox ID="txtSS" runat="server" CssClass="cmbFielde" Width="20px" AutoPostBack="false"></asp:TextBox>
                    <asp:Label ID="Label3" runat="server" Text="SS" CssClass="FieldName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblMaxinvestment" runat="server" Text="Max Investment:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtinvestment" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="regtxtinvestment" ControlToValidate="txtinvestment"
                        runat="server" ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*$" ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td colspan="2" align="center">
                    <asp:RequiredFieldValidator ID="rfvtxtHH" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Hour"
                        Display="Dynamic" ControlToValidate="txtHH" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtHH" ControlToValidate="txtHH" runat="server"
                        ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG" ValidationExpression="[0-9]\d*$"
                        ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvtxtMM" ControlToValidate="txtMM" ErrorMessage="Please Enter Minute"
                        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtMM" ControlToValidate="txtMM" runat="server"
                        ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG" ValidationExpression="[0-9]\d*$"
                        ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvtxtSS" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Second"
                        Display="Dynamic" ControlToValidate="txtSS" ValidationGroup="btnsubmit">
                    
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtSS" ControlToValidate="txtSS" runat="server"
                        ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG" ValidationExpression="[0-9]\d*$"
                        ValidationGroup="btnsubmit">
                    </asp:RegularExpressionValidator>
                    <asp:RangeValidator ID="rngtxtHH" runat="server" Type="Integer" MinimumValue="0"
                        CssClass="rfvPCG" MaximumValue="24" ControlToValidate="txtHH" ErrorMessage="HH must be between 0 to 23"
                        ValidationGroup="btnsubmit"></asp:RangeValidator>
                    <asp:RangeValidator ID="rngtxtMM" runat="server" Type="Integer" MinimumValue="0"
                        CssClass="rfvPCG" MaximumValue="60" ControlToValidate="txtMM" ErrorMessage="MIN must be between 0 to 59"
                        ValidationGroup="btnsubmit"></asp:RangeValidator>
                    <asp:RangeValidator ID="rngtxtSS" runat="server" Type="Integer" MinimumValue="0"
                        CssClass="rfvPCG" MaximumValue="60" ControlToValidate="txtSS" ErrorMessage="SEC must be between 0 to 59"
                        ValidationGroup="btnsubmit"></asp:RangeValidator>
                </td>
            </tr>
            <tr>
                <%-- <td align="right">
            </td>
            <td>
            </td>--%>
                <td align="right">
                    <asp:Label ID="lblGenerationfreq" runat="server" Text="File Generation Freq:" CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlGenerationfreq" runat="server" CssClass="cmbField" AutoPostBack="false">
                        <asp:ListItem Text="Select" Value="Select" Selected="false" />
                    </asp:DropDownList>
                    <%-- <span id="Span36" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvddlGenerationfreq" runat="server" ErrorMessage="Please select file generation freq."
                        CssClass="rfvPCG" ControlToValidate="ddlGenerationfreq" ValidationGroup="btnsubmit"
                        Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>--%>
                </td>
                <td align="right">
                    <asp:Label ID="lblFvalue" runat="server" Text="Face Value:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFvale" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <%-- <span id="Span12" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtFvale" runat="server" ErrorMessage="Please Enter Face value"
                        CssClass="rfvPCG" ControlToValidate="txtFvale" ValidationGroup="btnsubmit" Display="Dynamic"
                        InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtFvale" ControlToValidate="txtFvale" runat="server"
                        ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG" ValidationExpression="[1-9]\d*$"
                        ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblEload" runat="server" Text="Entry Load %:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEload" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <span id="Span14" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtEload" runat="server" ErrorMessage="Please Enter Entry Load"
                        CssClass="rfvPCG" ControlToValidate="txtEload" ValidationGroup="btnsubmit" Display="Dynamic"
                        InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtEload" ControlToValidate="txtEload" runat="server"
                        ErrorMessage="Enter Only Number" Display="Dynamic" CssClass="cvPCG" ValidationExpression="[0-9]\d*(\.\d?[0-9])?$"
                        ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblELremark" runat="server" Text="Entry Load Remark:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtELremark" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <span id="Span15" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtELremark" runat="server" ErrorMessage="Please Enter Load Remark"
                        CssClass="rfvPCG" ControlToValidate="txtELremark" ValidationGroup="btnsubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <%-- <asp:RegularExpressionValidator ID="regtxtELremark" ControlToValidate="txtELremark"
                        ErrorMessage="Enter Only letters" runat="server" Display="Dynamic" CssClass="cvPCG"
                        ValidationExpression="[a-zA-Z ]*$" ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblExitLoad" runat="server" Text="Exit Load %:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExitLoad" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <span id="Span16" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtExitLoad" runat="server" ErrorMessage="Please Enter Exit Load"
                        CssClass="rfvPCG" ControlToValidate="txtExitLoad" ValidationGroup="btnsubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="regtxtExitLoad" ControlToValidate="txtExitLoad"
                        ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                        ValidationExpression="[0-9]\d*(\.\d?[0-9])?$" ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>
                </td>
                <td align="right">
                    <asp:Label ID="lblExitLremark" runat="server" Text="Exit Load Remark:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExitLremark" runat="server" CssClass="cmbFielde"></asp:TextBox>
                    <span id="Span25" class="spnRequiredField">*</span>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvtxtExitLremark" runat="server" ErrorMessage="Please Enter Exit Load Remark"
                        CssClass="rfvPCG" ControlToValidate="txtExitLremark" ValidationGroup="btnsubmit"
                        Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                    <%--  <asp:RegularExpressionValidator ID="regtxtExitLremark" ControlToValidate="txtExitLremark"
                        ErrorMessage="Enter Only letters" runat="server" Display="Dynamic" CssClass="cvPCG"
                        ValidationExpression="[a-zA-Z ]*$" ValidationGroup="btnsubmit">     
                    </asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="LalISPurchage" runat="server" Text="IS Purchase Available:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkISPurchage" runat="server" Text="Yes" CssClass="FieldName" OnCheckedChanged="oncheckedISpurchage_OnCheckedChanged"
                        AutoPostBack="true" />
                </td>
                <td align="right">
                    <asp:Label ID="LalISRedeem" runat="server" Text="Is Redeem Available:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkISRedeem" runat="server" Text="Yes" CssClass="FieldName" OnCheckedChanged="oncheckedredemavaliable_OnCheckedChanged"
                        AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="LalChkISSwitch" runat="server" Text="Is Switch Available:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkISSwitch" runat="server" Text="Yes" CssClass="FieldName" OnCheckedChanged="oncheckedSwtchAvaliable_OnCheckedChanged"
                        AutoPostBack="true" />
                </td>
                <td align="right">
                    <asp:Label ID="lblchkOnlineEnablement" runat="server" Text="OnlineEnablement:" CssClass="FieldName"></asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="chkOnlineEnablement" runat="server" Text="Yes" CssClass="FieldName" />
                </td>
            </tr>
            <tr id="trIPAmount" runat="server" visible="false">
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 25%;">
                                <asp:Label ID="lblInitalPamount" runat="server" Text="Min. New Purchase Amount:"
                                    CssClass="FieldName">
                                </asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ID="txtInitalPamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span6" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtInitalPamount" runat="server" ErrorMessage="Please Enter Min. New Purchase Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtInitalPamount" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regfvtxtInitalPamount" ControlToValidate="txtInitalPamount"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="^[0-9]*[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="CmptxtInitalPamount" ControlToValidate="txtInitalPamount" runat="server"
                ControlToCompare="txtAdditional" Display="Dynamic" ErrorMessage="<br/>From Range Less Than To Range"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                            <td align="right" style="width: 20%;">
                                <asp:Label ID="lblIMultipleamount" runat="server" Text="New Purchase Multiple Amount:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span13" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtIMultipleamount" runat="server" ErrorMessage="Please Enter Purchage Multiple Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtIMultipleamount" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regetxtIMultipleamount" ControlToValidate="txtIMultipleamount"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="CmptxtIMultipleamount" ControlToValidate="txtIMultipleamount" runat="server"
                ControlToCompare="txtAddMultipleamount" Display="Dynamic" ErrorMessage="<br/>From Range Less Than To Range"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="SetUpSubmit"></asp:CompareValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 24%;">
                                <asp:Label ID="lblAdditionalPamount" runat="server" Text="Min. Additional Purchase Amount:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ID="txtAdditional" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span7" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtAdditional" runat="server" ErrorMessage="Please Enter Min. Additional Purchase Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtAdditional" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regfvtxtAdditional" ControlToValidate="txtAdditional"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit" />
                                <%-- <asp:CompareValidator ID="cmptxtAdditional" ControlToValidate="txtAdditional" runat="server"
                ControlToCompare="txtIMultipleamount" Display="Dynamic" ErrorMessage="<br/>To Range Greater Than From Range"
                Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                            <td align="right" style="width: 20%;">
                                <asp:Label ID="lblAddMultipleamount" runat="server" Text="Additional Purchase Multiple Amount:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAddMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span5" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtAddMultipleamount" runat="server" ErrorMessage="Please Enter Additional Purchase Multiple Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtAddMultipleamount" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regetxtAddMultipleamount" ControlToValidate="txtAddMultipleamount"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit" />
                                <%-- <asp:CompareValidator ID="CmptxtAddMultipleamount" ControlToValidate="txtAddMultipleamount" runat="server"
                ControlToCompare="txtIMultipleamount" Display="Dynamic" ErrorMessage="<br/>To Range Greater Than From Range"
                Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trMINRedemPtion" runat="server" visible="false">
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 25%;">
                                <asp:Label ID="lblMinRedemption" runat="server" Text="Min Redemption Amount:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ID="txtMinRedemption" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span17" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="rfvtxtMinRedemption" runat="server" ErrorMessage="<br />Please Enter Min Redemption Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtMinRedemption" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegtxtMinRedemption" ControlToValidate="txtMinRedemption"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="cmptxtMinRedemption" ControlToValidate="txtMinRedemption" runat="server"
                ControlToCompare="txtRedemptionmultiple" Display="Dynamic" ErrorMessage="<br/>Please Enter less than redemption multiple amount"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                            <td align="right" style="width: 20%;">
                                <asp:Label ID="lblRedemptionmultiple" runat="server" Text="Redemption Multiple Amount:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRedemptionmultiple" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span18" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rvftxtRedemptionmultiple" runat="server" ErrorMessage="Please Enter Redemption Multiple Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtRedemptionmultiple" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rfvtxtRedemptionmultiple" ControlToValidate="txtRedemptionmultiple"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="cmptxtRedemptionmultiple" ControlToValidate="txtRedemptionmultiple" runat="server"
                ControlToCompare="txtMinRedemption" Display="Dynamic" ErrorMessage="<br/>Please Enter Greater than redemption multiple amount"
                Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%;">
                                <asp:Label ID="lblMinRedemptionUnits" runat="server" Text="Min Redemption Units:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ID="txtMinRedemptioUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span19" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtMinRedemptioUnits" runat="server" ErrorMessage="Please Enter Min Redemption Units"
                                    CssClass="rfvPCG" ControlToValidate="txtMinRedemptioUnits" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgetxtMinRedemptioUnits" ControlToValidate="txtMinRedemptioUnits"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%--<asp:CompareValidator ID="cmptxtRedemptionMultiplesUnits" ControlToValidate="txtMinRedemptioUnits" runat="server"
                ControlToCompare="txtRedemptionMultiplesUnits" Display="Dynamic" ErrorMessage="<br/>Please Enter less than redemption multiple units"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                            <td align="right" style="width: 20%;">
                                <asp:Label ID="lblRedemptionMultiplesUnits" runat="server" Text="Redemption Multiples Units:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRedemptionMultiplesUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span20" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtRedemptionMultiplesUnits" runat="server" ErrorMessage="Please Enter Redemption Multiples Units"
                                    CssClass="rfvPCG" ControlToValidate="txtRedemptionMultiplesUnits" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="rgetxtRedemptionMultiplesUnits" ControlToValidate="txtRedemptionMultiplesUnits"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="cmpttxtRedemptionMultiplesUnits" ControlToValidate="txtRedemptionMultiplesUnits" runat="server"
                ControlToCompare="txtMinRedemptioUnits" Display="Dynamic" ErrorMessage="<br/>Please Enter Greater than redemption units"
                Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trSwitchPavailable" runat="server" visible="false">
                <td colspan="4">
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 24%;">
                                <asp:Label ID="lblMinSwitchAmount" runat="server" Text="Min Switch Amount:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ID="txtMinSwitchAmount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span21" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtMinSwitchAmount" runat="server" ErrorMessage="Please Enter Min Switch Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtMinSwitchAmount" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regtxtMinSwitchAmount" ControlToValidate="txtMinSwitchAmount"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="cmptxtMinSwitchAmount" ControlToValidate="txtMinSwitchAmount" runat="server"
                ControlToCompare="txtSwitchMultipleAmount" Display="Dynamic" ErrorMessage="<br/>Please Enter less than switch multiple amount"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                            <td style="width: 20%;" align="right">
                                <asp:Label ID="lblSwitchMultipleAmount" runat="server" Text="Switch Multiple Amount:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSwitchMultipleAmount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span22" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtSwitchMultipleAmount" runat="server" ErrorMessage="Please Enter Switch Multiple Amount"
                                    CssClass="rfvPCG" ControlToValidate="txtSwitchMultipleAmount" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regetxtSwitchMultipleAmount" ControlToValidate="txtSwitchMultipleAmount"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtSwitchMultipleAmount" runat="server"
                ControlToCompare="txtMinSwitchAmount" Display="Dynamic" ErrorMessage="<br/>Please Enter Grater than min switch amount"
                Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 25%;">
                                <asp:Label ID="lblMinSwitchUnits" runat="server" Text="Min Switch Units:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td style="width: 15%;">
                                <asp:TextBox ID="txtMinSwitchUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span34" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtMinSwitchUnits" runat="server" ErrorMessage="Please Enter Min Switch Unit"
                                    CssClass="rfvPCG" ControlToValidate="txtMinSwitchUnits" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="reptxtMinSwitchUnits" ControlToValidate="txtMinSwitchUnits"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%-- <asp:CompareValidator ID="cmptxtMinSwitchUnits" ControlToValidate="txtMinSwitchUnits" runat="server"
                ControlToCompare="txtSwitchMultipleUnits" Display="Dynamic" ErrorMessage="<br/>Please Enter less than min switch units"
                Type="Integer" Operator="LessThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                            <td align="right" style="width: 20%;">
                                <asp:Label ID="lblSwitchMultipleUnits" runat="server" Text="Switch Multiples Units:"
                                    CssClass="FieldName"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtSwitchMultipleUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                                <span id="Span35" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtSwitchMultipleUnits" runat="server" ErrorMessage="Please Enter Switch Multiple Unit"
                                    CssClass="rfvPCG" ControlToValidate="txtSwitchMultipleUnits" ValidationGroup="btnsubmit"
                                    Display="Dynamic" InitialValue=""></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="regtxtSwitchMultipleUnits" ControlToValidate="txtSwitchMultipleUnits"
                                    ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                    ValidationExpression="[1-9]\d*$" ValidationGroup="btnsubmit">     
                                </asp:RegularExpressionValidator>
                                <%--<asp:CompareValidator ID="cmptxtSwitchMultipleUnits" ControlToValidate="txtSwitchMultipleUnits" runat="server"
                ControlToCompare="txtMinSwitchUnits" Display="Dynamic" ErrorMessage="<br/>Please Enter greater than multiple switch units"
                Type="Integer" Operator="GreaterThan" CssClass="cvPCG" ValidationGroup="btnsubmit"></asp:CompareValidator>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblNACustomerType" runat="server" Text="Not Allowed Customer Type:"
                        CssClass="FieldName"> </asp:Label>
                </td>
                <td>
                    <asp:CheckBox ID="ChkNRI" runat="server" Text="NRI" CssClass="FieldName" />
                    &nbsp;
                    <asp:CheckBox ID="ChkBO" runat="server" Text="BOI" CssClass="FieldName" />
                </td>
            </tr>
            <%-- <tr>
            <td colspan="5">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Systematic Details
                </div>
            </td>
        </tr>--%>
            <tr>
                <td align="right">
                    <asp:CheckBox ID="ChkISSIP" runat="server" Text="Is SIP Available" CssClass="FieldName"
                        AutoPostBack="true" OnCheckedChanged="ChkISSIP_OnCheckedChanged" Enabled="false" />
                </td>
                <td align="right">
                    <asp:CheckBox ID="ChkISSWP" runat="server" Text="Is SWP Available" AutoPostBack="true"
                        CssClass="FieldName" OnCheckedChanged="ChkISSWP_OnCheckedChanged" Visible="false" />
                </td>
                <td align="right">
                    <asp:CheckBox ID="ChkISSTP" runat="server" Text="Is STP Available" AutoPostBack="true"
                        CssClass="FieldName" OnCheckedChanged="ChkISSTP_OnCheckedChanged" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <%-- <tr>
           
        </tr>--%>
    <%--<tr id="trsystematic" runat="server" visible="false">
    <td colspan="5">
        <div class="divSectionHeading" style="vertical-align: text-bottom">
            Systematic Details
        </div>
    </td>
</tr>--%>
    <asp:Panel ID="pnlSIPDetails" runat="server" Width="68%" ScrollBars="Horizontal"
        Visible="false">
        <table width="100%">
            <tr>
                <td>
                    <telerik:RadGrid ID="gvSIPDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" ClientSettings-AllowColumnsReorder="false"
                        AllowAutomaticInserts="false" OnNeedDataSource="gvSIPDetails_OnNeedDataSource"
                        OnItemDataBound="gvSIPDetails_OnItemDataBound" OnItemCommand="gvSIPDetails_OnItemCommand">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="PASPSD_SystematicDetailsId,PASP_SchemePlanCode,XF_FrequencyCode"
                            AllowFilteringByColumn="true" Width="100%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="Top" EditMode="PopUp">
                            <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                                AddNewRecordText="Add New Systematic Details" ShowExportToCsvButton="false" ShowAddNewRecordButton="true"
                                ShowRefreshButton="false" />
                            <Columns>
                                <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                    UpdateText="Update" HeaderStyle-Width="80px">
                                </telerik:GridEditCommandColumn>
                                <telerik:GridBoundColumn DataField="XF_Frequency" HeaderText="Frequency" AllowFiltering="true"
                                    SortExpression="XF_Frequency" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="XF_Frequency" FooterStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="100px">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_SystematicDetailsId" HeaderText="SystematicDetailsId"
                                    AllowFiltering="true" SortExpression="PASPSD_SystematicDetailsId" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASPSD_SystematicDetailsId"
                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="100px" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_StatingDates" DataFormatString="{0:dd/MM/yyyy hh:mm:ss}"
                                    AllowFiltering="true" HeaderText="Start Date" UniqueName="PASPSD_StatingDates"
                                    SortExpression="PASPSD_StatingDates" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" HeaderStyle-Width="80px" FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MinDues" AllowFiltering="true" HeaderText="Min Dues"
                                    UniqueName="PASPSD_MinDues" SortExpression="PASPSD_MinDues" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="75px"
                                    FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MaxDues" AllowFiltering="true" HeaderText="Max Dues"
                                    UniqueName="PASPSD_MaxDues" SortExpression="PASPSD_MaxDues" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="75px"
                                    FilterControlWidth="50px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MinAmount" AllowFiltering="false" HeaderText="Min Amount"
                                    UniqueName="PASPSD_MinAmount" SortExpression="PASPSD_MinAmount" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                    FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MultipleAmount" HeaderText="Amount Multiplier"
                                    AllowFiltering="true" HeaderStyle-Wrap="false" SortExpression="PASPSD_MultipleAmount"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AutoPostBackOnFilter="true"
                                    HeaderStyle-Width="100px" UniqueName="PASPSD_MultipleAmount" FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn Visible="false" DataField="PASP_SchemePlanCode" HeaderText="SchemePlanName"
                                    AllowFiltering="true" SortExpression="PASP_SchemePlanCode" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" UniqueName="PASP_SchemePlanCode"
                                    FooterStyle-HorizontalAlign="Left" HeaderStyle-Width="300px">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings FormTableStyle-Height="10px" EditFormType="Template" FormTableStyle-Width="1000px"
                                PopUpSettings-Width="530px" PopUpSettings-Height="300px" CaptionFormatString="Systematic Details">
                                <FormTemplate>
                                    <table width="100%" style="background-color: White">
                                        <tr id="trCustomerTypeSelection" runat="server">
                                            <td colspan="4">
                                                <table width="100%">
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblFrequency" runat="server" CssClass="FieldName" Text="Frequency:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFrquency" runat="server" CssClass="cmbField" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                            <span id="Span38" class="spnRequiredField">*</span>
                                                            <asp:RequiredFieldValidator ID="reqddlFrquency" ControlToValidate="ddlFrquency" ErrorMessage="<br />Please select Frquency"
                                                                Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="Submit" InitialValue="Select"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" align="right">
                                                            <asp:Label ID="lblstartDate" runat="server" CssClass="FieldName" Text="Start Date:"></asp:Label>
                                                        </td>
                                                        <td class="rightField" align="right">
                                                            <asp:TextBox ID="txtstartDate" runat="server" CssClass="txtField" Text='<%# Bind("PASPSD_StatingDates") %>'
                                                                AutoPostBack="false"></asp:TextBox>
                                                            <span id="Span8" class="spnRequiredField">*<br></br>
                                                                Multiple entries to be separated by(;)like-[12;24]</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtstartDate"
                                                                ErrorMessage="<br />Please Enter Date" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                              <asp:RegularExpressionValidator ID="reqtxtstartDate" ControlToValidate="txtstartDate"
                                                                ErrorMessage=" </br>Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                                                ValidationExpression="^\d+(;\d+)*$" ValidationGroup="Submit">     
                                                            </asp:RegularExpressionValidator>
                                                            <%-- ^([0-9]{0,2})+(;[0-9]{0,2})*$--%>
                                                            <asp:CustomValidator ID="Custtxtstartdate" runat="server" ErrorMessage="</br>Start date should not be greater than 31st"
                                                                ControlToValidate="txtstartDate" EnableClientScript="true" Display="Dynamic"
                                                                OnServerValidate="txtstartDate_OnServerValidate" ValidationGroup="Submit" CssClass="rfvPCG">
                                                            </asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" align="right">
                                                            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Min Dues:"></asp:Label>
                                                        </td>
                                                        <td class="rightField">
                                                            <asp:TextBox ID="txtMinDues" runat="server" CssClass="txtField" Text='<%# Bind("PASPSD_MinDues") %>'
                                                                AutoPostBack="false"></asp:TextBox>
                                                            <span id="Span1" class="spnRequiredField">*</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtMinDues"
                                                                ErrorMessage="<br />Please Enter Min Dues" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="regtxtMinDues" ControlToValidate="txtMinDues"
                                                                ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                                                ValidationExpression="[1-9]\d*$" ValidationGroup="Submit">     
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" align="right">
                                                            <asp:Label ID="Label6" runat="server" CssClass="FieldName" Text="Max Dues:"></asp:Label>
                                                        </td>
                                                        <td class="rightField">
                                                            <asp:TextBox ID="txtMaxDues" runat="server" CssClass="txtField" Text='<%# Bind("PASPSD_MaxDues") %>'
                                                                AutoPostBack="false"></asp:TextBox>
                                                            <span id="Span2" class="spnRequiredField">* </span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtMaxDues"
                                                                ErrorMessage="<br />Please Enter Max Dues" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="reqtxtMaxDues" ControlToValidate="txtMaxDues"
                                                                ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                                                ValidationExpression="[1-9]\d*$" ValidationGroup="Submit">     
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" align="right">
                                                            <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Min Amount:"></asp:Label>
                                                        </td>
                                                        <td class="rightField">
                                                            <asp:TextBox ID="txtMinAmount" runat="server" CssClass="txtField" Text='<%# Bind("PASPSD_MinAmount") %>'
                                                                AutoPostBack="false"></asp:TextBox>
                                                            <span id="Span3" class="spnRequiredField">*</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtMinAmount"
                                                                ErrorMessage="<br />Please Enter Min Amount" Display="Dynamic" runat="server"
                                                                CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="reqtxtMinAmount" ControlToValidate="txtMinAmount"
                                                                ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                                                ValidationExpression="[1-9]\d*$" ValidationGroup="Submit">     
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" align="right">
                                                            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="Multiple Amount:"></asp:Label>
                                                        </td>
                                                        <td class="rightField">
                                                            <asp:TextBox ID="txtMultipleAmount" runat="server" CssClass="txtField" Text='<%# Bind("PASPSD_MultipleAmount") %>'
                                                                AutoPostBack="false"></asp:TextBox>
                                                            <span id="Span4" class="spnRequiredField">*</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtMultipleAmount"
                                                                ErrorMessage="<br />Please Enter Multiple Amount" Display="Dynamic" runat="server"
                                                                CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="reqtxtMultipleAmount" ControlToValidate="txtMultipleAmount"
                                                                ErrorMessage="Enter Only Number" runat="server" Display="Dynamic" CssClass="cvPCG"
                                                                ValidationExpression="[1-9]\d*$" ValidationGroup="Submit">     
                                                            </asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                ValidationGroup="Submit" ></asp:Button>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button2" Text="Cancel" runat="server" CausesValidation="False" CssClass="PCGButton"
                                                                CommandName="Cancel"></asp:Button>
                                                        </td>
                                                    </tr>
                                        </tr>
                                    </table>
                                    </td> </tr> </table>
                                </FormTemplate>
                            </EditFormSettings>
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
    <table>
        <tr>
            <td align="right">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnsubmit_click"
                    Visible="false" ValidationGroup="btnsubmit" />
            </td>
            <td align="left">
                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="PCGButton" OnClick="btnUpdate_click"
                    Style="height: 26px" ValidationGroup="btnsubmit" Visible="false" />
                <%-- ValidationGroup="btnsubmit"  ValidationGroup="btnsubmit"--%>
            </td>
        </tr>
    </table>
    <telerik:RadWindow ID="radwindowPopup" runat="server" VisibleOnPageLoad="false" Height="200px"
        Width="600px" Modal="true" BackColor="#4B4726" VisibleStatusbar="false" Behaviors="Close,Move"
        Title="Merge Scheme" Left="200" Top="200" Expanded="true" Visible="true">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="2" align="right">
                        <asp:LinkButton ID="lnkMargeEdit" runat="server" CssClass="FieldName" OnClick="lnkMargeEdit_Click"
                            Text="Edit" Visible="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblMargescheme" runat="server" Text="Merge To Scheme:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMargeScheme" runat="server" CssClass="cmbField" Width="450px">
                        </asp:DropDownList>
                        <span id="Span37" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="ReqmergeScheme" runat="server" ErrorMessage="Select Scheme To Merge"
                            CssClass="rfvPCG" ControlToValidate="ddlMargeScheme" ValidationGroup="btnsubmit1"
                            Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblMargedate" runat="server" Text="Merge Date:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="txtSchemeMargeDate" CssClass="txtField" runat="server"
                            Culture="English (United States)" AutoPostBack="false" Skin="Telerik" EnableEmbeddedSkins="false"
                            ShowAnimation-Type="Fade" MinDate="1900-01-01" TabIndex="5">
                            <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                Skin="Telerik" EnableEmbeddedSkins="false">
                            </Calendar>
                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                            <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                            </DateInput>
                        </telerik:RadDatePicker>
                        <span id="Span39" class="spnRequiredField">*</span>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Merge Date"
                            CssClass="rfvPCG" ControlToValidate="txtSchemeMargeDate" ValidationGroup="btnsubmit1"
                            Display="Dynamic" InitialValue="Select"></asp:RequiredFieldValidator>
                    </td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPStatus" runat="server" CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnMSSubmit" runat="server" Text="Submit" CssClass="PCGLongButton"
                            OnClick="btnMSSubmit_Click" ValidationGroup="btnsubmit1" />
                    </td>
                    <td>
                        <asp:Button ID="btnMSUpdate" runat="server" Text="Update" CssClass="PCGButton" Visible="false"
                            OnClick="btnMSUpdate_Click" ValidationGroup="btnsubmit1" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            CssClass="PCGButton" Visible="false" />
                        <asp:Button ID="btnReset" runat="server" Text="UnMerge" OnClick="btnReset_Click"
                            CssClass="PCGButton" Visible="false" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblNote" runat="server" CssClass="FieldName" Text="Note:"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblMergeNote" runat="server" Text="The merged scheme will be avaliable for transaction only till 1 PM </br> one business day prior to the stipulated merger date"
                            CssClass="FieldName"></asp:Label>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <telerik:RadWindow ID="radproductcode" runat="server" VisibleOnPageLoad="false" Height="380px"
        Width="300px" Modal="true" BackColor="#4B4726" VisibleStatusbar="false" Behaviors="None"
        Title="Add Productcode" Left="20" Top="20" Expanded="true" Visible="true">
        <ContentTemplate>
            <table>
                <tr>
                    <td align="center">
                        <telerik:RadGrid ID="gvproductcode" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="5" AllowPaging="True" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="True" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                            Width="100%" Skin="Telerik" AllowFilteringByColumn="true" EnableViewState="true"
                            OnNeedDataSource="gvproductcode_OnNeedDataSource" OnItemCommand="gvproductcode_OnItemCommand">
                            <MasterTableView DataKeyNames="PASM_Id,PASC_AMC_ExternalCode" AllowFilteringByColumn="true"
                                Width="120%" AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="Top"
                                EditMode="PopUp">
                                <CommandItemSettings ShowExportToWordButton="false" ShowExportToExcelButton="false"
                                    AddNewRecordText="Add New Productcode" ShowExportToCsvButton="false" ShowAddNewRecordButton="true"
                                    ShowRefreshButton="false" />
                                <Columns>
                                    <telerik:GridEditCommandColumn EditText="Edit" UniqueName="editColumn" CancelText="Cancel"
                                        UpdateText="Update" HeaderStyle-Width="80px">
                                    </telerik:GridEditCommandColumn>
                                    <telerik:GridBoundColumn DataField="PASC_AMC_ExternalCode" HeaderStyle-Width="50px"
                                        CurrentFilterFunction="Contains" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                        HeaderText="Product Code" UniqueName="PASC_AMC_ExternalCode" SortExpression="PASC_AMC_ExternalCode"
                                        AllowFiltering="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings FormTableStyle-Height="100px" EditFormType="Template" FormTableStyle-Width="100px"
                                    PopUpSettings-Width="300px" PopUpSettings-Height="100px" CaptionFormatString="Add product Code">
                                    <FormTemplate>
                                        <table width="100%">
                                            <tr id="trCustomerTypeSelection" runat="server">
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="right">
                                                                <asp:Label ID="lblgProductcode" runat="server" CssClass="FieldName" Text="Productcode:"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtgproductcode" runat="server" CssClass="cmbFielde" ValidationGroup="pductcodesubmit"
                                                                    Text='<%# Bind("PASC_AMC_ExternalCode") %>'></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnProductcode" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                    runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                    ValidationGroup="pductcodesubmit"></asp:Button>
                                                            </td>
                                                            <%-- <td>
                                                                <asp:Button ID="btnProductcode" runat="server" CssClass="PCGButton" Text="Submit"
                                                                    OnClick="btnProductcode_OnClick" />
                                                            </td>--%>
                                                        </tr>
                                                    </table>
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
                    <td>
                        <asp:Button ID="btncancelproductcode" runat="server" CssClass="PCGButton" Text="Cancel"
                            OnClick="btncancelproductcode_OnClick" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
    <asp:HiddenField ID="hdnSchemePlanCode" runat="server" />
    <asp:HiddenField ID="hdnCategory" runat="server" />
    <asp:HiddenField ID="hdnAMC" runat="server" />
    <asp:HiddenField ID="hdnExternalSource" runat="server" />
