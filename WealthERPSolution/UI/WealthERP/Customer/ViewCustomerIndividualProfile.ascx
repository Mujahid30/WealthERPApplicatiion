<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerIndividualProfile.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerIndividualProfile" %>

<script type="text/javascript" src="../Scripts/tabber.js"></script>


<%--Javascript Calendar Controls - Required Files--%>

<script type="text/javascript" src="../Scripts/Calender/calendar.js"></script>

<script type="text/javascript" src="../Scripts/Calender/lang/calendar-en.js"></script>

<script type="text/javascript" src="../Scripts/Calender/calendar-setup.js"></script>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<link href="../Scripts/Calender/skins/aqua/theme.css" rel="stylesheet" type="text/css" />

<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this profile?');
        if (bool) {
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hiddenDelete").click();
            return true;
        }
    }
   
</script>

<table class="TableBackground" style="width: 100%">
    <tr>
        <td class="HeaderCell" colspan="2">
            <asp:Label ID="Label61" runat="server" CssClass="HeaderTextBig" Text="Profile"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
        <td class="leftField">
            &nbsp;
        </td>
        <td class="rightField" width="75%">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 20%">
            <asp:Label ID="lblCustomerType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblType" runat="server" CssClass="FieldName" Text="Customer Type:"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="lblCustomerSubType" runat="server" CssClass="FieldName" Text="Customer Sub Type:"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblSubType" runat="server" CssClass="FieldName" Text="Customer SubType:"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label2" runat="server" Text="Date of Profiling:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:Label ID="lblProfilingDate" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" Text="Name (First/Middle/Last):" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" Text="Customer Code:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:Label ID="lblCustCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label7" runat="server" Text="Date of Birth:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:Label ID="lblDob" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" Text="PAN Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:Label ID="lblPanNum" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr id="trGuardianName" runat="server">
        <td class="leftField">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Guardian Name(First/Middle/Last):"></asp:Label>
        </td>
        <td class="rightField" width="75%">
            <asp:Label ID="lblGuardianName" runat="server" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField" colspan="2">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="style1" colspan="2">
            <hr />
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div class="tabber" style="width: 100%">
                <div class="tabbertab">
                    <h6>
                        Correspondence Address</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label10" runat="server" Text="Correspondence Address" CssClass="HeaderTextSmall"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label11" runat="server" Text="Line1(HouseNo/Building):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblCorrLine1" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label12" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblCorrLine2" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label13" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblCorrLine3" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblLivingSince" runat="server" Text="Living Since:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblLiving" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label14" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblCorrCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label16" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblCorrState" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label15" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblCorrPinCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label17" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblCorrCountry" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab">
                    <h6>
                        Permanent Address</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4" width="25%">
                                <asp:Label ID="Label18" runat="server" Text="Permanent Address " CssClass="HeaderTextSmall"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label60" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblPermLine1" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label20" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblPermLine2" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label21" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblPermLine3" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label22" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblPermCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label23" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblPermState" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label24" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblPermPinCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label25" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblPermCountry" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab">
                    <h6>
                        Office Address</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label26" runat="server" Text="Office Address" CssClass="HeaderTextSmall"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label34" runat="server" Text="Company Name:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblCompanyName" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label27" runat="server" Text="Line1(House No/Building):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblOfcLine1" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label28" runat="server" Text="Line2(Street):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" colspan="3">
                                <asp:Label ID="lblOfcLine2" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label29" runat="server" Text="Line3(Area):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblOfcLine3" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblJobStartDate" runat="server" Text="Job Start Date:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField">
                                <asp:Label ID="lblJobStart" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label30" runat="server" Text="City:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblOfcCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label31" runat="server" Text="State:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblOfcState" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label32" runat="server" Text="Pin Code:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblOfcPinCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label33" runat="server" Text="Country:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblOfcCountry" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab">
                    <h6>
                        Contact Details</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label35" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label36" runat="server" Text="Telephone No. (Res):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblResPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label41" runat="server" Text="Fax (Res):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblResFax" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label37" runat="server" Text="Telephone No.(Ofc):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblOfcPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label42" runat="server" Text="Fax (Ofc):" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblOfcFax" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label38" runat="server" Text="Mobile1:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblMobile1" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label43" runat="server" Text="Mobile2:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblMobile2" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label39" runat="server" Text="Email:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label40" runat="server" Text="Alternate Email:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblAltEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="tabbertab">
                    <h6>
                        Additional Information</h6>
                    <table width="100%">
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="Label44" runat="server" Text="Additional Information" CssClass="HeaderTextSmall"></asp:Label>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label45" runat="server" Text="Occupation:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblOccupation" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label46" runat="server" Text="Qualification:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblQualification" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label47" runat="server" Text="Marital Status:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblMaritalStatus" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label48" runat="server" Text="Nationality:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblNationality" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label49" runat="server" Text="RBI Reference No:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblRBIRefNo" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                            <td class="leftField" width="25%">
                                <asp:Label ID="Label50" runat="server" Text="RBI Reference Date:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblRBIRefDate" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftField" width="25%">
                                <asp:Label ID="lblMotherMaidenName" runat="server" Text="Mother's Maiden Name:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                <asp:Label ID="lblMotherMaiden" runat="server" Text="Label" CssClass="Field"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
    <tr id="trDelete" runat="server">
        <td colspan="2" class="SubmitCell">
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewCustomerIndividualProfile_btnDelete', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewCustomerIndividualProfile_btnDelete', 'S');" />
            <asp:Button ID="hiddenDelete" runat="server" OnClick="hiddenDelete_Click" Text=""
                BorderStyle="None" BackColor="Transparent" />
            <asp:HiddenField ID="hdnMsgValue" runat="server" />
        </td>
    </tr>
</table>
