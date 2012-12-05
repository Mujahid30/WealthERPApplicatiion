<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerISARequest.ascx.cs"
    Inherits="WealthERP.Customer.CustomerISARequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<meta http-equiv="content-type" content="text/html; charset=ISO-8859-1" />

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<asp:ScriptManager ID="scrptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<style type="text/css">
    /*CollapsiblePanel*/</style>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
</script>

<script type="text/javascript">

    function ResetPagetoFirstLoadState() {
        document.getElementById("<%= ddlCustomerCategory.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;
        //        document.getElementById("<%= ddlPriority.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlReasonStage1.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlStatusStage1.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
        document.getElementById("<%= rdbNewCustomer.ClientID %>").checked = true;



        document.getElementById("<%= txtPanNum.ClientID %>").value = "";
        document.getElementById("<%= txtGenerateReqstNum.ClientID %>").value = "";
        document.getElementById("<%= ddlBMBranch.ClientID %>").selectedIndex = "0";
        document.getElementById("<%= txtMobileNum.ClientID %>").value = "";
        document.getElementById("<%= txtEmailID.ClientID %>").value = "";
        document.getElementById("<%= txtCustomerNameEntry.ClientID %>").value = "";
        //        document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
        document.getElementById("<%= ddlStatusStage1.ClientID %>").selectedIndex = "0";
    }
</script>

<script type="text/javascript">
    function HideAndShowBasedOnRole(val) {

        document.getElementById("<%= ddlBackToStepStage3.ClientID %>").disabled = true;
        document.getElementById("<%= ddlBackToStepStage2.ClientID %>").disabled = true;
        document.getElementById("<%= ddlBackToStepStage4.ClientID %>").disabled = true;

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


            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;
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


            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = false;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = false;

            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;

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


            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;

        }
        if (val == "CUSCR") {

            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;

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

            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;
        }
        if (val == "VERIfY") {


            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;

            document.getElementById("<%= txtComments.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddComments3.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage3.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = false;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;

            document.getElementById("<%= txtAddcommentsStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage2.ClientID %>").disabled = true;
        }
        if (val == "VERIfYDisable") {


            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;

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
            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;
        }


        if (val == "ISAGE") {

            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;

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

            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = false;
        }

        if (val == "ISAGEDisable") {

            document.getElementById("<%= ddlCustomerCategory.ClientID %>").disabled = true;
            document.getElementById("<%= ddlBMBranch.ClientID %>").disabled = true;
            document.getElementById("<%= txtCustomerNameEntry.ClientID %>").disabled = true;
            document.getElementById("<%= txtPanNum.ClientID %>").disabled = true;
            document.getElementById("<%= txtEmailID.ClientID %>").disabled = true;
            document.getElementById("<%= txtMobileNum.ClientID %>").disabled = true;
            document.getElementById("<%= btnGenerateReqstNum.ClientID %>").disabled = true;
            //            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddStage1.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitAddMoreStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;

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


            document.getElementById("<%= btnSubmitStage3.ClientID %>").disabled = true;
            document.getElementById("<%= btnSubmitStage4.ClientID %>").disabled = true;
        }
    }
</script>

<script type="text/javascript">
    function GetVerificationType(val) {
        //in val u get dropdown list selected value
        var verificationType = val;
        var cusType = document.getElementById("<%= ddlCustomerCategory.ClientID %>").selectedIndex;

        if (verificationType == "Normal" || verificationType == "Select") {

        }
        else if (verificationType == "Urgent" && cusType == "1") {




        }
        else if (verificationType == "Urgent" && cusType == "2") {



        }
    }
    function GetVerificationTypeThroughCustCategory(val) {
        //in val u get dropdown list selected value
        var verificationType = val;
        var priorityType = document.getElementById("<%= ddlPriority.ClientID %>").selectedIndex;


        if (priorityType == "0" || priorityType == "2") {

        }
        else if (verificationType == "RBL" && priorityType == "1") {


        }
        else if (verificationType == "NRBL" && priorityType == "1") {


        }
    }

    function HideAndShowStepBack2(val) {
        //in val u get dropdown list selected value
        var ddlIndex = document.getElementById("<%= ddlStatusStage2.ClientID %>").selectedIndex;
        if (val == "PD") {
            document.getElementById("<%= ddlBackToStepStage2.ClientID %>").disabled = false;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= lblReasonStage2.ClientID %>").style.visibility = 'visible';
        }
        else if (val == "DO") {
        document.getElementById("<%= ddlBackToStepStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= lblReasonStage2.ClientID %>").style.visibility = 'collapse';
        }
        else {
            document.getElementById("<%= ddlBackToStepStage2.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage2.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= lblReasonStage2.ClientID %>").style.visibility = 'visible';
        }
    }
    function HideAndShowStepBack3(val) {
        //in val u get dropdown list selected value

        var ddlIndex = document.getElementById("<%= ddlStatusStage3.ClientID %>").selectedIndex;
        if (val == "PD") {
            document.getElementById("<%= ddlBackToStepStage3.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= lblStatusReason3.ClientID %>").style.visibility = 'visible';
        }
        else if (val == "DO") {
            document.getElementById("<%= ddlBackToStepStage3.ClientID %>").disabled = true;
            document.getElementById("<%= lblStatusReason3.ClientID %>").style.visibility = 'collapse';
        }
        else {
            document.getElementById("<%= ddlBackToStepStage3.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusReason3.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= lblStatusReason3.ClientID %>").style.visibility = 'visible';
        }
    }

    function HideAndShowStepBack4(val) {
        //in val u get dropdown list selected value
        var ddlIndex = document.getElementById("<%= ddlStatusStage4.ClientID %>").selectedIndex;

        if (val == "PD") {
            document.getElementById("<%= ddlBackToStepStage4.ClientID %>").disabled = false;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= lblReasonStage4.ClientID %>").style.visibility = 'visible';
        }
        else if (val == "DO") {
            document.getElementById("<%= ddlBackToStepStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= lblReasonStage4.ClientID %>").style.visibility = 'collapse';
        }
        else {
            document.getElementById("<%= ddlBackToStepStage4.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage4.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= lblReasonStage4.ClientID %>").style.visibility = 'visible';
        }
    }
</script>

<script type="text/javascript">
    function DisableStatusReason(val) {
        var status = val;
        if (status == "DO") {
            document.getElementById("<%= lblReasonStage1.ClientID %>").style.visibility = 'collapse';
            document.getElementById("<%= ddlReasonStage1.ClientID %>").style.visibility = 'collapse';
        }
        else {
            document.getElementById("<%= lblReasonStage1.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= ddlReasonStage1.ClientID %>").style.visibility = 'visible';

        }

    }   

</script>

<script type="text/javascript">
    function enableDisablePriority(val) {

        if (val == "true") {
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = true;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = true;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = true;

        }
        if (val == "false") {
            document.getElementById("<%= ddlPriority.ClientID %>").disabled = false;
            document.getElementById("<%= ddlStatusStage1.ClientID %>").disabled = false;
            document.getElementById("<%= ddlReasonStage1.ClientID %>").disabled = false;
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
            else {
                args.IsValid = true;
            }

        }
    } 
</script>

<script type="text/javascript">

    function DisplayCustomerSearch(val) {
        //in val u get dropdown list selected value

        if (val == 'EXISTING') {
            alert('EXISTING');
            document.getElementById("<%= trExistingCustomer.ClientID %>").style.visibility = 'visible';
            document.getElementById("<%= trNewCustomer.ClientID %>").style.visibility = 'collapse';

            document.getElementById("<%= txtCustomerName.ClientID %>").value = null;
            document.getElementById("<%= lblGetBranch.ClientID %>").innerHTML = null;
            document.getElementById("<%= lblGetRM.ClientID %>").innerHTML = null;
            document.getElementById("<%= txtPanNum.ClientID %>").value = null;
            document.getElementById("<%= txtMobileNum.ClientID %>").value = null;
            document.getElementById("<%= txtEmailID.ClientID %>").value = null;


        }
        else if (val == 'NEW') {
            alert('NEW');
            document.getElementById("<%= trExistingCustomer.ClientID %>").style.display = 'collapse';
            document.getElementById("<%= trNewCustomer.ClientID %>").style.display = 'visible';

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



    function openpopupAddAddress() {

        window.open('PopUp.aspx?PageId=ViewCustomerProofs &LinkId= AddAddress', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;

    }

    function openpopupViewFormsProof() {

        window.open('PopUp.aspx?PageId=ViewCustomerProofs &LinkId= ViewFormsAndProofs', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
        return false;

    }

    function openpopupAdd_EditCustomerDetails() {
        if (document.getElementById("<%= txtGenerateReqstNum.ClientID %>").value != "") {
            window.open('PopUp.aspx?PageId=EditCustomerIndividualProfile &LinkId=AddEditProfile', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
            return false;
        }
        else {
            alert("Please generate your request first");
            return false;
        }

       
        
    }

    

</script>


<script type="text/javascript">
 function openpopup_View_CustomerDetails() {
            if (document.getElementById("<%= txtGenerateReqstNum.ClientID %>").value != "") {
                window.open('PopUp.aspx?PageId=EditCustomerIndividualProfile &LinkId=ViewEditProfile', 'mywindow', 'width=750,height=500,scrollbars=yes,location=no')
                return false;
            }

            else {
                alert("Please generate your request first");
                return false;
            }
        }
</script>
<script type="text/javascript">

    function pageLoad() {
        function StepEventFireCollapseExpand(stepName) {

            if (stepName == 'one') {
                alert(stepName);
                $(document).ready(function() {
                    $("#imgCEStepOne").attr("src", "../Images/Section-Collapse.png");

                    $(".StepTwoContentTable").slideToggle(50);
                    $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");

                    $(".StepThreeContentTable").slideToggle(50);
                    $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");

                    $(".StepFourContentTable").slideToggle(50);
                    $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
                });

            } else if (stepName == 'two') {
                $(document).ready(function() {

                    $(".StepOneContentTable").slideToggle(50);
                    $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");

                    $(".StepThreeContentTable").slideToggle(50);
                    $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");

                    $(".StepFourContentTable").slideToggle(50);
                    $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
                });

            } else if (stepName == 'three') {
                $(document).ready(function() {

                    $(".StepTwoContentTable").slideToggle(50);
                    $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");

                    $(".StepOneContentTable").slideToggle(50);
                    $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");

                    $(".StepFourContentTable").slideToggle(50);
                    $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
                });
            } else if (stepName == 'four') {
                $(document).ready(function() {

                    $(".StepTwoContentTable").slideToggle(50);
                    $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");

                    $(".StepThreeContentTable").slideToggle(50);
                    $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");

                    $(".StepOneContentTable").slideToggle(50);
                    $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");
                });
            }
        }
    }
</script>

<script type="text/javascript">

    $(document).ready(function() {
        //    alert($("#imgCEStepOne").attr('src'))
        $(".StepOneContentTable").hide();
        $("#imgCEStepOne").click(function() {
            $(".StepOneContentTable").slideToggle(50);
            var src = $(this).attr('src');
            if (src == '../Images/Section-Expand.png') {
                $("#imgCEStepOne").attr("src", "../Images/Section-Collapse.png");

                if ($("#imgCEStepTwo").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepTwoContentTable").slideToggle(50);
                    $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepThree").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepThreeContentTable").slideToggle(50);
                    $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepFour").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepFourContentTable").slideToggle(50);
                    $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
                }

            }
            else if (src == '../Images/Section-Collapse.png') {
                $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");
            }
        });

    });


    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

    $(document).ready(function() {
        $(".StepTwoContentTable").hide();
        $("#imgCEStepTwo").click(function() {
            $(".StepTwoContentTable").slideToggle(50);
            var src = $(this).attr('src');
            if (src == '../Images/Section-Expand.png') {
                $("#imgCEStepTwo").attr("src", "../Images/Section-Collapse.png");
                if ($("#imgCEStepOne").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepOneContentTable").slideToggle(50);
                    $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepThree").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepThreeContentTable").slideToggle(50);
                    $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepFour").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepFourContentTable").slideToggle(50);
                    $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
                }
            }
            else if (src == '../Images/Section-Collapse.png')
                $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");
        });

    });

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

    $(document).ready(function() {
        $(".StepThreeContentTable").hide();
        $("#imgCEStepThree").click(function() {
            $(".StepThreeContentTable").slideToggle(50);
            var src = $(this).attr('src');
            if (src == '../Images/Section-Expand.png') {
                $("#imgCEStepThree").attr("src", "../Images/Section-Collapse.png");

                if ($("#imgCEStepTwo").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepTwoContentTable").slideToggle(50);
                    $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepOne").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepOneContentTable").slideToggle(50);
                    $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepFour").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepFourContentTable").slideToggle(50);
                    $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
                }
            }
            else if (src == '../Images/Section-Collapse.png')
                $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");
        });

    });

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

    $(document).ready(function() {
        $(".StepFourContentTable").hide();
        $("#imgCEStepFour").click(function() {
            $(".StepFourContentTable").slideToggle(50);
            var src = $(this).attr('src');
            if (src == '../Images/Section-Expand.png') {
                $("#imgCEStepFour").attr("src", "../Images/Section-Collapse.png");

                if ($("#imgCEStepTwo").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepTwoContentTable").slideToggle(50);
                    $("#imgCEStepTwo").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepThree").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepThreeContentTable").slideToggle(50);
                    $("#imgCEStepThree").attr("src", "../Images/Section-Expand.png");
                }
                if ($("#imgCEStepOne").attr('src') == '../Images/Section-Collapse.png') {
                    $(".StepOneContentTable").slideToggle(50);
                    $("#imgCEStepOne").attr("src", "../Images/Section-Expand.png");
                }
            }
            else if (src == '../Images/Section-Collapse.png')
                $("#imgCEStepFour").attr("src", "../Images/Section-Expand.png");
        });

    });

    function keyPress(sender, args) {
        if (args.keyCode == 13) {
            return false;
        }
    }

    /*---SECTION FOR POSTBACK HANDEL--*/



    
</script>

<style type="text/css">
    .table
    {
        border: 1px solid orange;
    }
    select
    {
        width: 98% !important;
    }
    .leftLabel
    {
        width: 10%;
        text-align: right;
    }
    .rightData
    {
        width: 15%;
        text-align: left;
    }
    .rightDataTwoColumn
    {
        width: 25%;
        text-align: left;
    }
    .rightDataFourColumn
    {
        width: 50%;
        text-align: left;
    }
    .rightDataThreeColumn
    {
        width: 40%;
        text-align: left;
    }
    .tdSectionHeading
    {
        padding-bottom: 6px;
        padding-top: 6px;
        width: 100%;
    }
    .divSectionHeading table td span
    {
        padding-bottom: 5px !important;
    }
    .fltlft
    {
        float: left;
        padding-left: 3px;
        width: 20%;
    }
    .divCollapseImage
    {
        float: left;
        padding-left: 5px;
        width: 2%;
        float: right;
        text-align: right;
        cursor: pointer;
        cursor: hand;
    }
    .imgCollapse
    {
        background: Url(../Images/Section-Expand.png);
        cursor: pointer;
        cursor: hand;
    }
    .imgExpand
    {
        background: Url(../Images/Section-Collapse.png) no-repeat left top;
        cursor: pointer;
        cursor: hand;
    }
    .fltlftStep
    {
        float: left;
    }
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable, .StepFourContentTable
    {
        width: 100%;
    }
    .SectionBody
    {
        width: 100%;
    }
    .collapse
    {
        text-align: right;
    }
    .divStepStatus
    {
        float: left;
        padding-left: 2px;
        padding-right: 5px;
    }
</style>
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
            <%--***********************************************REQUEST STATUS********************************--%>
            <tr id="trRequestHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading">
                        ISA Request Details
                    </div>
                </td>
            </tr>
            <tr id="trRequestContent" runat="server">
                <td>
                    <table width="100%">
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtCusName" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblRequestNumber" runat="server" Text="Request No.: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRequestNumber" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblBranchCode" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtBranchCode" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="txtRequestTimeText" runat="server" CssClass="FieldName" Text="Request Time: ">
                                </asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRequestTimeValue" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblISAGenerationStatus" runat="server" Text="ISA Status: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtlblISAGenerationStatus" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblRequestdate" runat="server" Text="Request Date: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRequestdate" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblRBLCode" runat="server" Text="RBL Code: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRBLCode" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="txtRequestedByText" runat="server" CssClass="FieldName" Text="Requested By: ">
                                </asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRequestedByValue" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--***********************************************BM STEP ONE***************************************--%>
            <tr id="trStepOneHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading">
                        <div class="divStepStatus">
                            <asp:Image ID="imgStepOneStatus" ImageUrl="" alt="" runat="server" />
                            &nbsp;
                        </div>
                        <div class="divSectionHeadingNumber fltlftStep">
                            1
                        </div>
                        <%-- <div class="fltlftStep">
                            &nbsp; Step &nbsp;
                        </div>--%>
                        <div class="fltlft">
                            &nbsp;
                            <asp:Label ID="lblStage" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="lblStageName" runat="server" Text="ISA Request" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblResponsibility" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="lblResponsibilityName" runat="server" Text="BM" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblStatus" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="lblHeaderStatusStage1" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblClosingDate" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtClosingDate" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="divCollapseImage">
                            <img id="imgCEStepOne" src="../Images/Section-Expand.png" alt="Collapse/Expand" class="imgCollapse" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trStepOneContent" runat="server">
                <td class="SectionBody">
                    <table class="StepOneContentTable">
                        <tr>
                            <td class="leftLabel" id="tdCustomerSelection1" runat="server">
                                <asp:Label ID="lblCustomerNeworExisting" runat="server" Text="Relationship:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData" id="tdCustomerSelection2" runat="server">
                                <asp:RadioButton ID="rdbNewCustomer" runat="server" GroupName="CustomerType" Text="New"
                                    Checked="true" Class="FieldName" OnCheckedChanged="rdbNewCustomer_CheckedChanged"
                                    AutoPostBack="true" />
                                <asp:RadioButton ID="rdbExistingCustomer" runat="server" GroupName="CustomerType"
                                    Text="Existing" Class="FieldName" OnCheckedChanged="rdbExistingCustomer_CheckedChanged"
                                    AutoPostBack="true" />
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblCustomerCategory" runat="server" Text="Category:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlCustomerCategory" runat="server" onchange="GetVerificationTypeThroughCustCategory(this.options[this.selectedIndex].value);"
                                    CssClass="cmbField">
                                  
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddlCustomerCategory" runat="server" CssClass="revPCG"
                                    ErrorMessage="Please Select Customer Category" Display="Dynamic" ValidationGroup="vgbtnSubmit"
                                    ControlToValidate="ddlCustomerCategory" InitialValue="Select">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td colspan="4" class="rightDataFourColumn">
                                <div class="ISAAccountMsg" align="center" id="divSuccessMsg" runat="server" visible="false">
                                    Request Created Successfully, Please enter the "Request No." on the ISA Form
                                </div>
                            </td>
                        </tr>
                        <tr id="trNewCustomer" runat="server">
                            <td class="leftLabel">
                                <asp:Label ID="lblCustomerNameEntry" runat="server" Text="Customer Name:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtCustomerNameEntry" runat="server" CssClass="txtField"></asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblChooseBr" runat="server" CssClass="FieldName" Text="Branch: "></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlBMBranch" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td colspan="4" class="rightDataFourColumn">
                            </td>
                        </tr>
                        <tr id="trExistingCustomer" runat="server">
                            <td class="leftLabel">
                                <asp:Label ID="lblCustomerSearch" runat="server" Text="Search Customer:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
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
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblRM" runat="server" Text="RM: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="lblGetRM" runat="server" Text="" CssClass="txtField"></asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblBranch" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="lblGetBranch" runat="server" Text="" CssClass="txtField"></asp:Label>
                            </td>
                            <td colspan="2" class="rightDataTwoColumn">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblPanNum" runat="server" Text="PAN:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtPanNum" runat="server" MaxLength="10" CssClass="txtField" onblur="return chkPanExists()"></asp:TextBox>
                                <span id="spnLoginStatus"></span>
                                <br />
                                <asp:CustomValidator ID="cvPanOrName" runat="server" ValidationGroup="vgbtnSubmit"
                                    Display="Dynamic" ClientValidationFunction="chkPanorCustomerNameRequired" CssClass="revPCG"
                                    ErrorMessage="CustomValidator">Either Name or Pan Number is must</asp:CustomValidator>
                                <asp:RegularExpressionValidator ID="revPANNum" ControlToValidate="txtPanNum" ValidationGroup="vgbtnSubmit"
                                    ErrorMessage="Not A Valid PAN" Display="Dynamic" runat="server" ValidationExpression="^[0-9a-zA-Z]+$"
                                    CssClass="revPCG"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblEmailID" runat="server" Text="Email:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtField"></asp:TextBox>
                                <br />
                                <asp:RegularExpressionValidator ID="revValidEmailID" ControlToValidate="txtEmailID"
                                    ValidationGroup="vgbtnSubmit" ErrorMessage="Please enter a valid Email ID" Display="Dynamic"
                                    runat="server" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    CssClass="revPCG"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblMobileNum" runat="server" Text="Mobile:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtMobileNum" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                            </td>
                            <td colspan="2" class="rightDataTwoColumn">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblPriority" runat="server" Text="Priority:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlPriority" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="Urgent" Value="Urgent">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Normal" Value="Normal" Selected="True">
                                    </asp:ListItem>
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="reqValddlPriority" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Priority Type" Display="Dynamic" ControlToValidate="ddlPriority"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitQueue">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                            </td>
                            <td class="rightData">
                                <asp:Button ID="btnGenerateReqstNum" runat="server" Text="Generate Req No." ValidationGroup="vgbtnSubmit"
                                    CssClass="PCGMediumButton" OnClick="btnGenerateReqstNum_OnClick" />
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblGenerateReqstNum" runat="server" Text="Request No.:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtGenerateReqstNum" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvtxtGenerateReqstNum" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Generate Request No." Display="Dynamic" ControlToValidate="txtGenerateReqstNum"
                                    ValidationGroup="vgBtnSubmitQueue"> 
                                </asp:RequiredFieldValidator>
                            </td>
                            <td colspan="4" class="rightDataTwoColumn">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <div style="height: 4px; border-color: Gray; float: left; width: 100%">
                                </div>
                            </td>
                        </tr>
                        <tr id="trFormUpload" runat="server">
                            <td class="leftLabel">
                                <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Upload/View Document:"></asp:Label>
                            </td>
                            <td colspan="7" id="tdpriorityUrgent" runat="server">
                                <div runat="server" id="tdUploadSection" style="float: left">
                                    &nbsp;
                                    <asp:LinkButton ID="lbtnUploadISAForm" runat="server" Font-Size="X-Small" CausesValidation="False"
                                        Text="Upload ISA Form" OnClientClick="return openpopupAddISA()"></asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lbtnUploadPanProof" runat="server" Font-Size="X-Small" CausesValidation="False"
                                        Text="Upload PAN Proof" OnClientClick="return openpopupAddPan()"></asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="lbtnUploadAddressProof" runat="server" Font-Size="X-Small" CausesValidation="False"
                                        Text="Upload Address Proof" OnClientClick="return openpopupAddAddress()"></asp:LinkButton>
                                </div>
                                <div style="float: left">
                                    &nbsp;
                                    <asp:LinkButton ID="lnkViewFormsAndProofBM" runat="server" Font-Size="X-Small" CausesValidation="False"
                                        Text="View Uploaded Forms/Proof" OnClientClick="return openpopupViewFormsProof()"
                                        Visible="false"></asp:LinkButton>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblStatusStage1" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlStatusStage1" runat="server" CssClass="cmbField" onchange="DisableStatusReason(this.options[this.selectedIndex].value);">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddlStatusStage1" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlStatusStage1"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitQueue">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblReasonStage1" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlReasonStage1" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitAddStage1" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitQueue"
                                    CausesValidation="true" CssClass="PCGButton" OnClick="btnSubmitAddStage1_Click" />
                            </td>
                            <td class="rightData">
                                <asp:Button ID="btnSubmitAddMoreStage1" runat="server" Text="Submit & Add More" CssClass="PCGMediumButton"
                                    ValidationGroup="vgBtnSubmitQueue" OnClick="btnSubmitAddMoreStage1_Click" />
                            </td>
                            <td colspan="2" class="rightDataTwoColumn">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--***********************************************OPS MAKER STEP TWO***************************************--%>
            <tr id="trStepTwoHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        <div class="divStepStatus">
                           
                            <asp:Image ID="imgStepTwoStatus" ImageUrl="" alt="" runat="server" />
                             &nbsp;
                        </div>
                        <div class="divSectionHeadingNumber fltlftStep">
                            2
                        </div>
                        <%-- <div class="fltlftStep">
                            &nbsp; Step &nbsp;
                        </div>--%>
                        <div class="fltlft">
                          &nbsp;
                            <asp:Label ID="lblStage2" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtStage2" runat="server" Text="Verify Forms/Proofs" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblResponsibility2" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtResponsibility2" runat="server" Text="Ops-Date Entry" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblStatus2" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtStatus2" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblClosingDate2" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtClosingDate2" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="divCollapseImage">
                            <img id="imgCEStepTwo" src="../Images/Section-Expand.png" alt="Collapse/Expand" class="imgCollapse" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trStepTwoContent" runat="server">
                <td class="SectionBody">
                    <table class="StepTwoContentTable">
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblStatusStage2" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlStatusStage2" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusStage2_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlStatusStage2"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblReasonStage2" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlReasonStage2" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblBackToStepStage2" runat="server" Text="Send back to:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlBackToStepStage2" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="Select" Value="Select">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Step 1" Value="ISARQ">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" class="rightDataTwoColumn">
                                <asp:LinkButton ID="lnkViewFormsAndProofOPS" runat="server" Font-Size="X-Small" CausesValidation="False"
                                    Text="View Forms/Proof" OnClientClick="return openpopupViewFormsProof()"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblCommentsStage2" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtComments" runat="server" CssClass="txtField" TextMode="MultiLine"
                                    Height="40px"></asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitStage2" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage2"
                                    CausesValidation="true" CssClass="PCGButton" OnClick="btnSubmitStage2_Click" />
                            </td>
                            <td class="rightData">
                            </td>
                            <td colspan="4" class="rightDataFourColumn">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--***********************************************OPS CHECKER STEP THREE***************************************--%>
            <tr id="trStepThreeHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading">
                        <div class="divStepStatus">
                           
                            <asp:Image ID="imgStepThreeStatus" ImageUrl="" alt="" runat="server" />
                             &nbsp;
                        </div>
                        <div class="divSectionHeadingNumber fltlftStep">
                            3
                        </div>
                        <%--<div class="fltlftStep">
                            &nbsp; Step &nbsp;
                        </div>--%>
                        <div class="fltlft">
                          &nbsp;
                            <asp:Label ID="lblStage3" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtStage3" runat="server" Text="Profile-ISA Account" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblResponsibility3" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtResponsibility3" runat="server" Text="Ops-Date Entry" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblStatus3" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtStatus3" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblClosingDate3" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtClosingDate3" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="divCollapseImage">
                            <img id="imgCEStepThree" src="../Images/Section-Expand.png" alt="Collapse/Expand"
                                class="imgCollapse" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trStepThreeContent" runat="server">
                <td class="SectionBody">
                    <table class="StepThreeContentTable">
                        <tr>
                            <td class="leftLabel">
                                <%--<asp:Label ID="lblVerifyForms" runat="server" Text="Forms/Proofs:" CssClass="FieldName"></asp:Label>--%>
                            </td>
                            <td class="rightData">
                                <asp:LinkButton ID="lnkbtnAddEditCustomerProfile" runat="server" Font-Size="X-Small"
                                    Text="Add/Edit Customer Profile" OnClientClick="return openpopupAdd_EditCustomerDetails()"></asp:LinkButton>
                                    
                                    <asp:LinkButton ID="lnkbtnViewCustomerProfile" runat="server" Font-Size="X-Small"
                                    Text="View Customer Profile" OnClientClick="return openpopup_View_CustomerDetails()"></asp:LinkButton>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblStatusStage3" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlStatusStage3" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusStage3_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlStatusStage3"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitStage3">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblStatusReason3" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlStatusReason3" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblBackToStepStage3" runat="server" Text="Send back to:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlBackToStepStage3" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="Select" Value="Select">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Step 1" Value="ISARQ">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Step 2" Value="VERIfY">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblAddComments3" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtAddComments3" runat="server" CssClass="txtField" TextMode="MultiLine"
                                    Height="40px"></asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitStage3" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage3"
                                    CausesValidation="true" CssClass="PCGButton" OnClick="btnSubmitStage3_Click" />
                            </td>
                            <td class="rightData">
                            </td>
                            <td colspan="4" class="rightDataFourColumn">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--***********************************************STEP FOUR***************************************--%>
            <tr id="trStepFourHeading" runat="server">
                <td class="tdSectionHeading">
                    <div class="divSectionHeading">
                      <div class="divStepStatus">
                           
                            <asp:Image ID="imgStepFourStatus" ImageUrl="" alt="" runat="server" />
                             &nbsp;
                        </div>
                        <div class="divSectionHeadingNumber fltlftStep">
                            4
                        </div>
                        <%--<div class="fltlftStep">
                            &nbsp; Step &nbsp;
                        </div>--%>
                      
                        <div class="fltlft">
                          &nbsp;
                            <asp:Label ID="lblStage4" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtStage4" runat="server" Text="Approve ISA Account" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblResponsibility4" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtResponsibility4" runat="server" Text="Ops-Date Authoriser" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblHeaderStatusStage4" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtStatusStage4" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="lblClosingDateStage4" runat="server" Text="Closing Date:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtClosingDateStage4" runat="server" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="divCollapseImage">
                            <img id="imgCEStepFour" src="../Images/Section-Expand.png" alt="Collapse/Expand"
                                class="imgCollapse" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr id="trStepFourContent" runat="server">
                <td class="SectionBody">
                    <table class="StepFourContentTable">
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblStatusStage4" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlStatusStage4" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlStatusStage4_OnSelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlStatusStage4"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitStage4">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblReasonStage4" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlReasonStage4" runat="server" CssClass="cmbField">
                                </asp:DropDownList>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblBackToStepStage4" runat="server" Text="Send back to:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlBackToStepStage4" runat="server" CssClass="cmbField">
                                    <asp:ListItem Text="Select" Value="Select">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Step 1" Value="ISARQ">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Step 2" Value="VERIfY">
                                    </asp:ListItem>
                                    <asp:ListItem Text="Step 3" Value="CUSCR">
                                    </asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td colspan="2" class="rightDataTwoColumn">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblAddcommentsStage4" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtAddcommentsStage4" runat="server" CssClass="txtField" TextMode="MultiLine"
                                    Height="40px"></asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitStage4" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage4"
                                    CausesValidation="true" CssClass="PCGButton" OnClick="btnSubmitStage4_Click" />
                            </td>
                            <td class="rightData">
                            </td>
                            <td colspan="4" class="rightDataFourColumn">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--***********************************************STEP FIVE***************************************--%>
            <tr id="trStepFiveHeading" runat="server">
                <td style="padding-bottom: 6px; padding-top: 6px;">
                    <div class="divSectionHeading" style="vertical-align: text-bottom">
                        &nbsp;&nbsp;Step &nbsp;
                        <div class="divSectionHeadingNumber">
                            5</div>
                    </div>
                </td>
            </tr>
            <tr id="trStepFiveContent" runat="server">
                <td>
                    <table>
                        <tr>
                            <td colspan="8" align="center">
                                <asp:Label ID="Label1" runat="server" Text="Coming Soon..." class="HeaderTextBig"></asp:Label>
                            </td>
                        </tr>
                    </table>
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
