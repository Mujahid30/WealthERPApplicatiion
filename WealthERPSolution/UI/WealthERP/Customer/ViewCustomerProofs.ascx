<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewCustomerProofs.ascx.cs"
    Inherits="WealthERP.Customer.ViewCustomerProofs" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>

<script src="../Scripts/jquery.easing.1.3.js" type="text/javascript"></script>

<script src="../Scripts/jquery.quickZoom.1.0.js" type="text/javascript"></script>

<link href="../CSS/quickZoom.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>

<script type="text/javascript" src="jquery.magnifier.js">

</script>



<script type="text/javascript">

    function HideValidations() {
        document.getElementById("<%= cmpProofType.ClientID %>").style.visibility = 'hidden';
        document.getElementById("<%= cmpProof.ClientID %>").style.visibility = 'hidden';
        document.getElementById("<%= cmpProofCopyType.ClientID %>").style.visibility = 'hidden';
    }

    function ShowValidations() {
        document.getElementById("<%= cmpProofType.ClientID %>").style.visibility = 'visible';
        document.getElementById("<%= cmpProof.ClientID %>").style.visibility = 'visible';
        document.getElementById("<%= cmpProofCopyType.ClientID %>").style.visibility = 'visible';
    }

    function validateRadUpload1(source, arguments) {
        arguments.IsValid = $find('<%= radUploadProof.ClientID %>').validateExtensions();

        var btnText = document.getElementById('ctrl_ViewCustomerProofs_btnSubmit');

        if (btnText.value != 'Update') {
            var upload = $find("<%= radUploadProof.ClientID %>");
            var inputs = upload.getFileInputs();
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].value == '') {
                    arguments.IsValid = false;
                    return false;
                }
            }
        }


    }
</script>

<script type="text/javascript">
    $(document).ready(function() {
        $('.quickZoom li').quickZoom({
            zoom: 3,
            speedIn: 500,
            speedOut: 400,
            easeIn: 'easeOutBack',
            titleInSpeed: 100
        });
        $('.customClass li').quickZoom({
            zoom: 1,
            speedIn: 500,
            speedOut: 200,
            easeIn: 'easeOutBack',
            titleInSpeed: 140,
            sqThumb: true
        });
    });
     </script>

<style type="text/css">
    .customClass
    {
        width: 340px;
        margin: 0 auto;
    }
    .customClass li
    {
        list-style: none;
        margin: 0;
        padding: 0;
        float: left;
        margin: 10px;
        position: relative;
    }
    .customClass li, .customClass li img
    {
        width: 110px;
        height: 110px;
        position: relative;
    }
    h1
    {
        text-align: center;
        font-family: helvetica;
    }
</style>

<script language="javascript" type="text/javascript">

    function showDeleteConfirmAlert() {

        var bool = window.confirm('Are you sure you want to delete this proof ?');

        if (bool) {
            document.getElementById("ctrl_ViewCustomerProofs_hdnMsgValue").value = 1;
            document.getElementById("ctrl_ViewCustomerProofs_hdnbtnDelete").click();

            return false;
        }
        else {
            document.getElementById("ctrl_ViewCustomerProofs_hdnMsgValue").value = 0;
            document.getElementById("ctrl_ViewCustomerProofs_hdnbtnDelete").click();

            return true;
        }
    }
    
</script>

<telerik:RadScriptManager ID="RadScriptManager1" runat="server">
</telerik:RadScriptManager>
<telerik:RadTabStrip ID="radPOCProof" runat="server" EnableTheming="True" Skin="Telerik"
    MultiPageID="multiPageView" EnableEmbeddedSkins="false">
    <tabs>
        <telerik:RadTab runat="server" Text="Proof Upload" onclick="ShowValidations();" Value="Proof_Upload"
            TabIndex="0">
        </telerik:RadTab>
        <telerik:RadTab runat="server" Text="Proof View" onclick="HideValidations();" Value="Proof_View"
            TabIndex="1">
        </telerik:RadTab>
    </tabs>
</telerik:RadTabStrip>
<telerik:RadMultiPage ID="multiPageView" runat="server" SelectedIndex="0">
    <telerik:RadPageView ID="pageProofAdd" runat="server">
        <asp:Panel ID="pnlProofAdd" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <label class="HeaderTextBig">
                            Customer proof Upload</label>
                        <hr />
                    </td>
                </tr>
            </table>
            <table id="tblproofAdd" width="100%">
                <tr>
                    <td align="right">
                        <label class="cmbField">
                            Proof Type:
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProofType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProofType_SelectedIndexChanged"
                            CssClass="cmbField">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpProofType" ValidationGroup="VaultValidations" runat="server"
                            ControlToValidate="ddlProofType" ErrorMessage="Please select a Proof type" Operator="NotEqual"
                            ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="cmbField">
                            Proof:
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProof" AutoPostBack="false" runat="server" CssClass="cmbField">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpProof" runat="server" ValidationGroup="VaultValidations"
                            ControlToValidate="ddlProof" ErrorMessage="Please select a Proof" Operator="NotEqual"
                            ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <label class="cmbField">
                            Proof Copy type:
                        </label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlProofCopyType" runat="server" AutoPostBack="false" CssClass="cmbField">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="cmpProofCopyType" runat="server" ValidationGroup="VaultValidations"
                            ControlToValidate="ddlProofCopyType" ErrorMessage="Please select a Proof copy type"
                            Operator="NotEqual" ValueToCompare="Select" CssClass="cvPCG" Display="Dynamic">
                        </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" style="vertical-align: middle">
                        <label class="cmbField">
                            Upload:
                        </label>
                    </td>
                    <td align="left" style="vertical-align: middle">
                        <span style="font-size: xx-small">(Allowed extensions are: .jpg,.jpeg,.bmp,.png,.pdf)</span>
                        <telerik:RadUpload ID="radUploadProof" runat="server" ControlObjectsVisibility="None"
                            AllowedFileExtensions=".jpg,.jpeg,.bmp,.png,.pdf" Skin="Telerik" EnableEmbeddedSkins="false">
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="Customvalidator1" ValidationGroup="VaultValidations" Font-Bold="true"
                            Font-Size="X-Small" ErrorMessage="Empty / Invalid File..!!!" ForeColor="Red"
                            runat="server" Display="Dynamic" ClientValidationFunction="validateRadUpload1"></asp:CustomValidator>
                        <asp:Label ID="lblFileUploaded" runat="server" CssClass="cmbField" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnSubmit" ValidationGroup="VaultValidations" Text="Submit" runat="server"
                            CssClass="PCGButton" OnClick="btnSubmit_Click" />
                    </td>
                    <td align="left">
                        <asp:Button ID="btnSubmitAdd" ValidationGroup="VaultValidations" Text="Submit & Add More"
                            runat="server" CssClass="PCGMediumButton" OnClick="btnSubmitAdd_Click" />
                        <asp:Button ID="btnDelete" runat="server" CssClass="PCGButton" Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                </tr>
                <%--<tr>
                    <td>
                    </td>
                    <td>
                        
                    </td>
                </tr>--%>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
    <telerik:RadPageView ID="pageProofView" runat="server">
        <asp:Panel ID="pnlUploadView" runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <label class="HeaderTextBig">
                            Customer proof</label>
                        <hr />
                    </td>
                </tr>
            </table>
            <table id="tblUploadView" width="100%">
                <tr>
                    <td>
                        <asp:Repeater ID="repProofImages" OnItemDataBound="repProofImages_ItemDataBound"
                            OnItemCommand="repProofImages_ItemCommand" runat="server">
                            <HeaderTemplate>
                                <table width="100%">
                                    <tr>
                                        <th align="center" style="text-decoration: underline" class="HeaderText">
                                            Proof type
                                        </th>
                                        <th align="center" style="text-decoration: underline" class="HeaderText">
                                            Proof
                                        </th>
                                        <th align="center" style="text-decoration: underline" class="HeaderText">
                                            Copy type
                                        </th>
                                       <%-- <th align="center" style="text-decoration: underline" class="HeaderText">
                                            Proof
                                        </th>--%>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lblProofType" runat="server" CssClass="cmbField" Text='<%# Eval("ProofType").ToString() %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblProof" runat="server" CssClass="cmbField" Text='<%# Eval("ProofName").ToString() %>'></asp:Label>
                                    </td>
                                    <td align="center">
                                        <asp:Label ID="lblProofCopy" runat="server" CssClass="cmbField" Text='<%# Eval("ProofCopyType").ToString() %>'></asp:Label>
                                    </td>
                                   <%-- <td align="center" style="border: 0; float: left" runat="server" id="tdProofImages">
                                        <%# LoadControls(Eval("ProofExtensions").ToString(), Eval("ProofImage").ToString(), Eval("ProofFileName").ToString())%>
                                    </td>
--%>                                    <td align="center">
                                        <asp:LinkButton ID="lnkMail" runat="server" CssClass="LinkButtons" CommandName="Send Mail"
                                            CommandArgument='<%# Eval("ProofUploadId") %>' Text="Send Email"></asp:LinkButton>
                                    </td>
                                    <td align="center">
                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" CommandName="Edit proof"
                                            CommandArgument='<%# Eval("ProofUploadId") %>' Text="Edit / Delete"></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="bntViewProofImage" runat="server" Text="View" CommandArgument='<%# Eval("ProofUploadId") %>'
                                         CssClass="LinkButtons" CommandName="View proof"/>
                                    </td>
                                    <td align="center" visible="false">
                                        <asp:LinkButton ID="lnkPrint" Visible="false" runat="server" CssClass="LinkButtons"
                                            CommandName="Print proof" CommandArgument='<%# Eval("ProofUploadId") %>' Text="Print"></asp:LinkButton>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <SeparatorTemplate>
                                <tr>
                                    <td colspan="7">
                                        <hr />
                                    </td>
                                </tr>
                            </SeparatorTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </telerik:RadPageView>
</telerik:RadMultiPage>
<div style="visibility: hidden">
    <asp:HiddenField ID="hdnMsgValue" runat="server" />
    <asp:Button ID="hdnbtnDelete" runat="server" BorderColor="Transparent" BackColor="Transparent"
        OnClick="hdnbtnDelete_Click" />
</div>
