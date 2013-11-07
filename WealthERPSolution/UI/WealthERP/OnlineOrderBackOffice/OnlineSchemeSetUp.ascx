<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineSchemeSetUp.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineSchemeSetUp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table width="100%">
                    <tr>
                        <td align="left">
                            Online Scheme Setup
                        </td>
                        <td>
                            <asp:LinkButton runat="server" ID="lbBack" CssClass="LinkButtons" Text="Edit" Visible="false"
                                OnClick="lbBack_OnClick"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <table id="tblMessage" width="100%" runat="server" visible="false" style="padding-top: 20px;">
        <tr id="trSumbitSuccess">
            <td align="center">
                <div id="msgRecordStatus" class="success-msg" align="center" runat="server">
                </div>
            </td>
        </tr>
    </table>
</table>
<table width="100%">
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Basic Details
            </div>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblProduct" runat="server" Text="Product" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="false">
                <asp:ListItem Text="Mutual Funds" Value="MF" />
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblAmc" runat="server" Text="AMC" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlAmc" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlAmcCode_SelectedIndexChanged">
                <%--  <asp:ListItem Text="Select" Value="Select" Selected="true" />--%>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblScname" runat="server" Text="New Scheme Name" CssClass="FieldName"
                Visible="false"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtScname" runat="server" CssClass="cmbFielde" Visible="false"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblAcode" runat="server" Text="AMFI Code" CssClass="FieldName" Visible="false"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtAMFI" runat="server" CssClass="cmbFielde" Visible="false"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblcategory" runat="server" Text="Category" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlcategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlCategory_OnSelectedIndexChanged">
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="lblScategory" runat="server" Text="Sub Category" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlScategory" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlScategory_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="false" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblSScategory" runat="server" Text="Sub Sub Category" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSScategory" runat="server" CssClass="cmbField" AutoPostBack="false">
                <asp:ListItem Text="Select" Value="Select" Selected="true" />
            </asp:DropDownList>
        </td>
        <td align="right">
            <asp:Label ID="Label4" runat="server" Text="Scheme" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlSchemeList" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlSchemeList_OnSelectedIndexChanged">
                <asp:ListItem Text="Select" Value="Select" Selected="false" />
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblRT" runat="server" Text="R&T" CssClass="FieldName"> </asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlRT" runat="server" CssClass="cmbField" AutoPostBack="false">
                <asp:ListItem Text="Select" Value="Select" Selected="true" />
                <asp:ListItem Text="CAMS" Value="CAMS"></asp:ListItem>
                <asp:ListItem Text="Deutsche" Value="Deutsche" Enabled="false">
                </asp:ListItem>
                <asp:ListItem Text="Templeton" Value="Templeton">
                </asp:ListItem>
                <asp:ListItem Text="KARVY" Value="KARVY">
                </asp:ListItem>
                <asp:ListItem Text="Sundaram" Value="Sundaram">
                </asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblToadd" runat="server" Text="Do You Wish To Add" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:CheckBox ID="chkonline" AutoPostBack="true" runat="server" Text="Online Scheme"
                CssClass="FieldName" OnCheckedChanged="oncheckedOnlin_OnCheckedChanged" />
        </td>
        <td align="right">
            <asp:CheckBox ID="chkoffline" runat="server" Text="Offline Scheme" CssClass="FieldName" Checked="false"/>
        </td>
    </tr>
</table>
<table width="100%">
    <div id="schemedetails" runat="server" visible="false">
        <tr>
            <td colspan="5">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Scheme Details
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblSctype" runat="server" Text="Scheme Type" CssClass="FieldName"> </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlSctype" runat="server" CssClass="cmbField" AutoPostBack="false">
                    <asp:ListItem Text="Select" Value="Select" />
                    <asp:ListItem Text="Open Ended" Value="OE" />
                    <asp:ListItem Text="Close Ended" Value="CE" />
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblFvalue" runat="server" Text="Face Value" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFvale" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblOption" runat="server" Text="Option" CssClass="FieldName"> </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlOption" runat="server" CssClass="cmbField" AutoPostBack="false">
                    <asp:ListItem Text="Select" Value="Select" Selected="true" />
                    <asp:ListItem Text="Dividend" Value="DV" />
                    <asp:ListItem Text="Growth" Value="GR" />
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblDFrequency" runat="server" Text="Dividend Reinvestment flag" CssClass="FieldName"> </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlDFrequency" runat="server" CssClass="cmbField" AutoPostBack="false">
                    <asp:ListItem Text="Select" Value="Select" Selected="true" />
                    <asp:ListItem Text="Dividend Reinvestment" Value="DVR" />
                    <asp:ListItem Text="Dividend Payout" Value="DVP" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblBname" runat="server" Text="Bank Name" CssClass="FieldName"> </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlBname" runat="server" CssClass="cmbField" AutoPostBack="false">
                    <%--<asp:ListItem Text="Select" Value="Select" Selected="true" />--%>
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblBranch" runat="server" Text="Bank Branch" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBranch" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblACno" runat="server" Text="Bank Account Number" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtACno" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:CheckBox ID="chkInfo" runat="server" Text="Is NFO" CssClass="FieldName" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblNfostartdate" runat="server" Text="NFO Start Date" CssClass="FieldName"></asp:Label>
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
                <%--<span id="Span7" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="appRecidRequiredFieldValidator" ControlToValidate="txtNFOStartDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>--%>
            </td>
            <td align="right">
                <asp:Label ID="lblNfoEnddate" runat="server" Text="NFO End Date" CssClass="FieldName"></asp:Label>
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
                <%--<span id="Span1" class="spnRequiredField">*</span>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtNFOStartDate"
                CssClass="rfvPCG" ErrorMessage="<br />Please select NFO End Date" Display="Dynamic"
                runat="server" InitialValue="" ValidationGroup="btnsubmit"></asp:RequiredFieldValidator>--%>
            </td>
            <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToValidate="txtNFOendDate"
                ErrorMessage="<br/> To Date should be greater than From Date" Type="Date" Operator="GreaterThanEqual"
                ControlToCompare="txtNFOStartDate" CssClass="cvPCG" ValidationGroup="btnsubmit"
                Display="Dynamic">
            </asp:CompareValidator>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblLIperiod" runat="server" Text="Lock In Period" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLIperiod" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblCOtime" runat="server" Text="Cut Off Time" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtHH" runat="server" CssClass="cmbFielde" Width="20px"></asp:TextBox>
                <asp:Label ID="Label1" runat="server" Text="HH" CssClass="FieldName"></asp:Label>
                <asp:TextBox ID="txtMM" runat="server" CssClass="cmbFielde" Width="20px"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text="MM" CssClass="FieldName"></asp:Label>
                <asp:TextBox ID="txtSS" runat="server" CssClass="cmbFielde" Width="20px"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text="SS" CssClass="FieldName"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblEload" runat="server" Text="Entry Load %" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtEload" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblELremark" runat="server" Text="Entry Load Remark" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtELremark" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblExitLoad" runat="server" Text="Exit Load %" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExitLoad" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblExitLremark" runat="server" Text="Exit Load Remark" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtExitLremark" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:CheckBox ID="ChkISPurchage" runat="server" Text="Is Purchage Available" CssClass="FieldName" />
            </td>
            <td>
            </td>
            <td align="right">
                <asp:CheckBox ID="ChkISRedeem" runat="server" Text="Is Redeem Available" CssClass="FieldName" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:CheckBox ID="ChkISSwitch" runat="server" Text="Is Switch Available" CssClass="FieldName" />
            </td>
            <td>
            </td>
            <td align="right">
                <asp:CheckBox ID="ChkISactive" runat="server" Text="Is Active" CssClass="FieldName" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblInitalPamount" runat="server" Text="Initial Purchase Amount" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInitalPamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblIMultipleamount" runat="server" Text="Inital Multiple Amount" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblAdditionalPamount" runat="server" Text="Additional Purchase Amount"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAdditional" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblAddMultipleamount" runat="server" Text="Additional Multiple Amount"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblMinRedemption" runat="server" Text="Min Redemption Amount" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMinRedemption" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblRedemptionmultiple" runat="server" Text="Redemption Multiple Amount"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRedemptionmultiple" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblMinRedemptionUnits" runat="server" Text="Min Redemption Units"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMinRedemptioUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblRedemptionMultiplesUnits" runat="server" Text="Redemption Multiples Units"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtRedemptionMultiplesUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblMinSwitchAmount" runat="server" Text="Min Switch Amount" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMinSwitchAmount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblMinSwitchUnits" runat="server" Text="Min Switch Units" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMinSwitchUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblSwitchMultipleAmount" runat="server" Text="Switch Multiple Amount"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSwitchMultipleAmount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblSwitchMultipleUnits" runat="server" Text="Switch Multiples Units"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSwitchMultipleUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblGenerationfreq" runat="server" Text="File Generation Freq" CssClass="FieldName"> </asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="ddlGenerationfreq" runat="server" CssClass="cmbField" AutoPostBack="false">
                    <asp:ListItem Text="Select" Value="Select" Selected="true" />
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblMaxinvestment" runat="server" Text="Max Investment" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtinvestment" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblNACustomerType" runat="server" Text="Not Allowed Customer Type"
                    CssClass="FieldName"> </asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ChkNRI" runat="server" Text="NRI" CssClass="FieldName" />
            </td>
            <td align="right">
                <asp:CheckBox ID="ChkBO" runat="server" Text="BOI" CssClass="FieldName" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblSecuritycode" runat="server" Text="Security Code" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSecuritycode" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblESSchemecode" runat="server" Text="External System Scheme Code"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtESSchemecode" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr>
        </tr>
        
    
    <tr>
        <td colspan="5">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Systematic Details
            </div>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:CheckBox ID="ChkISSIP" runat="server" Text="Is SIP Available" CssClass="FieldName" />
        </td>
        <td>
        </td>
        <td align="right">
            <asp:CheckBox ID="ChkISSWP" runat="server" Text="Is SWP Available" CssClass="FieldName" />
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:CheckBox ID="ChkISSTP" runat="server" Text="Is STP Available" CssClass="FieldName" />
        </td>
    </tr>
    <tr>
            <td align="right">
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="PCGButton" OnClick="btnsubmit_click" />
            </td>
            <td align="left">
                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="PCGButton" OnClick="btnUpdate_click"
                    Style="height: 26px" />
                <%-- ValidationGroup="btnsubmit"  ValidationGroup="btnsubmit"--%>
            </td>
        </tr>
    </div>
</table>
