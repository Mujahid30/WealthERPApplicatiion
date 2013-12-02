<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IPOIssueTransact.ascx.cs"
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

        if (typeof (Page_ClientValidate('btnSubmit')) == 'function') {
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
        <div class="divOnlinePageHeading">
            <div class="divClientAccountBalance">
                <asp:Label ID="Label1" runat="server" Text="Available Limits:" CssClass="BalanceLabel"> </asp:Label>               
                <asp:Label ID="lblAvailableLimits" runat="server" Text="" CssClass="BalanceAmount"></asp:Label>
            </div>
        </div>
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
        <div style="float: left; padding-top: 20px; padding-left: 20px;" id="divControlContainer"
            runat="server">
            <table width="100%">
                <tr>
                    <td colspan="4">
                        <telerik:RadGrid ID="RadGridIPOIssueList" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="false" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                            Skin="Telerik" AllowFilteringByColumn="false">
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" DataKeyNames="AIM_IssueId"
                                AutoGenerateColumns="false" Width="100%" PagerStyle-AlwaysVisible="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="AIM_IssueName" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="AIM_IssueName"
                                        SortExpression="AIM_IssueName">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FaceValue" HeaderStyle-Width="200px" HeaderText="Face Value"
                                        ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_CapPrice" HeaderStyle-Width="200px" HeaderText="Cap Price"
                                        ShowFilterIcon="false" UniqueName="AIM_CapPrice" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Floor Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FloorPrice" Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FixedPrice" HeaderStyle-Width="200px" HeaderText="Fixed Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FixedPrice" Visible="true">
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
                                    <telerik:GridBoundColumn DataField="AIM_TradingInMultipleOf" HeaderStyle-Width="200px"
                                        HeaderText="Trading In MultipleOf" ShowFilterIcon="false" UniqueName="AIM_TradingInMultipleOf"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_MinApplicationSize" HeaderStyle-Width="200px"
                                        HeaderText="Min App Size" ShowFilterIcon="false" UniqueName="AIM_MinApplicationSize"
                                        Visible="true">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_Rating" HeaderStyle-Width="200px" ShowFilterIcon="false"
                                        AutoPostBackOnFilter="true" HeaderText="Issue Name" UniqueName="AIM_Rating" SortExpression="AIM_Rating">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AIM_FloorPrice" HeaderStyle-Width="200px" HeaderText="Floor Price"
                                        ShowFilterIcon="false" UniqueName="AIM_FaceValue" Visible="true">
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
                        <telerik:RadGrid ID="RadGridIPOBid" runat="server" AllowSorting="True" enableloadondemand="True"
                            PageSize="10" AllowPaging="false" AutoGenerateColumns="False" EnableEmbeddedSkins="False"
                            GridLines="None" ShowFooter="true" PagerStyle-AlwaysVisible="false" ShowStatusBar="True"
                            Skin="Telerik" AllowFilteringByColumn="false">
                            <MasterTableView AllowMultiColumnSorting="True" AllowSorting="true" AutoGenerateColumns="false"
                                DataKeyNames="IssueBidNo" Width="100%" PagerStyle-AlwaysVisible="false">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="BidOptions" HeaderStyle-Width="200px" CurrentFilterFunction="Contains"
                                        ShowFilterIcon="true" AutoPostBackOnFilter="true" HeaderText="Bidding Options"
                                        UniqueName="BidOptions" SortExpression="BidOptions">
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        Visible="true" UniqueName="CheckCutOff" HeaderText="Cut-Off">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCutOffCheck" runat="server" AutoPostBack="true" OnCheckedChanged="CutOffCheckBox_Changed" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        UniqueName="BidQuantity" HeaderText="Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidQuantity" runat="server" Text='<%# Bind("BidQty")%>' CssClass="txtField"
                                                OnTextChanged="bidTextBoxs_TextChanged" AutoPostBack="true"> </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblBidQuantity"></asp:Label>
                                        </FooterTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        FooterText="" UniqueName="BidPrice" HeaderText="Price" FooterAggregateFormatString="{0:N2}">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidPrice" runat="server" CssClass="txtField" Text='<%# Bind("BidPrice")%>'
                                                OnTextChanged="bidTextBoxs_TextChanged" AutoPostBack="true"> </asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblBidPrice"></asp:Label>
                                        </FooterTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" DataField="" HeaderStyle-Width="100px"
                                        FooterText="" UniqueName="BidAmount" HeaderText="Amount" FooterAggregateFormatString="{0:N2}">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBidAmount" runat="server" ReadOnly="true" CssClass="txtField"
                                                BackColor="LightGray" Text='<%# Bind("BidAmount")%>'></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label runat="server" ID="lblBidAmount"></asp:Label>
                                        </FooterTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr class="spaceUnder" id="trTermsCondition" runat="server">
                    <td align="left" style="width: 15%">
                        <asp:CheckBox ID="chkTermsCondition" runat="server" Font-Bold="True" Font-Names="Shruti"
                            Enabled="false" Checked="false" ForeColor="#145765" Text="" ToolTip="Click 'Terms & Conditions' to proceed further"
                            CausesValidation="true" />
                        <asp:LinkButton ID="lnkTermsCondition" CausesValidation="false" Text="Terms & Conditions"
                            runat="server" CssClass="txtField" OnClick="lnkTermsCondition_Click" ToolTip="Click here to accept terms & conditions"></asp:LinkButton>
                        <span id="Span9" class="spnRequiredField">*</span>
                    </td>
                    <td colspan="3" style="width: 85%" align="left">
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please read terms & conditions"
                            ClientValidationFunction="ValidateTermsConditions" EnableClientScript="true"
                            OnServerValidate="TermsConditionCheckBox" Display="Dynamic" ValidationGroup="btnSubmit"
                            CssClass="rfvPCG">
                    Please read terms & conditions
                        </asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 10%">
                        <asp:Button ID="btnConfirmOrder" runat="server" Text="Confirm Order" OnClick="btnConfirmOrder_Click"
                            CssClass="PCGMediumButton" ValidationGroup="btnSubmit" />
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
                                    CausesValidation="false" ValidationGroup="btnSubmit" />
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
                                    ValidationGroup="btnSubmit" OnClientClick="return PreventClicks();"></asp:Button>
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
