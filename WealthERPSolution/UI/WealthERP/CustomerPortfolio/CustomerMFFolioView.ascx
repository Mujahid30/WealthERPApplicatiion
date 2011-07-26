<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerMFFolioView.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerMFFolioView" %>
<%@ Register Src="~/General/Pager.ascx" TagPrefix="Pager" TagName="Pager" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="~/CustomerPortfolio/AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<style>
.yellow-box {
    background-color:#FFFFE5;
    border:1px solid #F5E082;
    padding:10px;
}

</style>

<script type="text/javascript" language="javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= hdnCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }
</script>

<script type="text/javascript">
    function GetCustomerId(source, eventArgs) {
        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();
        return false;
    }

    function CheckSelection() {

        var form = document.forms[0];

        var count = 0
        for (var i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == 'checkbox') {
                if (form.elements[i].checked == true) {
                    count++;
                }
            }
        }
        if (count == 0) {
            alert("Please select atleast one folio to transfer.")
            return false;
        }
        if (document.getElementById("<%= txtCustomerId.ClientID %>").value == "") {
            alert("Please select a customer.");
            return false;
        }
        return true;
    }
</script>

<script type="text/javascript">

    function ShowAlertToDelete() {

        var bool = window.confirm('Are you sure you want to delete this MF Folio?');

        if (bool) {
            document.getElementById("ctrl_CustomerMFFolioView_hdnStatusValue").value = 1;
            document.getElementById("ctrl_CustomerMFFolioView_btnFolioAssociation").click();

            return false;
        }
        else {
            document.getElementById("ctrl_CustomerMFFolioView_hdnStatusValue").value = 0;
            document.getElementById("ctrl_CustomerMFFolioView_btnFolioAssociation").click();
            return true;
        }
    }

</script>

<table class="TableBackground" style="width: 100%">

<tr>
    <td class="HeaderCell">
        <asp:Label ID="lblHeader" runat="server" CssClass="HeaderTextBig" Text="MF Folio View"></asp:Label>
        <hr />
    </td>
</tr>
</table>

<table>
    <tr>        
        <td align="right">
            <asp:Label ID="Label1" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
            </td>
        <td>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>        
    </tr>   
</table>
<table class="TableBackground" style="width: 100%">
   
     <tr>
        <td align="right">
            <asp:Label ID="lblCurrentPage" class="Field" runat="server"></asp:Label>
            <asp:Label ID="lblTotalRows" class="Field" runat="server"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td>
            <asp:GridView ID="gvMFFolio" runat="server" AutoGenerateColumns="False" CellPadding="4"
                CssClass="GridViewStyle" DataKeyNames="FolioId"  HorizontalAlign="Center"
                ShowFooter="True">
                <FooterStyle CssClass="FooterStyle" />
                <RowStyle CssClass="RowStyle" />
                <EditRowStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="EditRowStyle" />
                <SelectedRowStyle CssClass="SelectedRowStyle" />
                <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                <HeaderStyle CssClass="HeaderStyle" />
                <AlternatingRowStyle CssClass="AltRowStyle" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkBox" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="GridViewCmbField"
                                OnSelectedIndexChanged="ddlAction_SelectedIndexChanged">
                                <asp:ListItem Text="Select" />
                                <asp:ListItem Text="View" />
                                <asp:ListItem Text="Edit" />
                                <asp:ListItem Text="Delete" />
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Folio No" HeaderText="Folio No" />
                    <asp:BoundField DataField="AMC Name" HeaderText="AMC Name" />
                    <asp:BoundField DataField="Name" HeaderText="Name" />
                    <asp:BoundField DataField="Mode Of Holding" HeaderText="Mode Of Holding" />
                    <asp:BoundField DataField="A/C Opening Date" HeaderText="A/C Opening Date" />
                </Columns>
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
            <div id="DivPager" runat="server" style="display: none">
                <table style="width: 100%">
                    <tr>
                        <td>
                            <Pager:Pager ID="mypager" runat="server"></Pager:Pager>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <%--<asp:Button ID="btnTransferFolio" runat="server" Text="Transfer Folio" CssClass="PCGMediumButton"
                OnClick="btnTransferFolio_Click" />--%>
        </td>
    </tr>
    <tr>
    <td>
        <%--<asp:Button ID="btnMoveFolio" runat="server" Text="Move Folio to Another Portfolio" CssClass="PCGLongButton"
        OnClick="btnMoveFolio_Click"/>--%>
    </td>
    </tr>
</table>

<table class="TableBackground" width="100%">
    <tr id="trErrorMsg" runat="server">
        <td align="center">
            <asp:Label ID="lblMessage" runat="server" CssClass="Error" Text="No Records Found!"></asp:Label>
        </td>
    </tr>
</table>
<table class="TableBackground" visible="false" style="width: 100%">
     <tr id="trSelectAction" runat="server">        
        <td style="width:150px" align="right">
            <asp:Label ID="lblSelectAction" runat="server" CssClass="FieldName" Text="Select Action:"></asp:Label>
            </td>
        <td style="width:280px">
            <asp:DropDownList ID="ddlAction" runat="server" CssClass="cmbLongField" 
                AutoPostBack="true" onselectedindexchanged="ddlAction_SelectedIndexChanged1">
                <asp:ListItem Value="0">Select Action</asp:ListItem>
                <asp:ListItem Value="TF">Transfer Folio</asp:ListItem>
                <asp:ListItem Value="MFtoAP">Move Folio to another Portfolio</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td style="width:150px">  
        </td>
    </tr>
</table>

<table border="0" id="tblMoveFolio" runat="server" visible="false" width="100%"
    style="border: solid 2px #8BA0BD">
   <tr id="trPickPortfolio" runat="server">        
        <td align="right" style="width:150px">
            <asp:Label ID="lblPickPortfolio" Text="Pick a Portfolio:" CssClass="FieldName" runat="server"></asp:Label>
        </td>
        <td style="width:280px">
            <asp:DropDownList ID="ddlPickPortfolio" runat="server" CssClass="cmbField">
            </asp:DropDownList>                
            <span id="Span2" class="spnRequiredField">*
            <asp:RequiredFieldValidator ID="rfvddlPickPortfolio" ControlToValidate="ddlPickPortfolio" ErrorMessage="Please pick a portfolio"
        Display="Dynamic" runat="server" CssClass="rfvPCG" ValidationGroup="btnSubmitMoveFolio">
        </asp:RequiredFieldValidator>
            </span>                
        </td>
        <td style="width:150px">
            &nbsp;</td>                
    </tr> 
    <tr>
        
        <td align="right">
        
            <asp:Button ID="btnSubmitMoveFolio" runat="server" Text="Submit" 
                OnClientClick="return CheckSelection()" CssClass="PCGButton" 
                onclick="btnSubmitMoveFolio_Click" />
        
        </td>
    </tr>
    <tr>
        <td colspan="2">
        <div runat="server" id="div2">
            <asp:Label ID="Label4" runat="server" Text="" CssClass="SuccessMsg"></asp:Label>
        </div>
        
        </td>
    </tr>
</table>

<table border="0" id="tblTransferFolio" runat="server" visible="false" width="100%"
    style="border: solid 2px #8BA0BD">
    <tr>
        <td colspan="2">
            <h3 class="HeaderTextBig">
                Transfer folio</h3>
        </td>
    </tr>
    <tr>       
        <td style="width:150px" align="right">
            <asp:Label ID="Label2" runat="server" Text="Customer Name :" CssClass="FieldName"></asp:Label>
        </td>
        <td style="width:280px">
            <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
            <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off" AutoPostBack="true">
            </asp:TextBox>
            <ajaxToolkit:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender"
                runat="server" TargetControlID="txtCustomer" WatermarkText="Type the Customer Name">
                </ajaxToolkit:TextBoxWatermarkExtender>
            <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                TargetControlID="txtCustomer" ServiceMethod="GetCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                UseContextKey="true" OnClientItemSelected="GetCustomerId" />
            <span id="Span1" class="spnRequiredField">*</span>
            <span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter few characters of customer name.</span>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                ValidationGroup="btnSubmit">
            </asp:RequiredFieldValidator>
            
        </td>
        <td  style="width:150px">
            
            </td>
    </tr>
    <tr id="trReassignBranch" runat="server">        
        <td align="right" style="width:150px">      
            <asp:Label ID="Label3" Text="Transfer To:" CssClass="FieldName" runat="server"></asp:Label> 
        </td>
        <td style="width:280px">
            <asp:DropDownList ID="ddlAdvisorBranchList" runat="server" CssClass="cmbField" 
                 AutoPostBack="true">
            </asp:DropDownList>
            <%--<span id="spanAdvisorBranch" class="spnRequiredField" runat="server">*</span>--%>
        </td>
        <td style="width:150px"></td>
        
    </tr>
    <tr id="trCustomerDetails" runat="server" visible="false">
        <td style="width:150px" align="right">
            <asp:Label ID="Label11" runat="server" CssClass="FieldName" Text="PAN :"></asp:Label>            
        </td>
        <td style="width:280px">
            <table>
            <tr>
                <td>
                    <asp:TextBox ID="txtPanParent" runat="server" CssClass="txtField" BackColor="Transparent"
                    BorderStyle="None"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblAddress" runat="server" CssClass="FieldName" Text="Address:"></asp:Label> 
                </td>
                <td>
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="txtField" BackColor="Transparent"
                    BorderStyle="None"></asp:TextBox>
                </td>
            </tr>
            </table>
        </td>        
        <td style="width:150px" align="right">                       
        </td>
        <td  style="width:150px" align="left">            
        </td>
    </tr>
    <tr>       
        <td align="right">
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
                OnClientClick="return CheckSelection()" CssClass="PCGButton" />
        </td>
        <td>
        </td>
        <td></td>
    </tr>
    <tr>
        <td colspan="2">
        <div runat="server" id="divMessage">
            <asp:Label ID="lblTransferMsg" runat="server" Text="" CssClass="SuccessMsg"></asp:Label>
        </div>
        
        </td>
    </tr>
</table>
<table class="TableBackgrounds" width="100%">
    <tr id="trFolioStatus" runat="server">
        <td align="center">
            <div id="msgFolioStatus" runat="server" class="success-msg" align="center">
                Folio Moved Successfully
            </div>
        </td>
    </tr>
</table>
<div id="Div1" runat="server" style="display: none">
    <table style="width: 100%">
        <tr align="center">
            <td>
                <Pager:Pager ID="Pager1" runat="server"></Pager:Pager>
            </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="hdnSort" runat="server" Value="InstrumentCategory ASC" />
<asp:HiddenField ID="hdnRecordCount" runat="server" />
<asp:HiddenField ID="hdnCurrentPage" runat="server" />
<asp:HiddenField ID="hdnCustomerId" runat="server"/> 
<asp:HiddenField ID="hdnStatusValue" runat="server" />
<asp:Button ID="btnFolioAssociation" runat="server" BorderStyle="None" 
    BackColor="Transparent" onclick="btnFolioAssociation_Click" 
    style="height: 22px" />
