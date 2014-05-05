﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IPOIssueTransact.ascx.cs"
    Inherits="WealthERP.OnlineOrderManagement.IPOIssueTransact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<script src="../Scripts/jquery.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.2.6.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery.min.js" type="text/javascript"></script>

<script src="../Scripts/jquery-1.3.1.min.js" type="text/javascript"></script>

<script src="../Scripts/jQuery.bubbletip-1.0.6.js" type="text/javascript"></script>

<script src="../Scripts/JScript.js" type="text/javascript"></script>

<script type="text/javascript">
    function ValidateTermsConditions(sender, args) {

        if (document.getElementById("<%=chkTermsCondition.ClientID %>").checked == true) {
            args.IsValid = true;
        } else {
            args.IsValid = false;
        }
    }
</script>

<script type="text/javascript">

    function closeCustomConfirm() {
        $find("<%=rw_customConfirm.ClientID %>").close();
    }

   
    
</script>

<script type="text/javascript">
    var crnt = 0;
    function PreventClicks() {

        if (typeof (Page_ClientValidate('btnConfirmOrder')) == 'function') {
            Page_ClientValidate();
        }

        if (Page_IsValid) {
            if (++crnt > 1) {
                return false;
            }
            return true;
        }
        else {
            return false;
        }
    }
</script>

<asp:ScriptManager ID="scrptMgr" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <div class="divOnlinePageHeading">
                        <div class="divClientAccountBalance">
                            <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"> </asp:Label>
                            <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <table class="tblMessage" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <div id="divMessage" align="center">
                    </div>
                    <div style="clear: both">
                    </div>
                </td>
            </tr>
        </table>
        <div id="divControlContainer" class="divControlContiner" runat="server">
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <telerik:RadGrid ID="RadGridIPOIssueList" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                            Skin="Telerik" AllowFilteringByColumn="false">
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId,AIIC_PriceDiscountType,AIIC_PriceDiscountValue"
                                AutoGenerateColumns="false" Width="100%" PagerStyle-AlwaysVisible="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="AIM_IssueName"
                                        SortExpression="AIM_IssueName">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_IssueSizeQty" HeaderStyle-Width="200px" HeaderText="Issue Size"
                                        ShowFilterIcon="false" UniqueName="AIM_IssueSizeQty" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_IssueSizeAmt" HeaderStyle-Width="200px" HeaderText="Issue Size Amount"
                                        ShowFilterIcon="false" UniqueName="AIM_IssueSizeAmt" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="200px" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" HeaderText="Grading" UniqueName="AIM_Rating" SortExpression="AIM_Rating">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_IsBookBuilding" HeaderStyle-Width="200px"
                                        ShowFilterIcon="false" AutoPostBackOnFilter="true" HeaderText="Basis" UniqueName="AIM_IsBookBuilding"
                                        SortExpression="AIM_IsBookBuilding">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="200px" HeaderText="Face Value"
                                        ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_MInQty" HeaderStyle-Width="200px" HeaderText="Minimum Qty"
                                        ShowFilterIcon="false" UniqueName="AIM_MInQty" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="200px"
                                        HeaderText="Multiples of Qty" ShowFilterIcon="false" UniqueName="AIM_TradingInMultipleOf"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_MaxQty" HeaderStyle-Width="200px" HeaderText="Maximum Qty"
                                        ShowFilterIcon="false" UniqueName="AIM_MaxQty" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Min Bid Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FloorPrice" Visible="true" DataType="System.Decimal"
                                        DataFormatString="{0:0.00}">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_CapPrice" HeaderStyle-Width="200px" HeaderText="Max Bid Price"
                                        ShowFilterIcon="false" UniqueName="AIM_CapPrice" Visible="true" DataType="System.Decimal"
                                        DataFormatString="{0:0.00}">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FixedPrice" HeaderStyle-Width="200px" HeaderText="Max Bid Price"
                                        Visible="false" ShowFilterIcon="false" UniqueName="AIM_FixedPrice">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_OpenDate" HeaderStyle-Width="200px" HeaderText="Open Date"
                                        ShowFilterIcon="false" UniqueName="AIM_OpenDate" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_CloseDate" HeaderStyle-Width="200px" HeaderText="Open Date"
                                        ShowFilterIcon="false" UniqueName="AIM_CloseDate" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DiscountType" HeaderStyle-Width="200px" HeaderText="Discount Type"
                                        ShowFilterIcon="false" UniqueName="DiscountType" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DiscountValue" HeaderStyle-Width="200px" HeaderText="Discount Value/Bid Qnt"
                                        ShowFilterIcon="false" UniqueName="DiscountValue" Visible="true" DataFormatString="{0:0.00}">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:ValidationSummary ID="vsSummary" runat="server" CssClass="rfvPCG" Visible="true"
                            ValidationGroup="btnConfirmOrder" ShowSummary="true" DisplayMode="BulletList" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <telerik:RadGrid ID="RadGridIPOBid" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="true" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                            Skin="Telerik" AllowFilteringByColumn="false" OnItemDataBound="RadGridIPOBid_ItemDataBound">
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                DataKeyNames="IssueBidNo" Width="100%" PagerStyle-AlwaysVisible="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="BidOptions" HeaderStyle-Width="120px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Bidding Options"
                                        UniqueName="BidOptions" SortExpression="BidOptions">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="80px"
                                        Visible="true" UniqueName="CheckCutOff" HeaderText="Cut-Off" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCutOffCheck" runat="server" Visible='<%# (Convert.ToInt32(Eval("IssueBidNo")) == 1)? true: false %>'
                                                AutoPostBack="true" OnCheckedChanged="CutOffCheckBox_Changed" />
                                            <a href="#" class="popper" data-popbox="divCutOffCheck">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                            <div id="divCutOffCheck" class="popbox">
                                                <h2>
                                                    CUT-OFF!</h2>
                                                <p>
                                                    1)If this box is checked then price filed will auto fill with Max Bid Price.</p>
                                            </div>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        UniqueName="BidQuantity" HeaderText="Quantity" FooterAggregateFormatString="{0:N2}"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidQuantity" runat="server" Text='<%# Bind("BidQty")%>' CssClass="txtField"
                                                OnTextChanged="BidQuantityPrice_TextChanged" AutoPostBack="true" onkeypress="return isNumberKey(event)"> </asp:TextBox>
                                            <a href="#" class="popper" data-popbox="divBidQuantity">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                            <div id="divBidQuantity" class="popbox">
                                                <h2>
                                                    BID-QUANTITY!</h2>
                                                <p>
                                                    1)Please enter value between MinQantity and MaxQantity.</p>
                                            </div>
                                            <asp:RangeValidator ID="rvQuantity" runat="server" ControlToValidate="txtBidQuantity"
                                                ValidationGroup="btnConfirmOrder" Type="Integer" CssClass="rfvPCG" Text="*" ErrorMessage="BidQuantity should be between MinQantity and MaxQantity"
                                                Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="revtxtBidQuantity" ControlToValidate="txtBidQuantity"
                                                runat="server" ErrorMessage="Please enter a valid bid quantity" Text="*" Display="Dynamic"
                                                ValidationExpression="[0-9]*" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RegularExpressionValidator>                                            
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        FooterText="" UniqueName="BidPrice" HeaderText="Price" FooterAggregateFormatString="{0:N2}"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidPrice" runat="server" CssClass="txtField" Text='<%# Bind("BidPrice")%>'
                                                AutoPostBack="true" OnTextChanged="BidQuantityPrice_TextChanged" onkeypress="return isNumberKey(event)"> </asp:TextBox>
                                            <a href="#" class="popper" data-popbox="divBidPrice">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a>
                                            <div id="divBidPrice" class="popbox">
                                                <h2>
                                                    BID-PRICE!</h2>
                                                <p>
                                                    1)Please enter value between Min Bid Price and Min Max Price.
                                                    <br />
                                                    2)In case of cutoff cheked Max Bid price will be use for same field</p>
                                            </div>
                                            <asp:RangeValidator ID="rvBidPrice" runat="server" ControlToValidate="txtBidPrice"
                                                ValidationGroup="btnConfirmOrder" Type="Double" CssClass="rfvPCG" Text="*" ErrorMessage="BidPrice should be between Min Bid Price and Min Max Price"
                                                Display="Dynamic" />
                                            <asp:RegularExpressionValidator ID="revtxtBidPrice" ControlToValidate="txtBidPrice"
                                                runat="server" ErrorMessage="Please enter a valid bid price" Text="*" Display="Dynamic"
                                                ValidationExpression="^\d+(\.\d{1,2})?$" CssClass="rfvPCG" ValidationGroup="btnConfirmOrder"></asp:RegularExpressionValidator>
                                           
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblBidHighestValue" Text="Highest Bid Value"></asp:Label>
                                        </FooterTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        FooterText="" UniqueName="BidAmountPayable" HeaderText="Amount Payable" FooterAggregateFormatString="{0:N2}"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidAmountPayable" runat="server" ReadOnly="true" CssClass="txtDisableField"
                                                Text='<%# Bind("BidAmountPayable")%>'></asp:TextBox>
                                        
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblFinalBidAmountPayable" Text="0"></asp:Label>
                                            <asp:TextBox ID="txtFinalBidValue" runat="server" CssClass="txtField" Text="0" Visible="false">
                                            </asp:TextBox>
                                        </FooterTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        FooterText="" UniqueName="BidAmount" HeaderText="Amount Bid" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidAmount" runat="server" ReadOnly="true" CssClass="txtDisableField"
                                                Text='<%# Bind("BidAmount")%>'></asp:TextBox>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr class="spaceUnder" id="trTermsCondition" runat="server">
                    <td align="left" style="width: 30%">
                        <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                            Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                            CausesValidation="true" />
                        <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                            runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                        <span id="Span9" class="spnRequiredField">*</span>
                    </td>
                    <td colspan="3" style="width: 70%" align="left">
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                            ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                            OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnConfirmOrder"
                            CssClass="rfvPCG">
                    Please read terms & conditions
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10%">
                        <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order" OnClick="btnConfirmOrder_Click"
                            CssClass="PCGMediumButton" ValidationGroup="btnConfirmOrder" />
                    </td>
                    <td colspan="3" style="width: 90%">
                    </td>
                </tr>
            </table>
        </div>
        <telerik:RadWindow ID="rwTermsCondition" runat="server" VisibleOnPageLoad="false"
            Width="1000px" Modal="true" BackColor="#DADADA" VisibleStatusbar="false" Behaviors="Move, Resize,Close"
            Title="Terms & Conditions" EnableShadow="true" Left="580" Top="-8">
            <ContentTemplate>
                <div style="padding: 0px; width: 100%">
                    <table width="100%" cellpadding="0" cellpadding="0">
                        <tr>
                            <td align="left">
                                <%--  <a href="../ReferenceFiles/MF-Terms-Condition.html">../ReferenceFiles/MF-Terms-Condition.html</a>--%>
                                <iframe src="../ReferenceFiles/MF-Terms-Condition.html" name="iframeTermsCondition"
                                    style="width: 100%"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnAccept" runat="server" Text="Accept" CssClass="PCGButton" OnClick="btnAccept_Click"
                                    CausesValidation="false" ValidationGroup="btnConfirmOrder" />
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentTemplate>
        </telerik:RadWindow>
        <telerik:RadWindowManager runat="server" ID="RadWindowManager1">
            <Windows>
                <telerik:RadWindow ID="rw_customConfirm" Modal="true" Behaviors="Close, Move" VisibleStatusbar="false"
                    Width="700px" Height="160px" runat="server" Title="EUIN Confirm">
                    <ContentTemplate>
                        <div class="rwDialogPopup radconfirm">
                            <div class="rwDialogText">
                                <asp:Label ID="confirmMessage" Text="" runat="server" />
                            </div>
                            <div>
                                <asp:Button runat="server" ID="rbConfirm_OK" Text="OK" OnClick="rbConfirm_OK_Click"
                                    ValidationGroup="btnConfirmOrder" OnClientClick="return PreventClicks();"></asp:Button>
                                <asp:Button runat="server" ID="rbConfirm_Cancel" Text="Cancel" OnClientClicked="closeCustomConfirm">
                                </asp:Button>
                            </div>
                        </div>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </ContentTemplate>
    <Triggers>
    </Triggers>
</asp:UpdatePanel>
