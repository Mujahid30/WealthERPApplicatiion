<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewMutualFundPortfolio.ascx.cs"
    Inherits="WealthERP.CustomerPortfolio.ViewMutualFundPortfolio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" src="../Scripts/JScript.js"></script>

<script type="text/javascript">

    var tabberOptions = { 'onClick': function(argsObj) {

        var t = argsObj.tabber; /* Tabber object */
        var id = t.id; /* ID of the main tabber DIV */
        var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
        var e = argsObj.event; /* Event object */
        if (i == 3) {
            document.getElementById('<%= imgBtnExport.ClientID  %>').style.visibility = 'hidden';
        }
        else {
            document.getElementById('<%= imgBtnExport.ClientID  %>').style.visibility = 'visible';
        }

        document.getElementById('<%= hdnSelectedTab.ClientID %>').value = i;
    }
    };
</script>

<script type="text/javascript">

    function checkDateSelection() {
        
        var txtDate = document.getElementById('<%= txtPickDate.ClientID %>').value;
        if (txtDate == 'dd/mm/yyyy') {
            alert('Please pick the date..!!');
        }
    }

</script>

<script type="text/javascript" src="../Scripts/tabber.js"></script>

<script language="javascript" type="text/javascript">
    function Print_Click(div, btnID) {
        var ContentToPrint = document.getElementById(div);
        var myWindowToPrint = window.open('', '', 'width=700,height=300,toolbar=0,scrollbars=1,status=0,resizable=1,location=0,directories=0');
        myWindowToPrint.document.write(document.getElementById(div).innerHTML);
        myWindowToPrint.document.close();
        myWindowToPrint.focus();
        myWindowToPrint.print();
        myWindowToPrint.close();

        var btn = document.getElementById(btnID);
        btn.click();
    }
    function AferExportAll(btnID) {
        var btn = document.getElementById(btnID);
        btn.click();
    }


    function setFormat(format) {

        document.getElementById('<%= hdnDownloadFormat.ClientID %>').value = format;
        btn = document.getElementById('<%= btnExportExcel.ClientID %>');
        btn.click();
    }
</script>

<table>
    <asp:ScriptManager ID="scrptMgr" runat="server">
    </asp:ScriptManager>
    <tr>
        <td>
            <asp:ImageButton ID="imgBtnExport" ImageUrl="~/App_Themes/Maroon/Images/Export_Excel.png"
                runat="server" AlternateText="Excel" ToolTip="Export To Excel" OnClick="imgBtnExport_Click"
                Height="25px" Width="25px" />
            <asp:ImageButton ID="imgBtnWord" ImageUrl="~/Images/Export_Word.jpg" runat="server"
                AlternateText="Word" ToolTip="Export To Word" OnClick="imgBtnWord_Click" Visible="false" />
            <asp:ImageButton ID="imgBtnPdf" ImageUrl="~/Images/Export_Pdf.gif" runat="server"
                AlternateText="PDF" ToolTip="Export To PDF" OnClick="imgBtnPdf_Click" Visible="false" />
            <asp:ImageButton ID="imgBtnPrint" ImageUrl="~/Images/Print.gif" runat="server" AlternateText="Print"
                ToolTip="Print" OnClick="imgBtnPrint_Click" Visible="false"/>
            <asp:Button ID="btnPrintGrid" runat="server" Text="" OnClick="btnPrintGrid_Click"
                BorderStyle="None" BackColor="Transparent" ToolTip="Print" Visible="false"/>
            <asp:Button class="ExportButton" ID="btnExportExcel" runat="server" Style="display: none"
                OnClick="btnExportExcel_Click" Height="31px" Width="35px" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPortfolio" runat="server" CssClass="FieldName" Text="Portfolio Name:"></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddlPortfolio" runat="server" CssClass="cmbField" AutoPostBack="true"
                OnSelectedIndexChanged="ddlPortfolio_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMFDate" runat="server" CssClass="FieldName" Text="Valuation Date"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblPickDate" runat="server" Text="Pick date :" CssClass="FieldName"></asp:Label>
        </td>
        <td>
            <asp:TextBox ID="txtPickDate"  runat="server" CssClass="cmbField" ></asp:TextBox>
            <cc1:CalendarExtender ID="txtPickDate_CalendarExtender" runat="server" TargetControlID="txtPickDate"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <cc1:TextBoxWatermarkExtender ID="txtPickDate_TextBoxWatermarkExtender" runat="server"
                            TargetControlID="txtPickDate" WatermarkText="dd/mm/yyyy">
                        </cc1:TextBoxWatermarkExtender>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnGo" runat="server" onfocus="checkDateSelection()" Text="Go" CssClass="PCGButton" 
                onclick="btnGo_Click" />
        </td>
    </tr>
</table>

  <asp:Panel ID="tbl" runat="server" class="Landscape" Width="100%" ScrollBars="Horizontal">
<table style="width: 100%;">
    <tr>
        <td>
            <div class="tabber" id="divMain">
                <div class="tabbertab tabbertabdefault" runat="server" id="divNotional">
                    <h6 class="HeaderText">
                        Holdings</h6>
              
                    <table width="100%" cellspacing="0" cellpadding="0">
                        <tr>
                             <td class="rightField" width="100%">
                                <div id="dvNotionalPortfolio" runat="server">
                                    <asp:Label ID="lblMessageNotional" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label>
                                    <asp:Label ID="lblMessageForCategoryNotional" Visible="false" Text="No Record Exists for this category"
                                        runat="server" CssClass="Field"></asp:Label><asp:LinkButton ID="lnkGetBackNotionalLink"
                                            runat="server" OnClick="GetBackNotionalLink_Click" Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                    <asp:GridView ID="gvMFPortfolioNotional" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="SI.No" EnableViewState="true" HorizontalAlign="Center" CssClass="GridViewStyle"
                                        ShowFooter="True" OnSorting="gvMFPortfolioNotional_Sorting" OnRowCommand="gvMFPortfolioNotional_RowCommand"
                                        OnPageIndexChanging="gvMFPortfolioNotional_PageIndexChanging" Width="100%" OnDataBound="gvMFPortfolioNotional_DataBound"
                                        OnRowDataBound="gvMFPortfolioNotional_RowDataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="SI.No" HeaderText="SI.No" Visible="false" />
                                            <%--<asp:BoundField DataField="Folio Num" HeaderText="Folio Num"  />--%>
                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Transaction Type">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategory" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                                        OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryHeader" runat="server" Text='<%# Eval("AssetInstrumentCategoryName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblNotionalName" runat="server" Text="Scheme"></asp:Label>
                                                    <asp:TextBox ID="txtNotionalSchemeNameSearch" runat="server" CssClass="GridViewtxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewMutualFundPortfolio_btnPortfolioNotionalSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotionalNameHeader" runat="server" Text='<%# Eval("FundDescription").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblNotionalFolio" runat="server" Text="Folio No"></asp:Label>
                                                    <asp:TextBox ID="txtNotionalFolioSearch" runat="server" CssClass="GridViewtxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewMutualFundPortfolio_btnPortfolioNotionalSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNotionalFolioHeader" runat="server" Text='<%# Eval("FolioNum").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TransactionDate" HeaderText="FolioStartDate" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Wrap="false" />
                                            <%--<asp:BoundField DataField="Fund Description" HeaderText="Scheme Name" SortExpression="Fund Description" />--%>
                                            <asp:BoundField DataField="CurrentHoldings" HeaderText="Units" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="AcqCostExclDivReinvst" HeaderText="Acq Cost" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="CostOfAcquisition" HeaderText="Net Acq Cost" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="CurrentNAV" HeaderText="Curr NAV" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="CurrentValue" HeaderText="Curr Value" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="DividendPayout" HeaderText="Div Payout(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="DividendReinvested" HeaderText="Div Reinvstd(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="DividendTotal" HeaderText="Div Total(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="UnRealizedPL" HeaderText="UnRealized P/L" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                                <asp:BoundField DataField="TotalPL" HeaderText="Total P/L" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="AbsoluteReturn" HeaderText="Absolute Return" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STCG" HeaderText="Eligible STCG" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LTCG" HeaderText="Eligible LTCG" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                  
                </div>
                <div class="tabbertab" runat="server" id="divAll">
                    <h6 class="HeaderText">
                        All</h6>
                    <table id="tblAll" runat="server">
                        <tr>
                            <td>
                                <div id="dvMFPortfolio" runat="server">
                                    <asp:Label ID="lblMessageAll" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label><asp:LinkButton ID="lnlGoBackAll"
                                            runat="server" OnClick="GoBackAllLink_Click" Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                    <asp:GridView ID="gvMFPortfolio" DataKeyNames="SI.No" EnableViewState="true" CssClass="GridViewStyle"
                                        runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowCommand="gvMFPortfolio_RowCommand"
                                        OnPageIndexChanging="gvMFPortfolio_PageIndexChanging" OnDataBound="gvMFPortfolio_DataBound"
                                        OnRowDataBound="gvMFPortfolio_RowDataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="SI.No" HeaderText="SI.No" Visible="false" ItemStyle-HorizontalAlign="Right">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Transaction Type">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblCategoryAll" runat="server" Text="Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategoryAll" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                                        OnSelectedIndexChanged="ddlCategoryAll_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryRealizedHeader" runat="server" Text='<%# Eval("PortfolioCategoryName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                            </asp:TemplateField>                                            
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblName" runat="server" Text="Scheme"></asp:Label>
                                                    <asp:TextBox ID="txtSchemeNameSearch" runat="server" CssClass="GridViewtxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewMutualFundPortfolio_btnPortfolioSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNameHeader" runat="server" Text='<%# Eval("FundDescription").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblFolio" runat="server" Text="Folio No"></asp:Label>
                                                    <asp:TextBox ID="txtFolioSearch" runat="server" CssClass="GridViewtxtField" onkeydown="return JSdoPostback(event,'ctrl_ViewMutualFundPortfolio_btnPortfolioSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFolioHeader" runat="server" Text='<%# Eval("FolioNum").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CurrentHoldings" HeaderText="Current Units" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AveragePrice" HeaderText="Avg Price" HtmlEncode="false"
                                                ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AcqCostExclDivReinvst" HeaderText="Acq Cost" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false" />
                                            <asp:BoundField DataField="CostOfAcquisition" HeaderText="Net Acq Cost" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CurrentNAV" HeaderText="Curr NAV" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CurrentValue" HeaderText="Curr Value" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                             <asp:BoundField DataField="unitssold" HeaderText="Units Sold" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="costofsales" HeaderText="Cost of Sales" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="salesproceeds" HeaderText="Sales Proceeds" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DividendPayout" HeaderText="Div Payout(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DividendReinvested" HeaderText="Div Reinvstd(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DividendIncome" HeaderText="Div Total(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UnRealizedPL" HeaderText="UnRealized P/L (Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RealizedPL" HeaderText="Realized P/L (Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TotalPL" HeaderText="Total P/L (Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AbsReturn" HeaderText="Abs Return (%)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%--  This is the Set of things that is working for Realized Tab--%>
                
                
                <%-- 
                =======================================================================================================
                <comments> In this Realized grid Dividend Income have been relabled to Dividend Total.  But Logic is same                
                </comments>
                =======================================================================================================
                --%>
                <div class="tabbertab" runat="server" id="divRealized">
                    <h6 class="HeaderText" >
                        Realized</h6>
                    <table>
                        <tr>
                            <td>
                                <div id="dvRealizedPortfolio" runat="server">
                                    <asp:Label ID="lblMessageRealized" Visible="false" Text="No Record Exists" runat="server"
                                        CssClass="Field"></asp:Label><asp:LinkButton ID="lnkGoBackRealized"
                                            runat="server" OnClick="GoBackRealizedLink_Click" Visible="false" CssClass="FieldName">Go Back</asp:LinkButton>
                                    <asp:GridView ID="gvMFPortfolioRealized" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="SI.No" EnableViewState="true" CssClass="GridViewStyle"
                                        ShowFooter="True" OnSorting="gvMFPortfolioRealized_Sorting" OnRowDataBound="gvMFPortfolioRealized_RowDataBound"
                                        OnRowCommand="gvMFPortfolioRealized_RowCommand" OnPageIndexChanging="gvMFPortfolioRealized_PageIndexChanging"
                                        OnDataBound="gvMFPortfolioRealized_DataBound">
                                        <RowStyle CssClass="RowStyle" />
                                        <FooterStyle CssClass="FooterStyle" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PagerStyle" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <HeaderStyle CssClass="HeaderStyle" />
                                        <EditRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderText="Select" ShowHeader="True" Text="Select" />
                                            <asp:BoundField DataField="SI.No" HeaderText="SI.No" Visible="false" />
                                            <%--<asp:BoundField DataField="Folio Num" HeaderText="Folio Num" 
                                ItemStyle-HorizontalAlign="Right">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                            </asp:BoundField>--%>
                                            <asp:TemplateField ItemStyle-Wrap="false" HeaderText="Transaction Type">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblCategoryRealized" runat="server" Text="Category"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategoryRealized" AutoPostBack="true" runat="server" CssClass="GridViewCmbField"
                                                        OnSelectedIndexChanged="ddlCategoryRealized_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryRealizedHeader" runat="server" Text='<%# Eval("RealizedCategoryName").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="False"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblRealizedName" runat="server" Text="Scheme"></asp:Label>
                                                    <asp:TextBox ID="txtRealizedSchemeNameSearch" runat="server" CssClass="txtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewMutualFundPortfolio_btnPortfolioRealizedSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRealizedNameHeader" runat="server" Text='<%# Eval("FundDescription").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Fund Description" HeaderText="Scheme Name" 
                                ItemStyle-Width="325" />--%>
                                            <asp:TemplateField ItemStyle-Wrap="false">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblRealizedFolio" runat="server" Text="Folio No"></asp:Label>
                                                    <asp:TextBox ID="txtRealizedFolioSearch" runat="server" CssClass="GridViewtxtField"
                                                        onkeydown="return JSdoPostback(event,'ctrl_ViewMutualFundPortfolio_btnPortfolioRealizedSearch');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRealizedFolioHeader" runat="server" Text='<%# Eval("FolioNum").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SalesQty" HeaderText="Units Sold" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                                 <asp:BoundField DataField="CostOfSales" HeaderText="Cost Of Sales (Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RealizedSalesProceeds" HeaderText="Sale Proceeds" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                       
                                            <asp:BoundField DataField="DividendPayout" HeaderText="Div Payout(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DividendReinvested" HeaderText="Div Reinvstd(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DividendIncome" HeaderText="Div Total(Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="RealizedPL" HeaderText="Realized P/L (Rs)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Absolute Return" HeaderText="Abs Ret" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>                                            
                                            <asp:BoundField DataField="XIRR" HeaderText="XIRR (%)" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STCG" HeaderText="STCG" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LTCG" HeaderText="LTCG" ItemStyle-HorizontalAlign="Right"
                                                HeaderStyle-Wrap="false">
                                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Div1" class="tabbertab" runat="server">
                    <h6 class="HeaderText" >
                        Performance and Analysis</h6>
                    <table id="Table1" runat="server">
                        <tr id="trMFCode" runat="server">
                            <td>
                                <asp:DropDownList ID="ddlMFClassificationCode" runat="server" CssClass="cmbField"
                                    Height="16px" Width="176px">
                                    <asp:ListItem>MF Classification Code</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="trChart" runat="server">
                            <td>
                                <div id="Div2" runat="server">
                                    <asp:Chart ID="chrtMFClassification" runat="server" BackColor="Transparent" Palette="Pastel" Width="500px" Height="250px">
                                        <Series>
                                            <asp:Series Name="seriesMFC">
                                            </asp:Series>
                                        </Series>
                                        <ChartAreas>
                                            <asp:ChartArea Name="careaMFC">
                                            </asp:ChartArea>
                                        </ChartAreas>
                                    </asp:Chart>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <!--  <asp:Button ID="btnUpdateNP" runat="server" OnClick="btnUpdateNP_Click" Text="Update Net Position"
                Visible="False" CssClass="ButtonField" Width="162px" />-->
        </td>
    </tr>
    <tr>
        <td>
           
        </td>
    </tr>
    <%-- <tr>
        <td colspan="3">
            <div>
                <table style="width: 30%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" CssClass="HeaderTextSmall" Text="Mutual Fund Portfolio Summary"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                  <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="FieldName" Text="Current Holdings"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentHolding" runat="server" CssClass="Field" Text="lblCurrentHolding"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
               <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" CssClass="FieldName" Text="Cost of Acquisition (Rs) :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCostOfAcqusition" runat="server" CssClass="Field" Text="lblCostOfAcqusition"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" CssClass="FieldName" Text="Current Value (Rs) :"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentValue" runat="server" CssClass="Field" Text="lblCurrentValue"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>--%>
</table>
</asp:Panel>
<table>
<tr>
        <td colspan="3">
            <asp:Label ID="lblPortfolioMsg" runat="server" CssClass="HeaderTextSmall" Text="** Folios with Broker code Change in/out transactions"></asp:Label>
        </td>
    </tr>
    </table>
    
<table width="100%">
    <tr>
        <td align="center">
            <div id="msgRecordStatus" runat="server" class="warning-msg" align="center" visible="false">
                Valuation is not done for selected date...!
            </div>
       </td>
   </tr>
</table>

<asp:Button ID="btnPortfolioSearch" runat="server" Text="" OnClick="btnPortfolioSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnPortfolioRealizedSearch" runat="server" Text="" OnClick="btnPortfolioRealizedSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:Button ID="btnPortfolioNotionalSearch" runat="server" Text="" OnClick="btnPortfolioNotionalSearch_Click"
    BorderStyle="None" BackColor="Transparent" />
<asp:HiddenField ID="hdnFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSchemeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedSchemeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNotionalFolioFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNotionalSchemeFilter" runat="server" Visible="false" />
<asp:HiddenField ID="hdnPortfolioSort" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedSort" runat="server" Visible="false" />
<asp:HiddenField ID="hdnNotionalSort" runat="server" Visible="false" />
<asp:HiddenField ID="hdnSort" runat="server" Visible="false" />
<asp:HiddenField ID="hdnDownloadPageType" runat="server" Visible="true" />
<asp:HiddenField ID="hdnDownloadFormat" runat="server" Visible="true" />
<asp:HiddenField ID="hdnSelectedTab" runat="server" Visible="true" />
<asp:HiddenField ID="hdnSelectedCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnRealizedSelectedCategory" runat="server" Visible="false" />
<asp:HiddenField ID="hdnAllSelectedCategory" runat="server" Visible="false" />

<script type="text/javascript">
    document.getElementById("<%= divAll.ClientID %>").style.display = 'none';
    document.getElementById("<%= Div1.ClientID %>").style.display = 'none';
    document.getElementById("<%= divRealized.ClientID %>").style.display = 'none';
    
    if (document.getElementById('<%= hdnSelectedTab.ClientID %>').value == "" || document.getElementById('<%= hdnSelectedTab.ClientID %>').value == "3") {
        document.getElementById('<%= hdnSelectedTab.ClientID %>').value = 0;
    }
</script>

