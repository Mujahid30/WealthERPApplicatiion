<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerProfile.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerProfile" %>
<script language="javascript" type="text/javascript">
    function showmessage() {

        var bool = window.confirm('Are you sure you want to delete this profile?');
        if (bool) {
            document.getElementById("ctrl_ViewCustomerProfile_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewCustomerProfile_hiddenDelete").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerProfile_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewCustomerProfile_hiddenDelete").click();
            return true;
        }
    }
   
</script>
<script type="text/javascript" src="../Scripts/tabber.js"></script>


<div style="height: 1509px; width: 760px;">
    <table style="width: 100%; height: 384px;">
        <tr>
            <td class="style15">
                &nbsp;
            </td>
            <td class="style14" colspan="2">
                &nbsp;
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
            <td class="style15">
                <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date of Profiling"></asp:Label>
            </td>
            <td class="style16">
                <asp:TextBox ID="txtProfilingDate" runat="server" Width="270px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
            <td class="style14">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style15">
                <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Name (First/Middle/Last)"></asp:Label>
            </td>
            <td class="style14" colspan="2">
                <asp:TextBox ID="txtFirstName" runat="server" Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtMiddleName" runat="server" Width="90px"></asp:TextBox>
                <asp:TextBox ID="txtLastName" runat="server" Width="90px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style15">
                <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Customer Code"></asp:Label>
            </td>
            <td class="style14" colspan="2">
                <asp:TextBox ID="txtCustomerCode" runat="server" Width="270px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style15">
                <asp:Label ID="Label7" runat="server" CssClass="FieldName" Text="Date of Birth"></asp:Label>
            </td>
            <td class="style14">
                <asp:TextBox ID="txtDob" runat="server" Width="270px"></asp:TextBox>
            </td>
            <td class="style14">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style15">
                <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN Number"></asp:Label>
            </td>
            <td class="style14" colspan="2">
                <asp:TextBox ID="txtPanNumber" runat="server" Width="270px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style15">
                <asp:Label ID="Label9" runat="server" CssClass="FieldName" Text="RM Name"></asp:Label>
            </td>
            <td class="style14" colspan="2">
                <asp:TextBox ID="txtRmName" runat="server" Width="270px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style15">
                &nbsp;
            </td>
            <td class="style14" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style15">
                &nbsp;
            </td>
            <td class="style14" colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <div class="tabber">
        <div class="tabbertab">
            <h6>
                Correspondence Address</h6>
            <table style="width: 100%; height: 196px;">
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label10" CssClass="FieldName" runat="server" Text="Correspondence Address"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label11" CssClass="FieldName" runat="server" Text="Line1"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCorrAdrLine1" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label12" CssClass="FieldName" runat="server" Text="Line2"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCorrAdrLine2" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label13" CssClass="FieldName" runat="server" Text="Line3"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCorrAdrLine3" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style8">
                        <asp:Label ID="Label14" CssClass="FieldName" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="style9">
                        <asp:TextBox ID="txtCorrAdrCity" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style10">
                        <asp:Label ID="Label16" CssClass="FieldName" runat="server" Text="State"></asp:Label>
                    </td>
                    <td class="style11">
                        <asp:TextBox ID="txtCorrAdrState" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label15" CssClass="FieldName" runat="server" Text="Pin Code"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtCorrAdrPinCode" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label17" CssClass="FieldName" runat="server" Text="Country"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCorrAdrCountry" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabbertab">
            <h6>
                Permanent Address</h6>
            <table style="width: 100%; height: 196px;">
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label18" CssClass="FieldName" runat="server" Text="Permanent Address "></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label19" CssClass="FieldName" runat="server" Text="Line1"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPermAdrLine1" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label20" CssClass="FieldName" runat="server" Text="Line2"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPermAdrLine2" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label21" CssClass="FieldName" runat="server" Text="Line3"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPermAdrLine3" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label22" CssClass="FieldName" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPermAdrCity" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label23" CssClass="FieldName" runat="server" Text="State"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPermAdrState" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label24" CssClass="FieldName" runat="server" Text="Pin Code"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtPermAdrPinCode" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label25" CssClass="FieldName" runat="server" Text="Country"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPermAdrCountry" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabbertab">
            <h6>
                Office Address</h6>
            <table style="width: 100%; height: 213px;">
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label26" CssClass="FieldName" runat="server" Text="Office Address"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label34" CssClass="FieldName" runat="server" Text="Company Name"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcCompanyName" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label27" CssClass="FieldName" runat="server" Text="Line1"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcAdrLine1" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label28" CssClass="FieldName" runat="server" Text="Line2"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcAdrLine2" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label29" CssClass="FieldName" runat="server" Text="Line3"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcAdrLine3" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label30" CssClass="FieldName" runat="server" Text="City"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcAdrCity" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label31" CssClass="FieldName" runat="server" Text="State"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfcAdrState" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label32" CssClass="FieldName" runat="server" Text="Pin Code"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcAdrPinCode" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label33" CssClass="FieldName" runat="server" Text="Country"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfcAdrCountry" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabbertab">
            <h6>
                Contact Details</h6>
            <table style="width: 103%; height: 170px;">
                <tr>
                    <td class="style1">
                        &nbsp;
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label35" CssClass="FieldName" runat="server" Text="Contact Details"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label36" CssClass="FieldName" runat="server" Text="Telephone No. (Res)"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtResPhoneNoIsd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtResPhoneNoStd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtResPhoneNo" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label41" CssClass="FieldName" runat="server" Text="Fax (Res)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResFaxIsd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtResFaxStd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtResFax" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label37" CssClass="FieldName" runat="server" Text="Telephone No.(Ofc)"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOfcPhoneNoIsd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtOfcPhoneNoStd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtOfcPhoneNo" runat="server" Width="100px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label42" CssClass="FieldName" runat="server" Text="Fax (Ofc)"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOfcFaxIsd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtOfcFaxStd" runat="server" Width="40px"></asp:TextBox>
                        <asp:TextBox ID="txtOfcFax" runat="server" Width="100px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label38" CssClass="FieldName" runat="server" Text="Mobile1"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtMobile1" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label43" CssClass="FieldName" runat="server" Text="Mobile2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile2" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label39" CssClass="FieldName" runat="server" Text="Email"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtEmail" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label40" CssClass="FieldName" runat="server" Text="Alternate Email"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAltEmail" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tabbertab">
            <h6>
                Additional Information</h6>
            <table style="width: 101%; height: 143px;">
                <tr>
                    <td class="style12">
                    </td>
                    <td colspan="2" class="style13">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="Label44" CssClass="FieldName" runat="server" Text="Additional Information"></asp:Label>
                    </td>
                    <td class="style13">
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label45" CssClass="FieldName" runat="server" Text="Occupation"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtOccupation" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label46" CssClass="FieldName" runat="server" Text="Qualification"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQualification" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label47" CssClass="FieldName" runat="server" Text="Marital Status"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtMaritalStatus" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label48" CssClass="FieldName" runat="server" Text="Nationality"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNationality" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label49" CssClass="FieldName" runat="server" Text="RBI Reference No."></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox ID="txtRBIRefNo" runat="server" Width="180px"></asp:TextBox>
                    </td>
                    <td class="style3">
                        <asp:Label ID="Label50" CssClass="FieldName" runat="server" Text="RBI Reference Date"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRBIRefDate" runat="server" Width="180px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                    <td class="style2">
                        &nbsp;</td>
                    <td class="style3">
                        <asp:Button ID="btnDelete" runat="server" CssClass="PCGButton" 
                            OnClick="btnDelete_Click" 
                            onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewCustomerIndividualProfile_btnDelete', 'S');" 
                            onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewCustomerIndividualProfile_btnDelete', 'S');" 
                            Text="Delete" />
                        <asp:Button ID="hiddenDelete" runat="server" BackColor="Transparent" 
                            BorderStyle="None" OnClick="hiddenDelete_Click" Text="" />
                        <asp:HiddenField ID="hdnMsgValue" runat="server" />
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
    <%--<table>
        <tr>
            <td class="style6">
            </td>
            <td class="style5">
                &nbsp;
            </td>
            <td class="style4">
                &nbsp;
            </td>
            <td class="style7">
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
    </table>--%>
</div>
