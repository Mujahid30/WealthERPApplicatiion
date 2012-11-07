<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerISARequest.ascx.cs"
    Inherits="WealthERP.Customer.CustomerISARequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<%--
<ajaxToolkit:CollapsiblePanelExtender ID="cpe" runat="Server"
    TargetControlID="Panel1"
    CollapsedSize="0"
    ExpandedSize="300"
    Collapsed="True"
    ExpandControlID="LinkButton1"
    CollapseControlID="LinkButton1"
    AutoCollapse="False"
    AutoExpand="False"
    ScrollContents="True"
    TextLabelID="Label1"
    CollapsedText="Show Details..."
    ExpandedText="Hide Details" 
    ImageControlID="Image1"
    ExpandedImage="~/images/collapse.jpg"
    CollapsedImage="~/images/expand.jpg"
    ExpandDirection="Vertical" />--%>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
</script>

<script type="text/javascript">

    function ResetPagetoFirstLoadState() {
        document.getElementById("<%= ddlCustomerCategory.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlPriority.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlStatusStage1.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
        document.getElementById("<%= rdbNewCustomer.ClientID %>").checked = true;
        document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        document.getElementById("<%= trCustomerSearch.ClientID %>").style.visibility = 'collapse';
        document.getElementById("<%= trCustomerNameDetails.ClientID %>").style.visibility = 'visible';
        document.getElementById("<%= txtPanNum.ClientID %>").value = "";
        document.getElementById("<%= txtGenerateReqstNum.ClientID %>").value = "";
        document.getElementById("<%= ddlBMBranch.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= txtMobileNum.ClientID %>").value = "";
        document.getElementById("<%= txtEmailID.ClientID %>").value = "";
        document.getElementById("<%= txtCustomerNameEntry.ClientID %>").value = "";
        document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
        document.getElementById("<%= ddlStatusStage1.ClientID %>").selectedIndex = "0";        
    }
</script>


<script type="text/javascript">
    function HideAndShowBasedOnRole(val) {
        
        if (val == "ISARQNewEntry") {
            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;
        }

        if (val == "ISARQNewEntryDisable") {
            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;

            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = false;

        }
        if (val == "ISARQStatusDone") {
            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;

            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;         

        }
        if (val == "CUSCR") {
            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;         
            
            document.getElementById("<%= txtComments.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = false;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = false;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = false;
        }
        if (val == "VERIfY") {
        
            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;  
        
            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = false;
            
            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;
        }
        if (val == "VERIfYDisable") {

            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;

            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;
        }

        
        if (val == "ISAGE") {
            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;  
        
            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = false;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = false;
        }

        if (val == "ISAGEDisable") {
            document.getElementById("<%= trSelectNewOrExistingCustomer.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;

            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;
        }      
    }
</script>

<script type="text/javascript">
    function GetVerificationType(val) {
        //in val u get dropdown list selected value
        var verificationType = val;
        var cusType = document.getElementById("<%= ddlCustomerCategory.ClientID %>").selectedIndex;
        document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        if (verificationType == "Normal" || verificationType == "Select") {
            document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        }
        else if (verificationType == "Urgent" && cusType == "1") {
            document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdPanLink.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= tdAddressLink.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= tdISALink.ClientID %>").style.visibility = 'visible';


        }
        else if (verificationType == "Urgent" && cusType == "2") {

            document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdPanLink.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdAddressLink.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdISALink.ClientID %>").style.visibility = 'visible';
        }
    }
    function GetVerificationTypeThroughCustCategory(val) {
        //in val u get dropdown list selected value
        var verificationType = val;
        var priorityType = document.getElementById("<%= ddlPriority.ClientID %>").selectedIndex;

        document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        if (priorityType == "0" || priorityType == "2") {
            document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        }
        else if (verificationType == "RBL" && priorityType == "1") {
            document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdPanLink.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= tdAddressLink.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= tdISALink.ClientID %>").style.visibility = 'visible';
        }
        else if (verificationType == "NRBL" && priorityType == "1") {
            document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdPanLink.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdAddressLink.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= tdISALink.ClientID %>").style.visibility = 'visible';
        }
    }
</script>


<script type="text/javascript">
    function enableDisablePriority(val) {
        document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        if (val == "true") {
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            
        }
        if (val == "false") {
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = false;
           
        }
    }   

</script>
<script type="text/javascript">
    function chkPanExists() {
        $("#<%= hidValidCheck.ClientID %>").val("0");
        if ($("#<%=txtPanNum.ClientID %>").val() == "") {
            $("#spnLoginStatus").html("");
            return;
        }

        $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            url: "ControlHost.aspx/CheckPANNoAvailability",
            data: "{ 'PanNumber': '" + $("#<%=txtPanNum.ClientID %>").val() + "','BranchId': '" + $("#<%=ddlBMBranch.ClientID %>").val() + "','adviserId': '" + $("#<%=hdnAdviserID.ClientID %>").val() + "' }",
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
                    alert("Pan Number Already Exists");
                    return false;
                }

            }
        });
    }
</script>

<script type="text/javascript">
    function chkPanorCustomerNameRequired(source, args) {
       
        if (document.getElementById("<%= rdbNewCustomer.ClientID %>").checked == true) {
            var txtnameEntry = document.getElementById("<%= txtCustomerNameEntry.ClientID %>").value;
            var txtPanNumEntry = document.getElementById("<%= txtPanNum.ClientID %>").value;
            alert("chk1");
            if (txtnameEntry == "" && txtPanNumEntry == "")
                args.IsValid = false;
            else if (txtnameEntry == "" && txtPanNumEntry != "")
                args.IsValid = true;
            else if (txtnameEntry != "" && txtPanNumEntry == "")
                args.IsValid = true;
            else if (txtnameEntry != "" && txtPanNumEntry !== "")
                args.IsValid = true;
        }
        if (document.getElementById("<%= rdbExistingCustomer.ClientID %>").checked == true) {
            var txtnameEntry = document.getElementById("<%= txtCustomerName.ClientID %>").value;
            var custId = document.getElementById("<%= txtCustomerId.ClientID %>").value;

            if (custId == "") {                
                args.IsValid = false;
            }
            else
            {
               args.IsValid = true;
            }
           
        }
    } 
</script>

<script type="text/javascript">
    function HideCustomerSearch() {
        
        if (document.getElementById("<%= rdbExistingCustomer.ClientID %>").checked == true) {
            document.getElementById("<%= trCustomerSearch.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= trCustomerNameDetails.ClientID %>").style.visibility = 'collapse';
        }
        else if (document.getElementById("<%= rdbNewCustomer.ClientID %>").checked == true) {
            document.getElementById("<%= trCustomerSearch.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= trCustomerNameDetails.ClientID %>").style.visibility = 'visible';
        }
        
    }
</script>

<script type="text/javascript">

    function DisplayCustomerSearch() {
        //in val u get dropdown list selected value
        document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'collapse';
        document.getElementById("<%= trCustomerSearch.ClientID %>").style.visibility = 'collapse';
        if (document.getElementById("<%= rdbExistingCustomer.ClientID %>").checked == true) {
            document.getElementById("<%= trCustomerSearch.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= txtCustomerName.ClientID %>").value = null;
            document.getElementById("<%= lblGetBranch.ClientID %>").innerHTML = null;
            document.getElementById("<%= lblGetRM.ClientID %>").innerHTML = null;
            document.getElementById("<%= txtPanNum.ClientID %>").value = null;
            document.getElementById("<%= txtMobileNum.ClientID %>").value = null;
            document.getElementById("<%= txtEmailID.ClientID %>").value = null;
            document.getElementById("<%= trCustomerNameDetails.ClientID %>").style.visibility = 'collapse';

        }
        else if (document.getElementById("<%= rdbNewCustomer.ClientID %>").checked == true) {
            document.getElementById("<%= trCustomerSearch.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= trCustomerNameDetails.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = false;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = false;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = false;
            document.getElementById("<%= txtPanNum.ClientID %>").value = null;
            document.getElementById("<%= txtMobileNum.ClientID %>").value = null;
            document.getElementById("<%= txtCustomerName.ClientID %>").value = null;
            document.getElementById("<%= lblGetRM.ClientID %>").innerHTML = null;
            document.getElementById("<%= lblGetBranch.ClientID %>").innerHTML = null;
            document.getElementById("<%= txtEmailID.ClientID %>").value = null;
        }


    }
</script>

<script type="text/javascript">

    function openpopupAddISA() {
        window.open('PopUp.aspx?PageId=ViewCustomerProofs &LinkId= AddISA', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function openpopupAddPan() {
        window.open('PopUp.aspx?PageId=ViewCustomerProofs &LinkId= AddPan', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

    function openpopupViewFormsAndProofs() {
        window.open('PopUp.aspx?PageId=ViewCustomerProofs &LinkId= ViewFormsAndProofs', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }


    function openpopupAddAddress() {
        window.open('PopUp.aspx?PageId=ViewCustomerProofs &LinkId= AddAddress', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;
    }

</script>

<script type="text/javascript">

    function DisplayProofFormType() {

        document.getElementById("<%= trLinkToUpload.ClientID %>").style.visibility = 'visible';       

    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table width="100%">
    <tr>
        <td>
            <div class="divPageHeading">
                <table cellspacing="0" cellpadding="3" width="100%">
                    <tr>
                        <td align="left">
                            Request ISA
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td colspan="8">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                ISA Request Details
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtCusName" runat="server" CssClass="txtField">
            </asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblRequestNumber" runat="server" Text="Request No: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtRequestNumber" runat="server" CssClass="txtField">
            </asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblBranchCode" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtBranchCode" runat="server" CssClass="txtField">
            </asp:Label>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblISAGenerationStatus" runat="server" Text="ISA Generation Status: "
                CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtlblISAGenerationStatus" runat="server" CssClass="txtField">
            </asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblRequestdate" runat="server" Text="Request Date: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtRequestdate" runat="server" CssClass="txtField">
            </asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblRBLCode" runat="server" Text="RBL Code: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtRBLCode" runat="server" CssClass="txtField">
            </asp:Label>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                <div class="divSectionHeadingNumber">
                    1</div>
             &nbsp; Step
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
            <asp:Label ID="lblStage" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblStageName" runat="server" Text="ISA Request" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblResponsibility" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblResponsibilityName" runat="server" Text="BM" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblHeaderStatusStage1" runat="server" CssClass="txtField"></asp:Label>
            <%--<asp:DropDownList ID="ddlStatus" runat="server" CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select">
                </asp:ListItem>
                <asp:ListItem Text="Pending" Value="Pending">
                </asp:ListItem>
                <asp:ListItem Text="Done" Value="Done">
                </asp:ListItem>
            </asp:DropDownList>--%>
        </td>
        <td align="right">
            <asp:Label ID="lblClosingDate" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtClosingDate" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblCustomerCategory" runat="server" Text="Customer Category:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" colspan="7">
            <asp:DropDownList ID="ddlCustomerCategory" runat="server" onchange="GetVerificationTypeThroughCustCategory(this.options[this.selectedIndex].value);"
                CssClass="cmbField">
                <asp:ListItem Text="Select" Value="Select">
                </asp:ListItem>
                <asp:ListItem Text="RBL" Value="RBL">
                </asp:ListItem>
                <asp:ListItem Text="Non RBL" Value="NRBL">
                </asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlCustomerCategory" runat="server" CssClass="revPCG"
                ErrorMessage="Please Select Customer Category" Display="Dynamic" ValidationGroup="vgbtnSubmit"
                ControlToValidate="ddlCustomerCategory" InitialValue="Select">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trSelectNewOrExistingCustomer" runat="server">
        <td align="right">
            <asp:Label ID="lblCustomerNeworExisting" runat="server" Text="Customer:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" colspan="7">
            <asp:RadioButton ID="rdbNewCustomer" runat="server" GroupName="CustomerType" Text="New"
                Checked="true" Class="FieldName" onClick="DisplayCustomerSearch()" />
            <asp:RadioButton ID="rdbExistingCustomer" runat="server" GroupName="CustomerType"
                Text="Existing" Class="FieldName" onClick="DisplayCustomerSearch()" />
        </td>
    </tr>
    <tr id="trCustomerSearch" runat="server">
        <td align="right">
            <asp:Label ID="lblCustomerSearch" runat="server" Text="Search Customer:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="txtField" AutoComplete="Off"
                AutoPostBack="True"></asp:TextBox>
            <cc1:TextBoxWatermarkExtender ID="txtCustomer_water" TargetControlID="txtCustomerName"
                WatermarkText="Enter few chars of Customer" runat="server" EnableViewState="false">
            </cc1:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomerName_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomerName" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="False" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="True" OnClientItemSelected="GetCustomerId" DelimiterCharacters=""
                Enabled="True" />
               <%-- <br />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCustomerName"
                ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server"
                CssClass="revPCG" ValidationGroup="vgbtnSubmit"></asp:RequiredFieldValidator>--%>
        </td>
        <td align="right">
            <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="txtField"></asp:Label>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr id="trCustomerNameDetails" runat="server">
        <td align="right">
            <asp:Label ID="lblCustomerNameEntry" runat="server" Text="Customer Name:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtCustomerNameEntry" runat="server" CssClass="txtField"></asp:TextBox>
        </td>
        <td align="right">
            <asp:Label ID="lblChooseBr" runat="server" CssClass="FieldName" Text="Branch: "></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlBMBranch" runat="server" CssClass="cmbField">
            </asp:DropDownList>
        </td>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPanNum" runat="server" Text="Pan Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtPanNum" runat="server" CssClass="txtField" onblur="return chkPanExists()"></asp:TextBox>
            <span id="spnLoginStatus"></span>
            <br />
            <asp:CustomValidator ID="cvPanOrName" runat="server" ValidationGroup="vgbtnSubmit" Display="Dynamic"
                ClientValidationFunction="chkPanorCustomerNameRequired" CssClass="revPCG" ErrorMessage="CustomValidator">Either Name or Pan Number is must</asp:CustomValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblEmailID" runat="server" Text="Email Id:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtField"></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator ID="revValidEmailID" ControlToValidate="txtEmailID"
                ValidationGroup="vgbtnSubmit" ErrorMessage="Please enter a valid Email ID" Display="Dynamic"
                runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                CssClass="revPCG"></asp:RegularExpressionValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblMobileNum" runat="server" Text="Mobile Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtMobileNum" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
            <%--<br />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtMobileNum"
                ValidationGroup="vgbtnSubmit" ErrorMessage="Enter a valid 10 Digit Mobile No."
                Display="Dynamic" runat="server" ValidationExpression="^([1-9]{1})([0-9]{9})$"
                CssClass="revPCG"></asp:RegularExpressionValidator>--%>
        </td>
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblGenerateReqstNum" runat="server" Text="Request Number:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtGenerateReqstNum" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="rfvtxtGenerateReqstNum" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Generate Request No." Display="Dynamic" 
                ControlToValidate="txtGenerateReqstNum" ValidationGroup="vgBtnSubmitQueue"> 
            </asp:RequiredFieldValidator>
        </td>
        <td align="left" colspan="2">
            <asp:Button ID="btnGenerateReqstNum" runat="server" Text="Generate Req No." ValidationGroup="vgbtnSubmit" CssClass="PCGMediumButton"
                OnClick="btnGenerateReqstNum_OnClick"/>
        </td>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblPriority" runat="server" Text="Priority:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" colspan="7">
            <asp:DropDownList ID="ddlPriority" runat="server" CssClass="cmbField" onchange="GetVerificationType(this.options[this.selectedIndex].value);">
                <asp:ListItem Text="Select" Value="Select">
                </asp:ListItem>
                <asp:ListItem Text="Urgent" Value="Urgent">
                </asp:ListItem>
                <asp:ListItem Text="Normal" Value="Normal">
                </asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="reqValddlPriority" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Priority Type" Display="Dynamic" 
                ControlToValidate="ddlPriority" InitialValue="Select" ValidationGroup="vgBtnSubmitQueue">
            </asp:RequiredFieldValidator>
        </td>
    </tr>
    <%--    <tr>
        <td align="right" colspan="3">
            <asp:Label ID="lblVerificationType" runat="server" Text="verification Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left" colspan="6">
               <asp:DropDownList ID="ddlVerificationType" runat="server" CssClass="cmbField" onchange="GetVerificationType(this.options[this.selectedIndex].value);">
                <asp:ListItem Text="Select" Value="Select">
                </asp:ListItem>
                <asp:ListItem Text="Offline" Value="Offline">
                </asp:ListItem>
                <asp:ListItem Text="Online" Value="Online">
                </asp:ListItem>
            </asp:DropDownList>
        
        </td>
    </tr>--%>
    <%-- <tr id="trDocsType" runat="server">
        <td align="right">
            <asp:Label ID="lblDocumentType" runat="server" Text="Document Type:" CssClass="FieldName"></asp:Label>
        </td>
        <td colspan="7">
            <asp:RadioButton ID="rdbProofType" runat="server" GroupName="DocsType" Text="Proof"
                Class="FieldName" onClick="DisplayProofFormType()" />
            <asp:RadioButton ID="rdbFormType" runat="server" GroupName="DocsType" Text="Form"
                Class="FieldName" onClick="DisplayProofFormType()" />
        </td>
    </tr>--%>
    <tr id="trLinkToUpload" runat="server">
        <td>
        </td>
        <td id="tdISALink" align="center" runat="server">
            <asp:LinkButton ID="lbtnUploadISAForm" runat="server" Font-Size="X-Small" CausesValidation="False"
                Text="Upload ISA Form" OnClientClick="return openpopupAddISA()"></asp:LinkButton>
        </td>
        <td id="tdPanLink" align="left" runat="server">
            <asp:LinkButton ID="lbtnUploadPanProof" runat="server" Font-Size="X-Small" CausesValidation="False"
                Text="Upload PAN Proof" OnClientClick="return openpopupAddPan()"></asp:LinkButton>
        </td>
        <td id="tdAddressLink" align="left" runat="server">
            <asp:LinkButton ID="lbtnUploadAddressProof" runat="server" Font-Size="X-Small" CausesValidation="False"
                Text="Upload Address Proof" OnClientClick="return openpopupAddAddress()"></asp:LinkButton>
        </td>
        <td colspan="4">
        </td>
    </tr>
    <tr>
       <td align="right">
            <asp:Label ID="lblStatusStage1" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlStatusStage1" runat="server" CssClass="cmbField">                
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="rfvddlStatusStage1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Status" Display="Dynamic" 
                ControlToValidate="ddlStatusStage1" InitialValue="Select" ValidationGroup="vgBtnSubmitQueue">
            </asp:RequiredFieldValidator>
        </td>
             <td align="right">
            <asp:Label ID="lblReasonStage1" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlReasonStage1" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <%--
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Reason" Display="Dynamic" 
                ControlToValidate="ddlReasonStage2" InitialValue="Select" ValidationGroup="vgBtnSubmitQueue">
            </asp:RequiredFieldValidator>--%>
        </td>
        <td align="left">
            <asp:Button ID="btnSubmitAddStage1" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitQueue"
                CausesValidation="true" CssClass="PCGButton" 
                onclick="btnSubmitAddStage1_Click" />
        </td>
        <td align="left">
            <asp:Button ID="btnSubmitAddMoreStage1" runat="server" Text="Submit & Add More" 
                CssClass="PCGMediumButton" ValidationGroup="vgBtnSubmitQueue" 
                onclick="btnSubmitAddMoreStage1_Click" />
        </td>
        
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                &nbsp;&nbsp;Step
                <div class="divSectionHeadingNumber">
                    2</div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
            <asp:Label ID="lblStage2" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtStage2" runat="server" Text="Verify Forms and Proofs" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblResponsibility2" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtResponsibility2" runat="server" Text="Ops-Date Entry" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblStatus2" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtStatus2" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblClosingDate2" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtClosingDate2" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
            <asp:Label ID="lblViewFormsandProofs" runat="server" Text="View Forms and Proofs:"
                CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:LinkButton ID="lnkBtnViewFormsandProofs" runat="server" CausesValidation="False"
                Font-Size="X-Small" Text="View Forms & Proofs" OnClientClick="return openpopupViewFormsAndProofs()"></asp:LinkButton>
        </td>
        <td align="right">
            <asp:Label ID="lblStatusStage2" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlStatusStage2" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Status" Display="Dynamic" 
                ControlToValidate="ddlStatusStage2" InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
            </asp:RequiredFieldValidator>
            
        </td>
        <td align="right">
            <asp:Label ID="lblReasonStage2" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlReasonStage2" runat="server" CssClass="cmbField">
            </asp:DropDownList>
           <%-- 
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Reason" Display="Dynamic" 
                ControlToValidate="ddlReasonStage2" InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
            </asp:RequiredFieldValidator>--%>
        </td>
      
    </tr>
    
      <tr>
      <td colspan="3"></td>
        <td align="left">
            <asp:Button ID="btnSubmitStage2" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage2"
                CausesValidation="true" CssClass="PCGButton" 
                onclick="btnSubmitStage2_Click" />
        </td>
        <td colspan="2">
        
        </td>
        
          <td align="right">
            <asp:Label ID="lblCommentsStage2" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtComments" runat="server" CssClass="txtField" TextMode="MultiLine"
                Width="130px" Height="50px"></asp:TextBox>
        </td>
        </tr>
    
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                &nbsp;&nbsp;Step
                <div class="divSectionHeadingNumber">
                    3</div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
            <asp:Label ID="lblStage3" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtStage3" runat="server" Text="Create Customer Profile-ISA Account" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblResponsibility3" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtResponsibility3" runat="server" Text="Ops-Date Entry" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblStatus3" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtStatus3" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblClosingDate3" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtClosingDate3" runat="server" CssClass="txtField"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
            <asp:Label ID="Label9" runat="server" Text="View Forms and Proofs:"
                CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:LinkButton ID="lnkbtnAddEditCustomerProfile" runat="server" 
                Font-Size="X-Small" Text="Add/Edit Customer Profile" ></asp:LinkButton>
        </td>
        <td align="right">
            <asp:Label ID="lblStatusStage3" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlStatusStage3" runat="server" CssClass="cmbField">
            </asp:DropDownList>
             <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Status" Display="Dynamic" 
                ControlToValidate="ddlStatusStage2" InitialValue="Select" ValidationGroup="vgBtnSubmitStage3">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblStatusReason3" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlStatusReason3" runat="server" CssClass="cmbField">               
            </asp:DropDownList>
        </td>
      
    </tr>
    </tr>
    
      <tr>
      <td colspan="3"></td>
        <td align="left">
            <asp:Button ID="btnSubmitStage3" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage3"
                CausesValidation="true" CssClass="PCGButton" 
                onclick="btnSubmitStage3_Click" />
        </td>
        <td colspan="2"></td>
          <td align="right">
            <asp:Label ID="lblAddComments3" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtAddComments3" runat="server" CssClass="txtField" TextMode="MultiLine"
                Width="130px" Height="50px"></asp:TextBox>
        </td>
        </tr>
    
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <td colspan="8">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                &nbsp;&nbsp;Step
                <div class="divSectionHeadingNumber">
                 4</div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
            <asp:Label ID="lblStage4" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtStage4" runat="server" Text="Approve Customer/ISA Account" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblResponsibility4" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtResponsibility4" runat="server" Text="Ops-Date Authoriser" CssClass="txtField"></asp:Label>
        </td>
        <td align="right">
            <asp:Label ID="lblHeaderStatusStage4" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtStatusStage4" runat="server" CssClass="FieldName"></asp:Label>
        </td>
        
        <td align="right">
            <asp:Label ID="lblClosingDateStage4" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="txtClosingDateStage4" runat="server" CssClass="txtField"></asp:Label>
        </td>
        
    </tr>
    
    <tr>
        <td colspan="8">
        </td>
    </tr>
    <tr>
        <%-- <td>
            <asp:Label ID="lblStep1" runat="server" Text="Step 1" CssClass="FieldName"></asp:Label>
        </td>--%>
        <td align="right">
           <%-- <asp:Label ID="lbl" runat="server" Text="View Forms and Proofs:"
                CssClass="FieldName"></asp:Label>--%>
        </td>
        <td align="left">
         <%--   <asp:LinkButton ID="LinkButton1" runat="server" 
                Font-Size="X-Small" Text="Add/Edit Customer Profile" ></asp:LinkButton>
--%>       
 </td>
        <td align="right">
            <asp:Label ID="lblStatusStage4" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlStatusStage4" runat="server" CssClass="cmbField">
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                ErrorMessage="Please Select Status" Display="Dynamic" 
                ControlToValidate="ddlStatusStage2" InitialValue="Select" ValidationGroup="vgBtnSubmitStage4">
            </asp:RequiredFieldValidator>
        </td>
        <td align="right">
            <asp:Label ID="lblReasonStage4" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:DropDownList ID="ddlReasonStage4" runat="server" CssClass="cmbField">               
            </asp:DropDownList>
        </td>
       
    </tr>
    
    <tr>
    <td colspan="3"></td>
     <td align="left">
            <asp:Button ID="btnSubmitStage4" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage4"
                CausesValidation="true" CssClass="PCGButton" 
                onclick="btnSubmitStage4_Click" />
        </td>
          <td colspan="2"></td>
           <td align="right">
            <asp:Label ID="lblAddcommentsStage4" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
        </td>
        <td align="left">
            <asp:TextBox ID="txtAddcommentsStage4" runat="server" CssClass="txtField" TextMode="MultiLine"
                Width="130px" Height="50px"></asp:TextBox>
        </td>
    </tr>
</table>
</ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
<asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged1" />
<asp:HiddenField ID="hdnCustomerId" runat="server" />
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="hdnAdviserID" runat="server" />

