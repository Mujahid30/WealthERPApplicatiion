<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomerEQAccountAdd.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.CustomerEQAccountAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
    
 <meta http-equiv="content-type" content="text/html; charset=ISO-8859-1">

<script src="../Scripts/jquery.js" type="text/javascript"></script>
 
 <style>
        body
        {
            background-color: #EBEFF9;
        }
        .error
        {
            color: Red;
            font-weight: bold;
            font-size: 12px;
        }
        .success
        {
            color: Green;
            font-weight: bold;
            font-size: 12px;
        }
        input
        {
            border: 1px solid #ccc;
            color: #333333;
            font-size: 12px;
            margin-top: 2px;
            padding: 3px;
            width: 200px;
        }
        .left-td
        {
            text-align: right;
            width: 52%;
            padding-left:100px;
            color:#16518A;
            
        }
        .rightField
        {
           
            text-align: left;
        }
        .spnRequiredField
        {
            color: #FF0033;
            font-size: x-small;
        }
    </style>
    <script type="text/javascript">
        function checkLoginId() {

            $("#hidValid").val("0");
            if ($("#<%=txtTradeNum.ClientID %>").val() == "") {
                $("#spnLoginStatus").html("");
                return;
            }
            $("#spnLoginStatus").html("<img src='Images/loader.gif' />");
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                url: "ControlHost.aspx/CheckTradeNoAvailability",
                data: "{ 'TradeAccNo': '" + $("#<%=txtTradeNum.ClientID %>").val() + "','BrokerCode': '" + $("#<%=ddlBrokerCode.ClientID %>").val() + "','PortfolioId': '" + $("#<%=ddlPortfolio.ClientID %>").val() + "' }",
                error: function(xhr, status, error) {
                    alert("Sorry. Something went wrong!");
                },
                success: function(msg) {

                    if (msg.d) {
                        $("#hidValid").val("1");
                        $("#spnLoginStatus").removeClass();
                        $("#spnLoginStatus").addClass("success");
                        $("#spnLoginStatus").html("");
                    }
                    else {
                        $("#hidValid").val("0");
                        $("#spnLoginStatus").removeClass();
                        $("#spnLoginStatus").addClass("error");
                        $("#spnLoginStatus").html("This trade account exists.");
                    }
                }

            });
        }
        function isValid() {
           
            if (document.getElementById('hidValid').value == '1') {
                Page_ClientValidate();
                return Page_IsValid;
            }
            else {
                Page_ClientValidate();
                alert('Your Selected Trade Number is not available. Please choose some other Trade Number');
                return false;
            }
        }
</script>
            

<script type="text/javascript">


    function checkDate(sender, args) {

        var selectedDate = new Date();
        selectedDate = sender._selectedDate;

        var todayDate = new Date();
        var msg = "";

        if (selectedDate > todayDate) {
            sender._selectedDate = todayDate;
            sender._textbox.set_Value(sender._selectedDate.format(sender._format));
            alert("Warning! - Date Cannot be in the future");
        }
    }
    
</script>


<asp:ScriptManager ID="ScriptManager1" runat="server">
    <Services>
        <asp:ServiceReference Path="AutoComplete.asmx" />
    </Services>
</asp:ScriptManager>

<asp:UpdatePanel ID="upnl1" runat="server">
    <ContentTemplate>
    <table width="100%" class="TableBackground">
<tr>
        <td class="HeaderCell">
            <asp:Label ID="lblView" runat="server" CssClass="HeaderTextBig" Text="Add EQ Account"></asp:Label>
            <hr />
        </td>
    </tr>
</table>

    
    <table> <tr>
                <td class="tdRequiredText">
                    
                     <asp:Label ID="lblError" runat="server" CssClass="lblRequiredText" Text="Note: Fields marked with ' * ' are compulsory"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnkEdit" runat="server" CssClass="LinkButtons" 
                        OnClick="lnkEdit_Click" CommandName="EditClick" Visible="False">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkBack" runat="server" CommandName="BackClick" 
                        CssClass="LinkButtons" OnClick="lnkBack_Click" Visible="False">Back</asp:LinkButton>
                </td>
            </tr>
             <tr>
       <%-- <td class="leftField">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td colspan="4" class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>--%>
    </tr></table>
        <table width="100%" class="TableBackground">
           <tr>
                 <td class="leftField" align="right">
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td class="rightField">
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
            </tr>
           
            <tr>
                <td class="leftField" align="right">
                    <asp:Label ID="lblFolioNum0" runat="server" CssClass="FieldName" Text="Broker Code :"></asp:Label>
                </td>
                <td class="rightField">
                    <asp:DropDownList ID="ddlBrokerCode" AutoPostBack="true" runat="server" CssClass="cmbField" 
                        Height="16px" Width="274px">
                    </asp:DropDownList>
                    <span id="Span2" class="spnRequiredField">*</span>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlBrokerCode"
                        ErrorMessage="Please select an Broker Code" Operator="NotEqual" ValueToCompare="Select a Broker Code" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField" align="right">
                    <asp:Label ID="lblFolioNum" runat="server" CssClass="FieldName" Text="Trade Account Number : "></asp:Label>
                </td>
                <td class="rightField">
                    <asp:TextBox ID="txtTradeNum" runat="server" OnTextChanged="txtTradeNum_TextChanged" CssClass="txtField" AutoPostBack="true" MaxLength="15" onchange="checkLoginId()"></asp:TextBox>
                    <span id="Span3" class="spnRequiredField">*</span><span id="spnLoginStatus"></span>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtTradeNum"
                        ErrorMessage="Please enter the trade number" runat="server" CssClass="rfvPCG">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator10" runat="server" 
                        Type="String" ControlToValidate="txtTradeNum" Operator="DataTypeCheck" CssClass="cvPCG"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="leftField" align="right">
                    <asp:Label ID="lblAccountStartingDate" runat="server" CssClass="FieldName" Text="Account Starting Date:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtAccountStartingDate" runat="server" CssClass="txtField"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtAccountStartingDate_CalendarExtender" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="txtAccountStartingDate" OnClientDateSelectionChanged="checkDate">
                    </ajaxToolkit:CalendarExtender>
                    <ajaxToolkit:TextBoxWatermarkExtender ID="txtAccountStartingDate_TextBoxWatermarkExtender" 
                        runat="server" TargetControlID="txtAccountStartingDate" WatermarkText="dd/mm/yyyy">
                    </ajaxToolkit:TextBoxWatermarkExtender>
                   
                   
                    
                </td>
            </tr>
            <tr>
                <td class="leftField" align="right">
                    
                    
                    <asp:Label ID="lblBrokeragePerDelivery" runat="server" 
                        Text="Brokerage % for Delivery" CssClass="FieldName"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtBrokeragePerDelivery" runat="server" CssClass="txtField"></asp:TextBox>                    
                    <asp:RangeValidator ID="RangeValidator4" runat="server" 
                        ErrorMessage="Percentage should not be greater than 100" MaximumValue="100" 
                        MinimumValue="0" ControlToValidate="txtBrokeragePerDelivery" 
                        Type="Double" Display="Dynamic"></asp:RangeValidator>
                    
                </td>
            </tr>
            <tr>
                <td class="leftField" align="right">
                    
                    
                    <asp:Label ID="lblBrokeragePerSpeculative" runat="server" 
                        Text="Brokerage % for Speculative" CssClass="FieldName"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtBrokeragePerSpeculative" runat="server" CssClass="txtField"></asp:TextBox>                    
                    <asp:RangeValidator ID="RangeValidator2" runat="server" 
                        ErrorMessage="Percentage should not be greater than 100" MaximumValue="100" 
                        MinimumValue="0" ControlToValidate="txtBrokeragePerSpeculative" 
                        Type="Double"></asp:RangeValidator>
                        
                </td>
            </tr>
            <tr>
                <td class="leftField" align="right">
                    
                    
                    <asp:Label ID="lblOtherCharges" runat="server" Text="Other Charges(%)" 
                        CssClass="FieldName"></asp:Label>
                    
                </td>
                <td>
                    <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="txtField"></asp:TextBox>                    
                    <asp:RangeValidator ID="RangeValidator3" runat="server" 
                        ControlToValidate="txtOtherCharges" 
                        ErrorMessage="Percentage should not be greater than 100" MaximumValue="100" 
                        MinimumValue="0" Type="Double"></asp:RangeValidator>
                        
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr id="trDPAccount" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblDPAccounts" runat="server" CssClass="HeaderTextSmall" Text="DP Accounts"></asp:Label>
                    <hr />
                </td>
            </tr>
            <tr id="trDPAccountsGrid" runat="server">
                <td colspan="2">
                    <asp:GridView ID="gvDPAccounts" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        DataKeyNames="DpId" ForeColor="#333333" Width="624px" Height="78px" AllowSorting="True"
                        Font-Size="Small"  CssClass="GridViewStyle">
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
                                    <asp:CheckBox ID="chkId" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DP Name" HeaderText="DP Name" SortExpression="DP Name" />
                            <asp:BoundField DataField="DP Id" HeaderText="DP Id" SortExpression="DP Id" />
                        </Columns>
                    </asp:GridView>
                                                       
                </td>
            </tr>
            
     
            
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
<table width="100%">
<tr>
         <td colspan="2" class="SubmitCell">
            <asp:Button ID="btnSubmit" runat="server" CssClass="PCGButton" onmouseover="javascript:ChangeButtonCss('hover', 'ctrl_CustomerEQAccountAdd_btnSubmit', 'S');"
                onmouseout="javascript:ChangeButtonCss('out', 'ctrl_CustomerEQAccountAdd_btnSubmit', 'S');"
                Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return isValid()" />
           <asp:Button ID="btnUpdate" runat="server" CssClass="PCGButton" Text="Update" onclick="btnUpdate_Click" Visible="False" />
        </td>
      
    </tr>
    </table>

<input type="hidden" id="hidValid" />
<input type="hidden" id="hidStatus" runat="server" />
<table width="100%" class="TableBackground" border="1">
    
</table>
