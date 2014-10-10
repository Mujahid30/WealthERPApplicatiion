<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddAssociates.ascx.cs"
    Inherits="WealthERP.Associates.AddAssociates" %>
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


    /*---SECTION FOR POSTBACK HANDEL--*/



    
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
            url: "ControlHost.aspx/CheckPANNoAvailabilityForAssociates",
            data: "{ 'PanNumber': '" + $("#<%=txtPanNum.ClientID %>").val() + "','adviserId': '" + $("#<%=hdnAdviserID.ClientID %>").val() + "' }",
            error: function(xhr, status, error) {

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
    .rightDataThreeColumnFe
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
    .StepOneContentTable, .StepTwoContentTable, .StageRequestTable, .StepThreeContentTable
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
<table width="100%">
    <tr>
        <td colspan="3" style="width: 100%;">
            <div class="divPageHeading">
                <table cellspacing="0" width="100%">
                    <tr>
                        <td align="left">
                            Request Associates
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%">
            <tr id="trRequestHeading" runat="server">
                <td class="tdSectionHeading" colspan="8">
                    <div class="divSectionHeading">
                        Associate Request Details
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblHeaderInfo" runat="server">
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblAssoName" runat="server" Text="Name: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtAssoName" runat="server" CssClass="txtField">
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
                                <asp:Label ID="lblBMName" runat="server" Text="Branch: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtBMName" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblRMName" runat="server" CssClass="FieldName" Text="RM : ">
                                </asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRMName" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblFStatus" runat="server" Text="Status: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtFinalStatus" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblRequestdate" runat="server" Text="Request Date: " CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:Label ID="txtRequestdate" runat="server" CssClass="txtField">
                                </asp:Label>
                            </td>
                            <td class="leftLabel" colspan="4">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                </td>
            </tr>
            <%--***********************************************STEP ONE***************************************--%>
            <tr id="trStepOneHeading" runat="server">
                <td class="tdSectionHeading" colspan="8">
                    <div class="divSectionHeading">
                        <div class="divStepStatus">
                            <asp:Image ID="imgStepOneStatus" ImageUrl="" alt="" runat="server" />
                            &nbsp;
                        </div>
                        <div class="divSectionHeadingNumber fltlftStep">
                            1
                        </div>
                        <div class="fltlft">
                            &nbsp;
                            <asp:Label ID="lblStage" runat="server" Text="Stage:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="lblStageName" runat="server" Text="Associate Request" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <%--<asp:Label ID="lblResponsibility" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="lblResponsibilityName" runat="server" Text="BM;OPS" CssClass="txtField"></asp:Label>--%>
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
            <tr id="trBranchRM" runat="server">
                <td align="right" class="SectionBody">
                    <table class="StepOneContentTable">
                        <tr>
                            <td colspan="2">
                            </td>
                            <td colspan="4">
                                <div class="ISAAccountMsg" align="center" id="divStep1SuccMsg" runat="server" visible="false">
                                    Request Created Successfully. Please update the status.
                                </div>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblTitleList" runat="server" Text="Title:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlTitleList" runat="server" CssClass="cmbField" OnSelectedIndexChanged="ddlTitleList_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                                <span id="Span5" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Select Title"
                                    CssClass="rfvPCG" ControlToValidate="ddlTitleList" ValidationGroup="Submit"
                                    Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            
                             <td align="right">
                                <asp:Label ID="lblRM" runat="server" CssClass="FieldName" Text="Staff:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlRM" runat="server" CssClass="cmbField" AutoPostBack="true" OnSelectedIndexChanged="ddlRM_SelectedIndexChanged"
                                    Style="vertical-align: middle">
                                </asp:DropDownList>
                                <span id="Span1" class="spnRequiredField">*</span>
                                <br />
                                <asp:CompareValidator ID="cvRM" runat="server" ValidationGroup="Submit" ControlToValidate="ddlRM"
                                    ErrorMessage="Please select a RM" Operator="NotEqual" ValueToCompare="Select"
                                    CssClass="cvPCG" Display="Dynamic">
                                </asp:CompareValidator>
                            </td>
                            
                            <td class="leftLabel" id="tdCustomerSelection1" runat="server">
                                <asp:Label ID="lblBranch" runat="server" CssClass="FieldName" Text="Branch:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" Style="vertical-align: middle" AutoPostBack="false"
                                    CssClass="cmbField">
                                </asp:DropDownList>
                                <span id="Span2" class="spnRequiredField">*</span>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="Submit"
                                    ControlToValidate="ddlBranch" ErrorMessage="Please select a Branch" Operator="NotEqual"
                                    TextToCompare="Select" CssClass="cvPCG" Display="Dynamic">
                                </asp:CompareValidator>
                            </td>
                           
                            <td align="right">
                                <asp:Label ID="lblAssociateName" runat="server" CssClass="FieldName" Text="Name: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAssociateName" runat="server" CssClass="txtField"></asp:TextBox>
                                <span id="Span3" class="spnRequiredField">*</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtAssociateName"
                                    ErrorMessage="<br />Please Enter Associate Name" Display="Dynamic" runat="server"
                                    CssClass="rfvPCG" ValidationGroup="Submit"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblPanNum" runat="server" Text="PAN:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtPanNum" runat="server" MaxLength="10" CssClass="txtFieldUpper"
                                    onblur="return chkPanExists()"></asp:TextBox>
                                <span id="spnLoginStatus"></span>
                                <br />
                                <asp:RegularExpressionValidator ID="revPANNum" ControlToValidate="txtPanNum" ValidationGroup="Submit"
                                    ErrorMessage="Not A Valid PAN" Display="Dynamic" runat="server" ValidationExpression="^[0-9a-zA-Z]+$"
                                    CssClass="revPCG"></asp:RegularExpressionValidator>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblEmailId" runat="server" CssClass="FieldName" Text="Email Id: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="txtField"></asp:TextBox>
                                <span id="Span4" class="spnRequiredField">*</span>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtEmailId"
                                    ValidationGroup="Submit" ErrorMessage="Please enter an EmailId" Display="Dynamic"
                                    runat="server" CssClass="rfvPCG">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtEmailId"
                                    ErrorMessage="Please enter a valid EmailId" Display="Dynamic" runat="server"
                                    ValidationGroup="Submit" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    CssClass="revPCG"></asp:RegularExpressionValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblMobileNum" runat="server" Text="Mobile:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtMobileNum" runat="server" CssClass="txtField" MaxLength="10"></asp:TextBox>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Generate Request" CssClass="PCGLongButton"
                                    ValidationGroup="Submit" OnClick="btnSave_Click" />
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
                            <td class="rightDataFourColumn" colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblstatus1" runat="server" CssClass="FieldName" Text="Status:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlstatus1" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                                    CssClass="cmbField" OnSelectedIndexChanged="ddlstatus1_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddlStatusStage1" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlstatus1"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitQueue">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblReasonStage1" runat="server" Text="Reason:" CssClass="FieldName"
                                    Visible="false"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlReasonStage1" runat="server" CssClass="cmbField" Visible="false">
                                </asp:DropDownList>
                            </td>
                            <td colspan="4">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblCommentsstep1" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtCommentstep1" runat="server" TextMode="MultiLine" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitAddStage1" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitQueue"
                                    CausesValidation="true" CssClass="PCGButton" OnClick="btnSubmitAddStage1_Click"
                                    Visible="true" />
                            </td>
                            <td class="rightDataFourColumn" colspan="3">
                                <div class="ISAAccountMsg" align="center" id="divAssociateAdd" runat="server" visible="false">
                                    Please add associates details.
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--    <tr>
        <td colspan="6">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Step-2
            </div>
        </td>
    </tr>--%>
            <tr id="trStepTwoHeading" runat="server">
                <td class="tdSectionHeading" colspan="8">
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
                            <asp:Label ID="txtStage2" runat="server" Text="Profiling" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <%-- <asp:Label ID="lblResponsibility2" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtResponsibility2" runat="server" Text="Ops/BM" CssClass="txtField"></asp:Label>--%>
                        </div>
                        <div class="fltlft">
                            <asp:Label ID="Label1" runat="server" Text="Status:" CssClass="FieldName"></asp:Label>
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
            <tr>
                <td class="SectionBody">
                    <table class="StepTwoContentTable">
                        <tr>
                            <td class="leftLabel">
                                <td colspan="4">
                                    <asp:LinkButton runat="server" ID="lnlStep2" CssClass="LinkButtons" Text="Enter Associate Profile"
                                        OnClick="lnlStep2_Click" Enabled="false"></asp:LinkButton>
                                </td>
                                <td colspan="2">
                                </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <td colspan="4">
                                    <asp:LinkButton runat="server" ID="lnkAgentCode" CssClass="LinkButtons" Text="Generate Agent Code"
                                        OnClick="lnkAgentCode_Click" Enabled="false"></asp:LinkButton>
                                </td>
                                <td colspan="2">
                                </td>
                        </tr>
                        <%--<tr>
        <td class="leftLabel">
            <asp:Label ID="lblName" CssClass="FieldName" runat="server" Text="Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtName" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
        </td>
        <td class="leftLabel">
            <asp:Label ID="lblAgentCode" CssClass="FieldName" runat="server" Text="AgentCode:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:TextBox ID="txtAgentCode" runat="server" CssClass="txtField" Enabled="false"></asp:TextBox>
        </td>
        <td colspan="2">
        <asp:Button ID="btnSubmitAgentCode" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage3"
                CausesValidation="true" CssClass="PCGButton" 
                onclick="btnSubmitAgentCode_Click" />
        </td>
    </tr>--%>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblStatus2" runat="server" CssClass="FieldName" Text="Status:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlstatus2" runat="server" Style="vertical-align: middle" AutoPostBack="true"
                                    CssClass="cmbField" Enabled="false" OnSelectedIndexChanged="ddlstatus2_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlstatus2"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitStage2">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblReasonStage2" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlReasonStage2" runat="server" CssClass="cmbField" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblComments2" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtComments2" runat="server" TextMode="MultiLine" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitAddStage2" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage2"
                                    CausesValidation="true" CssClass="PCGButton" Visible="false" OnClick="btnSubmitAddStage2_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--    <tr>
        <td colspan="8">
            <div class="divSectionHeading" style="vertical-align: text-bottom">
                Step-3
            </div>
        </td>
    </tr>--%>
            <tr id="trStepThreeHeading" runat="server">
                <td class="tdSectionHeading" colspan="8">
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
                            <asp:Label ID="txtStage3" runat="server" Text="Verification" CssClass="txtField"></asp:Label>
                        </div>
                        <div class="fltlft">
                            <%--  <asp:Label ID="lblResponsibility3" runat="server" Text="Responsibility:" CssClass="FieldName"></asp:Label>
                            <asp:Label ID="txtResponsibility3" runat="server" Text="Ops" CssClass="txtField"></asp:Label>--%>
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
            <tr>
                <td colspan="8">
                </td>
            </tr>
            <tr>
                <td class="SectionBody">
                    <table class="StepThreeContentTable">
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblStepStatus3" runat="server" CssClass="FieldName" Text="Status:"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStepstatus3" runat="server" AutoPostBack="true" CssClass="cmbField"
                                    Enabled="false" OnSelectedIndexChanged="ddlStepstatus3_SelectedIndexChanged">
                                </asp:DropDownList>
                                <br />
                                <asp:RequiredFieldValidator ID="rfvddlStepStatus3" runat="server" CssClass="rfvPCG"
                                    ErrorMessage="Please Select Status" Display="Dynamic" ControlToValidate="ddlStepstatus3"
                                    InitialValue="Select" ValidationGroup="vgBtnSubmitStage3">
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="leftLabel">
                                <asp:Label ID="lblReasonStep3" runat="server" Text="Reason:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:DropDownList ID="ddlReasonStep3" runat="server" CssClass="cmbField" Enabled="false">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                                <%--<asp:CheckBox ID="chkMailSend" Checked="false" runat="server" Text="Send Login info?" AutoPostBack="true"
                                    CssClass="cmbField" Visible="false" />--%>
                                <br />
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="leftLabel">
                                <asp:Label ID="lblCommentStep3" runat="server" Text="Comments:" CssClass="FieldName"></asp:Label>
                            </td>
                            <td class="rightData">
                                <asp:TextBox ID="txtCommentStep3" runat="server" TextMode="MultiLine" CssClass="txtField">
                                </asp:TextBox>
                            </td>
                            <td class="leftLabel">
                                <asp:Button ID="btnSubmitStep3" runat="server" Text="Submit" ValidationGroup="vgBtnSubmitStage3"
                                    CausesValidation="true" CssClass="PCGButton" Visible="false" OnClick="btnSubmitStep3_Click1" />
                            </td>
                            <td colspan="5">
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
<asp:HiddenField ID="hidValidCheck" runat="server" EnableViewState="true" />
<asp:HiddenField ID="hdnAdviserID" runat="server" />
