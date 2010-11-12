<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewNonIndividualProfile.ascx.cs"
    Inherits="WealthERP.Customer.ViewNonIndividualProfile" %>


<script src="../Scripts/tabber.js" type="text/javascript"></script>
<script language="javascript" type="text/javascript">
function showassocation() {

        var bool = window.confirm('Customer has associations,cannot be deteted');
        if (bool) {
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hdnassociation").value = 1;
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hiddenassociationfound").click();
            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hdnassociation").value = 0;
            document.getElementById("ctrl_ViewCustomerIndividualProfile_hiddenassociationfound").click();
            return true;
        }
    }
   
   
</script>


<table class="TableBackground" style="width: 50%;">
    <tr>
        <td class="rightField" colspan="2">
            <asp:Label ID="Label26" runat="server" CssClass="HeaderTextBig" Text="Profile"></asp:Label>
            <hr />
        </td>
    </tr>
    <tr>
    <td>
    </td>
    <td>
    <asp:Checkbox ID="chkprospectn" runat="server" CssClass="txtField"  Text="Prospect" 
                AutoPostBack="false"  Enabled = "false" /></asp:Label>
                </td>
    </tr>
    <tr>
        <td class="leftField" style="width: 35%">
            <asp:Label ID="lblBranchName" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch Name:"></asp:Label>
        </td>
    </tr>
    <tr>
    <td class="leftField" style="width: 20%">
            <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="RM Name:"></asp:Label>
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
            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Date of Profiling :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblProfilingDate" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Name of Company :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCompanyName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label5" runat="server" CssClass="FieldName" Text="Customer Code :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCustomerCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label10" runat="server" CssClass="FieldName" Text="Date Of Registration :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblRegistrationDate" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Date Of Commencement :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCommencementDate" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label12" runat="server" CssClass="FieldName" Text="Reg. No. with ROC-Registrar :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblRegistrationNum" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label13" runat="server" CssClass="FieldName" Text="Place Of Registration :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblRegistrationPlace" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label14" runat="server" CssClass="FieldName" Text="Company Website :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblCompanyWebsite" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label15" runat="server" CssClass="FieldName" Text="Contact Person Name :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblName" runat="server" Text="Label" CssClass="Field"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="leftField">
            <asp:Label ID="Label8" runat="server" CssClass="FieldName" Text="PAN Number :"></asp:Label>
        </td>
        <td class="rightField">
            <asp:Label ID="lblPanNum" runat="server" Text="Label" CssClass="Field"></asp:Label>
              &nbsp;
            &nbsp;
            &nbsp;
            <asp:Checkbox ID="chkdummypan" runat="server" CssClass="txtField" Text="Dummy PAN"
                AutoPostBack="true"  Enabled = "false"/>
        </td>
    </tr>
     
</table>
<div class="tabber" style="width: 100%">
    <div class="tabbertab" style="width: 100%">
        <h6>
            Correspondence Address</h6>
        <table style="width: 100%;">
            <tr>
                <td colspan="4" class="rightField">
                    <asp:Label ID="Label1" runat="server" Text="Correspondence Address" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label3" runat="server" Text="Line1(House No./Building) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblCorrLine1" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="rightField">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label6" runat="server" Text="Line2(Street) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblCorrLine2" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="style21">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label7" runat="server" Text="Line3(Area) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblCorrLine3" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label9" runat="server" Text="City :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblCorrCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label16" runat="server" Text="State :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblCorrState" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label19" runat="server" Text="Pincode :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblCorrPinCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label17" runat="server" Text="Country :" CssClass="FieldName"></asp:Label>
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
        <table style="width: 100%;">
            <tr>
                <td colspan="4" class="rightField">
                    <asp:Label ID="Label18" runat="server" Text="Permanent Address " CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label60" runat="server" Text="Line1(House No./Building) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblPermLine1" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label20" runat="server" Text="Line2(Street) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblPermLine2" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField">
                    <asp:Label ID="Label21" runat="server" Text="Line3(Area) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:Label ID="lblPermLine3" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label22" runat="server" Text="City :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblPermCity" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label23" runat="server" Text="State :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblPermState" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label24" runat="server" Text="Pincode :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblPermPinCode" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label25" runat="server" Text="Country :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblPermCountry" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="tabbertab">
        <h6>
            Contact Details</h6>
        <table style="width: 100%;">
            <tr>
                <td colspan="4" class="rightField">
                    <asp:Label ID="Label35" runat="server" Text="Contact Details" CssClass="HeaderTextSmall"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label36" runat="server" Text="Telephone No.(Res) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblResPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label41" runat="server" Text="Fax :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblResFax" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label37" runat="server" Text="Telephone No.(Off) :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblOfcPhone" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label40" runat="server" Text="Alternate Email :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblAltEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="leftField" width="25%">
                    <asp:Label ID="Label39" runat="server" Text="Email :" CssClass="FieldName"></asp:Label>
                </td>
                <td class="rightField" width="25%">
                    <asp:Label ID="lblEmail" runat="server" Text="Label" CssClass="Field"></asp:Label>
                </td>
                <td width="25%">
                    &nbsp;
                </td>
                <td width="25%">
                    &nbsp;
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
                        <td class="leftField" width="25%">
                                <asp:Label ID="Label27" runat="server" Text="Alert Preferences:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightField" width="25%">
                                 <asp:Checkbox ID="chkmailn" runat="server" CssClass="txtField" Text="Via Mail"
                AutoPostBack="true"  Enabled = "false"/>
                &nbsp;
            &nbsp;
                 <asp:Checkbox ID="chksmsn" runat="server" CssClass="txtField" Text="Via SMS"
                AutoPostBack="true"  Enabled = "false"/>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        
                       
                        
                    </table>
                </div>
</div>
<table width="100%">
    <tr id="trDelete" runat="server">
        <td colspan="3" class="SubmitCell">
            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete" Visible="false"
                CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_ViewNonIndividualProfile_btnDelete');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_ViewNonIndividualProfile_btnDelete');" />
                <asp:HiddenField ID="hdnassociationcount" runat="server" />
        </td>
    </tr>
</table>
