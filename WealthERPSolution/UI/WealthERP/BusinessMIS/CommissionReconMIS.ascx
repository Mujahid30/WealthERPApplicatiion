<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommissionReconMIS.ascx.cs"
    Inherits="WealthERP.BusinessMIS.CommissionReconMIS" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
<telerik:RadScriptManager ID="scptMgr" runat="server">
</telerik:RadScriptManager>

<script language="javascript" type="text/javascript">
    function checkAllBoxes() {

        //get total number of rows in the gridview and do whatever
        //you want with it..just grabbing it just cause
        var gvControl = document.getElementById('<%= gvBrokerageRequestStatus.ClientID %>');

        //this is the checkbox in the item template...this has to be the same name as the ID of it
        var gvChkBoxControl = "chkItem";

        //this is the checkbox in the header template
        var mainChkBox = document.getElementById("chkboxSelectAll");

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


<script type="text/javascript">
             var TargetBaseControl = null;

             window.onload = function() {
                 try {
                     //get target base control.
                     TargetBaseControl =
           document.getElementById('<%= this.gvBrokerageRequestStatus.ClientID %>');
                 }
                 catch (err) {
                     TargetBaseControl = null;
                 }
             }

             function TestCheckBox() {
              
                 if (TargetBaseControl == null) return false;

                 //get target child control.
                 var TargetChildControl = "chkItem";

                 //get all the control of the type INPUT in the base control.
                 var Inputs = TargetBaseControl.getElementsByTagName("input");

                 for (var n = 0; n < Inputs.length; ++n)
                     if (Inputs[n].type == 'checkbox' & Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
                     return true;

                 alert('Select at least one checkbox!');
                 return false;
             }
</script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<telerik:RadTabStrip ID="rtsCalculationRequest" runat="server" EnableTheming="True"
    Skin="Telerik" EnableEmbeddedSkins="False" MultiPageID="multipageCalculationRequest"
    SelectedIndex="0">
    <Tabs>
        <telerik:RadTab runat="server" Text="Add Request" Value="RequestCalculation" TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Check Request Status" Value="calculationStatus">
        </telerik:RadTab>
    </Tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="multipageCalculationRequest" EnableViewState="true" runat="server">
    <telerik:RadPageView ID="rpvCalculationDetails" runat="server" Selected="true">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <div id="msgReconComplete" runat="server" class="success-msg" align="center" visible="false">
                                Recon Status Marked closed
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center">
                            <div id="msgReconClosed" runat="server" class="failure-msg" align="center" visible="false">
                                Recon staus Closed,So can't save changes
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <div class="divPageHeading">
                                <table cellspacing="0" cellpadding="3" width="100%">
                                    <tr>
                                        <td align="left">
                                            Commission Report
                                        </td>
                                        <td align="right">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr id="tblMessagee" runat="server" visible="false">
                        <td colspan="6">
                            <table class="tblMessage">
                                <tr>
                                    <td align="center">
                                        <div id="divMessage" align="center">
                                        </div>
                                        <div style="clear: both">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table width="90%" class="TableBackground" cellspacing="0" cellpadding="2">
                    <tr>
                        <td align="left" class="leftField" width="20%">
                            <asp:Label ID="lblSelectProduct" runat="server" Text=" Product:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlProduct"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                        <td class="leftField" width="16%">
                            <asp:Label ID="lblSearchType" runat="server" Text=" Type:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="rightField">
                            <asp:DropDownList ID="ddlSearchType" AutoPostBack="true" runat="server" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlSearchType_OnSelectedIndexChanged">
                                <asp:ListItem Text="Brokerage" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="ddlSearchType"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select a Commission type"
                                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                        <td align="left" class="leftField" width="20%" id="tdCategory" runat="server" visible="false">
                            <asp:Label ID="lblCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="right" id="tdDdlCategory" runat="server" visible="false">
                            <asp:DropDownList ID="ddlProductCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlProductCategory_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="ddlProductCategory"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="trSelectProduct" runat="server">
                        <td align="left" class="leftField" width="20%">
                            <asp:Label ID="Label2" runat="server" Text="Order Status:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOrderStatus" runat="server" CssClass="cmbField">
                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                <asp:ListItem Text="EXECUTED" Value="IP"> </asp:ListItem>
                                <asp:ListItem Text="ORDERED" Value="IP"> </asp:ListItem>
                                <asp:ListItem Text="ACCEPTED" Value="OR"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="ddlOrderStatus"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select Order Status"
                                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                        <td class="leftField" width="16%">
                            <asp:Label ID="lblOffline" runat="server" Text="Channel:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelectMode" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlSelectMode_OnSelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="Both" Value="2">
                                </asp:ListItem>
                                <asp:ListItem Text="Online-Only" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Offline-Only" Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td id="td1" align="left" class="leftField" width="16%" runat="server" visible="true">
                            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Commission Type:"></asp:Label>
                        </td>
                        <td id="td2" runat="server" visible="true">
                            <asp:DropDownList ID="ddlCommType" runat="server" CssClass="cmbField" AutoPostBack="true">
                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                <asp:ListItem Text="Upfront" Value="UF" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                                <asp:ListItem Text="Incentive" Value="IN"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="ddlCommType"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="trNCDIPO" runat="server" visible="false">
                        <td align="left" class="leftField">
                            <asp:Label ID="lblIssueType" runat="server" CssClass="FieldName" Text="Issue Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlIssueType_OnSelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                <asp:ListItem Text="Closed Issues" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Current Issues" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="cvddlIssueType" runat="server" ControlToValidate="ddlIssueType"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue Type"
                                Operator="NotEqual" ValidationGroup="vgbtnSubmit" ValueToCompare="Select" Enabled="false"></asp:CompareValidator>
                        </td>
                        <td align="left" class="leftField">
                            <asp:Label ID="lblIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIssueName" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trSelectMutualFund" runat="server" visible="false">
                        <td align="left" class="leftField">
                            <asp:Label ID="lblSelectMutualFund" runat="server" CssClass="FieldName" Text="Issuer:"></asp:Label>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlProduct"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                                ValidationGroup="vgbtnSubmit" ValueToCompare="Select Product Type"></asp:CompareValidator>
                        </td>
                        
                        <td>
                            <asp:DropDownList ID="ddlIssuer" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="cvddlSelectMutualFund" runat="server" ControlToValidate="ddlIssuer"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                        <td align="left" class="leftField">
                            <asp:Label ID="lblNAVCategory" runat="server" CssClass="FieldName" Text="Category:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td align="left" class="leftField">
                            <asp:Label ID="lblSelectSchemeNAV" runat="server" CssClass="FieldName" Text="Scheme:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlScheme" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlScheme"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Scheme" Operator="NotEqual"
                                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                    </tr>
                    <%--<tr id="trIssue" runat="server"  visible="false">
    <td>
    <asp:Label ID="lblIssue" runat="server"  Text="Select Issue:" CssClass="FieldName"></asp:Label>
    </td>
    <td>
    <asp:DropDownList ID="ddlIssueName" runat="server" AutoPostBack="true" CssClass="cmbField"></asp:DropDownList>
    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlIssueName"
                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select Issue" Operator="NotEqual"
                ValidationGroup="vgbtnSubmit" ValueToCompare="Select"></asp:CompareValidator>
    </td>
    </tr>--%>
                    <tr>
                        <td class="leftField" id="tdTolbl" runat="server" visible="true">
                            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="Year:"></asp:Label>
                        </td>
                        <td class="rightField" id="tdToDate" runat="server" visible="true">
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlYear"
                                CssClass="rfvPCG" ErrorMessage="<br />Please select a Year" Display="Dynamic"
                                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
                        </td>
                        <td id="tdFromDate" class="leftField" runat="server" visible="true">
                            <asp:Label ID="lblPeriod" Text="Month/Quarter:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" id="tdFrom" runat="server" visible="true">
                            <asp:DropDownList ID="ddlMnthQtr" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlMnthQtr"
                                CssClass="rfvPCG" ErrorMessage="<br />Please Select Month" Display="Dynamic"
                                runat="server" InitialValue="0" ValidationGroup="vgbtnSubmit"> </asp:RequiredFieldValidator>
                        </td>
                        <td class="leftField" id="td3" runat="server">
                            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Authenticated:"></asp:Label>
                        </td>
                        <td class="rightField" id="td4" runat="server">
                            <asp:DropDownList ID="ddlOrderType" runat="server" CssClass="cmbField">
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="All" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="rightField" style="padding-right: 50px">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" OnClick="GdBind_Click"
                                Text="Request" ValidationGroup="vgbtnSubmit" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Image ID="imgProgress" ImageUrl="~/Images/ajax-loader.gif" AlternateText="Processing"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                    <%--<img alt="Processing" src="~/Images/ajax_loader.gif" style="width: 200px; height: 100px" />--%>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblIllegal" runat="server" CssClass="Error" Text="" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportFilteredData" />
            </Triggers>
        </asp:UpdatePanel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="rpvAddRequest" runat="server">
        <asp:UpdatePanel ID="upFilterDetails" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td>
                            <div class="divPageHeading">
                                <table cellspacing="0" cellpadding="3" width="100%">
                                    <tr>
                                        <td align="left">
                                            Commission Report
                                        </td>
                                        <td align="right">
                                            <asp:ImageButton ID="btnExportFilteredData" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                                                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="btnExportFilteredData_OnClick"
                                                OnClientClick="setFormat('excel')" Height="25px" Width="25px"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
                <table width="90%" runat="server" id="tbIssueType">
                    <tr>
                        <td align="left" class="leftField" width="20%">
                            <asp:Label ID="lblProduct" runat="server" Text=" Product:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRequestProduct" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlRequestProduct_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ControlToValidate="ddlRequestProduct"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Product type"
                                Operator="NotEqual" ValidationGroup="btnGo2" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                        <td align="left" class="leftField" id="tdlblRequestAmc" runat="server" visible="false">
                            <asp:Label ID="lblRequestAmc" runat="server" CssClass="FieldName" Text="Issuer"></asp:Label>
                        </td>
                        <td id="tdddlRequestAmc" visible="false" runat="server">
                            <asp:DropDownList ID="ddlRequestAmc" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlIssuer_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="ddlRequestAmc"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="Please Select AMC Code" Operator="NotEqual"
                                ValidationGroup="btnGo2" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                        <td id="td5" align="left" class="leftField" width="16%" runat="server" visible="true">
                            <asp:Label ID="lblRequestCommissionType" runat="server" CssClass="FieldName" Text="Commission Type:"></asp:Label>
                        </td>
                        <td id="td6" runat="server" visible="true">
                            <asp:DropDownList ID="ddlRequestCommissionType" runat="server" CssClass="cmbField"
                                AutoPostBack="true">
                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                  <asp:ListItem Text="All" Value="AL"></asp:ListItem>
                                <asp:ListItem Text="Upfront" Value="UF" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Trail" Value="TC"></asp:ListItem>
                              
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="ddlRequestCommissionType"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Commission type"
                                Operator="NotEqual" ValidationGroup="btnGo2" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                         <td align="left" class="leftField" width="20%" id="tdRSCategory" runat="server" visible="false">
                            <asp:Label ID="lblRSCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td align="right" id="tdddlRSCategory" runat="server" visible="false">
                            <asp:DropDownList ID="ddlRSProductCategory" runat="server" AutoPostBack="true" CssClass="cmbField"
                                OnSelectedIndexChanged="ddlRSProductCategory_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToValidate="ddlRSProductCategory"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please select an Category type"
                                Operator="NotEqual" ValidationGroup="btnGo2" ValueToCompare="Select"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr id="tdNonMF" runat="server" visible="false">
                        <td align="left" class="leftField">
                            <asp:Label ID="lblRSIssueType" runat="server" CssClass="FieldName" Text="Issue Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddRSlIssueType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddRSlIssueType_OnSelectedIndexChanged"
                                AutoPostBack="true">
                                <asp:ListItem Text="Select" Value="Select"></asp:ListItem>
                                <asp:ListItem Text="Closed Issues" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Current Issues" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToValidate="ddRSlIssueType"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue Type"
                                Operator="NotEqual" ValidationGroup="btnGo2" ValueToCompare="Select" ></asp:CompareValidator>
                        </td>
                        <td align="left" class="leftField">
                            <asp:Label ID="lblRSIssueName" runat="server" CssClass="FieldName" Text="Issue Name"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRSIssueName" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                             <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToValidate="ddlRSIssueName"
                                CssClass="cvPCG" Display="Dynamic" ErrorMessage="<br />Please Select Issue "
                                Operator="NotEqual" ValidationGroup="btnGo2" ValueToCompare="0" ></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftField" id="td7" runat="server" visible="true">
                            <asp:Label ID="lblRequestYear" runat="server" CssClass="FieldName" Text="Year:"></asp:Label>
                        </td>
                        <td class="rightField" id="td8" runat="server" visible="true">
                            <asp:DropDownList ID="ddlRequestYear" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlRequestYear"
                                CssClass="rfvPCG" ErrorMessage="<br />Please select a Year" Display="Dynamic"
                                runat="server" InitialValue="0" ValidationGroup="btnGo2"> </asp:RequiredFieldValidator>
                        </td>
                        <td id="td9" class="leftField" runat="server" visible="true">
                            <asp:Label ID="lblRequestMnthQtr" Text="Month/Quarter:" runat="server" CssClass="FieldName"></asp:Label>
                        </td>
                        <td class="rightField" id="td10" runat="server" visible="true">
                            <asp:DropDownList ID="ddlRequesttMnthQtr" runat="server" CssClass="cmbField">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="ddlRequesttMnthQtr"
                                CssClass="rfvPCG" ErrorMessage="<br />Please Select Month" Display="Dynamic"
                                runat="server" InitialValue="0" ValidationGroup="btnGo2"> </asp:RequiredFieldValidator>
                        </td>
                        
                        <td class="rightField" align="right" style="width: 50px;" id="tdbtnGo2" runat="server"
                            visible="true">
                            <asp:Button ID="btnGo2" runat="server" Text="Go" CssClass="PCGButton" ValidationGroup="btnGo2"
                                OnClick="btnGo2_OnClick" />
                        </td>
                    </tr>
                     
                    <%--<tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="Select Type:" CssClass="FieldName"></asp:Label>
                        </td>
                        <%--<td >
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlType_OnSelectedIndexChanged"
                                AutoPostBack="true" Width="170px" InitialValue="Select">
                                <asp:ListItem Value="Select">Select</asp:ListItem>
                                <asp:ListItem Value="RI">RequestId</asp:ListItem>
                                <asp:ListItem Value="RD">Request Date</asp:ListItem>
                            </asp:DropDownList>
                            <span id="Span6" class="spnRequiredField">*</span>
                            
                            <asp:RequiredFieldValidator ID="rfvType5" runat="server" ErrorMessage="Please Select a Type"
                                CssClass="rfvPCG" ControlToValidate="ddlType" ValidationGroup="btnGo2" Display="Dynamic"
                                InitialValue="Select"></asp:RequiredFieldValidator>
                        </td>--%>
                    </tr>--%>
                    <tr>
                        <td align="right" id="tdlblRequestId" runat="server" visible="false">
                            <asp:Label ID="lblRequestId" runat="server" Text="Select RequestId:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td id="tdtxtRequestId" runat="server" visible="false">
                            <asp:TextBox ID="txtRequestId" runat="server" CssClass="txtField"></asp:TextBox>
                            <span id="Span5" class="spnRequiredField">*</span>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvRequestId" runat="server" ErrorMessage="Please Enter A RequestId"
                                CssClass="rfvPCG" ControlToValidate="txtRequestId" ValidationGroup="btnGo2" Display="Dynamic"
                                InitialValue="Select"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" id="tdlbFromdate" runat="server" visible="false">
                            <asp:Label ID="lbFromdate" runat="server" Text="From Date:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td id="tdtxtReqFromDate" runat="server" visible="false">
                            <telerik:RadDatePicker ID="txtReqFromDate" CssClass="txtField" runat="server" Culture="English (United States)"
                                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                                TabIndex="3" Width="150px" AutoPostBack="true">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                    Skin="Telerik" EnableEmbeddedSkins="false">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                            <span id="Span18" class="spnRequiredField">*</span>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Requested Date"
                                Display="Dynamic" ControlToValidate="txtReqFromDate" InitialValue="" ValidationGroup="btnGo2">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td align="right" id="tdlblToDate" runat="server" visible="false">
                            <asp:Label ID="lblToDate" runat="server" Text="To Date:" CssClass="FieldName"></asp:Label>
                        </td>
                        <td id="tdtxtReqToDate" runat="server" visible="false">
                            <telerik:RadDatePicker ID="txtReqToDate" CssClass="txtField" runat="server" Culture="English (United States)"
                                Skin="Telerik" EnableEmbeddedSkins="false" ShowAnimation-Type="Fade" MinDate="1900-01-01"
                                TabIndex="3" Width="150px" AutoPostBack="true">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                    Skin="Telerik" EnableEmbeddedSkins="false">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="d/M/yyyy" DateFormat="d/M/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                            <span id="Span4" class="spnRequiredField">*</span>
                            <br />
                            <asp:RequiredFieldValidator ID="rfvToDate" runat="server" CssClass="rfvPCG" ErrorMessage="Please Enter Requested Date"
                                Display="Dynamic" ControlToValidate="txtReqToDate" InitialValue="" ValidationGroup="btnGo2">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvToDate" runat="server" CssClass="rfvPCG" ErrorMessage="To-date should be after From-date"
                                Display="Dynamic" ControlToValidate="txtReqToDate" ControlToCompare="txtReqFromDate"
                                Operator="GreaterThan" ValidationGroup="btnGo2"></asp:CompareValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="leftLabel">
                            &nbsp;
                        </td>
                        <td class="leftLabel">
                            &nbsp;
                        </td>
                        <td class="rightData">
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="100%" cellspacing="0" cellpadding="0">
                    <tr>
                        <td id="tdBulkOrderStatusList" runat="server">
                            <div id="DivBulkOrderStatusList" runat="server" style="width: 100%; padding-left: 5px; height: auto">
                                <telerik:RadGrid ID="gvBrokerageRequestStatus" runat="server" AllowAutomaticDeletes="false"
                                    EnableEmbeddedSkins="false" AllowFilteringByColumn="true" AutoGenerateColumns="False"
                                    ShowStatusBar="false" ShowFooter="true" AllowPaging="true" AllowSorting="true"
                                    GridLines="none" AllowAutomaticInserts="false" Skin="Telerik" EnableHeaderContextMenu="true"
                                    Visible="false" OnItemDataBound="gvBrokerageRequestStatus_OnDataBound" OnItemCommand="gvBrokerageRequestStatus_OnItemCommand"
                                    OnNeedDataSource="gvBrokerageRequestStatus_OnNeedDataSource">
                                    <ExportSettings HideStructureColumns="true">
                                    </ExportSettings>
                                    <MasterTableView DataKeyNames="WR_RequestId" Width="99%" AllowMultiColumnSorting="True"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <telerik:GridTemplateColumn AllowFiltering="false" HeaderStyle-Width="20px" UniqueName="chkBoxColumn">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkboxSelectAll" runat="server"  />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkItem" runat="server" ></asp:CheckBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="20px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridButtonColumn CommandName="Calculation" Text="View Calculation" HeaderText="Calculation"
                                                HeaderStyle-Width="47px" UniqueName="Calculation">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="true" VerticalAlign="top" />
                                            </telerik:GridButtonColumn>
                                            <telerik:GridBoundColumn DataField="PA_AMCName" UniqueName="PA_AMCName" HeaderText="Amc Name"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="PA_AMCName" FilterControlWidth="50px">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="true" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                              <%-- <telerik:GridBoundColumn DataField="Cannel" UniqueName="Cannel" HeaderText="commission type"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="Cannel" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>--%>
                                            <telerik:GridBoundColumn DataField="brokeragemonth" UniqueName="brokeragemonth" HeaderText="Month"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="brokeragemonth" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="brokerageyear" UniqueName="brokerageyear" HeaderText="Year"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="brokerageyear" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WRL_ExecuteStartTime" UniqueName="WRL_ExecuteStartTime"
                                                HeaderText="Start Date Time" ShowFilterIcon="false" AutoPostBackOnFilter="true"
                                                AllowFiltering="true" HeaderStyle-Width="67px" SortExpression="WRL_ExecuteStartTime"
                                                FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WRL_EndTime" UniqueName="WRL_EndTime" HeaderText="End Date Time"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="WRL_EndTime" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StatusMsg" UniqueName="StatusMsg" HeaderText="Status"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="StatusMsg" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="WR_RequestId" UniqueName="WR_RequestId" HeaderText="RequestId"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="WR_RequestId" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="RequestCount" UniqueName="RequestCount" HeaderText="Count"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="RequestCount" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                              <telerik:GridBoundColumn DataField="WRD_InputParameterValue" UniqueName="WP_ParameterCode" HeaderText="Commission Type"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="WP_ParameterCode" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn DataField="Cannel" UniqueName="Cannel" HeaderText="Online"
                                                ShowFilterIcon="false" AutoPostBackOnFilter="true" AllowFiltering="true" HeaderStyle-Width="67px"
                                                SortExpression="WR_Online" FilterControlWidth="50px" CurrentFilterFunction="Contains">
                                                <ItemStyle Width="100px" HorizontalAlign="left" Wrap="false" VerticalAlign="top" />
                                            </telerik:GridBoundColumn>
                                         
                                        </Columns>
                                    </MasterTableView>
                                    <ClientSettings>
                                        <Scrolling AllowScroll="true" UseStaticHeaders="false" ScrollHeight="380px" />
                                        <Resizing AllowColumnResize="true" />
                                        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                    </ClientSettings>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                    <tr>
                    <td colspan="2">
                    <asp:Button ID="btnReprocess" runat="server" CssClass="PCGButton" OnClick="RequestModification_Click"
                                Text="Reprocess"   Visible="false"  CommandName="Reprocess"/>
                                <asp:Button ID="btnDelete" runat="server" CssClass="PCGButton" OnClick="RequestModification_Click"
                                Text="Delete"  Visible="false"  CommandName="Delete"/>
                    </td>
                
                    </tr>
                </table>
                <div style="width: 100%; overflow: scroll;" runat="server" visible="false" id="dvMfMIS">
                    <telerik:RadGrid ID="gvCommissionReceiveRecon" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="350%"
                        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
                        EnableHeaderContextMenu="true" OnItemDataBound="gvWERPTrans_ItemDataBound" EnableHeaderContextFilterMenu="true"
                        OnNeedDataSource="gvCommissionReceiveRecon_OnNeedDataSource">
                        <MasterTableView AllowMultiColumnSorting="True" AutoGenerateColumns="false" CommandItemDisplay="None"
                            GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true" GroupLoadMode="Client"
                            ShowGroupFooter="true" DataKeyNames="CMFT_MFTransId,totalNAV,perDayTrail,PerDayAssets,commissionType">
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="false" UniqueName="action" DataField="action"
                                    Visible="false" HeaderStyle-Width="3%">
                                    <HeaderTemplate>
                                        <input id="chkIdAll" name="chkIdAll" type="checkbox" onclick="checkAllBoxes()" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkId" runat="server" />
                                        <asp:HiddenField ID="hdnchkBx" runat="server" Value='<%# Eval("CMFT_MFTransId").ToString()%>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnSave" CssClass="FieldName" runat="server" Text="Update Expected Amt"
                                            OnClick="btnSave_Click" />
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="4%" HeaderText="RuleName" DataField="ACSR_CommissionStructureRuleName"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_CommissionStructureRuleName"
                                    SortExpression="ACSR_CommissionStructureRuleName" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="100px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="3%" HeaderText="Customer Name" DataField="CustomerName"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="CustomerName" SortExpression="CustomerName"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Order No" DataField="CO_OrderId"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="CO_OrderId" SortExpression="CO_OrderId"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Folio No" DataField="CMFA_FolioNum"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="CMFA_FolioNum" SortExpression="CMFA_FolioNum"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="3.5%" HeaderText="SchemePlan" DataField="schemeplanname"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="schemeplanname" SortExpression="schemeplanname"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="113px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Transaction Date" DataField="transactiondate"
                                    UniqueName="transactiondate" SortExpression="transactiondate" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="140px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Fee Start Date" DataField="WCD_Tr_EndDate"
                                    UniqueName="WCD_Tr_EndDate" SortExpression="WCD_Tr_EndDate" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}"
                                    HeaderStyle-HorizontalAlign="Center" Visible="false">
                                    <ItemStyle Width="140px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Fee End Date" DataField="WCD_Tr_startDate"
                                    UniqueName="WCD_Tr_startDate" SortExpression="WCD_Tr_startDate" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}"
                                    HeaderStyle-HorizontalAlign="Center" Visible="true">
                                    <ItemStyle Width="140px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Status" DataField="WOS_OrderStep"
                                    UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="140px" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Sub Broker Code" DataField="CMFT_SubBrokerCode"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="CMFT_SubBrokerCode" SortExpression="CMFT_SubBrokerCode"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="ARN No" DataField="ARN_No"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ARN_No" SortExpression="ARN_No"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Transaction Type" DataField="transactiontype"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="transactiontype" SortExpression="transactiontype"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Is T15" DataField="WCD_IsT15"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="WCD_IsT15" SortExpression="WCD_IsT15"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Category" DataField="PAIC_AssetInstrumentCategoryName"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="PAIC_AssetInstrumentCategoryName"
                                    SortExpression="PAIC_AssetInstrumentCategoryName" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Associates" DataField="AA_ContactPersonName"
                    UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}" HeaderStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>
                 <telerik:GridBoundColumn HeaderStyle-Width="113px" HeaderText="Parents" DataField="ParentName" HeaderStyle-HorizontalAlign="Center"
                    UniqueName="Parents" SortExpression="ParentName" AutoPostBackOnFilter="true"
                    ShowFilterIcon="false" CurrentFilterFunction="Contains" DataFormatString="{0:d}">
                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Commission Type" DataField="commissionType"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="commissionType" SortExpression="commissionType"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="CalculatedOn" DataField="WCCO_CalculatedOn"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="WCCO_CalculatedOn" SortExpression="WCCO_CalculatedOn"
                                    AutoPostBackOnFilter="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    DataFormatString="{0:d}">
                                    <ItemStyle HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Service Tax(%)" DataField="ACSR_ServiceTaxValue"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_ServiceTaxValue" SortExpression="ACSR_ServiceTaxValue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="KKC(%)" DataField="WCD_ACSR_KKC"
                                    HeaderStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="SKC(%)" DataField="WCD_ACSR_SKC"
                                    HeaderStyle-HorizontalAlign="Left"  AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="TDS(%)" DataField="ACSR_ReducedValue"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_ReducedValue" SortExpression="ACSR_ReducedValue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Amount" DataField="amount"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="amount" SortExpression="amount"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" Aggregate="Sum"
                                    CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Units" DataField="units"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="units" SortExpression="units"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Age" DataField="Age"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="Age" SortExpression="Age" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Current Value" DataField="currentvalue"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="currentvalue" SortExpression="currentvalue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Commission Amount Received"
                                    DataField="receivedamount" HeaderStyle-HorizontalAlign="Center" UniqueName="receivedamount"
                                    SortExpression="receivedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                                    Visible="false">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Total NAV" DataField="CumNAv"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="CumNAv" SortExpression="CumNAv"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Total Holding value"
                                    DataField="totalNAV" HeaderStyle-HorizontalAlign="Center" UniqueName="totalNAV"
                                    SortExpression="totalNAV" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                                    DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Transaction Nav" DataField="CMFT_NAV"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="CMFT_NAV" SortExpression="CMFT_NAV"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Closing Nav" DataField="ClS_NAV"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ClS_NAV" SortExpression="ClS_NAV"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="PerDayAssets" DataField="PerDayAssets"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="PerDayAssets" SortExpression="PerDayAssets"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Per Day Trail Rate" DataField="perDayTrail"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="perDayTrail" SortExpression="perDayTrail"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Receivable Rate" DataField="Rec_ACSR_BrokerageValue"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="Rec_ACSR_BrokerageValue" SortExpression="Rec_ACSR_BrokerageValue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Receivable Rate unit"
                                    DataField="Rec_WCU_UnitCode" HeaderStyle-HorizontalAlign="Center" UniqueName="Rec_WCU_UnitCode"
                                    SortExpression="Rec_WCU_UnitCode" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Receivable Expected Commission"
                                    DataField="Rec_Expectedamount" HeaderStyle-HorizontalAlign="Center" UniqueName="Rec_Expectedamount"
                                    SortExpression="Rec_Expectedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCalculatedColumn DataFields="Rec_Expectedamount,ACSR_ReducedValue,ACSR_ServiceTaxValue,WCD_ACSR_KKC,WCD_ACSR_SKC"
                                    Expression="(({0}*100)/({2}+100))-(({0}*100)/({3}+100))-(({0}*100)/({4}+100))-({0}*{1}/100)" SortExpression="RecborkageExpectedvalue"
                                    UniqueName="RecborkageExpectedvalue" HeaderText="Receivable Net Commission" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" HeaderStyle-Width="2%" ShowFilterIcon="false"
                                    Visible="true" DataType="System.Decimal" AllowFiltering="false" DataFormatString="{0:n3}">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridCalculatedColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Payable Rate" DataField="ACSR_BrokerageValue"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ACSR_BrokerageValue" SortExpression="ACSR_BrokerageValue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText=" Payable Rate unit" DataField="WCU_UnitCode"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="WCU_UnitCode" SortExpression="WCU_UnitCode"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Payable Expected Commission"
                                    DataField="expectedamount" HeaderStyle-HorizontalAlign="Center" UniqueName="expectedamount"
                                    SortExpression="expectedamount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCalculatedColumn DataFields="expectedamount,ACSR_ReducedValue,ACSR_ServiceTaxValue,WCD_ACSR_KKC,WCD_ACSR_SKC"
                                    Expression="(({0}*100)/({2}+100))-(({0}*100)/({3}+100))-(({0}*100)/({4}+100))-({0}*{1}/100)" SortExpression="PayborkageExpectedvalue"
                                    UniqueName="PayborkageExpectedvalue" HeaderText="Payable Net Commission" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" HeaderStyle-Width="2%" ShowFilterIcon="false"
                                    Visible="true" DataType="System.Decimal" AllowFiltering="false" DataFormatString="{0:n3}">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridCalculatedColumn>
                                <telerik:GridCalculatedColumn DataFields="RecborkageExpectedvalue,PayborkageExpectedvalue,sum"
                                    Expression="{0}-{1}" SortExpression="Retention1" UniqueName="Retention1" HeaderText="Retention"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="2%"
                                    ShowFilterIcon="false" AllowFiltering="false" DataType="System.Decimal"
                                    Aggregate="Sum">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridCalculatedColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="2%" HeaderText="Commission Adjustment Amount"
                                    DataField="CMFT_ReceivedCommissionAdjustment" SortExpression="CMFT_ReceivedCommissionAdjustment"
                                    HeaderStyle-HorizontalAlign="Center" AutoPostBackOnFilter="true" ShowFilterIcon="false"
                                    Visible="false" UniqueName="CMFT_ReceivedCommissionAdjustment">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAdjustAmount" CssClass="txtField" runat="server" Text='<%# Eval("adjust").ToString()%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAdjustAmountMultiple" CssClass="txtField" runat="server" />
                                    </FooterTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Amount Difference" DataField="diff"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="diff" SortExpression="diff"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Commission Updated Expected Amount"
                                    DataField="UpdatedExpectedAmount" HeaderStyle-HorizontalAlign="Center" UniqueName="UpdatedExpectedAmount"
                                    SortExpression="UpdatedExpectedAmount" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="2%" HeaderText="Recon Status" DataField="ReconStatus"
                                    HeaderStyle-HorizontalAlign="Center" UniqueName="ReconStatus" SortExpression="ReconStatus"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            <Resizing AllowColumnResize="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
                <div id="dvNCDIPOMIS" style="width: 100%; overflow: scroll;" runat="server" visible="false">
                    <telerik:RadGrid ID="rgNCDIPOMIS" runat="server" GridLines="None" AutoGenerateColumns="False"
                        PageSize="15" AllowSorting="true" AllowPaging="True" ShowStatusBar="True" ShowFooter="true"
                        Skin="Telerik" EnableViewState="true" EnableEmbeddedSkins="false" Width="110%"
                        AllowFilteringByColumn="true" AllowAutomaticInserts="false" ExportSettings-ExportOnlyData="true"
                        EnableHeaderContextMenu="true" EnableHeaderContextFilterMenu="true" OnNeedDataSource="rgNCDIPOMIS_OnNeedDataSource"
                        OnItemDataBound="rgNCDIPOMIS_OnItemDataBound">
                        <MasterTableView Width="180%" AllowMultiColumnSorting="True" AutoGenerateColumns="false"
                            CommandItemDisplay="None" GroupsDefaultExpanded="false" ExpandCollapseColumn-Groupable="true"
                            GroupLoadMode="Client" ShowGroupFooter="true">
                            <Columns>
                                <telerik:GridBoundColumn HeaderStyle-Width="20%" HeaderText="Rule Name" DataField="ACSR_CommissionStructureRuleName"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="ACSR_CommissionStructureRuleName"
                                    SortExpression="ACSR_CommissionStructureRuleName" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Application No." DataField="CO_ApplicationNumber"
                                    HeaderStyle-HorizontalAlign="left" UniqueName="Application No" SortExpression="CO_ApplicationNumber"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="15%" HeaderText="Customer Name" DataField="CustomerName"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="CustomerName" SortExpression="CustomerName"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Order No." DataField="CO_OrderId"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="CO_OrderId" SortExpression="CO_OrderId"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Transaction Date." DataField="transactionDate"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="transactionDate" SortExpression="transactionDate"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="20%" HeaderText="Issue" DataField="AIM_IssueName"
                                    UniqueName="AIM_IssueName" SortExpression="AIM_IssueName" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains">
                                    <ItemStyle Width="" HorizontalAlign="left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Status" DataField="WOS_OrderStep"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="WOS_OrderStep" SortExpression="WOS_OrderStep"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Sub Broker Code" DataField="AAC_AgentCode"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="AAC_AgentCode" SortExpression="AAC_AgentCode"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Associates" DataField="AA_ContactPersonName"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="AA_ContactPersonName" SortExpression="AA_ContactPersonName"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Parent" DataField="ParentName"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="ParentName" SortExpression="ParentName"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Commission Type" DataField="commissionType"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="commissionType" SortExpression="commissionType"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="CalculatedOn" DataField="calculatedOn"
                                    HeaderStyle-HorizontalAlign="Left" UniqueName="calculatedOn" SortExpression="calculatedOn"
                                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <%-- <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Commission SubType" DataField="IncentiveType"
                    HeaderStyle-HorizontalAlign="Left" UniqueName="IncentiveType" SortExpression="IncentiveType"
                    AutoPostBackOnFilter="true" AllowFiltering="true" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                                <%--<telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="User Type" DataField="CustomerAssociate"
                    HeaderStyle-HorizontalAlign="Right" UniqueName="CustomerAssociate" SortExpression="CustomerAssociate"
                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                    FooterStyle-HorizontalAlign="Right" Visible="false">
                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Ordered Qty\Accepted Qty"
                                    DataField="allotedQty" UniqueName="allotedQty" SortExpression="allotedQty" AutoPostBackOnFilter="true"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" AllowFiltering="false">
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Holdings Amount" DataField="holdings"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="holdings" SortExpression="holdings"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" Aggregate="Sum"
                                    CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Broker Name" DataField="XB_BrokerShortName"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="XB_BrokerShortName" SortExpression="XB_BrokerShortName"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Category" DataField="AIIC_InvestorCatgeoryName"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="AIIC_InvestorCatgeoryName" SortExpression="AIIC_InvestorCatgeoryName"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Series" DataField="AID_IssueDetailName"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="AID_IssueDetailName" SortExpression="AID_IssueDetailName"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Tenure" DataField="AID_Tenure"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="AID_Tenure" SortExpression="AID_Tenure"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Mobilised No Of Application"
                                    DataField="ParentMobilize_Orders" AllowFiltering="false" ShowFilterIcon="false"
                                    CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Mobilised Amount" DataField="ParentMobilize_Amount"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Service Tax(%)" DataField="ACSR_ServiceTaxValue"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="ACSR_ServiceTaxValue" SortExpression="ACSR_ServiceTaxValue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="KKC(%)" DataField="WCD_ACSR_KKC"
                                    HeaderStyle-HorizontalAlign="Left" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="SKC(%)" DataField="WCD_ACSR_SKC"
                                    HeaderStyle-HorizontalAlign="Left"  AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="TDS(%)" DataField="ACSR_ReducedValue"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="ACSR_ReducedValue" SortExpression="ACSR_ReducedValue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Receivable  Rate" DataField="Rec_rate"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="Rec_rate" SortExpression="Rec_rate"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Receivable Rate unit"
                                    DataField="Rec_WCU_UnitCode" HeaderStyle-HorizontalAlign="Right" UniqueName="Rec_WCU_UnitCode"
                                    SortExpression="Rec_WCU_UnitCode" AutoPostBackOnFilter="true" AllowFiltering="false"
                                    ShowFilterIcon="false" CurrentFilterFunction="Contains" FooterStyle-HorizontalAlign="Right"
                                    Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Receivable" DataField="Rec_brokeragevalue"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="Rec_brokeragevalue" SortExpression="Rec_brokeragevalue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                              
                                   <telerik:GridCalculatedColumn DataFields="Rec_brokeragevalue,ACSR_ReducedValue,ACSR_ServiceTaxValue,WCD_ACSR_KKC,WCD_ACSR_SKC"
                                    Expression="(({0}*100)/({2}+100))-(({0}*100)/({3}+100))-(({0}*100)/({4}+100))-({0}*{1}/100)" SortExpression="Rec_borkageExpectedvalue"
                                    UniqueName="Rec_borkageExpectedvalue" HeaderText="Receivable Net Commission" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" HeaderStyle-Width="2%" ShowFilterIcon="false"
                                    Visible="true" DataType="System.Decimal" AllowFiltering="false" DataFormatString="{0:n3}">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridCalculatedColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Payable Rate" DataField="rate"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="rate" SortExpression="rate" AutoPostBackOnFilter="true"
                                    AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Payable Rate unit" DataField="WCU_UnitCode"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="WCU_UnitCode" SortExpression="WCU_UnitCode"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Left" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderStyle-Width="10%" HeaderText="Payable" DataField="brokeragevalue"
                                    HeaderStyle-HorizontalAlign="Right" UniqueName="brokeragevalue" SortExpression="brokeragevalue"
                                    AutoPostBackOnFilter="true" AllowFiltering="false" ShowFilterIcon="false" CurrentFilterFunction="Contains"
                                    FooterStyle-HorizontalAlign="Right" Aggregate="Sum" Visible="false">
                                    <ItemStyle Width="" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridBoundColumn>
                                <telerik:GridCalculatedColumn DataFields="brokeragevalue,ACSR_ReducedValue,ACSR_ServiceTaxValue,WCD_ACSR_KKC,WCD_ACSR_SKC"
                                    Expression="(({0}*100)/({2}+100))-(({0}*100)/({3}+100))-(({0}*100)/({4}+100))-({0}*{1}/100)" SortExpression="borkageExpectedvalue"
                                    UniqueName="borkageExpectedvalue" HeaderText="Net Payable" AutoPostBackOnFilter="true"
                                    CurrentFilterFunction="Contains" HeaderStyle-Width="2%" ShowFilterIcon="false"
                                    Visible="true" DataType="System.Decimal" AllowFiltering="false" DataFormatString="{0:n3}">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridCalculatedColumn>
                                
                                <telerik:GridCalculatedColumn DataFields="Rec_borkageExpectedvalue,borkageExpectedvalue"
                                    Expression="{0}-{1}" SortExpression="Retention1" UniqueName="Retention1" HeaderText="Retention"
                                    AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" HeaderStyle-Width="10%"
                                    ShowFilterIcon="false" Aggregate="Sum" Visible="false" AllowFiltering="false"
                                    DataType="System.Decimal">
                                    <ItemStyle Width="90px" HorizontalAlign="Right" Wrap="false" VerticalAlign="Top" />
                                </telerik:GridCalculatedColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                            <Resizing AllowColumnResize="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnExportFilteredData" />
            </Triggers>
        </asp:UpdatePanel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<asp:HiddenField ID="hdnschemeId" runat="server" Visible="false" />
<asp:HiddenField ID="hdnCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnFromDate" runat="server" />
<asp:HiddenField ID="hdnToDate" runat="server" />
<asp:HiddenField ID="hdnSBbrokercode" runat="server" />
<asp:HiddenField ID="hdnIssueId" runat="server" />
<asp:HiddenField ID="hdnProductCategory" runat="server" />
<asp:HiddenField ID="hdnAgentCode" runat="server" />
<div runat="server" id="divBtnActionSection" visible="false">
    <asp:Button ID="btnUpload" runat="server" Text="Mark Recon Status" CssClass="PCGLongButton"
        OnClick="btnUpload_OnClick" />
</div>
