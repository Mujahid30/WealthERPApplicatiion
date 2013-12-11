<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OnlineSchemeSetUp.ascx.cs"
    Inherits="WealthERP.OnlineOrderBackOffice.OnlineSchemeSetUp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            <asp:Label ID="lblProduct" runat="server" Text="Product" CssClass="FieldName"> </asp:Label>
        </td>
        <td width="25%">
            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="cmbField" AutoPostBack="false">
                <asp:ListItem Text="Mutual Funds" Value="MF" />
            </asp:DropDownList>
        </td>
        <td align="right" width="25%">
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
            <asp:DropDownList ID="ddlSScategory" runat="server" CssClass="cmbField" AutoPostBack="true">
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
            <asp:DropDownList ID="ddlRT" runat="server" CssClass="cmbField" AutoPostBack="true">
                <%--<asp:ListItem Text="Select" Value="Select" Selected="true" />
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
        </td>
        <td align="right">
            <%--ID="imgBtnAddBank" ImageUrl="~/Images/user_add.png" runat="server"
                ToolTip="Click here to Add Bank" OnClientClick="return openpopupAddBank()" Height="15px"
                Width="15px"></asp:ImageButton>--%>
            <asp:LinkButton runat="server" ID="LinkButton1" CssClass="LinkButtons" Text="Scheme Mapping"
                OnClientClick="return openpopupSchemeSetUp()"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblToadd" runat="server" Text="Do You Wish To Add" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            &nbsp;
            <asp:CheckBox ID="chkonline" AutoPostBack="true" runat="server" Text="Online Scheme"
                CssClass="FieldName" OnCheckedChanged="oncheckedOnlin_OnCheckedChanged" Checked="true" />
               <%-- CssClass="FieldName" OnCheckedChanged="oncheckedOnlin_OnCheckedChanged" Checked="false"/>--%>
                &nbsp;
                
               <%-- <asp:CheckBox ID="chkoffline" runat="server" Text="Offline Scheme" CssClass="FieldName" Checked="false"/>--%>
        </td>
        
    </tr> 
    
</table>
</div>
<div id="schemedetails" runat="server" visible="true">
    <table width="100%">
        <tr>
            <td colspan="5">
                <div class="divSectionHeading" style="vertical-align: text-bottom">
                    Scheme Details
                </div>
            </td>
        </tr>
    
        <tr>
            <td align="right" width="25%">
                <asp:Label ID="lblSctype" runat="server" Text="Scheme Type" CssClass="FieldName"> </asp:Label>
            </td>
            <td width="25%">
                <asp:DropDownList ID="ddlSctype" runat="server" CssClass="cmbField" AutoPostBack="false">
                    <asp:ListItem Text="Select" Value="Select" />
                    <asp:ListItem Text="Open Ended" Value="OE" />
                    <asp:ListItem Text="Close Ended" Value="CE" />
                </asp:DropDownList>
            </td>
            <td align="right" width="25%">
                <asp:Label ID="lblSecuritycode" runat="server" Text="Security Code" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtSecuritycode" runat="server" CssClass="cmbFielde" 
                    style="margin-left: 0px"></asp:TextBox>
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
                <asp:Label ID="LalISnfo" runat="server" Text="Is NFO" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkInfo" runat="server" Text="Yes" CssClass="FieldName" />
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
                <asp:Label ID="lblESSchemecode" runat="server" Text="External System Scheme Code"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtESSchemecode" runat="server" CssClass="cmbFielde"></asp:TextBox>
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
                <asp:Label ID="LalISPurchage" runat="server" Text="IS Purchase Available" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ChkISPurchage" runat="server" Text="Yes" CssClass="FieldName" OnCheckedChanged="oncheckedISpurchage_OnCheckedChanged" AutoPostBack="true"/>
            </td>
            <td align="right">
                <asp:Label ID="LalISRedeem" runat="server" Text="Is Redeem Available" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ChkISRedeem" runat="server" Text="Yes" CssClass="FieldName" OnCheckedChanged="oncheckedredemavaliable_OnCheckedChanged" AutoPostBack="true"/>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="LalChkISSwitch" runat="server" Text="Is Switch Available" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ChkISSwitch" runat="server" Text="Yes" CssClass="FieldName" OnCheckedChanged="oncheckedSwtchAvaliable_OnCheckedChanged" AutoPostBack="true"/>
            </td>
            <td align="right">
                <asp:Label ID="LbllISactive" runat="server" Text="Is Active" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="ChkISactive" runat="server" Text="Yes" CssClass="FieldName" />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="lblIMultipleamount" runat="server" Text="Inital Multiple Amount" CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtIMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="lblAddMultipleamount" runat="server" Text="Additional Multiple Amount"
                    CssClass="FieldName"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAddMultipleamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
            </td>
        </tr>
        <tr id="trIPAmount" runat="server" visible="false">
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td align="right" style="width:24%";>
                            <asp:Label ID="lblInitalPamount" runat="server" Text="Initial Purchase Amount" CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:24%;">
                            <asp:TextBox ID="txtInitalPamount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                        <td align="right" style="width:24.5%;">
                            <asp:Label ID="lblAdditionalPamount" runat="server" Text="Additional Purchase Amount"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:24.5%;">
                            <asp:TextBox ID="txtAdditional" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr id="trMINRedemPtion" runat="server" visible="false">
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td align="right" style="width:20%;">
                            <asp:Label ID="lblMinRedemption" runat="server" Text="Min Redemption Amount" CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:24%;">
                            <asp:TextBox ID="txtMinRedemption" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                        <td align="right" style="width:25%;">
                            <asp:Label ID="lblRedemptionmultiple" runat="server" Text="Redemption Multiple Amount"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:26.5%;">
                            <asp:TextBox ID="txtRedemptionmultiple" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:25%;">
                            <asp:Label ID="lblMinRedemptionUnits" runat="server" Text="Min Redemption Units"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:25%;">
                            <asp:TextBox ID="txtMinRedemptioUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                        <td align="right" style="width:24.5%;">
                            <asp:Label ID="lblRedemptionMultiplesUnits" runat="server" Text="Redemption Multiples Units"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:26.5%;">
                            <asp:TextBox ID="txtRedemptionMultiplesUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr  id="trSwitchPavailable" runat="server" visible="false">
            <td colspan="4">
                <table width="100%">
                    <tr>
                        <td align="right" style="width:24%;">
                            <asp:Label ID="lblMinSwitchAmount" runat="server" Text="Min Switch Amount" CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:24%;">
                            <asp:TextBox ID="txtMinSwitchAmount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                        <td align="right" style="width:25%;">
                            <asp:Label ID="lblMinSwitchUnits" runat="server" Text="Min Switch Units" CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:26.5%;">
                            <asp:TextBox ID="txtMinSwitchUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="width:25%;">
                            <asp:Label ID="lblSwitchMultipleAmount" runat="server" Text="Switch Multiple Amount"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:25%;">
                            <asp:TextBox ID="txtSwitchMultipleAmount" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                        <td align="right" style="width:24.5%;">
                            <asp:Label ID="lblSwitchMultipleUnits" runat="server" Text="Switch Multiples Units"
                                CssClass="FieldName"></asp:Label>
                        </td>
                        <td style="width:26.5%;">
                            <asp:TextBox ID="txtSwitchMultipleUnits" runat="server" CssClass="cmbFielde"></asp:TextBox>
                        </td>
                    </tr>
                </table>
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
                &nbsp;
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
                    AutoPostBack="true" OnCheckedChanged="ChkISSIP_OnCheckedChanged" />
            </td>
            <td align="right">
                <asp:CheckBox ID="ChkISSWP" runat="server" Text="Is SWP Available" AutoPostBack="true"
                    CssClass="FieldName" OnCheckedChanged="ChkISSWP_OnCheckedChanged" />
            </td>
            <td align="right">
                <asp:CheckBox ID="ChkISSTP" runat="server" Text="Is STP Available" AutoPostBack="true"
                    CssClass="FieldName" OnCheckedChanged="ChkISSTP_OnCheckedChanged" />
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
<table>
<tr>
    <asp:Panel ID="pnlSIPDetails" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal"
        Visible="true">
        <table width="100%">
            <tr>
                <td>
                    <telerik:RadGrid ID="gvSIPDetails" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="10" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableEmbeddedSkins="false" Width="100%" ClientSettings-AllowColumnsReorder="true"
                        AllowAutomaticInserts="false" OnNeedDataSource="gvSIPDetails_OnNeedDataSource"
                        OnItemDataBound="gvSIPDetails_OnItemDataBound" OnItemCommand="gvSIPDetails_OnItemCommand">
                        <ExportSettings HideStructureColumns="true" ExportOnlyData="true" FileName="OrderMIS">
                        </ExportSettings>
                        <MasterTableView DataKeyNames="PASPSD_SystematicDetailsId,PASP_SchemePlanCode" AllowFilteringByColumn="true" Width="100%" AllowMultiColumnSorting="True"
                            AutoGenerateColumns="false" CommandItemDisplay="Top" EditMode="PopUp">
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
                                <telerik:GridBoundColumn DataField="PASPSD_SystematicDetailsId" HeaderText="SystematicDetailsId" AllowFiltering="true"
                                    SortExpression="PASPSD_SystematicDetailsId" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    AutoPostBackOnFilter="true" UniqueName="PASPSD_SystematicDetailsId" FooterStyle-HorizontalAlign="Left"
                                    HeaderStyle-Width="100px" Visible="false">
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
                                <telerik:GridBoundColumn DataField="PASPSD_MinAmount" AllowFiltering="false" HeaderText="Min Amount" DataFormatString="{0:N2}"
                                    UniqueName="PASPSD_MinAmount" SortExpression="PASPSD_MinAmount" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" AutoPostBackOnFilter="true" HeaderStyle-Width="80px"
                                    FilterControlWidth="60px">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PASPSD_MultipleAmount" HeaderText="Amount Multiplier" DataFormatString="{0:N0}"
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
                                PopUpSettings-Width="530px" PopUpSettings-Height="300px">
                                <FormTemplate>
                                    <table width="100%" style="background-color: White">
                                        <tr id="trCustomerTypeSelection" runat="server">
                                            <td colspan="4">
                                                <table width="50%">
                                                    <tr>
                                                        <td align="right">
                                                            <asp:Label ID="lblFrequency" runat="server" CssClass="FieldName" Text="Frequency:"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFrquency" runat="server" CssClass="cmbField" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="leftField" align="right">
                                                            <asp:Label ID="lblstartDate" runat="server" CssClass="FieldName" Text="Start Date:"></asp:Label>
                                                        </td>
                                                        <td class="rightField" align="right">
                                                            <asp:TextBox ID="txtstartDate" runat="server" CssClass="txtField" Text='<%# Bind("PASPSD_StatingDates") %>'
                                                                AutoPostBack="false"></asp:TextBox>
                                                            <span id="Span8" class="spnRequiredField">*Multiple entries to be separated by(;)like-[12;24]</span>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtstartDate"
                                                                ErrorMessage="<br />Please Enter Date" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                                                ValidationGroup="Submit"></asp:RequiredFieldValidator>
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
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="Button1" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                                runat="server" CssClass="PCGButton" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                ValidationGroup="Submit"></asp:Button>
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
</table>
<asp:HiddenField ID="hdnSchemePlanCode" runat="server" />
<asp:HiddenField ID="hdnCategory" runat="server" />
<asp:HiddenField ID="hdnAMC" runat="server" />
<asp:HiddenField ID="hdnExternalSource" runat="server" />
