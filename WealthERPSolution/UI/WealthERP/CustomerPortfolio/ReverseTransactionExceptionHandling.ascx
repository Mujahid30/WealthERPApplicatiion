<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReverseTransactionExceptionHandling.ascx.cs" Inherits="WealthERP.CustomerPortfolio.ReverseTransactionExceptionHandling" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:ScriptManager ID="scptMgr" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>
<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script type="text/javascript" language="javascript">
function GetCustomerId(source, eventArgs) {

        document.getElementById("<%= txtCustomerId.ClientID %>").value = eventArgs.get_value();

        return false;
    };
  
</script>
<table>
<tr><td colspan="4"><asp:Label ID="lblMFTransactionRejectionHandling" Text="Reverse Transaction Exception Handling" CssClass="HeaderTextSmall" runat="server"></asp:Label></td></tr>
<tr><td colspan="2"><asp:RadioButton ID="rbtnAll" runat="server" CssClass="Field" 
        Text="All" GroupName="CGroup" Checked="true" AutoPostBack="true" 
        oncheckedchanged="rbtnAll_CheckedChanged" /></td>
<td colspan="2"><asp:RadioButton ID="rbtnCustomer" runat="server" CssClass="Field" Text="Customer" GroupName="CGroup" AutoPostBack="true" oncheckedchanged="rbtnAll_CheckedChanged"/></td>
</tr>

</table>
<table id="tblCustomerSearch" runat="server" visible="false">
    <tr>
        <td id="tdGroupHead" runat="server">
            <asp:Label ID="lblGroupHead" runat="server" CssClass="FieldName" Text="Customer :"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:HiddenField ID="txtCustomerId" runat="server" OnValueChanged="txtCustomerId_ValueChanged" />
                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="txtField" AutoComplete="Off"
                                        AutoPostBack="true"></asp:TextBox><cc1:TextBoxWatermarkExtender ID="txtCustomer_TextBoxWatermarkExtender"
                                            runat="server" TargetControlID="txtCustomer" WatermarkText="Type the Customer Name">
                                        </cc1:TextBoxWatermarkExtender>
                                    <ajaxToolkit:AutoCompleteExtender ID="txtCustomer_autoCompleteExtender" runat="server"
                                        TargetControlID="txtCustomer" ServiceMethod="GetAdviserCustomerName" ServicePath="~/CustomerPortfolio/AutoComplete.asmx"
                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="100"
                                        CompletionListCssClass="AutoCompleteExtender_CompletionList" CompletionListItemCssClass="AutoCompleteExtender_CompletionListItem"
                                        CompletionListHighlightedItemCssClass="AutoCompleteExtender_HighlightedItem"
                                        UseContextKey="true" OnClientItemSelected="GetCustomerId" />
                                    <span id="Span1" class="spnRequiredField">*<br />
                                    </span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtCustomer"
                                        ErrorMessage="Please Enter Customer Name" Display="Dynamic" runat="server" CssClass="rfvPCG"
                                        ValidationGroup="btnSubmit">
                                    </asp:RequiredFieldValidator><span style='font-size: 8px; font-weight: normal' class='FieldName'>Enter
                                        few characters of customer name.</span>
        </td>
        <td id="tdPortfolios" visible="false" runat="server">
        <asp:Label ID="lblPortfolios" runat="server" CssClass="FieldName" Text="Select a Portfolio:"></asp:Label>
        <asp:DropDownList ID="ddlPortfolios" runat="server" CssClass="Field" 
                onselectedindexchanged="ddlPortfolios_SelectedIndexChanged" AutoPostBack="true">
        
        </asp:DropDownList>
        </td>
    </tr>
</table>
<table width="100%">
<tr>
<td colspan="2">

</td>
</tr>
<tr>
<td>
<asp:Label  ID="lblRejectedTransactions" Text="" runat="server" CssClass="HeaderTextSmaller"></asp:Label>
</td>

</tr>
<tr>
<td id="tdRejectedTransactions" style="border-style:solid; border-width:thin; border-color:Maroon" runat="server" visible="false">

<asp:Panel ID="pnlRejectedTransactions" runat="server" Height="200px" 
                       Width="100%" ScrollBars="Vertical" Visible="false" HorizontalAlign="Left">
<asp:GridView ID="gvRejectedTransactions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyNames="MFTransId" ForeColor="#333333" Width="624px" Height="78px" 
                        Font-Size="Small" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" CssClass="GridViewStyle">
                        <FooterStyle Font-Bold="True" ForeColor="White" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                        <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                        <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                        <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>                      

                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                   <asp:RadioButton GroupName="Rejected" ID="rdRejectId" runat="server" OnCheckedChanged="rdRejectId_CheckedChanged" AutoPostBack="true"   />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:BoundField DataField="CustomerName" HeaderText="Name"  />
                            <asp:BoundField DataField="Scheme" HeaderText="Scheme"  />
                            <asp:BoundField DataField="Folio" HeaderText="Folio"  />
                            <asp:BoundField DataField="Date" HeaderText="Date"  />
                            <asp:BoundField DataField="TransactioType" HeaderText="TransactionType" />
                            <asp:BoundField DataField="Units" HeaderText="Units"  />
                            <asp:BoundField DataField="Price" HeaderText="Price"  />
                            <asp:BoundField DataField="Amount" HeaderText="Amount"  />
                        </Columns>
                    </asp:GridView>
                    </asp:Panel>
                    
</td>



</tr>
<tr><td>
<asp:Label  ID="lblOriginalTransactions" Text="" runat="server" CssClass="HeaderTextSmaller"></asp:Label>
</td></tr>
<tr><td id="tdOriginalTransactions" style="border-style:solid; border-width:thin; border-color:Maroon"  runat="server" visible="false">


<asp:Panel ID="pnlOriginalTransactions" runat="server" Height="200px" 
                       Width="100%" ScrollBars="Vertical" Visible="false" HorizontalAlign="Left">

<asp:GridView ID="gvOriginalTransactions" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyNames="MFTransId" ForeColor="#333333" Width="624px" Height="78px"
                        Font-Size="Small" BackImageUrl="~/CSS/Images/HeaderGlassBlack.jpg" CssClass="GridViewStyle">
                        <FooterStyle Font-Bold="True" ForeColor="White" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                        <SelectedRowStyle Font-Bold="True" CssClass="SelectedRowStyle" />
                        <HeaderStyle Font-Bold="True" Font-Size="Small" ForeColor="White" CssClass="HeaderStyle" />
                        <EditRowStyle Font-Size="X-Small" CssClass="EditRowStyle" />
                        <AlternatingRowStyle BorderStyle="None" CssClass="AltRowStyle" />
                        <RowStyle CssClass="RowStyle" />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                   <asp:RadioButton GroupName="Original" ID="rdOriginalId" runat="server" OnCheckedChanged="rdOriginalId_CheckedChanged" AutoPostBack="true" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Scheme" HeaderText="Scheme"  />
                            <asp:BoundField DataField="Folio" HeaderText="Folio"  />
                            <asp:BoundField DataField="Date" HeaderText="Date"  />
                            <asp:BoundField DataField="TransactioType" HeaderText="Transaction Type" />
                            <asp:BoundField DataField="Units" HeaderText="Units"  />
                            <asp:BoundField DataField="Price" HeaderText="Price"  />
                            <asp:BoundField DataField="Amount" HeaderText="Amount"  />
                        </Columns>
                    </asp:GridView>
                   </asp:Panel>

                    
</td></tr>
<tr><td colspan="2" align="left"><asp:Button ID="btnMap" CssClass="PCGLongButton" 
        Text="Map Transaction" runat="server" Visible="false" onclick="btnMap_Click"/></td></tr>
</table>